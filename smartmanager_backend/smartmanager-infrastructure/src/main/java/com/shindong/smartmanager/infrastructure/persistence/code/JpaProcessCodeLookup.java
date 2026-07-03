package com.shindong.smartmanager.infrastructure.persistence.code;

import com.shindong.smartmanager.application.process.ProcessCodeLookup;
import java.util.Optional;
import org.springframework.stereotype.Repository;

@Repository
public class JpaProcessCodeLookup implements ProcessCodeLookup {

    private static final String PROCESS_USAGE_TYPE = "PROCESS";

    private final SpringDataPublicCodeRepository publicCodeRepository;

    public JpaProcessCodeLookup(SpringDataPublicCodeRepository publicCodeRepository) {
        this.publicCodeRepository = publicCodeRepository;
    }

    @Override
    public Optional<ProcessCodeInfo> findActiveProcessCode(long processCodeId) {
        return publicCodeRepository
                .findByIdAndUsageTypeAndRecordingState(processCodeId, PROCESS_USAGE_TYPE, 1)
                .filter(code -> code.getSmallCode() != null)
                .map(code -> new ProcessCodeInfo(code.getId(), code.getSmallCode(), code.getSmallName()));
    }

    @Override
    public Optional<ProcessCodeInfo> findActiveProcessCodeBySmallCode(String smallCode) {
        return publicCodeRepository
                .findBySmallCodeAndUsageTypeAndRecordingState(smallCode, PROCESS_USAGE_TYPE, 1)
                .map(code -> new ProcessCodeInfo(code.getId(), code.getSmallCode(), code.getSmallName()));
    }
}
