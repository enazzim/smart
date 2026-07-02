package com.shindong.smartmanager.api.web.company;

import com.shindong.smartmanager.application.company.CompanyView;
import com.shindong.smartmanager.domain.company.CompanyRoleType;
import java.time.Instant;
import java.util.List;

public record CompanyResponse(
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
    public static CompanyResponse from(CompanyView view) {
        return new CompanyResponse(
                view.id(),
                view.companyName(),
                view.presidentName(),
                view.businessRegNo(),
                view.corporationRegNo(),
                view.businessAddress(),
                view.homepageUrl(),
                view.businessType(),
                view.businessItem(),
                view.telephone(),
                view.fax(),
                view.saleStandardDay(),
                view.billApprovalStandard(),
                view.fixCollectDay1(),
                view.contactName(),
                view.contactEmail(),
                view.roles(),
                view.createdAt()
        );
    }
}
