package com.shindong.smartmanager.application.inventory;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

import com.shindong.smartmanager.application.bom.ItemCompositionRepository;
import com.shindong.smartmanager.application.bom.ItemCompositionView;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessView;
import com.shindong.smartmanager.application.unitprice.UnitPriceView;
import com.shindong.smartmanager.domain.item.CheckDistinction;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import com.shindong.smartmanager.domain.pricing.CostType;
import com.shindong.smartmanager.domain.process.ProcessVariant;
import com.shindong.smartmanager.domain.process.WorkDistinction;
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
class OutsourceStocktakePlannerTest {

    private final Map<Long, ItemView> items = new HashMap<>();
    private final Map<Long, List<ProcessView>> processes = new HashMap<>();
    private final Map<Long, List<ItemCompositionView>> bom = new HashMap<>();
    private OutsourceStocktakePlanner planner;

    @BeforeEach
    void setUp() {
        planner = new OutsourceStocktakePlanner(processes(), compositions(), items());
    }

    @Test
    void firstOutsourceProcessUsesBomChildAndDoesNotCreateAProcess() {
        items.put(10L, item(10L, "FG", PropertyClassification.제품, false));
        items.put(11L, item(11L, "RM", PropertyClassification.원자재, false));
        processes.put(10L, List.of(process(1L, 10L, (short) 10, 100L, "P1", WorkDistinction.INHOUSE)));
        processes.put(11L, List.of(process(99L, 11L, (short) 1, 1L, OutsourceStocktakePlanner.MATERIAL_PROCESS_CODE, WorkDistinction.INHOUSE)));
        bom.put(10L, List.of(composition(10L, 11L)));

        List<OutsourceStocktakeSlot> slots = planner.plan(price(10L, 7L, "123-45", "외주처", 100L));

        assertEquals(1, slots.size());
        assertEquals(11L, slots.get(0).itemId());
        assertEquals(99L, slots.get(0).inputProcessId());
        assertEquals("123-45", slots.get(0).partnerNo());
        assertEquals(null, slots.get(0).error());
    }

    @Test
    void missingMaterialProcessIsARowError() {
        items.put(10L, item(10L, "FG", PropertyClassification.제품, false));
        items.put(11L, item(11L, "RM", PropertyClassification.원자재, false));
        processes.put(10L, List.of(process(1L, 10L, (short) 10, 100L, "P1", WorkDistinction.INHOUSE)));
        processes.put(11L, List.of());
        bom.put(10L, List.of(composition(10L, 11L)));

        List<OutsourceStocktakeSlot> slots = planner.plan(price(10L, 7L, "123-45", "외주처", 100L));

        assertEquals(1, slots.size());
        assertTrue(slots.get(0).error().contains("자재 공정"));
    }

    @Test
    void laterOutsourceProcessUsesTheImmediatePreviousProcess() {
        items.put(10L, item(10L, "FG", PropertyClassification.제품, false));
        processes.put(10L, List.of(
                process(1L, 10L, (short) 10, 100L, "P1", WorkDistinction.INHOUSE),
                process(2L, 10L, (short) 20, 200L, "P2", WorkDistinction.OUTSOURCE)
        ));

        List<OutsourceStocktakeSlot> slots = planner.plan(price(10L, 8L, "999", "둘째", 200L));

        assertEquals(1, slots.size());
        assertEquals(10L, slots.get(0).itemId());
        assertEquals(1L, slots.get(0).inputProcessId());
        assertEquals(8L, slots.get(0).companyId());
    }

    @Test
    void wipChildSkipsOutsourceProcesses() {
        items.put(20L, item(20L, "PARENT", PropertyClassification.제품, false));
        items.put(21L, item(21L, "WIP", PropertyClassification.공정품, false));
        processes.put(21L, List.of(
                process(41L, 21L, (short) 10, 500L, "IN", WorkDistinction.INHOUSE),
                process(42L, 21L, (short) 20, 600L, "OS", WorkDistinction.OUTSOURCE),
                process(43L, 21L, (short) 30, 700L, "END", WorkDistinction.INHOUSE)
        ));
        bom.put(20L, List.of(composition(20L, 21L)));

        List<OutsourceStocktakeSlot> slots = planner.plan(price(20L, 7L, "123", "외주처", 700L));

        assertEquals(1, slots.size());
        assertEquals(41L, slots.get(0).inputProcessId());
        assertEquals(21L, slots.get(0).itemId());
    }

    @Test
    void missingPartnerIsOnlyThatRow() {
        List<OutsourceStocktakeSlot> slots = planner.plan(price(10L, 0L, " ", "없음", 100L));
        assertTrue(slots.get(0).error().contains("거래처"));
    }

    @Test
    void eachUnitPricePartnerGetsItsOwnRow() {
        items.put(10L, item(10L, "FG", PropertyClassification.제품, false));
        processes.put(10L, List.of(
                process(1L, 10L, (short) 10, 100L, "P1", WorkDistinction.INHOUSE),
                process(2L, 10L, (short) 20, 200L, "P2", WorkDistinction.OUTSOURCE)
        ));
        OutsourceStocktakeSlot first = planner.plan(price(10L, 1L, "111", "갑", 200L)).get(0);
        OutsourceStocktakeSlot second = planner.plan(price(10L, 2L, "222", "을", 200L)).get(0);
        assertEquals(1L, first.companyId());
        assertEquals(2L, second.companyId());
        assertEquals(first.inputProcessId(), second.inputProcessId());
    }

    private ProcessRepository processes() {
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
                return processes.values().stream().flatMap(List::stream).filter(process -> process.id() == id).findFirst();
            }

            @Override
            public List<ProcessView> findAllActiveByItemId(long itemId, ProcessVariant variant) {
                return processes.getOrDefault(itemId, List.of());
            }

            @Override
            public List<ProcessView> findAllActive(ProcessVariant variant) {
                return List.of();
            }

            @Override
            public boolean existsActiveDuplicate(long itemId, long processCodeId, short processSequenceNum, Long excludeId) {
                return false;
            }

            @Override
            public Optional<ProcessView> findActiveByItemIdAndPublicCodeIdAndSequence(
                    long itemId, long publicCodeId, short processSequenceNum, ProcessVariant variant
            ) {
                return Optional.empty();
            }

            @Override
            public long ensureMaterialProcess(long itemId, String actorUserId) {
                throw new AssertionError("공정 마스터를 만들었습니다.");
            }
        };
    }

    private ItemRepository items() {
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
                return Optional.ofNullable(items.get(id));
            }

            @Override
            public Optional<ItemView> findActiveByItemNo(String itemNo) {
                return Optional.empty();
            }
        };
    }

    private ItemCompositionRepository compositions() {
        return new ItemCompositionRepository() {
            @Override
            public long save(com.shindong.smartmanager.application.bom.ItemCompositionCommand command, String actorUserId) {
                throw new UnsupportedOperationException();
            }

            @Override
            public void updateQuantities(long id, com.shindong.smartmanager.application.bom.ItemCompositionUpdateCommand command, String actorUserId) {
                throw new UnsupportedOperationException();
            }

            @Override
            public void softDelete(long id, String actorUserId) {
                throw new UnsupportedOperationException();
            }

            @Override
            public Optional<ItemCompositionView> findActiveById(long id) {
                return Optional.empty();
            }

            @Override
            public List<ItemCompositionView> findAllActive(Long parentItemId, Long childItemId, String parentItemNoQuery, String childItemNoQuery) {
                return List.of();
            }

            @Override
            public List<ItemCompositionView> findActiveByParentItemId(long parentItemId) {
                return bom.getOrDefault(parentItemId, List.of());
            }

            @Override
            public List<ItemCompositionView> findActiveByChildItemId(long childItemId) {
                return List.of();
            }

            @Override
            public boolean existsActiveUk(long parentItemId, long childItemId, Long excludeId) {
                return false;
            }

            @Override
            public void appendChangeLog(long itemCompositionId, String changeReason, String actorUserId) {
                throw new UnsupportedOperationException();
            }
        };
    }

    private static ItemView item(long id, String no, PropertyClassification classification, boolean lotTracked) {
        return new ItemView(
                id, no, no, classification, null, "EA", null, null,
                CheckDistinction.NONE, null, null, null, null, lotTracked, Instant.EPOCH
        );
    }

    private static ProcessView process(
            long id, long itemId, short sequence, long codeId, String code, WorkDistinction distinction
    ) {
        return new ProcessView(
                id, itemId, "", "", sequence, codeId, code, code,
                distinction, null, null, 0, (short) 0, Instant.EPOCH
        );
    }

    private static ItemCompositionView composition(long parentId, long childId) {
        return new ItemCompositionView(
                1L, parentId, "", "", childId, "", "",
                BigDecimal.ONE, BigDecimal.ONE, LocalDate.EPOCH, null, Instant.EPOCH
        );
    }

    private static UnitPriceView price(long itemId, long companyId, String regNo, String companyName, long beginCodeId) {
        return new UnitPriceView(
                1L, CostType.OUTSOURCE, itemId, "", "", "", companyId, companyName, regNo,
                beginCodeId, null, null, beginCodeId, null, null,
                null, null, null, null, null, Instant.EPOCH
        );
    }
}
