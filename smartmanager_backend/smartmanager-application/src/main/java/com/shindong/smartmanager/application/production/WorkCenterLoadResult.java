package com.shindong.smartmanager.application.production;

import java.util.List;

public record WorkCenterLoadResult(
        double warnLoadThreshold,
        List<WorkCenterLoadDayView> days
) {
}
