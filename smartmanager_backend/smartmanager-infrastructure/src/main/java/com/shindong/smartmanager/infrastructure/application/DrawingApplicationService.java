package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.drawing.DrawingHistoryDetailView;
import com.shindong.smartmanager.application.drawing.DrawingHistoryView;
import com.shindong.smartmanager.application.drawing.DrawingInfoUpdateCommand;
import com.shindong.smartmanager.application.drawing.DrawingListView;
import com.shindong.smartmanager.application.drawing.DrawingRegisterCommand;
import com.shindong.smartmanager.application.drawing.DrawingReviseCommand;
import com.shindong.smartmanager.application.drawing.DrawingService;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class DrawingApplicationService {

    private final DrawingService drawingService;

    public DrawingApplicationService(DrawingService drawingService) {
        this.drawingService = drawingService;
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
}
