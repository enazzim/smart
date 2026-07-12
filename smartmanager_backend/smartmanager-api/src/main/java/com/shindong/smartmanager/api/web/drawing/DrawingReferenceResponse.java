package com.shindong.smartmanager.api.web.drawing;

import com.shindong.smartmanager.application.drawing.DrawingReferenceChildCommand;
import com.shindong.smartmanager.application.drawing.DrawingReferencePeerView;
import com.shindong.smartmanager.application.drawing.DrawingReferenceView;
import com.shindong.smartmanager.application.drawing.DrawingWhereUsedView;
import java.util.List;

public record DrawingReferenceResponse(
        String id,
        String parentHistoryId,
        String childHistoryId,
        String refRole,
        int sortOrder,
        String remark,
        DrawingReferencePeerResponse child
) {
    public static DrawingReferenceResponse from(DrawingReferenceView view) {
        return new DrawingReferenceResponse(
                view.id(),
                view.parentHistoryId(),
                view.childHistoryId(),
                view.refRole(),
                view.sortOrder(),
                view.remark(),
                DrawingReferencePeerResponse.from(view.child())
        );
    }

    public record DrawingReferencePeerResponse(
            String historyId,
            String masterId,
            String partNo,
            String partName,
            String drawingType,
            int majorVersion,
            int minorVersion,
            Long itemId,
            String itemNo
    ) {
        static DrawingReferencePeerResponse from(DrawingReferencePeerView peer) {
            return new DrawingReferencePeerResponse(
                    peer.historyId(),
                    peer.masterId(),
                    peer.partNo(),
                    peer.partName(),
                    peer.drawingType().name(),
                    peer.majorVersion(),
                    peer.minorVersion(),
                    peer.itemId(),
                    peer.itemNo()
            );
        }
    }

    public static DrawingWhereUsedResponse fromWhereUsed(DrawingWhereUsedView view) {
        return new DrawingWhereUsedResponse(
                view.id(),
                view.parentHistoryId(),
                view.childHistoryId(),
                view.refRole(),
                view.sortOrder(),
                view.remark(),
                DrawingReferencePeerResponse.from(view.parent())
        );
    }

    public record DrawingWhereUsedResponse(
            String id,
            String parentHistoryId,
            String childHistoryId,
            String refRole,
            int sortOrder,
            String remark,
            DrawingReferencePeerResponse parent
    ) {
    }

    public record ReplaceRequest(List<ChildRequest> children) {
        public List<DrawingReferenceChildCommand> toCommands() {
            if (children == null) {
                return List.of();
            }
            return children.stream()
                    .map(c -> new DrawingReferenceChildCommand(
                            c.childHistoryId(),
                            c.refRole(),
                            c.sortOrder() == null ? 0 : c.sortOrder(),
                            c.remark()
                    ))
                    .toList();
        }
    }

    public record ChildRequest(
            String childHistoryId,
            String refRole,
            Integer sortOrder,
            String remark
    ) {
    }
}
