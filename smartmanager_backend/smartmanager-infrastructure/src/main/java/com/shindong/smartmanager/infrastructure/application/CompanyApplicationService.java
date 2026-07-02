package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.company.CompanyCommand;
import com.shindong.smartmanager.application.company.CompanyService;
import com.shindong.smartmanager.application.company.CompanyUpdateCommand;
import com.shindong.smartmanager.application.company.CompanyView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class CompanyApplicationService {

    private final CompanyService companyService;

    public CompanyApplicationService(CompanyService companyService) {
        this.companyService = companyService;
    }

    @Transactional
    public CompanyView register(CompanyCommand command, String actorUserId) {
        return companyService.register(command, actorUserId);
    }

    @Transactional(readOnly = true)
    public List<CompanyView> listActive() {
        return companyService.listActive();
    }

    @Transactional(readOnly = true)
    public CompanyView getActive(long id) {
        return companyService.getActive(id);
    }

    @Transactional
    public CompanyView update(long id, CompanyUpdateCommand command, String actorUserId) {
        return companyService.update(id, command, actorUserId);
    }

    @Transactional
    public void delete(long id, String actorUserId) {
        companyService.delete(id, actorUserId);
    }
}
