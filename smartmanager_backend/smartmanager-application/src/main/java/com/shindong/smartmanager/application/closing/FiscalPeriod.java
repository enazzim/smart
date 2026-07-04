package com.shindong.smartmanager.application.closing;

public record FiscalPeriod(int fiscalYear, int fiscalMonth) {

    public FiscalPeriod {
        if (fiscalMonth < 1 || fiscalMonth > 12) {
            throw new IllegalArgumentException("회계월은 1~12 사이여야 합니다.");
        }
    }

    public FiscalPeriod previous() {
        if (fiscalMonth == 1) {
            return new FiscalPeriod(fiscalYear - 1, 12);
        }
        return new FiscalPeriod(fiscalYear, fiscalMonth - 1);
    }
}
