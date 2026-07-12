package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.bom.ItemCompositionRepository;
import com.shindong.smartmanager.application.bom.ItemCompositionService;
import com.shindong.smartmanager.application.calendar.WorkCenterCapaService;
import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.inventory.InventoryBalanceService;
import com.shindong.smartmanager.application.inventory.LotService;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.WipBalanceProjector;
import com.shindong.smartmanager.application.production.BomConsumptionCalculator;
import com.shindong.smartmanager.application.production.MrpRepository;
import com.shindong.smartmanager.application.production.MrpService;
import com.shindong.smartmanager.application.production.ProductionPlanRepository;
import com.shindong.smartmanager.application.production.ProductionPlanService;
import com.shindong.smartmanager.application.production.WorkCenterLoadQueryService;
import com.shindong.smartmanager.application.production.WorkOrderRepository;
import com.shindong.smartmanager.application.production.WorkOrderService;
import com.shindong.smartmanager.application.production.WorkPlanRepository;
import com.shindong.smartmanager.application.production.WorkPlanService;
import com.shindong.smartmanager.application.production.MaterialIssueInventoryService;
import com.shindong.smartmanager.application.production.MaterialIssueRepository;
import com.shindong.smartmanager.application.production.MaterialIssueService;
import com.shindong.smartmanager.application.production.WorkReportConsumptionInventoryService;
import com.shindong.smartmanager.application.production.WorkReportInventoryService;
import com.shindong.smartmanager.application.production.WorkReportRepository;
import com.shindong.smartmanager.application.production.WorkReportService;
import com.shindong.smartmanager.application.inventory.LotService;
import com.shindong.smartmanager.application.purchase.PurchaseOrderRepository;
import com.shindong.smartmanager.application.sales.SalesOrderRepository;
import com.shindong.smartmanager.application.system.SystemSettingRepository;
import com.shindong.smartmanager.application.system.SystemSettingService;
import com.shindong.smartmanager.application.workstandard.WorkStandardRepository;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class ProductionApplicationConfig {

    @Bean
    public ProductionPlanService productionPlanService(
            ProductionPlanRepository productionPlanRepository,
            SalesOrderRepository salesOrderRepository,
            ItemRepository itemRepository
    ) {
        return new ProductionPlanService(productionPlanRepository, salesOrderRepository, itemRepository);
    }

    @Bean
    public MrpService mrpService(
            MrpRepository mrpRepository,
            ProductionPlanRepository productionPlanRepository,
            ItemCompositionService itemCompositionService,
            ItemRepository itemRepository,
            PurchaseOrderRepository purchaseOrderRepository,
            SystemSettingService systemSettingService
    ) {
        return new MrpService(
                mrpRepository,
                productionPlanRepository,
                itemCompositionService,
                itemRepository,
                purchaseOrderRepository,
                systemSettingService
        );
    }

    @Bean
    public WorkPlanService workPlanService(
            WorkPlanRepository workPlanRepository,
            ProductionPlanRepository productionPlanRepository,
            ProcessRepository processRepository,
            WorkStandardRepository workStandardRepository,
            ItemCompositionService itemCompositionService,
            ItemRepository itemRepository
    ) {
        return new WorkPlanService(
                workPlanRepository,
                productionPlanRepository,
                processRepository,
                workStandardRepository,
                itemCompositionService,
                itemRepository
        );
    }

    @Bean
    public WorkCenterLoadQueryService workCenterLoadQueryService(
            WorkPlanRepository workPlanRepository,
            WorkCenterCapaService workCenterCapaService,
            SystemSettingRepository systemSettingRepository
    ) {
        return new WorkCenterLoadQueryService(
                workPlanRepository,
                workCenterCapaService,
                systemSettingRepository
        );
    }

    @Bean
    public WorkOrderService workOrderService(
            WorkOrderRepository workOrderRepository,
            WorkPlanRepository workPlanRepository
    ) {
        return new WorkOrderService(workOrderRepository, workPlanRepository);
    }

    @Bean
    public WorkReportInventoryService workReportInventoryService(
            InventoryBalanceService inventoryBalanceService,
            ProcessRepository processRepository,
            ItemRepository itemRepository,
            WipBalanceProjector wipBalanceProjector,
            LotService lotService
    ) {
        return new WorkReportInventoryService(
                inventoryBalanceService,
                processRepository,
                itemRepository,
                wipBalanceProjector,
                lotService
        );
    }

    @Bean
    public BomConsumptionCalculator bomConsumptionCalculator(
            ItemCompositionRepository itemCompositionRepository,
            ItemRepository itemRepository,
            ProcessRepository processRepository
    ) {
        return new BomConsumptionCalculator(itemCompositionRepository, itemRepository, processRepository);
    }

    @Bean
    public MaterialIssueInventoryService materialIssueInventoryService(
            InventoryBalanceService inventoryBalanceService,
            ItemRepository itemRepository,
            WipBalanceProjector wipBalanceProjector,
            BomConsumptionCalculator bomConsumptionCalculator
    ) {
        return new MaterialIssueInventoryService(
                inventoryBalanceService,
                itemRepository,
                wipBalanceProjector,
                bomConsumptionCalculator
        );
    }

    @Bean
    public WorkReportConsumptionInventoryService workReportConsumptionInventoryService(
            InventoryBalanceService inventoryBalanceService,
            ProcessRepository processRepository,
            ItemRepository itemRepository,
            WipBalanceProjector wipBalanceProjector,
            BomConsumptionCalculator bomConsumptionCalculator
    ) {
        return new WorkReportConsumptionInventoryService(
                inventoryBalanceService,
                processRepository,
                itemRepository,
                wipBalanceProjector,
                bomConsumptionCalculator
        );
    }

    @Bean
    public MaterialIssueService materialIssueService(
            MaterialIssueRepository materialIssueRepository,
            WorkOrderRepository workOrderRepository,
            WorkPlanRepository workPlanRepository,
            MonthClosingService monthClosingService,
            BomConsumptionCalculator bomConsumptionCalculator,
            MaterialIssueInventoryService materialIssueInventoryService,
            ItemCompositionRepository itemCompositionRepository,
            SystemSettingService systemSettingService
    ) {
        return new MaterialIssueService(
                materialIssueRepository,
                workOrderRepository,
                workPlanRepository,
                monthClosingService,
                bomConsumptionCalculator,
                materialIssueInventoryService,
                itemCompositionRepository,
                systemSettingService
        );
    }

    @Bean
    public WorkReportService workReportService(
            WorkReportRepository workReportRepository,
            WorkOrderRepository workOrderRepository,
            WorkPlanRepository workPlanRepository,
            ProductionPlanRepository productionPlanRepository,
            WorkReportInventoryService workReportInventoryService,
            WorkReportConsumptionInventoryService workReportConsumptionInventoryService,
            MonthClosingService monthClosingService,
            FiscalCalendarService fiscalCalendarService,
            BomConsumptionCalculator bomConsumptionCalculator,
            MaterialIssueRepository materialIssueRepository,
            ItemCompositionRepository itemCompositionRepository,
            SystemSettingService systemSettingService,
            LotService lotService
    ) {
        return new WorkReportService(
                workReportRepository,
                workOrderRepository,
                workPlanRepository,
                productionPlanRepository,
                workReportInventoryService,
                workReportConsumptionInventoryService,
                monthClosingService,
                fiscalCalendarService,
                bomConsumptionCalculator,
                materialIssueRepository,
                itemCompositionRepository,
                systemSettingService,
                lotService
        );
    }
}
