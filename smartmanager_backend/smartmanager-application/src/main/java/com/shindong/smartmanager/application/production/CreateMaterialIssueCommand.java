package com.shindong.smartmanager.application.production;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record CreateMaterialIssueCommand(
        long workOrderId,
        LocalDate issueDate,
        List<MaterialIssueLineCommand> lines
) {
}
