package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.auth.AuthUserRepository;
import com.shindong.smartmanager.application.bom.ItemCompositionService;
import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.company.CompanyService;
import com.shindong.smartmanager.application.equipment.EquipmentRepository;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemService;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessService;
import com.shindong.smartmanager.application.publiccode.PublicCodeRepository;
import com.shindong.smartmanager.application.system.imports.MasterDataImportService;
import com.shindong.smartmanager.application.unitprice.UnitPriceService;
import com.shindong.smartmanager.application.workcenter.WorkCenterRepository;
import com.shindong.smartmanager.application.workcenter.WorkCenterService;
import com.shindong.smartmanager.application.workstandard.WorkStandardService;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class SystemImportApplicationConfig {

    @Bean
    public MasterDataImportService masterDataImportService(
            CompanyService companyService,
            CompanyRepository companyRepository,
            ItemService itemService,
            ItemRepository itemRepository,
            ItemCompositionService itemCompositionService,
            WorkCenterService workCenterService,
            WorkCenterRepository workCenterRepository,
            ProcessService processService,
            ProcessRepository processRepository,
            WorkStandardService workStandardService,
            UnitPriceService unitPriceService,
            PublicCodeRepository publicCodeRepository,
            EquipmentRepository equipmentRepository,
            AuthUserRepository authUserRepository
    ) {
        return new MasterDataImportService(
                companyService,
                companyRepository,
                itemService,
                itemRepository,
                itemCompositionService,
                workCenterService,
                workCenterRepository,
                processService,
                processRepository,
                workStandardService,
                unitPriceService,
                publicCodeRepository,
                equipmentRepository,
                authUserRepository
        );
    }
}
