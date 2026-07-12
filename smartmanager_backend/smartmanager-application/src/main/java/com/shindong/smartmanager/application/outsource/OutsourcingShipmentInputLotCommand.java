package com.shindong.smartmanager.application.outsource;

public record OutsourcingShipmentInputLotCommand(
        long itemId,
        Long lotId
) {
}
