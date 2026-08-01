package com.shindong.smartmanager.api.web.board;

import com.shindong.smartmanager.application.board.BoardPostDetailView;
import java.time.Instant;
import java.util.List;

public record BoardPostDetailResponse(
        long id,
        String boardType,
        String postKind,
        Long parentPostId,
        Long threadRootId,
        String title,
        String content,
        long authorUserId,
        String authorName,
        int viewCount,
        boolean pinned,
        Instant createdAt,
        Instant updatedAt,
        List<BoardAttachmentResponse> attachments,
        List<BoardPostDetailResponse> replies,
        List<BoardPostReaderResponse> readers,
        boolean canEdit,
        boolean canDelete
) {
    public static BoardPostDetailResponse from(BoardPostDetailView view) {
        return new BoardPostDetailResponse(
                view.id(),
                view.boardType().name(),
                view.postKind().name(),
                view.parentPostId(),
                view.threadRootId(),
                view.title(),
                view.content(),
                view.authorUserId(),
                view.authorName(),
                view.viewCount(),
                view.pinned(),
                view.createdAt(),
                view.updatedAt(),
                view.attachments().stream().map(BoardAttachmentResponse::from).toList(),
                view.replies().stream().map(BoardPostDetailResponse::from).toList(),
                view.readers().stream().map(BoardPostReaderResponse::from).toList(),
                view.canEdit(),
                view.canDelete()
        );
    }
}
