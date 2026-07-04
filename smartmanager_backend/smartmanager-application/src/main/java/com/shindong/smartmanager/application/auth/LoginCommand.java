package com.shindong.smartmanager.application.auth;

import java.util.List;

public record LoginCommand(
        String loginId,
        String password
) {
}
