package com.shindong.smartmanager.api.web.stats;

import com.shindong.smartmanager.application.stats.ItemStockMovementCriteria;
import com.shindong.smartmanager.application.stats.ItemStockMovementView;
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
