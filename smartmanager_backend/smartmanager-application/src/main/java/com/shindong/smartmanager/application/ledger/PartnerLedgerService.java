package com.shindong.smartmanager.application.ledger;

import com.shindong.smartmanager.application.closing.FiscalPeriod;
import java.math.BigDecimal;
import java.time.LocalDate;

public interface PartnerLedgerService {

    void addPurchaseAmount(long companyId, LocalDate transactionDate, BigDecimal amount, String actorUserId);

    void addPurchaseAmount(long companyId, FiscalPeriod period, BigDecimal amount, String actorUserId);

    void subtractPurchaseAmount(long companyId, LocalDate transactionDate, BigDecimal amount, String actorUserId);

    void subtractPurchaseAmount(long companyId, FiscalPeriod period, BigDecimal amount, String actorUserId);

    void addSalesAmount(long companyId, LocalDate transactionDate, BigDecimal amount, String actorUserId);

    void subtractSalesAmount(long companyId, LocalDate transactionDate, BigDecimal amount, String actorUserId);

    void addCollectedAmount(long companyId, LocalDate transactionDate, BigDecimal amount, String actorUserId);

    void subtractCollectedAmount(long companyId, LocalDate transactionDate, BigDecimal amount, String actorUserId);

    void addPaidAmount(long companyId, LocalDate transactionDate, BigDecimal amount, String actorUserId);

    void subtractPaidAmount(long companyId, LocalDate transactionDate, BigDecimal amount, String actorUserId);
}
