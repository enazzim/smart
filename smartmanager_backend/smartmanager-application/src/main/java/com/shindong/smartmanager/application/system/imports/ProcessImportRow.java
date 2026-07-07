package com.shindong.smartmanager.application.system.imports;

import com.shindong.smartmanager.domain.process.WorkDistinction;

public record ProcessImportRow(
        String itemNo,
        Short processSequenceNum,
        String processSmallCode,
        WorkDistinction workDistinction,
        String workCenterName,
        Integer outsideOrderRate,
        Short progressRate
) {
}
