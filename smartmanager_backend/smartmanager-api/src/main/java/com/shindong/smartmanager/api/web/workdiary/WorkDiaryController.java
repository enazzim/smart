package com.shindong.smartmanager.api.web.workdiary;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.workdiary.ApproveWorkDiaryCommand;
import com.shindong.smartmanager.application.workdiary.CancelWorkDiaryApprovalCommand;
import com.shindong.smartmanager.application.workdiary.CreateWorkDiaryCommand;
import com.shindong.smartmanager.application.workdiary.SubmitWorkDiaryCommand;
import com.shindong.smartmanager.application.workdiary.UpdateWorkDiaryCommand;
import com.shindong.smartmanager.application.workdiary.WorkDiaryApplicationService;
import com.shindong.smartmanager.application.workdiary.WorkDiaryDetailView;
import com.shindong.smartmanager.application.workdiary.WorkDiaryListCriteria;
import com.shindong.smartmanager.application.workdiary.WorkDiaryListItemView;
import com.shindong.smartmanager.application.workdiary.WorkDiaryPageView;
import com.shindong.smartmanager.application.workdiary.WorkDiaryTemplateView;
import jakarta.validation.Valid;
import java.time.LocalDate;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.HttpStatus;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@Validated
@RequestMapping("/api/v1/work-diaries")
public class WorkDiaryController {

    private final WorkDiaryApplicationService workDiaryApplicationService;

    public WorkDiaryController(WorkDiaryApplicationService workDiaryApplicationService) {
        this.workDiaryApplicationService = workDiaryApplicationService;
    }

    @GetMapping("/my-template")
    @PreAuthorize("hasAnyAuthority('community:workdiary:read','community:workdiary:write','community:workdiary:approve')")
    public WorkDiaryTemplateResponse myTemplate() {
        var principal = SecurityUtils.requirePrincipal();
        WorkDiaryTemplateView view = workDiaryApplicationService.getMyTemplate(principal.userId());
        return new WorkDiaryTemplateResponse(
                view.templateCode(),
                view.workDiaryGroupId(),
                view.workDiaryGroupName(),
                view.templateName(),
                view.fieldSchema()
        );
    }

    @GetMapping
    @PreAuthorize("hasAnyAuthority('community:workdiary:read','community:workdiary:write','community:workdiary:approve')")
    public WorkDiaryPageResponse list(@Valid @ModelAttribute WorkDiaryListQuery query) {
        var principal = SecurityUtils.requirePrincipal();
        boolean approver = principal.authorities().contains("community:workdiary:approve");
        WorkDiaryPageView page = workDiaryApplicationService.list(new WorkDiaryListCriteria(
                principal.userId(),
                approver,
                query.fromDate(),
                query.toDate(),
                query.status() != null ? toApplicationStatus(query.status()) : null,
                query.authorUserId(),
                query.page() != null ? query.page() : 0,
                query.size() != null ? query.size() : 20
        ));
        return new WorkDiaryPageResponse(
                page.items().stream().map(this::toListItemResponse).toList(),
                page.totalElements(),
                page.page(),
                page.size()
        );
    }

    @GetMapping("/{id}")
    @PreAuthorize("hasAnyAuthority('community:workdiary:read','community:workdiary:write','community:workdiary:approve')")
    public WorkDiaryDetailResponse get(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        boolean approver = principal.authorities().contains("community:workdiary:approve");
        return toDetailResponse(workDiaryApplicationService.getDetail(id, principal.userId(), approver));
    }

    @GetMapping("/by-date/{workDate}")
    @PreAuthorize("hasAnyAuthority('community:workdiary:read','community:workdiary:write','community:workdiary:approve')")
    public WorkDiaryDetailResponse getByDate(
            @PathVariable @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate workDate
    ) {
        var principal = SecurityUtils.requirePrincipal();
        return toDetailResponse(workDiaryApplicationService.getByDate(workDate, principal.userId()));
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('community:workdiary:write')")
    public WorkDiaryDetailResponse create(@Valid @RequestBody CreateWorkDiaryRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return toDetailResponse(workDiaryApplicationService.create(new CreateWorkDiaryCommand(
                request.workDate(),
                request.fieldValues(),
                request.listed(),
                request.closingNote(),
                toApplicationStatus(request.status()),
                principal.userId(),
                principal.loginId(),
                String.valueOf(principal.userId())
        )));
    }

    @PutMapping("/{id}")
    @PreAuthorize("hasAuthority('community:workdiary:write')")
    public WorkDiaryDetailResponse update(@PathVariable long id, @Valid @RequestBody UpdateWorkDiaryRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return toDetailResponse(workDiaryApplicationService.update(new UpdateWorkDiaryCommand(
                id,
                request.fieldValues(),
                request.listed(),
                request.closingNote(),
                principal.userId(),
                principal.loginId(),
                String.valueOf(principal.userId())
        )));
    }

    @DeleteMapping("/{id}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('community:workdiary:write')")
    public void delete(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        workDiaryApplicationService.delete(
                id,
                principal.userId(),
                principal.loginId(),
                String.valueOf(principal.userId())
        );
    }

    @PostMapping("/{id}/submit")
    @PreAuthorize("hasAuthority('community:workdiary:write')")
    public WorkDiarySubmitResponse submit(@PathVariable long id, @Valid @RequestBody SubmitWorkDiaryRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        var result = workDiaryApplicationService.submit(new SubmitWorkDiaryCommand(
                id,
                request.comment(),
                principal.userId(),
                principal.loginId(),
                String.valueOf(principal.userId())
        ));
        return new WorkDiarySubmitResponse(
                result.id(),
                toApiStatus(result.status()),
                result.submittedAt()
        );
    }

    @PostMapping("/{id}/approve")
    @PreAuthorize("hasAuthority('community:workdiary:approve')")
    public WorkDiaryApproveResponse approve(@PathVariable long id, @Valid @RequestBody ApproveWorkDiaryRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        var result = workDiaryApplicationService.approve(new ApproveWorkDiaryCommand(
                id,
                request.directiveNote(),
                principal.userId(),
                principal.loginId(),
                String.valueOf(principal.userId())
        ));
        return new WorkDiaryApproveResponse(
                result.id(),
                toApiStatus(result.status()),
                result.directiveNote(),
                result.approvedAt(),
                result.approvedByUserId(),
                result.approvedByName()
        );
    }

    @PostMapping("/{id}/cancel-approval")
    @PreAuthorize("hasAuthority('community:workdiary:approve')")
    public WorkDiaryCancelApprovalResponse cancelApproval(
            @PathVariable long id,
            @Valid @RequestBody CancelWorkDiaryApprovalRequest request
    ) {
        var principal = SecurityUtils.requirePrincipal();
        var result = workDiaryApplicationService.cancelApproval(new CancelWorkDiaryApprovalCommand(
                id,
                request.reason(),
                principal.userId(),
                principal.loginId(),
                String.valueOf(principal.userId())
        ));
        return new WorkDiaryCancelApprovalResponse(
                result.id(),
                toApiStatus(result.status()),
                result.approvalCanceledAt(),
                result.approvalCanceledByUserId(),
                result.approvalCanceledByName()
        );
    }

    private WorkDiaryListItemResponse toListItemResponse(WorkDiaryListItemView view) {
        return new WorkDiaryListItemResponse(
                view.id(),
                view.workDate(),
                view.workDateTitle(),
                view.authorUserId(),
                view.authorName(),
                view.workDiaryGroupId(),
                view.workDiaryGroupName(),
                view.templateCode(),
                toApiStatus(view.status()),
                view.listed(),
                view.approvedAt(),
                view.directiveNotePreview()
        );
    }

    private WorkDiaryDetailResponse toDetailResponse(WorkDiaryDetailView view) {
        return new WorkDiaryDetailResponse(
                view.id(),
                view.workDate(),
                view.workDateTitle(),
                view.authorUserId(),
                view.authorName(),
                view.workDiaryGroupId(),
                view.workDiaryGroupName(),
                view.templateCode(),
                view.templateName(),
                view.fieldSchema(),
                view.fieldValues(),
                view.directiveNote(),
                toApiStatus(view.status()),
                view.listed(),
                view.closingNote(),
                view.submittedAt(),
                view.approvedAt(),
                view.approvedByUserId(),
                view.approvedByName(),
                view.approvalCanceledAt(),
                view.approvalCanceledByUserId(),
                view.approvalCanceledByName(),
                view.canEdit(),
                view.canDelete(),
                view.canApprove(),
                view.canCancelApproval(),
                view.createdAt(),
                view.updatedAt()
        );
    }

    private com.shindong.smartmanager.application.workdiary.WorkDiaryStatus toApplicationStatus(WorkDiaryStatus status) {
        return com.shindong.smartmanager.application.workdiary.WorkDiaryStatus.valueOf(status.name());
    }

    private WorkDiaryStatus toApiStatus(com.shindong.smartmanager.application.workdiary.WorkDiaryStatus status) {
        return WorkDiaryStatus.valueOf(status.name());
    }
}

