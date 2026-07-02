package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.process.ProcessCommand;
import com.shindong.smartmanager.application.process.ProcessService;
import com.shindong.smartmanager.application.process.ProcessUpdateCommand;
import com.shindong.smartmanager.application.process.ProcessView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class ProcessApplicationService {

    private final ProcessService processService;

    public ProcessApplicationService(ProcessService processService) {
        this.processService = processService;
    }

    @Transactional
    public ProcessView register(ProcessCommand command, String actorUserId) {
        return processService.register(command, actorUserId);
    }

    @Transactional
    public ProcessView update(long id, ProcessUpdateCommand command, String actorUserId) {
        return processService.update(id, command, actorUserId);
    }

    @Transactional
    public void delete(long id, String actorUserId) {
        processService.delete(id, actorUserId);
    }

    @Transactional(readOnly = true)
    public List<ProcessView> listActiveByItemId(long itemId) {
        return processService.listActiveByItemId(itemId);
    }

    @Transactional(readOnly = true)
    public List<ProcessView> listActiveAll() {
        return processService.listActiveAll();
    }

    @Transactional(readOnly = true)
    public ProcessView getActive(long id) {
        return processService.getActive(id);
    }
}
