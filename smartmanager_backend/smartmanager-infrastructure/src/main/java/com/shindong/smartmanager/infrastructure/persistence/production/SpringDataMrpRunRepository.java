package com.shindong.smartmanager.infrastructure.persistence.production;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataMrpRunRepository extends JpaRepository<MrpRunJpaEntity, Long> {

    List<MrpRunJpaEntity> findByRunNoStartingWithAndRecordingState(String prefix, int recordingState);

    List<MrpRunJpaEntity> findByRecordingStateOrderByIdDesc(int recordingState);

    java.util.Optional<MrpRunJpaEntity> findByIdAndRecordingState(long id, int recordingState);
}
