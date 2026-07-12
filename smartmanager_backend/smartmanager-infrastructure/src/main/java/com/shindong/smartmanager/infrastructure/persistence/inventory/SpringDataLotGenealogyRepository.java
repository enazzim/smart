package com.shindong.smartmanager.infrastructure.persistence.inventory;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataLotGenealogyRepository extends JpaRepository<LotGenealogyJpaEntity, Long> {

    List<LotGenealogyJpaEntity> findByParentLotIdAndRecordingStateOrderByIdAsc(long parentLotId, int recordingState);

    List<LotGenealogyJpaEntity> findByChildLotIdAndRecordingStateOrderByIdAsc(long childLotId, int recordingState);

    @Modifying(clearAutomatically = true)
    @Query("""
            UPDATE LotGenealogyJpaEntity g
            SET g.recordingState = 0
            WHERE g.sourceDocType = :sourceDocType
              AND g.sourceDocId = :sourceDocId
              AND g.recordingState = 1
            """)
    int softDeactivateBySourceDoc(
            @Param("sourceDocType") String sourceDocType,
            @Param("sourceDocId") long sourceDocId
    );
}
