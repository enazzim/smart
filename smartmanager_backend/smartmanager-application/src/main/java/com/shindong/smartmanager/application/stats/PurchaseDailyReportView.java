package com.shindong.smartmanager.application.stats;

import java.math.BigDecimal;
import java.time.LocalDate;

public record PurchaseDailyReportView(
        long historyId,
        String ledgerKind,
        long companyId,
        String companyName,
        Long itemId,
        String itemNo,
        String itemName,
        String unit,
        String processName,
        LocalDate inputDate,
        LocalDate receiptDate,
        BigDecimal currentStockQty,
        BigDecimal receiptQty,
        BigDecimal passedQty,
        BigDecimal failedQty,
        BigDecimal standardUnitPrice,
        BigDecimal unitPrice,
        BigDecimal amount,
        String division,
        String approvalStatus,
        int fiscalYear,
        int fiscalMonth,
        /** 해당 거래처의 선택 연·월 금액 합 (검색 결과와 무관) */
        BigDecimal monthTotal,
        /** 해당 거래처의 선택 연도 금액 합 (검색 결과와 무관) */
        BigDecimal yearTotal
) {
}
