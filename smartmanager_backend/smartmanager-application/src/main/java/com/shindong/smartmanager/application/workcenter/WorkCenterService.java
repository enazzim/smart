package com.shindong.smartmanager.application.workcenter;

import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.process.ProcessCodeLookup;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import java.util.List;
import java.util.Set;

public class WorkCenterService {

    private static final Set<String> EXCLUDED_PROCESS_CODES = Set.of("14000000", "14009999");
    private static final int MAX_OPERATION_TIME = 1440;

    private final WorkCenterRepository workCenterRepository;
    private final ProcessCodeLookup processCodeLookup;
    private final DomainEventStore domainEventStore;

    public WorkCenterService(
            WorkCenterRepository workCenterRepository,
            ProcessCodeLookup processCodeLookup,
            DomainEventStore domainEventStore
    ) {
        this.workCenterRepository = workCenterRepository;
        this.processCodeLookup = processCodeLookup;
        this.domainEventStore = domainEventStore;
    }

    public WorkCenterView register(WorkCenterCommand command, String actorUserId) {
        validateCommand(command);
        ProcessCodeLookup.ProcessCodeInfo processCode = getAllowedProcessCode(command.mainProcessCodeId());
        if (workCenterRepository.existsActiveByWcName(command.wcName(), null)) {
            throw new IllegalArgumentException("이미 등록된 작업장명입니다: " + command.wcName());
        }

        long id = workCenterRepository.save(command, actorUserId);
        appendEvent(EventTypes.WORK_CENTER_REGISTERED, id, actorUserId, command, processCode);
        return getActive(id);
    }

    public WorkCenterView update(long id, WorkCenterCommand command, String actorUserId) {
        getActive(id);
        validateCommand(command);
        ProcessCodeLookup.ProcessCodeInfo processCode = getAllowedProcessCode(command.mainProcessCodeId());
        if (workCenterRepository.existsActiveByWcName(command.wcName(), id)) {
            throw new IllegalArgumentException("이미 등록된 작업장명입니다: " + command.wcName());
        }

        workCenterRepository.update(id, command, actorUserId);
        appendEvent(EventTypes.WORK_CENTER_UPDATED, id, actorUserId, command, processCode);
        return getActive(id);
    }

    public void delete(long id, String actorUserId) {
        WorkCenterView existing = getActive(id);
        if (workCenterRepository.isReferencedByActiveProcess(id)) {
            throw new IllegalArgumentException("공정에서 사용 중인 작업장은 삭제할 수 없습니다.");
        }

        workCenterRepository.softDelete(id, actorUserId);
        domainEventStore.append(DomainEvent.create(
                EventTypes.WORK_CENTER_DELETED,
                1,
                AggregateTypes.WORK_CENTER,
                String.valueOf(id),
                actorUserId,
                """
                {"workCenterId":%d,"wcName":"%s"}
                """.formatted(id, escape(existing.wcName())).trim()
        ));
    }

    public List<WorkCenterView> listActive(String query) {
        return workCenterRepository.findAllActive(query);
    }

    public WorkCenterView getActive(long id) {
        return workCenterRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("작업장을 찾을 수 없습니다: " + id));
    }

    private void validateCommand(WorkCenterCommand command) {
        if (command.wcName() == null || command.wcName().isBlank()) {
            throw new IllegalArgumentException("작업장명은 필수입니다.");
        }
        if (command.operationTime() < 1 || command.operationTime() > MAX_OPERATION_TIME) {
            throw new IllegalArgumentException("일일 가동시간은 1~" + MAX_OPERATION_TIME + "분 사이여야 합니다.");
        }
    }

    private ProcessCodeLookup.ProcessCodeInfo getAllowedProcessCode(long processCodeId) {
        ProcessCodeLookup.ProcessCodeInfo code = processCodeLookup.findActiveProcessCode(processCodeId)
                .orElseThrow(() -> new IllegalArgumentException("공정코드를 찾을 수 없습니다: " + processCodeId));
        if (EXCLUDED_PROCESS_CODES.contains(code.smallCode())) {
            throw new IllegalArgumentException("예약 공정코드는 사용할 수 없습니다: " + code.smallCode());
        }
        return code;
    }

    private void appendEvent(
            String eventType,
            long id,
            String actorUserId,
            WorkCenterCommand command,
            ProcessCodeLookup.ProcessCodeInfo processCode
    ) {
        domainEventStore.append(DomainEvent.create(
                eventType,
                1,
                AggregateTypes.WORK_CENTER,
                String.valueOf(id),
                actorUserId,
                """
                {"workCenterId":%d,"wcName":"%s","mainProcessCodeId":%d,"mainProcessCode":"%s","operationTime":%d}
                """.formatted(
                        id,
                        escape(command.wcName()),
                        command.mainProcessCodeId(),
                        escape(processCode.smallCode()),
                        command.operationTime()
                ).trim()
        ));
    }

    private static String escape(String value) {
        return value.replace("\\", "\\\\").replace("\"", "\\\"");
    }
}
