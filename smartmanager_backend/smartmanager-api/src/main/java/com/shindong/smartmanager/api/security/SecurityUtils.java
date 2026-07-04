package com.shindong.smartmanager.api.security;

import com.shindong.smartmanager.api.security.JwtUserPrincipal;
import org.springframework.security.access.AccessDeniedException;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;

public final class SecurityUtils {

    private SecurityUtils() {
    }

    public static JwtUserPrincipal requirePrincipal() {
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
        if (authentication == null || !(authentication.getPrincipal() instanceof JwtUserPrincipal principal)) {
            throw new AccessDeniedException("인증이 필요합니다.");
        }
        return principal;
    }

    public static long requireUserId() {
        return requirePrincipal().userId();
    }
}
