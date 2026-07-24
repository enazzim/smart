package com.shindong.smartmanager.application.stats;

import java.math.BigDecimal;
import java.util.Collection;
import java.util.List;
import java.util.Map;

public interface StatsReportRepository {

    List<VendorPurchaseTotalView> findVendorPurchaseTotals(VendorPurchaseTotalCriteria criteria);

    List<VendorPurchaseStatusView> findVendorPurchaseStatus(VendorPurchaseStatusCriteria criteria);

    List<PurchaseDailyReportView> findPurchaseDailyReport(PurchaseDailyReportCriteria criteria);

    List<PartnerMonthlyPayableView> findPartnerMonthlyPayable(PartnerMonthlyPayableCriteria criteria);

    /**
     * 거래처별 금액 합 — 레거시 월계/연도누계용.
     * 품목·일자 필터는 적용하지 않고, 구분·승인·연도·(선택)월만 반영한다.
     * {@code fiscalMonth == null} 이면 해당 연도 전체(누계).
     */
    Map<Long, BigDecimal> sumPurchaseDailyAmountsByCompany(
            PurchaseDailyReportCriteria criteria,
            Collection<Long> companyIds,
            int fiscalYear,
            Integer fiscalMonth
    );

    List<WarehouseMonthlyIoView> findWarehouseMonthlyIo(WarehouseMonthlyIoCriteria criteria);

    List<ItemStockMovementView> findItemStockMovements(ItemStockMovementCriteria criteria);

    List<OrderVsReceiptView> findOrderVsReceipt(OrderVsReceiptCriteria criteria);
}
