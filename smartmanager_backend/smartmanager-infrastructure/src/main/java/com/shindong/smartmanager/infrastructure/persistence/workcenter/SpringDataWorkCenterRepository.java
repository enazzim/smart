package com.shindong.smartmanager.infrastructure.persistence.workcenter;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataWorkCenterRepository extends JpaRepository<WorkCenterJpaEntity, Long> {

    List<WorkCenterJpaEntity> findByRecordingStateOrderByWcNameAsc(int recordingState);
}
