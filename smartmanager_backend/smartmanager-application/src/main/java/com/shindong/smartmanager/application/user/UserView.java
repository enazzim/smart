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
        List<String> roleNames,
        Long workDiaryGroupId,
        String workDiaryGroupName,
        Instant createdAt,
        boolean workDiaryApprover
) {
    public UserView withWorkDiaryApprover(boolean value) {
        return new UserView(
                id, loginId, name, contact, email, roleIds, roleCodes, roleNames,
                workDiaryGroupId, workDiaryGroupName, createdAt, value
        );
    }
}
