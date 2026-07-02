package com.shindong.smartmanager.infrastructure.persistence.code;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;

@Entity
@Table(name = "public_code")
public class PublicCodeJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "large_code", nullable = false, length = 20)
    private String largeCode;

    @Column(name = "large_name", nullable = false, length = 100)
    private String largeName;

    @Column(name = "small_code", length = 20)
    private String smallCode;

    @Column(name = "small_name", length = 100)
    private String smallName;

    @Column(name = "usage_type", nullable = false, length = 20)
    private String usageType;

    @Column(name = "recording_state", nullable = false, columnDefinition = "TINYINT")
    private int recordingState = 1;

    protected PublicCodeJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public String getLargeCode() {
        return largeCode;
    }

    public String getSmallCode() {
        return smallCode;
    }

    public String getSmallName() {
        return smallName;
    }

    public String getUsageType() {
        return usageType;
    }

    public int getRecordingState() {
        return recordingState;
    }
}
