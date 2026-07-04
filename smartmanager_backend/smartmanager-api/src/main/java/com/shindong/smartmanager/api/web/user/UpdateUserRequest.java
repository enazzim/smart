package com.shindong.smartmanager.api.web.user;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.Size;
import java.util.List;

public record UpdateUserRequest(
        @NotBlank @Size(max = 100) String name,
        @Size(max = 100) String password,
        @Size(max = 50) String contact,
        @Size(max = 200) String email,
        @NotEmpty List<Long> roleIds,
        Long workDiaryGroupId
) {
}
