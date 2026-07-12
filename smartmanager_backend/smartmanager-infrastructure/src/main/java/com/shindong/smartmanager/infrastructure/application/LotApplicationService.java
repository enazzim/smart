package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.inventory.CreateLotCommand;
import com.shindong.smartmanager.application.inventory.LotGenealogyLinkView;
import com.shindong.smartmanager.application.inventory.LotListCriteria;
import com.shindong.smartmanager.application.inventory.LotService;
import com.shindong.smartmanager.application.inventory.LotView;
import com.shindong.smartmanager.application.inventory.StockMovementListItemView;
import com.shindong.smartmanager.application.inventory.UpdateLotCommand;
import com.shindong.smartmanager.domain.inventory.LotGenealogyDirection;
import com.shindong.smartmanager.domain.inventory.LotOriginType;
import java.math.BigDecimal;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class LotApplicationService {

    private final LotService lotService;

    public LotApplicationService(LotService lotService) {
        this.lotService = lotService;
    }

    @Transactional(readOnly = true)
    public List<LotView> list(LotListCriteria criteria) {
        return lotService.list(criteria);
    }

    @Transactional(readOnly = true)
    public LotView get(long id) {
        return lotService.get(id);
    }

    @Transactional
    public LotView create(CreateLotCommand command, String actorUserId) {
        return lotService.create(command, actorUserId);
    }

    @Transactional
    public LotView update(long id, UpdateLotCommand command, String actorUserId) {
        return lotService.update(id, command, actorUserId);
    }

    @Transactional
    public void delete(long id, String actorUserId) {
        lotService.delete(id, actorUserId);
    }

    @Transactional(readOnly = true)
    public List<LotView> findAvailableLots(long itemId, String locationCode, Long outputProcessId) {
        return lotService.findAvailableLots(itemId, locationCode, outputProcessId);
    }

    @Transactional(readOnly = true)
    public List<LotGenealogyLinkView> findGenealogy(long lotId, LotGenealogyDirection direction) {
        return lotService.findGenealogy(lotId, direction);
    }

    @Transactional(readOnly = true)
    public List<StockMovementListItemView> findMovements(long lotId) {
        return lotService.findMovements(lotId);
    }

    @Transactional
    public LotView resolveOrCreate(long itemId, String lotNo, boolean autoGenerate, String actorUserId) {
        return lotService.resolveOrCreate(itemId, lotNo, autoGenerate, actorUserId);
    }

    @Transactional
    public LotView createOnReceipt(
            long itemId,
            String lotNo,
            LotOriginType originType,
            String originDocType,
            Long originDocId,
            String actorUserId
    ) {
        return lotService.createOnReceipt(itemId, lotNo, originType, originDocType, originDocId, actorUserId);
    }

    @Transactional
    public void assertSufficientLotQty(long lotId, long inventoryBalanceId, BigDecimal qty) {
        lotService.assertSufficientLotQty(lotId, inventoryBalanceId, qty);
    }
}
