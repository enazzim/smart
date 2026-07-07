package com.shindong.smartmanager.domain.event;

/**
 * 애그리거트 타입 상수.
 */
public final class AggregateTypes {

    public static final String COMPANY = "Company";
    public static final String ITEM = "Item";
    public static final String PROCESS = "Process";
    public static final String UNIT_PRICE = "UnitPrice";
    public static final String BOM_LINE = "BomLine";
    public static final String WORK_CENTER = "WorkCenter";
    public static final String WORK_STANDARD = "WorkStandard";
    public static final String EQUIPMENT = "Equipment";
    public static final String USER = "User";
    public static final String PRODUCTION_CALENDAR = "ProductionCalendar";
    public static final String WORK_CENTER_CALENDAR = "WorkCenterCalendar";
    public static final String SALES_ORDER = "SalesOrder";
    public static final String PURCHASE_ORDER = "PurchaseOrder";
    public static final String OUTSOURCING_ORDER = "OutsourcingOrder";
    public static final String OUTSOURCING_SHIPMENT = "OutsourcingShipment";
    public static final String OUTSOURCING_RECEIPT = "OutsourcingReceipt";
    public static final String SALES_SHIPMENT = "SalesShipment";
    public static final String SALES_REVENUE = "SalesRevenue";
    public static final String SALES_COLLECTION = "SalesCollection";
    public static final String PARTNER_PAYMENT = "PartnerPayment";

    private AggregateTypes() {
    }
}
