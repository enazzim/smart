package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.code.CodeGroupOptionsRepository;
import com.shindong.smartmanager.application.user.UserRepository;
import com.shindong.smartmanager.application.workdiary.WorkDiaryRepository;
import com.shindong.smartmanager.application.workdiary.WorkDiaryService;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class WorkDiaryApplicationConfig {

    @Bean
    public WorkDiaryService workDiaryService(
            WorkDiaryRepository workDiaryRepository,
            UserRepository userRepository,
            CodeGroupOptionsRepository codeGroupOptionsRepository
    ) {
        return new WorkDiaryService(workDiaryRepository, userRepository, codeGroupOptionsRepository);
    }
}

