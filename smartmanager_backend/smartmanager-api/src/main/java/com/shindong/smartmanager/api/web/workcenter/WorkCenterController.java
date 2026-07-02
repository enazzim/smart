package com.shindong.smartmanager.api.web.workcenter;

import com.shindong.smartmanager.application.workcenter.WorkCenterRepository;
import com.shindong.smartmanager.application.workcenter.WorkCenterView;
import java.util.List;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/basis/work-centers")
public class WorkCenterController {

    private final WorkCenterRepository workCenterRepository;

    public WorkCenterController(WorkCenterRepository workCenterRepository) {
        this.workCenterRepository = workCenterRepository;
    }

    @GetMapping
    public List<WorkCenterResponse> list() {
        return workCenterRepository.findAllActive().stream()
                .map(WorkCenterResponse::from)
                .toList();
    }

    public record WorkCenterResponse(long id, String wcName, long mainProcessCodeId) {
        static WorkCenterResponse from(WorkCenterView view) {
            return new WorkCenterResponse(view.id(), view.wcName(), view.mainProcessCodeId());
        }
    }
}
