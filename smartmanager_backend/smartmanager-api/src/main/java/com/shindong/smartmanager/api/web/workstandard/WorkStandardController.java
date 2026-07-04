package com.shindong.smartmanager.api.web.workstandard;

import com.shindong.smartmanager.application.workstandard.WorkStandardUpdateCommand;
import com.shindong.smartmanager.infrastructure.application.WorkStandardApplicationService;
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
@RequestMapping("/api/v1/basis/work-standards/plan")
public class WorkStandardController {

    private static final String DEFAULT_ACTOR = "local-dev";

    private final WorkStandardApplicationService workStandardApplicationService;

    public WorkStandardController(WorkStandardApplicationService workStandardApplicationService) {
        this.workStandardApplicationService = workStandardApplicationService;
    }

    @GetMapping
    public List<WorkStandardResponse> list(@RequestParam(required = false) String itemNum) {
        return workStandardApplicationService.listActive(itemNum).stream()
                .map(WorkStandardResponse::from)
                .toList();
    }

    @GetMapping("/{id}")
    public WorkStandardResponse get(@PathVariable long id) {
        return WorkStandardResponse.from(workStandardApplicationService.getActive(id));
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    public WorkStandardResponse create(
            @Valid @RequestBody CreateWorkStandardRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        return WorkStandardResponse.from(workStandardApplicationService.register(
                request.itemNum(),
                request.processSequenceId(),
                request.workCenterId(),
                request.equipmentId(),
                request.priorityOrder(),
                request.mainWorkerId(),
                request.toolName(),
                request.setupTime(),
                request.standardTime(),
                actor
        ));
    }

    @PutMapping("/{id}")
    public WorkStandardResponse update(
            @PathVariable long id,
            @Valid @RequestBody UpdateWorkStandardRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        WorkStandardUpdateCommand command = new WorkStandardUpdateCommand(
                request.workCenterId(),
                request.equipmentId(),
                request.priorityOrder(),
                request.mainWorkerId(),
                request.toolName(),
                request.setupTime(),
                request.standardTime()
        );
        return WorkStandardResponse.from(workStandardApplicationService.update(id, command, actor));
    }

    @DeleteMapping("/{id}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    public void delete(
            @PathVariable long id,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        workStandardApplicationService.delete(id, actor);
    }

    @PostMapping("/copy")
    public CopyWorkStandardResponse copy(
            @Valid @RequestBody CopyWorkStandardRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        int copied = workStandardApplicationService.copyStandards(
                request.sourceItemNum(),
                request.targetItemNum(),
                actor
        );
        return new CopyWorkStandardResponse(copied);
    }
}
