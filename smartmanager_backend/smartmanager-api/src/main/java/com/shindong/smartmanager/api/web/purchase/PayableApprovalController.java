package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.purchase.ApproveOffsetResultView;
import com.shindong.smartmanager.application.purchase.PayableApprovalCriteria;
import com.shindong.smartmanager.application.purchase.PayableApprovalItemCommand;
import com.shindong.smartmanager.application.purchase.UpdatePayableApprovalFiscalPeriodCommand;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;
import com.shindong.smartmanager.domain.purchase.PayableApprovalCategory;
import com.shindong.smartmanager.domain.purchase.PayableApprovalLedgerKind;
import com.shindong.smartmanager.infrastructure.application.PayableApprovalApplicationService;
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
import org.springframework.web.bind.annotation.PatchMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/purchase/payable-approvals")
public class PayableApprovalController {

    private final PayableApprovalApplicationService payableApprovalApplicationService;

    public PayableApprovalController(PayableApprovalApplicationService payableApprovalApplicationService) {
        this.payableApprovalApplicationService = payableApprovalApplicationService;
    }

    @GetMapping("/pending")
    @PreAuthorize("hasAuthority('purchase:payable-approval:read')")
    public List<PayableApprovalResponse> listPending(
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) String itemName,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateTo,
            @RequestParam(required = false) Integer fiscalYear,
            @RequestParam(required = false) Integer fiscalMonth,
            @RequestParam(required = false) PayableApprovalCategory category
    ) {
        return payableApprovalApplicationService.listPending(new PayableApprovalCriteria(
                partnerName, itemNo, itemName, receiptDateFrom, receiptDateTo, fiscalYear, fiscalMonth, category
        )).stream().map(PayableApprovalResponse::from).toList();
    }

    @GetMapping("/approved")
    @PreAuthorize("hasAuthority('purchase:payable-approval:read')")
    public List<PayableApprovalResponse> listApproved(
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) String itemName,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateTo,
            @RequestParam(required = false) Integer fiscalYear,
            @RequestParam(required = false) Integer fiscalMonth,
            @RequestParam(required = false) PayableApprovalCategory category
    ) {
        return payableApprovalApplicationService.listApproved(new PayableApprovalCriteria(
                partnerName, itemNo, itemName, receiptDateFrom, receiptDateTo, fiscalYear, fiscalMonth, category
        )).stream().map(PayableApprovalResponse::from).toList();
    }

    @PostMapping("/approve/preview")
    @PreAuthorize("hasAuthority('purchase:payable-approval:write')")
    public ApproveOffsetResultResponse preview(@Valid @RequestBody PayableApprovalBatchRequest request) {
        return ApproveOffsetResultResponse.from(payableApprovalApplicationService.preview(
                request.items().stream()
                        .map(item -> new PayableApprovalItemCommand(item.ledgerKind(), item.historyId()))
                        .toList()
        ));
    }

    @PostMapping("/approve")
    @PreAuthorize("hasAuthority('purchase:payable-approval:write')")
    public ApproveOffsetResultResponse approve(@Valid @RequestBody PayableApprovalBatchRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return ApproveOffsetResultResponse.from(payableApprovalApplicationService.approve(
                request.items().stream()
                        .map(item -> new PayableApprovalItemCommand(item.ledgerKind(), item.historyId()))
                        .toList(),
                principal.userId(),
                principal.loginId()
        ));
    }

    @PostMapping("/cancel-approval")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('purchase:payable-approval:write')")
    public void cancelApproval(@Valid @RequestBody PayableApprovalBatchRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        payableApprovalApplicationService.cancelApproval(
                request.items().stream()
                        .map(item -> new PayableApprovalItemCommand(item.ledgerKind(), item.historyId()))
                        .toList(),
                principal.userId(),
                principal.loginId()
        );
    }

    @PatchMapping("/fiscal-period")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('purchase:payable-approval:write')")
    public void updateFiscalPeriod(@Valid @RequestBody UpdatePayableApprovalFiscalPeriodRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        payableApprovalApplicationService.updateFiscalPeriod(
                new UpdatePayableApprovalFiscalPeriodCommand(
                        request.ledgerKind(),
                        request.historyId(),
                        request.fiscalYear(),
                        request.fiscalMonth()
                ),
                principal.loginId()
        );
    }

    public record PayableApprovalBatchRequest(
            @NotEmpty List<PayableApprovalItemRequest> items
    ) {
    }

    public record PayableApprovalItemRequest(
            @NotNull PayableApprovalLedgerKind ledgerKind,
            @NotNull Long historyId
    ) {
    }

    public record UpdatePayableApprovalFiscalPeriodRequest(
            @NotNull PayableApprovalLedgerKind ledgerKind,
            @NotNull Long historyId,
            @NotNull Integer fiscalYear,
            @NotNull Integer fiscalMonth
    ) {
    }

    public record ApproveOffsetResultResponse(
            int itemCount,
            BigDecimal totalApproveAmount,
            BigDecimal totalOffsetAmount,
            BigDecimal totalUnpaidIncrease,
            List<ApproveOffsetItemResponse> offsets
    ) {
        public static ApproveOffsetResultResponse from(ApproveOffsetResultView view) {
            return new ApproveOffsetResultResponse(
                    view.itemCount(),
                    view.totalApproveAmount(),
                    view.totalOffsetAmount(),
                    view.totalUnpaidIncrease(),
                    view.offsets().stream().map(ApproveOffsetItemResponse::from).toList()
            );
        }
    }

    public record ApproveOffsetItemResponse(
            PayableApprovalLedgerKind ledgerKind,
            long historyId,
            long partnerId,
            String partnerName,
            Long itemId,
            String itemNo,
            String itemName,
            PartnerPaymentCostCategory costCategory,
            String costCategoryLabel,
            BigDecimal approveAmount,
            BigDecimal offsetAmount,
            BigDecimal prepaidAfter,
            BigDecimal unpaidIncrease,
            boolean offsetApplicable
    ) {
        public static ApproveOffsetItemResponse from(ApproveOffsetResultView.ApproveOffsetItemView view) {
            return new ApproveOffsetItemResponse(
                    view.ledgerKind(),
                    view.historyId(),
                    view.partnerId(),
                    view.partnerName(),
                    view.itemId(),
                    view.itemNo(),
                    view.itemName(),
                    view.costCategory(),
                    view.costCategory() == null
                            ? null
                            : (view.costCategory() == PartnerPaymentCostCategory.PURCHASE ? "구매" : "외주"),
                    view.approveAmount(),
                    view.offsetAmount(),
                    view.prepaidAfter(),
                    view.unpaidIncrease(),
                    view.offsetApplicable()
            );
        }
    }
}
