package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.domain.production.WorkReportHistorySourceType;
import java.util.List;
import java.util.Optional;

public interface WorkReportRepository {

    List<WorkOrderView> findReportTargets();

    WorkReportView save(WorkReportSaveCommand command, String actorUserId);

    List<WorkReportView> findAllActive(WorkReportListCriteria criteria);

    Optional<WorkReportView> findActiveRegisteredById(long id);

    long countByReportNumPrefix(String prefix);

    void cancelById(long id, String actorUserId);

    void saveHistory(WorkReportHistorySaveCommand command, String actorUserId);

    void deactivateHistory(WorkReportHistorySourceType sourceType, long sourceId, String actorUserId);

    List<WorkReportConsumptionRecordView> saveConsumptionLines(
            long workReportId,
            List<WorkReportConsumptionSaveCommand> commands,
            String actorUserId
    );

    List<WorkReportConsumptionRecordView> findActiveConsumptionLinesByReportId(long workReportId);

    void deactivateConsumptionLinesByReportId(long workReportId, String actorUserId);

    void updateOutputLotId(long workReportId, Long outputLotId, String actorUserId);

    Optional<Long> findOutputLotId(long workReportId);
}
