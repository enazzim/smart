package com.shindong.smartmanager.application.user;

import com.shindong.smartmanager.application.code.CodeGroupOptionsRepository;
import com.shindong.smartmanager.application.code.CodeOptionView;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.role.RoleRepository;
import com.shindong.smartmanager.application.security.PasswordHasher;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import java.util.List;
import java.util.regex.Pattern;

public class UserService {

    private static final String ADMIN_LOGIN_ID = "admin";
    private static final int MIN_PASSWORD_LENGTH = 8;
    private static final Pattern EMAIL_PATTERN =
            Pattern.compile("^[\\w.%+-]+@[\\w.-]+\\.[A-Za-z]{2,}$");
    private static final String WORK_DIARY_GROUP = "WORK_DIARY_GROUP";

    private final UserRepository userRepository;
    private final RoleRepository roleRepository;
    private final CodeGroupOptionsRepository codeGroupOptionsRepository;
    private final PasswordHasher passwordHasher;
    private final DomainEventStore domainEventStore;

    public UserService(
            UserRepository userRepository,
            RoleRepository roleRepository,
            CodeGroupOptionsRepository codeGroupOptionsRepository,
            PasswordHasher passwordHasher,
            DomainEventStore domainEventStore
    ) {
        this.userRepository = userRepository;
        this.roleRepository = roleRepository;
        this.codeGroupOptionsRepository = codeGroupOptionsRepository;
        this.passwordHasher = passwordHasher;
        this.domainEventStore = domainEventStore;
    }

    public UserView register(UserCommand command, String actorUserId) {
        validateRegisterCommand(command);
        validateRoles(command.roleIds());
        validateWorkDiaryGroup(command.workDiaryGroupId());

        String loginId = command.loginId().trim();
        if (userRepository.existsActiveByLoginId(loginId, null)) {
            throw new IllegalArgumentException("이미 사용 중인 로그인 아이디입니다: " + loginId);
        }

        String passwordHash = passwordHasher.hash(command.password());
        long id = userRepository.save(command, passwordHash, actorUserId);
        userRepository.replaceRoles(id, command.roleIds());
        appendEvent(EventTypes.USER_REGISTERED, id, actorUserId, loginId);
        return getActive(id);
    }

    public UserView update(long id, UserUpdateCommand command, String actorUserId) {
        UserView existing = getActive(id);
        validateUpdateCommand(command);
        validateRoles(command.roleIds());
        validateWorkDiaryGroup(command.workDiaryGroupId());

        String passwordHash = null;
        if (command.password() != null && !command.password().isBlank()) {
            validatePassword(command.password());
            passwordHash = passwordHasher.hash(command.password());
        }

        userRepository.update(id, command, passwordHash, actorUserId);
        userRepository.replaceRoles(id, command.roleIds());
        appendEvent(EventTypes.USER_UPDATED, id, actorUserId, existing.loginId());
        return getActive(id);
    }

    public void delete(long id, String actorUserId) {
        UserView existing = getActive(id);
        if (ADMIN_LOGIN_ID.equalsIgnoreCase(existing.loginId())) {
            throw new IllegalArgumentException("admin 계정은 삭제할 수 없습니다.");
        }

        userRepository.softDelete(id, actorUserId);
        domainEventStore.append(DomainEvent.create(
                EventTypes.USER_DELETED,
                1,
                AggregateTypes.USER,
                String.valueOf(id),
                actorUserId,
                """
                {"userId":%d,"loginId":"%s"}
                """.formatted(id, escape(existing.loginId())).trim()
        ));
    }

    public List<UserView> listActive(String query) {
        return userRepository.findAllActive(query);
    }

    public UserView getActive(long id) {
        return userRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("사용자를 찾을 수 없습니다: " + id));
    }

    public boolean isLoginIdAvailable(String loginId) {
        if (loginId == null || loginId.isBlank()) {
            return false;
        }
        return !userRepository.existsActiveByLoginId(loginId.trim(), null);
    }

    private void validateRegisterCommand(UserCommand command) {
        if (command.loginId() == null || command.loginId().isBlank()) {
            throw new IllegalArgumentException("로그인 아이디는 필수입니다.");
        }
        validatePassword(command.password());
        validateName(command.name());
        validateEmail(command.email());
    }

    private void validateUpdateCommand(UserUpdateCommand command) {
        validateName(command.name());
        validateEmail(command.email());
    }

    private void validateName(String name) {
        if (name == null || name.isBlank()) {
            throw new IllegalArgumentException("사원명은 필수입니다.");
        }
    }

    private void validatePassword(String password) {
        if (password == null || password.isBlank()) {
            throw new IllegalArgumentException("비밀번호는 필수입니다.");
        }
        if (password.length() < MIN_PASSWORD_LENGTH) {
            throw new IllegalArgumentException("비밀번호는 " + MIN_PASSWORD_LENGTH + "자 이상이어야 합니다.");
        }
    }

    private void validateEmail(String email) {
        if (email != null && !email.isBlank() && !EMAIL_PATTERN.matcher(email.trim()).matches()) {
            throw new IllegalArgumentException("이메일 형식이 올바르지 않습니다.");
        }
    }

    private void validateRoles(List<Long> roleIds) {
        if (roleIds == null || roleIds.isEmpty()) {
            throw new IllegalArgumentException("사용권한을 1개 이상 선택해 주세요.");
        }
        if (!roleRepository.allActiveIdsExist(roleIds)) {
            throw new IllegalArgumentException("존재하지 않는 역할이 포함되어 있습니다.");
        }
    }

    private void validateWorkDiaryGroup(Long workDiaryGroupId) {
        if (workDiaryGroupId == null) {
            return;
        }
        boolean found = codeGroupOptionsRepository.findActiveOptions(WORK_DIARY_GROUP).stream()
                .map(CodeOptionView::id)
                .anyMatch(id -> id == workDiaryGroupId);
        if (!found) {
            throw new IllegalArgumentException("업무일지그룹을 찾을 수 없습니다: " + workDiaryGroupId);
        }
    }

    private void appendEvent(String eventType, long id, String actorUserId, String loginId) {
        domainEventStore.append(DomainEvent.create(
                eventType,
                1,
                AggregateTypes.USER,
                String.valueOf(id),
                actorUserId,
                """
                {"userId":%d,"loginId":"%s"}
                """.formatted(id, escape(loginId)).trim()
        ));
    }

    private static String escape(String value) {
        return value.replace("\\", "\\\\").replace("\"", "\\\"");
    }
}
