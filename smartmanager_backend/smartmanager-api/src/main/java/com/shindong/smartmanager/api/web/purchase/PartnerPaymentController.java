package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.purchase.CreatePartnerPaymentCommand;
import com.shindong.smartmanager.application.purchase.PartnerPaymentCandidateCriteria;
import com.shindong.smartmanager.application.purchase.PartnerPaymentLineCommand;
import com.shindong.smartmanager.application.purchase.PartnerPaymentListCriteria;
import com.shindong.smartmanager.application.purchase.PrepaidBalanceView;
import com.shindong.smartmanager.application.purchase.PrepaidOrderLineCandidateCriteria;
import com.shindong.smartmanager.application.purchase.PrepaidOrderLineCandidateView;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentKind;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentStatus;
import com.shindong.smartmanager.infrastructure.application.PartnerPaymentApplicationService;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotNull;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.HttpStatus;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/purchase/payments")
public class PartnerPaymentController {

    private final PartnerPaymentApplicationService partnerPaymentApplicationService;

    public PartnerPaymentController(PartnerPaymentApplicationService partnerPaymentApplicationService) {
        this.partnerPaymentApplicationService = partnerPaymentApplicationService;
    }

    @GetMapping("/candidates")
    @PreAuthorize("hasAuthority('purchase:payment:read')")
    public List<PartnerPaymentCandidateResponse> listCandidates(
            @RequestParam(required = false) String partnerName,
            @RequestParam(defaultValue = "false") boolean includeZeroUnpaid
    ) {
        return partnerPaymentApplicationService.listCandidates(
                        new PartnerPaymentCandidateCriteria(partnerName, includeZeroUnpaid))
                .stream()
                .map(PartnerPaymentCandidateResponse::from)
                .toList();
    }

    @GetMapping("/prepaid-balances")
    @PreAuthorize("hasAuthority('purchase:payment:read')")
    public List<PrepaidBalanceResponse> listPrepaidBalances(
            @RequestParam(required = false) Long partnerId,
            @RequestParam(required = false) PartnerPaymentCostCategory costCategory
    ) {
        return partnerPaymentApplicationService.listPrepaidBalances(partnerId, costCategory).stream()
                .map(PrepaidBalanceResponse::from)
                .toList();
    }

    @GetMapping("/order-line-candidates")
    @PreAuthorize("hasAuthority('purchase:payment:read')")
    public List<PrepaidOrderLineCandidateResponse> listOrderLineCandidates(
            @RequestParam(required = false) Long partnerId,
            @RequestParam(required = false) PartnerPaymentCostCategory costCategory,
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) String itemName,
            @RequestParam(required = false) String orderNo
    ) {
        return partnerPaymentApplicationService.listOrderLineCandidates(
                        new PrepaidOrderLineCandidateCriteria(partnerId, costCategory, itemNo, itemName, orderNo))
                .stream()
                .map(PrepaidOrderLineCandidateResponse::from)
                .toList();
    }

    @GetMapping
    @PreAuthorize("hasAuthority('purchase:payment:read')")
    public List<PartnerPaymentResponse> list(
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate paymentDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate paymentDateTo,
            @RequestParam(required = false) String paymentNo,
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) PartnerPaymentStatus status,
            @RequestParam(defaultValue = "true") boolean excludeCancelled
    ) {
        return partnerPaymentApplicationService.list(new PartnerPaymentListCriteria(
                paymentDateFrom,
                paymentDateTo,
                paymentNo,
                partnerName,
                status,
                excludeCancelled
        )).stream().map(PartnerPaymentResponse::from).toList();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('purchase:payment:write')")
    public PartnerPaymentResponse create(@Valid @RequestBody CreatePartnerPaymentRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        List<PartnerPaymentLineCommand> lines = request.lines() == null
                ? List.of()
                : request.lines().stream()
                        .map(line -> new PartnerPaymentLineCommand(
                                line.itemId(),
                                line.purchaseOrderLineId(),
                                line.outsourcingOrderLineId(),
                                line.supplyAmount(),
                                line.vatAmount()
                        ))
                        .toList();
        return PartnerPaymentResponse.from(partnerPaymentApplicationService.register(
                new CreatePartnerPaymentCommand(
                        request.partnerId(),
                        request.paymentDate(),
                        request.costCategory(),
                        request.paymentKind() != null ? request.paymentKind() : PartnerPaymentKind.NORMAL,
                        request.supplyAmount(),
                        request.vatAmount(),
                        request.paymentMethod(),
                        request.remark(),
                        lines
                ),
                principal.loginId()
        ));
    }

    @PostMapping("/{id}/cancel")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('purchase:payment:write')")
    public void cancel(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        partnerPaymentApplicationService.cancel(id, principal.loginId());
    }

    public record CreatePartnerPaymentRequest(
            @NotNull Long partnerId,
            @NotNull LocalDate paymentDate,
            @NotNull PartnerPaymentCostCategory costCategory,
            PartnerPaymentKind paymentKind,
            BigDecimal supplyAmount,
            BigDecimal vatAmount,
            String paymentMethod,
            String remark,
            List<CreatePartnerPaymentLineRequest> lines
    ) {
    }

    public record CreatePartnerPaymentLineRequest(
            @NotNull Long itemId,
            Long purchaseOrderLineId,
            Long outsourcingOrderLineId,
            @NotNull BigDecimal supplyAmount,
            BigDecimal vatAmount
    ) {
    }

    public record PrepaidBalanceResponse(
            long partnerId,
            String partnerName,
            long itemId,
            String itemNo,
            String itemName,
            PartnerPaymentCostCategory costCategory,
            String costCategoryLabel,
            BigDecimal prepaidIn,
            BigDecimal prepaidOut,
            BigDecimal prepaidRemaining
    ) {
        public static PrepaidBalanceResponse from(PrepaidBalanceView view) {
            return new PrepaidBalanceResponse(
                    view.partnerId(),
                    view.partnerName(),
                    view.itemId(),
                    view.itemNo(),
                    view.itemName(),
                    view.costCategory(),
                    view.costCategory() == PartnerPaymentCostCategory.PURCHASE ? "구매" : "외주",
                    view.prepaidIn(),
                    view.prepaidOut(),
                    view.prepaidRemaining()
            );
        }
    }

    public record PrepaidOrderLineCandidateResponse(
            PartnerPaymentCostCategory costCategory,
            String costCategoryLabel,
            long orderLineId,
            long orderId,
            String orderNo,
            short lineNo,
            long partnerId,
            String partnerName,
            long itemId,
            String itemNo,
            String itemName,
            LocalDate orderDate,
            BigDecimal orderAmount,
            BigDecimal prepaidLinkedAmount,
            BigDecimal remainingAmount
    ) {
        public static PrepaidOrderLineCandidateResponse from(PrepaidOrderLineCandidateView view) {
            return new PrepaidOrderLineCandidateResponse(
                    view.costCategory(),
                    view.costCategory() == PartnerPaymentCostCategory.PURCHASE ? "구매" : "외주",
                    view.orderLineId(),
                    view.orderId(),
                    view.orderNo(),
                    view.lineNo(),
                    view.partnerId(),
                    view.partnerName(),
                    view.itemId(),
                    view.itemNo(),
                    view.itemName(),
                    view.orderDate(),
                    view.orderAmount(),
                    view.prepaidLinkedAmount(),
                    view.remainingAmount()
            );
        }
    }
}
