package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.FiscalPeriod;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.ledger.PartnerLedgerService;
import com.shindong.smartmanager.application.outsource.OutsourceHistoryRecord;
import com.shindong.smartmanager.domain.purchase.PayableApprovalLedgerKind;
import com.shindong.smartmanager.domain.purchase.PayableApprovalStatus;
import java.math.BigDecimal;
import java.util.List;

public class PayableApprovalService {

    private final PayableApprovalRepository payableApprovalRepository;
    private final PartnerPaymentRepository partnerPaymentRepository;
    private final PartnerLedgerService partnerLedgerService;
    private final MonthClosingService monthClosingService;
    private final FiscalCalendarService fiscalCalendarService;

    public PayableApprovalService(
            PayableApprovalRepository payableApprovalRepository,
            PartnerPaymentRepository partnerPaymentRepository,
            PartnerLedgerService partnerLedgerService,
            MonthClosingService monthClosingService,
            FiscalCalendarService fiscalCalendarService
    ) {
        this.payableApprovalRepository = payableApprovalRepository;
        this.partnerPaymentRepository = partnerPaymentRepository;
        this.partnerLedgerService = partnerLedgerService;
        this.monthClosingService = monthClosingService;
        this.fiscalCalendarService = fiscalCalendarService;
    }

    public List<PayableApprovalView> listPending(PayableApprovalCriteria criteria) {
        return payableApprovalRepository.findPending(criteria);
    }

    public List<PayableApprovalView> listApproved(PayableApprovalCriteria criteria) {
        return payableApprovalRepository.findApproved(criteria);
    }

    public void approve(List<PayableApprovalItemCommand> items, long userId, String actorUserId) {
        if (items == null || items.isEmpty()) {
            throw new IllegalArgumentException("승인할 항목을 1건 이상 선택해 주세요.");
        }
        for (PayableApprovalItemCommand item : items) {
            approveOne(item, userId, actorUserId);
        }
    }

    public void cancelApproval(List<PayableApprovalItemCommand> items, long userId, String actorUserId) {
        if (items == null || items.isEmpty()) {
            throw new IllegalArgumentException("승인취소할 항목을 1건 이상 선택해 주세요.");
        }
        for (PayableApprovalItemCommand item : items) {
            cancelOne(item, userId, actorUserId);
        }
    }

    public void updateFiscalPeriod(UpdatePayableApprovalFiscalPeriodCommand command, String actorUserId) {
        if (command.ledgerKind() == PayableApprovalLedgerKind.PURCHASE) {
            updatePurchaseFiscalPeriod(command, actorUserId);
            return;
        }
        if (command.ledgerKind() == PayableApprovalLedgerKind.OUTSOURCE) {
            updateOutsourceFiscalPeriod(command, actorUserId);
            return;
        }
        throw new IllegalArgumentException("지원하지 않는 원장 구분입니다.");
    }

    private void approveOne(PayableApprovalItemCommand item, long userId, String actorUserId) {
        if (item.ledgerKind() == PayableApprovalLedgerKind.PURCHASE) {
            PurchaseHistoryRecord history = payableApprovalRepository.findActivePurchaseHistory(item.historyId());
            if (history.approvalStatus() == PayableApprovalStatus.APPROVED) {
                return;
            }
            monthClosingService.assertTransactionOpen(history.historyDate());
            FiscalPeriod period = toFiscalPeriod(history);
            monthClosingService.assertPeriodOpen(period.fiscalYear(), period.fiscalMonth());
            payableApprovalRepository.approvePurchaseHistory(history.id(), userId);
            partnerLedgerService.addPurchaseAmount(
                    history.companyId(), period, history.amount(), actorUserId);
            return;
        }
        if (item.ledgerKind() == PayableApprovalLedgerKind.OUTSOURCE) {
            OutsourceHistoryRecord history = payableApprovalRepository.findActiveOutsourceHistory(item.historyId());
            if (history.approvalStatus() == PayableApprovalStatus.APPROVED) {
                return;
            }
            monthClosingService.assertTransactionOpen(history.historyDate());
            FiscalPeriod period = toFiscalPeriod(history);
            monthClosingService.assertPeriodOpen(period.fiscalYear(), period.fiscalMonth());
            payableApprovalRepository.approveOutsourceHistory(history.id(), userId);
            partnerLedgerService.addPurchaseAmount(
                    history.companyId(), period, history.amount(), actorUserId);
            return;
        }
        throw new IllegalArgumentException("지원하지 않는 원장 구분입니다.");
    }

    private void cancelOne(PayableApprovalItemCommand item, long userId, String actorUserId) {
        if (item.ledgerKind() == PayableApprovalLedgerKind.PURCHASE) {
            PurchaseHistoryRecord history = payableApprovalRepository.findActivePurchaseHistory(item.historyId());
            if (history.approvalStatus() == PayableApprovalStatus.PENDING) {
                return;
            }
            assertCancelable(history.companyId(), history.amount());
            monthClosingService.assertTransactionOpen(history.historyDate());
            FiscalPeriod period = toFiscalPeriod(history);
            monthClosingService.assertPeriodOpen(period.fiscalYear(), period.fiscalMonth());
            payableApprovalRepository.cancelApprovalPurchaseHistory(history.id(), userId);
            partnerLedgerService.subtractPurchaseAmount(
                    history.companyId(), period, history.amount(), actorUserId);
            return;
        }
        if (item.ledgerKind() == PayableApprovalLedgerKind.OUTSOURCE) {
            OutsourceHistoryRecord history = payableApprovalRepository.findActiveOutsourceHistory(item.historyId());
            if (history.approvalStatus() == PayableApprovalStatus.PENDING) {
                return;
            }
            assertCancelable(history.companyId(), history.amount());
            monthClosingService.assertTransactionOpen(history.historyDate());
            FiscalPeriod period = toFiscalPeriod(history);
            monthClosingService.assertPeriodOpen(period.fiscalYear(), period.fiscalMonth());
            payableApprovalRepository.cancelApprovalOutsourceHistory(history.id(), userId);
            partnerLedgerService.subtractPurchaseAmount(
                    history.companyId(), period, history.amount(), actorUserId);
            return;
        }
        throw new IllegalArgumentException("지원하지 않는 원장 구분입니다.");
    }

    private void updatePurchaseFiscalPeriod(UpdatePayableApprovalFiscalPeriodCommand command, String actorUserId) {
        PurchaseHistoryRecord history = payableApprovalRepository.findActivePurchaseHistory(command.historyId());
        applyFiscalPeriodChange(
                history.approvalStatus(),
                history.companyId(),
                history.amount(),
                history.historyDate(),
                history.fiscalYear(),
                history.fiscalMonth(),
                command.fiscalYear(),
                command.fiscalMonth(),
                actorUserId,
                (year, month) -> payableApprovalRepository.updatePurchaseHistoryFiscalPeriod(
                        history.id(), year, month)
        );
    }

    private void updateOutsourceFiscalPeriod(UpdatePayableApprovalFiscalPeriodCommand command, String actorUserId) {
        OutsourceHistoryRecord history = payableApprovalRepository.findActiveOutsourceHistory(command.historyId());
        applyFiscalPeriodChange(
                history.approvalStatus(),
                history.companyId(),
                history.amount(),
                history.historyDate(),
                history.fiscalYear(),
                history.fiscalMonth(),
                command.fiscalYear(),
                command.fiscalMonth(),
                actorUserId,
                (year, month) -> payableApprovalRepository.updateOutsourceHistoryFiscalPeriod(
                        history.id(), year, month)
        );
    }

    @FunctionalInterface
    private interface FiscalPeriodUpdater {
        void apply(int fiscalYear, int fiscalMonth);
    }

    private void applyFiscalPeriodChange(
            PayableApprovalStatus approvalStatus,
            long companyId,
            BigDecimal amount,
            java.time.LocalDate historyDate,
            int currentYear,
            int currentMonth,
            Integer requestedYear,
            Integer requestedMonth,
            String actorUserId,
            FiscalPeriodUpdater updater
    ) {
        FiscalPeriod oldPeriod = new FiscalPeriod(currentYear, currentMonth);
        FiscalPeriod newPeriod = fiscalCalendarService.resolvePeriod(historyDate, requestedYear, requestedMonth);
        if (oldPeriod.fiscalYear() == newPeriod.fiscalYear()
                && oldPeriod.fiscalMonth() == newPeriod.fiscalMonth()) {
            return;
        }

        monthClosingService.assertPeriodOpen(newPeriod.fiscalYear(), newPeriod.fiscalMonth());
        if (approvalStatus == PayableApprovalStatus.APPROVED) {
            monthClosingService.assertPeriodOpen(oldPeriod.fiscalYear(), oldPeriod.fiscalMonth());
            partnerLedgerService.subtractPurchaseAmount(companyId, oldPeriod, amount, actorUserId);
        }

        updater.apply(newPeriod.fiscalYear(), newPeriod.fiscalMonth());

        if (approvalStatus == PayableApprovalStatus.APPROVED) {
            partnerLedgerService.addPurchaseAmount(companyId, newPeriod, amount, actorUserId);
        }
    }

    private static FiscalPeriod toFiscalPeriod(PurchaseHistoryRecord history) {
        return new FiscalPeriod(history.fiscalYear(), history.fiscalMonth());
    }

    private static FiscalPeriod toFiscalPeriod(OutsourceHistoryRecord history) {
        return new FiscalPeriod(history.fiscalYear(), history.fiscalMonth());
    }

    private void assertCancelable(long partnerId, BigDecimal amount) {
        BigDecimal payable = partnerPaymentRepository.sumIssuedPayableAmountByPartnerId(partnerId);
        BigDecimal paid = partnerPaymentRepository.sumIssuedPaymentAmountByPartnerId(partnerId);
        BigDecimal unpaid = payable.subtract(paid).max(BigDecimal.ZERO);
        if (amount.compareTo(unpaid) > 0) {
            throw new IllegalArgumentException(
                    "승인취소 금액이 미지급 잔액(" + unpaid.stripTrailingZeros().toPlainString() + ")을 초과합니다."
            );
        }
    }
}
