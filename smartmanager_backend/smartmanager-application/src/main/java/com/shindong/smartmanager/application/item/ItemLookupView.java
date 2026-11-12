package com.shindong.smartmanager.application.item;

import com.shindong.smartmanager.domain.item.PropertyClassification;

public record ItemLookupView(
        long id,
        String itemNo,
        String itemName,
        PropertyClassification propertyClassification,
        String modelType,
        boolean lotTracked
) {
}
