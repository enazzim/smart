package com.shindong.smartmanager.infrastructure.persistence.drawing;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataDrawingMasterRepository extends JpaRepository<DrawingMasterJpaEntity, String> {

    boolean existsByPartNoAndRecordingState(String partNo, int recordingState);

    Optional<DrawingMasterJpaEntity> findByPartNoAndRecordingState(String partNo, int recordingState);
}
