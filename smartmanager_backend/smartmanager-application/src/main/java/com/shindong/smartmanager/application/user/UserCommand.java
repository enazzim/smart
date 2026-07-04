package com.shindong.smartmanager.application.user;

import java.util.List;

public record UserCommand(
        String loginId,
        String password,
        String name,
        String contact,
        String email,
        List<Long> roleIds,
        Long workDiaryGroupId
) {
}
