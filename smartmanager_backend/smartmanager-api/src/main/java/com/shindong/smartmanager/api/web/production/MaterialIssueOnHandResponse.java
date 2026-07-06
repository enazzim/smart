package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.application.production.MaterialIssueOnHandView;
import java.math.BigDecimal;

public record MaterialIssueOnHandResponse(
        long itemId,
        BigDecimal onHandQty
) {
    public static MaterialIssueOnHandResponse from(MaterialIssueOnHandView view) {
        return new MaterialIssueOnHandResponse(view.itemId(), view.onHandQty());
    }
}
