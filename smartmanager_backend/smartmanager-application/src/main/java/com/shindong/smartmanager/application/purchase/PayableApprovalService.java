package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.FiscalPeriod;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.ledger.PartnerLedgerService;
import com.shindong.smartmanager.application.outsource.OutsourceHistoryRecord;
import com.shindong.smartmanager.domain.purchase.EtcClaimRecognition;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;
import com.shindong.smartmanager.domain.purchase.PartnerPrepaidOffsetLedgerKind;
import com.shindong.smartmanager.domain.purchase.PayableApprovalLedgerKind;
import com.shindong.smartmanager.domain.purchase.PayableApprovalStatus;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class PayableApprovalService {

    private final PayableApprovalRepository payableApprovalRepository;
    private final PartnerPaymentRepository partnerPaymentRepository;
    private final PartnerLedgerService partnerLedgerService;
    private final MonthClosingService monthClosingService;
    private final FiscalCalendarService fiscalCalendarService;
    private final CompanyRepository companyRepository;
    private final ItemRepository itemRepository;

    public PayableApprovalService(
            PayableApprovalRepository payableApprovalRepository,
            PartnerPaymentRepository partnerPaymentRepository,
            PartnerLedgerService partnerLedgerService,
            MonthClosingService monthClosingService,
            FiscalCalendarService fiscalCalendarService,
            CompanyRepository companyRepository,
            ItemRepository itemRepository
    ) {
        this.payableApprovalRepository = payableApprovalRepository;
        this.partnerPaymentRepository = partnerPaymentRepository;
        this.partnerLedgerService = partnerLedgerService;
        this.monthClosingService = monthClosingService;
        this.fiscalCalendarService = fiscalCalendarService;
        this.companyRepository = companyRepository;
        this.itemRepository = itemRepository;
    }

    public List<PayableApprovalView> listPending(PayableApprovalCriteria criteria) {
        return payableApprovalRepository.findPending(criteria);
    }

    public List<PayableApprovalView> listApproved(PayableApprovalCriteria criteria) {
        return payableApprovalRepository.findApproved(criteria);
    }

    public ApproveOffsetResultView preview(List<PayableApprovalItemCommand> items) {
        if (items == null || items.isEmpty()) {
            throw new IllegalArgumentException("승인할 항목을 1건 이상 선택해 주세요.");
        }
        Map<String, BigDecimal> remainingByBucket = new HashMap<>();
        List<ApproveOffsetResultView.ApproveOffsetItemView> offsets = new ArrayList<>();
        for (PayableApprovalItemCommand item : items) {
            offsets.add(previewOne(item, remainingByBucket));
        }
        return toResult(offsets);
    }

    public ApproveOffsetResultView approve(List<PayableApprovalItemCommand> items, long userId, String actorUserId) {
        if (items == null || items.isEmpty()) {
            throw new IllegalArgumentException("승인할 항목을 1건 이상 선택해 주세요.");
        }
        List<ApproveOffsetResultView.ApproveOffsetItemView> offsets = new ArrayList<>();
        for (PayableApprovalItemCommand item : items) {
            offsets.add(approveOne(item, userId, actorUserId));
        }
        return toResult(offsets);
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
        if (command.ledgerKind() == PayableApprovalLedgerKind.ETC_CLAIM) {
            updateEtcClaimFiscalPeriod(command, actorUserId);
            return;
        }
        if (command.ledgerKind() == PayableApprovalLedgerKind.DEFECT_CLAIM) {
            updateDefectClaimFiscalPeriod(command, actorUserId);
            return;
        }
        throw new IllegalArgumentException("지원하지 않는 원장 구분입니다.");
    }

    private ApproveOffsetResultView.ApproveOffsetItemView previewOne(
            PayableApprovalItemCommand item,
            Map<String, BigDecimal> remainingByBucket
    ) {
        if (item.ledgerKind() == PayableApprovalLedgerKind.PURCHASE) {
            PurchaseHistoryRecord history = payableApprovalRepository.findActivePurchaseHistory(item.historyId());
            return simulateOffset(
                    PayableApprovalLedgerKind.PURCHASE,
                    history.id(),
                    history.companyId(),
                    history.itemId(),
                    PartnerPaymentCostCategory.PURCHASE,
                    history.amount(),
                    remainingByBucket
            );
        }
        if (item.ledgerKind() == PayableApprovalLedgerKind.OUTSOURCE) {
            OutsourceHistoryRecord history = payableApprovalRepository.findActiveOutsourceHistory(item.historyId());
            return simulateOffset(
                    PayableApprovalLedgerKind.OUTSOURCE,
                    history.id(),
                    history.companyId(),
                    history.itemId(),
                    PartnerPaymentCostCategory.OUTSOURCE,
                    history.amount(),
                    remainingByBucket
            );
        }
        if (item.ledgerKind() == PayableApprovalLedgerKind.ETC_CLAIM) {
            EtcClaimHistoryRecord claim = payableApprovalRepository.findActiveEtcClaim(item.historyId());
            return nonOffsetItem(
                    PayableApprovalLedgerKind.ETC_CLAIM,
                    claim.id(),
                    claim.companyId(),
                    null,
                    claim.amount()
            );
        }
        if (item.ledgerKind() == PayableApprovalLedgerKind.DEFECT_CLAIM) {
            DefectClaimHistoryRecord claim = payableApprovalRepository.findActiveDefectClaim(item.historyId());
            return nonOffsetItem(
                    PayableApprovalLedgerKind.DEFECT_CLAIM,
                    claim.id(),
                    claim.companyId(),
                    null,
                    claim.amount()
            );
        }
        throw new IllegalArgumentException("지원하지 않는 원장 구분입니다.");
    }

    private ApproveOffsetResultView.ApproveOffsetItemView approveOne(
            PayableApprovalItemCommand item,
            long userId,
            String actorUserId
    ) {
        if (item.ledgerKind() == PayableApprovalLedgerKind.PURCHASE) {
            PurchaseHistoryRecord history = payableApprovalRepository.findActivePurchaseHistory(item.historyId());
            if (history.approvalStatus() == PayableApprovalStatus.APPROVED) {
                return alreadyApprovedItem(
                        PayableApprovalLedgerKind.PURCHASE,
                        history.id(),
                        history.companyId(),
                        history.itemId(),
                        PartnerPaymentCostCategory.PURCHASE,
                        history.amount()
                );
            }
            monthClosingService.assertTransactionOpen(history.historyDate());
            FiscalPeriod period = toFiscalPeriod(history);
            monthClosingService.assertPeriodOpen(period.fiscalYear(), period.fiscalMonth());
            payableApprovalRepository.approvePurchaseHistory(history.id(), userId);
            partnerLedgerService.addPurchaseAmount(
                    history.companyId(), period, history.amount(), actorUserId);
            return applyFifoOffset(
                    PayableApprovalLedgerKind.PURCHASE,
                    PartnerPrepaidOffsetLedgerKind.PURCHASE_HISTORY,
                    history.id(),
                    history.companyId(),
                    history.itemId(),
                    PartnerPaymentCostCategory.PURCHASE,
                    history.amount(),
                    actorUserId
            );
        }
        if (item.ledgerKind() == PayableApprovalLedgerKind.OUTSOURCE) {
            OutsourceHistoryRecord history = payableApprovalRepository.findActiveOutsourceHistory(item.historyId());
            if (history.approvalStatus() == PayableApprovalStatus.APPROVED) {
                return alreadyApprovedItem(
                        PayableApprovalLedgerKind.OUTSOURCE,
                        history.id(),
                        history.companyId(),
                        history.itemId(),
                        PartnerPaymentCostCategory.OUTSOURCE,
                        history.amount()
                );
            }
            monthClosingService.assertTransactionOpen(history.historyDate());
            FiscalPeriod period = toFiscalPeriod(history);
            monthClosingService.assertPeriodOpen(period.fiscalYear(), period.fiscalMonth());
            payableApprovalRepository.approveOutsourceHistory(history.id(), userId);
            partnerLedgerService.addPurchaseAmount(
                    history.companyId(), period, history.amount(), actorUserId);
            return applyFifoOffset(
                    PayableApprovalLedgerKind.OUTSOURCE,
                    PartnerPrepaidOffsetLedgerKind.OUTSOURCE_HISTORY,
                    history.id(),
                    history.companyId(),
                    history.itemId(),
                    PartnerPaymentCostCategory.OUTSOURCE,
                    history.amount(),
                    actorUserId
            );
        }
        if (item.ledgerKind() == PayableApprovalLedgerKind.ETC_CLAIM) {
            EtcClaimHistoryRecord claim = payableApprovalRepository.findActiveEtcClaim(item.historyId());
            if (claim.recognition() == EtcClaimRecognition.APPROVED) {
                return nonOffsetItem(
                        PayableApprovalLedgerKind.ETC_CLAIM,
                        claim.id(),
                        claim.companyId(),
                        null,
                        claim.amount()
                );
            }
            monthClosingService.assertTransactionOpen(claim.receiptDate());
            FiscalPeriod period = toFiscalPeriod(claim);
            monthClosingService.assertPeriodOpen(period.fiscalYear(), period.fiscalMonth());
            payableApprovalRepository.approveEtcClaim(claim.id(), userId);
            partnerLedgerService.subtractPurchaseAmount(
                    claim.companyId(), period, claim.amount(), actorUserId);
            return nonOffsetItem(
                    PayableApprovalLedgerKind.ETC_CLAIM,
                    claim.id(),
                    claim.companyId(),
                    null,
                    claim.amount()
            );
        }
        if (item.ledgerKind() == PayableApprovalLedgerKind.DEFECT_CLAIM) {
            DefectClaimHistoryRecord claim = payableApprovalRepository.findActiveDefectClaim(item.historyId());
            if (claim.recognition() == EtcClaimRecognition.APPROVED) {
                return nonOffsetItem(
                        PayableApprovalLedgerKind.DEFECT_CLAIM,
                        claim.id(),
                        claim.companyId(),
                        null,
                        claim.amount()
                );
            }
            monthClosingService.assertTransactionOpen(claim.receiptDate());
            FiscalPeriod period = toFiscalPeriod(claim);
            monthClosingService.assertPeriodOpen(period.fiscalYear(), period.fiscalMonth());
            payableApprovalRepository.approveDefectClaim(claim.id(), userId);
            partnerLedgerService.subtractPurchaseAmount(
                    claim.companyId(), period, claim.amount(), actorUserId);
            return nonOffsetItem(
                    PayableApprovalLedgerKind.DEFECT_CLAIM,
                    claim.id(),
                    claim.companyId(),
                    null,
                    claim.amount()
            );
        }
        throw new IllegalArgumentException("지원하지 않는 원장 구분입니다.");
    }

    private ApproveOffsetResultView.ApproveOffsetItemView applyFifoOffset(
            PayableApprovalLedgerKind ledgerKind,
            PartnerPrepaidOffsetLedgerKind offsetLedgerKind,
            long historyId,
            long partnerId,
            Long itemId,
            PartnerPaymentCostCategory costCategory,
            BigDecimal approveAmount,
            String actorUserId
    ) {
        if (itemId == null) {
            return nonOffsetItem(ledgerKind, historyId, partnerId, null, approveAmount);
        }
        List<FifoPrepaidLineView> fifoLines = partnerPaymentRepository.findFifoPrepaidLines(
                partnerId, itemId, costCategory);
        BigDecimal remainToOffset = approveAmount;
        List<PartnerPrepaidOffsetSaveCommand> saves = new ArrayList<>();
        for (FifoPrepaidLineView line : fifoLines) {
            if (remainToOffset.compareTo(BigDecimal.ZERO) <= 0) {
                break;
            }
            BigDecimal take = line.remainingAmount().min(remainToOffset).setScale(2, RoundingMode.HALF_UP);
            if (take.compareTo(BigDecimal.ZERO) <= 0) {
                continue;
            }
            saves.add(new PartnerPrepaidOffsetSaveCommand(
                    line.paymentLineId(),
                    offsetLedgerKind,
                    historyId,
                    take
            ));
            remainToOffset = remainToOffset.subtract(take);
        }
        partnerPaymentRepository.saveOffsets(saves, actorUserId);
        BigDecimal offsetAmount = approveAmount.subtract(remainToOffset).setScale(2, RoundingMode.HALF_UP);
        BigDecimal prepaidAfter = fifoLines.stream()
                .map(FifoPrepaidLineView::remainingAmount)
                .reduce(BigDecimal.ZERO, BigDecimal::add)
                .subtract(offsetAmount)
                .max(BigDecimal.ZERO);
        return toOffsetItem(
                ledgerKind,
                historyId,
                partnerId,
                itemId,
                costCategory,
                approveAmount,
                offsetAmount,
                prepaidAfter,
                true
        );
    }

    private ApproveOffsetResultView.ApproveOffsetItemView simulateOffset(
            PayableApprovalLedgerKind ledgerKind,
            long historyId,
            long partnerId,
            Long itemId,
            PartnerPaymentCostCategory costCategory,
            BigDecimal approveAmount,
            Map<String, BigDecimal> remainingByBucket
    ) {
        if (itemId == null) {
            return nonOffsetItem(ledgerKind, historyId, partnerId, null, approveAmount);
        }
        String bucketKey = partnerId + "|" + itemId + "|" + costCategory.name();
        BigDecimal bucketRemaining = remainingByBucket.computeIfAbsent(bucketKey, key ->
                partnerPaymentRepository.findFifoPrepaidLines(partnerId, itemId, costCategory).stream()
                        .map(FifoPrepaidLineView::remainingAmount)
                        .reduce(BigDecimal.ZERO, BigDecimal::add)
        );
        BigDecimal offsetAmount = approveAmount.min(bucketRemaining).setScale(2, RoundingMode.HALF_UP);
        BigDecimal prepaidAfter = bucketRemaining.subtract(offsetAmount).max(BigDecimal.ZERO);
        remainingByBucket.put(bucketKey, prepaidAfter);
        return toOffsetItem(
                ledgerKind,
                historyId,
                partnerId,
                itemId,
                costCategory,
                approveAmount,
                offsetAmount,
                prepaidAfter,
                true
        );
    }

    private ApproveOffsetResultView.ApproveOffsetItemView alreadyApprovedItem(
            PayableApprovalLedgerKind ledgerKind,
            long historyId,
            long partnerId,
            Long itemId,
            PartnerPaymentCostCategory costCategory,
            BigDecimal approveAmount
    ) {
        BigDecimal offset = partnerPaymentRepository.sumActiveOffsetByHistory(
                ledgerKind == PayableApprovalLedgerKind.PURCHASE
                        ? PartnerPrepaidOffsetLedgerKind.PURCHASE_HISTORY
                        : PartnerPrepaidOffsetLedgerKind.OUTSOURCE_HISTORY,
                historyId
        );
        BigDecimal prepaidAfter = itemId == null
                ? BigDecimal.ZERO
                : partnerPaymentRepository.findFifoPrepaidLines(partnerId, itemId, costCategory).stream()
                        .map(FifoPrepaidLineView::remainingAmount)
                        .reduce(BigDecimal.ZERO, BigDecimal::add);
        return toOffsetItem(
                ledgerKind,
                historyId,
                partnerId,
                itemId,
                costCategory,
                approveAmount,
                offset,
                prepaidAfter,
                itemId != null
        );
    }

    private ApproveOffsetResultView.ApproveOffsetItemView nonOffsetItem(
            PayableApprovalLedgerKind ledgerKind,
            long historyId,
            long partnerId,
            Long itemId,
            BigDecimal amount
    ) {
        return toOffsetItem(
                ledgerKind,
                historyId,
                partnerId,
                itemId,
                null,
                amount,
                BigDecimal.ZERO,
                BigDecimal.ZERO,
                false
        );
    }

    private ApproveOffsetResultView.ApproveOffsetItemView toOffsetItem(
            PayableApprovalLedgerKind ledgerKind,
            long historyId,
            long partnerId,
            Long itemId,
            PartnerPaymentCostCategory costCategory,
            BigDecimal approveAmount,
            BigDecimal offsetAmount,
            BigDecimal prepaidAfter,
            boolean offsetApplicable
    ) {
        String partnerName = companyRepository.findActiveById(partnerId)
                .map(c -> c.companyName())
                .orElse("");
        String itemNo = "";
        String itemName = "";
        if (itemId != null) {
            ItemView item = itemRepository.findActiveById(itemId).orElse(null);
            if (item != null) {
                itemNo = item.itemNo();
                itemName = item.itemName();
            }
        }
        BigDecimal unpaidIncrease = approveAmount.subtract(offsetAmount).max(BigDecimal.ZERO);
        return new ApproveOffsetResultView.ApproveOffsetItemView(
                ledgerKind,
                historyId,
                partnerId,
                partnerName,
                itemId,
                itemNo,
                itemName,
                costCategory,
                approveAmount,
                offsetAmount,
                prepaidAfter,
                unpaidIncrease,
                offsetApplicable
        );
    }

    private static ApproveOffsetResultView toResult(List<ApproveOffsetResultView.ApproveOffsetItemView> offsets) {
        BigDecimal totalApprove = offsets.stream()
                .map(ApproveOffsetResultView.ApproveOffsetItemView::approveAmount)
                .reduce(BigDecimal.ZERO, BigDecimal::add);
        BigDecimal totalOffset = offsets.stream()
                .map(ApproveOffsetResultView.ApproveOffsetItemView::offsetAmount)
                .reduce(BigDecimal.ZERO, BigDecimal::add);
        BigDecimal totalUnpaid = offsets.stream()
                .map(ApproveOffsetResultView.ApproveOffsetItemView::unpaidIncrease)
                .reduce(BigDecimal.ZERO, BigDecimal::add);
        return new ApproveOffsetResultView(offsets.size(), totalApprove, totalOffset, totalUnpaid, offsets);
    }

    private void cancelOne(PayableApprovalItemCommand item, long userId, String actorUserId) {
        if (item.ledgerKind() == PayableApprovalLedgerKind.PURCHASE) {
            PurchaseHistoryRecord history = payableApprovalRepository.findActivePurchaseHistory(item.historyId());
            if (history.approvalStatus() == PayableApprovalStatus.PENDING) {
                return;
            }
            BigDecimal historyOffset = partnerPaymentRepository.sumActiveOffsetByHistory(
                    PartnerPrepaidOffsetLedgerKind.PURCHASE_HISTORY, history.id());
            assertCancelable(history.companyId(), history.amount(), historyOffset);
            monthClosingService.assertTransactionOpen(history.historyDate());
            FiscalPeriod period = toFiscalPeriod(history);
            monthClosingService.assertPeriodOpen(period.fiscalYear(), period.fiscalMonth());
            partnerPaymentRepository.deactivateOffsetsByHistory(
                    PartnerPrepaidOffsetLedgerKind.PURCHASE_HISTORY, history.id(), actorUserId);
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
            BigDecimal historyOffset = partnerPaymentRepository.sumActiveOffsetByHistory(
                    PartnerPrepaidOffsetLedgerKind.OUTSOURCE_HISTORY, history.id());
            assertCancelable(history.companyId(), history.amount(), historyOffset);
            monthClosingService.assertTransactionOpen(history.historyDate());
            FiscalPeriod period = toFiscalPeriod(history);
            monthClosingService.assertPeriodOpen(period.fiscalYear(), period.fiscalMonth());
            partnerPaymentRepository.deactivateOffsetsByHistory(
                    PartnerPrepaidOffsetLedgerKind.OUTSOURCE_HISTORY, history.id(), actorUserId);
            payableApprovalRepository.cancelApprovalOutsourceHistory(history.id(), userId);
            partnerLedgerService.subtractPurchaseAmount(
                    history.companyId(), period, history.amount(), actorUserId);
            return;
        }
        if (item.ledgerKind() == PayableApprovalLedgerKind.ETC_CLAIM) {
            EtcClaimHistoryRecord claim = payableApprovalRepository.findActiveEtcClaim(item.historyId());
            if (claim.recognition() == EtcClaimRecognition.PENDING) {
                return;
            }
            monthClosingService.assertTransactionOpen(claim.receiptDate());
            FiscalPeriod period = toFiscalPeriod(claim);
            monthClosingService.assertPeriodOpen(period.fiscalYear(), period.fiscalMonth());
            payableApprovalRepository.cancelApprovalEtcClaim(claim.id(), userId);
            partnerLedgerService.addPurchaseAmount(
                    claim.companyId(), period, claim.amount(), actorUserId);
            return;
        }
        if (item.ledgerKind() == PayableApprovalLedgerKind.DEFECT_CLAIM) {
            DefectClaimHistoryRecord claim = payableApprovalRepository.findActiveDefectClaim(item.historyId());
            if (claim.recognition() == EtcClaimRecognition.PENDING) {
                return;
            }
            monthClosingService.assertTransactionOpen(claim.receiptDate());
            FiscalPeriod period = toFiscalPeriod(claim);
            monthClosingService.assertPeriodOpen(period.fiscalYear(), period.fiscalMonth());
            payableApprovalRepository.cancelApprovalDefectClaim(claim.id(), userId);
            partnerLedgerService.addPurchaseAmount(
                    claim.companyId(), period, claim.amount(), actorUserId);
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

    private void updateEtcClaimFiscalPeriod(UpdatePayableApprovalFiscalPeriodCommand command, String actorUserId) {
        EtcClaimHistoryRecord claim = payableApprovalRepository.findActiveEtcClaim(command.historyId());
        PayableApprovalStatus status = claim.recognition() == EtcClaimRecognition.APPROVED
                ? PayableApprovalStatus.APPROVED
                : PayableApprovalStatus.PENDING;
        applyFiscalPeriodChange(
                status,
                claim.companyId(),
                claim.amount().negate(),
                claim.receiptDate(),
                claim.fiscalYear(),
                claim.fiscalMonth(),
                command.fiscalYear(),
                command.fiscalMonth(),
                actorUserId,
                (year, month) -> payableApprovalRepository.updateEtcClaimFiscalPeriod(claim.id(), year, month)
        );
    }

    private void updateDefectClaimFiscalPeriod(UpdatePayableApprovalFiscalPeriodCommand command, String actorUserId) {
        DefectClaimHistoryRecord claim = payableApprovalRepository.findActiveDefectClaim(command.historyId());
        PayableApprovalStatus status = claim.recognition() == EtcClaimRecognition.APPROVED
                ? PayableApprovalStatus.APPROVED
                : PayableApprovalStatus.PENDING;
        applyFiscalPeriodChange(
                status,
                claim.companyId(),
                claim.amount().negate(),
                claim.receiptDate(),
                claim.fiscalYear(),
                claim.fiscalMonth(),
                command.fiscalYear(),
                command.fiscalMonth(),
                actorUserId,
                (year, month) -> payableApprovalRepository.updateDefectClaimFiscalPeriod(claim.id(), year, month)
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

    private static FiscalPeriod toFiscalPeriod(EtcClaimHistoryRecord claim) {
        return new FiscalPeriod(claim.fiscalYear(), claim.fiscalMonth());
    }

    private static FiscalPeriod toFiscalPeriod(DefectClaimHistoryRecord claim) {
        return new FiscalPeriod(claim.fiscalYear(), claim.fiscalMonth());
    }

    private void assertCancelable(long partnerId, BigDecimal amount, BigDecimal historyOffset) {
        BigDecimal unpaid = partnerPaymentRepository.unpaidPartnerAmount(partnerId);
        BigDecimal effectiveUnpaid = unpaid.add(historyOffset != null ? historyOffset : BigDecimal.ZERO);
        if (amount.compareTo(effectiveUnpaid) > 0) {
            throw new IllegalArgumentException(
                    "승인취소 금액이 미지급 잔액(" + unpaid.stripTrailingZeros().toPlainString() + ")을 초과합니다."
            );
        }
    }
}
