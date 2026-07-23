package com.shindong.smartmanager.domain.company;

/**
 * 사업자등록번호 정규화·표시 포맷.
 * 표준 10자리 → XXX-XX-XXXXX, 그 외는 trim 원문 유지.
 */
public final class BusinessRegNos {

    private BusinessRegNos() {
    }

    public static String digitsOnly(String value) {
        if (value == null) {
            return "";
        }
        return value.replaceAll("\\D", "");
    }

    public static boolean isStandardTenDigit(String value) {
        return digitsOnly(value).length() == 10;
    }

    /** 저장·UK 조회용: 10자리면 XXX-XX-XXXXX, 아니면 trim */
    public static String canonicalize(String value) {
        if (value == null) {
            return "";
        }
        String raw = value.trim();
        String digits = digitsOnly(raw);
        if (digits.length() == 10) {
            return format(digits);
        }
        return raw;
    }

    /** 화면·문서 표시용 */
    public static String formatForDisplay(String value) {
        if (value == null || value.isBlank()) {
            return "";
        }
        String digits = digitsOnly(value);
        if (digits.length() == 10) {
            return format(digits);
        }
        return value.trim();
    }

    private static String format(String tenDigits) {
        return tenDigits.substring(0, 3)
                + "-"
                + tenDigits.substring(3, 5)
                + "-"
                + tenDigits.substring(5);
    }
}
