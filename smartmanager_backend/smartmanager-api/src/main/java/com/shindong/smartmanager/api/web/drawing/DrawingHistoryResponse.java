package com.shindong.smartmanager.api.web.drawing;

import com.shindong.smartmanager.application.drawing.DrawingHistoryView;
import java.time.ZoneId;
import java.time.format.DateTimeFormatter;

public record DrawingHistoryResponse(
        String id,
        Integer majorVersion,
        Integer minorVersion,
        String isLatest,
        String changeType,
        String changeReason,
        String createdAt
) {
    private static final DateTimeFormatter FORMATTER =
            DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm").withZone(ZoneId.of("Asia/Seoul"));

    public static DrawingHistoryResponse from(DrawingHistoryView view) {
        return new DrawingHistoryResponse(
                view.id(),
                view.majorVersion(),
                view.minorVersion(),
                view.isLatest(),
                view.changeType(),
                view.changeReason(),
                FORMATTER.format(view.createdAt())
        );
    }
}
