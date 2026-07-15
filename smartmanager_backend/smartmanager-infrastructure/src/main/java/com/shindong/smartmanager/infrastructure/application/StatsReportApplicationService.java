package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.stats.ItemStockMovementCriteria;
import com.shindong.smartmanager.application.stats.ItemStockMovementView;
import com.shindong.smartmanager.application.stats.OrderVsReceiptCriteria;
import com.shindong.smartmanager.application.stats.OrderVsReceiptView;
import com.shindong.smartmanager.application.stats.StatsReportQueryService;
import com.shindong.smartmanager.application.stats.PurchaseDailyReportCriteria;
import com.shindong.smartmanager.application.stats.PurchaseDailyReportView;
import com.shindong.smartmanager.application.stats.VendorPurchaseStatusCriteria;
import com.shindong.smartmanager.application.stats.VendorPurchaseStatusView;
import com.shindong.smartmanager.application.stats.VendorPurchaseTotalCriteria;
import com.shindong.smartmanager.application.stats.VendorPurchaseTotalView;
import com.shindong.smartmanager.application.stats.WarehouseMonthlyIoCriteria;
import com.shindong.smartmanager.application.stats.WarehouseMonthlyIoView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class StatsReportApplicationService {

    private final StatsReportQueryService statsReportQueryService;

    public StatsReportApplicationService(StatsReportQueryService statsReportQueryService) {
        this.statsReportQueryService = statsReportQueryService;
    }

    @Transactional(readOnly = true)
    public List<VendorPurchaseTotalView> listVendorPurchaseTotals(VendorPurchaseTotalCriteria criteria) {
        return statsReportQueryService.listVendorPurchaseTotals(criteria);
    }

    @Transactional(readOnly = true)
    public List<VendorPurchaseStatusView> listVendorPurchaseStatus(VendorPurchaseStatusCriteria criteria) {
        return statsReportQueryService.listVendorPurchaseStatus(criteria);
    }

    @Transactional(readOnly = true)
    public List<PurchaseDailyReportView> listPurchaseDailyReport(PurchaseDailyReportCriteria criteria) {
        return statsReportQueryService.listPurchaseDailyReport(criteria);
    }

    @Transactional(readOnly = true)
    public List<WarehouseMonthlyIoView> listWarehouseMonthlyIo(WarehouseMonthlyIoCriteria criteria) {
        return statsReportQueryService.listWarehouseMonthlyIo(criteria);
    }

    @Transactional(readOnly = true)
    public List<ItemStockMovementView> listItemStockMovements(ItemStockMovementCriteria criteria) {
        return statsReportQueryService.listItemStockMovements(criteria);
    }

    @Transactional(readOnly = true)
    public List<OrderVsReceiptView> listOrderVsReceipt(OrderVsReceiptCriteria criteria) {
        return statsReportQueryService.listOrderVsReceipt(criteria);
    }
}
