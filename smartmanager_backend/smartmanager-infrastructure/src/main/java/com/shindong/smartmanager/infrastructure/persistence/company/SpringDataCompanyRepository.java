package com.shindong.smartmanager.infrastructure.persistence.company;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataCompanyRepository extends JpaRepository<CompanyJpaEntity, Long> {

    boolean existsByBusinessRegNoAndRecordingState(String businessRegNo, int recordingState);

    List<CompanyJpaEntity> findByRecordingStateOrderByIdDesc(int recordingState);

    Optional<CompanyJpaEntity> findByIdAndRecordingState(Long id, int recordingState);

    Optional<CompanyJpaEntity> findByBusinessRegNoAndRecordingState(String businessRegNo, int recordingState);
}
