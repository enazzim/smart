package com.shindong.smartmanager.api.web.inventory;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.inventory.CreateMiscStockMovementCommand;
import com.shindong.smartmanager.application.inventory.MiscStockMovementListCriteria;
import com.shindong.smartmanager.domain.inventory.MiscStockMovementDirection;
import com.shindong.smartmanager.infrastructure.application.MiscStockMovementApplicationService;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Positive;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.HttpStatus;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/inventory/misc-movements")
public class MiscStockMovementController {

    private final MiscStockMovementApplicationService miscStockMovementApplicationService;

    public MiscStockMovementController(MiscStockMovementApplicationService miscStockMovementApplicationService) {
        this.miscStockMovementApplicationService = miscStockMovementApplicationService;
    }

    @GetMapping("/preview")
    @PreAuthorize("hasAuthority('inventory:misc-movement:read')")
    public MiscStockMovementPreviewResponse preview(
            @RequestParam long itemId,
            @RequestParam(required = false) Long processSequenceId,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate movementDate
    ) {
        return MiscStockMovementPreviewResponse.from(
                miscStockMovementApplicationService.preview(
                        itemId,
                        processSequenceId,
                        movementDate,
                        SecurityUtils.requirePrincipal().loginId()
                )
        );
    }

    @GetMapping
    @PreAuthorize("hasAuthority('inventory:misc-movement:read')")
    public List<MiscStockMovementResponse> list(
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) String itemName,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate movementDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate movementDateTo
    ) {
        return miscStockMovementApplicationService.list(new MiscStockMovementListCriteria(
                itemNo, itemName, movementDateFrom, movementDateTo
        )).stream().map(MiscStockMovementResponse::from).toList();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('inventory:misc-movement:write')")
    public MiscStockMovementResponse create(@Valid @RequestBody CreateMiscStockMovementRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return MiscStockMovementResponse.from(miscStockMovementApplicationService.register(
                new CreateMiscStockMovementCommand(
                        request.movementDate(),
                        request.movementDirection(),
                        request.itemId(),
                        request.processSequenceId(),
                        request.qty(),
                        request.reasonCodeId(),
                        request.note()
                ),
                principal.loginId()
        ));
    }

    @PutMapping("/{id}")
    @PreAuthorize("hasAuthority('inventory:misc-movement:write')")
    public MiscStockMovementResponse update(
            @PathVariable long id,
            @Valid @RequestBody CreateMiscStockMovementRequest request
    ) {
        var principal = SecurityUtils.requirePrincipal();
        return MiscStockMovementResponse.from(miscStockMovementApplicationService.update(
                id,
                new CreateMiscStockMovementCommand(
                        request.movementDate(),
                        request.movementDirection(),
                        request.itemId(),
                        request.processSequenceId(),
                        request.qty(),
                        request.reasonCodeId(),
                        request.note()
                ),
                principal.loginId()
        ));
    }

    @PostMapping("/{id}/cancel")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('inventory:misc-movement:write')")
    public void delete(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        miscStockMovementApplicationService.delete(id, principal.loginId());
    }

    public record CreateMiscStockMovementRequest(
            @NotNull LocalDate movementDate,
            @NotNull MiscStockMovementDirection movementDirection,
            @NotNull Long itemId,
            Long processSequenceId,
            @NotNull @Positive BigDecimal qty,
            Long reasonCodeId,
            String note
    ) {
    }
}
