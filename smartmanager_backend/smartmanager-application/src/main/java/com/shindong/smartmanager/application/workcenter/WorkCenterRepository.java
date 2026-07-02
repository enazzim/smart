package com.shindong.smartmanager.application.workcenter;

import java.util.List;

public interface WorkCenterRepository {

    List<WorkCenterView> findAllActive();
}
