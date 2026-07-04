package com.shindong.smartmanager.application.publiccode;

public record CreateSmallCommand(
        String largeCode,
        String smallCode,
        String smallName
) {
}
