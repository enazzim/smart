package com.shindong.smartmanager.infrastructure.persistence.system;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataSystemSettingRepository extends JpaRepository<SystemSettingJpaEntity, Long> {

    List<SystemSettingJpaEntity> findAllByRecordingStateOrderBySettingKeyAsc(int recordingState);

    Optional<SystemSettingJpaEntity> findBySettingKeyAndRecordingState(String settingKey, int recordingState);
}
