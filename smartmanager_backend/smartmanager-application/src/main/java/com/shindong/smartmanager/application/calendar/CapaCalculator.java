package com.shindong.smartmanager.application.calendar;

public final class CapaCalculator {

    private CapaCalculator() {
    }

    public static int calculate(int effectiveMinutes, String capacityDistinction, int retentionStaff) {
        if ("TIME_WORKERS".equalsIgnoreCase(capacityDistinction)) {
            return effectiveMinutes * Math.max(1, retentionStaff);
        }
        return effectiveMinutes;
    }
}
