package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.application.production.MaterialIssueView;
import java.time.LocalDate;

public record MaterialIssueResponse(
        long id,
        String issueNum,
        long workOrderId,
        String orderNum,
        long itemId,
        String itemNo,
        String itemName,
        String processName,
        LocalDate issueDate,
        String status,
        String statusLabel,
        boolean cancellable,
        String createdAt,
        String createdBy
) {
    public static MaterialIssueResponse from(MaterialIssueView view) {
        return new MaterialIssueResponse(
                view.id(),
                view.issueNum(),
                view.workOrderId(),
                view.orderNum(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.processName(),
                view.issueDate(),
                view.status().name(),
                view.statusLabel(),
                view.cancellable(),
                view.createdAt() != null ? view.createdAt().toString() : null,
                view.createdBy()
        );
    }
}
