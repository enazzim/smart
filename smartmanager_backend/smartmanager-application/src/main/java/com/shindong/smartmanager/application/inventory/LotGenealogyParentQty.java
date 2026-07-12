package com.shindong.smartmanager.application.inventory;

import java.math.BigDecimal;

/** 작업실적 genealogy 부모 Lot(투입) + 수량. */
public record LotGenealogyParentQty(long parentLotId, BigDecimal qty) {
}
