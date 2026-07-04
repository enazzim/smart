package com.shindong.smartmanager.application.publiccode;

public record PublicCodeSmallView(
        long id,
        String largeCode,
        String largeName,
        String smallCode,
        String smallName,
        String usageType
) {
}
