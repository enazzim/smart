package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.FiscalPeriod;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.system.SystemSettingService;
import com.shindong.smartmanager.domain.inventory.StockMovementType;
import java.math.BigDecimal;
import java.time.LocalDate;

/**
 * 재고 수량 변경의 단일 진입점. 모든 입·출고 TX는 {@link #recordMovement}를 통해야 하며,
 * 마이너스 재고 허용 여부는 {@link SystemSettingService#isNegativeStockAllowed()}로 일괄 적용된다.
 */
public class InventoryBalanceService {

    private final InventoryLocationQueryRepository locationRepository;
    private final InventoryStockBalanceRepository balanceRepository;
    private final StockMovementRepository movementRepository;
    private final InventoryBalanceMonthlyRepository monthlyRepository;
    private final FiscalCalendarService fiscalCalendarService;
    private final MonthClosingService monthClosingService;
    private final SystemSettingService systemSettingService;

    public InventoryBalanceService(
            InventoryLocationQueryRepository locationRepository,
            InventoryStockBalanceRepository balanceRepository,
            StockMovementRepository movementRepository,
            InventoryBalanceMonthlyRepository monthlyRepository,
            FiscalCalendarService fiscalCalendarService,
            MonthClosingService monthClosingService,
            SystemSettingService systemSettingService
    ) {
        this.locationRepository = locationRepository;
        this.balanceRepository = balanceRepository;
        this.movementRepository = movementRepository;
        this.monthlyRepository = monthlyRepository;
        this.fiscalCalendarService = fiscalCalendarService;
        this.monthClosingService = monthClosingService;
        this.systemSettingService = systemSettingService;
    }

    public InventoryBalanceSlotView ensureBalance(InventoryBalanceKey key) {
        return balanceRepository.ensureBalance(key);
    }

    public BigDecimal currentStockQty(
            long itemId,
            String locationCode,
            LocalDate movementDate,
            Long outputProcessId,
            Long inputProcessId,
            Long partnerId
    ) {
        long locationId = locationRepository.findActiveLocationIdByCode(locationCode)
                .orElseThrow(() -> new IllegalArgumentException("창고를 찾을 수 없습니다: " + locationCode));
        FiscalPeriod period = fiscalCalendarService.resolvePeriod(movementDate);
        InventoryBalanceKey key = new InventoryBalanceKey(
                itemId,
                locationId,
                period.fiscalYear(),
                outputProcessId,
                inputProcessId,
                partnerId
        );
        return balanceRepository.ensureBalance(key).stockQty();
    }

    public void assertSufficientStockForOutbound(
            long itemId,
            String itemNo,
            String locationCode,
            LocalDate movementDate,
            BigDecimal qty,
            Long outputProcessId,
            Long inputProcessId
    ) {
        if (systemSettingService.isNegativeStockAllowed()) {
            return;
        }
        if (qty == null || qty.compareTo(BigDecimal.ZERO) <= 0) {
            return;
        }
        BigDecimal onHand = currentStockQty(itemId, locationCode, movementDate, outputProcessId, inputProcessId, null);
        if (onHand.compareTo(qty) < 0) {
            throw new IllegalArgumentException(
                    "재고가 부족합니다: " + itemNo + " (" + locationCode + ") — 필요 "
                            + qty.stripTrailingZeros().toPlainString()
                            + ", 보유 " + onHand.stripTrailingZeros().toPlainString()
                            + ". 투입 체크를 해제하거나 재고를 보충해 주세요."
            );
        }
    }

    public StockMovementView recordMovement(RecordStockMovementCommand command) {
        monthClosingService.assertTransactionOpen(command.movementDate());

        long locationId = locationRepository.findActiveLocationIdByCode(command.locationCode())
                .orElseThrow(() -> new IllegalArgumentException("창고를 찾을 수 없습니다: " + command.locationCode()));

        FiscalPeriod period = fiscalCalendarService.resolvePeriod(command.movementDate());
        int fiscalYear = period.fiscalYear();
        int fiscalMonth = period.fiscalMonth();

        InventoryBalanceKey key = new InventoryBalanceKey(
                command.itemId(),
                locationId,
                fiscalYear,
                command.outputProcessId(),
                command.inputProcessId(),
                command.partnerId()
        );

        InventoryBalanceSlotView balance = balanceRepository.ensureBalance(key);
        BigDecimal signedQty = signedQuantity(command.movementType(), command.qty());
        BigDecimal signedAmount = signedQuantity(command.movementType(), command.amount());

        BigDecimal newQty = balance.stockQty().add(signedQty);
        if (!systemSettingService.isNegativeStockAllowed() && newQty.compareTo(BigDecimal.ZERO) < 0) {
            throw new IllegalStateException("재고 수량이 부족합니다. itemId=" + command.itemId()
                    + ", location=" + command.locationCode());
        }

        BigDecimal newAmount = balance.stockAmount().add(signedAmount);
        InventoryBalanceSlotView updated = balanceRepository.saveBalance(new InventoryBalanceSlotView(
                balance.id(),
                balance.itemId(),
                balance.locationId(),
                balance.fiscalYear(),
                balance.outputProcessId(),
                balance.inputProcessId(),
                balance.partnerId(),
                newQty,
                newAmount
        ));

        StockMovementView movement = movementRepository.save(new StockMovementView(
                0L,
                updated.id(),
                updated.itemId(),
                updated.locationId(),
                fiscalYear,
                fiscalMonth,
                command.movementType(),
                command.qty(),
                command.amount(),
                command.referenceType(),
                command.referenceId(),
                command.movementDate()
        ));

        applyMonthly(updated.id(), fiscalMonth, command.movementType(), command.qty(), command.amount(), signedQty);

        return movement;
    }

    private void applyMonthly(
            long balanceId,
            int monthNum,
            StockMovementType movementType,
            BigDecimal qty,
            BigDecimal amount,
            BigDecimal signedQty
    ) {
        BigDecimal inQty = BigDecimal.ZERO;
        BigDecimal inAmount = BigDecimal.ZERO;
        BigDecimal outQty = BigDecimal.ZERO;
        BigDecimal outAmount = BigDecimal.ZERO;

        switch (movementType) {
            case IN -> {
                inQty = qty;
                inAmount = amount;
            }
            case OUT -> {
                outQty = qty;
                outAmount = amount;
            }
            case ADJUST -> {
                if (signedQty.compareTo(BigDecimal.ZERO) >= 0) {
                    inQty = signedQty;
                    inAmount = amount;
                } else {
                    outQty = signedQty.abs();
                    outAmount = amount.abs();
                }
            }
        }

        monthlyRepository.applyMovement(balanceId, monthNum, inQty, inAmount, outQty, outAmount, signedQty);
    }

    private static BigDecimal signedQuantity(StockMovementType type, BigDecimal qty) {
        return switch (type) {
            case IN -> qty;
            case OUT -> qty.negate();
            case ADJUST -> qty;
        };
    }
}
