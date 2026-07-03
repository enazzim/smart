package com.shindong.smartmanager.infrastructure.persistence.bom;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataItemCompositionRepository extends JpaRepository<ItemCompositionJpaEntity, Long> {

    Optional<ItemCompositionJpaEntity> findByIdAndRecordingState(Long id, int recordingState);

    List<ItemCompositionJpaEntity> findByParentItemIdAndRecordingStateOrderByChildItemIdAsc(
            Long parentItemId,
            int recordingState
    );

    List<ItemCompositionJpaEntity> findByChildItemIdAndRecordingStateOrderByParentItemIdAsc(
            Long childItemId,
            int recordingState
    );

    @Query("""
            SELECT COUNT(c) > 0 FROM ItemCompositionJpaEntity c
            WHERE c.parentItemId = :parentItemId
              AND c.childItemId = :childItemId
              AND c.recordingState = 1
              AND (:excludeId IS NULL OR c.id <> :excludeId)
            """)
    boolean existsActiveUk(
            @Param("parentItemId") Long parentItemId,
            @Param("childItemId") Long childItemId,
            @Param("excludeId") Long excludeId
    );

    @Query("""
            SELECT c FROM ItemCompositionJpaEntity c
            JOIN ItemJpaEntity parent ON parent.id = c.parentItemId
            JOIN ItemJpaEntity child ON child.id = c.childItemId
            WHERE c.recordingState = 1
              AND (:parentItemNo IS NULL OR :parentItemNo = '' OR parent.itemNo LIKE CONCAT('%', :parentItemNo, '%'))
              AND (:childItemNo IS NULL OR :childItemNo = '' OR child.itemNo LIKE CONCAT('%', :childItemNo, '%'))
            ORDER BY parent.itemNo, child.itemNo
            """)
    List<ItemCompositionJpaEntity> findAllActiveFiltered(
            @Param("parentItemNo") String parentItemNo,
            @Param("childItemNo") String childItemNo
    );
}
