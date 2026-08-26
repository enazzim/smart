package com.shindong.smartmanager.api.web.process;

import com.shindong.smartmanager.api.security.BasisAuthorize;
import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.process.ProcessCommand;
import com.shindong.smartmanager.application.process.ProcessUpdateCommand;
import com.shindong.smartmanager.infrastructure.application.ProcessApplicationService;
import jakarta.validation.Valid;
import java.util.List;
import org.springframework.http.HttpStatus;
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
@RequestMapping("/api/v1/basis/processes/plan")
@BasisAuthorize.ProcessRead
public class ProcessController {

    private final ProcessApplicationService processApplicationService;

    public ProcessController(ProcessApplicationService processApplicationService) {
        this.processApplicationService = processApplicationService;
    }

    @GetMapping
    public List<ProcessResponse> list(@RequestParam(required = false) Long itemId) {
        if (itemId == null) {
            return processApplicationService.listActiveAll().stream()
                    .map(ProcessResponse::from)
                    .toList();
        }
        return processApplicationService.listActiveByItemId(itemId).stream()
                .map(ProcessResponse::from)
                .toList();
    }

    @GetMapping("/{id}")
    public ProcessResponse get(@PathVariable long id) {
        return ProcessResponse.from(processApplicationService.getActive(id));
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @BasisAuthorize.ProcessWrite
    public ProcessResponse create(
            @Valid @RequestBody CreateProcessRequest request
    ) {
        String actor = SecurityUtils.requireLoginId();
        ProcessCommand command = toCommand(request);
        return ProcessResponse.from(processApplicationService.register(command, actor));
    }

    @PutMapping("/{id}")
    @BasisAuthorize.ProcessWrite
    public ProcessResponse update(
            @PathVariable long id,
            @Valid @RequestBody UpdateProcessRequest request
    ) {
        String actor = SecurityUtils.requireLoginId();
        ProcessUpdateCommand command = toUpdateCommand(request);
        return ProcessResponse.from(processApplicationService.update(id, command, actor));
    }

    @DeleteMapping("/{id}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @BasisAuthorize.ProcessWrite
    public void delete(@PathVariable long id) {
        processApplicationService.delete(id, SecurityUtils.requireLoginId());
    }

    private ProcessCommand toCommand(CreateProcessRequest request) {
        return new ProcessCommand(
                request.itemId(),
                request.processSequenceNum(),
                request.processCodeId(),
                request.workDistinction(),
                request.workCenterId(),
                request.outsideOrderRate() != null ? request.outsideOrderRate() : 0,
                request.progressRate()
        );
    }

    private ProcessUpdateCommand toUpdateCommand(UpdateProcessRequest request) {
        return new ProcessUpdateCommand(
                request.itemId(),
                request.processSequenceNum(),
                request.processCodeId(),
                request.workDistinction(),
                request.workCenterId(),
                request.outsideOrderRate() != null ? request.outsideOrderRate() : 0,
                request.progressRate()
        );
    }
}
