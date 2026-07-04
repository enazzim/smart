package com.shindong.smartmanager.api.web.basis.publiccode;

import com.shindong.smartmanager.api.security.BasisAuthorize;
import com.shindong.smartmanager.api.web.system.publiccode.PublicCodeSmallResponse;
import com.shindong.smartmanager.infrastructure.application.PublicCodeApplicationService;
import java.util.List;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/basis/public-codes")
@BasisAuthorize.PublicCodeRead
public class BasisPublicCodeController {

    private final PublicCodeApplicationService publicCodeApplicationService;

    public BasisPublicCodeController(PublicCodeApplicationService publicCodeApplicationService) {
        this.publicCodeApplicationService = publicCodeApplicationService;
    }

    @GetMapping
    public List<PublicCodeSmallResponse> list(
            @RequestParam(required = false) String largeCode,
            @RequestParam(required = false) String usageType
    ) {
        return publicCodeApplicationService.listActiveSmallCodes(largeCode, usageType).stream()
                .map(PublicCodeSmallResponse::from)
                .toList();
    }
}
