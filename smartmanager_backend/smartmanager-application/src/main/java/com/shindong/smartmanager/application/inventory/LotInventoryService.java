package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.domain.inventory.LotStatus;
import com.shindong.smartmanager.domain.inventory.StockMovementType;
import java.math.BigDecimal;

/**
 * Lot × 슬롯 잔량 갱신. {@link InventoryBalanceService#recordMovement}에서 lot_tracked 시 호출.
 */
public class LotInventoryService {

    private final LotRepository lotRepository;
    private final ItemRepository itemRepository;

    public LotInventoryService(LotRepository lotRepository, ItemRepository itemRepository) {
        this.lotRepository = lotRepository;
        this.itemRepository = itemRepository;
    }

    /**
     * 슬롯 재고 반영 직전/직후 동일 TX에서 Lot 잔량을 맞춘다.
     */
    public void applyLotMovement(
            long itemId,
            Long lotId,
            long inventoryBalanceId,
            StockMovementType movementType,
            BigDecimal qty,
            String actorUserId
    ) {
        ItemView item = itemRepository.findActiveById(itemId)
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + itemId));

        if (!item.lotTracked()) {
            if (lotId != null) {
                throw new IllegalArgumentException("Lot 비추적 품목에 lotId를 지정할 수 없습니다: " + item.itemNo());
            }
            return;
        }
        if (lotId == null) {
            throw new IllegalArgumentException("Lot 추적 품목은 lotId가 필수입니다: " + item.itemNo());
        }

        LotView lot = lotRepository.findActiveById(lotId)
                .orElseThrow(() -> new IllegalArgumentException("Lot를 찾을 수 없습니다: " + lotId));
        if (lot.itemId() != itemId) {
            throw new IllegalArgumentException("Lot와 품목이 일치하지 않습니다.");
        }
        if (lot.status() == LotStatus.BLOCKED) {
            throw new IllegalArgumentException("차단된 Lot는 입출고할 수 없습니다: " + lot.lotNo());
        }

        BigDecimal signedQty = signedQuantity(movementType, qty);
        if (signedQty.compareTo(BigDecimal.ZERO) < 0) {
            BigDecimal onHand = lotRepository.findLotBalanceQty(lotId, inventoryBalanceId)
                    .orElse(BigDecimal.ZERO);
            if (onHand.add(signedQty).compareTo(BigDecimal.ZERO) < 0) {
                throw new IllegalArgumentException(
                        "Lot 잔량이 부족합니다: " + lot.lotNo()
                                + " — 필요 " + qty.stripTrailingZeros().toPlainString()
                                + ", 보유 " + onHand.stripTrailingZeros().toPlainString());
            }
        }

        lotRepository.applyLotBalanceDelta(lotId, inventoryBalanceId, signedQty, actorUserId);
        lotRepository.refreshLotStatusFromBalances(lotId, actorUserId);
    }

    private static BigDecimal signedQuantity(StockMovementType type, BigDecimal qty) {
        return switch (type) {
            case IN -> qty;
            case OUT -> qty.negate();
            case ADJUST -> qty;
        };
    }
}
