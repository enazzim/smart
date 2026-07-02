package com.shindong.smartmanager.infrastructure.persistence.workcenter;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.time.Instant;

@Entity
@Table(name = "work_center")
public class WorkCenterJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "wc_name", nullable = false, length = 100)
    private String wcName;

    @Column(name = "main_process_code_id", nullable = false)
    private Long mainProcessCodeId;

    @Column(name = "recording_state", nullable = false, columnDefinition = "TINYINT")
    private int recordingState = 1;

    protected WorkCenterJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public String getWcName() {
        return wcName;
    }

    public Long getMainProcessCodeId() {
        return mainProcessCodeId;
    }

    public int getRecordingState() {
        return recordingState;
    }
}
