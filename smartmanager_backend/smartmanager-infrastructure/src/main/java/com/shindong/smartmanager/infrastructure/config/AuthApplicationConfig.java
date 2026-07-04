package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.auth.AuthService;
import com.shindong.smartmanager.application.auth.AuthUserRepository;
import com.shindong.smartmanager.application.auth.JwtTokenPort;
import com.shindong.smartmanager.application.security.PasswordHasher;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
@EnableConfigurationProperties(SmartManagerSecurityProperties.class)
public class AuthApplicationConfig {

    @Bean
    public AuthService authService(
            AuthUserRepository authUserRepository,
            PasswordHasher passwordHasher,
            JwtTokenPort jwtTokenPort
    ) {
        return new AuthService(authUserRepository, passwordHasher, jwtTokenPort);
    }
}
