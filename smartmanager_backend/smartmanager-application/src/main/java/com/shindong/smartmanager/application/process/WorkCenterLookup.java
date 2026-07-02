package com.shindong.smartmanager.application.process;

public interface WorkCenterLookup {

    boolean existsActive(long workCenterId);

    String findActiveName(long workCenterId);
}
