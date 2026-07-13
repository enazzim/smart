package com.shindong.smartmanager.application.stats;

import java.util.List;

public interface StatsReportRepository {

    List<VendorPurchaseTotalView> findVendorPurchaseTotals(VendorPurchaseTotalCriteria criteria);

    List<WarehouseMonthlyIoView> findWarehouseMonthlyIo(WarehouseMonthlyIoCriteria criteria);

    List<ItemStockMovementView> findItemStockMovements(ItemStockMovementCriteria criteria);
}
