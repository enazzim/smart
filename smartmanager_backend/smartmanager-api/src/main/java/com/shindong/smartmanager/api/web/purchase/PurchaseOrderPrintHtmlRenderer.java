package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.application.purchase.PurchaseOrderPrintLineView;
import com.shindong.smartmanager.application.purchase.PurchaseOrderPrintView;
import com.shindong.smartmanager.domain.company.BusinessRegNos;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.List;

final class PurchaseOrderPrintHtmlRenderer {

    private static final DateTimeFormatter KOREAN_DATE = DateTimeFormatter.ofPattern("yyyy년 M월 d일");

    private PurchaseOrderPrintHtmlRenderer() {
    }

    static String render(PurchaseOrderPrintView view) {
        return renderBatch(List.of(view));
    }

    static String renderBatch(List<PurchaseOrderPrintView> views) {
        StringBuilder pages = new StringBuilder();
        for (int i = 0; i < views.size(); i++) {
            if (i > 0) {
                pages.append("<div class=\"page-break\"></div>");
            }
            pages.append(renderPage(views.get(i)));
        }

        return """
                <!DOCTYPE html>
                <html lang="ko">
                <head>
                  <meta charset="UTF-8"/>
                  <title>주문서</title>
                  <style>
                    @page { size: A4; margin: 12mm; }
                    body { font-family: "Malgun Gothic", sans-serif; font-size: 11pt; color: #111; margin: 0; }
                    .page-break { page-break-before: always; }
                    h1 { text-align: center; letter-spacing: 0.5em; font-size: 22pt; margin: 0 0 16px; }
                    .meta { display: flex; justify-content: space-between; gap: 24px; margin-bottom: 12px; }
                    .meta-box { flex: 1; border: 1px solid #333; padding: 10px 12px; min-height: 88px; }
                    .meta-box h2 { margin: 0 0 8px; font-size: 11pt; }
                    .meta-box p { margin: 2px 0; font-size: 10pt; }
                    .intro { margin: 12px 0; font-size: 10pt; line-height: 1.5; }
                    table { width: 100%%; border-collapse: collapse; margin-top: 8px; }
                    th, td { border: 1px solid #333; padding: 5px 6px; font-size: 9.5pt; }
                    th { background: #f3f4f6; text-align: center; }
                    .center { text-align: center; }
                    .right { text-align: right; }
                    .footer { margin-top: 14px; font-size: 10pt; }
                    .total { text-align: right; font-weight: bold; margin-top: 8px; font-size: 11pt; }
                    .note { margin-top: 10px; font-size: 9pt; color: #444; line-height: 1.45; }
                    @media print {
                      .no-print { display: none; }
                    }
                  </style>
                </head>
                <body>
                  <div class="no-print" style="margin-bottom: 12px;">
                    <button type="button" onclick="window.print()">인쇄</button>
                  </div>
                  %s
                </body>
                </html>
                """.formatted(pages);
    }

    private static String renderPage(PurchaseOrderPrintView view) {
        StringBuilder rows = new StringBuilder();
        int index = 1;
        for (PurchaseOrderPrintLineView line : view.lines()) {
            rows.append("""
                    <tr>
                      <td class="center">%d</td>
                      <td>%s</td>
                      <td class="center">%s</td>
                      <td>%s</td>
                      <td class="center">%s</td>
                      <td class="right">%s</td>
                      <td class="right">%s</td>
                      <td class="right">%s</td>
                      <td class="center">%s</td>
                    </tr>
                    """.formatted(
                    index++,
                    escape(line.itemName()),
                    escape(line.itemNo()),
                    escape(nullToDash(line.standard())),
                    escape(line.unit()),
                    formatQty(line.orderQty()),
                    formatAmount(line.unitPrice()),
                    formatAmount(line.amount()),
                    formatDeliveryDate(line.requestedDeliveryDate())
            ));
        }

        return """
                  <h1>주 문 서</h1>
                  <div class="meta">
                    <div class="meta-box">
                      <h2>수신 (구매거래처)</h2>
                      <p><strong>%s</strong></p>
                      <p>사업자번호: %s</p>
                    </div>
                    <div class="meta-box">
                      <h2>발신</h2>
                      <p><strong>%s</strong></p>
                      <p>%s</p>
                      <p>TEL %s / FAX %s</p>
                    </div>
                  </div>
                  <p class="intro">
                    %s<br/>
                    아래와 같이 주문하오니 납기 내 납품하여 주시기 바랍니다.<br/>
                    문의사항은 아래 발주담당자에게 연락 바랍니다. (발주담당: %s)
                  </p>
                  <table>
                    <thead>
                      <tr>
                        <th style="width:4%%">순번</th>
                        <th style="width:22%%">품명</th>
                        <th style="width:12%%">품번</th>
                        <th style="width:12%%">규격</th>
                        <th style="width:6%%">단위</th>
                        <th style="width:8%%">수량</th>
                        <th style="width:10%%">단가</th>
                        <th style="width:12%%">금액</th>
                        <th style="width:10%%">납기일</th>
                      </tr>
                    </thead>
                    <tbody>
                      %s
                    </tbody>
                  </table>
                  <p class="total">총금액: %s원</p>
                  <p class="note">
                    ※ 납품서에는 품번을 반드시 기재해 주십시오.<br/>
                    ※ 단가 상이 시 발주담당자에게 사전 연락 바랍니다.
                  </p>
                """.formatted(
                escape(view.partnerName()),
                escape(nullToDash(BusinessRegNos.formatForDisplay(view.partnerBusinessRegNo()))),
                escape(view.issuerCompanyName()),
                escape(nullToDash(view.issuerAddress())),
                escape(nullToDash(view.issuerPhone())),
                escape(nullToDash(view.issuerFax())),
                formatKoreanDate(view.orderDate()),
                escape(nullToDash(view.orderManagerName())),
                rows,
                formatAmount(view.totalAmount())
        );
    }

    private static String formatKoreanDate(LocalDate date) {
        return date != null ? date.format(KOREAN_DATE) : "";
    }

    private static String formatDeliveryDate(LocalDate date) {
        if (date == null) {
            return "—";
        }
        return date.toString().replace('-', '/');
    }

    private static String formatQty(BigDecimal value) {
        if (value == null) {
            return "0";
        }
        return value.stripTrailingZeros().toPlainString();
    }

    private static String formatAmount(BigDecimal value) {
        if (value == null) {
            return "0";
        }
        return String.format("%,.2f", value);
    }

    private static String nullToDash(String value) {
        return value == null || value.isBlank() ? "—" : value;
    }

    private static String escape(String value) {
        if (value == null) {
            return "";
        }
        return value
                .replace("&", "&amp;")
                .replace("<", "&lt;")
                .replace(">", "&gt;")
                .replace("\"", "&quot;");
    }
}
