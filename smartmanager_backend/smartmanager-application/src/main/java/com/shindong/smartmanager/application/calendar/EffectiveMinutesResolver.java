package com.shindong.smartmanager.application.calendar;

public final class EffectiveMinutesResolver {

    public static final int DEFAULT_WORK_TIME = 480;

    private EffectiveMinutesResolver() {
    }

    public static int resolve(
            Integer overrideWorkTime,
            Integer baseCalendarWorkTime,
            int operationTimeFallback,
            boolean autoOffDay
    ) {
        if (overrideWorkTime != null) {
            return overrideWorkTime;
        }
        if (baseCalendarWorkTime != null) {
            return baseCalendarWorkTime;
        }
        if (autoOffDay) {
            return 0;
        }
        return operationTimeFallback;
    }

    public static int resolveBaseWithoutOverride(
            Integer baseCalendarWorkTime,
            int operationTimeFallback,
            boolean autoOffDay
    ) {
        if (baseCalendarWorkTime != null) {
            return baseCalendarWorkTime;
        }
        if (autoOffDay) {
            return 0;
        }
        return operationTimeFallback;
    }
}
