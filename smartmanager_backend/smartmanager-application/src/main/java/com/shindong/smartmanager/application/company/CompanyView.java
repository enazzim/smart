package com.shindong.smartmanager.application.company;

import com.shindong.smartmanager.domain.company.CompanyRoleType;
import java.time.Instant;
import java.util.List;

public record CompanyView(
        Long id,
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
        List<CompanyRoleType> roles,
        Instant createdAt
) {
}
