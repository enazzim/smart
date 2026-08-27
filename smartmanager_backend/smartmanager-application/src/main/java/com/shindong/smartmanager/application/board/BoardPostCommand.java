package com.shindong.smartmanager.application.board;

import com.shindong.smartmanager.domain.board.BoardType;
import java.util.List;

public record BoardPostCommand(
        BoardType boardType,
        String title,
        String content,
        List<BoardAttachmentInput> attachments,
        List<Long> requiredReaderUserIds
) {
    public BoardPostCommand {
        if (attachments == null) {
            attachments = List.of();
        }
        if (requiredReaderUserIds == null) {
            requiredReaderUserIds = List.of();
        }
    }
}
