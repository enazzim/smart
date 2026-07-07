package com.shindong.smartmanager.domain.inventory;

import java.util.Map;

public final class InventoryLocationLabels {

    private static final Map<String, String> LABELS = Map.of(
            "RAW", "원자재창고",
            "WIP", "공정창고",
            "SALES", "영업창고",
            "DELIVERY", "납품창고",
            "OUTSOURCE", "외주창고"
    );

    private InventoryLocationLabels() {
    }

    public static String label(String locationCode) {
        if (locationCode == null || locationCode.isBlank()) {
            return "";
        }
        return LABELS.getOrDefault(locationCode.trim().toUpperCase(), locationCode.trim());
    }

    public static String labelWithProcess(String locationCode, Integer processSequence, String processName) {
        String base = label(locationCode);
        if ("WIP".equalsIgnoreCase(locationCode) && processName != null && !processName.isBlank()) {
            String sequence = processSequence != null ? processSequence + ". " : "";
            return base + " · " + sequence + processName;
        }
        return base;
    }
}
