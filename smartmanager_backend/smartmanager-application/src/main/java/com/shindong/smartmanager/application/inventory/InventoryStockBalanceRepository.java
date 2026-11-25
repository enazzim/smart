package com.shindong.smartmanager.application.inventory;

import java.math.BigDecimal;
import java.util.List;

public interface InventoryStockBalanceRepository {

    InventoryBalanceSlotView ensureBalance(InventoryBalanceKey key);

    InventoryBalanceSlotView saveBalance(InventoryBalanceSlotView balance);

    /** 회계연도 이하 슬롯 합. 슬롯을 만들지 않는다. */
    BigDecimal sumQtyThrough(InventoryBalanceKey key);

    /** 같은 논리 키의 회계연도 이하 슬롯을 회계연도, id 순으로 잠근다. */
    List<InventoryBalanceSlotView> lockSlotsThrough(InventoryBalanceKey key);
}
