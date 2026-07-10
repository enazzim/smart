package com.shindong.smartmanager.application.workdiary;

import java.time.LocalDate;
import java.util.List;

public interface WorkDiaryApplicationService {

    WorkDiaryTemplateView getMyTemplate(long userId);

    List<WorkDiaryTemplateView> listTemplates();

    WorkDiaryTemplateView updateTemplate(UpdateWorkDiaryTemplateCommand command);

    WorkDiaryPageView list(WorkDiaryListCriteria criteria);

    boolean isApprover(long actorUserId);

    WorkDiaryDetailView getDetail(long id, long actorUserId, boolean approver);

    WorkDiaryDetailView getByDate(LocalDate workDate, long actorUserId);

    WorkDiaryDetailView create(CreateWorkDiaryCommand command);

    WorkDiaryDetailView update(UpdateWorkDiaryCommand command);

    void delete(long id, long actorUserId, String actorLoginId, String actorUserIdText);

    WorkDiarySubmitResult submit(SubmitWorkDiaryCommand command);

    WorkDiaryApproveResult approve(ApproveWorkDiaryCommand command);

    WorkDiaryCancelApprovalResult cancelApproval(CancelWorkDiaryApprovalCommand command);
}

