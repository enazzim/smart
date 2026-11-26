package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.code.CodeGroupOptionsRepository;
import com.shindong.smartmanager.application.menu.GroupMenuService;
import com.shindong.smartmanager.application.menu.WorkDiaryGroupMenuRepository;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class GroupMenuApplicationConfig {

    @Bean
    public GroupMenuService groupMenuService(
            CodeGroupOptionsRepository codeGroupOptionsRepository,
            WorkDiaryGroupMenuRepository workDiaryGroupMenuRepository
    ) {
        return new GroupMenuService(codeGroupOptionsRepository, workDiaryGroupMenuRepository);
    }
}
