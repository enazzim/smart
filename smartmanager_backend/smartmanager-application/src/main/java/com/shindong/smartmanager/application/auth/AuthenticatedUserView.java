package com.shindong.smartmanager.application.auth;

import java.util.List;

public record AuthenticatedUserView(
        long id,
        String loginId,
        String name,
        List<String> roleCodes,
        List<String> authorities
) {
}
