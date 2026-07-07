package com.shindong.smartmanager.application.board;

import com.shindong.smartmanager.domain.board.BoardType;
import com.shindong.smartmanager.domain.board.PostKind;
import java.time.Instant;
import java.util.List;

public record BoardPostSummaryView(
        long id,
        BoardType boardType,
        PostKind postKind,
        String title,
        String authorName,
        int viewCount,
        boolean pinned,
        boolean hasAttachment,
        Instant createdAt
) {
}
