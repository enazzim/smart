package com.shindong.smartmanager.application.board;

import com.shindong.smartmanager.domain.board.BoardType;
import com.shindong.smartmanager.domain.board.PostKind;
import java.time.Instant;
import java.util.List;

public record BoardPostDetailView(
        long id,
        BoardType boardType,
        PostKind postKind,
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
        List<BoardAttachmentView> attachments,
        List<BoardPostDetailView> replies
) {
}
