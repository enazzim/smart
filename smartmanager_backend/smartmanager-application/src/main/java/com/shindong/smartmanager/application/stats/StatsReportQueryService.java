package com.shindong.smartmanager.application.stats;

import java.time.Year;
import java.time.YearMonth;
import java.util.List;

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
}
