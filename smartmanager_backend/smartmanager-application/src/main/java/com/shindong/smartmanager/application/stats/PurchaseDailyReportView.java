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
        /** 선급 상계액 (partner_prepaid_offset 합). 없으면 0 */
        BigDecimal offsetAmount,
        /** 승인 행의 실지급대상 증가분 (amount − offset). 미승인·공제는 0 */
        BigDecimal unpaidIncrease,
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
