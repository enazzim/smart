package com.shindong.smartmanager.application.closing;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.junit.jupiter.api.Assertions.assertTrue;

import java.time.Instant;
import java.time.LocalDate;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

class MonthClosingServiceTest {

    private InMemoryMonthClosingRepository repository;
    private MonthClosingService service;

    @BeforeEach
    void setUp() {
        repository = new InMemoryMonthClosingRepository();
        service = new MonthClosingService(repository, new FiscalCalendarService());
    }

    @Test
    void closeRequiresPreviousMonthWhenHistoryExists() {
        repository.saveClose(2026, 4, "admin", "admin");

        IllegalArgumentException ex = assertThrows(
                IllegalArgumentException.class,
                () -> service.close(new CloseMonthCommand(2026, 6), "admin", "admin")
        );
        assertTrue(ex.getMessage().contains("2026-05"));
    }

    @Test
    void assertTransactionOpenThrowsWhenClosed() {
        repository.saveClose(2026, 6, "admin", "admin");

        IllegalStateException ex = assertThrows(
                IllegalStateException.class,
                () -> service.assertTransactionOpen(LocalDate.of(2026, 6, 10))
        );
        assertTrue(ex.getMessage().contains("2026-06"));
    }

    @Test
    void closeIsIdempotent() {
        repository.saveClose(2026, 5, "admin", "admin");
        MonthClosingView first = service.close(new CloseMonthCommand(2026, 5), "admin", "admin");
        MonthClosingView second = service.close(new CloseMonthCommand(2026, 5), "admin", "admin");
        assertEquals(first.id(), second.id());
    }

    @Test
    void closeReopenCloseReopenCycle() {
        service.close(new CloseMonthCommand(2026, 6), "admin", "admin");
        service.reopen(new CloseMonthCommand(2026, 6));
        assertTrue(!repository.isClosed(2026, 6));

        service.close(new CloseMonthCommand(2026, 6), "admin", "admin");
        assertTrue(repository.isClosed(2026, 6));

        service.reopen(new CloseMonthCommand(2026, 6));
        assertTrue(!repository.isClosed(2026, 6));
    }

    @Test
    void reopenOnlyLatestClosedMonth() {
        repository.saveClose(2026, 4, "admin", "admin");
        repository.saveClose(2026, 5, "admin", "admin");

        IllegalArgumentException ex = assertThrows(
                IllegalArgumentException.class,
                () -> service.reopen(new CloseMonthCommand(2026, 4))
        );
        assertTrue(ex.getMessage().contains("2026-05"));

        service.reopen(new CloseMonthCommand(2026, 5));
        assertTrue(!repository.isClosed(2026, 5));
        assertTrue(repository.isClosed(2026, 4));
    }

    private static final class InMemoryMonthClosingRepository implements MonthClosingRepository {

        private long nextId = 1;
        private final Map<String, MonthClosingRecord> store = new HashMap<>();

        @Override
        public List<MonthClosingView> findAllClosed() {
            return store.values().stream()
                    .filter(MonthClosingRecord::active)
                    .map(MonthClosingRecord::view)
                    .toList();
        }

        @Override
        public Optional<MonthClosingView> findClosed(int fiscalYear, int fiscalMonth) {
            MonthClosingRecord record = store.get(key(fiscalYear, fiscalMonth));
            if (record == null || !record.active()) {
                return Optional.empty();
            }
            return Optional.of(record.view());
        }

        @Override
        public boolean isClosed(int fiscalYear, int fiscalMonth) {
            MonthClosingRecord record = store.get(key(fiscalYear, fiscalMonth));
            return record != null && record.active();
        }

        @Override
        public boolean hasAnyClosed() {
            return store.values().stream().anyMatch(MonthClosingRecord::active);
        }

        @Override
        public MonthClosingView saveClose(int fiscalYear, int fiscalMonth, String closedBy, String closedById) {
            String key = key(fiscalYear, fiscalMonth);
            MonthClosingRecord existing = store.get(key);
            if (existing != null && existing.active()) {
                return existing.view();
            }
            MonthClosingView view = new MonthClosingView(
                    nextId++,
                    fiscalYear,
                    fiscalMonth,
                    Instant.now(),
                    closedBy,
                    closedById
            );
            store.put(key, new MonthClosingRecord(view, true));
            return view;
        }

        @Override
        public Optional<MonthClosingView> findLatestClosed() {
            return store.values().stream()
                    .filter(MonthClosingRecord::active)
                    .map(MonthClosingRecord::view)
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
            String key = key(fiscalYear, fiscalMonth);
            MonthClosingRecord existing = store.get(key);
            if (existing == null || !existing.active()) {
                throw new IllegalArgumentException("마감 정보를 찾을 수 없습니다.");
            }
            store.remove(key);
        }

        private String key(int fiscalYear, int fiscalMonth) {
            return fiscalYear + "-" + fiscalMonth;
        }

        private record MonthClosingRecord(MonthClosingView view, boolean active) {
        }
    }
}
