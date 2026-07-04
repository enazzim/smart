package com.shindong.smartmanager.api.web.workcenter;

import com.shindong.smartmanager.application.workcenter.WorkCenterView;
import java.time.Instant;

public record WorkCenterResponse(
        long id,
        String wcName,
        long mainProcessCodeId,
        String mainProcessCode,
        String mainProcessName,
        int operationTime,
        Instant createdAt
) {
    static WorkCenterResponse from(WorkCenterView view) {
        return new WorkCenterResponse(
                view.id(),
                view.wcName(),
                view.mainProcessCodeId(),
                view.mainProcessCode(),
                view.mainProcessName(),
                view.operationTime(),
                view.createdAt()
        );
    }
}
