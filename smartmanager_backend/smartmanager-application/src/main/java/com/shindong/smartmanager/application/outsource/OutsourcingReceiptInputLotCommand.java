package com.shindong.smartmanager.application.outsource;

public record OutsourcingReceiptInputLotCommand(
        long itemId,
        Long lotId
) {
}
