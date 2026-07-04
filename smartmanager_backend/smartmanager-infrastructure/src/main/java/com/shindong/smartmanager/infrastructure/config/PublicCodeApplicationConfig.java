package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.publiccode.PublicCodeReferenceChecker;
import com.shindong.smartmanager.application.publiccode.PublicCodeRepository;
import com.shindong.smartmanager.application.publiccode.PublicCodeService;
import java.util.List;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class PublicCodeApplicationConfig {

    @Bean
    public PublicCodeService publicCodeService(
            PublicCodeRepository publicCodeRepository,
            List<PublicCodeReferenceChecker> referenceCheckers
    ) {
        return new PublicCodeService(publicCodeRepository, referenceCheckers);
    }
}
