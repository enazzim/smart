package com.shindong.smartmanager.api.web.workstandard;

import com.shindong.smartmanager.application.workstandard.WorkStandardView;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import java.time.Instant;

public record WorkStandardResponse(
        long id,
        long itemId,
        String itemNum,
        String itemName,
        long processSequenceId,
        short processSequenceNum,
        long processCodeId,
        String processCode,
        String processName,
        WorkDistinction workDistinction,
        long workCenterId,
        String wcName,
        Long equipmentId,
        int priorityOrder,
        Long mainWorkerId,
        String toolName,
        int setupTime,
        int standardTime,
        Instant createdAt
) {
    static WorkStandardResponse from(WorkStandardView view) {
        return new WorkStandardResponse(
                view.id(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.processSequenceId(),
                view.processSequenceNum(),
                view.processCodeId(),
                view.processCode(),
                view.processName(),
                view.workDistinction(),
                view.workCenterId(),
                view.wcName(),
                view.equipmentId(),
                view.priorityOrder(),
                view.mainWorkerId(),
                view.toolName(),
                view.setupTime(),
                view.standardTime(),
                view.createdAt()
        );
    }
}
