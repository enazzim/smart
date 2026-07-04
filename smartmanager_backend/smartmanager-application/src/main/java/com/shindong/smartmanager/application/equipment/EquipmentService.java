package com.shindong.smartmanager.application.equipment;

import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.process.WorkCenterLookup;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import java.util.List;

public class EquipmentService {

    private final EquipmentRepository equipmentRepository;
    private final EquipmentCategoryLookup equipmentCategoryLookup;
    private final WorkCenterLookup workCenterLookup;
    private final DomainEventStore domainEventStore;

    public EquipmentService(
            EquipmentRepository equipmentRepository,
            EquipmentCategoryLookup equipmentCategoryLookup,
            WorkCenterLookup workCenterLookup,
            DomainEventStore domainEventStore
    ) {
        this.equipmentRepository = equipmentRepository;
        this.equipmentCategoryLookup = equipmentCategoryLookup;
        this.workCenterLookup = workCenterLookup;
        this.domainEventStore = domainEventStore;
    }

    public EquipmentView register(EquipmentCommand command, String actorUserId) {
        validateCommand(command);
        getAllowedCategory(command.equipmentCategoryId());
        validateWorkCenter(command.workCenterId());

        if (equipmentRepository.existsActiveByEquipmentNum(command.equipmentNum(), null)) {
            throw new IllegalArgumentException("이미 등록된 설비번호입니다: " + command.equipmentNum());
        }

        long id = equipmentRepository.save(command, actorUserId);
        appendEvent(EventTypes.EQUIPMENT_REGISTERED, id, actorUserId, command.equipmentNum());
        return getActive(id);
    }

    public EquipmentView update(long id, EquipmentUpdateCommand command, String actorUserId) {
        EquipmentView existing = getActive(id);
        validateUpdateCommand(command);
        getAllowedCategory(command.equipmentCategoryId());
        validateWorkCenter(command.workCenterId());

        equipmentRepository.update(id, command, actorUserId);
        appendEvent(EventTypes.EQUIPMENT_UPDATED, id, actorUserId, existing.equipmentNum());
        return getActive(id);
    }

    public void delete(long id, String actorUserId) {
        EquipmentView existing = getActive(id);
        if (equipmentRepository.isReferencedByActiveWorkStandard(id)) {
            throw new IllegalArgumentException("작업표준에서 사용 중인 설비는 삭제할 수 없습니다.");
        }

        equipmentRepository.softDelete(id, actorUserId);
        domainEventStore.append(DomainEvent.create(
                EventTypes.EQUIPMENT_DELETED,
                1,
                AggregateTypes.EQUIPMENT,
                String.valueOf(id),
                actorUserId,
                """
                {"equipmentId":%d,"equipmentNum":"%s"}
                """.formatted(id, escape(existing.equipmentNum())).trim()
        ));
    }

    public List<EquipmentView> listActive(String query) {
        return equipmentRepository.findAllActive(query);
    }

    public EquipmentView getActive(long id) {
        return equipmentRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("설비를 찾을 수 없습니다: " + id));
    }

    public static boolean isReplacementDue(int designShot, int accumulatedShot) {
        return designShot > 0 && accumulatedShot >= designShot;
    }

    private void validateCommand(EquipmentCommand command) {
        if (command.equipmentNum() == null || command.equipmentNum().isBlank()) {
            throw new IllegalArgumentException("설비번호는 필수입니다.");
        }
        if (command.equipmentName() == null || command.equipmentName().isBlank()) {
            throw new IllegalArgumentException("설비명은 필수입니다.");
        }
        validateShots(command.designShot(), command.initialShot());
    }

    private void validateUpdateCommand(EquipmentUpdateCommand command) {
        if (command.equipmentName() == null || command.equipmentName().isBlank()) {
            throw new IllegalArgumentException("설비명은 필수입니다.");
        }
        validateShots(command.designShot(), command.initialShot());
    }

    private void validateShots(int designShot, int initialShot) {
        if (designShot < 0) {
            throw new IllegalArgumentException("설계샷은 0 이상이어야 합니다.");
        }
        if (initialShot < 0) {
            throw new IllegalArgumentException("초기샷은 0 이상이어야 합니다.");
        }
    }

    private EquipmentCategoryLookup.CategoryInfo getAllowedCategory(long categoryId) {
        return equipmentCategoryLookup.findActiveCategory(categoryId)
                .orElseThrow(() -> new IllegalArgumentException("설비분류를 찾을 수 없습니다: " + categoryId));
    }

    private void validateWorkCenter(Long workCenterId) {
        if (workCenterId != null && !workCenterLookup.existsActive(workCenterId)) {
            throw new IllegalArgumentException("작업장을 찾을 수 없습니다: " + workCenterId);
        }
    }

    private void appendEvent(String eventType, long id, String actorUserId, String equipmentNum) {
        domainEventStore.append(DomainEvent.create(
                eventType,
                1,
                AggregateTypes.EQUIPMENT,
                String.valueOf(id),
                actorUserId,
                """
                {"equipmentId":%d,"equipmentNum":"%s"}
                """.formatted(id, escape(equipmentNum)).trim()
        ));
    }

    private static String escape(String value) {
        return value.replace("\\", "\\\\").replace("\"", "\\\"");
    }
}
