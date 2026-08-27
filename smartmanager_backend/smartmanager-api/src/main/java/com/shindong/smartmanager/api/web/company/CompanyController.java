package com.shindong.smartmanager.api.web.company;

import com.shindong.smartmanager.api.security.BasisAuthorize;
import com.shindong.smartmanager.api.security.SecurityUtils;
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
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/basis/companies")
@BasisAuthorize.CompanyRead
public class CompanyController {

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
    @BasisAuthorize.CompanyWrite
    public CompanyResponse create(
            @Valid @RequestBody CreateCompanyRequest request
    ) {
        String actor = SecurityUtils.requireLoginId();
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
    @BasisAuthorize.CompanyWrite
    public CompanyResponse update(
            @PathVariable long id,
            @Valid @RequestBody UpdateCompanyRequest request
    ) {
        String actor = SecurityUtils.requireLoginId();
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
    @BasisAuthorize.CompanyWrite
    public void delete(@PathVariable long id) {
        companyApplicationService.delete(id, SecurityUtils.requireLoginId());
    }
}
