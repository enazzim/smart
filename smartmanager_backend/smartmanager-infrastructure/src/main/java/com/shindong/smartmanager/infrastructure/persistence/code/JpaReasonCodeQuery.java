package com.shindong.smartmanager.infrastructure.persistence.code;

import com.shindong.smartmanager.application.inventory.ReasonCodeQuery;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaReasonCodeQuery implements ReasonCodeQuery {

    private final SpringDataPublicCodeRepository publicCodeRepository;

    public JpaReasonCodeQuery(SpringDataPublicCodeRepository publicCodeRepository) {
        this.publicCodeRepository = publicCodeRepository;
    }

    @Override
    @Transactional(readOnly = true)
    public long requireId(String largeCode, String smallCode) {
        return publicCodeRepository
                .findByLargeCodeAndSmallCodeAndRecordingState(largeCode, smallCode, 1)
                .orElseThrow(() -> new IllegalStateException("사유 코드를 찾을 수 없습니다: " + smallCode))
                .getId();
    }
}
