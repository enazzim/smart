package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.inventory.CreateMiscStockMovementCommand;
import com.shindong.smartmanager.application.inventory.MiscStockMovementRepository;
import com.shindong.smartmanager.application.inventory.MiscStockMovementService;
import com.shindong.smartmanager.application.inventory.NumberSequenceKind;
import com.shindong.smartmanager.application.inventory.PrefixedNumberSequence;
import com.shindong.smartmanager.application.inventory.ReasonCodeQuery;
import com.shindong.smartmanager.application.inventory.StocktakeBookLine;
import com.shindong.smartmanager.application.inventory.StocktakeLineInput;
import com.shindong.smartmanager.application.inventory.StocktakePreviewLine;
import com.shindong.smartmanager.application.inventory.StocktakeService;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.domain.inventory.MiscStockMovementDirection;
import com.shindong.smartmanager.infrastructure.persistence.inventory.SpringDataStocktakeBatchRepository;
import com.shindong.smartmanager.infrastructure.persistence.inventory.SpringDataStocktakeLineRepository;
import com.shindong.smartmanager.infrastructure.persistence.inventory.StocktakeBatchJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.inventory.StocktakeLineJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;
import org.springframework.dao.PessimisticLockingFailureException;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class StocktakeApplicationService {

    private final StocktakeService stocktakeService;
    private final MonthClosingService monthClosingService;
    private final MiscStockMovementService miscStockMovementService;
    private final PrefixedNumberSequence numberSequence;
    private final MiscStockMovementRepository miscStockMovementRepository;
    private final ReasonCodeQuery reasonCodeQuery;
    private final ItemRepository itemRepository;
    private final SpringDataStocktakeBatchRepository batchRepository;
    private final SpringDataStocktakeLineRepository lineRepository;
    private final MasterAuditActorLookup actorLookup;

    public StocktakeApplicationService(
            StocktakeService stocktakeService,
            MonthClosingService monthClosingService,
            MiscStockMovementService miscStockMovementService,
            PrefixedNumberSequence numberSequence,
            MiscStockMovementRepository miscStockMovementRepository,
            ReasonCodeQuery reasonCodeQuery,
            ItemRepository itemRepository,
            SpringDataStocktakeBatchRepository batchRepository,
            SpringDataStocktakeLineRepository lineRepository,
            MasterAuditActorLookup actorLookup
    ) {
        this.stocktakeService = stocktakeService;
        this.monthClosingService = monthClosingService;
        this.miscStockMovementService = miscStockMovementService;
        this.numberSequence = numberSequence;
        this.miscStockMovementRepository = miscStockMovementRepository;
        this.reasonCodeQuery = reasonCodeQuery;
        this.itemRepository = itemRepository;
        this.batchRepository = batchRepository;
        this.lineRepository = lineRepository;
        this.actorLookup = actorLookup;
    }

    @Transactional(readOnly = true)
    public List<StocktakeBookLine> book(LocalDate stocktakeDate) {
        return stocktakeService.book(stocktakeDate);
    }

    @Transactional(readOnly = true)
    public List<StocktakePreviewLine> preview(LocalDate stocktakeDate, List<StocktakeLineInput> lines) {
        return stocktakeService.preview(stocktakeDate, lines);
    }

    @Transactional
    public String commit(LocalDate stocktakeDate, List<StocktakeLineInput> lines, String actorUserId) {
        try {
            monthClosingService.assertTransactionOpen(stocktakeDate);
            List<StocktakePreviewLine> preview = stocktakeService.preview(stocktakeDate, lines);
            List<String> errors = preview.stream()
                    .filter(line -> line.error() != null)
                    .map(line -> line.rowNo() + "행: " + line.error())
                    .toList();
            if (!errors.isEmpty()) {
                throw new IllegalArgumentException(String.join("\n", errors));
            }
            List<PostedLine> posting = new ArrayList<>();
            for (int i = 0; i < lines.size(); i++) {
                StocktakePreviewLine evaluated = preview.get(i);
                if ("IN".equals(evaluated.action()) || "OUT".equals(evaluated.action())) {
                    posting.add(new PostedLine(lines.get(i), evaluated));
                }
            }
            if (posting.isEmpty()) {
                throw new IllegalArgumentException("반영할 실사 행이 없습니다.");
            }
            posting.sort(Comparator
                    .comparing((PostedLine line) -> line.input.itemNo() == null ? "" : line.input.itemNo())
                    .thenComparing(line -> line.evaluated.locationCode() == null ? "" : line.evaluated.locationCode())
                    .thenComparing(line -> line.evaluated.outputProcessId() == null ? 0L : line.evaluated.outputProcessId())
                    .thenComparing(line -> line.evaluated.partnerId() == null ? 0L : line.evaluated.partnerId())
                    .thenComparing(line -> line.evaluated.lotNo() == null ? "" : line.evaluated.lotNo()));
            String batchNo = nextBatchNo(stocktakeDate);
            long openingReasonId = reasonCodeQuery.requireId("1500", "150005");
            long adjustReasonId = reasonCodeQuery.requireId("1500", "150001");
            StocktakeBatchJpaEntity batch = new StocktakeBatchJpaEntity();
            batch.setBatchNo(batchNo);
            batch.setStocktakeDate(stocktakeDate);
            batch.setStatus("POSTED");
            batch.setCreatedById(actorLookup.idOf(actorUserId));
            batch.setCreatedAt(Instant.now());
            batch = batchRepository.save(batch);

            for (PostedLine line : posting) {
                StocktakeLineInput input = line.input;
                StocktakePreviewLine evaluated = line.evaluated;
                long reasonId = "기초재고".equals(evaluated.reasonName()) ? openingReasonId : adjustReasonId;
                String movementNo = nextMovementNo(stocktakeDate);
                var saved = miscStockMovementService.registerAssigned(
                        new CreateMiscStockMovementCommand(
                                stocktakeDate,
                                MiscStockMovementDirection.valueOf(evaluated.action()),
                                itemRepository.findActiveByItemNo(input.itemNo().trim()).orElseThrow().id(),
                                evaluated.inputProcessId() == null ? input.processSequenceId() : null,
                                evaluated.movementQty(),
                                reasonId,
                                "실사 " + batchNo,
                                evaluated.lotId(),
                                evaluated.lotId() == null ? evaluated.lotNo() : null,
                                false,
                                evaluated.inputProcessId(),
                                evaluated.partnerId()
                        ),
                        movementNo,
                        actorUserId
                );
                StocktakeLineJpaEntity row = new StocktakeLineJpaEntity();
                row.setBatchId(batch.getId());
                row.setItemId(saved.itemId());
                row.setLocationCode(evaluated.locationCode());
                row.setOutputProcessId(evaluated.outputProcessId());
                row.setInputProcessId(evaluated.inputProcessId());
                row.setPartnerId(evaluated.partnerId());
                row.setLotId(saved.lotId());
                row.setBookQty(evaluated.bookQty());
                row.setCountedQty(input.countedQty());
                row.setMiscStockMovementId(saved.id());
                row.setCreatedAt(Instant.now());
                lineRepository.save(row);
            }
            return batchNo;
        } catch (PessimisticLockingFailureException ex) {
            throw new IllegalArgumentException("다른 입출고와 겹쳤습니다. 다시 시도해 주세요.");
        }
    }

    private record PostedLine(StocktakeLineInput input, StocktakePreviewLine evaluated) {
    }

    private String nextBatchNo(LocalDate stocktakeDate) {
        String prefix = "STK-" + stocktakeDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        int existingMax = batchRepository.maxBatchSuffix(prefix);
        int seq = numberSequence.next(NumberSequenceKind.STOCKTAKE_BATCH, prefix, existingMax);
        return prefix + String.format("%03d", seq);
    }

    private String nextMovementNo(LocalDate stocktakeDate) {
        String prefix = "MSM-" + stocktakeDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        int existingMax = miscStockMovementRepository.maxMovementNoSuffix(prefix);
        int seq = numberSequence.next(NumberSequenceKind.MISC_MOVEMENT, prefix, existingMax);
        return prefix + String.format("%03d", seq);
    }
}
