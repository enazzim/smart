package com.shindong.smartmanager.api.web.inventory;

import com.shindong.smartmanager.application.inventory.InventoryBalanceLedgerView;
import com.shindong.smartmanager.application.inventory.InventoryBalanceListCriteria;
import com.shindong.smartmanager.application.inventory.StockMovementListCriteria;
import com.shindong.smartmanager.application.inventory.StockMovementListItemView;
import com.shindong.smartmanager.infrastructure.application.InventoryLedgerApplicationService;
import java.time.LocalDate;
import java.util.List;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/inventory")
public class InventoryLedgerController {

    private final InventoryLedgerApplicationService ledgerApplicationService;

    public InventoryLedgerController(InventoryLedgerApplicationService ledgerApplicationService) {
        this.ledgerApplicationService = ledgerApplicationService;
    }

    @GetMapping("/stock-movements")
    @PreAuthorize("hasAuthority('inventory:ledger:read')")
    public List<StockMovementResponse> listStockMovements(
            @RequestParam(required = false) Long itemId,
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) String locationCode,
            @RequestParam(required = false) String referenceType,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate movementDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate movementDateTo
    ) {
        return ledgerApplicationService.listStockMovements(new StockMovementListCriteria(
                itemId, itemNo, locationCode, referenceType, movementDateFrom, movementDateTo
        )).stream().map(StockMovementResponse::from).toList();
    }

    @GetMapping("/balances")
    @PreAuthorize("hasAuthority('inventory:ledger:read')")
    public List<InventoryBalanceResponse> listBalances(
            @RequestParam(required = false) Long itemId,
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) String locationCode,
            @RequestParam(required = false) Integer fiscalYear
    ) {
        return ledgerApplicationService.listBalances(new InventoryBalanceListCriteria(
                itemId, itemNo, locationCode, fiscalYear
        )).stream().map(InventoryBalanceResponse::from).toList();
    }
}

record StockMovementResponse(
        long id,
        long itemId,
        String itemNo,
        String itemName,
        String locationCode,
        String locationName,
        Integer outputProcessSequence,
        String outputProcessName,
        String movementType,
        java.math.BigDecimal qty,
        java.math.BigDecimal amount,
        String referenceType,
        long referenceId,
        LocalDate movementDate,
        int fiscalYear,
        int fiscalMonth
) {
    static StockMovementResponse from(StockMovementListItemView view) {
        return new StockMovementResponse(
                view.id(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.locationCode(),
                view.locationName(),
                view.outputProcessSequence(),
                view.outputProcessName(),
                view.movementType().name(),
                view.qty(),
                view.amount(),
                view.referenceType(),
                view.referenceId(),
                view.movementDate(),
                view.fiscalYear(),
                view.fiscalMonth()
        );
    }
}

record InventoryBalanceResponse(
        long balanceId,
        long itemId,
        String itemNo,
        String itemName,
        String locationCode,
        String locationName,
        Integer outputProcessSequence,
        String outputProcessName,
        int fiscalYear,
        java.math.BigDecimal stockQty,
        java.math.BigDecimal stockAmount,
        List<InventoryBalanceMonthlyResponse> months
) {
    static InventoryBalanceResponse from(InventoryBalanceLedgerView view) {
        return new InventoryBalanceResponse(
                view.balanceId(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.locationCode(),
                view.locationName(),
                view.outputProcessSequence(),
                view.outputProcessName(),
                view.fiscalYear(),
                view.stockQty(),
                view.stockAmount(),
                view.months().stream().map(InventoryBalanceMonthlyResponse::from).toList()
        );
    }
}

record InventoryBalanceMonthlyResponse(
        int monthNum,
        java.math.BigDecimal inQty,
        java.math.BigDecimal outQty,
        java.math.BigDecimal stockQty
) {
    static InventoryBalanceMonthlyResponse from(
            com.shindong.smartmanager.application.inventory.InventoryBalanceMonthlyView view
    ) {
        return new InventoryBalanceMonthlyResponse(
                view.monthNum(),
                view.inQty(),
                view.outQty(),
                view.stockQty()
        );
    }
}
