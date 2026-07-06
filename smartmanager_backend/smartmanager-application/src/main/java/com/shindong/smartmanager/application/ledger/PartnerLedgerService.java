package com.shindong.smartmanager.application.ledger;

import java.math.BigDecimal;
import java.time.LocalDate;

public interface PartnerLedgerService {

    void addPurchaseAmount(long companyId, LocalDate transactionDate, BigDecimal amount, String actorUserId);

    void subtractPurchaseAmount(long companyId, LocalDate transactionDate, BigDecimal amount, String actorUserId);
}
