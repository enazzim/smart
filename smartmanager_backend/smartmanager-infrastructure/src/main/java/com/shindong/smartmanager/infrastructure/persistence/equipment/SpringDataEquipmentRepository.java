package com.shindong.smartmanager.infrastructure.persistence.equipment;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataEquipmentRepository extends JpaRepository<EquipmentJpaEntity, Long> {

    Optional<EquipmentJpaEntity> findByIdAndRecordingState(Long id, int recordingState);

    @Query("""
            SELECT e FROM EquipmentJpaEntity e
            WHERE e.recordingState = 1
              AND (:query IS NULL OR :query = ''
                   OR LOWER(e.equipmentNum) LIKE LOWER(CONCAT('%', :query, '%'))
                   OR LOWER(e.equipmentName) LIKE LOWER(CONCAT('%', :query, '%')))
            ORDER BY e.equipmentNum ASC
            """)
    List<EquipmentJpaEntity> searchActive(@Param("query") String query);

    @Query("""
            SELECT CASE WHEN COUNT(e) > 0 THEN true ELSE false END
            FROM EquipmentJpaEntity e
            WHERE e.equipmentNum = :equipmentNum
              AND e.recordingState = 1
              AND (:excludeId IS NULL OR e.id <> :excludeId)
            """)
    boolean existsActiveByEquipmentNum(@Param("equipmentNum") String equipmentNum, @Param("excludeId") Long excludeId);

    boolean existsByWorkCenterIdAndRecordingState(Long workCenterId, int recordingState);
}
