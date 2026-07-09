package com.shindong.smartmanager.api.web.outsource;

import com.shindong.smartmanager.application.outsource.OutsourcingOrderPrintLineView;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderPrintView;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.List;

final class OutsourcingOrderPrintHtmlRenderer {

    private static final DateTimeFormatter KOREAN_DATE = DateTimeFormatter.ofPattern("yyyy년 M월 d일");

    private OutsourcingOrderPrintHtmlRenderer() {
    }

    static String render(OutsourcingOrderPrintView view) {
        return renderBatch(List.of(view));
    }

    static String renderBatch(List<OutsourcingOrderPrintView> views) {
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
                  <title>외주 주문서</title>
                  <style>
                    @page { size: A4; margin: 10mm; }
                    body { font-family: "Malgun Gothic", sans-serif; font-size: 10pt; color: #111; margin: 0; }
                    .page { position: relative; min-height: 277mm; }
                    .page-break { page-break-before: always; }
                    h1 { text-align: center; letter-spacing: 0.45em; font-size: 20pt; margin: 0 0 10px; font-weight: 700; }
                    .issuer-top { text-align: right; font-size: 11pt; font-weight: 700; margin-bottom: 6px; }
                    .header-grid { display: grid; grid-template-columns: 1fr 1fr; gap: 8px 16px; margin-bottom: 8px; font-size: 9.5pt; }
                    .header-grid p { margin: 2px 0; }
                    .intro { text-align: center; margin: 8px 0 10px; font-size: 9.5pt; line-height: 1.5; }
                    table.items { width: 100%%; border-collapse: collapse; table-layout: fixed; }
                    table.items th, table.items td { border: 1px solid #333; padding: 3px 4px; font-size: 8.5pt; word-break: break-all; }
                    table.items th { background: #f3f4f6; text-align: center; font-weight: 600; }
                    .center { text-align: center; }
                    .right { text-align: right; }
                    .footer-grid { display: grid; grid-template-columns: 1.1fr 1.4fr 0.9fr; gap: 8px; margin-top: 10px; align-items: stretch; }
                    .coop-box { border: 1px solid #333; padding: 6px 8px; font-size: 8.5pt; }
                    .coop-box h3 { margin: 0 0 6px; font-size: 9pt; text-align: center; }
                    .coop-box ol { margin: 0; padding-left: 18px; }
                    .notes-box { border: 1px solid #333; min-height: 92px; padding: 6px 8px; font-size: 8.5pt; }
                    .notes-box .issuer { text-align: center; margin-top: 48px; font-weight: 700; }
                    .approval { border: 1px solid #333; display: grid; grid-template-rows: auto 1fr; }
                    .approval-title { text-align: center; border-bottom: 1px solid #333; padding: 4px; font-weight: 700; }
                    .approval-cols { display: grid; grid-template-columns: repeat(3, 1fr); min-height: 72px; }
                    .approval-cols div { border-right: 1px solid #333; text-align: center; padding-top: 4px; font-size: 8.5pt; }
                    .approval-cols div:last-child { border-right: none; }
                    .doc-footer { display: flex; justify-content: space-between; margin-top: 8px; font-size: 8pt; color: #444; }
                    .doc-footer .center-name { flex: 1; text-align: center; font-size: 10pt; font-weight: 700; }
                    .no-print { margin-bottom: 12px; }
                    @media print { .no-print { display: none; } }
                  </style>
                </head>
                <body>
                  <div class="no-print">
                    <button type="button" onclick="window.print()">인쇄</button>
                  </div>
                  %s
                </body>
                </html>
                """.formatted(pages);
    }

    private static String renderPage(OutsourcingOrderPrintView view) {
        StringBuilder rows = new StringBuilder();
        for (OutsourcingOrderPrintLineView line : view.lines()) {
            rows.append("""
                    <tr>
                      <td class="center">%d</td>
                      <td>%s</td>
                      <td class="center">%s</td>
                      <td class="center">%s</td>
                      <td>%s</td>
                      <td class="center">%s</td>
                      <td class="center">%s</td>
                      <td class="center">%s</td>
                      <td class="right">%s</td>
                      <td class="right">%s</td>
                      <td class="right">%s</td>
                      <td class="center">%s</td>
                      <td>%s</td>
                    </tr>
                    """.formatted(
                    line.lineNo(),
                    escape(line.itemName()),
                    escape(line.itemNo()),
                    escape(nullToDash(line.material())),
                    escape(nullToDash(line.standard())),
                    escape(line.beginProcessName()),
                    escape(line.endProcessName()),
                    escape(nullToDash(line.unit())),
                    formatQty(line.orderQty()),
                    formatAmount(line.unitPrice()),
                    formatAmount(line.amount()),
                    formatDeliveryDate(line.requestedDeliveryDate()),
                    escape(nullToDash(line.remarks()))
            ));
        }

        int blankRows = Math.max(0, 8 - view.lines().size());
        for (int i = 0; i < blankRows; i++) {
            rows.append("""
                    <tr>
                      <td>&nbsp;</td><td></td><td></td><td></td><td></td><td></td><td></td>
                      <td></td><td></td><td></td><td></td><td></td><td></td>
                    </tr>
                    """);
        }

        return """
                <div class="page">
                  <div class="issuer-top">%s</div>
                  <h1>외 주 주 문 서</h1>
                  <div class="header-grid">
                    <div>
                      <p>발주번호 : %s</p>
                      <p>%s</p>
                      <p>FAX : %s</p>
                    </div>
                    <div>
                      <p style="text-align:right;">주소 : %s</p>
                    </div>
                    <div>
                      <p>업체명 : <strong>%s</strong></p>
                    </div>
                    <div>
                      <p style="text-align:right;">전화 : %s &nbsp; FAX: %s</p>
                    </div>
                  </div>
                  <p class="intro">
                    아래와 같이 주문하오니 납기내 납품하여 주시기 바랍니다.<br/>
                    문의사항은 아래 발주담당자에게 문의요망.
                  </p>
                  <table class="items">
                    <thead>
                      <tr>
                        <th style="width:4%%">순번</th>
                        <th style="width:11%%">품명</th>
                        <th style="width:11%%">품번</th>
                        <th style="width:7%%">재질</th>
                        <th style="width:10%%">규격</th>
                        <th style="width:7%%">시작공정</th>
                        <th style="width:7%%">종료공정</th>
                        <th style="width:4%%">단위</th>
                        <th style="width:5%%">수량</th>
                        <th style="width:7%%">단가</th>
                        <th style="width:8%%">금액</th>
                        <th style="width:8%%">납기일</th>
                        <th style="width:11%%">비고</th>
                      </tr>
                    </thead>
                    <tbody>
                      %s
                    </tbody>
                  </table>
                  <div class="footer-grid">
                    <div class="coop-box">
                      <h3>거래처 협조사항</h3>
                      <ol>
                        <li>납품서에 품번을 반드시 명기할 것</li>
                        <li>주문서의 단가 이상시 담당자에게 연락할 것</li>
                      </ol>
                    </div>
                    <div class="notes-box">
                      <div>발주자 : %s</div>
                      <div class="issuer">%s</div>
                    </div>
                    <div class="approval">
                      <div class="approval-title">결 재</div>
                      <div class="approval-cols">
                        <div>담당</div>
                        <div>차장</div>
                        <div>전무</div>
                      </div>
                    </div>
                  </div>
                  <div class="doc-footer">
                    <span>SDQ-05-002-4</span>
                    <span class="center-name">%s</span>
                    <span>A4(210X297)</span>
                  </div>
                </div>
                """.formatted(
                escape(view.issuerCompanyName()),
                escape(view.orderNos()),
                formatKoreanDate(view.orderDate()),
                escape(nullToDash(view.issuerFax())),
                escape(nullToDash(view.issuerAddress())),
                escape(view.partnerName()),
                escape(nullToDash(view.partnerTelephone())),
                escape(nullToDash(view.partnerFax())),
                rows,
                escape(nullToDash(view.orderManagerName())),
                escape(view.issuerCompanyName()),
                escape(view.issuerCompanyName())
        );
    }

    private static String formatKoreanDate(LocalDate date) {
        return date != null ? date.format(KOREAN_DATE) : "";
    }

    private static String formatDeliveryDate(LocalDate date) {
        if (date == null) {
            return "";
        }
        return date.format(DateTimeFormatter.ofPattern("yyyy/ M/ d"));
    }

    private static String formatQty(BigDecimal value) {
        if (value == null) {
            return "";
        }
        return value.stripTrailingZeros().toPlainString();
    }

    private static String formatAmount(BigDecimal value) {
        if (value == null || value.compareTo(BigDecimal.ZERO) == 0) {
            return "";
        }
        return String.format("%,.0f", value);
    }

    private static String nullToDash(String value) {
        return value == null || value.isBlank() ? "" : value;
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
