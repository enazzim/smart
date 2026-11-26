package com.shindong.smartmanager.application.menu;

import java.util.List;

/** 그룹 허용 대상이 되는 왼쪽 하위 메뉴. 그룹별 화면 설정 메뉴 자신은 포함하지 않는다. */
public final class GroupMenuCatalog {

    public record MenuItem(String categoryLabel, String menuId, String menuLabel) {
    }

    public static final List<MenuItem> MENUS = List.of(
            item("영업", "sales-order", "수주"),
            item("영업", "sales-shipment", "출고·납품"),
            item("영업", "sales-revenue", "매출"),
            item("영업", "sales-collection", "수금"),
            item("생산", "prod-plan", "생산계획"),
            item("생산", "prod-mrp", "자재소요"),
            item("생산", "prod-work-plan", "작업계획"),
            item("생산", "prod-schedule", "작업장 부하"),
            item("생산", "prod-work-order", "작업지시"),
            item("생산", "prod-material-issue", "자재투입"),
            item("생산", "prod-work-diary", "작업일보"),
            item("구매", "purchase-order", "구매발주"),
            item("구매", "purchase-receipt", "구매입고"),
            item("구매", "purchase-etc-order", "기타구매발주"),
            item("구매", "purchase-etc-receipt", "기타구매입고"),
            item("구매", "purchase-etc-claim", "기타공제등록"),
            item("구매", "purchase-defect-claim", "불량변상"),
            item("구매", "purchase-payable-approval", "승인처리"),
            item("구매", "purchase-payment", "지급"),
            item("외주", "outsource-order", "외주발주"),
            item("외주", "outsource-shipment", "출고"),
            item("외주", "outsource-receipt", "입고"),
            item("재고", "inventory-misc-movement", "기타 입출고"),
            item("재고", "inventory-ledger", "재고·원장"),
            item("재고", "inventory-lot", "Lot 마스터"),
            item("재고", "inventory-stocktake", "재고실사"),
            item("품질", "quality-inspection", "품질검사"),
            item("통계및 지표", "stats-vendor-purchase-status", "매입처별 매입현황"),
            item("통계및 지표", "stats-purchase-daily", "매입일보"),
            item("통계및 지표", "stats-partner-monthly-payable", "월별 실지급액"),
            item("통계및 지표", "stats-warehouse-io", "창고별 수불현황"),
            item("통계및 지표", "stats-item-io", "품목별 수불현황"),
            item("통계및 지표", "stats-order-vs-receipt", "발주대비입고"),
            item("시스템정보", "systemSettings", "시스템 설정"),
            item("시스템정보", "masterImport", "초기정보 일괄입력"),
            item("시스템정보", "publicCode", "공용코드"),
            item("시스템정보", "role", "권한설정"),
            item("시스템정보", "monthClosing", "월마감")
    );

    private GroupMenuCatalog() {
    }

    public static boolean contains(String menuId) {
        return MENUS.stream().anyMatch(menu -> menu.menuId().equals(menuId));
    }

    private static MenuItem item(String categoryLabel, String menuId, String menuLabel) {
        return new MenuItem(categoryLabel, menuId, menuLabel);
    }
}
