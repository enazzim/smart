package com.shindong.smartmanager.api.web.outsource;

import com.shindong.smartmanager.application.outsource.OutsourceAdvanceProcessOptionView;

public record OutsourceAdvanceProcessOptionResponse(
        long beginProcessCodeId,
        String beginProcessName,
        long endProcessCodeId,
        String endProcessName
) {
    public static OutsourceAdvanceProcessOptionResponse from(OutsourceAdvanceProcessOptionView view) {
        return new OutsourceAdvanceProcessOptionResponse(
                view.beginProcessCodeId(),
                view.beginProcessName(),
                view.endProcessCodeId(),
                view.endProcessName()
        );
    }
}
