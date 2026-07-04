package com.shindong.smartmanager.infrastructure.persistence.code;

import com.shindong.smartmanager.application.publiccode.PublicCodeReferenceChecker;
import com.shindong.smartmanager.infrastructure.persistence.process.SpringDataProcessSequenceRepository;
import com.shindong.smartmanager.infrastructure.persistence.unitprice.SpringDataUnitPriceRepository;
import com.shindong.smartmanager.infrastructure.persistence.workcenter.SpringDataWorkCenterRepository;
import org.springframework.stereotype.Component;

@Component
public class ProcessPublicCodeReferenceChecker implements PublicCodeReferenceChecker {

    private static final String PROCESS_USAGE_TYPE = "PROCESS";

    private final SpringDataWorkCenterRepository workCenterRepository;
    private final SpringDataProcessSequenceRepository processSequenceRepository;
    private final SpringDataUnitPriceRepository unitPriceRepository;

    public ProcessPublicCodeReferenceChecker(
            SpringDataWorkCenterRepository workCenterRepository,
            SpringDataProcessSequenceRepository processSequenceRepository,
            SpringDataUnitPriceRepository unitPriceRepository
    ) {
        this.workCenterRepository = workCenterRepository;
        this.processSequenceRepository = processSequenceRepository;
        this.unitPriceRepository = unitPriceRepository;
    }

    @Override
    public String usageType() {
        return PROCESS_USAGE_TYPE;
    }

    @Override
    public void assertNotReferenced(long publicCodeId, String smallCode, String smallName) {
        if (workCenterRepository.existsByMainProcessCodeIdAndRecordingState(publicCodeId, 1)) {
            throw referenced(smallCode, smallName, "작업장");
        }
        if (processSequenceRepository.existsByPublicCodeIdAndRecordingState(publicCodeId, 1)) {
            throw referenced(smallCode, smallName, "공정순서");
        }
        if (unitPriceRepository.existsByBeginProcessCodeIdAndRecordingState(publicCodeId, 1)
                || unitPriceRepository.existsByEndProcessCodeIdAndRecordingState(publicCodeId, 1)) {
            throw referenced(smallCode, smallName, "단가");
        }
    }

    private static IllegalArgumentException referenced(String smallCode, String smallName, String module) {
        String label = smallName != null && !smallName.isBlank()
                ? "「" + smallName + "(" + smallCode + ")」"
                : "「" + smallCode + "」";
        return new IllegalArgumentException(label + "은(는) " + module + "에서 사용 중이라 삭제할 수 없습니다.");
    }
}
