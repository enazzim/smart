package com.shindong.smartmanager.api.web.inventory;

import com.shindong.smartmanager.application.inventory.MiscStockMovementPreviewView;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import java.math.BigDecimal;

public record MiscStockMovementPreviewResponse(
        long itemId,
        String itemNo,
        String itemName,
        PropertyClassification propertyClassification,
        String locationCode,
        String locationLabel,
        Long outputProcessId,
        Short outputProcessSequence,
        String outputProcessName,
        boolean processRequired,
        BigDecimal onHandQty,
        boolean lotTracked
) {
    public static MiscStockMovementPreviewResponse from(MiscStockMovementPreviewView view) {
        return new MiscStockMovementPreviewResponse(
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.propertyClassification(),
                view.locationCode(),
                view.locationLabel(),
                view.outputProcessId(),
                view.outputProcessSequence(),
                view.outputProcessName(),
                view.processRequired(),
                view.onHandQty(),
                view.lotTracked()
        );
    }
}
