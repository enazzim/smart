package com.shindong.smartmanager.application.inventory;

public record OutsourceStocktakeSlot(
        long companyId,
        String partnerNo,
        String partnerName,
        long itemId,
        String itemNo,
        String itemName,
        String propertyClassification,
        boolean lotTracked,
        Long inputProcessId,
        String processName,
        String error
) {
}
