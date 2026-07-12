package com.shindong.smartmanager.api.web.inventory;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.inventory.CreateLotCommand;
import com.shindong.smartmanager.application.inventory.LotListCriteria;
import com.shindong.smartmanager.application.inventory.UpdateLotCommand;
import com.shindong.smartmanager.domain.inventory.LotGenealogyDirection;
import com.shindong.smartmanager.domain.inventory.LotOriginType;
import com.shindong.smartmanager.domain.inventory.LotStatus;
import com.shindong.smartmanager.infrastructure.application.LotApplicationService;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotNull;
import java.time.LocalDate;
import java.util.List;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.HttpStatus;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/inventory/lots")
public class LotController {

    private final LotApplicationService lotApplicationService;

    public LotController(LotApplicationService lotApplicationService) {
        this.lotApplicationService = lotApplicationService;
    }

    @GetMapping
    @PreAuthorize("hasAuthority('inventory:lot:read')")
    public List<LotResponse> list(
            @RequestParam(required = false) Long itemId,
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) String lotNo,
            @RequestParam(required = false) LotStatus status,
            @RequestParam(required = false) String locationCode
    ) {
        return lotApplicationService.list(new LotListCriteria(
                itemId, itemNo, lotNo, status, locationCode
        )).stream().map(LotResponse::from).toList();
    }

    @GetMapping("/available")
    @PreAuthorize("hasAuthority('inventory:lot:read')")
    public List<LotResponse> available(
            @RequestParam long itemId,
            @RequestParam String locationCode,
            @RequestParam(required = false) Long outputProcessId
    ) {
        return lotApplicationService.findAvailableLots(itemId, locationCode, outputProcessId)
                .stream()
                .map(LotResponse::from)
                .toList();
    }

    @GetMapping("/{id}")
    @PreAuthorize("hasAuthority('inventory:lot:read')")
    public LotResponse get(@PathVariable long id) {
        return LotResponse.from(lotApplicationService.get(id));
    }

    @GetMapping("/{id}/genealogy")
    @PreAuthorize("hasAuthority('inventory:lot:read')")
    public List<LotGenealogyLinkResponse> genealogy(
            @PathVariable long id,
            @RequestParam(defaultValue = "UP") LotGenealogyDirection direction
    ) {
        return lotApplicationService.findGenealogy(id, direction).stream()
                .map(LotGenealogyLinkResponse::from)
                .toList();
    }

    @GetMapping("/{id}/movements")
    @PreAuthorize("hasAuthority('inventory:lot:read')")
    public List<StockMovementResponse> movements(@PathVariable long id) {
        return lotApplicationService.findMovements(id).stream()
                .map(StockMovementResponse::from)
                .toList();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('inventory:lot:write')")
    public LotResponse create(@Valid @RequestBody CreateLotRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return LotResponse.from(lotApplicationService.create(
                new CreateLotCommand(
                        request.itemId(),
                        request.lotNo(),
                        Boolean.TRUE.equals(request.autoGenerate()),
                        request.originType() != null ? request.originType() : LotOriginType.MANUAL,
                        request.originDocType(),
                        request.originDocId(),
                        request.p1(),
                        request.p2(),
                        request.expiryDate(),
                        request.certificateRef(),
                        request.remark()
                ),
                principal.loginId()
        ));
    }

    @PutMapping("/{id}")
    @PreAuthorize("hasAuthority('inventory:lot:write')")
    public LotResponse update(@PathVariable long id, @Valid @RequestBody UpdateLotRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return LotResponse.from(lotApplicationService.update(
                id,
                new UpdateLotCommand(
                        request.status(),
                        request.p1(),
                        request.p2(),
                        request.expiryDate(),
                        request.certificateRef(),
                        request.remark()
                ),
                principal.loginId()
        ));
    }

    @DeleteMapping("/{id}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('inventory:lot:write')")
    public void delete(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        lotApplicationService.delete(id, principal.loginId());
    }

    public record CreateLotRequest(
            @NotNull Long itemId,
            String lotNo,
            Boolean autoGenerate,
            LotOriginType originType,
            String originDocType,
            Long originDocId,
            String p1,
            String p2,
            @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate expiryDate,
            String certificateRef,
            String remark
    ) {
    }

    public record UpdateLotRequest(
            @NotNull LotStatus status,
            String p1,
            String p2,
            @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate expiryDate,
            String certificateRef,
            String remark
    ) {
    }
}
