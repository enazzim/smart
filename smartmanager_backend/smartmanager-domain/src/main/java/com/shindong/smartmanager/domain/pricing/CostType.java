package com.shindong.smartmanager.domain.pricing;

public enum CostType {
    SALE,
    PURCHASE,
    OUTSOURCE;

    /**
     * 일괄등록 엑셀 Cut-over 호환: 판매단가/구매단가/외주단가 및 영문 enum.
     */
    public static CostType fromImportValue(String raw) {
        if (raw == null || raw.isBlank()) {
            return null;
        }
        String normalized = raw.trim();
        String upper = normalized.toUpperCase();
        return switch (upper) {
            case "SALE" -> SALE;
            case "PURCHASE" -> PURCHASE;
            case "OUTSOURCE" -> OUTSOURCE;
            default -> switch (normalized) {
                case "판매", "판매단가" -> SALE;
                case "구매", "구매단가" -> PURCHASE;
                case "외주", "외주단가" -> OUTSOURCE;
                default -> throw new IllegalArgumentException(
                        "단가구분은 SALE/PURCHASE/OUTSOURCE(또는 판매단가/구매단가/외주단가)여야 합니다: " + raw
                );
            };
        };
    }
}
