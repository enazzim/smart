package com.shindong.smartmanager.api.web.system.publiccode;

import com.shindong.smartmanager.application.publiccode.PublicCodeSmallView;

public record PublicCodeSmallResponse(
        long id,
        String largeCode,
        String largeName,
        String smallCode,
        String smallName,
        String usageType
) {
    public static PublicCodeSmallResponse from(PublicCodeSmallView view) {
        return new PublicCodeSmallResponse(
                view.id(),
                view.largeCode(),
                view.largeName(),
                view.smallCode(),
                view.smallName(),
                view.usageType()
        );
    }
}
