package com.shindong.smartmanager.infrastructure.persistence.inventory;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.Table;

@Entity
@Table(name = "misc_movement_no_sequence")
public class MiscMovementNoSequenceJpaEntity {

    @Id
    @Column(name = "prefix", length = 40)
    private String prefix;

    @Column(name = "last_seq", nullable = false)
    private int lastSeq;

    protected MiscMovementNoSequenceJpaEntity() {
    }

    public MiscMovementNoSequenceJpaEntity(String prefix, int lastSeq) {
        this.prefix = prefix;
        this.lastSeq = lastSeq;
    }

    public String getPrefix() {
        return prefix;
    }

    public int getLastSeq() {
        return lastSeq;
    }

    public void setLastSeq(int lastSeq) {
        this.lastSeq = lastSeq;
    }
}
