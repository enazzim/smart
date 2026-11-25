package com.shindong.smartmanager.application.inventory;

import java.math.BigDecimal;

public record StocktakeLotQty(long lotId, String lotNo, BigDecimal qty) {
}
