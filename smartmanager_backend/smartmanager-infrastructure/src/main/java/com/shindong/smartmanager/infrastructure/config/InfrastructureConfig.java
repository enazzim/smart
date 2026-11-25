package com.shindong.smartmanager.infrastructure.config;

import org.springframework.boot.persistence.autoconfigure.EntityScan;
import org.springframework.context.annotation.Configuration;
import org.springframework.data.jpa.repository.config.EnableJpaRepositories;

@Configuration
@EnableJpaRepositories(basePackages = "com.shindong.smartmanager.infrastructure.persistence")
@EntityScan(basePackages = "com.shindong.smartmanager.infrastructure.persistence")
public class InfrastructureConfig {
}
