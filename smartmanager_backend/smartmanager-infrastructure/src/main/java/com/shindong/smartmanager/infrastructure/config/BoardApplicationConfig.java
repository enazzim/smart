package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.board.BoardPostRepository;
import com.shindong.smartmanager.application.board.BoardPostService;
import com.shindong.smartmanager.application.user.UserRepository;
import java.util.Locale;
import java.util.Set;
import java.util.stream.Collectors;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
@EnableConfigurationProperties(BoardProperties.class)
public class BoardApplicationConfig {

    @Bean
    public BoardPostService boardPostService(
            BoardPostRepository boardPostRepository,
            com.shindong.smartmanager.application.board.BoardFileStorage boardFileStorage,
            UserRepository userRepository,
            BoardProperties boardProperties
    ) {
        Set<String> allowedExtensions = boardProperties.allowedExtensions().stream()
                .map(ext -> ext.toLowerCase(Locale.ROOT))
                .collect(Collectors.toUnmodifiableSet());
        return new BoardPostService(
                boardPostRepository,
                boardFileStorage,
                userRepository,
                boardProperties.maxFileSizeBytes(),
                allowedExtensions
        );
    }
}
