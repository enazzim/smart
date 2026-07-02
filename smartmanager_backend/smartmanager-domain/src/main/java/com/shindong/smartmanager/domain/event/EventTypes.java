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

    private EventTypes() {
    }
}
