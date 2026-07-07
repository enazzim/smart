package com.shindong.smartmanager.application.system.imports;

public record WorkStandardImportRow(
        String itemNum,
        Short processSequenceNum,
        String processSmallCode,
        String workCenterName,
        String equipmentNum,
        Integer priorityOrder,
        String mainWorkerLoginId,
        String toolName,
        Integer setupTime,
        Integer standardTime
) {
}
