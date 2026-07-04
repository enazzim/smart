package com.shindong.smartmanager.api.security;

import java.util.List;

public record JwtUserPrincipal(
        long userId,
        String loginId,
        List<String> authorities
) {
}
