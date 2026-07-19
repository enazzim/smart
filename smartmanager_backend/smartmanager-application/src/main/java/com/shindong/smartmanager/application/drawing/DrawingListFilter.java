package com.shindong.smartmanager.application.drawing;

import com.shindong.smartmanager.domain.drawing.DrawingLifecycleStage;

public record DrawingListFilter(
        DrawingLifecycleStage lifecycleStage,
        String historyQuery
) {
    public static DrawingListFilter empty() {
        return new DrawingListFilter(null, null);
    }

    public boolean hasLifecycleStage() {
        return lifecycleStage != null;
    }

    public boolean hasHistoryQuery() {
        return historyQuery != null && !historyQuery.isBlank();
    }
}
