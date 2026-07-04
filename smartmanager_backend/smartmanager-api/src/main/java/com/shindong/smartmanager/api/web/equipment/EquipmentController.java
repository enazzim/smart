package com.shindong.smartmanager.api.web.equipment;

import com.shindong.smartmanager.api.security.BasisAuthorize;
import com.shindong.smartmanager.application.equipment.EquipmentCommand;
import com.shindong.smartmanager.application.equipment.EquipmentUpdateCommand;
import com.shindong.smartmanager.infrastructure.application.EquipmentApplicationService;
import jakarta.validation.Valid;
import java.util.List;
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
@RequestMapping("/api/v1/basis/equipment")
@BasisAuthorize.EquipmentRead
public class EquipmentController {

    private static final String DEFAULT_ACTOR = "local-dev";

    private final EquipmentApplicationService equipmentApplicationService;

    public EquipmentController(EquipmentApplicationService equipmentApplicationService) {
        this.equipmentApplicationService = equipmentApplicationService;
    }

    @GetMapping
    public List<EquipmentResponse> list(@RequestParam(required = false) String q) {
        return equipmentApplicationService.listActive(q).stream()
                .map(EquipmentResponse::from)
                .toList();
    }

    @GetMapping("/{id}")
    public EquipmentResponse get(@PathVariable long id) {
        return EquipmentResponse.from(equipmentApplicationService.getActive(id));
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @BasisAuthorize.EquipmentWrite
    public EquipmentResponse create(
            @Valid @RequestBody CreateEquipmentRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        EquipmentCommand command = new EquipmentCommand(
                request.equipmentNum(),
                request.equipmentName(),
                request.equipmentCategoryId(),
                request.workCenterId(),
                request.designShot(),
                request.initialShot()
        );
        return EquipmentResponse.from(equipmentApplicationService.register(command, actor));
    }

    @PutMapping("/{id}")
    @BasisAuthorize.EquipmentWrite
    public EquipmentResponse update(
            @PathVariable long id,
            @Valid @RequestBody UpdateEquipmentRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        EquipmentUpdateCommand command = new EquipmentUpdateCommand(
                request.equipmentName(),
                request.equipmentCategoryId(),
                request.workCenterId(),
                request.designShot(),
                request.initialShot()
        );
        return EquipmentResponse.from(equipmentApplicationService.update(id, command, actor));
    }

    @DeleteMapping("/{id}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @BasisAuthorize.EquipmentWrite
    public void delete(
            @PathVariable long id,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        equipmentApplicationService.delete(id, actor);
    }
}
