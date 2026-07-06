package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.quality.CompleteQualityInspectionCommand;
import com.shindong.smartmanager.application.quality.QualityInspectionListCriteria;
import com.shindong.smartmanager.application.quality.QualityInspectionService;
import com.shindong.smartmanager.application.quality.QualityInspectionView;
import com.shindong.smartmanager.domain.quality.QualityInspectionSourceType;
import com.shindong.smartmanager.domain.quality.QualityInspectionStatus;
import java.time.LocalDate;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class QualityInspectionApplicationService {

    private final QualityInspectionService qualityInspectionService;

    public QualityInspectionApplicationService(QualityInspectionService qualityInspectionService) {
        this.qualityInspectionService = qualityInspectionService;
    }

    @Transactional(readOnly = true)
    public List<QualityInspectionView> list(QualityInspectionListCriteria criteria) {
        return qualityInspectionService.list(criteria);
    }

    @Transactional(readOnly = true)
    public QualityInspectionView get(long id) {
        return qualityInspectionService.get(id);
    }

    @Transactional
    public QualityInspectionView complete(long id, CompleteQualityInspectionCommand command, String actorUserId) {
        return qualityInspectionService.complete(id, command, actorUserId);
    }

    @Transactional
    public void cancel(long id, String actorUserId) {
        qualityInspectionService.cancel(id, actorUserId);
    }
}
