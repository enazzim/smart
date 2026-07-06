package com.shindong.smartmanager.infrastructure.persistence.system;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.time.Instant;

@Entity
@Table(name = "system_settings")
public class SystemSettingJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "setting_key", nullable = false, length = 100)
    private String settingKey;

    @Column(name = "value_json", nullable = false, columnDefinition = "json")
    private String valueJson;

    @Column(name = "recording_state", nullable = false, columnDefinition = "TINYINT")
    private int recordingState = 1;

    @Column(name = "created_by", length = 100)
    private String createdBy;

    @Column(name = "created_by_id", length = 100)
    private String createdById;

    @Column(name = "created_at", nullable = false)
    private Instant createdAt;

    @Column(name = "updated_by", length = 100)
    private String updatedBy;

    @Column(name = "updated_by_id", length = 100)
    private String updatedById;

    @Column(name = "updated_at")
    private Instant updatedAt;

    protected SystemSettingJpaEntity() {
    }

    public static SystemSettingJpaEntity create(String settingKey, String valueJson, String actorUserId) {
        SystemSettingJpaEntity entity = new SystemSettingJpaEntity();
        entity.settingKey = settingKey;
        entity.valueJson = valueJson;
        entity.recordingState = 1;
        entity.createdBy = actorUserId;
        entity.createdById = actorUserId;
        entity.createdAt = Instant.now();
        entity.updatedBy = actorUserId;
        entity.updatedById = actorUserId;
        entity.updatedAt = entity.createdAt;
        return entity;
    }

    public void updateValue(String valueJson, String actorUserId) {
        this.valueJson = valueJson;
        this.updatedBy = actorUserId;
        this.updatedById = actorUserId;
        this.updatedAt = Instant.now();
    }

    public String getSettingKey() {
        return settingKey;
    }

    public String getValueJson() {
        return valueJson;
    }

    public int getRecordingState() {
        return recordingState;
    }

    public Instant getUpdatedAt() {
        return updatedAt;
    }

    public String getUpdatedBy() {
        return updatedBy;
    }
}
