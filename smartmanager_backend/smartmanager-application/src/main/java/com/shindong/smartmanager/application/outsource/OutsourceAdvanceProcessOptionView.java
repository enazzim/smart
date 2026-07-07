package com.shindong.smartmanager.application.outsource;

public record OutsourceAdvanceProcessOptionView(
        long beginProcessCodeId,
        String beginProcessName,
        long endProcessCodeId,
        String endProcessName
) {
}
