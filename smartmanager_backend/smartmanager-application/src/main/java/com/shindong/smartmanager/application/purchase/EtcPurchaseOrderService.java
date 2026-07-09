package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.domain.company.CompanyRoleType;
import com.shindong.smartmanager.domain.purchase.EtcPurchaseOrderStatus;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.List;

public class EtcPurchaseOrderService {

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
