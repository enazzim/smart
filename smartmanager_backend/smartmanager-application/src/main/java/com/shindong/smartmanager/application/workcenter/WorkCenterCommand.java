package com.shindong.smartmanager.application.workcenter;

public record WorkCenterCommand(String wcName, long mainProcessCodeId, int operationTime) {
}
