package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.workcenter.WorkCenterCommand;
import com.shindong.smartmanager.application.workcenter.WorkCenterService;
import com.shindong.smartmanager.application.workcenter.WorkCenterView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class WorkCenterApplicationService {

    private final WorkCenterService workCenterService;

    public WorkCenterApplicationService(WorkCenterService workCenterService) {
        this.workCenterService = workCenterService;
    }

    @Transactional
    public WorkCenterView register(WorkCenterCommand command, String actorUserId) {
        return workCenterService.register(command, actorUserId);
    }

    @Transactional(readOnly = true)
    public List<WorkCenterView> listActive(String query) {
        return workCenterService.listActive(query);
    }

    @Transactional(readOnly = true)
    public WorkCenterView getActive(long id) {
        return workCenterService.getActive(id);
    }

    @Transactional
    public WorkCenterView update(long id, WorkCenterCommand command, String actorUserId) {
        return workCenterService.update(id, command, actorUserId);
    }

    @Transactional
    public void delete(long id, String actorUserId) {
        workCenterService.delete(id, actorUserId);
    }
}
