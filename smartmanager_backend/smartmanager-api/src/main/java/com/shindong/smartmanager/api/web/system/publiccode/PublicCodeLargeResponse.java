package com.shindong.smartmanager.api.web.system.publiccode;

import com.shindong.smartmanager.application.publiccode.PublicCodeLargeView;
import com.shindong.smartmanager.application.publiccode.PublicCodeSmallView;

public record PublicCodeLargeResponse(
        long id,
        String largeCode,
        String largeName,
        String usageType
) {
    public static PublicCodeLargeResponse from(PublicCodeLargeView view) {
        return new PublicCodeLargeResponse(
                view.id(),
                view.largeCode(),
                view.largeName(),
                view.usageType()
        );
    }
}
