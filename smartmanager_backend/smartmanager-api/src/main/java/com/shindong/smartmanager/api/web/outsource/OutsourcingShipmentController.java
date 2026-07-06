package com.shindong.smartmanager.api.web.outsource;

import com.shindong.smartmanager.application.outsource.CreateOutsourcingShipmentCommand;
import com.shindong.smartmanager.application.outsource.CreateOutsourcingShipmentLineCommand;
import com.shindong.smartmanager.application.outsource.OutsourcingShipmentInputPreviewView;
import com.shindong.smartmanager.application.outsource.OutsourcingShipmentListCriteria;
import com.shindong.smartmanager.domain.outsource.OutsourcingShipmentStatus;
import com.shindong.smartmanager.infrastructure.application.OutsourcingShipmentApplicationService;
import com.shindong.smartmanager.api.security.SecurityUtils;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.HttpStatus;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/outsource/shipments")
public class OutsourcingShipmentController {

    private final OutsourcingShipmentApplicationService outsourcingShipmentApplicationService;

    public OutsourcingShipmentController(OutsourcingShipmentApplicationService outsourcingShipmentApplicationService) {
        this.outsourcingShipmentApplicationService = outsourcingShipmentApplicationService;
    }

    @GetMapping("/candidates")
    @PreAuthorize("hasAuthority('outsource:shipment:read')")
    public List<OutsourcingShipmentCandidateResponse> listCandidates() {
        return outsourcingShipmentApplicationService.listCandidates().stream()
                .map(OutsourcingShipmentCandidateResponse::from)
                .toList();
    }

    @GetMapping("/input-preview")
    @PreAuthorize("hasAuthority('outsource:shipment:read')")
    public OutsourcingShipmentInputPreviewResponse previewInput(
            @RequestParam long orderLineId,
            @RequestParam BigDecimal shipmentQty,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate shipmentDate
    ) {
        OutsourcingShipmentInputPreviewView view = outsourcingShipmentApplicationService.previewInput(
                orderLineId, shipmentQty, shipmentDate);
        return OutsourcingShipmentInputPreviewResponse.from(view);
    }

    @GetMapping
    @PreAuthorize("hasAuthority('outsource:shipment:read')")
    public List<OutsourcingShipmentResponse> list(
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate shipmentDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate shipmentDateTo,
            @RequestParam(required = false) String shipmentNo,
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) OutsourcingShipmentStatus status,
            @RequestParam(defaultValue = "true") boolean excludeCancelled
    ) {
        return outsourcingShipmentApplicationService.list(new OutsourcingShipmentListCriteria(
                shipmentDateFrom,
                shipmentDateTo,
                shipmentNo,
                partnerName,
                status,
                excludeCancelled
        )).stream().map(OutsourcingShipmentResponse::from).toList();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('outsource:shipment:write')")
    public OutsourcingShipmentResponse create(@Valid @RequestBody CreateOutsourcingShipmentRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return OutsourcingShipmentResponse.from(outsourcingShipmentApplicationService.register(
                new CreateOutsourcingShipmentCommand(
                        request.shipmentDate(),
                        request.lines().stream()
                                .map(line -> new CreateOutsourcingShipmentLineCommand(
                                        line.orderLineId(),
                                        line.shipmentQty()
                                ))
                                .toList()
                ),
                principal.loginId()
        ));
    }

    @PostMapping("/{id}/cancel")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('outsource:shipment:write')")
    public void cancel(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        outsourcingShipmentApplicationService.cancel(id, principal.loginId());
    }

    public record CreateOutsourcingShipmentRequest(
            @NotNull LocalDate shipmentDate,
            @NotEmpty List<CreateOutsourcingShipmentLineRequest> lines
    ) {
    }

    public record CreateOutsourcingShipmentLineRequest(
            @NotNull Long orderLineId,
            @NotNull BigDecimal shipmentQty
    ) {
    }
}
