package com.shindong.smartmanager.infrastructure.persistence.item;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataItemRepository extends JpaRepository<ItemJpaEntity, Long> {

    boolean existsByItemNoAndRecordingState(String itemNo, int recordingState);

    Optional<ItemJpaEntity> findByIdAndRecordingState(Long id, int recordingState);

    Optional<ItemJpaEntity> findByItemNoAndRecordingState(String itemNo, int recordingState);

    @Query("""
            SELECT i FROM ItemJpaEntity i
            WHERE i.recordingState = 1
              AND (:itemNo = '' OR LOWER(i.itemNo) LIKE LOWER(CONCAT('%', :itemNo, '%')))
              AND (:itemName = '' OR LOWER(i.itemName) LIKE LOWER(CONCAT('%', :itemName, '%')))
            ORDER BY i.id DESC
            """)
    List<ItemJpaEntity> searchActive(@Param("itemNo") String itemNo, @Param("itemName") String itemName);
}
