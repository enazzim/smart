package com.shindong.smartmanager.domain.purchase;

/** 승인처리 검색 구분 — 전체 / 구매입고 / 외주입고 / 기타구매입고 / 기타공제 */
public enum PayableApprovalCategory {
    ALL,
    PURCHASE,
    OUTSOURCE,
    ETC,
    CLAIM
}
