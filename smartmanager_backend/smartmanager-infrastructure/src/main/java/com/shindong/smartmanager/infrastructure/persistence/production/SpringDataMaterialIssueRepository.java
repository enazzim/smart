package com.shindong.smartmanager.infrastructure.persistence.production;

import com.shindong.smartmanager.domain.production.MaterialIssueStatus;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataMaterialIssueRepository extends JpaRepository<MaterialIssueJpaEntity, Long> {

    long countByIssueNumStartingWithAndRecordingState(String prefix, int recordingState);

    Optional<MaterialIssueJpaEntity> findByIdAndRecordingStateAndStatus(
            long id,
            int recordingState,
            MaterialIssueStatus status
    );
}
