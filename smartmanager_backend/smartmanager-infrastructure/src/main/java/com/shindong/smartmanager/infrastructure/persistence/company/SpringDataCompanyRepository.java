package com.shindong.smartmanager.infrastructure.persistence.company;

import com.shindong.smartmanager.domain.company.CompanyRoleType;
import java.util.Collection;
import java.util.List;
import java.util.Optional;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataCompanyRepository extends JpaRepository<CompanyJpaEntity, Long> {

    boolean existsByBusinessRegNoAndRecordingState(String businessRegNo, int recordingState);

    List<CompanyJpaEntity> findByRecordingStateOrderByIdDesc(int recordingState);

    Optional<CompanyJpaEntity> findByIdAndRecordingState(Long id, int recordingState);

    Optional<CompanyJpaEntity> findByBusinessRegNoAndRecordingState(String businessRegNo, int recordingState);

    @Query("""
            SELECT c FROM CompanyJpaEntity c
            WHERE c.recordingState = 1
              AND (
                   LOWER(c.companyName) LIKE LOWER(CONCAT('%', :q, '%'))
                OR LOWER(c.businessRegNo) LIKE LOWER(CONCAT('%', :q, '%'))
              )
            ORDER BY c.companyName ASC
            """)
    List<CompanyJpaEntity> lookupActive(@Param("q") String q, Pageable pageable);

    @Query("""
            SELECT DISTINCT c FROM CompanyJpaEntity c
            WHERE c.recordingState = 1
              AND EXISTS (
                    SELECT 1 FROM CompanyRoleJpaEntity r
                    WHERE r.companyId = c.id
                      AND r.roleType IN :roles
              )
              AND (
                   LOWER(c.companyName) LIKE LOWER(CONCAT('%', :q, '%'))
                OR LOWER(c.businessRegNo) LIKE LOWER(CONCAT('%', :q, '%'))
              )
            ORDER BY c.companyName ASC
            """)
    List<CompanyJpaEntity> lookupActiveByRoles(
            @Param("q") String q,
            @Param("roles") Collection<CompanyRoleType> roles,
            Pageable pageable
    );
}
