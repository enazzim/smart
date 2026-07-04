package com.shindong.smartmanager.application.equipment;

public interface EquipmentLookup {

    boolean existsActive(long equipmentId);

    String findActiveName(long equipmentId);
}
