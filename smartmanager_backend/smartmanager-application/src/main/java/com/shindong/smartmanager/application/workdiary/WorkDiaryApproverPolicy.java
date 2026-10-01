package com.shindong.smartmanager.application.workdiary;

import com.shindong.smartmanager.application.auth.AuthUserRepository;
import com.shindong.smartmanager.application.system.SystemSettingRepository;
import com.shindong.smartmanager.application.system.SystemSettingService;
import com.shindong.smartmanager.application.user.UserRepository;
import java.util.LinkedHashMap;
import java.util.Map;
import java.util.Optional;

/**
 * 업무일지 결재자 = approve 권한 보유 + 시스템 설정에 지정 + 활성 사용자. 하나라도 어긋나면 결재자 없음.
 */
public class WorkDiaryApproverPolicy {

    public static final String APPROVE_AUTHORITY = "community:workdiary:approve";
    public static final String CANDIDATE_ROLE_CODE = "CEO";
    public static final String UNASSIGNED_LABEL = "(미지정)";

    private final SystemSettingRepository systemSettingRepository;
    private final AuthUserRepository authUserRepository;
    private final UserRepository userRepository;

    public WorkDiaryApproverPolicy(
            SystemSettingRepository systemSettingRepository,
            AuthUserRepository authUserRepository,
            UserRepository userRepository
    ) {
        this.systemSettingRepository = systemSettingRepository;
        this.authUserRepository = authUserRepository;
        this.userRepository = userRepository;
    }

    public Optional<Long> resolveApproverUserId() {
        return systemSettingRepository.findActiveByKey(SystemSettingService.KEY_WORKDIARY_APPROVER_USER_ID)
                .map(view -> SystemSettingService.parseJsonString(view.valueJson()))
                .flatMap(WorkDiaryApproverPolicy::parseUserId)
                .filter(this::hasApproveAuthority);
    }

    public boolean isApprover(long userId) {
        return resolveApproverUserId().map(id -> id == userId).orElse(false);
    }

    /**
     * 시스템 설정 선택지(값→표시명). 미지정 + 활성 CEO 사용자. 저장값이 더 이상 유효하지 않으면 경고 항목으로 포함.
     */
    public Map<String, String> approverOptions(String currentValue) {
        Map<String, String> options = new LinkedHashMap<>();
        options.put("", UNASSIGNED_LABEL);
        userRepository.findAllActive(null).stream()
                .filter(user -> user.roleCodes().contains(CANDIDATE_ROLE_CODE))
                .filter(user -> hasApproveAuthority(user.id()))
                .forEach(user -> options.put(String.valueOf(user.id()), user.name() + " (" + user.loginId() + ")"));
        if (currentValue != null && !currentValue.isBlank() && !options.containsKey(currentValue)) {
            String name = parseUserId(currentValue)
                    .flatMap(userRepository::findActiveById)
                    .map(user -> user.name())
                    .orElse("#" + currentValue);
            options.put(currentValue, "(지정 해제됨: " + name + ")");
        }
        return options;
    }

    private boolean hasApproveAuthority(long userId) {
        return authUserRepository.findActiveById(userId)
                .map(user -> user.authorities().contains(APPROVE_AUTHORITY))
                .orElse(false);
    }

    private static Optional<Long> parseUserId(String value) {
        if (value == null || value.isBlank()) {
            return Optional.empty();
        }
        try {
            return Optional.of(Long.parseLong(value.trim()));
        } catch (NumberFormatException e) {
            return Optional.empty();
        }
    }
}
