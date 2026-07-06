package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.domain.production.MaterialIssueStatus;
import java.time.Instant;
import java.time.LocalDate;

public record MaterialIssueView(
        long id,
        String issueNum,
        long workOrderId,
        String orderNum,
        long itemId,
        String itemNo,
        String itemName,
        String processName,
        LocalDate issueDate,
        MaterialIssueStatus status,
        String statusLabel,
        boolean cancellable,
        Instant createdAt,
        String createdBy
) {
}
