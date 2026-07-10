package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.FiscalPeriod;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.ledger.PartnerLedgerService;
import com.shindong.smartmanager.domain.purchase.EtcPurchaseOrderStatus;
import com.shindong.smartmanager.domain.purchase.PayableApprovalStatus;
import com.shindong.smartmanager.domain.purchase.PurchaseHistorySourceType;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;

public class EtcPurchaseReceiptService {

    private final EtcPurchaseOrderRepository orderRepository;
    private final EtcPurchaseReceiptRepository receiptRepository;
    private final PurchaseHistoryRepository purchaseHistoryRepository;
    private final PartnerLedgerService partnerLedgerService;
    private final MonthClosingService monthClosingService;
    private final FiscalCalendarService fiscalCalendarService;

    public EtcPurchaseReceiptService(
            EtcPurchaseOrderRepository orderRepository,
            EtcPurchaseReceiptRepository receiptRepository,
            PurchaseHistoryRepository purchaseHistoryRepository,
            PartnerLedgerService partnerLedgerService,
            MonthClosingService monthClosingService,
            FiscalCalendarService fiscalCalendarService
    ) {
        this.orderRepository = orderRepository;
        this.receiptRepository = receiptRepository;
        this.purchaseHistoryRepository = purchaseHistoryRepository;
        this.partnerLedgerService = partnerLedgerService;
        this.monthClosingService = monthClosingService;
        this.fiscalCalendarService = fiscalCalendarService;
    }

    public List<EtcPurchaseReceiptCandidateView> listCandidates(EtcPurchaseReceiptCandidateCriteria criteria) {
        return orderRepository.findReceiptCandidates(criteria);
    }

    public List<EtcPurchaseReceiptView> list(EtcPurchaseReceiptListCriteria criteria) {
        return receiptRepository.findActive(criteria);
    }

    public EtcPurchaseReceiptView get(long id) {
        return receiptRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("기타구매입고를 찾을 수 없습니다: " + id));
    }

    public List<EtcPurchaseReceiptView> register(CreateEtcPurchaseReceiptCommand command, String actorUserId) {
        if (command.lines() == null || command.lines().isEmpty()) {
            throw new IllegalArgumentException("입고할 발주를 1건 이상 선택해 주세요.");
        }
        LocalDate receiptDate = requireDate(command.receiptDate(), "납품일");
        monthClosingService.assertTransactionOpen(receiptDate);
        FiscalPeriod period = fiscalCalendarService.resolvePeriod(
                receiptDate,
                command.fiscalYear(),
                command.fiscalMonth()
        );
        monthClosingService.assertPeriodOpen(period.fiscalYear(), period.fiscalMonth());

        List<EtcPurchaseReceiptView> created = new ArrayList<>();
        for (CreateEtcPurchaseReceiptLineCommand line : command.lines()) {
            created.add(registerLine(line, receiptDate, period, actorUserId));
        }
        return created;
    }

    public EtcPurchaseReceiptView update(long id, UpdateEtcPurchaseReceiptCommand command, String actorUserId) {
        EtcPurchaseReceiptView existing = get(id);
        LocalDate newDate = requireDate(command.receiptDate(), "납품일");
        BigDecimal newQty = normalizeQty(command.receiptQty(), "납품수량");
        BigDecimal newAmount = EtcPurchaseOrderService.lineAmount(newQty, existing.unitPrice());

        monthClosingService.assertTransactionOpen(existing.receiptDate());
        monthClosingService.assertTransactionOpen(newDate);

        reversePayable(existing, actorUserId);
        adjustOrderRemainOnUpdate(existing, newQty, actorUserId);

        FiscalPeriod period = fiscalCalendarService.resolvePeriod(
                newDate,
                command.fiscalYear(),
                command.fiscalMonth()
        );
        monthClosingService.assertPeriodOpen(period.fiscalYear(), period.fiscalMonth());
        receiptRepository.update(id, new EtcPurchaseReceiptSaveCommand(
                existing.etcPurchaseOrderId(),
                existing.partnerId(),
                existing.itemName(),
                newQty,
                existing.unitPrice(),
                newAmount,
                newDate,
                period.fiscalYear(),
                period.fiscalMonth()
        ), actorUserId);

        EtcPurchaseReceiptView updated = get(id);
        applyPayable(updated, actorUserId);
        return updated;
    }

    public void cancel(long id, String actorUserId) {
        EtcPurchaseReceiptView existing = get(id);
        monthClosingService.assertTransactionOpen(existing.receiptDate());
        reversePayable(existing, actorUserId);

        EtcPurchaseOrderView order = orderRepository.findActiveById(existing.etcPurchaseOrderId())
                .orElseThrow(() -> new IllegalStateException("기타구매발주를 찾을 수 없습니다."));
        BigDecimal newRemain = order.remainQty().add(existing.receiptQty());
        orderRepository.updateRemainQtyAndStatus(existing.etcPurchaseOrderId(), newRemain, actorUserId);
        receiptRepository.delete(id);
    }

    private EtcPurchaseReceiptView registerLine(
            CreateEtcPurchaseReceiptLineCommand line,
            LocalDate receiptDate,
            FiscalPeriod period,
            String actorUserId
    ) {
        EtcPurchaseOrderView order = orderRepository.findActiveById(line.etcPurchaseOrderId())
                .orElseThrow(() -> new IllegalArgumentException("기타구매발주를 찾을 수 없습니다: " + line.etcPurchaseOrderId()));
        if (order.status() == EtcPurchaseOrderStatus.COMPLETED) {
            throw new IllegalArgumentException("이미 완료된 발주입니다: " + order.orderNo());
        }
        BigDecimal receiptQty = normalizeQty(line.receiptQty(), "납품수량");
        if (receiptQty.compareTo(order.remainQty()) > 0) {
            throw new IllegalArgumentException(
                    "납품수량이 잔량(" + order.remainQty().stripTrailingZeros().toPlainString() + ")을 초과합니다."
            );
        }

        BigDecimal amount = EtcPurchaseOrderService.lineAmount(receiptQty, order.unitPrice());
        String receiptNo = nextReceiptNo(receiptDate);
        long receiptId = receiptRepository.save(new EtcPurchaseReceiptSaveCommand(
                order.id(),
                order.partnerId(),
                order.itemName(),
                receiptQty,
                order.unitPrice(),
                amount,
                receiptDate,
                period.fiscalYear(),
                period.fiscalMonth()
        ), receiptNo, actorUserId);

        BigDecimal newRemain = order.remainQty().subtract(receiptQty);
        orderRepository.updateRemainQtyAndStatus(order.id(), newRemain, actorUserId);

        EtcPurchaseReceiptView saved = get(receiptId);
        applyPayable(saved, actorUserId);
        return saved;
    }

    private void adjustOrderRemainOnUpdate(EtcPurchaseReceiptView existing, BigDecimal newQty, String actorUserId) {
        EtcPurchaseOrderView order = orderRepository.findActiveById(existing.etcPurchaseOrderId())
                .orElseThrow(() -> new IllegalStateException("기타구매발주를 찾을 수 없습니다."));
        BigDecimal delta = existing.receiptQty().subtract(newQty);
        BigDecimal newRemain = order.remainQty().add(delta);
        if (newRemain.compareTo(BigDecimal.ZERO) < 0) {
            throw new IllegalArgumentException("납품수량이 발주 잔량을 초과합니다.");
        }
        if (newRemain.compareTo(order.orderQty()) > 0) {
            throw new IllegalArgumentException("납품수량 조정 결과가 올바르지 않습니다.");
        }
        orderRepository.updateRemainQtyAndStatus(order.id(), newRemain, actorUserId);
    }

    private void applyPayable(EtcPurchaseReceiptView receipt, String actorUserId) {
        purchaseHistoryRepository.save(new PurchaseHistoryCommand(
                receipt.partnerId(),
                null,
                receipt.itemName(),
                receipt.receiptQty(),
                receipt.unitPrice(),
                receipt.amount(),
                receipt.receiptDate(),
                PurchaseHistorySourceType.ETC_PURCHASE_RECEIPT,
                receipt.id(),
                receipt.fiscalYear(),
                receipt.fiscalMonth(),
                actorUserId
        ));
        // 지급 확정은 승인처리 화면에서 반영 (partner_ledger 미갱신)
    }

    private void reversePayable(EtcPurchaseReceiptView receipt, String actorUserId) {
        List<PurchaseHistoryRecord> histories = purchaseHistoryRepository.findActiveBySource(
                PurchaseHistorySourceType.ETC_PURCHASE_RECEIPT, receipt.id());
        purchaseHistoryRepository.deactivateBySource(
                PurchaseHistorySourceType.ETC_PURCHASE_RECEIPT,
                receipt.id(),
                actorUserId
        );
        for (PurchaseHistoryRecord history : histories) {
            if (history.approvalStatus() == PayableApprovalStatus.APPROVED) {
                partnerLedgerService.subtractPurchaseAmount(
                        receipt.partnerId(), history.historyDate(), history.amount(), actorUserId);
            }
        }
    }

    private String nextReceiptNo(LocalDate receiptDate) {
        String prefix = "EPR-" + receiptDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        long seq = receiptRepository.countByReceiptNoPrefix(prefix) + 1;
        return prefix + String.format("%03d", seq);
    }

    private static LocalDate requireDate(LocalDate value, String label) {
        if (value == null) {
            throw new IllegalArgumentException(label + "은(는) 필수입니다.");
        }
        return value;
    }

    private static BigDecimal normalizeQty(BigDecimal value, String label) {
        if (value == null || value.compareTo(BigDecimal.ZERO) <= 0) {
            throw new IllegalArgumentException(label + "은(는) 0보다 커야 합니다.");
        }
        return value.setScale(4, RoundingMode.HALF_UP);
    }
}
