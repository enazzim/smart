package com.shindong.smartmanager.infrastructure.persistence.company;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataCompanyRoleRepository extends JpaRepository<CompanyRoleJpaEntity, Long> {

    List<CompanyRoleJpaEntity> findByCompanyId(Long companyId);

    void deleteByCompanyId(Long companyId);
}
