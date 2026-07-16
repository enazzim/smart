package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.purchase.ApproveOffsetResultView;
import com.shindong.smartmanager.application.purchase.PayableApprovalCriteria;
import com.shindong.smartmanager.application.purchase.PayableApprovalItemCommand;
import com.shindong.smartmanager.application.purchase.PayableApprovalService;
import com.shindong.smartmanager.application.purchase.PayableApprovalView;
import com.shindong.smartmanager.application.purchase.UpdatePayableApprovalFiscalPeriodCommand;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class PayableApprovalApplicationService {

    private final PayableApprovalService payableApprovalService;

    public PayableApprovalApplicationService(PayableApprovalService payableApprovalService) {
        this.payableApprovalService = payableApprovalService;
    }

    @Transactional(readOnly = true)
    public List<PayableApprovalView> listPending(PayableApprovalCriteria criteria) {
        return payableApprovalService.listPending(criteria);
    }

    @Transactional(readOnly = true)
    public List<PayableApprovalView> listApproved(PayableApprovalCriteria criteria) {
        return payableApprovalService.listApproved(criteria);
    }

    @Transactional(readOnly = true)
    public ApproveOffsetResultView preview(List<PayableApprovalItemCommand> items) {
        return payableApprovalService.preview(items);
    }

    @Transactional
    public ApproveOffsetResultView approve(List<PayableApprovalItemCommand> items, long userId, String actorUserId) {
        return payableApprovalService.approve(items, userId, actorUserId);
    }

    @Transactional
    public void cancelApproval(List<PayableApprovalItemCommand> items, long userId, String actorUserId) {
        payableApprovalService.cancelApproval(items, userId, actorUserId);
    }

    @Transactional
    public void updateFiscalPeriod(UpdatePayableApprovalFiscalPeriodCommand command, String actorUserId) {
        payableApprovalService.updateFiscalPeriod(command, actorUserId);
    }
}
