package com.shindong.smartmanager.api.web.user;

import com.shindong.smartmanager.application.user.UserView;
import java.time.Instant;
import java.util.List;

public record UserResponse(
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
    static UserResponse from(UserView view) {
        return new UserResponse(
                view.id(),
                view.loginId(),
                view.name(),
                view.contact(),
                view.email(),
                view.roleIds(),
                view.roleCodes(),
                view.roleNames(),
                view.workDiaryGroupId(),
                view.workDiaryGroupName(),
                view.createdAt(),
                view.workDiaryApprover()
        );
    }
}
