package com.shindong.smartmanager.application.process;

import com.shindong.smartmanager.domain.process.ProcessVariant;
import java.util.List;
import java.util.Optional;

public interface ProcessRepository {

    long save(ProcessCommand command, ProcessVariant variant, String actorUserId);

    void update(long id, ProcessUpdateCommand command, String actorUserId);

    void softDelete(long id, String actorUserId);

    Optional<ProcessView> findActiveById(long id);

    List<ProcessView> findAllActiveByItemId(long itemId, ProcessVariant variant);

    List<ProcessView> findAllActive(ProcessVariant variant);

    boolean existsActiveDuplicate(long itemId, long processCodeId, short processSequenceNum, Long excludeId);

    /**
     * 원자재 BOM 투입용 소재공정(14000000) process_sequence를 조회·없으면 생성.
     */
    long ensureMaterialProcess(long itemId, String actorUserId);
}
