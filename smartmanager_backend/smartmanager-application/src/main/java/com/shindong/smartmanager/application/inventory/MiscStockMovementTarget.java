package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.domain.item.PropertyClassification;

public record MiscStockMovementTarget(
        String locationCode,
        Long outputProcessId,
        Short outputProcessSequence,
        String outputProcessName,
        boolean processRequired
) {
    public String locationLabel() {
        return com.shindong.smartmanager.domain.inventory.InventoryLocationLabels.labelWithProcess(
                locationCode,
                outputProcessSequence != null ? outputProcessSequence.intValue() : null,
                outputProcessName
        );
    }

    public static MiscStockMovementTarget of(
            String locationCode,
            Long outputProcessId,
            Short outputProcessSequence,
            String outputProcessName,
            PropertyClassification classification
    ) {
        boolean processRequired = classification == PropertyClassification.공정품
                || classification == PropertyClassification.제품;
        return new MiscStockMovementTarget(
                locationCode,
                outputProcessId,
                outputProcessSequence,
                outputProcessName,
                processRequired
        );
    }
}
