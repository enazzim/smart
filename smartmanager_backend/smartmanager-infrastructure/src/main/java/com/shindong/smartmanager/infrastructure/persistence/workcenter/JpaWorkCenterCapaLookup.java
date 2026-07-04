package com.shindong.smartmanager.infrastructure.persistence.workcenter;

import com.shindong.smartmanager.application.calendar.WorkCenterCapaLookup;
import com.shindong.smartmanager.application.calendar.WorkCenterCapaProfile;
import org.springframework.stereotype.Repository;

@Repository
public class JpaWorkCenterCapaLookup implements WorkCenterCapaLookup {

    private final SpringDataWorkCenterRepository workCenterRepository;

    public JpaWorkCenterCapaLookup(SpringDataWorkCenterRepository workCenterRepository) {
        this.workCenterRepository = workCenterRepository;
    }

    @Override
    public WorkCenterCapaProfile findActiveCapaProfile(long workCenterId) {
        WorkCenterJpaEntity workCenter = workCenterRepository.findById(workCenterId)
                .filter(wc -> wc.getRecordingState() == 1)
                .orElseThrow(() -> new IllegalArgumentException("작업장을 찾을 수 없습니다: " + workCenterId));
        return new WorkCenterCapaProfile(
                workCenter.getOperationTime(),
                workCenter.getRetentionStaff(),
                workCenter.getCapacityDistinction()
        );
    }
}
