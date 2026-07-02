package com.shindong.smartmanager.infrastructure.persistence.company;

import com.shindong.smartmanager.domain.company.CompanyRoleType;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.EnumType;
import jakarta.persistence.Enumerated;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;

@Entity
@Table(name = "company_role")
public class CompanyRoleJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "company_id", nullable = false)
    private Long companyId;

    @Enumerated(EnumType.STRING)
    @Column(name = "role_type", nullable = false, length = 20)
    private CompanyRoleType roleType;

    protected CompanyRoleJpaEntity() {
    }

    public CompanyRoleJpaEntity(Long companyId, CompanyRoleType roleType) {
        this.companyId = companyId;
        this.roleType = roleType;
    }

    public Long getCompanyId() {
        return companyId;
    }

    public CompanyRoleType getRoleType() {
        return roleType;
    }
}
