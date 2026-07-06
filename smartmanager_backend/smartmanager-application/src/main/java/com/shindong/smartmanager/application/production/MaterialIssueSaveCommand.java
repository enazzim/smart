package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.domain.production.MaterialIssueStatus;
import java.math.BigDecimal;
import java.time.LocalDate;

public record MaterialIssueSaveCommand(
        String issueNum,
        long workOrderId,
        LocalDate issueDate,
        MaterialIssueStatus status
) {
}
