package com.shindong.smartmanager.api.web.outsource;

import com.shindong.smartmanager.application.outsource.CreateOutsourcingOrderFromWorkPlanCommand;
import com.shindong.smartmanager.application.outsource.CreateOutsourcingOrderFromWorkPlanLineCommand;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderCommand;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderLineCommand;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderListCriteria;
import com.shindong.smartmanager.domain.outsource.OutsourcingOrderSourceType;
import com.shindong.smartmanager.domain.outsource.OutsourcingOrderStatus;
import com.shindong.smartmanager.infrastructure.application.OutsourcingOrderApplicationService;
import com.shindong.smartmanager.api.security.SecurityUtils;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotEmpty;
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
@RequestMapping("/api/v1/outsource/orders")
public class OutsourcingOrderController {

    private final OutsourcingOrderApplicationService outsourcingOrderApplicationService;

    public OutsourcingOrderController(OutsourcingOrderApplicationService outsourcingOrderApplicationService) {
        this.outsourcingOrderApplicationService = outsourcingOrderApplicationService;
    }

    @GetMapping
    @PreAuthorize("hasAuthority('outsource:order:read')")
    public List<OutsourcingOrderResponse> list(
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate orderDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate orderDateTo,
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) String orderNo,
            @RequestParam(required = false) OutsourcingOrderStatus status,
            @RequestParam(defaultValue = "true") boolean excludeCancelled
    ) {
        return outsourcingOrderApplicationService.list(new OutsourcingOrderListCriteria(
                orderDateFrom,
                orderDateTo,
                partnerName,
                orderNo,
                status,
                excludeCancelled
        )).stream().map(OutsourcingOrderResponse::from).toList();
    }

    @GetMapping("/work-plan-candidates")
    @PreAuthorize("hasAuthority('outsource:order:read')")
    public List<WorkPlanOutsourceCandidateResponse> listWorkPlanCandidates(
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate orderDate
    ) {
        return outsourcingOrderApplicationService.listWorkPlanCandidates(orderDate).stream()
                .map(WorkPlanOutsourceCandidateResponse::from)
                .toList();
    }

    @GetMapping("/next-order-no")
    @PreAuthorize("hasAuthority('outsource:order:read') or hasAuthority('outsource:order:write')")
    public NextOutsourcingOrderNoResponse nextOrderNo(
            @RequestParam @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate orderDate
    ) {
        return new NextOutsourcingOrderNoResponse(outsourcingOrderApplicationService.previewNextOrderNo(orderDate));
    }

    @GetMapping("/{id}")
    @PreAuthorize("hasAuthority('outsource:order:read')")
    public OutsourcingOrderResponse get(@PathVariable long id) {
        return OutsourcingOrderResponse.from(outsourcingOrderApplicationService.get(id));
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('outsource:order:write')")
    public OutsourcingOrderResponse create(@Valid @RequestBody OutsourcingOrderRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return OutsourcingOrderResponse.from(outsourcingOrderApplicationService.register(
                toCommand(request),
                principal.loginId()
        ));
    }

    @PostMapping("/from-work-plan")
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('outsource:order:write')")
    public OutsourcingOrderResponse createFromWorkPlan(@Valid @RequestBody CreateOutsourcingOrderFromWorkPlanRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return OutsourcingOrderResponse.from(outsourcingOrderApplicationService.registerFromWorkPlan(
                new CreateOutsourcingOrderFromWorkPlanCommand(
                        request.partnerId(),
                        request.orderDate(),
                        request.lines().stream()
                                .map(line -> new CreateOutsourcingOrderFromWorkPlanLineCommand(
                                        line.workPlanId(),
                                        line.beginProcessCodeId(),
                                        line.endProcessCodeId(),
                                        line.orderQty(),
                                        line.unitPrice(),
                                        line.requestedDeliveryDate()
                                ))
                                .toList()
                ),
                principal.loginId()
        ));
    }

    @PostMapping("/{id}/cancel")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('outsource:order:write')")
    public void cancel(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        outsourcingOrderApplicationService.cancel(id, principal.loginId());
    }

    private OutsourcingOrderCommand toCommand(OutsourcingOrderRequest request) {
        return new OutsourcingOrderCommand(
                request.orderNo(),
                request.partnerId(),
                request.orderDate(),
                request.sourceType() != null ? request.sourceType() : OutsourcingOrderSourceType.MANUAL,
                request.lines().stream()
                        .map(line -> new OutsourcingOrderLineCommand(
                                line.itemId(),
                                line.processSequenceId(),
                                line.beginProcessCodeId(),
                                line.endProcessCodeId(),
                                line.workPlanId(),
                                line.orderQty(),
                                line.unitPrice(),
                                line.requestedDeliveryDate()
                        ))
                        .toList()
        );
    }

    public record NextOutsourcingOrderNoResponse(String orderNo) {
    }

    public record OutsourcingOrderRequest(
            String orderNo,
            @NotNull Long partnerId,
            @NotNull LocalDate orderDate,
            OutsourcingOrderSourceType sourceType,
            @NotEmpty List<OutsourcingOrderLineRequest> lines
    ) {
    }

    public record OutsourcingOrderLineRequest(
            @NotNull Long itemId,
            @NotNull Long processSequenceId,
            @NotNull Long beginProcessCodeId,
            @NotNull Long endProcessCodeId,
            Long workPlanId,
            @NotNull BigDecimal orderQty,
            BigDecimal unitPrice,
            LocalDate requestedDeliveryDate
    ) {
    }

    public record CreateOutsourcingOrderFromWorkPlanRequest(
            @NotNull Long partnerId,
            @NotNull LocalDate orderDate,
            @NotEmpty List<CreateOutsourcingOrderFromWorkPlanLineRequest> lines
    ) {
    }

    public record CreateOutsourcingOrderFromWorkPlanLineRequest(
            @NotNull Long workPlanId,
            @NotNull Long beginProcessCodeId,
            @NotNull Long endProcessCodeId,
            @NotNull BigDecimal orderQty,
            BigDecimal unitPrice,
            LocalDate requestedDeliveryDate
    ) {
    }
}
