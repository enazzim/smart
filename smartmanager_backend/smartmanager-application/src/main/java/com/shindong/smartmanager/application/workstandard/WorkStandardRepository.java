package com.shindong.smartmanager.application.workstandard;

import java.util.List;
import java.util.Optional;

public interface WorkStandardRepository {

    long save(WorkStandardCommand command, String actorUserId);

    void update(long id, WorkStandardUpdateCommand command, String actorUserId);

    void softDelete(long id, String actorUserId);

    void softDeleteByProcessSequenceId(long processSequenceId, String actorUserId);

    List<WorkStandardView> findAllActive(String itemNumQuery);

    List<WorkStandardView> findActiveByItemId(long itemId);

    Optional<WorkStandardView> findActiveById(long id);

    boolean existsActiveUk(long itemId, long processSequenceId, int priorityOrder, Long excludeId);
}
