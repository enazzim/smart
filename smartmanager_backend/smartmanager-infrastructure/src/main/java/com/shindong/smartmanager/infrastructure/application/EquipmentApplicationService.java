package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.equipment.EquipmentCommand;
import com.shindong.smartmanager.application.equipment.EquipmentService;
import com.shindong.smartmanager.application.equipment.EquipmentUpdateCommand;
import com.shindong.smartmanager.application.equipment.EquipmentView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class EquipmentApplicationService {

    private final EquipmentService equipmentService;

    public EquipmentApplicationService(EquipmentService equipmentService) {
        this.equipmentService = equipmentService;
    }

    @Transactional
    public EquipmentView register(EquipmentCommand command, String actorUserId) {
        return equipmentService.register(command, actorUserId);
    }

    @Transactional
    public EquipmentView update(long id, EquipmentUpdateCommand command, String actorUserId) {
        return equipmentService.update(id, command, actorUserId);
    }

    @Transactional
    public void delete(long id, String actorUserId) {
        equipmentService.delete(id, actorUserId);
    }

    @Transactional(readOnly = true)
    public List<EquipmentView> listActive(String query) {
        return equipmentService.listActive(query);
    }

    @Transactional(readOnly = true)
    public EquipmentView getActive(long id) {
        return equipmentService.getActive(id);
    }
}
