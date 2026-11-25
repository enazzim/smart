package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.FiscalPeriod;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

public class StocktakeService {

    private static final String LOCATION_OUTSOURCE = "OUTSOURCE";

    private final ItemRepository itemRepository;
    private final MiscStockMovementResolver resolver;
    private final InventoryBalanceService inventoryBalanceService;
    private final InventoryLocationQueryRepository locationRepository;
    private final FiscalCalendarService fiscalCalendarService;
    private final StocktakeHistoryQuery historyQuery;
    private final StocktakeBookQuery bookQuery;
    private final OutsourceStocktakeCatalog outsourceCatalog;
    private final StocktakeLotQuery lotQuery;

    public StocktakeService(
            ItemRepository itemRepository,
            MiscStockMovementResolver resolver,
            InventoryBalanceService inventoryBalanceService,
            InventoryLocationQueryRepository locationRepository,
            FiscalCalendarService fiscalCalendarService,
            StocktakeHistoryQuery historyQuery,
            StocktakeBookQuery bookQuery,
            OutsourceStocktakeCatalog outsourceCatalog,
            StocktakeLotQuery lotQuery
    ) {
        this.itemRepository = itemRepository;
        this.resolver = resolver;
        this.inventoryBalanceService = inventoryBalanceService;
        this.locationRepository = locationRepository;
        this.fiscalCalendarService = fiscalCalendarService;
        this.historyQuery = historyQuery;
        this.bookQuery = bookQuery;
        this.outsourceCatalog = outsourceCatalog;
        this.lotQuery = lotQuery;
    }

    public List<StocktakeBookLine> book(LocalDate stocktakeDate) {
        FiscalPeriod period = fiscalCalendarService.resolvePeriod(stocktakeDate);
        List<StocktakeBookLine> lines = new ArrayList<>();
        for (StocktakeBookLine line : bookQuery.listInhouseNonLot(stocktakeDate, period.fiscalYear())) {
            if (line.lotTracked() && line.error() == null) {
                Long storedOutput = "SALES".equals(line.locationCode()) ? null : line.outputProcessId();
                lines.addAll(expandLots(line, period.fiscalYear(), storedOutput, null, null));
            } else {
                lines.add(line);
            }
        }
        Set<Long> rawItemIds = new HashSet<>();
        for (StocktakeBookLine line : lines) {
            if ("RAW".equals(line.locationCode())) {
                rawItemIds.add(line.itemId());
            }
        }
        for (StocktakeBookLine line : bookQuery.listRawWithoutBalance(period.fiscalYear())) {
            if (!rawItemIds.add(line.itemId())) {
                continue;
            }
            if (line.lotTracked() && line.error() == null) {
                lines.addAll(expandLots(line, period.fiscalYear(), null, null, null));
            } else {
                lines.add(line);
            }
        }
        for (OutsourceStocktakeSlot slot : outsourceCatalog.list()) {
            lines.addAll(outsourceBookLines(slot, stocktakeDate, period.fiscalYear()));
        }
        return lines;
    }

    public List<StocktakePreviewLine> preview(LocalDate stocktakeDate, List<StocktakeLineInput> lines) {
        FiscalPeriod period = fiscalCalendarService.resolvePeriod(stocktakeDate);
        List<OutsourceStocktakeSlot> outsourceSlots = outsourceCatalog.list();
        List<StocktakePreviewLine> result = new ArrayList<>();
        Set<String> seen = new HashSet<>();
        for (StocktakeLineInput line : lines) {
            result.add(evaluate(stocktakeDate, period.fiscalYear(), line, seen, outsourceSlots));
        }
        return result;
    }

    private List<StocktakeBookLine> outsourceBookLines(OutsourceStocktakeSlot slot, LocalDate stocktakeDate, int fiscalYear) {
        if (slot.error() != null) {
            return List.of(bookLine(
                    slot.itemNo(), slot.itemName(), slot.propertyClassification(), LOCATION_OUTSOURCE, "외주창고",
                    null, slot.processName(), null, slot.partnerNo(), slot.partnerName(), slot.inputProcessId(),
                    null, slot.itemId(), slot.lotTracked(), slot.error()
            ));
        }
        BigDecimal bookQty = readBook(
                slot.itemId(), LOCATION_OUTSOURCE, stocktakeDate, null, slot.inputProcessId(), slot.companyId()
        );
        String later = laterMovement(
                slot.itemId(), LOCATION_OUTSOURCE, null, slot.inputProcessId(), slot.companyId(), fiscalYear, stocktakeDate
        );
        StocktakeBookLine base = bookLine(
                slot.itemNo(), slot.itemName(), slot.propertyClassification(), LOCATION_OUTSOURCE, "외주창고",
                null, slot.processName(), bookQty, slot.partnerNo(), slot.partnerName(), slot.inputProcessId(),
                null, slot.itemId(), slot.lotTracked(), later
        );
        if (!slot.lotTracked() || later != null) {
            return List.of(base);
        }
        return expandLots(base, fiscalYear, null, slot.inputProcessId(), slot.companyId());
    }

    private List<StocktakeBookLine> expandLots(
            StocktakeBookLine line,
            int fiscalYear,
            Long outputProcessId,
            Long inputProcessId,
            Long partnerId
    ) {
        List<StocktakeLotQty> lots = lotQuery.listThrough(
                line.itemId(), line.locationCode(), fiscalYear, outputProcessId, inputProcessId, partnerId
        );
        BigDecimal lotSum = lots.stream().map(StocktakeLotQty::qty).reduce(BigDecimal.ZERO, BigDecimal::add);
        boolean mismatch = line.bookQty() != null && line.bookQty().compareTo(lotSum) != 0;
        if (lots.isEmpty()) {
            String error = mismatch ? "슬롯 합과 Lot 합이 다릅니다." : line.error();
            return List.of(copyBook(line, null, line.bookQty(), error));
        }
        List<StocktakeBookLine> rows = new ArrayList<>();
        for (StocktakeLotQty lot : lots) {
            rows.add(copyBook(
                    line,
                    lot.lotNo(),
                    lot.qty(),
                    mismatch ? "슬롯 합과 Lot 합이 다릅니다." : line.error()
            ));
        }
        return rows;
    }

    private StocktakePreviewLine evaluate(
            LocalDate stocktakeDate,
            int fiscalYear,
            StocktakeLineInput line,
            Set<String> seen,
            List<OutsourceStocktakeSlot> outsourceSlots
    ) {
        if (line.countedQty() == null) {
            return skipped(line);
        }
        if (line.itemNo() == null || line.itemNo().isBlank()) {
            return error(line, null, null, null, null, null, null, null, null, null, "품목번호가 없습니다.");
        }
        ItemView item = itemRepository.findActiveByItemNo(line.itemNo().trim()).orElse(null);
        if (item == null) {
            return error(line, null, null, null, null, null, null, null, null, null, "품목을 찾을 수 없습니다: " + line.itemNo());
        }
        if (line.partnerNo() != null && !line.partnerNo().isBlank()) {
            return evaluateOutsource(stocktakeDate, fiscalYear, line, item, seen, outsourceSlots);
        }
        if (line.lotNo() != null && !line.lotNo().isBlank() && !item.lotTracked()) {
            return error(line, item.itemName(), null, null, null, null, null, null, null, null, "Lot 비추적 품목에 Lot번호를 적을 수 없습니다.");
        }
        if (item.lotTracked() && (line.lotNo() == null || line.lotNo().isBlank())) {
            return error(line, item.itemName(), null, null, null, null, null, null, null, null, "Lot번호를 적어 주세요.");
        }
        MiscStockMovementTarget target;
        try {
            target = resolver.resolve(item, line.processSequenceId());
        } catch (IllegalArgumentException ex) {
            return error(line, item.itemName(), null, null, null, null, null, null, null, null, ex.getMessage());
        }
        return evaluateSlot(
                stocktakeDate, fiscalYear, line, item, seen,
                target.locationCode(), target.outputProcessId(), null, null, null,
                target.outputProcessName()
        );
    }

    private StocktakePreviewLine evaluateOutsource(
            LocalDate stocktakeDate,
            int fiscalYear,
            StocktakeLineInput line,
            ItemView item,
            Set<String> seen,
            List<OutsourceStocktakeSlot> outsourceSlots
    ) {
        String partnerNo = normalizePartnerNo(line.partnerNo());
        OutsourceStocktakeSlot matched = outsourceSlots.stream()
                .filter(slot -> slot.error() == null)
                .filter(slot -> slot.itemId() == item.id())
                .filter(slot -> normalizePartnerNo(slot.partnerNo()).equals(partnerNo))
                .filter(slot -> line.processSequenceId() != null && line.processSequenceId().equals(slot.inputProcessId()))
                .findFirst()
                .orElse(null);
        if (matched == null) {
            return error(line, item.itemName(), null, null, null, null, null, null, line.partnerNo(), line.lotNo(),
                    "외주 거래처와 투입 공정을 확인할 수 없습니다.");
        }
        if (line.lotNo() != null && !line.lotNo().isBlank() && !item.lotTracked()) {
            return error(line, item.itemName(), LOCATION_OUTSOURCE, null, matched.processName(),
                    null, matched.inputProcessId(), matched.companyId(), matched.partnerNo(), line.lotNo(),
                    "Lot 비추적 품목에 Lot번호를 적을 수 없습니다.");
        }
        if (item.lotTracked() && (line.lotNo() == null || line.lotNo().isBlank())) {
            return error(line, item.itemName(), LOCATION_OUTSOURCE, null, matched.processName(),
                    null, matched.inputProcessId(), matched.companyId(), matched.partnerNo(), null,
                    "Lot번호를 적어 주세요.");
        }
        return evaluateSlot(
                stocktakeDate, fiscalYear, line, item, seen,
                LOCATION_OUTSOURCE, null, matched.inputProcessId(), matched.companyId(), matched.partnerNo(),
                matched.processName()
        );
    }

    private StocktakePreviewLine evaluateSlot(
            LocalDate stocktakeDate,
            int fiscalYear,
            StocktakeLineInput line,
            ItemView item,
            Set<String> seen,
            String locationCode,
            Long outputProcessId,
            Long inputProcessId,
            Long partnerId,
            String partnerNo,
            String processName
    ) {
        String lotNo = line.lotNo() == null || line.lotNo().isBlank() ? null : line.lotNo().trim();
        String key = item.id() + "|" + locationCode + "|" + outputProcessId + "|" + inputProcessId + "|" + partnerId + "|" + lotNo;
        if (!seen.add(key)) {
            return error(line, item.itemName(), locationCode, outputProcessId, processName,
                    null, inputProcessId, partnerId, partnerNo, lotNo, "같은 창고·공정 행이 중복되었습니다.");
        }
        long locationId = locationRepository.findActiveLocationIdByCode(locationCode)
                .orElseThrow(() -> new IllegalArgumentException("창고를 찾을 수 없습니다: " + locationCode));
        BigDecimal slotQty = readBook(item.id(), locationCode, stocktakeDate, outputProcessId, inputProcessId, partnerId);
        List<StocktakeLotQty> lots = item.lotTracked()
                ? lotQuery.listThrough(item.id(), locationCode, fiscalYear, outputProcessId, inputProcessId, partnerId)
                : List.of();
        BigDecimal lotSum = lots.stream().map(StocktakeLotQty::qty).reduce(BigDecimal.ZERO, BigDecimal::add);
        StocktakeLotQty existingLot = lotNo == null ? null : lots.stream()
                .filter(lot -> lot.lotNo().equals(lotNo))
                .findFirst()
                .orElse(null);
        Long lotId;
        if (existingLot != null) {
            lotId = existingLot.lotId();
        } else if (lotNo == null) {
            lotId = null;
        } else {
            lotId = lotQuery.findLotId(item.id(), lotNo).orElse(null);
        }
        BigDecimal bookQty = item.lotTracked()
                ? (existingLot != null ? existingLot.qty() : BigDecimal.ZERO)
                : slotQty;
        if (line.expectedBookQty() != null && line.expectedBookQty().compareTo(bookQty) != 0) {
            return error(line, item.itemName(), locationCode, outputProcessId, processName,
                    bookQty, inputProcessId, partnerId, partnerNo, lotNo, "미리보기 이후 장부가 바뀌었습니다.");
        }
        if (historyQuery.hasLaterMovementInSummedYears(
                item.id(), locationId, outputProcessId, inputProcessId, partnerId, fiscalYear, stocktakeDate
        )) {
            return error(line, item.itemName(), locationCode, outputProcessId, processName,
                    bookQty, inputProcessId, partnerId, partnerNo, lotNo, "실사일 이후 입출고가 있어 반영할 수 없습니다.");
        }
        if (item.lotTracked() && slotQty.compareTo(lotSum) != 0) {
            return error(line, item.itemName(), locationCode, outputProcessId, processName,
                    bookQty, inputProcessId, partnerId, partnerNo, lotNo, "슬롯 합과 Lot 합이 다릅니다.");
        }
        if (item.lotTracked() && existingLot == null && line.countedQty().compareTo(BigDecimal.ZERO) > 0
                && slotQty.compareTo(lotSum) != 0) {
            return error(line, item.itemName(), locationCode, outputProcessId, processName,
                    bookQty, inputProcessId, partnerId, partnerNo, lotNo, "슬롯 합과 Lot 합이 다릅니다.");
        }
        boolean history = historyQuery.hasHistory(
                item.id(), locationId, outputProcessId, inputProcessId, partnerId, fiscalYear
        );
        BigDecimal historyBook = item.lotTracked() ? slotQty : bookQty;
        if (!history && historyBook.compareTo(BigDecimal.ZERO) != 0) {
            return error(line, item.itemName(), locationCode, outputProcessId, processName,
                    bookQty, inputProcessId, partnerId, partnerNo, lotNo, "입출고 이력이 없는데 장부가 0이 아닙니다.");
        }
        if (item.lotTracked() && existingLot == null && line.countedQty().compareTo(bookQty) < 0) {
            return error(line, item.itemName(), locationCode, outputProcessId, processName,
                    bookQty, inputProcessId, partnerId, partnerNo, lotNo, "출고는 기존 Lot가 필요합니다.");
        }
        if (!history) {
            if (line.countedQty().compareTo(BigDecimal.ZERO) <= 0) {
                return none(line, item.itemName(), locationCode, outputProcessId, processName, bookQty,
                        inputProcessId, partnerId, partnerNo, lotNo, lotId);
            }
            return posted(line, item.itemName(), locationCode, outputProcessId, processName, bookQty,
                    "IN", line.countedQty(), "기초재고", inputProcessId, partnerId, partnerNo, lotNo, lotId);
        }
        int compare = line.countedQty().compareTo(bookQty);
        if (compare == 0) {
            return none(line, item.itemName(), locationCode, outputProcessId, processName, bookQty,
                    inputProcessId, partnerId, partnerNo, lotNo, lotId);
        }
        if (compare > 0) {
            return posted(line, item.itemName(), locationCode, outputProcessId, processName, bookQty,
                    "IN", line.countedQty().subtract(bookQty), "실사조정",
                    inputProcessId, partnerId, partnerNo, lotNo, lotId);
        }
        return posted(line, item.itemName(), locationCode, outputProcessId, processName, bookQty,
                "OUT", bookQty.subtract(line.countedQty()), "실사조정",
                inputProcessId, partnerId, partnerNo, lotNo, lotId);
    }

    private BigDecimal readBook(
            long itemId,
            String locationCode,
            LocalDate stocktakeDate,
            Long outputProcessId,
            Long inputProcessId,
            Long partnerId
    ) {
        return inventoryBalanceService.currentStockQty(
                itemId, locationCode, stocktakeDate, outputProcessId, inputProcessId, partnerId
        );
    }

    private String laterMovement(
            long itemId,
            String locationCode,
            Long outputProcessId,
            Long inputProcessId,
            Long partnerId,
            int fiscalYear,
            LocalDate stocktakeDate
    ) {
        long locationId = locationRepository.findActiveLocationIdByCode(locationCode).orElse(0L);
        if (locationId == 0L) {
            return "창고를 찾을 수 없습니다: " + locationCode;
        }
        return historyQuery.hasLaterMovementInSummedYears(
                itemId, locationId, outputProcessId, inputProcessId, partnerId, fiscalYear, stocktakeDate
        ) ? "실사일 이후 입출고가 있어 반영할 수 없습니다." : null;
    }

    private static String normalizePartnerNo(String value) {
        if (value == null) {
            return "";
        }
        return value.replace("-", "").replace(" ", "").trim();
    }

    private static StocktakeBookLine bookLine(
            String itemNo,
            String itemName,
            String classification,
            String locationCode,
            String locationName,
            Long outputProcessId,
            String processName,
            BigDecimal bookQty,
            String partnerNo,
            String partnerName,
            Long inputProcessId,
            String lotNo,
            long itemId,
            boolean lotTracked,
            String error
    ) {
        return new StocktakeBookLine(
                itemNo, itemName, classification, locationCode, locationName, outputProcessId, processName,
                bookQty, error, partnerNo, partnerName, inputProcessId, lotNo, itemId, lotTracked
        );
    }

    private static StocktakeBookLine copyBook(StocktakeBookLine line, String lotNo, BigDecimal bookQty, String error) {
        return new StocktakeBookLine(
                line.itemNo(), line.itemName(), line.propertyClassification(), line.locationCode(), line.locationName(),
                line.outputProcessId(), line.processName(), bookQty, error, line.partnerNo(), line.partnerName(),
                line.inputProcessId(), lotNo, line.itemId(), line.lotTracked()
        );
    }

    private static StocktakePreviewLine skipped(StocktakeLineInput line) {
        return new StocktakePreviewLine(
                line.rowNo(), line.itemNo(), null, null, null, null,
                null, line.countedQty(), "SKIP", null, null, null,
                null, null, line.partnerNo(), line.lotNo(), null
        );
    }

    private static StocktakePreviewLine none(
            StocktakeLineInput line,
            String itemName,
            String locationCode,
            Long outputProcessId,
            String processName,
            BigDecimal bookQty,
            Long inputProcessId,
            Long partnerId,
            String partnerNo,
            String lotNo,
            Long lotId
    ) {
        return posted(line, itemName, locationCode, outputProcessId, processName, bookQty,
                "NONE", null, null, inputProcessId, partnerId, partnerNo, lotNo, lotId);
    }

    private static StocktakePreviewLine posted(
            StocktakeLineInput line,
            String itemName,
            String locationCode,
            Long outputProcessId,
            String processName,
            BigDecimal bookQty,
            String action,
            BigDecimal qty,
            String reason,
            Long inputProcessId,
            Long partnerId,
            String partnerNo,
            String lotNo,
            Long lotId
    ) {
        return new StocktakePreviewLine(
                line.rowNo(), line.itemNo(), itemName, locationCode, outputProcessId, processName,
                bookQty, line.countedQty(), action, qty, reason, null,
                inputProcessId, partnerId, partnerNo, lotNo, lotId
        );
    }

    private static StocktakePreviewLine error(
            StocktakeLineInput line,
            String itemName,
            String locationCode,
            Long processId,
            String processName,
            BigDecimal bookQty,
            Long inputProcessId,
            Long partnerId,
            String partnerNo,
            String lotNo,
            String message
    ) {
        return new StocktakePreviewLine(
                line.rowNo(), line.itemNo(), itemName, locationCode, processId, processName,
                bookQty, line.countedQty(), "ERROR", null, null, message,
                inputProcessId, partnerId, partnerNo, lotNo, null
        );
    }
}
