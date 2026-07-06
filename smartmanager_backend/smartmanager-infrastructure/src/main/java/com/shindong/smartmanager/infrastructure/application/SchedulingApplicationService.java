package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.production.WorkCenterLoadQuery;
import com.shindong.smartmanager.application.production.WorkCenterLoadQueryService;
import com.shindong.smartmanager.application.production.WorkCenterLoadResult;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class SchedulingApplicationService {

    private final WorkCenterLoadQueryService workCenterLoadQueryService;

    public SchedulingApplicationService(WorkCenterLoadQueryService workCenterLoadQueryService) {
        this.workCenterLoadQueryService = workCenterLoadQueryService;
    }

    @Transactional(readOnly = true)
    public WorkCenterLoadResult queryWorkCenterLoad(WorkCenterLoadQuery query) {
        return workCenterLoadQueryService.query(query);
    }
}
