package com.shindong.smartmanager.infrastructure.persistence.code;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.Table;

@Entity
@Table(name = "code_group")
public class CodeGroupJpaEntity {

    @Id
    @Column(name = "code_group_key", length = 64)
    private String codeGroupKey;

    @Column(name = "large_code", nullable = false, length = 4, columnDefinition = "CHAR(4)")
    private String largeCode;

    @Column(name = "usage_type", nullable = false, length = 20)
    private String usageType;

    @Column(name = "exclude_codes", columnDefinition = "JSON")
    private String excludeCodes;

    protected CodeGroupJpaEntity() {
    }

    public String getCodeGroupKey() {
        return codeGroupKey;
    }

    public String getLargeCode() {
        return largeCode;
    }

    public String getUsageType() {
        return usageType;
    }

    public String getExcludeCodes() {
        return excludeCodes;
    }
}
