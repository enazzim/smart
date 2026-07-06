package com.shindong.smartmanager.application.production;

import java.time.LocalDate;

public record MaterialIssueListCriteria(
        String itemNo,
        String itemName,
        String orderNum,
        String issueNum,
        LocalDate issueDateFrom,
        LocalDate issueDateTo
) {
}
