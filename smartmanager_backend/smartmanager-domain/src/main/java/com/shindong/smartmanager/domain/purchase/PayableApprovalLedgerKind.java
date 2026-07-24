package com.shindong.smartmanager.domain.purchase;

public enum PayableApprovalLedgerKind {
    PURCHASE,
    OUTSOURCE,
    /** 기타공제(ECL_HT) */
    ETC_CLAIM,
    /** 불량변상(PCL_HT) */
    DEFECT_CLAIM
}
