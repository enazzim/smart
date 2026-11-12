package com.shindong.smartmanager.api.web.item;

import com.shindong.smartmanager.application.item.ItemLookupView;
import com.shindong.smartmanager.domain.item.PropertyClassification;

public record ItemLookupResponse(
        long id,
        String itemNo,
        String itemName,
        PropertyClassification propertyClassification,
        String modelType,
        boolean lotTracked
) {
    public static ItemLookupResponse from(ItemLookupView view) {
        return new ItemLookupResponse(
                view.id(),
                view.itemNo(),
                view.itemName(),
                view.propertyClassification(),
                view.modelType(),
                view.lotTracked()
        );
    }
}
