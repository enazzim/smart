package com.shindong.smartmanager.application.purchase;

import static org.junit.jupiter.api.Assertions.assertEquals;

import com.shindong.smartmanager.domain.purchase.EtcPurchaseOrderStatus;
import java.math.BigDecimal;
import org.junit.jupiter.api.Test;

class EtcPurchaseOrderServiceTest {

    @Test
    void resolvesWaitingWhenRemainEqualsOrder() {
        assertEquals(
                EtcPurchaseOrderStatus.WAITING,
                EtcPurchaseOrderService.resolveStatus(new BigDecimal("10"), new BigDecimal("10"))
        );
    }

    @Test
    void resolvesCompletedWhenRemainZero() {
        assertEquals(
                EtcPurchaseOrderStatus.COMPLETED,
                EtcPurchaseOrderService.resolveStatus(new BigDecimal("10"), BigDecimal.ZERO)
        );
    }

    @Test
    void resolvesInProgressWhenPartialRemain() {
        assertEquals(
                EtcPurchaseOrderStatus.IN_PROGRESS,
                EtcPurchaseOrderService.resolveStatus(new BigDecimal("10"), new BigDecimal("3"))
        );
    }
}
