package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.domain.company.CompanyRoleType;
import com.shindong.smartmanager.domain.purchase.EtcPurchaseOrderStatus;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

public class EtcPurchaseOrderService {

    private static final String DEFAULT_ISSUER_COMPANY_NAME = "유한책임회사 신동공업";
    private static final String DEFAULT_ISSUER_ADDRESS = "경상남도 사천시 곤양면 곤북로 82";
    private static final String DEFAULT_ISSUER_PHONE = "055) 855-0145";
    private static final String DEFAULT_ISSUER_FAX = "855-0143";

    private final EtcPurchaseOrderRepository orderRepository;
    private final CompanyRepository companyRepository;
    private final MonthClosingService monthClosingService;

    public EtcPurchaseOrderService(
            EtcPurchaseOrderRepository orderRepository,
            CompanyRepository companyRepository,
            MonthClosingService monthClosingService
    ) {
        this.orderRepository = orderRepository;
        this.companyRepository = companyRepository;
        this.monthClosingService = monthClosingService;
    }

    public List<EtcPurchaseOrderView> list(EtcPurchaseOrderListCriteria criteria) {
        return orderRepository.findActive(criteria);
    }

    public EtcPurchaseOrderView get(long id) {
        return orderRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("기타구매발주를 찾을 수 없습니다: " + id));
    }

    public PurchaseOrderPrintView getPrintView(long id) {
        return buildPrintView(List.of(get(id)));
    }

    public List<PurchaseOrderPrintView> getBatchPrintViews(List<Long> orderIds) {
        if (orderIds == null || orderIds.isEmpty()) {
            throw new IllegalArgumentException("출력할 발주를 1건 이상 선택해 주세요.");
        }
        List<EtcPurchaseOrderView> orders = new ArrayList<>();
        for (Long orderId : orderIds) {
            if (orderId == null) {
                continue;
            }
            orders.add(get(orderId));
        }
        if (orders.isEmpty()) {
            throw new IllegalArgumentException("출력할 발주를 1건 이상 선택해 주세요.");
        }
        Map<Long, List<EtcPurchaseOrderView>> byPartner = orders.stream()
                .collect(Collectors.groupingBy(EtcPurchaseOrderView::partnerId));
        List<PurchaseOrderPrintView> views = new ArrayList<>();
        for (List<EtcPurchaseOrderView> partnerOrders : byPartner.values()) {
            views.add(buildPrintView(partnerOrders));
        }
        views.sort((left, right) -> left.partnerName().compareToIgnoreCase(right.partnerName()));
        return views;
    }

    private PurchaseOrderPrintView buildPrintView(List<EtcPurchaseOrderView> orders) {
        if (orders.isEmpty()) {
            throw new IllegalArgumentException("출력할 발주가 없습니다.");
        }
        EtcPurchaseOrderView first = orders.get(0);
        String orderNos = orders.stream()
                .map(EtcPurchaseOrderView::orderNo)
                .distinct()
                .collect(Collectors.joining(", "));
        LocalDate orderDate = orders.stream()
                .map(EtcPurchaseOrderView::orderDate)
                .min(LocalDate::compareTo)
                .orElse(first.orderDate());

        List<PurchaseOrderPrintLineView> lines = new ArrayList<>();
        BigDecimal totalAmount = BigDecimal.ZERO;
        int lineNo = 1;
        for (EtcPurchaseOrderView order : orders) {
            BigDecimal amount = order.amount() != null ? order.amount() : BigDecimal.ZERO;
            lines.add(new PurchaseOrderPrintLineView(
                    lineNo++,
                    "—",
                    order.itemName(),
                    order.categoryName(),
                    "—",
                    order.orderQty(),
                    order.unitPrice(),
                    amount,
                    order.requestedDeliveryDate()
            ));
            totalAmount = totalAmount.add(amount);
        }
        return new PurchaseOrderPrintView(
                orderNos,
                orderDate,
                first.partnerName(),
                first.partnerBusinessRegNo(),
                DEFAULT_ISSUER_COMPANY_NAME,
                DEFAULT_ISSUER_ADDRESS,
                DEFAULT_ISSUER_PHONE,
                DEFAULT_ISSUER_FAX,
                "—",
                lines,
                totalAmount
        );
    }

    public EtcPurchaseOrderView create(CreateEtcPurchaseOrderCommand command, String actorUserId) {
        validatePartner(command.partnerId());
        String itemName = requireText(command.itemName(), "품목명");
        BigDecimal unitPrice = normalizeMoney(command.unitPrice(), "개별 단가");
        BigDecimal orderQty = normalizeQty(command.orderQty(), "수량");
        LocalDate orderDate = command.orderDate() != null ? command.orderDate() : LocalDate.now();
        LocalDate deliveryDate = requireDate(command.requestedDeliveryDate(), "납기요구일");
        monthClosingService.assertTransactionOpen(orderDate);

        BigDecimal amount = lineAmount(orderQty, unitPrice);
        String orderNo = nextOrderNo(orderDate);
        long id = orderRepository.save(
                new CreateEtcPurchaseOrderCommand(
                        itemName,
                        command.partnerId(),
                        unitPrice,
                        orderQty,
                        deliveryDate,
                        command.categoryCodeId(),
                        orderDate
                ),
                orderNo,
                actorUserId
        );
        return get(id);
    }

    public EtcPurchaseOrderView update(long id, UpdateEtcPurchaseOrderCommand command, String actorUserId) {
        EtcPurchaseOrderView existing = get(id);
        assertEditable(existing);
        validatePartner(command.partnerId());
        String itemName = requireText(command.itemName(), "품목명");
        BigDecimal unitPrice = normalizeMoney(command.unitPrice(), "개별 단가");
        BigDecimal orderQty = normalizeQty(command.orderQty(), "수량");
        LocalDate deliveryDate = requireDate(command.requestedDeliveryDate(), "납기요구일");
        monthClosingService.assertTransactionOpen(existing.orderDate());

        if (orderQty.compareTo(existing.remainQty()) != 0) {
            throw new IllegalArgumentException("입고 이력이 없는 발주만 수정할 수 있습니다.");
        }

        orderRepository.update(id, new UpdateEtcPurchaseOrderCommand(
                itemName,
                command.partnerId(),
                unitPrice,
                orderQty,
                deliveryDate,
                command.categoryCodeId()
        ), actorUserId);
        orderRepository.updateRemainQtyAndStatus(id, orderQty, actorUserId);
        return get(id);
    }

    public void delete(long id, String actorUserId) {
        EtcPurchaseOrderView existing = get(id);
        assertEditable(existing);
        monthClosingService.assertTransactionOpen(existing.orderDate());
        orderRepository.softDelete(id, actorUserId);
    }

    public static EtcPurchaseOrderStatus resolveStatus(BigDecimal orderQty, BigDecimal remainQty) {
        if (remainQty.compareTo(orderQty) >= 0) {
            return EtcPurchaseOrderStatus.WAITING;
        }
        if (remainQty.compareTo(BigDecimal.ZERO) <= 0) {
            return EtcPurchaseOrderStatus.COMPLETED;
        }
        return EtcPurchaseOrderStatus.IN_PROGRESS;
    }

    public static BigDecimal lineAmount(BigDecimal qty, BigDecimal unitPrice) {
        return qty.multiply(unitPrice).setScale(2, RoundingMode.HALF_UP);
    }

    private void assertEditable(EtcPurchaseOrderView existing) {
        if (!existing.editable()) {
            throw new IllegalArgumentException("대기 상태의 발주만 수정·삭제할 수 있습니다.");
        }
    }

    private void validatePartner(long partnerId) {
        companyRepository.findActiveById(partnerId)
                .orElseThrow(() -> new IllegalArgumentException("거래처를 찾을 수 없습니다: " + partnerId));
        List<CompanyRoleType> roles = companyRepository.findRoles(partnerId);
        if (!roles.contains(CompanyRoleType.PURCHASE)) {
            throw new IllegalArgumentException("구매거래처만 선택할 수 있습니다.");
        }
    }

    private String nextOrderNo(LocalDate orderDate) {
        String prefix = "EPO-" + orderDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        long seq = orderRepository.countByOrderNoPrefix(prefix) + 1;
        return prefix + String.format("%03d", seq);
    }

    private static String requireText(String value, String label) {
        if (value == null || value.isBlank()) {
            throw new IllegalArgumentException(label + "은(는) 필수입니다.");
        }
        return value.trim();
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

    private static BigDecimal normalizeMoney(BigDecimal value, String label) {
        if (value == null || value.compareTo(BigDecimal.ZERO) < 0) {
            throw new IllegalArgumentException(label + "이(가) 올바르지 않습니다.");
        }
        return value.setScale(2, RoundingMode.HALF_UP);
    }
}
