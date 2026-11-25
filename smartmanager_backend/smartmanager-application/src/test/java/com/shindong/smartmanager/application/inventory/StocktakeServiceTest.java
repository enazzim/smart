package com.shindong.smartmanager.application.inventory;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNull;
import static org.junit.jupiter.api.Assertions.assertTrue;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessView;
import com.shindong.smartmanager.domain.item.CheckDistinction;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import com.shindong.smartmanager.domain.process.ProcessVariant;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Tag;
import org.junit.jupiter.api.Test;

@Tag("core")
class StocktakeServiceTest {

    private static final LocalDate STOCKTAKE_DATE = LocalDate.of(2026, 12, 20);

    private final Map<String, ItemView> items = new HashMap<>();
    private final Map<Long, ProcessView> processes = new HashMap<>();
    private final Map<String, BigDecimal> books = new HashMap<>();
    private final Map<String, Boolean> history = new HashMap<>();
    private final Map<String, Boolean> later = new HashMap<>();
    private final List<OutsourceStocktakeSlot> outsourceSlots = new ArrayList<>();
    private final List<StocktakeLotQty> lotBalances = new ArrayList<>();
    private StocktakeService service;

    @BeforeEach
    void setUp() {
        register("RM-1", 1L, PropertyClassification.원자재, false);
        register("SUB-1", 2L, PropertyClassification.부자재, false);
        register("LOT-1", 3L, PropertyClassification.원자재, true);
        register("FG-1", 4L, PropertyClassification.제품, false);
        register("WIP-1", 5L, PropertyClassification.공정품, false);
        processes.put(50L, process(50L, 4L, (short) 10));
        processes.put(60L, process(60L, 5L, (short) 10));
        processes.put(61L, process(61L, 5L, (short) 20));
        service = new StocktakeService(
                itemRepository(),
                new MiscStockMovementResolver(processRepository()),
                new InventoryBalanceService(
                        locationRepository(),
                        balanceRepository(),
                        null,
                        null,
                        new FiscalCalendarService(),
                        null,
                        null
                ),
                locationRepository(),
                new FiscalCalendarService(),
                historyQuery(),
                bookQuery(),
                () -> outsourceSlots,
                lotQuery()
        );
    }

    @Test
    void openingStockWhenThereIsNoHistoryAndBookIsZero() {
        StocktakePreviewLine line = one(input(2, "RM-1", null, null, null, new BigDecimal("10"), null));
        assertEquals("IN", line.action());
        assertEquals("기초재고", line.reasonName());
        assertEquals(0, new BigDecimal("10").compareTo(line.movementQty()));
        assertEquals("RAW", line.locationCode());
        assertNull(line.error());
    }

    @Test
    void adjustmentUsesOnlyTheDifferenceWhenHistoryExists() {
        books.put(bookKey(1L, "RAW", null), new BigDecimal("10"));
        history.put(historyKey(1L, 1L, null), true);
        StocktakePreviewLine line = one(input(2, "RM-1", 99L, null, null, new BigDecimal("12"), null));
        assertEquals("IN", line.action());
        assertEquals("실사조정", line.reasonName());
        assertEquals(0, new BigDecimal("2").compareTo(line.movementQty()));
    }

    @Test
    void explicitZeroWithHistoryPostsTheWholeBookAsOutbound() {
        books.put(bookKey(1L, "RAW", null), new BigDecimal("8"));
        history.put(historyKey(1L, 1L, null), true);
        StocktakePreviewLine line = one(input(2, "RM-1", null, null, null, BigDecimal.ZERO, null));
        assertEquals("OUT", line.action());
        assertEquals(0, new BigDecimal("8").compareTo(line.movementQty()));
    }

    @Test
    void emptyCountedQuantityIsSkippedEvenForLotTrackedItems() {
        StocktakePreviewLine line = one(input(2, "LOT-1", null, null, "L1", null, null));
        assertEquals("SKIP", line.action());
        assertNull(line.error());
    }

    @Test
    void filledQuantityOnLotTrackedItemIsRejected() {
        StocktakePreviewLine line = one(input(2, "LOT-1", null, null, null, BigDecimal.ONE, null));
        assertEquals("ERROR", line.action());
        assertTrue(line.error().contains("Lot"));
    }

    @Test
    void lotNumberOnNonTrackedItemIsRejected() {
        StocktakePreviewLine line = one(input(2, "RM-1", null, null, "L1", BigDecimal.ONE, null));
        assertEquals("ERROR", line.action());
    }

    @Test
    void partnerOnInhouseRowIsRejected() {
        StocktakePreviewLine line = one(input(2, "RM-1", null, "P1", null, BigDecimal.ONE, null));
        assertEquals("ERROR", line.action());
        assertTrue(line.error().contains("거래처"));
    }

    @Test
    void subsidiaryItemIsRejected() {
        StocktakePreviewLine line = one(input(2, "SUB-1", null, null, null, BigDecimal.ONE, null));
        assertEquals("ERROR", line.action());
        assertTrue(line.error().contains("부자재"));
    }

    @Test
    void duplicateUsesResolvedWarehouseAndProcess() {
        List<StocktakePreviewLine> lines = service.preview(STOCKTAKE_DATE, List.of(
                input(2, "RM-1", 11L, null, null, BigDecimal.ONE, null),
                input(3, "RM-1", 22L, null, null, new BigDecimal("2"), null)
        ));
        assertNull(lines.get(0).error());
        assertTrue(lines.get(1).error().contains("중복"));
    }

    @Test
    void noHistoryWithNonZeroBookIsRejected() {
        books.put(bookKey(1L, "RAW", null), new BigDecimal("5"));
        StocktakePreviewLine line = one(input(2, "RM-1", null, null, null, new BigDecimal("5"), null));
        assertEquals("ERROR", line.action());
        assertTrue(line.error().contains("이력"));
    }

    @Test
    void laterMovementInsideSummedYearsIsRejected() {
        later.put(historyKey(1L, 1L, null), true);
        StocktakePreviewLine line = one(input(2, "RM-1", null, null, null, BigDecimal.ONE, null));
        assertTrue(line.error().contains("이후"));
    }

    @Test
    void changedBookQuantityRollsTheRowBack() {
        StocktakePreviewLine line = one(input(2, "RM-1", null, null, null, BigDecimal.ONE, new BigDecimal("9")));
        assertTrue(line.error().contains("장부"));
    }

    @Test
    void finalProductProcessResolvesToSales() {
        StocktakePreviewLine line = one(input(2, "FG-1", 50L, null, null, new BigDecimal("3"), null));
        assertEquals("SALES", line.locationCode());
        assertNull(line.outputProcessId());
        assertEquals("기초재고", line.reasonName());
    }

    @Test
    void wipKeepsTheSelectedProcess() {
        StocktakePreviewLine line = one(input(2, "WIP-1", 60L, null, null, new BigDecimal("4"), null));
        assertEquals("WIP", line.locationCode());
        assertEquals(60L, line.outputProcessId());
    }

    @Test
    void newLotIsOpeningStockWhenSlotAndLotSumsMatch() {
        StocktakePreviewLine line = one(input(2, "LOT-1", null, null, "NEW-1", new BigDecimal("5"), null));
        assertEquals("IN", line.action());
        assertEquals("기초재고", line.reasonName());
        assertEquals(0, new BigDecimal("5").compareTo(line.movementQty()));
    }

    @Test
    void lotRowIsRejectedWhenSlotSumDiffersFromLotSum() {
        books.put(bookKey(3L, "RAW", null), new BigDecimal("5"));
        lotBalances.add(new StocktakeLotQty(9L, "OLD-1", BigDecimal.ZERO));
        StocktakePreviewLine line = one(input(2, "LOT-1", null, null, "OLD-1", new BigDecimal("5"), null));
        assertEquals("ERROR", line.action());
        assertTrue(line.error().contains("Lot 합"));
    }

    @Test
    void outsourceRowUsesTheUnitPriceSlot() {
        outsourceSlots.add(new OutsourceStocktakeSlot(
                7L, "123-45-67890", "외주처", 1L, "RM-1", "RM-1", "원자재",
                false, 70L, "투입", null
        ));
        StocktakePreviewLine line = one(input(2, "RM-1", 70L, "1234567890", null, new BigDecimal("4"), null));
        assertEquals("OUTSOURCE", line.locationCode());
        assertEquals(70L, line.inputProcessId());
        assertEquals("기초재고", line.reasonName());
    }

    @Test
    void bookIncludesRawMaterialThatHasNoBalanceAndDoesNotCreateASlot() {
        List<StocktakeBookLine> rows = service.book(STOCKTAKE_DATE);
        StocktakeBookLine raw = rows.stream().filter(row -> "RM-1".equals(row.itemNo()) && "RAW".equals(row.locationCode())).findFirst().orElseThrow();
        assertEquals(0, raw.bookQty().signum());
        assertNull(raw.error());
        assertTrue(rows.stream().noneMatch(row -> "SUB-1".equals(row.itemNo())));
        assertTrue(books.isEmpty());
    }

    @Test
    void previewDoesNotCreateABalanceSlot() {
        one(input(2, "RM-1", null, null, null, BigDecimal.ONE, null));
        assertTrue(books.isEmpty());
    }

    private StocktakePreviewLine one(StocktakeLineInput input) {
        return service.preview(STOCKTAKE_DATE, List.of(input)).get(0);
    }

    private void register(String itemNo, long id, PropertyClassification classification, boolean lotTracked) {
        items.put(itemNo, new ItemView(
                id, itemNo, itemNo, classification, null, "EA", null, null,
                CheckDistinction.NONE, null, null, null, null, lotTracked, Instant.EPOCH
        ));
    }

    private static ProcessView process(long id, long itemId, short sequence) {
        return new ProcessView(
                id, itemId, "", "", sequence, 1L, "P", "공정",
                WorkDistinction.INHOUSE, null, null, 0, (short) 0, Instant.EPOCH
        );
    }

    private static StocktakeLineInput input(
            int rowNo,
            String itemNo,
            Long processId,
            String partnerNo,
            String lotNo,
            BigDecimal counted,
            BigDecimal expectedBook
    ) {
        return new StocktakeLineInput(rowNo, itemNo, processId, partnerNo, lotNo, counted, expectedBook);
    }

    private static String bookKey(long itemId, String locationCode, Long processId) {
        return itemId + "|" + locationCode + "|" + processId;
    }

    private static String historyKey(long itemId, long locationId, Long processId) {
        return itemId + "|" + locationId + "|" + processId;
    }

    private StocktakeBookQuery bookQuery() {
        return new StocktakeBookQuery() {
            @Override
            public List<StocktakeBookLine> listInhouseNonLot(LocalDate stocktakeDate, int fiscalYear) {
                return List.of();
            }

            @Override
            public List<StocktakeBookLine> listRawWithoutBalance(int fiscalYear) {
                return items.values().stream()
                        .filter(item -> item.propertyClassification() == PropertyClassification.원자재)
                        .filter(item -> !item.lotTracked())
                        .map(item -> new StocktakeBookLine(
                                item.itemNo(), item.itemName(), "원자재", "RAW", "원자재창고",
                                null, null, BigDecimal.ZERO, null, null, null, null, null,
                                item.id(), false
                        ))
                        .toList();
            }
        };
    }

    private ItemRepository itemRepository() {
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
                    String query,
                    List<PropertyClassification> classifications,
                    int limit
            ) {
                throw new UnsupportedOperationException();
            }

            @Override
            public Optional<ItemView> findActiveById(long id) {
                throw new UnsupportedOperationException();
            }

            @Override
            public Optional<ItemView> findActiveByItemNo(String itemNo) {
                return Optional.ofNullable(items.get(itemNo));
            }
        };
    }

    private ProcessRepository processRepository() {
        return new ProcessRepository() {
            @Override
            public long save(com.shindong.smartmanager.application.process.ProcessCommand command, ProcessVariant variant, String actorUserId) {
                throw new UnsupportedOperationException();
            }

            @Override
            public void update(long id, com.shindong.smartmanager.application.process.ProcessUpdateCommand command, String actorUserId) {
                throw new UnsupportedOperationException();
            }

            @Override
            public void softDelete(long id, String actorUserId) {
                throw new UnsupportedOperationException();
            }

            @Override
            public Optional<ProcessView> findActiveById(long id) {
                return Optional.ofNullable(processes.get(id));
            }

            @Override
            public List<ProcessView> findAllActiveByItemId(long itemId, ProcessVariant variant) {
                return processes.values().stream().filter(process -> process.itemId() == itemId).toList();
            }

            @Override
            public List<ProcessView> findAllActive(ProcessVariant variant) {
                return new ArrayList<>(processes.values());
            }

            @Override
            public boolean existsActiveDuplicate(long itemId, long processCodeId, short processSequenceNum, Long excludeId) {
                return false;
            }

            @Override
            public Optional<ProcessView> findActiveByItemIdAndPublicCodeIdAndSequence(
                    long itemId,
                    long publicCodeId,
                    short processSequenceNum,
                    ProcessVariant variant
            ) {
                return Optional.empty();
            }

            @Override
            public long ensureMaterialProcess(long itemId, String actorUserId) {
                throw new UnsupportedOperationException();
            }
        };
    }

    private InventoryLocationQueryRepository locationRepository() {
        return locationCode -> switch (locationCode) {
            case "RAW" -> Optional.of(1L);
            case "WIP" -> Optional.of(2L);
            case "SALES" -> Optional.of(3L);
            case "OUTSOURCE" -> Optional.of(4L);
            default -> Optional.empty();
        };
    }

    private InventoryStockBalanceRepository balanceRepository() {
        return new InventoryStockBalanceRepository() {
            @Override
            public InventoryBalanceSlotView ensureBalance(InventoryBalanceKey key) {
                throw new AssertionError("미리보기가 재고 슬롯을 만들었습니다.");
            }

            @Override
            public InventoryBalanceSlotView saveBalance(InventoryBalanceSlotView balance) {
                throw new UnsupportedOperationException();
            }

            @Override
            public BigDecimal sumQtyThrough(InventoryBalanceKey key) {
                String locationCode = switch ((int) key.locationId()) {
                    case 1 -> "RAW";
                    case 2 -> "WIP";
                    case 3 -> "SALES";
                    default -> "?";
                };
                return books.getOrDefault(bookKey(key.itemId(), locationCode, key.outputProcessId()), BigDecimal.ZERO);
            }

            @Override
            public List<InventoryBalanceSlotView> lockSlotsThrough(InventoryBalanceKey key) {
                return List.of();
            }
        };
    }

    private StocktakeLotQuery lotQuery() {
        return new StocktakeLotQuery() {
            @Override
            public Optional<Long> findLotId(long itemId, String lotNo) {
                return lotBalances.stream()
                        .filter(lot -> lot.lotNo().equals(lotNo))
                        .map(StocktakeLotQty::lotId)
                        .findFirst();
            }

            @Override
            public List<StocktakeLotQty> listThrough(
                    long itemId,
                    String locationCode,
                    int fiscalYear,
                    Long outputProcessId,
                    Long inputProcessId,
                    Long partnerId
            ) {
                return List.copyOf(lotBalances);
            }
        };
    }

    private StocktakeHistoryQuery historyQuery() {
        return new StocktakeHistoryQuery() {
            @Override
            public boolean hasHistory(
                    long itemId,
                    long locationId,
                    Long outputProcessId,
                    Long inputProcessId,
                    Long partnerId,
                    int fiscalYear
            ) {
                return history.getOrDefault(historyKey(itemId, locationId, outputProcessId), false);
            }

            @Override
            public boolean hasLaterMovementInSummedYears(
                    long itemId,
                    long locationId,
                    Long outputProcessId,
                    Long inputProcessId,
                    Long partnerId,
                    int fiscalYear,
                    LocalDate stocktakeDate
            ) {
                return later.getOrDefault(historyKey(itemId, locationId, outputProcessId), false);
            }
        };
    }
}
