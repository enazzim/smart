package com.shindong.smartmanager.api.web.system.publiccode;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.publiccode.CreateLargeCommand;
import com.shindong.smartmanager.application.publiccode.CreateSmallCommand;
import com.shindong.smartmanager.application.publiccode.UpdateLargeCommand;
import com.shindong.smartmanager.application.publiccode.UpdateSmallCommand;
import com.shindong.smartmanager.infrastructure.application.PublicCodeApplicationService;
import jakarta.validation.Valid;
import java.util.List;
import org.springframework.http.HttpStatus;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/system/public-codes")
public class SystemPublicCodeController {

    private final PublicCodeApplicationService publicCodeApplicationService;

    public SystemPublicCodeController(PublicCodeApplicationService publicCodeApplicationService) {
        this.publicCodeApplicationService = publicCodeApplicationService;
    }

    @GetMapping("/large")
    @PreAuthorize("hasAuthority('system:public-code:read')")
    public List<PublicCodeLargeResponse> listLarge() {
        return publicCodeApplicationService.listLargeHeaders().stream()
                .map(PublicCodeLargeResponse::from)
                .toList();
    }

    @GetMapping("/small")
    @PreAuthorize("hasAuthority('system:public-code:read')")
    public List<PublicCodeSmallResponse> listSmall(@RequestParam String largeCode) {
        return publicCodeApplicationService.listSmallByLargeCode(largeCode).stream()
                .map(PublicCodeSmallResponse::from)
                .toList();
    }

    @GetMapping("/{id}")
    @PreAuthorize("hasAuthority('system:public-code:read')")
    public PublicCodeSmallResponse get(@PathVariable long id) {
        return PublicCodeSmallResponse.from(publicCodeApplicationService.getActiveSmall(id));
    }

    @PostMapping("/large")
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('system:public-code:write')")
    public PublicCodeLargeResponse createLarge(
            @Valid @RequestBody CreateLargePublicCodeRequest request
    ) {
        return PublicCodeLargeResponse.from(publicCodeApplicationService.createLarge(
                new CreateLargeCommand(request.largeCode(), request.largeName(), request.usageType()),
                SecurityUtils.requireLoginId()
        ));
    }

    @PostMapping("/small")
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('system:public-code:write')")
    public PublicCodeSmallResponse createSmall(
            @Valid @RequestBody CreateSmallPublicCodeRequest request
    ) {
        return PublicCodeSmallResponse.from(publicCodeApplicationService.createSmall(
                new CreateSmallCommand(request.largeCode(), request.smallCode(), request.smallName()),
                SecurityUtils.requireLoginId()
        ));
    }

    @PutMapping("/large/{largeCode}")
    @PreAuthorize("hasAuthority('system:public-code:write')")
    public PublicCodeLargeResponse updateLarge(
            @PathVariable String largeCode,
            @Valid @RequestBody UpdateLargePublicCodeRequest request
    ) {
        return PublicCodeLargeResponse.from(publicCodeApplicationService.updateLarge(
                largeCode,
                new UpdateLargeCommand(request.largeName(), request.usageType()),
                SecurityUtils.requireLoginId()
        ));
    }

    @PutMapping("/small/{id}")
    @PreAuthorize("hasAuthority('system:public-code:write')")
    public PublicCodeSmallResponse updateSmall(
            @PathVariable long id,
            @Valid @RequestBody UpdateSmallPublicCodeRequest request
    ) {
        return PublicCodeSmallResponse.from(publicCodeApplicationService.updateSmall(
                id,
                new UpdateSmallCommand(request.smallName()),
                SecurityUtils.requireLoginId()
        ));
    }

    @DeleteMapping("/large/{largeCode}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('system:public-code:write')")
    public void deleteLarge(@PathVariable String largeCode) {
        publicCodeApplicationService.deleteLarge(largeCode, SecurityUtils.requireLoginId());
    }

    @DeleteMapping("/small/{id}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('system:public-code:write')")
    public void deleteSmall(@PathVariable long id) {
        publicCodeApplicationService.deleteSmall(id, SecurityUtils.requireLoginId());
    }
}
