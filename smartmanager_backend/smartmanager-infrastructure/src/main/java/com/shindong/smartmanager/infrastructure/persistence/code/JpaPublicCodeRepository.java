package com.shindong.smartmanager.infrastructure.persistence.code;

import com.shindong.smartmanager.application.publiccode.CreateLargeCommand;
import com.shindong.smartmanager.application.publiccode.CreateSmallCommand;
import com.shindong.smartmanager.application.publiccode.PublicCodeLargeView;
import com.shindong.smartmanager.application.publiccode.PublicCodeRepository;
import com.shindong.smartmanager.application.publiccode.PublicCodeSmallView;
import com.shindong.smartmanager.application.publiccode.UpdateLargeCommand;
import com.shindong.smartmanager.application.publiccode.UpdateSmallCommand;
import java.time.Instant;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaPublicCodeRepository implements PublicCodeRepository {

    private final SpringDataPublicCodeRepository publicCodeRepository;

    public JpaPublicCodeRepository(SpringDataPublicCodeRepository publicCodeRepository) {
        this.publicCodeRepository = publicCodeRepository;
    }

    @Override
    @Transactional(readOnly = true)
    public List<PublicCodeLargeView> findActiveLargeHeaders() {
        return publicCodeRepository.findBySmallCodeIsNullAndRecordingStateOrderByLargeCodeAsc(1).stream()
                .map(this::toLargeView)
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public List<PublicCodeSmallView> findActiveSmallByLargeCode(String largeCode) {
        return publicCodeRepository
                .findByLargeCodeAndSmallCodeIsNotNullAndRecordingStateOrderBySmallCodeAsc(largeCode, 1)
                .stream()
                .map(this::toSmallView)
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public List<PublicCodeSmallView> findActiveSmallCodes(String largeCode, String usageType) {
        return publicCodeRepository.searchActiveSmallCodes(largeCode, usageType).stream()
                .map(this::toSmallView)
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<PublicCodeLargeView> findActiveLargeHeader(String largeCode) {
        return publicCodeRepository.findByLargeCodeAndSmallCodeIsNullAndRecordingState(largeCode, 1)
                .map(this::toLargeView);
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<PublicCodeSmallView> findActiveSmallById(long id) {
        return publicCodeRepository.findByIdAndRecordingState(id, 1)
                .filter(entity -> entity.getSmallCode() != null)
                .map(this::toSmallView);
    }

    @Override
    @Transactional
    public long saveLarge(CreateLargeCommand command, String actorUserId) {
        Instant now = Instant.now();
        PublicCodeJpaEntity entity = new PublicCodeJpaEntity();
        entity.setLargeCode(command.largeCode().trim());
        entity.setLargeName(command.largeName().trim());
        entity.setUsageType(command.usageType().trim());
        entity.setRecordingState(1);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedAt(now);
        return publicCodeRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public long saveSmall(CreateSmallCommand command, PublicCodeLargeView header, String actorUserId) {
        Instant now = Instant.now();
        PublicCodeJpaEntity entity = new PublicCodeJpaEntity();
        entity.setLargeCode(header.largeCode());
        entity.setLargeName(header.largeName());
        entity.setSmallCode(command.smallCode().trim());
        entity.setSmallName(command.smallName().trim());
        entity.setUsageType(header.usageType());
        entity.setRecordingState(1);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedAt(now);
        return publicCodeRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public void updateLarge(String largeCode, UpdateLargeCommand command, String actorUserId) {
        PublicCodeJpaEntity header = getActiveHeaderEntity(largeCode);
        Instant now = Instant.now();
        String nextLargeName = command.largeName().trim();
        String nextUsageType = command.usageType().trim();

        header.setLargeName(nextLargeName);
        header.setUsageType(nextUsageType);
        header.setUpdatedBy(actorUserId);
        header.setUpdatedAt(now);
        publicCodeRepository.save(header);

        for (PublicCodeJpaEntity small : publicCodeRepository.findByLargeCodeAndRecordingState(largeCode, 1)) {
            if (small.getSmallCode() == null) {
                continue;
            }
            small.setLargeName(nextLargeName);
            small.setUsageType(nextUsageType);
            small.setUpdatedBy(actorUserId);
            small.setUpdatedAt(now);
            publicCodeRepository.save(small);
        }
    }

    @Override
    @Transactional
    public void updateSmall(long id, UpdateSmallCommand command, String actorUserId) {
        PublicCodeJpaEntity entity = getActiveSmallEntity(id);
        entity.setSmallName(command.smallName().trim());
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedAt(Instant.now());
        publicCodeRepository.save(entity);
    }

    @Override
    @Transactional
    public void softDeleteLarge(String largeCode, String actorUserId) {
        Instant now = Instant.now();
        for (PublicCodeJpaEntity entity : publicCodeRepository.findByLargeCodeAndRecordingState(largeCode, 1)) {
            entity.setRecordingState(0);
            entity.setUpdatedBy(actorUserId);
            entity.setUpdatedAt(now);
            publicCodeRepository.save(entity);
        }
    }

    @Override
    @Transactional
    public void softDeleteSmall(long id, String actorUserId) {
        PublicCodeJpaEntity entity = getActiveSmallEntity(id);
        entity.setRecordingState(0);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedAt(Instant.now());
        publicCodeRepository.save(entity);
    }

    @Override
    @Transactional(readOnly = true)
    public boolean existsActiveLargeHeader(String largeCode) {
        return publicCodeRepository.existsByLargeCodeAndSmallCodeIsNullAndRecordingState(largeCode, 1);
    }

    @Override
    @Transactional(readOnly = true)
    public boolean existsActiveSmall(String largeCode, String smallCode) {
        return publicCodeRepository.existsByLargeCodeAndSmallCodeAndRecordingState(largeCode, smallCode, 1);
    }

    private PublicCodeJpaEntity getActiveHeaderEntity(String largeCode) {
        return publicCodeRepository.findByLargeCodeAndSmallCodeIsNullAndRecordingState(largeCode, 1)
                .orElseThrow(() -> new IllegalArgumentException("대분류를 찾을 수 없습니다: " + largeCode));
    }

    private PublicCodeJpaEntity getActiveSmallEntity(long id) {
        return publicCodeRepository.findByIdAndRecordingState(id, 1)
                .filter(entity -> entity.getSmallCode() != null)
                .orElseThrow(() -> new IllegalArgumentException("소분류를 찾을 수 없습니다: " + id));
    }

    private PublicCodeLargeView toLargeView(PublicCodeJpaEntity entity) {
        return new PublicCodeLargeView(
                entity.getId(),
                entity.getLargeCode(),
                entity.getLargeName(),
                entity.getUsageType()
        );
    }

    private PublicCodeSmallView toSmallView(PublicCodeJpaEntity entity) {
        return new PublicCodeSmallView(
                entity.getId(),
                entity.getLargeCode(),
                entity.getLargeName(),
                entity.getSmallCode(),
                entity.getSmallName(),
                entity.getUsageType()
        );
    }
}
