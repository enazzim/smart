package com.shindong.smartmanager.application.company;

import com.shindong.smartmanager.domain.company.CompanyRoleType;
import java.util.List;
import java.util.Optional;

public interface CompanyRepository {

    boolean existsActiveByBusinessRegNo(String businessRegNo);

    long save(CompanyCommand command, String actorUserId);

    void replaceRoles(long companyId, List<CompanyRoleType> roles);

    List<CompanyRoleType> findRoles(long companyId);

    List<CompanyView> findAllActive();

    Optional<CompanyView> findActiveById(long id);

    void update(long id, CompanyUpdateCommand command, String actorUserId);

    void softDelete(long id, String actorUserId);
}
