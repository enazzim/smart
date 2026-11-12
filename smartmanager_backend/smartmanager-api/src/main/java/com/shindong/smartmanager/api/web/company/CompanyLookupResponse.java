package com.shindong.smartmanager.api.web.company;

import com.shindong.smartmanager.application.company.CompanyLookupView;

public record CompanyLookupResponse(
        long id,
        String companyName,
        String businessRegNo
) {
    public static CompanyLookupResponse from(CompanyLookupView view) {
        return new CompanyLookupResponse(view.id(), view.companyName(), view.businessRegNo());
    }
}
