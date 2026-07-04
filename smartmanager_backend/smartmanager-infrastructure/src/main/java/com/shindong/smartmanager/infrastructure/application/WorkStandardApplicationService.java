package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.workstandard.WorkStandardCommand;
import com.shindong.smartmanager.application.workstandard.WorkStandardService;
import com.shindong.smartmanager.application.workstandard.WorkStandardUpdateCommand;
import com.shindong.smartmanager.application.workstandard.WorkStandardView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class WorkStandardApplicationService {

    private final WorkStandardService workStandardService;
    private final ItemRepository itemRepository;

    public WorkStandardApplicationService(
            WorkStandardService workStandardService,
            ItemRepository itemRepository
    ) {
        this.workStandardService = workStandardService;
        this.itemRepository = itemRepository;
    }

    @Transactional
    public WorkStandardView register(
            String itemNum,
            long processSequenceId,
            long workCenterId,
            Long equipmentId,
            int priorityOrder,
            Long mainWorkerId,
            String toolName,
            int setupTime,
            int standardTime,
            String actorUserId
    ) {
        ItemView item = itemRepository.findActiveByItemNo(itemNum)
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + itemNum));
        return workStandardService.register(
                new WorkStandardCommand(
                        item.id(),
                        processSequenceId,
                        workCenterId,
                        equipmentId,
                        priorityOrder,
                        mainWorkerId,
                        toolName,
                        setupTime,
                        standardTime
                ),
                actorUserId
        );
    }

    @Transactional
    public WorkStandardView update(long id, WorkStandardUpdateCommand command, String actorUserId) {
        return workStandardService.update(id, command, actorUserId);
    }

    @Transactional
    public void delete(long id, String actorUserId) {
        workStandardService.delete(id, actorUserId);
    }

    @Transactional(readOnly = true)
    public List<WorkStandardView> listActive(String itemNum) {
        return workStandardService.listActive(itemNum);
    }

    @Transactional(readOnly = true)
    public WorkStandardView getActive(long id) {
        return workStandardService.getActive(id);
    }

    @Transactional
    public int copyStandards(String sourceItemNum, String targetItemNum, String actorUserId) {
        return workStandardService.copyStandards(sourceItemNum, targetItemNum, actorUserId);
    }
}
