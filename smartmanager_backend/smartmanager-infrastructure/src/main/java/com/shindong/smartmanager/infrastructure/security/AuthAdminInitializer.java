package com.shindong.smartmanager.infrastructure.security;

import com.shindong.smartmanager.application.auth.AuthUserRepository;
import com.shindong.smartmanager.application.security.PasswordHasher;
import com.shindong.smartmanager.infrastructure.config.SmartManagerSecurityProperties;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.boot.context.event.ApplicationReadyEvent;
import org.springframework.context.event.EventListener;
import org.springframework.stereotype.Component;

@Component
public class AuthAdminInitializer {

    private static final Logger log = LoggerFactory.getLogger(AuthAdminInitializer.class);

    private final AuthUserRepository authUserRepository;
    private final PasswordHasher passwordHasher;
    private final SmartManagerSecurityProperties properties;

    public AuthAdminInitializer(
            AuthUserRepository authUserRepository,
            PasswordHasher passwordHasher,
            SmartManagerSecurityProperties properties
    ) {
        this.authUserRepository = authUserRepository;
        this.passwordHasher = passwordHasher;
        this.properties = properties;
    }

    @EventListener(ApplicationReadyEvent.class)
    public void seedAdminIfMissing() {
        SmartManagerSecurityProperties.AdminSeed admin = properties.admin();
        if (!admin.enabled() || authUserRepository.existsActiveAdmin()) {
            return;
        }

        authUserRepository.createAdmin(
                admin.loginId(),
                passwordHasher.hash(admin.initialPassword()),
                "시스템 관리자"
        );
        log.info("Default admin user created: loginId={}", admin.loginId());
    }
}
