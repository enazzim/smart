package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.domain.inventory.LotOriginType;
import com.shindong.smartmanager.domain.inventory.MiscStockMovementDirection;
import com.shindong.smartmanager.domain.inventory.MiscStockMovementStatus;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.List;

public class MiscStockMovementService {

    private final MiscStockMovementRepository miscStockMovementRepository;
    private final ItemRepository itemRepository;
    private final MiscStockMovementResolver resolver;
    private final MiscStockMovementInventoryService inventoryService;
    private final LotService lotService;
    private final MonthClosingService monthClosingService;

    public MiscStockMovementService(
            MiscStockMovementRepository miscStockMovementRepository,
            ItemRepository itemRepository,
            MiscStockMovementResolver resolver,
            MiscStockMovementInventoryService inventoryService,
            LotService lotService,
            MonthClosingService monthClosingService
    ) {
        this.miscStockMovementRepository = miscStockMovementRepository;
        this.itemRepository = itemRepository;
        this.resolver = resolver;
        this.inventoryService = inventoryService;
        this.lotService = lotService;
        this.monthClosingService = monthClosingService;
    }

    public List<MiscStockMovementView> list(MiscStockMovementListCriteria criteria) {
        return miscStockMovementRepository.findAllActive(criteria);
    }

    public MiscStockMovementPreviewView preview(
            long itemId,
            Long processSequenceId,
            LocalDate movementDate,
            String actorUserId
    ) {
        ItemView item = requireItem(itemId);
        LocalDate stockDate = movementDate != null ? movementDate : LocalDate.now();
        MiscStockMovementTarget target = resolver.resolve(item, processSequenceId);
        if (actorUserId != null) {
            inventoryService.ensureInventorySlot(
                    item.id(),
                    target.locationCode(),
                    stockDate,
                    target.outputProcessId(),
                    actorUserId
            );
        }
        BigDecimal onHand = inventoryService.resolveOnHandQty(
                item.id(),
                item.itemNo(),
                target.locationCode(),
                stockDate,
                target.outputProcessId()
        );
        return new MiscStockMovementPreviewView(
                item.id(),
                item.itemNo(),
                item.itemName(),
                item.propertyClassification(),
                target.locationCode(),
                target.locationLabel(),
                target.outputProcessId(),
                target.outputProcessSequence(),
                target.outputProcessName(),
                target.processRequired(),
                onHand,
                item.lotTracked()
        );
    }

    public MiscStockMovementView register(CreateMiscStockMovementCommand command, String actorUserId) {
        validateCommand(command);
        ItemView item = requireItem(command.itemId());
        monthClosingService.assertTransactionOpen(command.movementDate());

        MiscStockMovementTarget target = resolver.resolve(item, command.processSequenceId());
        Long lotId = resolveLotId(item, command, actorUserId);
        MiscStockMovementView saved = miscStockMovementRepository.save(
                new MiscStockMovementSaveCommand(
                        command.movementDate(),
                        command.movementDirection(),
                        item.id(),
                        target.locationCode(),
                        target.outputProcessId(),
                        normalizeQty(command.qty()),
                        command.reasonCodeId(),
                        normalizeNote(command.note()),
                        lotId
                ),
                nextMovementNo(command.movementDate()),
                actorUserId
        );

        inventoryService.applyRegistration(saved, item.itemNo(), actorUserId);
        return miscStockMovementRepository.findActiveById(saved.id())
                .orElse(saved);
    }

    public MiscStockMovementView update(long id, CreateMiscStockMovementCommand command, String actorUserId) {
        validateCommand(command);
        MiscStockMovementView existing = miscStockMovementRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("기타 입출고를 찾을 수 없습니다: " + id));
        if (existing.status() == MiscStockMovementStatus.CANCELLED) {
            throw new IllegalArgumentException("취소된 입출고는 수정할 수 없습니다.");
        }

        monthClosingService.assertTransactionOpen(existing.movementDate());
        monthClosingService.assertTransactionOpen(command.movementDate());

        ItemView oldItem = requireItem(existing.itemId());
        inventoryService.applyCancellation(existing, oldItem.itemNo(), actorUserId);

        ItemView item = requireItem(command.itemId());
        MiscStockMovementTarget target = resolver.resolve(item, command.processSequenceId());
        Long lotId = resolveLotId(item, command, actorUserId);
        MiscStockMovementView updated = miscStockMovementRepository.update(
                id,
                new MiscStockMovementSaveCommand(
                        command.movementDate(),
                        command.movementDirection(),
                        item.id(),
                        target.locationCode(),
                        target.outputProcessId(),
                        normalizeQty(command.qty()),
                        command.reasonCodeId(),
                        normalizeNote(command.note()),
                        lotId
                ),
                actorUserId
        );

        inventoryService.ensureInventorySlot(
                item.id(),
                target.locationCode(),
                command.movementDate(),
                target.outputProcessId(),
                actorUserId
        );
        inventoryService.applyRegistration(updated, item.itemNo(), actorUserId);
        return miscStockMovementRepository.findActiveById(updated.id())
                .orElse(updated);
    }

    public void delete(long id, String actorUserId) {
        MiscStockMovementView movement = miscStockMovementRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("기타 입출고를 찾을 수 없습니다: " + id));
        if (movement.status() == MiscStockMovementStatus.REGISTERED) {
            monthClosingService.assertTransactionOpen(movement.movementDate());
            ItemView item = requireItem(movement.itemId());
            inventoryService.applyCancellation(movement, item.itemNo(), actorUserId);
        }
        miscStockMovementRepository.delete(id);
    }

    private Long resolveLotId(ItemView item, CreateMiscStockMovementCommand command, String actorUserId) {
        if (!item.lotTracked()) {
            if (command.lotId() != null || command.autoGenerateLot()
                    || (command.lotNo() != null && !command.lotNo().isBlank())) {
                throw new IllegalArgumentException("Lot 비추적 품목은 Lot를 지정할 수 없습니다: " + item.itemNo());
            }
            return null;
        }
        if (command.movementDirection() == MiscStockMovementDirection.OUT) {
            if (command.lotId() == null) {
                throw new IllegalArgumentException("Lot 추적 품목 출고는 Lot를 선택해야 합니다: " + item.itemNo());
            }
            return command.lotId();
        }
        // IN: select existing or create
        if (command.lotId() != null) {
            return command.lotId();
        }
        if (command.autoGenerateLot()) {
            LotView created = lotService.resolveOrCreate(item.id(), null, true, actorUserId);
            return lotService.createOnReceipt(
                    item.id(),
                    created.lotNo(),
                    LotOriginType.ADJUSTMENT,
                    "misc_stock_movement",
                    null,
                    actorUserId
            ).id();
        }
        if (command.lotNo() == null || command.lotNo().isBlank()) {
            throw new IllegalArgumentException(
                    "Lot 추적 품목 입고는 Lot 선택·번호 또는 자동생성이 필요합니다: " + item.itemNo());
        }
        return lotService.createOnReceipt(
                item.id(),
                command.lotNo().trim(),
                LotOriginType.ADJUSTMENT,
                "misc_stock_movement",
                null,
                actorUserId
        ).id();
    }

    private void validateCommand(CreateMiscStockMovementCommand command) {
        if (command.movementDate() == null) {
            throw new IllegalArgumentException("입출고 일자는 필수입니다.");
        }
        if (command.movementDirection() == null) {
            throw new IllegalArgumentException("입고 또는 출고를 선택해 주세요.");
        }
        if (command.itemId() <= 0) {
            throw new IllegalArgumentException("품목을 선택해 주세요.");
        }
        normalizeQty(command.qty());
    }

    private ItemView requireItem(long itemId) {
        return itemRepository.findActiveById(itemId)
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + itemId));
    }

    private static BigDecimal normalizeQty(BigDecimal qty) {
        if (qty == null || qty.compareTo(BigDecimal.ZERO) <= 0) {
            throw new IllegalArgumentException("수량은 0보다 커야 합니다.");
        }
        return qty.setScale(4, RoundingMode.HALF_UP);
    }

    private static String normalizeNote(String note) {
        if (note == null) {
            return null;
        }
        String trimmed = note.trim();
        return trimmed.isEmpty() ? null : trimmed;
    }

    private String nextMovementNo(LocalDate movementDate) {
        String prefix = "MSM-" + movementDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        long seq = miscStockMovementRepository.countByMovementNoPrefix(prefix) + 1;
        return prefix + String.format("%03d", seq);
    }
}
