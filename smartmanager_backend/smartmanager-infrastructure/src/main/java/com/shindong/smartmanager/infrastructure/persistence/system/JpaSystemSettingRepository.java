package com.shindong.smartmanager.infrastructure.persistence.system;

import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;

import com.shindong.smartmanager.application.system.SystemSettingRepository;
import com.shindong.smartmanager.application.system.SystemSettingView;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;

@Repository
public class JpaSystemSettingRepository implements SystemSettingRepository {

    private static final int ACTIVE = 1;

    private final SpringDataSystemSettingRepository repository;
    private final MasterAuditActorLookup masterAuditActorLookup;

    public JpaSystemSettingRepository(
            SpringDataSystemSettingRepository repository,
            MasterAuditActorLookup masterAuditActorLookup
    ) {
        this.repository = repository;
        this.masterAuditActorLookup = masterAuditActorLookup;
    }

    @Override
    public List<SystemSettingView> findAllActive() {
        return repository.findAllByRecordingStateOrderBySettingKeyAsc(ACTIVE).stream()
                .map(this::toView)
                .toList();
    }

    @Override
    public Optional<SystemSettingView> findActiveByKey(String settingKey) {
        return repository.findBySettingKeyAndRecordingState(settingKey, ACTIVE)
                .map(this::toView);
    }

    @Override
    public void upsert(String settingKey, String valueJson, String actorUserId) {
        SystemSettingJpaEntity entity = repository.findBySettingKeyAndRecordingState(settingKey, ACTIVE)
                .orElse(null);
        if (entity == null) {
            repository.save(SystemSettingJpaEntity.create(
                    settingKey,
                    valueJson,
                    masterAuditActorLookup.idOf(actorUserId)
            ));
            return;
        }
        entity.updateValue(valueJson, masterAuditActorLookup.idOf(actorUserId));
        repository.save(entity);
    }

    private SystemSettingView toView(SystemSettingJpaEntity entity) {
        return new SystemSettingView(
                entity.getSettingKey(),
                entity.getValueJson(),
                entity.getUpdatedAt(),
                masterAuditActorLookup.nameOf(entity.getUpdatedById())
        );
    }
}
