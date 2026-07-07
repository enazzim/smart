package com.shindong.smartmanager.infrastructure.persistence.ledger;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.FiscalPeriod;
import com.shindong.smartmanager.application.company.PartnerLedgerAccountRepository;
import com.shindong.smartmanager.application.ledger.PartnerLedgerService;
import com.shindong.smartmanager.domain.company.PartnerLedgerType;
import com.shindong.smartmanager.infrastructure.persistence.company.PartnerLedgerMonthlyJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.company.SpringDataPartnerLedgerAccountRepository;
import com.shindong.smartmanager.infrastructure.persistence.company.SpringDataPartnerLedgerMonthlyRepository;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class JpaPartnerLedgerService implements PartnerLedgerService {

    private final PartnerLedgerAccountRepository ledgerAccountRepository;
    private final SpringDataPartnerLedgerAccountRepository accountRepository;
    private final SpringDataPartnerLedgerMonthlyRepository monthlyRepository;
    private final FiscalCalendarService fiscalCalendarService;

    public JpaPartnerLedgerService(
            PartnerLedgerAccountRepository ledgerAccountRepository,
            SpringDataPartnerLedgerAccountRepository accountRepository,
            SpringDataPartnerLedgerMonthlyRepository monthlyRepository,
            FiscalCalendarService fiscalCalendarService
    ) {
        this.ledgerAccountRepository = ledgerAccountRepository;
        this.accountRepository = accountRepository;
        this.monthlyRepository = monthlyRepository;
        this.fiscalCalendarService = fiscalCalendarService;
    }

    @Override
    @Transactional
    public void addPurchaseAmount(long companyId, LocalDate transactionDate, BigDecimal amount, String actorUserId) {
        if (amount == null || amount.compareTo(BigDecimal.ZERO) == 0) {
            return;
        }
        FiscalPeriod period = fiscalCalendarService.resolvePeriod(transactionDate);
        ledgerAccountRepository.ensureAccount(companyId, period.fiscalYear(), PartnerLedgerType.PURCHASE, actorUserId);

        long accountId = accountRepository
                .findByCompanyIdAndFiscalYearAndLedgerType(companyId, (short) period.fiscalYear(), PartnerLedgerType.PURCHASE)
                .orElseThrow(() -> new IllegalStateException("매입 원장 계정을 찾을 수 없습니다."))
                .getId();

        byte month = (byte) period.fiscalMonth();
        PartnerLedgerMonthlyJpaEntity monthly = monthlyRepository
                .findByLedgerAccountIdAndMonthNumAndRecordingState(accountId, month, 1)
                .orElseGet(() -> PartnerLedgerMonthlyJpaEntity.createNew(accountId, month));

        monthly.setPurchaseAmount(monthly.getPurchaseAmount().add(amount));
        monthly.setUpdatedAt(Instant.now());
        monthlyRepository.save(monthly);
    }

    @Override
    @Transactional
    public void subtractPurchaseAmount(long companyId, LocalDate transactionDate, BigDecimal amount, String actorUserId) {
        if (amount == null || amount.compareTo(BigDecimal.ZERO) == 0) {
            return;
        }
        addPurchaseAmount(companyId, transactionDate, amount.negate(), actorUserId);
    }

    @Override
    @Transactional
    public void addSalesAmount(long companyId, LocalDate transactionDate, BigDecimal amount, String actorUserId) {
        if (amount == null || amount.compareTo(BigDecimal.ZERO) == 0) {
            return;
        }
        FiscalPeriod period = fiscalCalendarService.resolvePeriod(transactionDate);
        ledgerAccountRepository.ensureAccount(companyId, period.fiscalYear(), PartnerLedgerType.SALES, actorUserId);

        long accountId = accountRepository
                .findByCompanyIdAndFiscalYearAndLedgerType(companyId, (short) period.fiscalYear(), PartnerLedgerType.SALES)
                .orElseThrow(() -> new IllegalStateException("매출 원장 계정을 찾을 수 없습니다."))
                .getId();

        byte month = (byte) period.fiscalMonth();
        PartnerLedgerMonthlyJpaEntity monthly = monthlyRepository
                .findByLedgerAccountIdAndMonthNumAndRecordingState(accountId, month, 1)
                .orElseGet(() -> PartnerLedgerMonthlyJpaEntity.createNew(accountId, month));

        monthly.setSaleAmount(monthly.getSaleAmount().add(amount));
        monthly.setUpdatedAt(Instant.now());
        monthlyRepository.save(monthly);
    }

    @Override
    @Transactional
    public void subtractSalesAmount(long companyId, LocalDate transactionDate, BigDecimal amount, String actorUserId) {
        if (amount == null || amount.compareTo(BigDecimal.ZERO) == 0) {
            return;
        }
        addSalesAmount(companyId, transactionDate, amount.negate(), actorUserId);
    }

    @Override
    @Transactional
    public void addCollectedAmount(long companyId, LocalDate transactionDate, BigDecimal amount, String actorUserId) {
        if (amount == null || amount.compareTo(BigDecimal.ZERO) == 0) {
            return;
        }
        FiscalPeriod period = fiscalCalendarService.resolvePeriod(transactionDate);
        ledgerAccountRepository.ensureAccount(companyId, period.fiscalYear(), PartnerLedgerType.SALES, actorUserId);

        long accountId = accountRepository
                .findByCompanyIdAndFiscalYearAndLedgerType(companyId, (short) period.fiscalYear(), PartnerLedgerType.SALES)
                .orElseThrow(() -> new IllegalStateException("매출 원장 계정을 찾을 수 없습니다."))
                .getId();

        byte month = (byte) period.fiscalMonth();
        PartnerLedgerMonthlyJpaEntity monthly = monthlyRepository
                .findByLedgerAccountIdAndMonthNumAndRecordingState(accountId, month, 1)
                .orElseGet(() -> PartnerLedgerMonthlyJpaEntity.createNew(accountId, month));

        monthly.setCollectedAmount(monthly.getCollectedAmount().add(amount));
        monthly.setUpdatedAt(Instant.now());
        monthlyRepository.save(monthly);
    }

    @Override
    @Transactional
    public void subtractCollectedAmount(long companyId, LocalDate transactionDate, BigDecimal amount, String actorUserId) {
        if (amount == null || amount.compareTo(BigDecimal.ZERO) == 0) {
            return;
        }
        addCollectedAmount(companyId, transactionDate, amount.negate(), actorUserId);
    }
}
