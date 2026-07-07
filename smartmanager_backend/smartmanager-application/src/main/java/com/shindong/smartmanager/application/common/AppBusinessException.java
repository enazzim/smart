package com.shindong.smartmanager.application.common;

public class AppBusinessException extends RuntimeException {

    private final AppErrorCode errorCode;

    public AppBusinessException(AppErrorCode errorCode, String message) {
        super(message);
        this.errorCode = errorCode;
    }

    public AppErrorCode errorCode() {
        return errorCode;
    }
}
