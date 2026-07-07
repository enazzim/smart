package com.shindong.smartmanager.api.web.user;

import com.shindong.smartmanager.api.security.BasisAuthorize;
import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.user.UserCommand;
import com.shindong.smartmanager.application.user.UserUpdateCommand;
import com.shindong.smartmanager.infrastructure.application.UserApplicationService;
import jakarta.validation.Valid;
import java.util.List;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/basis/users")
@BasisAuthorize.UserRead
public class UserController {

    private static final String DEFAULT_ACTOR = "local-dev";

    private final UserApplicationService userApplicationService;

    public UserController(UserApplicationService userApplicationService) {
        this.userApplicationService = userApplicationService;
    }

    @GetMapping
    public List<UserResponse> list(@RequestParam(required = false) String q) {
        var principal = SecurityUtils.requirePrincipal();
        return userApplicationService.listActiveForActor(q, principal.userId(), principal.authorities()).stream()
                .map(UserResponse::from)
                .toList();
    }

    @GetMapping("/search")
    public List<UserResponse> search(@RequestParam(required = false) String q) {
        return list(q);
    }

    @GetMapping("/check-login-id")
    public LoginIdAvailabilityResponse checkLoginId(@RequestParam String loginId) {
        return new LoginIdAvailabilityResponse(userApplicationService.isLoginIdAvailable(loginId));
    }

    @GetMapping("/{id}")
    public UserResponse get(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        return UserResponse.from(
                userApplicationService.getActiveForActor(id, principal.userId(), principal.authorities())
        );
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @BasisAuthorize.UserWrite
    public UserResponse create(
            @Valid @RequestBody CreateUserRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        UserCommand command = new UserCommand(
                request.loginId(),
                request.password(),
                request.name(),
                request.contact(),
                request.email(),
                request.roleIds(),
                request.workDiaryGroupId()
        );
        return UserResponse.from(userApplicationService.register(command, actor));
    }

    @PutMapping("/{id}")
    @BasisAuthorize.UserWrite
    public UserResponse update(
            @PathVariable long id,
            @Valid @RequestBody UpdateUserRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        UserUpdateCommand command = new UserUpdateCommand(
                request.name(),
                request.password(),
                request.contact(),
                request.email(),
                request.roleIds(),
                request.workDiaryGroupId()
        );
        return UserResponse.from(userApplicationService.update(id, command, actor));
    }

    @DeleteMapping("/{id}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @BasisAuthorize.UserWrite
    public void delete(
            @PathVariable long id,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        userApplicationService.delete(id, actor);
    }
}
