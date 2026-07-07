package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.ledger.PartnerLedgerService;
import com.shindong.smartmanager.domain.company.CompanyRoleType;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.List;

public class PartnerPaymentService {

    private final PartnerPaymentRepository partnerPaymentRepository;
    private final CompanyRepository companyRepository;
    private final PartnerLedgerService partnerLedgerService;
    private final MonthClosingService monthClosingService;
    private final DomainEventStore domainEventStore;

    public PartnerPaymentService(
            PartnerPaymentRepository partnerPaymentRepository,
            CompanyRepository companyRepository,
            PartnerLedgerService partnerLedgerService,
            MonthClosingService monthClosingService,
            DomainEventStore domainEventStore
    ) {
        this.partnerPaymentRepository = partnerPaymentRepository;
        this.companyRepository = companyRepository;
        this.partnerLedgerService = partnerLedgerService;
        this.monthClosingService = monthClosingService;
        this.domainEventStore = domainEventStore;
    }

    public List<PartnerPaymentCandidateView> listCandidates(PartnerPaymentCandidateCriteria criteria) {
        return partnerPaymentRepository.findCandidates(criteria);
    }

    public List<PartnerPaymentView> list(PartnerPaymentListCriteria criteria) {
        return partnerPaymentRepository.findAllActive(criteria);
    }

    public PartnerPaymentView register(CreatePartnerPaymentCommand command, String actorUserId) {
        if (command.partnerId() <= 0) {
            throw new IllegalArgumentException("지급 거래처를 선택해 주세요.");
        }
        if (command.costCategory() == null) {
            throw new IllegalArgumentException("비용 구분을 선택해 주세요.");
        }
        companyRepository.findActiveById(command.partnerId())
                .orElseThrow(() -> new IllegalArgumentException("거래처를 찾을 수 없습니다: " + command.partnerId()));

        List<CompanyRoleType> roles = companyRepository.findRoles(command.partnerId());
        if (command.costCategory() == PartnerPaymentCostCategory.PURCHASE
                && !roles.contains(CompanyRoleType.PURCHASE)) {
            throw new IllegalArgumentException("구매거래처만 구매 지급을 등록할 수 있습니다.");
        }
        if (command.costCategory() == PartnerPaymentCostCategory.OUTSOURCE
                && !roles.contains(CompanyRoleType.OUTSOURCE)) {
            throw new IllegalArgumentException("외주거래처만 외주 지급을 등록할 수 있습니다.");
        }

        BigDecimal supplyAmount = normalizeMoney(command.supplyAmount(), "공급가");
        BigDecimal vatAmount = command.vatAmount() != null ? normalizeMoney(command.vatAmount(), "부가세") : BigDecimal.ZERO;
        BigDecimal totalAmount = supplyAmount.add(vatAmount).setScale(2, RoundingMode.HALF_UP);
        if (totalAmount.compareTo(BigDecimal.ZERO) <= 0) {
            throw new IllegalArgumentException("지급 금액은 0보다 커야 합니다.");
        }

        LocalDate paymentDate = command.paymentDate() != null ? command.paymentDate() : LocalDate.now();
        monthClosingService.assertTransactionOpen(paymentDate);

        BigDecimal unpaid = unpaidAmount(command.partnerId());
        if (totalAmount.compareTo(unpaid) > 0) {
            throw new IllegalArgumentException(
                    "지급 금액이 미지급 잔액(" + unpaid.stripTrailingZeros().toPlainString() + ")을 초과합니다."
            );
        }

        String paymentNo = nextPaymentNo(paymentDate);
        PartnerPaymentView saved = partnerPaymentRepository.save(
                new PartnerPaymentSaveCommand(
                        paymentNo,
                        command.partnerId(),
                        paymentDate,
                        command.costCategory(),
                        supplyAmount,
                        vatAmount,
                        totalAmount,
                        normalizeText(command.paymentMethod()),
                        normalizeText(command.remark())
                ),
                actorUserId
        );

        partnerLedgerService.addPaidAmount(command.partnerId(), paymentDate, totalAmount, actorUserId);
        appendEvent(EventTypes.PARTNER_PAYMENT_REGISTERED, saved.id(), actorUserId, saved.paymentNo());
        return partnerPaymentRepository.findActiveIssuedById(saved.id()).orElse(saved);
    }

    public void cancel(long id, String actorUserId) {
        PartnerPaymentView payment = partnerPaymentRepository.findActiveIssuedById(id)
                .orElseThrow(() -> new IllegalArgumentException("지급을 찾을 수 없습니다: " + id));
        if (!payment.cancelable()) {
            throw new IllegalArgumentException("취소할 수 없는 지급입니다.");
        }
        monthClosingService.assertTransactionOpen(payment.paymentDate());

        partnerLedgerService.subtractPaidAmount(
                payment.partnerId(),
                payment.paymentDate(),
                payment.totalAmount(),
                actorUserId
        );
        partnerPaymentRepository.cancelById(id, actorUserId);
        appendEvent(EventTypes.PARTNER_PAYMENT_CANCELLED, id, actorUserId, payment.paymentNo());
    }

    private BigDecimal unpaidAmount(long partnerId) {
        BigDecimal payable = partnerPaymentRepository.sumIssuedPayableAmountByPartnerId(partnerId);
        BigDecimal paid = partnerPaymentRepository.sumIssuedPaymentAmountByPartnerId(partnerId);
        return payable.subtract(paid).max(BigDecimal.ZERO);
    }

    private static BigDecimal normalizeMoney(BigDecimal amount, String label) {
        if (amount == null) {
            throw new IllegalArgumentException(label + "는 필수입니다.");
        }
        if (amount.compareTo(BigDecimal.ZERO) < 0) {
            throw new IllegalArgumentException(label + "는 0 이상이어야 합니다.");
        }
        return amount.setScale(2, RoundingMode.HALF_UP);
    }

    private static String normalizeText(String value) {
        if (value == null) {
            return null;
        }
        String trimmed = value.trim();
        return trimmed.isEmpty() ? null : trimmed;
    }

    private String nextPaymentNo(LocalDate paymentDate) {
        String prefix = "PP-" + paymentDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        long seq = partnerPaymentRepository.countByPaymentNoPrefix(prefix) + 1;
        return prefix + String.format("%03d", seq);
    }

    private void appendEvent(String eventType, long paymentId, String actorUserId, String paymentNo) {
        String payload = """
                {"partnerPaymentId":%d,"paymentNo":"%s"}
                """.formatted(paymentId, paymentNo).trim();
        domainEventStore.append(DomainEvent.create(
                eventType,
                1,
                AggregateTypes.PARTNER_PAYMENT,
                String.valueOf(paymentId),
                actorUserId,
                payload
        ));
    }
}
