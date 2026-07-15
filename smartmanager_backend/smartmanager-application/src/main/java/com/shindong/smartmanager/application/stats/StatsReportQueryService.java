package com.shindong.smartmanager.application.stats;

import java.math.BigDecimal;
import java.time.Year;
import java.time.YearMonth;
import java.util.LinkedHashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;

public class StatsReportQueryService {

    private final StatsReportRepository statsReportRepository;

    public StatsReportQueryService(StatsReportRepository statsReportRepository) {
        this.statsReportRepository = statsReportRepository;
    }

    public List<VendorPurchaseTotalView> listVendorPurchaseTotals(VendorPurchaseTotalCriteria criteria) {
        VendorPurchaseTotalCriteria effective = criteria != null
                ? criteria
                : new VendorPurchaseTotalCriteria(null, null, null, null, null, null);
        if (effective.fiscalYear() == null) {
            YearMonth now = YearMonth.now();
            effective = new VendorPurchaseTotalCriteria(
                    effective.companyId(),
                    effective.itemId(),
                    effective.itemNo(),
                    now.getYear(),
                    effective.fiscalMonth() != null ? effective.fiscalMonth() : now.getMonthValue(),
                    effective.division()
            );
        }
        return statsReportRepository.findVendorPurchaseTotals(effective);
    }

    public List<VendorPurchaseStatusView> listVendorPurchaseStatus(VendorPurchaseStatusCriteria criteria) {
        VendorPurchaseStatusCriteria effective = criteria != null
                ? criteria
                : new VendorPurchaseStatusCriteria(
                        null, null, null, null, null, null, null, null, null, null, null
                );
        return statsReportRepository.findVendorPurchaseStatus(effective);
    }

    public List<PurchaseDailyReportView> listPurchaseDailyReport(PurchaseDailyReportCriteria criteria) {
        PurchaseDailyReportCriteria effective = criteria != null
                ? criteria
                : new PurchaseDailyReportCriteria(
                        null, null, null, null, null, null, null, null, null, null, null, null, null
                );
        List<PurchaseDailyReportView> rows = statsReportRepository.findPurchaseDailyReport(effective);
        if (rows.isEmpty()) {
            return rows;
        }

        YearMonth now = YearMonth.now();
        int fiscalYear = effective.fiscalYear() != null ? effective.fiscalYear() : now.getYear();
        Integer fiscalMonth = effective.fiscalMonth() != null ? effective.fiscalMonth() : now.getMonthValue();

        Set<Long> companyIds = new LinkedHashSet<>();
        for (PurchaseDailyReportView row : rows) {
            companyIds.add(row.companyId());
        }

        Map<Long, BigDecimal> monthTotals = statsReportRepository.sumPurchaseDailyAmountsByCompany(
                effective, companyIds, fiscalYear, fiscalMonth
        );
        Map<Long, BigDecimal> yearTotals = statsReportRepository.sumPurchaseDailyAmountsByCompany(
                effective, companyIds, fiscalYear, null
        );

        return rows.stream()
                .map(row -> new PurchaseDailyReportView(
                        row.historyId(),
                        row.ledgerKind(),
                        row.companyId(),
                        row.companyName(),
                        row.itemId(),
                        row.itemNo(),
                        row.itemName(),
                        row.unit(),
                        row.processName(),
                        row.inputDate(),
                        row.receiptDate(),
                        row.currentStockQty(),
                        row.receiptQty(),
                        row.passedQty(),
                        row.failedQty(),
                        row.standardUnitPrice(),
                        row.unitPrice(),
                        row.amount(),
                        row.division(),
                        row.approvalStatus(),
                        row.fiscalYear(),
                        row.fiscalMonth(),
                        monthTotals.getOrDefault(row.companyId(), BigDecimal.ZERO),
                        yearTotals.getOrDefault(row.companyId(), BigDecimal.ZERO)
                ))
                .toList();
    }

    public List<WarehouseMonthlyIoView> listWarehouseMonthlyIo(WarehouseMonthlyIoCriteria criteria) {
        if (criteria == null || criteria.locationCode() == null || criteria.locationCode().isBlank()) {
            throw new IllegalArgumentException("창고를 선택해 주세요.");
        }
        YearMonth now = YearMonth.now();
        int year = criteria.fiscalYear() > 0 ? criteria.fiscalYear() : Year.now().getValue();
        int month = criteria.fiscalMonth() > 0 ? criteria.fiscalMonth() : now.getMonthValue();
        if (month < 1 || month > 12) {
            throw new IllegalArgumentException("월은 1~12 범위여야 합니다.");
        }
        return statsReportRepository.findWarehouseMonthlyIo(new WarehouseMonthlyIoCriteria(
                criteria.locationCode().trim(),
                criteria.itemId(),
                criteria.itemNo(),
                year,
                month
        ));
    }

    public List<ItemStockMovementView> listItemStockMovements(ItemStockMovementCriteria criteria) {
        ItemStockMovementCriteria effective = criteria != null
                ? criteria
                : new ItemStockMovementCriteria(null, null, null, null, null, null, null);
        if (effective.itemId() == null
                && (effective.itemNo() == null || effective.itemNo().isBlank())) {
            throw new IllegalArgumentException("품목을 선택하거나 품번/품명을 입력해 주세요.");
        }
        return statsReportRepository.findItemStockMovements(effective);
    }

    public List<OrderVsReceiptView> listOrderVsReceipt(OrderVsReceiptCriteria criteria) {
        OrderVsReceiptCriteria effective = criteria != null
                ? criteria
                : new OrderVsReceiptCriteria(
                        null, null, null, null, null, null, null, null, null, null
                );
        return statsReportRepository.findOrderVsReceipt(effective);
    }
}
