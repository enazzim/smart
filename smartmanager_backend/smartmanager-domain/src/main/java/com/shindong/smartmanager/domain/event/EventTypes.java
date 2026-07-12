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

    public static final String EQUIPMENT_REGISTERED = "EquipmentRegistered";
    public static final String EQUIPMENT_UPDATED = "EquipmentUpdated";
    public static final String EQUIPMENT_DELETED = "EquipmentDeleted";

    public static final String USER_REGISTERED = "UserRegistered";
    public static final String USER_UPDATED = "UserUpdated";
    public static final String USER_DELETED = "UserDeleted";

    public static final String STANDARD_CALENDAR_DAY_REGISTERED = "StandardCalendarDayRegistered";
    public static final String STANDARD_CALENDAR_DAY_UPDATED = "StandardCalendarDayUpdated";
    public static final String STANDARD_CALENDAR_DAY_DELETED = "StandardCalendarDayDeleted";

    public static final String WORK_CENTER_CALENDAR_DAY_REGISTERED = "WorkCenterCalendarDayRegistered";
    public static final String WORK_CENTER_CALENDAR_DAY_UPDATED = "WorkCenterCalendarDayUpdated";
    public static final String WORK_CENTER_CALENDAR_DAY_DELETED = "WorkCenterCalendarDayDeleted";

    public static final String DRAWING_REGISTERED = "DrawingRegistered";
    public static final String DRAWING_REVISED = "DrawingRevised";
    public static final String DRAWING_DELETED = "DrawingDeleted";
    public static final String DRAWING_RESTORED = "DrawingRestored";
    public static final String DRAWING_INFO_UPDATED = "DrawingInfoUpdated";
    public static final String DRAWING_PROMOTED = "DrawingPromoted";
    public static final String DRAWING_HARD_DELETED = "DrawingHardDeleted";

    public static final String SALES_ORDER_REGISTERED = "SalesOrderRegistered";
    public static final String SALES_ORDER_UPDATED = "SalesOrderUpdated";
    public static final String SALES_ORDER_CONFIRMED = "SalesOrderConfirmed";
    public static final String SALES_ORDER_CANCELLED = "SalesOrderCancelled";

    public static final String PURCHASE_ORDER_REGISTERED = "PurchaseOrderRegistered";
    public static final String PURCHASE_ORDER_UPDATED = "PurchaseOrderUpdated";
    public static final String PURCHASE_ORDER_CONFIRMED = "PurchaseOrderConfirmed";
    public static final String PURCHASE_ORDER_CANCELLED = "PurchaseOrderCancelled";

    public static final String OUTSOURCING_ORDER_REGISTERED = "OutsourcingOrderRegistered";
    public static final String OUTSOURCING_ORDER_CANCELLED = "OutsourcingOrderCancelled";
    public static final String OUTSOURCING_SHIPMENT_REGISTERED = "OutsourcingShipmentRegistered";
    public static final String OUTSOURCING_SHIPMENT_CANCELLED = "OutsourcingShipmentCancelled";
    public static final String OUTSOURCING_RECEIPT_REGISTERED = "OutsourcingReceiptRegistered";

    public static final String SALES_SHIPMENT_REGISTERED = "SalesShipmentRegistered";
    public static final String SALES_SHIPMENT_CANCELLED = "SalesShipmentCancelled";
    public static final String SALES_REVENUE_REGISTERED = "SalesRevenueRegistered";
    public static final String SALES_REVENUE_CANCELLED = "SalesRevenueCancelled";
    public static final String SALES_COLLECTION_REGISTERED = "SalesCollectionRegistered";
    public static final String SALES_COLLECTION_CANCELLED = "SalesCollectionCancelled";

    public static final String PARTNER_PAYMENT_REGISTERED = "PartnerPaymentRegistered";
    public static final String PARTNER_PAYMENT_CANCELLED = "PartnerPaymentCancelled";

    private EventTypes() {
    }
}
