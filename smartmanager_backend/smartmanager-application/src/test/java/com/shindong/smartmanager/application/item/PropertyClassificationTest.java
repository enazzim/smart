package com.shindong.smartmanager.application.item;

import static org.junit.jupiter.api.Assertions.assertFalse;
import static org.junit.jupiter.api.Assertions.assertTrue;

import com.shindong.smartmanager.domain.item.PropertyClassification;
import org.junit.jupiter.api.Test;

class PropertyClassificationTest {

    @Test
    void salesWarehouseAtProductionComplete_onlyProduct() {
        assertTrue(PropertyClassification.제품.salesWarehouseAtProductionComplete());
        assertFalse(PropertyClassification.공정품.salesWarehouseAtProductionComplete());
        assertFalse(PropertyClassification.상품.salesWarehouseAtProductionComplete());
    }

    @Test
    void shipmentFromWipFinalProcess_onlyProcessItem() {
        assertTrue(PropertyClassification.공정품.shipmentFromWipFinalProcess());
        assertFalse(PropertyClassification.제품.shipmentFromWipFinalProcess());
        assertFalse(PropertyClassification.상품.shipmentFromWipFinalProcess());
    }
}
