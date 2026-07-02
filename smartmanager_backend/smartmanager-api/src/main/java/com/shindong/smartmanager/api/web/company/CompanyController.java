package com.shindong.smartmanager.api.web.company;

import com.shindong.smartmanager.application.company.CompanyCommand;
import com.shindong.smartmanager.application.company.CompanyUpdateCommand;
import com.shindong.smartmanager.infrastructure.application.CompanyApplicationService;
import jakarta.validation.Valid;
import java.util.List;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/basis/companies")
public class CompanyController {

    private static final String DEFAULT_ACTOR = "local-dev";

    private final CompanyApplicationService companyApplicationService;

    public CompanyController(CompanyApplicationService companyApplicationService) {
        this.companyApplicationService = companyApplicationService;
    }

    @GetMapping
    public List<CompanyResponse> list() {
        return companyApplicationService.listActive().stream()
                .map(CompanyResponse::from)
                .toList();
    }

    @GetMapping("/{id}")
    public CompanyResponse get(@PathVariable long id) {
        return CompanyResponse.from(companyApplicationService.getActive(id));
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    public CompanyResponse create(
            @Valid @RequestBody CreateCompanyRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        CompanyCommand command = new CompanyCommand(
                request.companyName(),
                request.presidentName(),
                request.businessRegNo(),
                request.corporationRegNo(),
                request.businessAddress(),
                request.homepageUrl(),
                request.businessType(),
                request.businessItem(),
                request.telephone(),
                request.fax(),
                request.saleStandardDay(),
                request.billApprovalStandard(),
                request.fixCollectDay1(),
                request.contactName(),
                request.contactEmail(),
                request.roles()
        );
        return CompanyResponse.from(companyApplicationService.register(command, actor));
    }

    @PutMapping("/{id}")
    public CompanyResponse update(
            @PathVariable long id,
            @Valid @RequestBody UpdateCompanyRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        CompanyUpdateCommand command = new CompanyUpdateCommand(
                request.companyName(),
                request.presidentName(),
                request.corporationRegNo(),
                request.businessAddress(),
                request.homepageUrl(),
                request.businessType(),
                request.businessItem(),
                request.telephone(),
                request.fax(),
                request.saleStandardDay(),
                request.billApprovalStandard(),
                request.fixCollectDay1(),
                request.contactName(),
                request.contactEmail(),
                request.roles()
        );
        return CompanyResponse.from(companyApplicationService.update(id, command, actor));
    }

    @DeleteMapping("/{id}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    public void delete(
            @PathVariable long id,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        companyApplicationService.delete(id, actor);
    }
}
