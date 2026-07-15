package com.shindong.smartmanager.application.quality;

import java.math.BigDecimal;
import java.time.LocalDate;

public record CompleteQualityInspectionCommand(
        BigDecimal passedQty,
        BigDecimal failedQty,
        Long inspectionDecisionCodeId,
        Long unsuitabilityCauseCodeId,
        Long unsuitabilityStatusCodeId,
        String failureReason,
        LocalDate completedDate,
        Integer fiscalYear,
        Integer fiscalMonth,
        String lotNo,
        boolean autoGenerateLot,
        boolean allowOverQty
) {
    public CompleteQualityInspectionCommand {
        if (passedQty == null || failedQty == null) {
            throw new IllegalArgumentException("합격·불량 수량은 필수입니다.");
        }
        if (passedQty.compareTo(BigDecimal.ZERO) < 0 || failedQty.compareTo(BigDecimal.ZERO) < 0) {
            throw new IllegalArgumentException("합격·불량 수량은 0 이상이어야 합니다.");
        }
        if (completedDate == null) {
            throw new IllegalArgumentException("검사완료일은 필수입니다.");
        }
        if (failedQty.compareTo(BigDecimal.ZERO) > 0
                && (failureReason == null || failureReason.isBlank())) {
            throw new IllegalArgumentException("불량수량이 있으면 불량사유를 입력해 주세요.");
        }
        if (failureReason != null && failureReason.length() > 500) {
            throw new IllegalArgumentException("불량사유는 500자 이하여야 합니다.");
        }
    }
}
