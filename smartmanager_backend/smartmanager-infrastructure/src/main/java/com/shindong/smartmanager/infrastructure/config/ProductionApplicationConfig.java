package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.production.ProductionPlanRepository;
import com.shindong.smartmanager.application.production.ProductionPlanService;
import com.shindong.smartmanager.application.sales.SalesOrderRepository;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class ProductionApplicationConfig {

    @Bean
    public ProductionPlanService productionPlanService(
            ProductionPlanRepository productionPlanRepository,
            SalesOrderRepository salesOrderRepository
    ) {
        return new ProductionPlanService(productionPlanRepository, salesOrderRepository);
    }
}
