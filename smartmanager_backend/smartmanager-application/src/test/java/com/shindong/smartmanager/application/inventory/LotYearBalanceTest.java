package com.shindong.smartmanager.application.inventory;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;

import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.domain.inventory.LotGenealogyDirection;
import com.shindong.smartmanager.domain.inventory.LotGenealogyLinkType;
import com.shindong.smartmanager.domain.inventory.LotOriginType;
import com.shindong.smartmanager.domain.inventory.LotStatus;
import com.shindong.smartmanager.domain.inventory.StockMovementType;
import com.shindong.smartmanager.domain.item.CheckDistinction;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Tag;
import org.junit.jupiter.api.Test;

@Tag("core")
class LotYearBalanceTest {

    private static final long DECEMBER = 10L;
    private static final long JANUARY = 20L;

    private final Map<Long, Integer> years = new HashMap<>();
    private final Map<String, BigDecimal> qty = new HashMap<>();
    private LotInventoryService service;

    @BeforeEach
    void setUp() {
        years.put(DECEMBER, 2026);
        years.put(JANUARY, 2027);
        qty.put(key(1L, DECEMBER), new BigDecimal("100"));
        qty.put(key(1L, JANUARY), BigDecimal.ZERO);
        service = new LotInventoryService(lots(), items());
    }

    @Test
    void januaryOutboundKeepsDecemberLotRowAndAllowsNegativeJanuaryRow() {
        service.applyLotMovement(1L, 1L, JANUARY, StockMovementType.OUT, new BigDecimal("30"), "user");

        assertEquals(0, new BigDecimal("100").compareTo(qty.get(key(1L, DECEMBER))));
        assertEquals(0, new BigDecimal("-30").compareTo(qty.get(key(1L, JANUARY))));
        assertEquals(0, new BigDecimal("70").compareTo(sumThrough(JANUARY)));
    }

    @Test
    void reversingJanuaryTouchesOnlyTheJanuaryRow() {
        service.applyLotMovement(1L, 1L, JANUARY, StockMovementType.OUT, new BigDecimal("30"), "user");
        service.applyLotMovement(1L, 1L, JANUARY, StockMovementType.IN, new BigDecimal("30"), "user");

        assertEquals(0, new BigDecimal("100").compareTo(qty.get(key(1L, DECEMBER))));
        assertEquals(0, BigDecimal.ZERO.compareTo(qty.get(key(1L, JANUARY))));
    }

    @Test
    void outboundIsRejectedWhenTheCumulativeLotQtyWouldGoNegative() {
        assertThrows(IllegalArgumentException.class, () ->
                service.applyLotMovement(1L, 1L, JANUARY, StockMovementType.OUT, new BigDecimal("130"), "user"));
        assertEquals(0, new BigDecimal("100").compareTo(qty.get(key(1L, DECEMBER))));
        assertEquals(0, BigDecimal.ZERO.compareTo(qty.get(key(1L, JANUARY))));
    }

    private BigDecimal sumThrough(long balanceId) {
        int year = years.get(balanceId);
        return qty.entrySet().stream()
                .filter(entry -> entry.getKey().startsWith("1:"))
                .filter(entry -> years.get(balanceIdOf(entry.getKey())) <= year)
                .map(Map.Entry::getValue)
                .reduce(BigDecimal.ZERO, BigDecimal::add);
    }

    private static String key(long lotId, long balanceId) {
        return lotId + ":" + balanceId;
    }

    private static long balanceIdOf(String key) {
        return Long.parseLong(key.substring(key.indexOf(':') + 1));
    }

    private ItemRepository items() {
        ItemView item = new ItemView(
                1L, "LOT", "LOT", PropertyClassification.원자재, null, "EA", null, null,
                CheckDistinction.NONE, null, null, null, null, true, Instant.EPOCH
        );
        return new ItemRepository() {
            @Override
            public boolean existsActiveByItemNo(String itemNo) {
                throw new UnsupportedOperationException();
            }

            @Override
            public long save(com.shindong.smartmanager.application.item.ItemCommand command, String actorUserId) {
                throw new UnsupportedOperationException();
            }

            @Override
            public void update(long id, com.shindong.smartmanager.application.item.ItemUpdateCommand command, String actorUserId) {
                throw new UnsupportedOperationException();
            }

            @Override
            public void updateLotTracked(long id, boolean lotTracked, String actorUserId) {
                throw new UnsupportedOperationException();
            }

            @Override
            public void softDelete(long id, String actorUserId) {
                throw new UnsupportedOperationException();
            }

            @Override
            public List<ItemView> findAllActive(String itemNoQuery, String itemNameQuery) {
                throw new UnsupportedOperationException();
            }

            @Override
            public List<com.shindong.smartmanager.application.item.ItemLookupView> lookupActive(
                    String query, List<PropertyClassification> classifications, int limit
            ) {
                throw new UnsupportedOperationException();
            }

            @Override
            public Optional<ItemView> findActiveById(long id) {
                return Optional.of(item);
            }

            @Override
            public Optional<ItemView> findActiveByItemNo(String itemNo) {
                return Optional.empty();
            }
        };
    }

    private LotRepository lots() {
        LotView lot = new LotView(
                1L, 1L, "LOT", "LOT", "L1", LotStatus.ACTIVE, LotOriginType.ADJUSTMENT,
                null, null, null, null, null, null, null, Instant.EPOCH, null, List.of()
        );
        return new LotRepository() {
            @Override
            public Optional<LotView> findActiveById(long id) {
                return Optional.of(lot);
            }

            @Override
            public Optional<LotView> findActiveByItemIdAndLotNo(long itemId, String lotNo) {
                return Optional.of(lot);
            }

            @Override
            public List<LotView> findAllActive(LotListCriteria criteria) {
                return List.of(lot);
            }

            @Override
            public List<LotBalanceView> findBalancesByLotId(long lotId) {
                return List.of();
            }

            @Override
            public List<LotView> findAvailableLots(long itemId, String locationCode, Long outputProcessId) {
                return List.of();
            }

            @Override
            public LotView saveNew(long itemId, String lotNo, LotStatus status, LotOriginType originType, String originDocType, Long originDocId, String p1, String p2, LocalDate expiryDate, String certificateRef, String remark, String actorUserId) {
                throw new UnsupportedOperationException();
            }

            @Override
            public LotView update(long id, LotStatus status, String p1, String p2, LocalDate expiryDate, String certificateRef, String remark, String actorUserId) {
                return lot;
            }

            @Override
            public void softDelete(long id, String actorUserId) {
            }

            @Override
            public Optional<BigDecimal> findLotBalanceQty(long lotId, long inventoryBalanceId) {
                return Optional.ofNullable(qty.get(key(lotId, inventoryBalanceId)));
            }

            @Override
            public BigDecimal sumQtyThroughBalance(long lotId, long inventoryBalanceId) {
                return sumThrough(inventoryBalanceId);
            }

            @Override
            public BigDecimal sumQtyOnHand(long lotId, String locationCode, int fiscalYear, Long outputProcessId, Long inputProcessId, Long partnerId) {
                return sumThrough(JANUARY);
            }

            @Override
            public String allocateLotNo(long itemId, String itemNo, LocalDate sequenceDate) {
                return "L1";
            }

            @Override
            public void applyLotBalanceDelta(long lotId, long inventoryBalanceId, BigDecimal signedQty, String actorUserId) {
                qty.put(key(lotId, inventoryBalanceId), qty.getOrDefault(key(lotId, inventoryBalanceId), BigDecimal.ZERO).add(signedQty));
            }

            @Override
            public void refreshLotStatusFromBalances(long lotId, String actorUserId) {
            }

            @Override
            public void saveGenealogyLink(long parentLotId, long childLotId, LotGenealogyLinkType linkType, BigDecimal qty, Long stockMovementId, String sourceDocType, Long sourceDocId, String actorUserId) {
            }

            @Override
            public void softDeactivateGenealogyBySourceDoc(String sourceDocType, long sourceDocId) {
            }

            @Override
            public List<LotGenealogyLinkView> findGenealogy(long lotId, LotGenealogyDirection direction) {
                return List.of();
            }

            @Override
            public List<StockMovementListItemView> findMovementsByLotId(long lotId) {
                return List.of();
            }
        };
    }
}
