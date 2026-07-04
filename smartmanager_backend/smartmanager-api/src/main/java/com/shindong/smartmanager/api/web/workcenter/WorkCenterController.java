package com.shindong.smartmanager.api.web.workcenter;

import com.shindong.smartmanager.api.security.BasisAuthorize;
import com.shindong.smartmanager.application.workcenter.WorkCenterCommand;
import com.shindong.smartmanager.infrastructure.application.WorkCenterApplicationService;
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
@RequestMapping("/api/v1/basis/work-centers")
@BasisAuthorize.WorkCenterRead
public class WorkCenterController {

    private static final String DEFAULT_ACTOR = "local-dev";

    private final WorkCenterApplicationService workCenterApplicationService;

    public WorkCenterController(WorkCenterApplicationService workCenterApplicationService) {
        this.workCenterApplicationService = workCenterApplicationService;
    }

    @GetMapping
    public List<WorkCenterResponse> list(@RequestParam(required = false) String q) {
        return workCenterApplicationService.listActive(q).stream()
                .map(WorkCenterResponse::from)
                .toList();
    }

    @GetMapping("/{id}")
    public WorkCenterResponse get(@PathVariable long id) {
        return WorkCenterResponse.from(workCenterApplicationService.getActive(id));
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @BasisAuthorize.WorkCenterWrite
    public WorkCenterResponse create(
            @Valid @RequestBody CreateWorkCenterRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        WorkCenterCommand command = toCommand(request);
        return WorkCenterResponse.from(workCenterApplicationService.register(command, actor));
    }

    @PutMapping("/{id}")
    @BasisAuthorize.WorkCenterWrite
    public WorkCenterResponse update(
            @PathVariable long id,
            @Valid @RequestBody CreateWorkCenterRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        WorkCenterCommand command = toCommand(request);
        return WorkCenterResponse.from(workCenterApplicationService.update(id, command, actor));
    }

    @DeleteMapping("/{id}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @BasisAuthorize.WorkCenterWrite
    public void delete(
            @PathVariable long id,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        workCenterApplicationService.delete(id, actor);
    }

    private static WorkCenterCommand toCommand(CreateWorkCenterRequest request) {
        return new WorkCenterCommand(
                request.wcName(),
                request.mainProcessCodeId(),
                request.operationTime()
        );
    }
}
