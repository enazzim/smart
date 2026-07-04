package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.publiccode.CreateLargeCommand;
import com.shindong.smartmanager.application.publiccode.CreateSmallCommand;
import com.shindong.smartmanager.application.publiccode.PublicCodeLargeView;
import com.shindong.smartmanager.application.publiccode.PublicCodeService;
import com.shindong.smartmanager.application.publiccode.PublicCodeSmallView;
import com.shindong.smartmanager.application.publiccode.UpdateLargeCommand;
import com.shindong.smartmanager.application.publiccode.UpdateSmallCommand;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class PublicCodeApplicationService {

    private final PublicCodeService publicCodeService;

    public PublicCodeApplicationService(PublicCodeService publicCodeService) {
        this.publicCodeService = publicCodeService;
    }

    @Transactional(readOnly = true)
    public List<PublicCodeLargeView> listLargeHeaders() {
        return publicCodeService.listLargeHeaders();
    }

    @Transactional(readOnly = true)
    public List<PublicCodeSmallView> listSmallByLargeCode(String largeCode) {
        return publicCodeService.listSmallByLargeCode(largeCode);
    }

    @Transactional(readOnly = true)
    public List<PublicCodeSmallView> listActiveSmallCodes(String largeCode, String usageType) {
        return publicCodeService.listActiveSmallCodes(largeCode, usageType);
    }

    @Transactional(readOnly = true)
    public PublicCodeSmallView getActiveSmall(long id) {
        return publicCodeService.getActiveSmall(id);
    }

    @Transactional
    public PublicCodeLargeView createLarge(CreateLargeCommand command, String actorUserId) {
        return publicCodeService.createLarge(command, actorUserId);
    }

    @Transactional
    public PublicCodeSmallView createSmall(CreateSmallCommand command, String actorUserId) {
        return publicCodeService.createSmall(command, actorUserId);
    }

    @Transactional
    public PublicCodeLargeView updateLarge(String largeCode, UpdateLargeCommand command, String actorUserId) {
        return publicCodeService.updateLarge(largeCode, command, actorUserId);
    }

    @Transactional
    public PublicCodeSmallView updateSmall(long id, UpdateSmallCommand command, String actorUserId) {
        return publicCodeService.updateSmall(id, command, actorUserId);
    }

    @Transactional
    public void deleteSmall(long id, String actorUserId) {
        publicCodeService.deleteSmall(id, actorUserId);
    }

    @Transactional
    public void deleteLarge(String largeCode, String actorUserId) {
        publicCodeService.deleteLarge(largeCode, actorUserId);
    }
}
