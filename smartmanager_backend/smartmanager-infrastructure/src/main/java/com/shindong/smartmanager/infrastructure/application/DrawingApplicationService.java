package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.drawing.DrawingHistoryDetailView;
import com.shindong.smartmanager.application.drawing.DrawingHistoryView;
import com.shindong.smartmanager.application.drawing.DrawingInfoUpdateCommand;
import com.shindong.smartmanager.application.drawing.DrawingListView;
import com.shindong.smartmanager.application.drawing.DrawingReferenceCandidateView;
import com.shindong.smartmanager.application.drawing.DrawingReferenceChildCommand;
import com.shindong.smartmanager.application.drawing.DrawingReferenceIntegrityIssue;
import com.shindong.smartmanager.application.drawing.DrawingReferenceService;
import com.shindong.smartmanager.application.drawing.DrawingReferenceView;
import com.shindong.smartmanager.application.drawing.DrawingRegisterCommand;
import com.shindong.smartmanager.application.drawing.DrawingReviseCommand;
import com.shindong.smartmanager.application.drawing.DrawingService;
import com.shindong.smartmanager.application.drawing.DrawingWhereUsedView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class DrawingApplicationService {

    private final DrawingService drawingService;
    private final DrawingReferenceService drawingReferenceService;

    public DrawingApplicationService(
            DrawingService drawingService,
            DrawingReferenceService drawingReferenceService
    ) {
        this.drawingService = drawingService;
        this.drawingReferenceService = drawingReferenceService;
    }

    @Transactional
    public String register(DrawingRegisterCommand command, String actorUserId) {
        return drawingService.register(command, actorUserId);
    }

    @Transactional(readOnly = true)
    public List<DrawingListView> listActive() {
        return drawingService.listActive();
    }

    @Transactional(readOnly = true)
    public List<DrawingListView> listDeleted() {
        return drawingService.listDeleted();
    }

    @Transactional(readOnly = true)
    public List<DrawingHistoryView> listHistories(String masterId) {
        return drawingService.listHistories(masterId);
    }

    @Transactional(readOnly = true)
    public DrawingHistoryDetailView getHistoryById(String historyId) {
        return drawingService.getHistoryById(historyId);
    }

    @Transactional(readOnly = true)
    public DrawingHistoryDetailView getLatestHistoryByPartNo(String partNo) {
        return drawingService.getLatestHistoryByPartNo(partNo);
    }

    @Transactional
    public String revise(String partNo, DrawingReviseCommand command, String actorUserId) {
        return drawingService.revise(partNo, command, actorUserId);
    }

    @Transactional
    public void softDelete(String partNo, String actorUserId) {
        drawingService.softDelete(partNo, actorUserId);
    }

    @Transactional
    public void restore(String masterId, String actorUserId) {
        drawingService.restore(masterId, actorUserId);
    }

    @Transactional
    public void updateInfo(String masterId, DrawingInfoUpdateCommand command, String actorUserId) {
        drawingService.updateInfo(masterId, command, actorUserId);
    }

    @Transactional
    public void hardDelete(String masterId, String actorUserId) {
        drawingService.hardDelete(masterId, actorUserId);
    }

    @Transactional
    public String promoteToProd(String partNo, String actorUserId) {
        return drawingService.promoteToProd(partNo, actorUserId);
    }

    @Transactional(readOnly = true)
    public boolean existsActivePartNo(String partNo) {
        return drawingService.existsActivePartNo(partNo);
    }

    @Transactional(readOnly = true)
    public List<DrawingReferenceView> listReferences(String masterId, String historyId) {
        return drawingReferenceService.listContains(masterId, historyId);
    }

    @Transactional(readOnly = true)
    public List<DrawingWhereUsedView> listWhereUsed(String historyId) {
        return drawingReferenceService.listWhereUsed(historyId);
    }

    @Transactional
    public void replaceReferences(
            String masterId,
            String historyId,
            List<DrawingReferenceChildCommand> children,
            String actorUserId
    ) {
        drawingReferenceService.replaceReferences(masterId, historyId, children, actorUserId);
    }

    @Transactional(readOnly = true)
    public List<DrawingReferenceIntegrityIssue> scanReferenceIntegrity() {
        return drawingReferenceService.scanIntegrity();
    }

    @Transactional(readOnly = true)
    public List<DrawingReferenceCandidateView> listBomReferenceCandidates(String masterId) {
        return drawingReferenceService.listBomCandidates(masterId);
    }
}
