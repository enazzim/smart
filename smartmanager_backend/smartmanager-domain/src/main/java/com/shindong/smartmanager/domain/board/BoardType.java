package com.shindong.smartmanager.domain.board;

public enum BoardType {
    NOTICE,
    PRESIDENT_NOTICE,
    PRODUCT,
    LASER,
    INSTITUTE,
    SALES_QC;

    public String displayTitle() {
        return switch (this) {
            case NOTICE -> "공지사항";
            case PRESIDENT_NOTICE -> "대표공지";
            case PRODUCT -> "생산자재공지";
            case LASER -> "레이저 공지";
            case INSTITUTE -> "연구소 공지";
            case SALES_QC -> "영업 QC공지";
        };
    }
}
