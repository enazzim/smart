package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.unitprice.UnitPriceChangeLogView;
import com.shindong.smartmanager.application.unitprice.UnitPriceCommand;
import com.shindong.smartmanager.application.unitprice.UnitPriceService;
import com.shindong.smartmanager.application.unitprice.UnitPriceUpdateCommand;
import com.shindong.smartmanager.application.unitprice.UnitPriceView;
import com.shindong.smartmanager.domain.pricing.CostType;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class UnitPriceApplicationService {

    private final UnitPriceService unitPriceService;
    private final ItemRepository itemRepository;

    public UnitPriceApplicationService(UnitPriceService unitPriceService, ItemRepository itemRepository) {
        this.unitPriceService = unitPriceService;
        this.itemRepository = itemRepository;
    }

    @Transactional
    public UnitPriceView register(UnitPriceCommand command, String actorUserId) {
        return unitPriceService.register(command, actorUserId);
    }

    @Transactional
    public UnitPriceView register(
            com.shindong.smartmanager.domain.pricing.CostType type,
            String itemNum,
            long companyId,
            Long beginProcessCodeId,
            Long endProcessCodeId,
            java.math.BigDecimal orderRate,
            java.math.BigDecimal standardUnitCost,
            java.math.BigDecimal discountUnitCost,
            java.time.LocalDate beginDate,
            java.time.LocalDate endDate,
            String actorUserId
    ) {
        ItemView item = itemRepository.findActiveByItemNo(itemNum)
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + itemNum));
        UnitPriceCommand command = new UnitPriceCommand(
                type,
                item.id(),
                companyId,
                beginProcessCodeId,
                endProcessCodeId,
                orderRate,
                standardUnitCost,
                discountUnitCost,
                beginDate,
                endDate
        );
        return unitPriceService.register(command, actorUserId);
    }

    @Transactional
    public UnitPriceView update(long id, UnitPriceUpdateCommand command, String actorUserId) {
        return unitPriceService.update(id, command, actorUserId);
    }

    @Transactional
    public void delete(long id, String actorUserId) {
        unitPriceService.delete(id, actorUserId);
    }

    @Transactional(readOnly = true)
    public List<UnitPriceView> listActive(CostType costType, String query) {
        return unitPriceService.listActive(costType, query);
    }

    @Transactional(readOnly = true)
    public UnitPriceView getActive(long id) {
        return unitPriceService.getActive(id);
    }

    @Transactional(readOnly = true)
    public UnitPriceView getActiveByItemNo(String itemNo, CostType costType) {
        return unitPriceService.getActiveByItemNo(itemNo, costType);
    }

    @Transactional(readOnly = true)
    public List<UnitPriceChangeLogView> listHistory(long id) {
        return unitPriceService.listHistory(id);
    }
}
