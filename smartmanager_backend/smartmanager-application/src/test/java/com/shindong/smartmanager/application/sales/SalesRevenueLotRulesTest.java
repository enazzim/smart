package com.shindong.smartmanager.application.sales;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNull;
import static org.junit.jupiter.api.Assertions.assertThrows;

import com.shindong.smartmanager.application.inventory.LotView;
import com.shindong.smartmanager.domain.inventory.LotStatus;
import java.util.List;
import org.junit.jupiter.api.Test;

class SalesRevenueLotRulesTest {

    @Test
    void usesShipmentLotWhenRequestOmitsLot() {
        LotView lot = lot(7L, 3L, "LOT-7", LotStatus.ACTIVE);
        assertEquals(7L, SalesRevenueService.resolveShipmentLot(3L, "T4710", true, 7L, null, lot));
    }

    @Test
    void usesShipmentLotWhenRequestMatches() {
        LotView lot = lot(7L, 3L, "LOT-7", LotStatus.ACTIVE);
        assertEquals(7L, SalesRevenueService.resolveShipmentLot(3L, "T4710", true, 7L, 7L, lot));
    }

    @Test
    void rejectsDifferentLot() {
        LotView lot = lot(7L, 3L, "LOT-7", LotStatus.ACTIVE);
        IllegalArgumentException ex = assertThrows(
                IllegalArgumentException.class,
                () -> SalesRevenueService.resolveShipmentLot(3L, "T4710", true, 7L, 8L, lot)
        );
        assertEquals("매출 Lot는 출고 Lot와 같아야 합니다: T4710", ex.getMessage());
    }

    @Test
    void keepsShipmentLotAfterTrackingIsTurnedOff() {
        LotView lot = lot(7L, 3L, "LOT-7", LotStatus.ACTIVE);
        assertEquals(7L, SalesRevenueService.resolveShipmentLot(3L, "T4710", false, 7L, null, lot));
    }

    @Test
    void rejectsBlockedShipmentLot() {
        LotView lot = lot(7L, 3L, "LOT-7", LotStatus.BLOCKED);
        IllegalArgumentException ex = assertThrows(
                IllegalArgumentException.class,
                () -> SalesRevenueService.resolveShipmentLot(3L, "T4710", true, 7L, null, lot)
        );
        assertEquals("차단된 Lot는 입출고할 수 없습니다: LOT-7", ex.getMessage());
    }

    @Test
    void rejectsMissingOrMismatchedShipmentLot() {
        assertThrows(
                IllegalArgumentException.class,
                () -> SalesRevenueService.resolveShipmentLot(3L, "T4710", true, 7L, null, null)
        );
        LotView otherItem = lot(7L, 9L, "LOT-7", LotStatus.ACTIVE);
        assertThrows(
                IllegalArgumentException.class,
                () -> SalesRevenueService.resolveShipmentLot(3L, "T4710", true, 7L, null, otherItem)
        );
    }

    @Test
    void rejectsTrackedItemWhoseShipmentHasNoLot() {
        IllegalArgumentException ex = assertThrows(
                IllegalArgumentException.class,
                () -> SalesRevenueService.resolveShipmentLot(3L, "T4710", true, null, 8L, null)
        );
        assertEquals("출고에 Lot가 없습니다. 출고를 취소하고 Lot를 지정해 다시 출고하세요: T4710", ex.getMessage());
    }

    @Test
    void untrackedShipmentWithoutLotIgnoresLot() {
        assertNull(SalesRevenueService.resolveShipmentLot(3L, "T4710", false, null, null, null));
        IllegalArgumentException ex = assertThrows(
                IllegalArgumentException.class,
                () -> SalesRevenueService.resolveShipmentLot(3L, "T4710", false, null, 8L, null)
        );
        assertEquals("Lot 비추적 품목은 Lot를 지정할 수 없습니다: T4710", ex.getMessage());
    }

    private static LotView lot(long id, long itemId, String lotNo, LotStatus status) {
        return new LotView(
                id, itemId, "T4710", "품목", lotNo, status,
                null, null, null, null, null, null, null, null, null, null, List.of()
        );
    }
}
