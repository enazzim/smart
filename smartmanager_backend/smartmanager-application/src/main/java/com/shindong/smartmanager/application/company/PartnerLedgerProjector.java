package com.shindong.smartmanager.application.company;

import com.shindong.smartmanager.domain.company.CompanyRoleType;
import com.shindong.smartmanager.domain.company.PartnerLedgerType;
import java.util.EnumSet;
import java.util.List;

/**
 * Ref: docs/step0/d4-company.md §2.3, domain-event-projector-matrix.md §2
 */
public class PartnerLedgerProjector {

    private final PartnerLedgerAccountRepository ledgerAccountRepository;

    public PartnerLedgerProjector(PartnerLedgerAccountRepository ledgerAccountRepository) {
        this.ledgerAccountRepository = ledgerAccountRepository;
    }

    public void ensureAccounts(long companyId, List<CompanyRoleType> roles, int fiscalYear, String actorUserId) {
        if (roles == null || roles.isEmpty()) {
            return;
        }
        EnumSet<CompanyRoleType> roleSet = EnumSet.copyOf(roles);
        if (roleSet.size() == 1 && roleSet.contains(CompanyRoleType.COST)) {
            return;
        }
        if (roleSet.contains(CompanyRoleType.SALES)) {
            ledgerAccountRepository.ensureAccount(companyId, fiscalYear, PartnerLedgerType.SALES, actorUserId);
        }
        if (roleSet.contains(CompanyRoleType.PURCHASE) || roleSet.contains(CompanyRoleType.OUTSOURCE)) {
            ledgerAccountRepository.ensureAccount(companyId, fiscalYear, PartnerLedgerType.PURCHASE, actorUserId);
        }
    }
}
