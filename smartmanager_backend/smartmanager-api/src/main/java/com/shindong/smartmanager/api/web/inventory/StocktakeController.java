package com.shindong.smartmanager.api.web.inventory;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.inventory.StocktakeLineInput;
import com.shindong.smartmanager.infrastructure.application.StocktakeApplicationService;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotNull;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.HttpStatus;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/inventory/stocktakes")
public class StocktakeController {

    private final StocktakeApplicationService stocktakeApplicationService;

    public StocktakeController(StocktakeApplicationService stocktakeApplicationService) {
        this.stocktakeApplicationService = stocktakeApplicationService;
    }

    @GetMapping("/book")
    @PreAuthorize("hasAuthority('inventory:stocktake:read')")
    public List<StocktakeBookResponse> book(
            @RequestParam @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate stocktakeDate
    ) {
        return stocktakeApplicationService.book(stocktakeDate).stream()
                .map(StocktakeBookResponse::from)
                .toList();
    }

    @PostMapping("/preview")
    @PreAuthorize("hasAuthority('inventory:stocktake:read')")
    public List<StocktakePreviewResponse> preview(@Valid @RequestBody StocktakeRequest request) {
        return stocktakeApplicationService.preview(request.stocktakeDate(), toInputs(request.lines())).stream()
                .map(StocktakePreviewResponse::from)
                .toList();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('inventory:stocktake:write')")
    public StocktakeCommitResponse commit(@Valid @RequestBody StocktakeRequest request) {
        String batchNo = stocktakeApplicationService.commit(
                request.stocktakeDate(),
                toInputs(request.lines()),
                SecurityUtils.requirePrincipal().loginId()
        );
        return new StocktakeCommitResponse(batchNo);
    }

    private static List<StocktakeLineInput> toInputs(List<StocktakeLineRequest> lines) {
        return lines.stream()
                .map(line -> new StocktakeLineInput(
                        line.rowNo(),
                        line.itemNo(),
                        line.processSequenceId(),
                        line.partnerNo(),
                        line.lotNo(),
                        line.countedQty(),
                        line.expectedBookQty()
                ))
                .toList();
    }

    public record StocktakeRequest(
            @NotNull LocalDate stocktakeDate,
            @NotNull List<@Valid StocktakeLineRequest> lines
    ) {
    }

    public record StocktakeLineRequest(
            int rowNo,
            String itemNo,
            Long processSequenceId,
            String partnerNo,
            String lotNo,
            BigDecimal countedQty,
            BigDecimal expectedBookQty
    ) {
    }

    public record StocktakeBookResponse(
            String itemNo,
            String itemName,
            String propertyClassification,
            String locationCode,
            String locationName,
            Long outputProcessId,
            String processName,
            BigDecimal bookQty,
            String error,
            String partnerNo,
            String partnerName,
            Long inputProcessId,
            String lotNo
    ) {
        static StocktakeBookResponse from(com.shindong.smartmanager.application.inventory.StocktakeBookLine line) {
            return new StocktakeBookResponse(
                    line.itemNo(),
                    line.itemName(),
                    line.propertyClassification(),
                    line.locationCode(),
                    line.locationName(),
                    line.outputProcessId(),
                    line.processName(),
                    line.bookQty(),
                    line.error(),
                    line.partnerNo(),
                    line.partnerName(),
                    line.inputProcessId(),
                    line.lotNo()
            );
        }
    }

    public record StocktakePreviewResponse(
            int rowNo,
            String itemNo,
            String itemName,
            String locationCode,
            Long outputProcessId,
            String processName,
            BigDecimal bookQty,
            BigDecimal countedQty,
            String action,
            BigDecimal movementQty,
            String reasonName,
            String error,
            String partnerNo,
            String lotNo
    ) {
        static StocktakePreviewResponse from(com.shindong.smartmanager.application.inventory.StocktakePreviewLine line) {
            return new StocktakePreviewResponse(
                    line.rowNo(),
                    line.itemNo(),
                    line.itemName(),
                    line.locationCode(),
                    line.outputProcessId(),
                    line.processName(),
                    line.bookQty(),
                    line.countedQty(),
                    line.action(),
                    line.movementQty(),
                    line.reasonName(),
                    line.error(),
                    line.partnerNo(),
                    line.lotNo()
            );
        }
    }

    public record StocktakeCommitResponse(String batchNo) {
    }
}
