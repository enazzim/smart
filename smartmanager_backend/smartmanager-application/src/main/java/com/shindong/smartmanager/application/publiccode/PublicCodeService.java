package com.shindong.smartmanager.application.publiccode;

import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.function.Function;
import java.util.stream.Collectors;

public class PublicCodeService {

    private static final Set<String> VALID_USAGE_TYPES = Set.of(
            "GENERIC",
            "PROCESS",
            "UNIT",
            "NC_REASON",
            "NC_DETAIL",
            "WORK_DIARY_GROUP"
    );
    private static final Set<String> TIER2_USAGE_TYPES = Set.of(
            "PROCESS",
            "UNIT",
            "NC_REASON",
            "NC_DETAIL",
            "WORK_DIARY_GROUP"
    );
    private static final Set<String> EXCLUDED_PROCESS_CODES = Set.of("14000000", "14009999");

    private final PublicCodeRepository publicCodeRepository;
    private final Map<String, PublicCodeReferenceChecker> referenceCheckers;

    public PublicCodeService(
            PublicCodeRepository publicCodeRepository,
            List<PublicCodeReferenceChecker> referenceCheckers
    ) {
        this.publicCodeRepository = publicCodeRepository;
        this.referenceCheckers = referenceCheckers.stream()
                .collect(Collectors.toMap(PublicCodeReferenceChecker::usageType, Function.identity()));
    }

    public List<PublicCodeLargeView> listLargeHeaders() {
        return publicCodeRepository.findActiveLargeHeaders();
    }

    public List<PublicCodeSmallView> listSmallByLargeCode(String largeCode) {
        requireLargeCode(largeCode);
        return publicCodeRepository.findActiveSmallByLargeCode(largeCode.trim());
    }

    public List<PublicCodeSmallView> listActiveSmallCodes(String largeCode, String usageType) {
        List<PublicCodeSmallView> rows = publicCodeRepository.findActiveSmallCodes(
                largeCode != null && !largeCode.isBlank() ? largeCode.trim() : null,
                usageType != null && !usageType.isBlank() ? usageType.trim() : null
        );
        if ("PROCESS".equals(usageType)) {
            return rows.stream()
                    .filter(row -> !EXCLUDED_PROCESS_CODES.contains(row.smallCode()))
                    .toList();
        }
        return rows;
    }

    public PublicCodeSmallView getActiveSmall(long id) {
        return publicCodeRepository.findActiveSmallById(id)
                .orElseThrow(() -> new IllegalArgumentException("공용코드를 찾을 수 없습니다: " + id));
    }

    public PublicCodeLargeView createLarge(CreateLargeCommand command, String actorUserId) {
        validateLargeCommand(command);
        String largeCode = command.largeCode().trim();
        String largeName = command.largeName().trim();
        if (publicCodeRepository.existsActiveLargeHeader(largeCode)) {
            throw new IllegalArgumentException("이미 등록된 대분류 코드입니다: " + largeCode);
        }
        assertUniqueLargeName(largeName, null);
        publicCodeRepository.saveLarge(command, actorUserId);
        return publicCodeRepository.findActiveLargeHeader(largeCode)
                .orElseThrow(() -> new IllegalStateException("대분류 등록 후 조회에 실패했습니다: " + largeCode));
    }

    public PublicCodeSmallView createSmall(CreateSmallCommand command, String actorUserId) {
        validateSmallCommand(command);
        String largeCode = command.largeCode().trim();
        PublicCodeLargeView header = publicCodeRepository.findActiveLargeHeader(largeCode)
                .orElseThrow(() -> new IllegalArgumentException("대분류를 찾을 수 없습니다: " + largeCode));
        String smallCode = command.smallCode().trim();
        String smallName = command.smallName().trim();
        if (publicCodeRepository.existsActiveSmall(largeCode, smallCode)) {
            throw new IllegalArgumentException("이미 등록된 소분류 코드입니다: " + smallCode);
        }
        assertUniqueSmallName(largeCode, smallName, null);
        long id = publicCodeRepository.saveSmall(command, header, actorUserId);
        return getActiveSmall(id);
    }

    public PublicCodeLargeView updateLarge(String largeCode, UpdateLargeCommand command, String actorUserId) {
        requireLargeCode(largeCode);
        PublicCodeLargeView existing = publicCodeRepository.findActiveLargeHeader(largeCode.trim())
                .orElseThrow(() -> new IllegalArgumentException("대분류를 찾을 수 없습니다: " + largeCode));
        validateUsageType(command.usageType());
        String nextLargeName = requireText(command.largeName(), "대분류명");
        String nextUsageType = command.usageType().trim();

        if (!existing.usageType().equals(nextUsageType) && isTier2(existing.usageType())) {
            assertNoReferencesForLarge(largeCode.trim(), existing.usageType());
        }
        assertUniqueLargeName(nextLargeName, largeCode.trim());

        publicCodeRepository.updateLarge(largeCode.trim(), command, actorUserId);
        return publicCodeRepository.findActiveLargeHeader(largeCode.trim())
                .orElseThrow(() -> new IllegalStateException("대분류 수정 후 조회에 실패했습니다: " + largeCode));
    }

    public PublicCodeSmallView updateSmall(long id, UpdateSmallCommand command, String actorUserId) {
        PublicCodeSmallView existing = getActiveSmall(id);
        String smallName = requireText(command.smallName(), "소분류명");
        assertUniqueSmallName(existing.largeCode(), smallName, id);
        publicCodeRepository.updateSmall(id, new UpdateSmallCommand(smallName), actorUserId);
        return getActiveSmall(id);
    }

    public void deleteSmall(long id, String actorUserId) {
        PublicCodeSmallView existing = getActiveSmall(id);
        assertNotReferenced(existing);
        publicCodeRepository.softDeleteSmall(id, actorUserId);
    }

    public void deleteLarge(String largeCode, String actorUserId) {
        requireLargeCode(largeCode);
        String normalized = largeCode.trim();
        PublicCodeLargeView header = publicCodeRepository.findActiveLargeHeader(normalized)
                .orElseThrow(() -> new IllegalArgumentException("대분류를 찾을 수 없습니다: " + largeCode));

        if (isTier2(header.usageType())) {
            for (PublicCodeSmallView small : publicCodeRepository.findActiveSmallByLargeCode(normalized)) {
                assertNotReferenced(small);
            }
        }

        publicCodeRepository.softDeleteLarge(normalized, actorUserId);
    }

    private void assertNoReferencesForLarge(String largeCode, String usageType) {
        if (!isTier2(usageType)) {
            return;
        }
        for (PublicCodeSmallView small : publicCodeRepository.findActiveSmallByLargeCode(largeCode)) {
            assertNotReferenced(small);
        }
    }

    private void assertNotReferenced(PublicCodeSmallView code) {
        PublicCodeReferenceChecker checker = referenceCheckers.get(code.usageType());
        if (checker != null) {
            checker.assertNotReferenced(code.id(), code.smallCode(), code.smallName());
        }
    }

    private void validateLargeCommand(CreateLargeCommand command) {
        requireText(command.largeCode(), "대분류 코드");
        requireText(command.largeName(), "대분류명");
        validateUsageType(command.usageType());
    }

    private void validateSmallCommand(CreateSmallCommand command) {
        requireLargeCode(command.largeCode());
        requireText(command.smallCode(), "소분류 코드");
        requireText(command.smallName(), "소분류명");
    }

    private void validateUsageType(String usageType) {
        if (usageType == null || usageType.isBlank()) {
            throw new IllegalArgumentException("용도(usageType)는 필수입니다.");
        }
        if (!VALID_USAGE_TYPES.contains(usageType.trim())) {
            throw new IllegalArgumentException("지원하지 않는 용도입니다: " + usageType);
        }
    }

    private static boolean isTier2(String usageType) {
        return TIER2_USAGE_TYPES.contains(usageType);
    }

    private static void requireLargeCode(String largeCode) {
        if (largeCode == null || largeCode.isBlank()) {
            throw new IllegalArgumentException("대분류 코드는 필수입니다.");
        }
    }

    private static String requireText(String value, String label) {
        if (value == null || value.isBlank()) {
            throw new IllegalArgumentException(label + "은(는) 필수입니다.");
        }
        return value.trim();
    }

    private void assertUniqueLargeName(String largeName, String excludeLargeCode) {
        if (publicCodeRepository.existsActiveLargeHeaderByName(largeName, excludeLargeCode)) {
            throw new IllegalArgumentException("이미 등록된 대분류명입니다: " + largeName);
        }
    }

    private void assertUniqueSmallName(String largeCode, String smallName, Long excludeId) {
        if (publicCodeRepository.existsActiveSmallByName(largeCode, smallName, excludeId)) {
            throw new IllegalArgumentException("이미 등록된 소분류명입니다: " + smallName);
        }
    }
}
