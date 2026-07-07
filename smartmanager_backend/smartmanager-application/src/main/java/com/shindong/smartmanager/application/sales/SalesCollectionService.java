package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.ledger.PartnerLedgerService;
import com.shindong.smartmanager.domain.company.CompanyRoleType;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.List;

public class SalesCollectionService {

    private final SalesCollectionRepository salesCollectionRepository;
    private final CompanyRepository companyRepository;
    private final PartnerLedgerService partnerLedgerService;
    private final MonthClosingService monthClosingService;
    private final DomainEventStore domainEventStore;

    public SalesCollectionService(
            SalesCollectionRepository salesCollectionRepository,
            CompanyRepository companyRepository,
            PartnerLedgerService partnerLedgerService,
            MonthClosingService monthClosingService,
            DomainEventStore domainEventStore
    ) {
        this.salesCollectionRepository = salesCollectionRepository;
        this.companyRepository = companyRepository;
        this.partnerLedgerService = partnerLedgerService;
        this.monthClosingService = monthClosingService;
        this.domainEventStore = domainEventStore;
    }

    public List<SalesCollectionCandidateView> listCandidates(SalesCollectionCandidateCriteria criteria) {
        return salesCollectionRepository.findCandidates(criteria);
    }

    public List<SalesCollectionView> list(SalesCollectionListCriteria criteria) {
        return salesCollectionRepository.findAllActive(criteria);
    }

    public SalesCollectionView register(CreateSalesCollectionCommand command, String actorUserId) {
        if (command.partnerId() <= 0) {
            throw new IllegalArgumentException("수금 거래처를 선택해 주세요.");
        }
        companyRepository.findActiveById(command.partnerId())
                .orElseThrow(() -> new IllegalArgumentException("거래처를 찾을 수 없습니다: " + command.partnerId()));
        if (!companyRepository.findRoles(command.partnerId()).contains(CompanyRoleType.SALES)) {
            throw new IllegalArgumentException("수주거래처(SALES)만 수금 등록할 수 있습니다.");
        }

        BigDecimal supplyAmount = normalizeMoney(command.supplyAmount(), "공급가");
        BigDecimal vatAmount = command.vatAmount() != null ? normalizeMoney(command.vatAmount(), "부가세") : BigDecimal.ZERO;
        BigDecimal totalAmount = supplyAmount.add(vatAmount).setScale(2, RoundingMode.HALF_UP);
        if (totalAmount.compareTo(BigDecimal.ZERO) <= 0) {
            throw new IllegalArgumentException("수금 금액은 0보다 커야 합니다.");
        }

        LocalDate collectionDate = command.collectionDate() != null ? command.collectionDate() : LocalDate.now();
        monthClosingService.assertTransactionOpen(collectionDate);

        BigDecimal uncollected = uncollectedAmount(command.partnerId());
        if (totalAmount.compareTo(uncollected) > 0) {
            throw new IllegalArgumentException(
                    "수금 금액이 미수 잔액(" + uncollected.stripTrailingZeros().toPlainString() + ")을 초과합니다."
            );
        }

        String collectionNo = nextCollectionNo(collectionDate);
        SalesCollectionView saved = salesCollectionRepository.save(
                new SalesCollectionSaveCommand(
                        collectionNo,
                        command.partnerId(),
                        collectionDate,
                        supplyAmount,
                        vatAmount,
                        totalAmount,
                        normalizeText(command.paymentMethod()),
                        normalizeText(command.remark())
                ),
                actorUserId
        );

        partnerLedgerService.addCollectedAmount(command.partnerId(), collectionDate, totalAmount, actorUserId);
        appendEvent(EventTypes.SALES_COLLECTION_REGISTERED, saved.id(), actorUserId, saved.collectionNo());
        return salesCollectionRepository.findActiveIssuedById(saved.id()).orElse(saved);
    }

    public void cancel(long id, String actorUserId) {
        SalesCollectionView collection = salesCollectionRepository.findActiveIssuedById(id)
                .orElseThrow(() -> new IllegalArgumentException("수금을 찾을 수 없습니다: " + id));
        if (!collection.cancelable()) {
            throw new IllegalArgumentException("취소할 수 없는 수금입니다.");
        }
        monthClosingService.assertTransactionOpen(collection.collectionDate());

        partnerLedgerService.subtractCollectedAmount(
                collection.partnerId(),
                collection.collectionDate(),
                collection.totalAmount(),
                actorUserId
        );
        salesCollectionRepository.cancelById(id, actorUserId);
        appendEvent(EventTypes.SALES_COLLECTION_CANCELLED, id, actorUserId, collection.collectionNo());
    }

    private BigDecimal uncollectedAmount(long partnerId) {
        BigDecimal revenue = salesCollectionRepository.sumIssuedRevenueAmountByPartnerId(partnerId);
        BigDecimal collected = salesCollectionRepository.sumIssuedCollectionAmountByPartnerId(partnerId);
        return revenue.subtract(collected).max(BigDecimal.ZERO);
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

    private String nextCollectionNo(LocalDate collectionDate) {
        String prefix = "SC-" + collectionDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        long seq = salesCollectionRepository.countByCollectionNoPrefix(prefix) + 1;
        return prefix + String.format("%03d", seq);
    }

    private void appendEvent(String eventType, long collectionId, String actorUserId, String collectionNo) {
        String payload = """
                {"salesCollectionId":%d,"collectionNo":"%s"}
                """.formatted(collectionId, collectionNo).trim();
        domainEventStore.append(DomainEvent.create(
                eventType,
                1,
                AggregateTypes.SALES_COLLECTION,
                String.valueOf(collectionId),
                actorUserId,
                payload
        ));
    }
}
