package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.inventory.CreateMiscStockMovementCommand;
import com.shindong.smartmanager.application.inventory.MiscStockMovementListCriteria;
import com.shindong.smartmanager.application.inventory.MiscStockMovementService;
import com.shindong.smartmanager.application.inventory.MiscStockMovementView;
import java.time.LocalDate;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class MiscStockMovementApplicationService {

    private final MiscStockMovementService miscStockMovementService;

    public MiscStockMovementApplicationService(MiscStockMovementService miscStockMovementService) {
        this.miscStockMovementService = miscStockMovementService;
    }

    @Transactional(readOnly = true)
    public List<MiscStockMovementView> list(MiscStockMovementListCriteria criteria) {
        return miscStockMovementService.list(criteria);
    }

    @Transactional
    public com.shindong.smartmanager.application.inventory.MiscStockMovementPreviewView preview(
            long itemId,
            Long processSequenceId,
            LocalDate movementDate,
            String actorUserId
    ) {
        return miscStockMovementService.preview(itemId, processSequenceId, movementDate, actorUserId);
    }

    @Transactional
    public MiscStockMovementView register(CreateMiscStockMovementCommand command, String actorUserId) {
        return miscStockMovementService.register(command, actorUserId);
    }

    @Transactional
    public MiscStockMovementView update(long id, CreateMiscStockMovementCommand command, String actorUserId) {
        return miscStockMovementService.update(id, command, actorUserId);
    }

    @Transactional
    public void delete(long id, String actorUserId) {
        miscStockMovementService.delete(id, actorUserId);
    }
}
