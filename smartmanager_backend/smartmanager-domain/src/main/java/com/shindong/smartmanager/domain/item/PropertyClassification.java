package com.shindong.smartmanager.domain.item;

public enum PropertyClassification {
    원자재,
    제품,
    상품,
    공정품;

    /** 생산 완료(최종 공정) 시 영업창고로 직접 입고하는 분류 */
    public boolean salesWarehouseAtProductionComplete() {
        return this == 제품;
    }

    /** 출고·납품 시 공정창고(최종 공정)에서 직접 납품창고로 이동하는 분류 */
    public boolean shipmentFromWipFinalProcess() {
        return this == 공정품;
    }
}
