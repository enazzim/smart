package com.shindong.smartmanager.application.item;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertFalse;
import static org.junit.jupiter.api.Assertions.assertThrows;
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

    @Test
    void fromImportLabel_mapsLegacyLabels() {
        assertEquals(PropertyClassification.부자재, PropertyClassification.fromImportLabel("소모품"));
        assertEquals(PropertyClassification.공정품, PropertyClassification.fromImportLabel("반제품"));
        assertEquals(PropertyClassification.원자재, PropertyClassification.fromImportLabel("원자재"));
    }

    @Test
    void fromImportLabel_rejectsUnknown() {
        assertThrows(IllegalArgumentException.class, () -> PropertyClassification.fromImportLabel("기타"));
    }
}
