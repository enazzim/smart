package com.shindong.smartmanager.application.user;

import java.util.List;

public record UserUpdateCommand(
        String name,
        String password,
        String contact,
        String email,
        List<Long> roleIds,
        Long workDiaryGroupId
) {
}
