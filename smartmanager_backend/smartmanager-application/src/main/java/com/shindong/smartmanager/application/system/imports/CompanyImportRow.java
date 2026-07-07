package com.shindong.smartmanager.application.system.imports;

import com.shindong.smartmanager.domain.company.CompanyRoleType;
import java.util.List;

public record CompanyImportRow(
        String companyName,
        String presidentName,
        String businessRegNo,
        String corporationRegNo,
        String businessAddress,
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
        List<CompanyRoleType> roles
) {
}
