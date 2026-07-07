package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;
import java.util.List;
import java.util.Map;
import java.util.Optional;

public interface MaterialIssueRepository {

    List<WorkOrderView> findIssueTargets();

    long countByIssueNumPrefix(String prefix);

    MaterialIssueView save(MaterialIssueSaveCommand command, String actorUserId);

    List<MaterialIssueLineRecordView> saveLines(
            long materialIssueId,
            List<MaterialIssueLineSaveCommand> lines,
            String actorUserId
    );

    Optional<MaterialIssueView> findActiveIssuedById(long id);

    List<MaterialIssueView> findAllActive(MaterialIssueListCriteria criteria);

    void cancelById(long id, String actorUserId);

    Map<Long, BigDecimal> sumIssuedQtyByWorkOrderId(long workOrderId);

    Map<Long, BigDecimal> sumIssuedQtyByItemIdForWorkOrder(long workOrderId);

    List<MaterialIssueLineRecordView> findActiveLinesByIssueId(long issueId);
}
