package com.shindong.smartmanager.application.inventory;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.junit.jupiter.api.Assertions.assertTrue;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.MonthClosingRepository;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.closing.MonthClosingView;
import com.shindong.smartmanager.application.system.SystemSettingService;
import com.shindong.smartmanager.application.system.SystemSettingView;
import com.shindong.smartmanager.domain.inventory.StockMovementType;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

class InventoryBalanceServiceTest {

    private static final long RAW_LOCATION_ID = 1L;

    private InMemoryLocationRepository locationRepository;
    private InMemoryStockBalanceRepository balanceRepository;
    private InMemoryMovementRepository movementRepository;
    private InMemoryMonthlyRepository monthlyRepository;
    private InMemorySystemSettingRepository systemSettingRepository;
    private SystemSettingService systemSettingService;
    private InventoryBalanceService service;

    @BeforeEach
    void setUp() {
        locationRepository = new InMemoryLocationRepository();
        balanceRepository = new InMemoryStockBalanceRepository();
        movementRepository = new InMemoryMovementRepository();
        monthlyRepository = new InMemoryMonthlyRepository();
        systemSettingRepository = new InMemorySystemSettingRepository();
        systemSettingService = new SystemSettingService(systemSettingRepository);
        MonthClosingService monthClosingService = new MonthClosingService(
                new InMemoryMonthClosingRepository(),
                new FiscalCalendarService()
        );
        service = new InventoryBalanceService(
                locationRepository,
                balanceRepository,
                movementRepository,
                monthlyRepository,
                new FiscalCalendarService(),
                monthClosingService,
                systemSettingService
        );
        locationRepository.register("RAW", RAW_LOCATION_ID);
    }

    @Test
    void recordInCreatesBalanceAndMovement() {
        StockMovementView movement = service.recordMovement(new RecordStockMovementCommand(
                100L,
                "RAW",
                LocalDate.of(2026, 6, 10),
                StockMovementType.IN,
                new BigDecimal("50"),
                new BigDecimal("1000"),
                "PURCHASE_RECEIPT",
                1L,
                null,
                null,
                null,
                "admin"
        ));

        assertTrue(movement.id() > 0);
        assertEquals(StockMovementType.IN, movement.movementType());
        assertEquals(6, movement.fiscalMonth());

        InventoryBalanceSlotView balance = balanceRepository.findById(movement.inventoryBalanceId()).orElseThrow();
        assertEquals(new BigDecimal("50"), balance.stockQty());
        assertEquals(new BigDecimal("1000"), balance.stockAmount());

        InMemoryMonthlyRepository.MonthlySnapshot monthly = monthlyRepository.get(movement.inventoryBalanceId(), 6);
        assertEquals(new BigDecimal("50"), monthly.inQty());
        assertEquals(new BigDecimal("1000"), monthly.inAmount());
        assertEquals(BigDecimal.ZERO, monthly.outQty());
    }

    @Test
    void recordOutReducesBalance() {
        service.recordMovement(inCommand(new BigDecimal("30")));
        service.recordMovement(new RecordStockMovementCommand(
                100L,
                "RAW",
                LocalDate.of(2026, 6, 15),
                StockMovementType.OUT,
                new BigDecimal("10"),
                new BigDecimal("200"),
                "PRODUCTION_ISSUE",
                2L,
                null,
                null,
                null,
                "admin"
        ));

        InventoryBalanceSlotView balance = balanceRepository.findFirst().orElseThrow();
        assertEquals(new BigDecimal("20"), balance.stockQty());
        assertEquals(new BigDecimal("400"), balance.stockAmount());
    }

    @Test
    void recordOutThrowsWhenInsufficientStockAndNegativeNotAllowed() {
        setNegativeStockAllowed(false);
        service.recordMovement(inCommand(new BigDecimal("5")));

        IllegalStateException ex = assertThrows(
                IllegalStateException.class,
                () -> service.recordMovement(new RecordStockMovementCommand(
                        100L,
                        "RAW",
                        LocalDate.of(2026, 6, 15),
                        StockMovementType.OUT,
                        new BigDecimal("10"),
                        BigDecimal.ZERO,
                        "PRODUCTION_ISSUE",
                        3L,
                        null,
                        null,
                        null,
                        "admin"
                ))
        );
        assertTrue(ex.getMessage().contains("부족"));
    }

    @Test
    void recordOutAllowsNegativeStockWhenConfigured() {
        setNegativeStockAllowed(true);
        service.recordMovement(inCommand(new BigDecimal("5")));
        service.recordMovement(new RecordStockMovementCommand(
                100L,
                "RAW",
                LocalDate.of(2026, 6, 15),
                StockMovementType.OUT,
                new BigDecimal("10"),
                BigDecimal.ZERO,
                "PRODUCTION_ISSUE",
                3L,
                null,
                null,
                null,
                "admin"
        ));

        InventoryBalanceSlotView balance = balanceRepository.findFirst().orElseThrow();
        assertEquals(new BigDecimal("-5"), balance.stockQty());
    }

    private void setNegativeStockAllowed(boolean allowed) {
        systemSettingRepository.upsert(
                SystemSettingService.KEY_INVENTORY_ALLOW_NEGATIVE_STOCK,
                "\"" + (allowed ? "YES" : "NO") + "\"",
                "test"
        );
    }

    @Test
    void recordMovementBlockedWhenMonthClosed() {
        MonthClosingService closedMonthService = new MonthClosingService(
                new InMemoryMonthClosingRepository() {{
                    saveClose(2026, 6, "admin", "admin");
                }},
                new FiscalCalendarService()
        );
        InventoryBalanceService closedService = new InventoryBalanceService(
                locationRepository,
                balanceRepository,
                movementRepository,
                monthlyRepository,
                new FiscalCalendarService(),
                closedMonthService,
                systemSettingService
        );

        assertThrows(
                IllegalStateException.class,
                () -> closedService.recordMovement(inCommand(new BigDecimal("1")))
        );
    }

    @Test
    void fiscalCutoverDayMapsToNextMonth() {
        StockMovementView movement = service.recordMovement(new RecordStockMovementCommand(
                100L,
                "RAW",
                LocalDate.of(2026, 6, 26),
                StockMovementType.IN,
                new BigDecimal("1"),
                BigDecimal.ZERO,
                "PURCHASE_RECEIPT",
                4L,
                null,
                null,
                null,
                "admin"
        ));
        assertEquals(7, movement.fiscalMonth());
    }

    private RecordStockMovementCommand inCommand(BigDecimal qty) {
        return new RecordStockMovementCommand(
                100L,
                "RAW",
                LocalDate.of(2026, 6, 10),
                StockMovementType.IN,
                qty,
                qty.multiply(new BigDecimal("20")),
                "PURCHASE_RECEIPT",
                1L,
                null,
                null,
                null,
                "admin"
        );
    }

    private static final class InMemoryLocationRepository implements InventoryLocationQueryRepository {

        private final Map<String, Long> codes = new HashMap<>();

        void register(String code, long id) {
            codes.put(code, id);
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
                    .filter(b -> matches(b, key))
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

        Optional<InventoryBalanceSlotView> findById(long id) {
            return Optional.ofNullable(store.get(id));
        }

        Optional<InventoryBalanceSlotView> findFirst() {
            return store.values().stream().findFirst();
        }

        private static boolean matches(InventoryBalanceSlotView balance, InventoryBalanceKey key) {
            return balance.itemId() == key.itemId()
                    && balance.locationId() == key.locationId()
                    && balance.fiscalYear() == key.fiscalYear()
                    && nullableEquals(balance.outputProcessId(), key.outputProcessId())
                    && nullableEquals(balance.inputProcessId(), key.inputProcessId())
                    && nullableEquals(balance.partnerId(), key.partnerId());
        }

        private static boolean nullableEquals(Long left, Long right) {
            if (left == null) {
                return right == null;
            }
            return left.equals(right);
        }
    }

    private static final class InMemoryMovementRepository implements StockMovementRepository {

        private long nextId = 1;

        @Override
        public StockMovementView save(StockMovementView movement) {
            return new StockMovementView(
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
                    movement.movementDate()
            );
        }
    }

    private static final class InMemoryMonthlyRepository implements InventoryBalanceMonthlyRepository {

        private final Map<String, MonthlySnapshot> store = new HashMap<>();

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
            String key = inventoryBalanceId + ":" + monthNum;
            MonthlySnapshot current = store.getOrDefault(
                    key,
                    new MonthlySnapshot(BigDecimal.ZERO, BigDecimal.ZERO, BigDecimal.ZERO, BigDecimal.ZERO, BigDecimal.ZERO)
            );
            store.put(key, new MonthlySnapshot(
                    current.inQty().add(inQty),
                    current.inAmount().add(inAmount),
                    current.outQty().add(outQty),
                    current.outAmount().add(outAmount),
                    current.stockQty().add(stockQtyDelta)
            ));
        }

        MonthlySnapshot get(long balanceId, int month) {
            return store.getOrDefault(
                    balanceId + ":" + month,
                    new MonthlySnapshot(BigDecimal.ZERO, BigDecimal.ZERO, BigDecimal.ZERO, BigDecimal.ZERO, BigDecimal.ZERO)
            );
        }

        record MonthlySnapshot(
                BigDecimal inQty,
                BigDecimal inAmount,
                BigDecimal outQty,
                BigDecimal outAmount,
                BigDecimal stockQty
        ) {
        }
    }

    private static class InMemoryMonthClosingRepository implements MonthClosingRepository {

        private long nextId = 1;
        private final Map<String, MonthClosingView> store = new HashMap<>();

        @Override
        public List<MonthClosingView> findAllClosed() {
            return new ArrayList<>(store.values());
        }

        @Override
        public Optional<MonthClosingView> findClosed(int fiscalYear, int fiscalMonth) {
            return Optional.ofNullable(store.get(key(fiscalYear, fiscalMonth)));
        }

        @Override
        public boolean isClosed(int fiscalYear, int fiscalMonth) {
            return store.containsKey(key(fiscalYear, fiscalMonth));
        }

        @Override
        public boolean hasAnyClosed() {
            return !store.isEmpty();
        }

        @Override
        public MonthClosingView saveClose(int fiscalYear, int fiscalMonth, String closedBy, String closedById) {
            MonthClosingView view = new MonthClosingView(
                    nextId++,
                    fiscalYear,
                    fiscalMonth,
                    Instant.now(),
                    closedBy,
                    closedById
            );
            store.put(key(fiscalYear, fiscalMonth), view);
            return view;
        }

        @Override
        public Optional<MonthClosingView> findLatestClosed() {
            return store.values().stream()
                    .sorted((a, b) -> {
                        if (a.fiscalYear() != b.fiscalYear()) {
                            return Integer.compare(b.fiscalYear(), a.fiscalYear());
                        }
                        return Integer.compare(b.fiscalMonth(), a.fiscalMonth());
                    })
                    .findFirst();
        }

        @Override
        public void reopen(int fiscalYear, int fiscalMonth) {
            store.remove(key(fiscalYear, fiscalMonth));
        }

        private static String key(int fiscalYear, int fiscalMonth) {
            return fiscalYear + "-" + fiscalMonth;
        }
    }

    private static final class InMemorySystemSettingRepository
            implements com.shindong.smartmanager.application.system.SystemSettingRepository {

        private final Map<String, SystemSettingView> store = new HashMap<>();

        @Override
        public List<SystemSettingView> findAllActive() {
            return new ArrayList<>(store.values());
        }

        @Override
        public Optional<SystemSettingView> findActiveByKey(String settingKey) {
            return Optional.ofNullable(store.get(settingKey));
        }

        @Override
        public void upsert(String settingKey, String valueJson, String actorUserId) {
            store.put(settingKey, new SystemSettingView(
                    settingKey,
                    valueJson,
                    Instant.now(),
                    actorUserId
            ));
        }
    }
}
