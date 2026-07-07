package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.workdiary.ApproveWorkDiaryCommand;
import com.shindong.smartmanager.application.workdiary.CancelWorkDiaryApprovalCommand;
import com.shindong.smartmanager.application.workdiary.CreateWorkDiaryCommand;
import com.shindong.smartmanager.application.workdiary.SubmitWorkDiaryCommand;
import com.shindong.smartmanager.application.workdiary.UpdateWorkDiaryCommand;
import com.shindong.smartmanager.application.workdiary.WorkDiaryApplicationService;
import com.shindong.smartmanager.application.workdiary.WorkDiaryApproveResult;
import com.shindong.smartmanager.application.workdiary.WorkDiaryCancelApprovalResult;
import com.shindong.smartmanager.application.workdiary.WorkDiaryDetailView;
import com.shindong.smartmanager.application.workdiary.WorkDiaryListCriteria;
import com.shindong.smartmanager.application.workdiary.WorkDiaryPageView;
import com.shindong.smartmanager.application.workdiary.WorkDiarySubmitResult;
import com.shindong.smartmanager.application.workdiary.WorkDiaryService;
import com.shindong.smartmanager.application.workdiary.WorkDiaryTemplateView;
import java.time.LocalDate;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class WorkDiaryApplicationServiceImpl implements WorkDiaryApplicationService {

    private final WorkDiaryService workDiaryService;

    public WorkDiaryApplicationServiceImpl(WorkDiaryService workDiaryService) {
        this.workDiaryService = workDiaryService;
    }

    @Override
    @Transactional(readOnly = true)
    public WorkDiaryTemplateView getMyTemplate(long userId) {
        return workDiaryService.getMyTemplate(userId);
    }

    @Override
    @Transactional(readOnly = true)
    public WorkDiaryPageView list(WorkDiaryListCriteria criteria) {
        return workDiaryService.list(criteria);
    }

    @Override
    @Transactional(readOnly = true)
    public WorkDiaryDetailView getDetail(long id, long actorUserId, boolean approver) {
        return workDiaryService.getDetail(id, actorUserId, approver);
    }

    @Override
    @Transactional(readOnly = true)
    public WorkDiaryDetailView getByDate(LocalDate workDate, long actorUserId) {
        return workDiaryService.getByDate(workDate, actorUserId);
    }

    @Override
    @Transactional
    public WorkDiaryDetailView create(CreateWorkDiaryCommand command) {
        return workDiaryService.create(command);
    }

    @Override
    @Transactional
    public WorkDiaryDetailView update(UpdateWorkDiaryCommand command) {
        return workDiaryService.update(command);
    }

    @Override
    @Transactional
    public void delete(long id, long actorUserId, String actorLoginId, String actorUserIdText) {
        workDiaryService.delete(id, actorUserId, actorLoginId, actorUserIdText);
    }

    @Override
    @Transactional
    public WorkDiarySubmitResult submit(SubmitWorkDiaryCommand command) {
        return workDiaryService.submit(command);
    }

    @Override
    @Transactional
    public WorkDiaryApproveResult approve(ApproveWorkDiaryCommand command) {
        return workDiaryService.approve(command);
    }

    @Override
    @Transactional
    public WorkDiaryCancelApprovalResult cancelApproval(CancelWorkDiaryApprovalCommand command) {
        return workDiaryService.cancelApproval(command);
    }
}

