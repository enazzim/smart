package com.shindong.smartmanager.api.web.bom;

import com.shindong.smartmanager.application.bom.ItemCompositionView;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record ItemCompositionResponse(
        long id,
        long parentItemId,
        String parentItemNo,
        String parentItemName,
        long childItemId,
        String childItemNo,
        String childItemName,
        BigDecimal parentQuantity,
        BigDecimal childQuantity,
        LocalDate beginDate,
        LocalDate endDate,
        Instant createdAt
) {
    static ItemCompositionResponse from(ItemCompositionView view) {
        return new ItemCompositionResponse(
                view.id(),
                view.parentItemId(),
                view.parentItemNo(),
                view.parentItemName(),
                view.childItemId(),
                view.childItemNo(),
                view.childItemName(),
                view.parentQuantity(),
                view.childQuantity(),
                view.beginDate(),
                view.endDate(),
                view.createdAt()
        );
    }
}
