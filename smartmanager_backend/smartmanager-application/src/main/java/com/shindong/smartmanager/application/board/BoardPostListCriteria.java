package com.shindong.smartmanager.application.board;

import com.shindong.smartmanager.domain.board.BoardType;
import java.util.List;

public record BoardPostListCriteria(
        BoardType boardType,
        String keyword,
        int page,
        int size
) {
    public int offset() {
        return Math.max(page, 0) * Math.max(size, 1);
    }
}
