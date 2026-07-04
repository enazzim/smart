package com.shindong.smartmanager.application.closing;

import java.util.List;
import java.util.Optional;

public interface MonthClosingRepository {

    List<MonthClosingView> findAllClosed();

    Optional<MonthClosingView> findClosed(int fiscalYear, int fiscalMonth);

    boolean isClosed(int fiscalYear, int fiscalMonth);

    boolean hasAnyClosed();

    MonthClosingView saveClose(int fiscalYear, int fiscalMonth, String closedBy, String closedById);

    Optional<MonthClosingView> findLatestClosed();

    void reopen(int fiscalYear, int fiscalMonth);
}
