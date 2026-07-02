package com.shindong.smartmanager.infrastructure.persistence.code;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.shindong.smartmanager.application.code.CodeGroupOptionsRepository;
import com.shindong.smartmanager.application.code.CodeOptionView;
import com.shindong.smartmanager.infrastructure.persistence.code.CodeGroupJpaEntity;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;
import java.util.Set;
import org.springframework.stereotype.Repository;

@Repository
public class JpaCodeGroupOptionsRepository implements CodeGroupOptionsRepository {

    private final SpringDataCodeGroupRepository codeGroupRepository;
    private final SpringDataPublicCodeRepository publicCodeRepository;
    private final ObjectMapper objectMapper;

    public JpaCodeGroupOptionsRepository(
            SpringDataCodeGroupRepository codeGroupRepository,
            SpringDataPublicCodeRepository publicCodeRepository,
            ObjectMapper objectMapper
    ) {
        this.codeGroupRepository = codeGroupRepository;
        this.publicCodeRepository = publicCodeRepository;
        this.objectMapper = objectMapper;
    }

    @Override
    public List<CodeOptionView> findActiveOptions(String codeGroupKey) {
        CodeGroupJpaEntity group = codeGroupRepository.findById(codeGroupKey)
                .orElseThrow(() -> new IllegalArgumentException("코드그룹을 찾을 수 없습니다: " + codeGroupKey));

        Set<String> excluded = parseExcludeCodes(group.getExcludeCodes());
        return publicCodeRepository.findActiveSmallCodes(group.getLargeCode(), group.getUsageType())
                .stream()
                .filter(code -> !excluded.contains(code.getSmallCode()))
                .map(code -> new CodeOptionView(code.getId(), code.getSmallCode(), code.getSmallName()))
                .toList();
    }

    private Set<String> parseExcludeCodes(String json) {
        if (json == null || json.isBlank()) {
            return Collections.emptySet();
        }
        try {
            List<String> codes = objectMapper.readValue(json, new TypeReference<>() {});
            return new HashSet<>(codes);
        } catch (Exception ex) {
            return Collections.emptySet();
        }
    }
}
