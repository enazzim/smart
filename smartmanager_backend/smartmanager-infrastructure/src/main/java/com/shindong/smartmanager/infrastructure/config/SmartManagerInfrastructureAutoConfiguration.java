package com.shindong.smartmanager.infrastructure.config;

import org.springframework.boot.autoconfigure.AutoConfiguration;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Import;

/**
 * API 모듈에서 infrastructure JAR의 설정·빈을 안정적으로 로드한다.
 * (멀티 모듈 bootRun 시 scanBasePackages만으로 누락되는 경우 방지)
 */
@AutoConfiguration
@Import(InfrastructureConfig.class)
@ComponentScan(basePackages = {
        "com.shindong.smartmanager.infrastructure.config",
        "com.shindong.smartmanager.infrastructure.persistence",
        "com.shindong.smartmanager.infrastructure.application",
        "com.shindong.smartmanager.infrastructure.security"
})
public class SmartManagerInfrastructureAutoConfiguration {
}
