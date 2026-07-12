package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.drawing.DrawingRepository;
import com.shindong.smartmanager.application.drawing.DrawingRevisionNotifier;
import com.shindong.smartmanager.application.drawing.DrawingService;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemRepository;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
@EnableConfigurationProperties(DrawingProperties.class)
public class DrawingApplicationConfig {

    @Bean
    public DrawingService drawingService(
            DrawingRepository drawingRepository,
            DomainEventStore domainEventStore,
            ItemRepository itemRepository,
            DrawingRevisionNotifier drawingRevisionNotifier
    ) {
        return new DrawingService(
                drawingRepository,
                domainEventStore,
                itemRepository,
                drawingRevisionNotifier
        );
    }
}
