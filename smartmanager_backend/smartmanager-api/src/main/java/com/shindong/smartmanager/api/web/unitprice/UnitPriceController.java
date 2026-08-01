package com.shindong.smartmanager.api.web.unitprice;

import com.shindong.smartmanager.api.security.BasisAuthorize;
import com.shindong.smartmanager.application.unitprice.UnitPriceHistorySearchQuery;
import com.shindong.smartmanager.application.unitprice.UnitPriceUpdateCommand;
import com.shindong.smartmanager.domain.pricing.CostType;
import com.shindong.smartmanager.infrastructure.application.UnitPriceApplicationService;
import jakarta.validation.Valid;
import java.time.LocalDate;
import java.util.List;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/basis/unit-prices")
@BasisAuthorize.UnitPriceRead
public class UnitPriceController {

    private static final String DEFAULT_ACTOR = "local-dev";

    private final UnitPriceApplicationService unitPriceApplicationService;

    public UnitPriceController(UnitPriceApplicationService unitPriceApplicationService) {
        this.unitPriceApplicationService = unitPriceApplicationService;
    }

    @GetMapping
    public List<UnitPriceResponse> list(
            @RequestParam CostType type,
            @RequestParam(required = false) String q
    ) {
        return unitPriceApplicationService.listActive(type, q).stream()
                .map(UnitPriceResponse::from)
                .toList();
    }

    @GetMapping("/history")
    public List<UnitPriceHistoryResponse> listHistory(
            @RequestParam(required = false) CostType type,
            @RequestParam(required = false) Long companyId,
            @RequestParam(required = false) Long itemId,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate changedFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate changedTo,
            @RequestParam(required = false) String changedBy
    ) {
        UnitPriceHistorySearchQuery query = new UnitPriceHistorySearchQuery(
                type,
                companyId,
                itemId,
                changedFrom,
                changedTo,
                changedBy
        );
        return unitPriceApplicationService.listAllHistory(query).stream()
                .map(UnitPriceHistoryResponse::from)
                .toList();
    }

    @GetMapping("/{id}")
    public UnitPriceResponse get(@PathVariable long id) {
        return UnitPriceResponse.from(unitPriceApplicationService.getActive(id));
    }

    @GetMapping("/by-item/{itemNum}")
    public UnitPriceResponse getByItem(
            @PathVariable String itemNum,
            @RequestParam CostType type
    ) {
        return UnitPriceResponse.from(unitPriceApplicationService.getActiveByItemNo(itemNum, type));
    }

    @GetMapping("/{id}/history")
    public List<UnitPriceHistoryResponse> history(@PathVariable long id) {
        return unitPriceApplicationService.listHistory(id).stream()
                .map(UnitPriceHistoryResponse::from)
                .toList();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @BasisAuthorize.UnitPriceWrite
    public UnitPriceResponse create(
            @Valid @RequestBody CreateUnitPriceRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        return UnitPriceResponse.from(unitPriceApplicationService.register(
                request.type(),
                request.itemNum(),
                request.companyId(),
                request.beginProcessCodeId(),
                request.endProcessCodeId(),
                request.orderRate(),
                request.standardUnitCost(),
                request.discountUnitCost(),
                request.beginDate(),
                request.endDate(),
                actor
        ));
    }

    @PutMapping("/{id}")
    @BasisAuthorize.UnitPriceWrite
    public UnitPriceResponse update(
            @PathVariable long id,
            @Valid @RequestBody UpdateUnitPriceRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        UnitPriceUpdateCommand command = new UnitPriceUpdateCommand(
                request.orderRate(),
                request.standardUnitCost(),
                request.discountUnitCost(),
                request.beginDate(),
                request.endDate(),
                request.updateReason()
        );
        return UnitPriceResponse.from(unitPriceApplicationService.update(id, command, actor));
    }

    @DeleteMapping("/{id}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @BasisAuthorize.UnitPriceWrite
    public void delete(
            @PathVariable long id,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        unitPriceApplicationService.delete(id, actor);
    }
}
