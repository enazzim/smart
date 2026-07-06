package com.shindong.smartmanager.api.web.production;

import java.util.List;

public record MrpCalculateRequest(List<Long> productionPlanIds) {
}
