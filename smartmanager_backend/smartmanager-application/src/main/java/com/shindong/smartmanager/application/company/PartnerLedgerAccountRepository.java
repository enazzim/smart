package com.shindong.smartmanager.application.company;

import com.shindong.smartmanager.domain.company.PartnerLedgerType;

public interface PartnerLedgerAccountRepository {

    void ensureAccount(long companyId, int fiscalYear, PartnerLedgerType ledgerType, String actorUserId);
}
