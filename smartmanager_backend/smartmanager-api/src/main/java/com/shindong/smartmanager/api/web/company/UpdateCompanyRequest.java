package com.shindong.smartmanager.api.web.company;

import com.shindong.smartmanager.domain.company.CompanyRoleType;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotEmpty;
import java.util.List;

public record UpdateCompanyRequest(
        @NotBlank String companyName,
        @NotBlank String presidentName,
        String corporationRegNo,
        @NotBlank String businessAddress,
        String homepageUrl,
        String businessType,
        String businessItem,
        String telephone,
        String fax,
        Integer saleStandardDay,
        Integer billApprovalStandard,
        Integer fixCollectDay1,
        String contactName,
        String contactEmail,
        @NotEmpty List<CompanyRoleType> roles
) {
}
