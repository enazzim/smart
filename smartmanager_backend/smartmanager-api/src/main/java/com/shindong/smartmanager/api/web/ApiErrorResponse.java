package com.shindong.smartmanager.api.web;

public record ApiErrorResponse(
        String errorCode,
        String message
) {
}
