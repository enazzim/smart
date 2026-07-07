package com.shindong.smartmanager.application.common;

public record BulkFailure(int rowIndex, String key, String message) {
}
