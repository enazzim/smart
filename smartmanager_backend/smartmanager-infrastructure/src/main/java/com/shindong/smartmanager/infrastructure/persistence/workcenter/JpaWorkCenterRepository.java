package com.shindong.smartmanager.infrastructure.persistence.workcenter;

import com.shindong.smartmanager.application.workcenter.WorkCenterRepository;
import com.shindong.smartmanager.application.workcenter.WorkCenterView;
import java.util.List;
import org.springframework.stereotype.Repository;

@Repository
public class JpaWorkCenterRepository implements WorkCenterRepository {

    private final SpringDataWorkCenterRepository workCenterRepository;

    public JpaWorkCenterRepository(SpringDataWorkCenterRepository workCenterRepository) {
        this.workCenterRepository = workCenterRepository;
    }

    @Override
    public List<WorkCenterView> findAllActive() {
        return workCenterRepository.findByRecordingStateOrderByWcNameAsc(1).stream()
                .map(wc -> new WorkCenterView(wc.getId(), wc.getWcName(), wc.getMainProcessCodeId()))
                .toList();
    }
}
