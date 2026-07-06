package com.shindong.smartmanager.domain.production;

public enum MrpGroupingMode {
    BY_PLAN,
    BY_COMPONENT;

    public static MrpGroupingMode fromValue(String value) {
        if (value == null || value.isBlank()) {
            return BY_PLAN;
        }
        try {
            return MrpGroupingMode.valueOf(value.trim());
        } catch (IllegalArgumentException ex) {
            throw new IllegalArgumentException("지원하지 않는 MRP 집계 모드입니다: " + value);
        }
    }

    public String label() {
        return switch (this) {
            case BY_PLAN -> "생산계획별";
            case BY_COMPONENT -> "자재별 합산";
        };
    }
}
