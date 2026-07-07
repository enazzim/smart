package com.shindong.smartmanager.application.board;

import java.util.List;

public record BoardReplyCommand(
        String content,
        List<BoardAttachmentInput> attachments
) {
}
