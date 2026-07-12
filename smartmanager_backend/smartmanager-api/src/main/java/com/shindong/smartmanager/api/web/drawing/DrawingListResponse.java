package com.shindong.smartmanager.api.web.drawing;

import com.shindong.smartmanager.application.drawing.DrawingListView;
import java.time.ZoneId;
import java.time.format.DateTimeFormatter;

public record DrawingListResponse(
        String id,
        String partNo,
        String partName,
        String modelGroup,
        Long itemId,
        String itemNo,
        Integer majorVersion,
        Integer minorVersion,
        String updatedAt,
        String drawingType
) {
    private static final DateTimeFormatter FORMATTER =
            DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm").withZone(ZoneId.of("Asia/Seoul"));

    public static DrawingListResponse from(DrawingListView view) {
        return new DrawingListResponse(
                view.id(),
                view.partNo(),
                view.partName(),
                view.modelGroup(),
                view.itemId(),
                view.itemNo(),
                view.majorVersion(),
                view.minorVersion(),
                FORMATTER.format(view.updatedAt()),
                view.drawingType().name()
        );
    }
}
