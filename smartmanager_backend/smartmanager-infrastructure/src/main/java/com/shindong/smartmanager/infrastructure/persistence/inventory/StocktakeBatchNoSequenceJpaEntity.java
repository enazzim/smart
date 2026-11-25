package com.shindong.smartmanager.infrastructure.persistence.inventory;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.Table;

@Entity
@Table(name = "stocktake_batch_no_sequence")
public class StocktakeBatchNoSequenceJpaEntity {

    @Id
    @Column(name = "prefix", length = 40)
    private String prefix;

    @Column(name = "last_seq", nullable = false)
    private int lastSeq;

    protected StocktakeBatchNoSequenceJpaEntity() {
    }

    public StocktakeBatchNoSequenceJpaEntity(String prefix, int lastSeq) {
        this.prefix = prefix;
        this.lastSeq = lastSeq;
    }

    public int getLastSeq() {
        return lastSeq;
    }

    public void setLastSeq(int lastSeq) {
        this.lastSeq = lastSeq;
    }
}
