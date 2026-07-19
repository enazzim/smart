package com.shindong.smartmanager.api.web.stats;

import com.shindong.smartmanager.application.stats.ItemStockMovementCriteria;
import com.shindong.smartmanager.application.stats.ItemStockMovementView;
import com.shindong.smartmanager.application.stats.OrderVsReceiptCriteria;
import com.shindong.smartmanager.application.stats.OrderVsReceiptView;
import com.shindong.smartmanager.application.stats.PartnerMonthlyPayableCriteria;
import com.shindong.smartmanager.application.stats.PartnerMonthlyPayableView;
import com.shindong.smartmanager.application.stats.PurchaseDailyReportCriteria;
import com.shindong.smartmanager.application.stats.PurchaseDailyReportView;
import com.shindong.smartmanager.application.stats.VendorPurchaseStatusCriteria;
import com.shindong.smartmanager.application.stats.VendorPurchaseStatusView;
import com.shindong.smartmanager.application.stats.VendorPurchaseTotalCriteria;
import com.shindong.smartmanager.application.stats.VendorPurchaseTotalView;
import com.shindong.smartmanager.application.stats.WarehouseMonthlyIoCriteria;
import com.shindong.smartmanager.application.stats.WarehouseMonthlyIoView;
import com.shindong.smartmanager.infrastructure.application.StatsReportApplicationService;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/stats")
public class StatsReportController {

    private final StatsReportApplicationService statsReportApplicationService;

    public StatsReportController(StatsReportApplicationService statsReportApplicationService) {
        this.statsReportApplicationService = statsReportApplicationService;
    }

    @GetMapping("/vendor-purchase-totals")
    @PreAuthorize("hasAuthority('stats:vendor-purchase:read')")
    public List<VendorPurchaseTotalResponse> listVendorPurchaseTotals(
            @RequestParam(required = false) Long companyId,
            @RequestParam(required = false) Long itemId,
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) Integer fiscalYear,
            @RequestParam(required = false) Integer fiscalMonth,
            @RequestParam(required = false) String division
    ) {
        return statsReportApplicationService.listVendorPurchaseTotals(new VendorPurchaseTotalCriteria(
                companyId, itemId, itemNo, fiscalYear, fiscalMonth, division
        )).stream().map(VendorPurchaseTotalResponse::from).toList();
    }

    @GetMapping("/purchase-daily-report")
    @PreAuthorize("hasAuthority('stats:purchase-daily:read')")
    public List<PurchaseDailyReportResponse> listPurchaseDailyReport(
            @RequestParam(required = false) Long companyId,
            @RequestParam(required = false) String companyName,
            @RequestParam(required = false) Long itemId,
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) String itemName,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate inputDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate inputDateTo,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateTo,
            @RequestParam(required = false) Integer fiscalYear,
            @RequestParam(required = false) Integer fiscalMonth,
            @RequestParam(required = false) String division,
            @RequestParam(required = false) String approvalStatus
    ) {
        return statsReportApplicationService.listPurchaseDailyReport(new PurchaseDailyReportCriteria(
                companyId,
                companyName,
                itemId,
                itemNo,
                itemName,
                inputDateFrom,
                inputDateTo,
                receiptDateFrom,
                receiptDateTo,
                fiscalYear,
                fiscalMonth,
                division,
                approvalStatus
        )).stream().map(PurchaseDailyReportResponse::from).toList();
    }

    @GetMapping("/partner-monthly-payable")
    @PreAuthorize("hasAuthority('stats:partner-monthly-payable:read')")
    public List<PartnerMonthlyPayableResponse> listPartnerMonthlyPayable(
            @RequestParam(required = false) Long companyId,
            @RequestParam(required = false) String companyName,
            @RequestParam(required = false) Integer fiscalYear,
            @RequestParam(required = false) Integer fiscalMonth
    ) {
        int year = fiscalYear != null ? fiscalYear : java.time.YearMonth.now().getYear();
        int month = fiscalMonth != null ? fiscalMonth : java.time.YearMonth.now().getMonthValue();
        return statsReportApplicationService.listPartnerMonthlyPayable(new PartnerMonthlyPayableCriteria(
                companyId,
                companyName,
                year,
                month
        )).stream().map(PartnerMonthlyPayableResponse::from).toList();
    }

    @GetMapping("/vendor-purchase-status")
    @PreAuthorize("hasAuthority('stats:vendor-purchase-status:read')")
    public List<VendorPurchaseStatusResponse> listVendorPurchaseStatus(
            @RequestParam(required = false) Long companyId,
            @RequestParam(required = false) String companyName,
            @RequestParam(required = false) Long itemId,
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) String itemName,
            @RequestParam(required = false) String modelType,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateTo,
            @RequestParam(required = false) Integer fiscalYear,
            @RequestParam(required = false) Integer fiscalMonth,
            @RequestParam(required = false) String division
    ) {
        return statsReportApplicationService.listVendorPurchaseStatus(new VendorPurchaseStatusCriteria(
                companyId,
                companyName,
                itemId,
                itemNo,
                itemName,
                modelType,
                receiptDateFrom,
                receiptDateTo,
                fiscalYear,
                fiscalMonth,
                division
        )).stream().map(VendorPurchaseStatusResponse::from).toList();
    }

    @GetMapping("/warehouse-monthly-io")
    @PreAuthorize("hasAuthority('stats:warehouse-io:read')")
    public List<WarehouseMonthlyIoResponse> listWarehouseMonthlyIo(
            @RequestParam String locationCode,
            @RequestParam(required = false) Long itemId,
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) Integer fiscalYear,
            @RequestParam(required = false) Integer fiscalMonth
    ) {
        int year = fiscalYear != null ? fiscalYear : LocalDate.now().getYear();
        int month = fiscalMonth != null ? fiscalMonth : LocalDate.now().getMonthValue();
        return statsReportApplicationService.listWarehouseMonthlyIo(new WarehouseMonthlyIoCriteria(
                locationCode, itemId, itemNo, year, month
        )).stream().map(WarehouseMonthlyIoResponse::from).toList();
    }

    @GetMapping("/item-stock-movements")
    @PreAuthorize("hasAuthority('stats:item-io:read')")
    public List<ItemStockMovementResponse> listItemStockMovements(
            @RequestParam(required = false) Long itemId,
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) Long companyId,
            @RequestParam(required = false) String locationCode,
            @RequestParam(required = false) Long outputProcessId,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate movementDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate movementDateTo
    ) {
        return statsReportApplicationService.listItemStockMovements(new ItemStockMovementCriteria(
                itemId, itemNo, companyId, locationCode, outputProcessId, movementDateFrom, movementDateTo
        )).stream().map(ItemStockMovementResponse::from).toList();
    }

    @GetMapping("/order-vs-receipt")
    @PreAuthorize("hasAuthority('stats:order-vs-receipt:read')")
    public List<OrderVsReceiptResponse> listOrderVsReceipt(
            @RequestParam(required = false) Long companyId,
            @RequestParam(required = false) String companyName,
            @RequestParam(required = false) Long itemId,
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) String itemName,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate orderDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate orderDateTo,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateTo,
            @RequestParam(required = false) String division
    ) {
        return statsReportApplicationService.listOrderVsReceipt(new OrderVsReceiptCriteria(
                companyId,
                companyName,
                itemId,
                itemNo,
                itemName,
                orderDateFrom,
                orderDateTo,
                receiptDateFrom,
                receiptDateTo,
                division
        )).stream().map(OrderVsReceiptResponse::from).toList();
    }
}

record VendorPurchaseTotalResponse(
        long companyId,
        String companyName,
        Long itemId,
        String itemNo,
        String itemName,
        String division,
        BigDecimal purchaseQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        int fiscalYear,
        int fiscalMonth
) {
    static VendorPurchaseTotalResponse from(VendorPurchaseTotalView view) {
        return new VendorPurchaseTotalResponse(
                view.companyId(),
                view.companyName(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.division(),
                view.purchaseQty(),
                view.unitPrice(),
                view.amount(),
                view.fiscalYear(),
                view.fiscalMonth()
        );
    }
}

record VendorPurchaseStatusResponse(
        long historyId,
        String ledgerKind,
        long companyId,
        String companyName,
        Long itemId,
        String itemNo,
        String itemName,
        String modelType,
        String processName,
        LocalDate receiptDate,
        BigDecimal currentStockQty,
        BigDecimal receiptQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        String division,
        int fiscalYear,
        int fiscalMonth
) {
    static VendorPurchaseStatusResponse from(VendorPurchaseStatusView view) {
        return new VendorPurchaseStatusResponse(
                view.historyId(),
                view.ledgerKind(),
                view.companyId(),
                view.companyName(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.modelType(),
                view.processName(),
                view.receiptDate(),
                view.currentStockQty(),
                view.receiptQty(),
                view.unitPrice(),
                view.amount(),
                view.division(),
                view.fiscalYear(),
                view.fiscalMonth()
        );
    }
}

record PurchaseDailyReportResponse(
        long historyId,
        String ledgerKind,
        long companyId,
        String companyName,
        Long itemId,
        String itemNo,
        String itemName,
        String unit,
        String processName,
        LocalDate inputDate,
        LocalDate receiptDate,
        BigDecimal currentStockQty,
        BigDecimal receiptQty,
        BigDecimal passedQty,
        BigDecimal failedQty,
        BigDecimal standardUnitPrice,
        BigDecimal unitPrice,
        BigDecimal amount,
        BigDecimal offsetAmount,
        BigDecimal unpaidIncrease,
        String division,
        String approvalStatus,
        int fiscalYear,
        int fiscalMonth,
        BigDecimal monthTotal,
        BigDecimal yearTotal
) {
    static PurchaseDailyReportResponse from(PurchaseDailyReportView view) {
        return new PurchaseDailyReportResponse(
                view.historyId(),
                view.ledgerKind(),
                view.companyId(),
                view.companyName(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.unit(),
                view.processName(),
                view.inputDate(),
                view.receiptDate(),
                view.currentStockQty(),
                view.receiptQty(),
                view.passedQty(),
                view.failedQty(),
                view.standardUnitPrice(),
                view.unitPrice(),
                view.amount(),
                view.offsetAmount(),
                view.unpaidIncrease(),
                view.division(),
                view.approvalStatus(),
                view.fiscalYear(),
                view.fiscalMonth(),
                view.monthTotal(),
                view.yearTotal()
        );
    }
}

record PartnerMonthlyPayableResponse(
        long companyId,
        String companyName,
        int fiscalYear,
        int fiscalMonth,
        BigDecimal approvedAmount,
        BigDecimal offsetAmount,
        BigDecimal payableAmount
) {
    static PartnerMonthlyPayableResponse from(PartnerMonthlyPayableView view) {
        return new PartnerMonthlyPayableResponse(
                view.companyId(),
                view.companyName(),
                view.fiscalYear(),
                view.fiscalMonth(),
                view.approvedAmount(),
                view.offsetAmount(),
                view.payableAmount()
        );
    }
}

record WarehouseMonthlyIoResponse(
        long itemId,
        String itemNo,
        String itemName,
        String locationCode,
        String locationName,
        Integer outputProcessSequence,
        String outputProcessName,
        int fiscalYear,
        int fiscalMonth,
        BigDecimal carryInQty,
        BigDecimal inQty,
        BigDecimal outQty,
        BigDecimal endingQty
) {
    static WarehouseMonthlyIoResponse from(WarehouseMonthlyIoView view) {
        return new WarehouseMonthlyIoResponse(
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.locationCode(),
                view.locationName(),
                view.outputProcessSequence(),
                view.outputProcessName(),
                view.fiscalYear(),
                view.fiscalMonth(),
                view.carryInQty(),
                view.inQty(),
                view.outQty(),
                view.endingQty()
        );
    }
}

record ItemStockMovementResponse(
        long id,
        long itemId,
        String itemNo,
        String itemName,
        String locationCode,
        String locationName,
        Integer outputProcessSequence,
        String outputProcessName,
        Long partnerId,
        String partnerName,
        String movementType,
        BigDecimal inQty,
        BigDecimal outQty,
        BigDecimal amount,
        String referenceType,
        long referenceId,
        LocalDate movementDate
) {
    static ItemStockMovementResponse from(ItemStockMovementView view) {
        return new ItemStockMovementResponse(
                view.id(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.locationCode(),
                view.locationName(),
                view.outputProcessSequence(),
                view.outputProcessName(),
                view.partnerId(),
                view.partnerName(),
                view.movementType(),
                view.inQty(),
                view.outQty(),
                view.amount(),
                view.referenceType(),
                view.referenceId(),
                view.movementDate()
        );
    }
}

record OrderVsReceiptResponse(
        long orderLineId,
        String division,
        long companyId,
        String companyName,
        long itemId,
        String itemNo,
        String itemName,
        String modelType,
        String processName,
        String unit,
        String standard,
        LocalDate orderDate,
        BigDecimal orderQty,
        BigDecimal orderUnitPrice,
        BigDecimal orderAmount,
        LocalDate requestedDeliveryDate,
        LocalDate lastReceiptDate,
        BigDecimal receivedQty,
        BigDecimal waitingInspectionQty,
        BigDecimal receiptAmount,
        BigDecimal remainQty,
        BigDecimal remainAmount,
        BigDecimal currentStockQty
) {
    static OrderVsReceiptResponse from(OrderVsReceiptView view) {
        return new OrderVsReceiptResponse(
                view.orderLineId(),
                view.division(),
                view.companyId(),
                view.companyName(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.modelType(),
                view.processName(),
                view.unit(),
                view.standard(),
                view.orderDate(),
                view.orderQty(),
                view.orderUnitPrice(),
                view.orderAmount(),
                view.requestedDeliveryDate(),
                view.lastReceiptDate(),
                view.receivedQty(),
                view.waitingInspectionQty(),
                view.receiptAmount(),
                view.remainQty(),
                view.remainAmount(),
                view.currentStockQty()
        );
    }
}
