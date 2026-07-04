package com.shindong.smartmanager.domain.event;

/**
 * Phase 1 P0 이벤트 타입 상수.
 */
public final class EventTypes {

    public static final String COMPANY_REGISTERED = "CompanyRegistered";
    public static final String COMPANY_UPDATED = "CompanyUpdated";
    public static final String COMPANY_DELETED = "CompanyDeleted";

    public static final String ITEM_REGISTERED = "ItemRegistered";
    public static final String ITEM_UPDATED = "ItemUpdated";
    public static final String ITEM_DELETED = "ItemDeleted";

    public static final String PROCESS_REGISTERED = "ProcessRegistered";
    public static final String PROCESS_UPDATED = "ProcessUpdated";
    public static final String PROCESS_DELETED = "ProcessDeleted";

    public static final String SALE_UNIT_PRICE_REGISTERED = "SaleUnitPriceRegistered";
    public static final String SALE_UNIT_PRICE_UPDATED = "SaleUnitPriceUpdated";
    public static final String SALE_UNIT_PRICE_DELETED = "SaleUnitPriceDeleted";

    public static final String PURCHASE_UNIT_PRICE_REGISTERED = "PurchaseUnitPriceRegistered";
    public static final String PURCHASE_UNIT_PRICE_UPDATED = "PurchaseUnitPriceUpdated";
    public static final String PURCHASE_UNIT_PRICE_DELETED = "PurchaseUnitPriceDeleted";

    public static final String OUTSOURCE_UNIT_PRICE_REGISTERED = "OutsourceUnitPriceRegistered";
    public static final String OUTSOURCE_UNIT_PRICE_UPDATED = "OutsourceUnitPriceUpdated";
    public static final String OUTSOURCE_UNIT_PRICE_DELETED = "OutsourceUnitPriceDeleted";

    public static final String BOM_LINE_REGISTERED = "BomLineRegistered";
    public static final String BOM_LINE_UPDATED = "BomLineUpdated";
    public static final String BOM_LINE_DELETED = "BomLineDeleted";

    public static final String WORK_CENTER_REGISTERED = "WorkCenterRegistered";
    public static final String WORK_CENTER_UPDATED = "WorkCenterUpdated";
    public static final String WORK_CENTER_DELETED = "WorkCenterDeleted";

    public static final String WORK_STANDARD_REGISTERED = "WorkStandardRegistered";
    public static final String WORK_STANDARD_UPDATED = "WorkStandardUpdated";
    public static final String WORK_STANDARD_DELETED = "WorkStandardDeleted";

    private EventTypes() {
    }
}
