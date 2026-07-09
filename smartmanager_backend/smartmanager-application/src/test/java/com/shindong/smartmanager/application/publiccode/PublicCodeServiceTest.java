package com.shindong.smartmanager.application.publiccode;

import static org.junit.jupiter.api.Assertions.assertDoesNotThrow;
import static org.junit.jupiter.api.Assertions.assertThrows;

import java.util.List;
import java.util.Optional;
import org.junit.jupiter.api.Test;

class PublicCodeServiceTest {

    @Test
    void rejectsDuplicateLargeNameOnCreate() {
        StubPublicCodeRepository repository = new StubPublicCodeRepository();
        repository.duplicateLargeName = true;
        PublicCodeService service = new PublicCodeService(repository, List.of());

        IllegalArgumentException ex = assertThrows(
                IllegalArgumentException.class,
                () -> service.createLarge(
                        new CreateLargeCommand("10000000", "중복명", "GENERIC"),
                        "1"
                )
        );
        assert ex.getMessage().contains("대분류명");
    }

    @Test
    void rejectsDuplicateSmallNameOnCreate() {
        StubPublicCodeRepository repository = new StubPublicCodeRepository();
        repository.duplicateSmallName = true;
        PublicCodeService service = new PublicCodeService(repository, List.of());

        IllegalArgumentException ex = assertThrows(
                IllegalArgumentException.class,
                () -> service.createSmall(
                        new CreateSmallCommand("10000000", "10000001", "중복명"),
                        "1"
                )
        );
        assert ex.getMessage().contains("소분류명");
    }

    @Test
    void allowsSameLargeNameOnUpdateWhenExcluded() {
        StubPublicCodeRepository repository = new StubPublicCodeRepository();
        PublicCodeService service = new PublicCodeService(repository, List.of());

        assertDoesNotThrow(() -> service.updateLarge(
                "10000000",
                new UpdateLargeCommand("기존명", "GENERIC"),
                "1"
        ));
    }

    @Test
    void rejectsDuplicateSmallNameOnUpdate() {
        StubPublicCodeRepository repository = new StubPublicCodeRepository();
        repository.duplicateSmallName = true;
        PublicCodeService service = new PublicCodeService(repository, List.of());

        IllegalArgumentException ex = assertThrows(
                IllegalArgumentException.class,
                () -> service.updateSmall(10L, new UpdateSmallCommand("중복명"), "1")
        );
        assert ex.getMessage().contains("소분류명");
    }

    private static final class StubPublicCodeRepository implements PublicCodeRepository {
        boolean duplicateLargeName;
        boolean duplicateSmallName;

        @Override
        public List<PublicCodeLargeView> findActiveLargeHeaders() {
            return List.of();
        }

        @Override
        public List<PublicCodeSmallView> findActiveSmallByLargeCode(String largeCode) {
            return List.of();
        }

        @Override
        public List<PublicCodeSmallView> findActiveSmallCodes(String largeCode, String usageType) {
            return List.of();
        }

        @Override
        public Optional<PublicCodeLargeView> findActiveLargeHeader(String largeCode) {
            return Optional.of(new PublicCodeLargeView(1L, largeCode, "기존명", "GENERIC"));
        }

        @Override
        public Optional<PublicCodeSmallView> findActiveSmallById(long id) {
            return Optional.of(new PublicCodeSmallView(
                    id,
                    "10000000",
                    "대분류",
                    "10000001",
                    "기존명",
                    "GENERIC"
            ));
        }

        @Override
        public Optional<PublicCodeSmallView> findActiveSmallBySmallCode(String smallCode, String usageType) {
            return Optional.empty();
        }

        @Override
        public long saveLarge(CreateLargeCommand command, String actorUserId) {
            return 1L;
        }

        @Override
        public long saveSmall(CreateSmallCommand command, PublicCodeLargeView header, String actorUserId) {
            return 10L;
        }

        @Override
        public void updateLarge(String largeCode, UpdateLargeCommand command, String actorUserId) {
        }

        @Override
        public void updateSmall(long id, UpdateSmallCommand command, String actorUserId) {
        }

        @Override
        public void softDeleteLarge(String largeCode, String actorUserId) {
        }

        @Override
        public void softDeleteSmall(long id, String actorUserId) {
        }

        @Override
        public boolean existsActiveLargeHeader(String largeCode) {
            return false;
        }

        @Override
        public boolean existsActiveSmall(String largeCode, String smallCode) {
            return false;
        }

        @Override
        public boolean existsActiveLargeHeaderByName(String largeName, String excludeLargeCode) {
            if (excludeLargeCode != null) {
                return false;
            }
            return duplicateLargeName;
        }

        @Override
        public boolean existsActiveSmallByName(String largeCode, String smallName, Long excludeId) {
            if (excludeId != null && smallName.equals("기존명")) {
                return false;
            }
            return duplicateSmallName;
        }
    }
}
