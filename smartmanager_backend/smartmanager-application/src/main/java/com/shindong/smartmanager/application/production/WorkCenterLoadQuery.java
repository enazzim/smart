package com.shindong.smartmanager.application.production;

import java.time.LocalDate;

public record WorkCenterLoadQuery(
        Long workCenterId,
        LocalDate from,
        LocalDate to
) {
}
