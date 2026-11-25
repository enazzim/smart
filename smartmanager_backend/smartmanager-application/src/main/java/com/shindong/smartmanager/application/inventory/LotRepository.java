package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.domain.inventory.LotGenealogyDirection;
import com.shindong.smartmanager.domain.inventory.LotGenealogyLinkType;
import com.shindong.smartmanager.domain.inventory.LotOriginType;
import com.shindong.smartmanager.domain.inventory.LotStatus;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;
import java.util.Optional;

public interface LotRepository {

    Optional<LotView> findActiveById(long id);

    Optional<LotView> findActiveByItemIdAndLotNo(long itemId, String lotNo);

    List<LotView> findAllActive(LotListCriteria criteria);

    List<LotBalanceView> findBalancesByLotId(long lotId);

    List<LotView> findAvailableLots(long itemId, String locationCode, Long outputProcessId);

    LotView saveNew(
            long itemId,
            String lotNo,
            LotStatus status,
            LotOriginType originType,
            String originDocType,
            Long originDocId,
            String p1,
            String p2,
            LocalDate expiryDate,
            String certificateRef,
            String remark,
            String actorUserId
    );

    LotView update(
            long id,
            LotStatus status,
            String p1,
            String p2,
            LocalDate expiryDate,
            String certificateRef,
            String remark,
            String actorUserId
    );

    void softDelete(long id, String actorUserId);

    Optional<BigDecimal> findLotBalanceQty(long lotId, long inventoryBalanceId);

    /** 이 슬롯과 같은 논리 키의 회계연도 이하 Lot 잔량 합. 기록 대상 행은 바꾸지 않는다. */
    BigDecimal sumQtyThroughBalance(long lotId, long inventoryBalanceId);

    /**
     * 지정 창고·회계연·공정·거래처 슬롯에 있는 Lot 잔량 합.
     * 공정·거래처가 null이면 해당 컬럼이 NULL인 슬롯만 합산한다.
     */
    BigDecimal sumQtyOnHand(
            long lotId,
            String locationCode,
            int fiscalYear,
            Long outputProcessId,
            Long inputProcessId,
            Long partnerId
    );

    String allocateLotNo(long itemId, String itemNo, LocalDate sequenceDate);

    void applyLotBalanceDelta(
            long lotId,
            long inventoryBalanceId,
            BigDecimal signedQty,
            String actorUserId
    );

    void refreshLotStatusFromBalances(long lotId, String actorUserId);

    void saveGenealogyLink(
            long parentLotId,
            long childLotId,
            LotGenealogyLinkType linkType,
            BigDecimal qty,
            Long stockMovementId,
            String sourceDocType,
            Long sourceDocId,
            String actorUserId
    );

    void softDeactivateGenealogyBySourceDoc(String sourceDocType, long sourceDocId);

    List<LotGenealogyLinkView> findGenealogy(long lotId, LotGenealogyDirection direction);

    /** Lot에 연결된 stock_movement (recording_state=1), 일자·id DESC, 최대 500건. */
    List<StockMovementListItemView> findMovementsByLotId(long lotId);
}

