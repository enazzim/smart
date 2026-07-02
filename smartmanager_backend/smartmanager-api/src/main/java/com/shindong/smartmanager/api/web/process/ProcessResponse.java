package com.shindong.smartmanager.api.web.process;

import com.shindong.smartmanager.application.process.ProcessView;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import java.time.Instant;

public record ProcessResponse(
        long id,
        long itemId,
        String itemNo,
        String itemName,
        short processSequenceNum,
        long processCodeId,
        String processCode,
        String processName,
        WorkDistinction workDistinction,
        Long workCenterId,
        String wcName,
        int outsideOrderRate,
        short progressRate,
        Instant createdAt
) {

    public static ProcessResponse from(ProcessView view) {
        return new ProcessResponse(
                view.id(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.processSequenceNum(),
                view.processCodeId(),
                view.processCode(),
                view.processName(),
                view.workDistinction(),
                view.workCenterId(),
                view.wcName(),
                view.outsideOrderRate(),
                view.progressRate(),
                view.createdAt()
        );
    }
}
