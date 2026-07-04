package com.shindong.smartmanager.application.publiccode;

public record CreateLargeCommand(
        String largeCode,
        String largeName,
        String usageType
) {
}
