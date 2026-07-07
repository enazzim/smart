package com.shindong.smartmanager.application.publiccode;

import java.util.List;
import java.util.Optional;

public interface PublicCodeRepository {

    List<PublicCodeLargeView> findActiveLargeHeaders();

    List<PublicCodeSmallView> findActiveSmallByLargeCode(String largeCode);

    List<PublicCodeSmallView> findActiveSmallCodes(String largeCode, String usageType);

    Optional<PublicCodeLargeView> findActiveLargeHeader(String largeCode);

    Optional<PublicCodeSmallView> findActiveSmallById(long id);

    Optional<PublicCodeSmallView> findActiveSmallBySmallCode(String smallCode, String usageType);

    long saveLarge(CreateLargeCommand command, String actorUserId);

    long saveSmall(CreateSmallCommand command, PublicCodeLargeView header, String actorUserId);

    void updateLarge(String largeCode, UpdateLargeCommand command, String actorUserId);

    void updateSmall(long id, UpdateSmallCommand command, String actorUserId);

    void softDeleteLarge(String largeCode, String actorUserId);

    void softDeleteSmall(long id, String actorUserId);

    boolean existsActiveLargeHeader(String largeCode);

    boolean existsActiveSmall(String largeCode, String smallCode);
}
