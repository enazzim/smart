package com.shindong.smartmanager.api.web.system.imports;

import com.shindong.smartmanager.application.system.imports.CompanyImportRow;
import com.shindong.smartmanager.domain.company.CompanyRoleType;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotEmpty;
import java.util.Arrays;
import java.util.List;

public record CompanyImportBulkRequest(@Valid @NotEmpty List<CompanyImportRowRequest> rows) {
    public List<CompanyImportRow> toRows() {
        return rows.stream().map(CompanyImportRowRequest::toRow).toList();
    }

    public record CompanyImportRowRequest(
            @NotBlank String companyName,
            @NotBlank String presidentName,
            @NotBlank String businessRegNo,
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
            @NotBlank String roles
    ) {
        CompanyImportRow toRow() {
            List<CompanyRoleType> roleTypes = Arrays.stream(roles.split(","))
                    .map(String::trim)
                    .filter(s -> !s.isEmpty())
                    .map(CompanyRoleType::valueOf)
                    .toList();
            return new CompanyImportRow(
                    companyName,
                    presidentName,
                    businessRegNo,
                    corporationRegNo,
                    businessAddress,
                    homepageUrl,
                    businessType,
                    businessItem,
                    telephone,
                    fax,
                    saleStandardDay,
                    billApprovalStandard,
                    fixCollectDay1,
                    contactName,
                    contactEmail,
                    roleTypes
            );
        }
    }
}
