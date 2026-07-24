package com.shindong.smartmanager.application.closing;

import java.time.LocalDate;

/** 회계월에 대응하는 달력 일자 구간(양끝 포함). */
public record FiscalPeriodDateRange(LocalDate startInclusive, LocalDate endInclusive) {

    public FiscalPeriodDateRange {
        if (startInclusive == null || endInclusive == null) {
            throw new IllegalArgumentException("회계월 일자 구간이 필요합니다.");
        }
        if (startInclusive.isAfter(endInclusive)) {
            throw new IllegalArgumentException("회계월 시작일이 종료일보다 늦습니다.");
        }
    }
}
