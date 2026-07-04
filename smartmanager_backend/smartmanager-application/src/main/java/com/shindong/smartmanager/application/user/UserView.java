package com.shindong.smartmanager.application.user;

import java.time.Instant;
import java.util.List;

public record UserView(
        long id,
        String loginId,
        String name,
        String contact,
        String email,
        List<Long> roleIds,
        List<String> roleCodes,
        Long workDiaryGroupId,
        String workDiaryGroupName,
        Instant createdAt
) {
}
