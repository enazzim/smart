package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.domain.inventory.LotGenealogyDirection;
import com.shindong.smartmanager.domain.inventory.LotGenealogyLinkType;
import com.shindong.smartmanager.domain.inventory.LotOriginType;
import com.shindong.smartmanager.domain.inventory.LotStatus;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

/**
 * Lot 마스터 CRUD·채번·입고 시 생성. 재고 슬롯 갱신({@code LotInventoryService})은 V079/LOT-2+.
 */
public class LotService {

    public static final String SOURCE_DOC_WORK_REPORT = "work_report";

    private final LotRepository lotRepository;
    private final ItemRepository itemRepository;

    public LotService(LotRepository lotRepository, ItemRepository itemRepository) {
        this.lotRepository = lotRepository;
        this.itemRepository = itemRepository;
    }

    public List<LotView> list(LotListCriteria criteria) {
        return lotRepository.findAllActive(criteria);
    }

    public LotView get(long id) {
        return requireLot(id);
    }

    public LotView create(CreateLotCommand command, String actorUserId) {
        ItemView item = requireItem(command.itemId());
        if (!item.lotTracked()) {
            throw new IllegalArgumentException("Lot 추적 품목이 아닙니다: " + item.itemNo());
        }
        LotOriginType originType = command.originType() != null ? command.originType() : LotOriginType.MANUAL;
        String lotNo = resolveLotNo(item, command.lotNo(), command.autoGenerate());
        assertLotNoUnique(item.id(), lotNo);
        return lotRepository.saveNew(
                item.id(),
                lotNo,
                LotStatus.ACTIVE,
                originType,
                trimToNull(command.originDocType()),
                command.originDocId(),
                trimToNull(command.p1()),
                trimToNull(command.p2()),
                command.expiryDate(),
                trimToNull(command.certificateRef()),
                trimToNull(command.remark()),
                actorUserId
        );
    }

    public LotView update(long id, UpdateLotCommand command, String actorUserId) {
        requireLot(id);
        if (command.status() == null) {
            throw new IllegalArgumentException("Lot 상태는 필수입니다.");
        }
        return lotRepository.update(
                id,
                command.status(),
                trimToNull(command.p1()),
                trimToNull(command.p2()),
                command.expiryDate(),
                trimToNull(command.certificateRef()),
                trimToNull(command.remark()),
                actorUserId
        );
    }

    public void delete(long id, String actorUserId) {
        LotView lot = requireLot(id);
        List<LotBalanceView> balances = lotRepository.findBalancesByLotId(lot.id());
        boolean hasQty = balances.stream()
                .anyMatch(b -> b.qtyOnHand() != null && b.qtyOnHand().compareTo(BigDecimal.ZERO) > 0);
        if (hasQty) {
            throw new IllegalArgumentException("잔량이 있는 Lot는 삭제할 수 없습니다.");
        }
        lotRepository.softDelete(id, actorUserId);
    }

    public LotView createOnReceipt(
            long itemId,
            String lotNo,
            LotOriginType originType,
            String originDocType,
            Long originDocId,
            String actorUserId
    ) {
        ItemView item = requireItem(itemId);
        if (!item.lotTracked()) {
            throw new IllegalArgumentException("Lot 추적 품목이 아닙니다: " + item.itemNo());
        }
        String normalized = requireLotNo(lotNo);
        return lotRepository.findActiveByItemIdAndLotNo(item.id(), normalized)
                .orElseGet(() -> lotRepository.saveNew(
                        item.id(),
                        normalized,
                        LotStatus.ACTIVE,
                        originType != null ? originType : LotOriginType.PURCHASE,
                        trimToNull(originDocType),
                        originDocId,
                        null,
                        null,
                        null,
                        null,
                        null,
                        actorUserId
                ));
    }

    /**
     * 작업실적 산출 Lot 생성.
     */
    public LotView createOnProduction(
            long itemId,
            String lotNo,
            boolean autoGenerate,
            String originDocType,
            Long originDocId,
            String actorUserId
    ) {
        ItemView item = requireItem(itemId);
        if (!item.lotTracked()) {
            throw new IllegalArgumentException("Lot 추적 품목이 아닙니다: " + item.itemNo());
        }
        String resolved = resolveLotNo(item, lotNo, autoGenerate);
        return lotRepository.findActiveByItemIdAndLotNo(item.id(), resolved)
                .orElseGet(() -> lotRepository.saveNew(
                        item.id(),
                        resolved,
                        LotStatus.ACTIVE,
                        LotOriginType.PRODUCTION,
                        trimToNull(originDocType),
                        originDocId,
                        null,
                        null,
                        null,
                        null,
                        null,
                        actorUserId
                ));
    }

    /**
     * 작업실적 등록 후 투입→산출 genealogy 기록.
     * 각 부모 Lot에 대해 CONSUME·PRODUCE 링크를 남긴다 (동일 TX, 취소 시 소프트 무효화).
     */
    public void recordWorkReportGenealogy(
            long workReportId,
            Long outputLotId,
            List<LotGenealogyParentQty> parents,
            String actorUserId
    ) {
        if (outputLotId == null || parents == null || parents.isEmpty()) {
            return;
        }
        requireLot(outputLotId);
        for (LotGenealogyParentQty parent : parents) {
            if (parent == null || parent.qty() == null || parent.qty().compareTo(BigDecimal.ZERO) <= 0) {
                continue;
            }
            if (parent.parentLotId() == outputLotId) {
                continue;
            }
            requireLot(parent.parentLotId());
            lotRepository.saveGenealogyLink(
                    parent.parentLotId(),
                    outputLotId,
                    LotGenealogyLinkType.CONSUME,
                    parent.qty(),
                    null,
                    SOURCE_DOC_WORK_REPORT,
                    workReportId,
                    actorUserId
            );
            lotRepository.saveGenealogyLink(
                    parent.parentLotId(),
                    outputLotId,
                    LotGenealogyLinkType.PRODUCE,
                    parent.qty(),
                    null,
                    SOURCE_DOC_WORK_REPORT,
                    workReportId,
                    actorUserId
            );
        }
    }

    public void deactivateWorkReportGenealogy(long workReportId) {
        lotRepository.softDeactivateGenealogyBySourceDoc(SOURCE_DOC_WORK_REPORT, workReportId);
    }

    public List<LotGenealogyLinkView> findGenealogy(long lotId, LotGenealogyDirection direction) {
        requireLot(lotId);
        if (direction == null) {
            throw new IllegalArgumentException("genealogy direction은 필수입니다 (UP|DOWN).");
        }
        return lotRepository.findGenealogy(lotId, direction);
    }

    public List<StockMovementListItemView> findMovements(long lotId) {
        requireLot(lotId);
        return lotRepository.findMovementsByLotId(lotId);
    }

    public LotView resolveOrCreate(long itemId, String lotNo, boolean autoGenerate, String actorUserId) {
        ItemView item = requireItem(itemId);
        if (!item.lotTracked()) {
            throw new IllegalArgumentException("Lot 추적 품목이 아닙니다: " + item.itemNo());
        }
        String resolved = resolveLotNo(item, lotNo, autoGenerate);
        return lotRepository.findActiveByItemIdAndLotNo(item.id(), resolved)
                .orElseGet(() -> lotRepository.saveNew(
                        item.id(),
                        resolved,
                        LotStatus.ACTIVE,
                        LotOriginType.MANUAL,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        actorUserId
                ));
    }

    public void assertSufficientLotQty(long lotId, long inventoryBalanceId, BigDecimal qty) {
        if (qty == null || qty.compareTo(BigDecimal.ZERO) <= 0) {
            throw new IllegalArgumentException("출고·투입 수량은 0보다 커야 합니다.");
        }
        requireLot(lotId);
        BigDecimal onHand = lotRepository.findLotBalanceQty(lotId, inventoryBalanceId)
                .orElse(BigDecimal.ZERO);
        if (onHand.compareTo(qty) < 0) {
            throw new IllegalArgumentException(
                    "Lot 잔량이 부족합니다. 필요=" + qty + ", 보유=" + onHand);
        }
    }

    public List<LotView> findAvailableLots(long itemId, String locationCode, Long outputProcessId) {
        requireItem(itemId);
        if (locationCode == null || locationCode.isBlank()) {
            throw new IllegalArgumentException("창고 코드는 필수입니다.");
        }
        return lotRepository.findAvailableLots(itemId, locationCode.trim().toUpperCase(), outputProcessId);
    }

    private String resolveLotNo(ItemView item, String lotNo, boolean autoGenerate) {
        if (autoGenerate) {
            return lotRepository.allocateLotNo(item.id(), item.itemNo(), LocalDate.now());
        }
        return requireLotNo(lotNo);
    }

    private void assertLotNoUnique(long itemId, String lotNo) {
        lotRepository.findActiveByItemIdAndLotNo(itemId, lotNo).ifPresent(existing -> {
            throw new IllegalArgumentException("동일 품목에 이미 존재하는 Lot 번호입니다: " + lotNo);
        });
    }

    private LotView requireLot(long id) {
        return lotRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("Lot를 찾을 수 없습니다: " + id));
    }

    private ItemView requireItem(long itemId) {
        return itemRepository.findActiveById(itemId)
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + itemId));
    }

    private static String requireLotNo(String lotNo) {
        String normalized = trimToNull(lotNo);
        if (normalized == null) {
            throw new IllegalArgumentException("Lot 번호는 필수입니다. 자동 채번을 사용하려면 autoGenerate=true 로 지정하세요.");
        }
        if (normalized.length() > 100) {
            throw new IllegalArgumentException("Lot 번호는 100자를 초과할 수 없습니다.");
        }
        return normalized;
    }

    private static String trimToNull(String value) {
        if (value == null) {
            return null;
        }
        String trimmed = value.trim();
        return trimmed.isEmpty() ? null : trimmed;
    }
}
