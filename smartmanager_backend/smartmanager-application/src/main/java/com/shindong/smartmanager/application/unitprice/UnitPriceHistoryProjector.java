package com.shindong.smartmanager.application.unitprice;

/**
 * Ref: docs/step0/d4-unit-price.md §3.7
 */
public class UnitPriceHistoryProjector {

    public static final String REASON_NORMAL_REGISTER = "정상등록";
    public static final String REASON_MASTER_IMPORT_REGISTER = "초기정보일괄등록";
    public static final String REASON_DELETE = "삭제";

    private final UnitPriceRepository unitPriceRepository;

    public UnitPriceHistoryProjector(UnitPriceRepository unitPriceRepository) {
        this.unitPriceRepository = unitPriceRepository;
    }

    public void appendHistory(long unitPriceId, String reason, String actorUserId) {
        unitPriceRepository.appendChangeLog(unitPriceId, reason, actorUserId);
    }
}
