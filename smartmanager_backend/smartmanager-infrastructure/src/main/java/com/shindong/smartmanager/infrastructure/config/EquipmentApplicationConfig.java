package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.equipment.EquipmentCategoryLookup;
import com.shindong.smartmanager.application.equipment.EquipmentRepository;
import com.shindong.smartmanager.application.equipment.EquipmentService;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.process.WorkCenterLookup;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class EquipmentApplicationConfig {

    @Bean
    public EquipmentService equipmentService(
            EquipmentRepository equipmentRepository,
            EquipmentCategoryLookup equipmentCategoryLookup,
            WorkCenterLookup workCenterLookup,
            DomainEventStore domainEventStore
    ) {
        return new EquipmentService(
                equipmentRepository,
                equipmentCategoryLookup,
                workCenterLookup,
                domainEventStore
        );
    }
}
