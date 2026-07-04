package com.shindong.smartmanager.infrastructure.persistence.code;

import com.shindong.smartmanager.application.publiccode.PublicCodeReferenceChecker;
import com.shindong.smartmanager.infrastructure.persistence.user.SpringDataUserRepository;
import org.springframework.stereotype.Component;

@Component
public class WorkDiaryGroupPublicCodeReferenceChecker implements PublicCodeReferenceChecker {

    private static final String WORK_DIARY_GROUP_USAGE_TYPE = "WORK_DIARY_GROUP";

    private final SpringDataUserRepository userRepository;

    public WorkDiaryGroupPublicCodeReferenceChecker(SpringDataUserRepository userRepository) {
        this.userRepository = userRepository;
    }

    @Override
    public String usageType() {
        return WORK_DIARY_GROUP_USAGE_TYPE;
    }

    @Override
    public void assertNotReferenced(long publicCodeId, String smallCode, String smallName) {
        if (userRepository.existsByWorkDiaryGroupIdAndRecordingState(publicCodeId, 1)) {
            String label = smallName != null && !smallName.isBlank()
                    ? "「" + smallName + "(" + smallCode + ")」"
                    : "「" + smallCode + "」";
            throw new IllegalArgumentException(label + "은(는) 사용자에서 사용 중이라 삭제할 수 없습니다.");
        }
    }
}
