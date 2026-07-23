package com.shindong.smartmanager.domain.item;

public enum PropertyClassification {
    원자재,
    제품,
    상품,
    공정품,
    부자재;

    /**
     * 일괄등록·Cut-over용 파싱. 레거시 표기는 현행 Enum으로 매핑한다.
     * <ul>
     *   <li>소모품 → 부자재</li>
     *   <li>반제품 → 공정품</li>
     * </ul>
     */
    public static PropertyClassification fromImportLabel(String raw) {
        if (raw == null || raw.isBlank()) {
            throw new IllegalArgumentException("자산분류는 필수입니다.");
        }
        String value = raw.trim();
        return switch (value) {
            case "소모품" -> 부자재;
            case "반제품" -> 공정품;
            case "원자재", "제품", "상품", "공정품", "부자재" -> PropertyClassification.valueOf(value);
            default -> throw new IllegalArgumentException(
                    "자산분류는 원자재/제품/상품/공정품/부자재 중 하나여야 합니다."
                            + " (레거시: 소모품→부자재, 반제품→공정품). 입력값="
                            + value);
        };
    }

    /** 생산 완료(최종 공정) 시 영업창고로 직접 입고하는 분류 */
    public boolean salesWarehouseAtProductionComplete() {
        return this == 제품;
    }

    /** 출고·납품 시 공정창고(최종 공정)에서 직접 납품창고로 이동하는 분류 */
    public boolean shipmentFromWipFinalProcess() {
        return this == 공정품;
    }
}
