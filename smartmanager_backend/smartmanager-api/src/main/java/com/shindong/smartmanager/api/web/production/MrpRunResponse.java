package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.application.production.MrpRunView;
import java.time.Instant;

public record MrpRunResponse(
        long id,
        String runNo,
        int planCount,
        int lineCount,
        Instant createdAt,
        String createdBy,
        boolean cancellable
) {
    public static MrpRunResponse from(MrpRunView view) {
        return new MrpRunResponse(
                view.id(),
                view.runNo(),
                view.planCount(),
                view.lineCount(),
                view.createdAt(),
                view.createdBy(),
                view.cancellable()
        );
    }
}
