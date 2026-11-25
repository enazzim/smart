package com.shindong.smartmanager.infrastructure.persistence.inventory;

import com.shindong.smartmanager.application.bom.ItemCompositionRepository;
import com.shindong.smartmanager.application.inventory.OutsourceStocktakeCatalog;
import com.shindong.smartmanager.application.inventory.OutsourceStocktakePlanner;
import com.shindong.smartmanager.application.inventory.OutsourceStocktakeSlot;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.unitprice.UnitPriceRepository;
import com.shindong.smartmanager.domain.pricing.CostType;
import java.util.ArrayList;
import java.util.List;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaOutsourceStocktakeCatalog implements OutsourceStocktakeCatalog {

    private final UnitPriceRepository unitPriceRepository;
    private final OutsourceStocktakePlanner planner;

    public JpaOutsourceStocktakeCatalog(
            UnitPriceRepository unitPriceRepository,
            ProcessRepository processRepository,
            ItemCompositionRepository itemCompositionRepository,
            ItemRepository itemRepository
    ) {
        this.unitPriceRepository = unitPriceRepository;
        this.planner = new OutsourceStocktakePlanner(processRepository, itemCompositionRepository, itemRepository);
    }

    @Override
    @Transactional(readOnly = true)
    public List<OutsourceStocktakeSlot> list() {
        List<OutsourceStocktakeSlot> slots = new ArrayList<>();
        unitPriceRepository.findAllActiveByCostType(CostType.OUTSOURCE, "").stream()
                .flatMap(price -> planner.plan(price).stream())
                .forEach(slots::add);
        return slots;
    }
}
