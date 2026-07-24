package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.ledger.PartnerLedgerService;
import com.shindong.smartmanager.domain.company.CompanyRoleType;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentKind;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;

public class PartnerPaymentService {

    private final PartnerPaymentRepository partnerPaymentRepository;
    private final CompanyRepository companyRepository;
    private final ItemRepository itemRepository;
    private final PartnerLedgerService partnerLedgerService;
    private final MonthClosingService monthClosingService;
    private final DomainEventStore domainEventStore;

    public PartnerPaymentService(
            PartnerPaymentRepository partnerPaymentRepository,
            CompanyRepository companyRepository,
            ItemRepository itemRepository,
            PartnerLedgerService partnerLedgerService,
            MonthClosingService monthClosingService,
            DomainEventStore domainEventStore
    ) {
        this.partnerPaymentRepository = partnerPaymentRepository;
        this.companyRepository = companyRepository;
        this.itemRepository = itemRepository;
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

    public List<PrepaidBalanceView> listPrepaidBalances(Long partnerId, PartnerPaymentCostCategory costCategory) {
        return partnerPaymentRepository.findPrepaidBalances(partnerId, costCategory);
    }

    public List<PrepaidOrderLineCandidateView> listOrderLineCandidates(PrepaidOrderLineCandidateCriteria criteria) {
        return partnerPaymentRepository.findOrderLineCandidates(criteria);
    }

    public PartnerPaymentView register(CreatePartnerPaymentCommand command, String actorUserId) {
        if (command.partnerId() <= 0) {
            throw new IllegalArgumentException("지급 거래처를 선택해 주세요.");
        }
        if (command.costCategory() == null) {
            throw new IllegalArgumentException("비용 구분을 선택해 주세요.");
        }
        PartnerPaymentKind paymentKind = command.paymentKind() != null
                ? command.paymentKind()
                : PartnerPaymentKind.NORMAL;

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

        LocalDate paymentDate = command.paymentDate() != null ? command.paymentDate() : LocalDate.now();
        monthClosingService.assertTransactionOpen(paymentDate);

        BigDecimal supplyAmount;
        BigDecimal vatAmount;
        BigDecimal totalAmount;
        List<PartnerPaymentLineCommand> lines = List.of();

        if (paymentKind == PartnerPaymentKind.PREPAID) {
            lines = validatePrepaidLines(command);
            supplyAmount = lines.stream().map(PartnerPaymentLineCommand::supplyAmount)
                    .reduce(BigDecimal.ZERO, BigDecimal::add);
            vatAmount = lines.stream()
                    .map(line -> line.vatAmount() != null ? line.vatAmount() : BigDecimal.ZERO)
                    .reduce(BigDecimal.ZERO, BigDecimal::add);
            totalAmount = supplyAmount.add(vatAmount).setScale(2, RoundingMode.HALF_UP);
        } else {
            if (command.lines() != null && !command.lines().isEmpty()) {
                throw new IllegalArgumentException("일반지급에는 품목 라인을 입력할 수 없습니다.");
            }
            supplyAmount = normalizeMoney(command.supplyAmount(), "공급가");
            vatAmount = command.vatAmount() != null ? normalizeMoney(command.vatAmount(), "부가세") : BigDecimal.ZERO;
            totalAmount = supplyAmount.add(vatAmount).setScale(2, RoundingMode.HALF_UP);
            if (supplyAmount.compareTo(BigDecimal.ZERO) <= 0) {
                throw new IllegalArgumentException("지급 공급가는 0보다 커야 합니다.");
            }
            // 미지급 잔액·원장은 공급가 기준 — 부가세 포함 총액과 비교하지 않는다.
            BigDecimal unpaid = partnerPaymentRepository.unpaidPartnerAmount(command.partnerId());
            if (supplyAmount.compareTo(unpaid) > 0) {
                throw new IllegalArgumentException(
                        "일반지급 공급가가 미지급 잔액(" + unpaid.stripTrailingZeros().toPlainString() + ")을 초과합니다."
                );
            }
        }

        String paymentNo = nextPaymentNo(paymentDate);
        PartnerPaymentView saved = partnerPaymentRepository.save(
                new PartnerPaymentSaveCommand(
                        paymentNo,
                        command.partnerId(),
                        paymentDate,
                        command.costCategory(),
                        paymentKind,
                        supplyAmount,
                        vatAmount,
                        totalAmount,
                        normalizeText(command.paymentMethod()),
                        normalizeText(command.remark()),
                        lines
                ),
                actorUserId
        );

        partnerLedgerService.addPaidAmount(command.partnerId(), paymentDate, supplyAmount, actorUserId);
        appendEvent(EventTypes.PARTNER_PAYMENT_REGISTERED, saved.id(), actorUserId, saved.paymentNo());
        return partnerPaymentRepository.findActiveIssuedById(saved.id()).orElse(saved);
    }

    public void cancel(long id, String actorUserId) {
        PartnerPaymentView payment = partnerPaymentRepository.findActiveIssuedById(id)
                .orElseThrow(() -> new IllegalArgumentException("지급을 찾을 수 없습니다: " + id));
        if (!payment.cancelable()) {
            throw new IllegalArgumentException("취소할 수 없는 지급입니다.");
        }
        if (partnerPaymentRepository.hasActiveOffsetByPaymentId(id)) {
            throw new IllegalArgumentException(
                    "선급 상계가 남아 있어 지급을 취소할 수 없습니다. 승인취소로 선급을 복원한 뒤 다시 시도해 주세요."
            );
        }
        monthClosingService.assertTransactionOpen(payment.paymentDate());

        partnerLedgerService.subtractPaidAmount(
                payment.partnerId(),
                payment.paymentDate(),
                payment.supplyAmount(),
                actorUserId
        );
        partnerPaymentRepository.cancelById(id, actorUserId);
        appendEvent(EventTypes.PARTNER_PAYMENT_CANCELLED, id, actorUserId, payment.paymentNo());
    }

    private List<PartnerPaymentLineCommand> validatePrepaidLines(CreatePartnerPaymentCommand command) {
        if (command.lines() == null || command.lines().isEmpty()) {
            throw new IllegalArgumentException("선지급은 품목 라인을 1건 이상 입력해 주세요.");
        }
        List<PartnerPaymentLineCommand> normalized = new ArrayList<>();
        BigDecimal headerTotal = BigDecimal.ZERO;
        for (PartnerPaymentLineCommand line : command.lines()) {
            if (line.itemId() <= 0) {
                throw new IllegalArgumentException("선지급 라인의 품목을 선택해 주세요.");
            }
            itemRepository.findActiveById(line.itemId())
                    .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + line.itemId()));

            if (line.purchaseOrderLineId() != null && line.outsourcingOrderLineId() != null) {
                throw new IllegalArgumentException("구매 발주라인과 외주 발주라인을 동시에 연결할 수 없습니다.");
            }
            if (command.costCategory() == PartnerPaymentCostCategory.PURCHASE
                    && line.outsourcingOrderLineId() != null) {
                throw new IllegalArgumentException("구매 선지급에는 외주 발주라인을 연결할 수 없습니다.");
            }
            if (command.costCategory() == PartnerPaymentCostCategory.OUTSOURCE
                    && line.purchaseOrderLineId() != null) {
                throw new IllegalArgumentException("외주 선지급에는 구매 발주라인을 연결할 수 없습니다.");
            }

            BigDecimal supply = normalizeMoney(line.supplyAmount(), "라인 공급가");
            BigDecimal vat = line.vatAmount() != null ? normalizeMoney(line.vatAmount(), "라인 부가세") : BigDecimal.ZERO;
            BigDecimal total = supply.add(vat).setScale(2, RoundingMode.HALF_UP);
            if (total.compareTo(BigDecimal.ZERO) <= 0) {
                throw new IllegalArgumentException("선지급 라인 금액은 0보다 커야 합니다.");
            }

            Long linkedLineId = line.purchaseOrderLineId() != null
                    ? line.purchaseOrderLineId()
                    : line.outsourcingOrderLineId();
            if (linkedLineId != null) {
                BigDecimal remaining = partnerPaymentRepository.remainingOrderLineAmount(
                        command.costCategory(), linkedLineId);
                if (supply.compareTo(remaining) > 0) {
                    throw new IllegalArgumentException(
                            "발주라인 잔여금액(" + remaining.stripTrailingZeros().toPlainString()
                                    + ")을 초과하는 선지급은 등록할 수 없습니다."
                    );
                }
            }

            normalized.add(new PartnerPaymentLineCommand(
                    line.itemId(),
                    line.purchaseOrderLineId(),
                    line.outsourcingOrderLineId(),
                    supply,
                    vat
            ));
            headerTotal = headerTotal.add(total);
        }

        if (command.supplyAmount() != null || command.vatAmount() != null) {
            BigDecimal requestedSupply = command.supplyAmount() != null
                    ? normalizeMoney(command.supplyAmount(), "공급가")
                    : normalized.stream().map(PartnerPaymentLineCommand::supplyAmount)
                            .reduce(BigDecimal.ZERO, BigDecimal::add);
            BigDecimal requestedVat = command.vatAmount() != null
                    ? normalizeMoney(command.vatAmount(), "부가세")
                    : normalized.stream()
                            .map(l -> l.vatAmount() != null ? l.vatAmount() : BigDecimal.ZERO)
                            .reduce(BigDecimal.ZERO, BigDecimal::add);
            BigDecimal requestedTotal = requestedSupply.add(requestedVat).setScale(2, RoundingMode.HALF_UP);
            if (requestedTotal.compareTo(headerTotal) != 0) {
                throw new IllegalArgumentException("선지급 헤더 금액과 라인 합계가 일치하지 않습니다.");
            }
        }
        return normalized;
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
