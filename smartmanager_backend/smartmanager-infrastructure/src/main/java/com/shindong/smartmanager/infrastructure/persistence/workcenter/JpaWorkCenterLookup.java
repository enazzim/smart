package com.shindong.smartmanager.infrastructure.persistence.workcenter;

import com.shindong.smartmanager.application.process.WorkCenterLookup;
import org.springframework.stereotype.Repository;

@Repository
public class JpaWorkCenterLookup implements WorkCenterLookup {

    private final SpringDataWorkCenterRepository workCenterRepository;

    public JpaWorkCenterLookup(SpringDataWorkCenterRepository workCenterRepository) {
        this.workCenterRepository = workCenterRepository;
    }

    @Override
    public boolean existsActive(long workCenterId) {
        return workCenterRepository.findById(workCenterId)
                .filter(wc -> wc.getRecordingState() == 1)
                .isPresent();
    }

    @Override
    public String findActiveName(long workCenterId) {
        return workCenterRepository.findById(workCenterId)
                .filter(wc -> wc.getRecordingState() == 1)
                .map(WorkCenterJpaEntity::getWcName)
                .orElse(null);
    }
}
