package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.code.CodeGroupOptionsRepository;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.role.RoleRepository;
import com.shindong.smartmanager.application.security.PasswordHasher;
import com.shindong.smartmanager.application.user.UserRepository;
import com.shindong.smartmanager.application.user.UserService;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class UserApplicationConfig {

    @Bean
    public UserService userService(
            UserRepository userRepository,
            RoleRepository roleRepository,
            CodeGroupOptionsRepository codeGroupOptionsRepository,
            PasswordHasher passwordHasher,
            DomainEventStore domainEventStore
    ) {
        return new UserService(
                userRepository,
                roleRepository,
                codeGroupOptionsRepository,
                passwordHasher,
                domainEventStore
        );
    }
}
