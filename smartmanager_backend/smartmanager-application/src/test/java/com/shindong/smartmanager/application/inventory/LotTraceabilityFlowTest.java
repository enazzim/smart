package com.shindong.smartmanager.application.inventory;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.MonthClosingRepository;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.closing.MonthClosingView;
import com.shindong.smartmanager.application.item.ItemCommand;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemUpdateCommand;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.system.SystemSettingService;
import com.shindong.smartmanager.application.system.SystemSettingView;
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
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.concurrent.atomic.AtomicLong;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

/**
 * LOT 체크리스트 E2E: 입고(RAW) → 투입(OUT) → 산출(SALES IN) → 출고(DELIVERY) → 매출(DELIVERY OUT).
 * 슬롯 집계와 Lot 잔량·genealogy가 동일 TX 경로({@link InventoryBalanceService})로 맞는지 검증한다.
 */
class LotTraceabilityFlowTest {

    private static final long RAW_ITEM_ID = 10L;
    private static final long FG_ITEM_ID = 20L;
    private static final LocalDate DAY = LocalDate.of(2026, 7, 12);

    private InMemoryLocationRepository locations;
    private InMemoryStockBalanceRepository balances;
    private InMemoryMovementRepository movements;
    private InMemoryMonthlyRepository monthly;
    private InMemoryLotRepository lots;
    private InMemoryItemRepository items;
    private InventoryBalanceService inventory;
    private LotService lotService;

    @BeforeEach
    void setUp() {
        locations = new InMemoryLocationRepository();
        locations.register("RAW", 1L);
        locations.register("SALES", 2L);
        locations.register("DELIVERY", 3L);

        balances = new InMemoryStockBalanceRepository();
        movements = new InMemoryMovementRepository();
        monthly = new InMemoryMonthlyRepository();
        lots = new InMemoryLotRepository();
        items = new InMemoryItemRepository();
        items.put(tracked(RAW_ITEM_ID, "RM-001", "원자재A", PropertyClassification.원자재));
        items.put(tracked(FG_ITEM_ID, "FG-001", "제품A", PropertyClassification.제품));

        SystemSettingService settings = new SystemSettingService(new InMemorySystemSettingRepository());
        MonthClosingService closing = new MonthClosingService(
                new InMemoryMonthClosingRepository(),
                new FiscalCalendarService()
        );
        LotInventoryService lotInventory = new LotInventoryService(lots, items);
        inventory = new InventoryBalanceService(
                locations,
                balances,
                movements,
                monthly,
                new FiscalCalendarService(),
                closing,
                settings,
                lotInventory
        );
        lotService = new LotService(lots, items);
    }

    @Test
    void purchaseConsumeProduceShipRevenue_lotBalancesAndGenealogy() {
        // 1) 구매입고: 원자재 Lot RAW IN
        LotView rawLot = lotService.createOnReceipt(
                RAW_ITEM_ID, "RM-001-20260712-001", LotOriginType.PURCHASE,
                "purchase_receipt", 100L, "admin");
        inventory.recordMovement(cmd(
                RAW_ITEM_ID, "RAW", StockMovementType.IN, "10",
                "PURCHASE_RECEIPT", 100L, null, rawLot.id()));

        assertEquals(bd("10"), slotQty(RAW_ITEM_ID, "RAW", null));
        assertEquals(bd("10"), lotQty(rawLot.id(), balances.findSlotId(RAW_ITEM_ID, locations.id("RAW"), null)));

        // 2) 작업실적 투입: RAW OUT
        inventory.recordMovement(cmd(
                RAW_ITEM_ID, "RAW", StockMovementType.OUT, "10",
                "WORK_REPORT_CONSUMPTION", 200L, null, rawLot.id()));
        assertEquals(bd("0"), slotQty(RAW_ITEM_ID, "RAW", null));
        assertEquals(bd("0"), lotQty(rawLot.id(), balances.findSlotId(RAW_ITEM_ID, locations.id("RAW"), null)));

        // 3) 작업실적 산출: 제품 Lot SALES IN + genealogy
        LotView fgLot = lotService.createOnProduction(
                FG_ITEM_ID, null, true, "work_report", 200L, "admin");
        inventory.recordMovement(cmd(
                FG_ITEM_ID, "SALES", StockMovementType.IN, "10",
                "WORK_REPORT", 200L, null, fgLot.id()));
        lotService.recordWorkReportGenealogy(
                200L,
                fgLot.id(),
                List.of(new LotGenealogyParentQty(rawLot.id(), bd("10"))),
                "admin"
        );

        assertEquals(bd("10"), slotQty(FG_ITEM_ID, "SALES", null));
        List<LotGenealogyLinkView> up = lotService.findGenealogy(fgLot.id(), LotGenealogyDirection.UP);
        assertTrue(up.stream().anyMatch(l -> l.linkType() == LotGenealogyLinkType.CONSUME
                && l.parentLotId() == rawLot.id()));
        assertTrue(up.stream().anyMatch(l -> l.linkType() == LotGenealogyLinkType.PRODUCE
                && l.parentLotId() == rawLot.id()));

        // 4) 영업출고: SALES → DELIVERY (동일 lot_id)
        inventory.recordMovement(cmd(
                FG_ITEM_ID, "SALES", StockMovementType.OUT, "10",
                "SALES_SHIPMENT", 300L, null, fgLot.id()));
        inventory.recordMovement(cmd(
                FG_ITEM_ID, "DELIVERY", StockMovementType.IN, "10",
                "SALES_SHIPMENT", 300L, null, fgLot.id()));

        assertEquals(bd("0"), slotQty(FG_ITEM_ID, "SALES", null));
        assertEquals(bd("10"), slotQty(FG_ITEM_ID, "DELIVERY", null));
        long salesBalanceId = balances.findSlotId(FG_ITEM_ID, locations.id("SALES"), null);
        long deliveryBalanceId = balances.findSlotId(FG_ITEM_ID, locations.id("DELIVERY"), null);
        assertEquals(bd("0"), lotQty(fgLot.id(), salesBalanceId));
        assertEquals(bd("10"), lotQty(fgLot.id(), deliveryBalanceId));

        // 5) 매출인식: DELIVERY OUT → Lot DEPLETED
        inventory.recordMovement(cmd(
                FG_ITEM_ID, "DELIVERY", StockMovementType.OUT, "10",
                "SALES_REVENUE", 400L, null, fgLot.id()));
        assertEquals(bd("0"), slotQty(FG_ITEM_ID, "DELIVERY", null));
        assertEquals(bd("0"), lotQty(fgLot.id(), deliveryBalanceId));
        assertEquals(LotStatus.DEPLETED, lots.findActiveById(fgLot.id()).orElseThrow().status());
    }

    private RecordStockMovementCommand cmd(
            long itemId,
            String location,
            StockMovementType type,
            String qty,
            String refType,
            long refId,
            Long processId,
            Long lotId
    ) {
        return new RecordStockMovementCommand(
                itemId,
                location,
                DAY,
                type,
                bd(qty),
                BigDecimal.ZERO,
                refType,
                refId,
                processId,
                null,
                null,
                lotId,
                "admin"
        );
    }

    private BigDecimal slotQty(long itemId, String locationCode, Long processId) {
        return inventory.currentStockQty(itemId, locationCode, DAY, processId, null, null);
    }

    private BigDecimal lotQty(long lotId, long balanceId) {
        return lots.findLotBalanceQty(lotId, balanceId).orElse(BigDecimal.ZERO);
    }

    private static BigDecimal bd(String v) {
        return new BigDecimal(v);
    }

    private static ItemView tracked(long id, String no, String name, PropertyClassification cls) {
        return new ItemView(
                id, no, name, cls, "STD", "EA", null, BigDecimal.ZERO,
                CheckDistinction.NONE, null, null, null, null, true, Instant.now()
        );
    }

    // --- in-memory stubs ---

    private static final class InMemoryItemRepository implements ItemRepository {
        private final Map<Long, ItemView> byId = new HashMap<>();

        void put(ItemView item) {
            byId.put(item.id(), item);
        }

        @Override
        public Optional<ItemView> findActiveById(long id) {
            return Optional.ofNullable(byId.get(id));
        }

        @Override
        public boolean existsActiveByItemNo(String itemNo) {
            return byId.values().stream().anyMatch(i -> i.itemNo().equals(itemNo));
        }

        @Override
        public long save(ItemCommand command, String actorUserId) {
            throw new UnsupportedOperationException();
        }

        @Override
        public void update(long id, ItemUpdateCommand command, String actorUserId) {
            throw new UnsupportedOperationException();
        }

        @Override
        public void softDelete(long id, String actorUserId) {
            throw new UnsupportedOperationException();
        }

        @Override
        public List<ItemView> findAllActive(String itemNoQuery, String itemNameQuery) {
            return List.copyOf(byId.values());
        }

        @Override
        public Optional<ItemView> findActiveByItemNo(String itemNo) {
            return byId.values().stream().filter(i -> i.itemNo().equals(itemNo)).findFirst();
        }
    }

    private static final class InMemoryLotRepository implements LotRepository {
        private final AtomicLong nextLotId = new AtomicLong(1);
        private final AtomicLong nextLinkId = new AtomicLong(1);
        private final Map<Long, LotView> lots = new HashMap<>();
        private final Map<String, BigDecimal> balances = new HashMap<>();
        private final List<LotGenealogyLinkView> genealogy = new ArrayList<>();

        private static String balKey(long lotId, long balanceId) {
            return lotId + ":" + balanceId;
        }

        @Override
        public Optional<LotView> findActiveById(long id) {
            return Optional.ofNullable(lots.get(id));
        }

        @Override
        public Optional<LotView> findActiveByItemIdAndLotNo(long itemId, String lotNo) {
            return lots.values().stream()
                    .filter(l -> l.itemId() == itemId && l.lotNo().equals(lotNo))
                    .findFirst();
        }

        @Override
        public List<LotView> findAllActive(LotListCriteria criteria) {
            return List.copyOf(lots.values());
        }

        @Override
        public List<LotBalanceView> findBalancesByLotId(long lotId) {
            List<LotBalanceView> result = new ArrayList<>();
            for (Map.Entry<String, BigDecimal> e : balances.entrySet()) {
                if (!e.getKey().startsWith(lotId + ":")) continue;
                long balanceId = Long.parseLong(e.getKey().substring(e.getKey().indexOf(':') + 1));
                result.add(new LotBalanceView(
                        balanceId, lotId, balanceId, "X", "X", null, null, null, e.getValue()));
            }
            return result;
        }

        @Override
        public List<LotView> findAvailableLots(long itemId, String locationCode, Long outputProcessId) {
            return lots.values().stream().filter(l -> l.itemId() == itemId).toList();
        }

        @Override
        public LotView saveNew(
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
        ) {
            long id = nextLotId.getAndIncrement();
            LotView view = new LotView(
                    id, itemId, "I" + itemId, "N" + itemId, lotNo, status, originType,
                    originDocType, originDocId, p1, p2, expiryDate, certificateRef, remark,
                    Instant.now(), null, List.of()
            );
            lots.put(id, view);
            return view;
        }

        @Override
        public LotView update(
                long id,
                LotStatus status,
                String p1,
                String p2,
                LocalDate expiryDate,
                String certificateRef,
                String remark,
                String actorUserId
        ) {
            LotView old = lots.get(id);
            LotView next = new LotView(
                    old.id(), old.itemId(), old.itemNo(), old.itemName(), old.lotNo(),
                    status, old.originType(), old.originDocType(), old.originDocId(),
                    p1, p2, expiryDate, certificateRef, remark, old.createdAt(), Instant.now(), old.balances()
            );
            lots.put(id, next);
            return next;
        }

        @Override
        public void softDelete(long id, String actorUserId) {
            lots.remove(id);
        }

        @Override
        public Optional<BigDecimal> findLotBalanceQty(long lotId, long inventoryBalanceId) {
            return Optional.ofNullable(balances.get(balKey(lotId, inventoryBalanceId)));
        }

        @Override
        public String allocateLotNo(long itemId, String itemNo, LocalDate sequenceDate) {
            return itemNo + "-" + sequenceDate.toString().replace("-", "") + "-001";
        }

        @Override
        public void applyLotBalanceDelta(
                long lotId,
                long inventoryBalanceId,
                BigDecimal signedQty,
                String actorUserId
        ) {
            String key = balKey(lotId, inventoryBalanceId);
            balances.put(key, balances.getOrDefault(key, BigDecimal.ZERO).add(signedQty));
        }

        @Override
        public void refreshLotStatusFromBalances(long lotId, String actorUserId) {
            LotView lot = lots.get(lotId);
            if (lot == null || lot.status() == LotStatus.BLOCKED) return;
            boolean hasQty = balances.entrySet().stream()
                    .filter(e -> e.getKey().startsWith(lotId + ":"))
                    .anyMatch(e -> e.getValue().compareTo(BigDecimal.ZERO) > 0);
            LotStatus next = hasQty ? LotStatus.ACTIVE : LotStatus.DEPLETED;
            if (lot.status() != next) {
                lots.put(lotId, new LotView(
                        lot.id(), lot.itemId(), lot.itemNo(), lot.itemName(), lot.lotNo(),
                        next, lot.originType(), lot.originDocType(), lot.originDocId(),
                        lot.p1(), lot.p2(), lot.expiryDate(), lot.certificateRef(), lot.remark(),
                        lot.createdAt(), Instant.now(), lot.balances()
                ));
            }
        }

        @Override
        public void saveGenealogyLink(
                long parentLotId,
                long childLotId,
                LotGenealogyLinkType linkType,
                BigDecimal qty,
                Long stockMovementId,
                String sourceDocType,
                Long sourceDocId,
                String actorUserId
        ) {
            LotView parent = lots.get(parentLotId);
            LotView child = lots.get(childLotId);
            genealogy.add(new LotGenealogyLinkView(
                    nextLinkId.getAndIncrement(),
                    parentLotId, parent.lotNo(), parent.itemId(), parent.itemNo(), parent.itemName(),
                    childLotId, child.lotNo(), child.itemId(), child.itemNo(), child.itemName(),
                    linkType, qty, stockMovementId, sourceDocType, sourceDocId, Instant.now()
            ));
        }

        @Override
        public void softDeactivateGenealogyBySourceDoc(String sourceDocType, long sourceDocId) {
            genealogy.removeIf(g -> sourceDocType.equals(g.sourceDocType())
                    && sourceDocId == g.sourceDocId());
        }

        @Override
        public List<LotGenealogyLinkView> findGenealogy(long lotId, LotGenealogyDirection direction) {
            if (direction == LotGenealogyDirection.UP) {
                return genealogy.stream().filter(g -> g.childLotId() == lotId).toList();
            }
            return genealogy.stream().filter(g -> g.parentLotId() == lotId).toList();
        }
    }

    private static final class InMemoryLocationRepository implements InventoryLocationQueryRepository {
        private final Map<String, Long> codes = new HashMap<>();

        void register(String code, long id) {
            codes.put(code, id);
        }

        long id(String code) {
            return codes.get(code);
        }

        @Override
        public Optional<Long> findActiveLocationIdByCode(String locationCode) {
            return Optional.ofNullable(codes.get(locationCode));
        }
    }

    private static final class InMemoryStockBalanceRepository implements InventoryStockBalanceRepository {
        private long nextId = 1;
        private final Map<Long, InventoryBalanceSlotView> store = new HashMap<>();

        @Override
        public InventoryBalanceSlotView ensureBalance(InventoryBalanceKey key) {
            return store.values().stream()
                    .filter(b -> b.itemId() == key.itemId()
                            && b.locationId() == key.locationId()
                            && b.fiscalYear() == key.fiscalYear()
                            && java.util.Objects.equals(b.outputProcessId(), key.outputProcessId())
                            && java.util.Objects.equals(b.inputProcessId(), key.inputProcessId())
                            && java.util.Objects.equals(b.partnerId(), key.partnerId()))
                    .findFirst()
                    .orElseGet(() -> {
                        InventoryBalanceSlotView created = new InventoryBalanceSlotView(
                                nextId++,
                                key.itemId(),
                                key.locationId(),
                                key.fiscalYear(),
                                key.outputProcessId(),
                                key.inputProcessId(),
                                key.partnerId(),
                                BigDecimal.ZERO,
                                BigDecimal.ZERO
                        );
                        store.put(created.id(), created);
                        return created;
                    });
        }

        @Override
        public InventoryBalanceSlotView saveBalance(InventoryBalanceSlotView balance) {
            store.put(balance.id(), balance);
            return balance;
        }

        long findSlotId(long itemId, long locationId, Long processId) {
            return store.values().stream()
                    .filter(b -> b.itemId() == itemId
                            && b.locationId() == locationId
                            && java.util.Objects.equals(b.outputProcessId(), processId))
                    .findFirst()
                    .orElseThrow()
                    .id();
        }
    }

    private static final class InMemoryMovementRepository implements StockMovementRepository {
        private long nextId = 1;
        private final List<StockMovementView> store = new ArrayList<>();

        @Override
        public StockMovementView save(StockMovementView movement) {
            StockMovementView saved = new StockMovementView(
                    nextId++,
                    movement.inventoryBalanceId(),
                    movement.itemId(),
                    movement.locationId(),
                    movement.fiscalYear(),
                    movement.fiscalMonth(),
                    movement.movementType(),
                    movement.qty(),
                    movement.amount(),
                    movement.referenceType(),
                    movement.referenceId(),
                    movement.movementDate(),
                    movement.lotId()
            );
            store.add(saved);
            return saved;
        }

        @Override
        public Optional<Long> findActiveLotIdByReference(String referenceType, long referenceId) {
            return store.stream()
                    .filter(m -> referenceType.equals(m.referenceType()) && m.referenceId() == referenceId)
                    .map(StockMovementView::lotId)
                    .filter(id -> id != null)
                    .findFirst();
        }
    }

    private static final class InMemoryMonthlyRepository implements InventoryBalanceMonthlyRepository {
        @Override
        public void applyMovement(
                long inventoryBalanceId,
                int monthNum,
                BigDecimal inQty,
                BigDecimal inAmount,
                BigDecimal outQty,
                BigDecimal outAmount,
                BigDecimal stockQtyDelta
        ) {
            // no-op for flow test
        }
    }

    private static final class InMemorySystemSettingRepository
            implements com.shindong.smartmanager.application.system.SystemSettingRepository {
        private final Map<String, SystemSettingView> store = new HashMap<>();

        @Override
        public Optional<SystemSettingView> findActiveByKey(String settingKey) {
            return Optional.ofNullable(store.get(settingKey));
        }

        @Override
        public void upsert(String settingKey, String valueJson, String actorUserId) {
            store.put(settingKey, new SystemSettingView(
                    settingKey, valueJson, Instant.now(), actorUserId));
        }

        @Override
        public List<SystemSettingView> findAllActive() {
            return List.copyOf(store.values());
        }
    }

    private static final class InMemoryMonthClosingRepository implements MonthClosingRepository {
        @Override
        public List<MonthClosingView> findAllClosed() {
            return List.of();
        }

        @Override
        public Optional<MonthClosingView> findClosed(int fiscalYear, int fiscalMonth) {
            return Optional.empty();
        }

        @Override
        public boolean isClosed(int fiscalYear, int fiscalMonth) {
            return false;
        }

        @Override
        public boolean hasAnyClosed() {
            return false;
        }

        @Override
        public MonthClosingView saveClose(int fiscalYear, int fiscalMonth, String closedBy, String closedById) {
            throw new UnsupportedOperationException();
        }

        @Override
        public Optional<MonthClosingView> findLatestClosed() {
            return Optional.empty();
        }

        @Override
        public void reopen(int fiscalYear, int fiscalMonth) {
            // no-op
        }
    }
}
