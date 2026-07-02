package com.shindong.smartmanager.infrastructure.persistence.event;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.time.Instant;

@Entity
@Table(name = "domain_event")
public class DomainEventJpaEntity {

    @Id
    @Column(name = "event_id", length = 36, nullable = false)
    private String eventId;

    @Column(name = "event_type", length = 64, nullable = false)
    private String eventType;

    @Column(name = "schema_version", nullable = false)
    private int schemaVersion;

    @Column(name = "aggregate_type", length = 64, nullable = false)
    private String aggregateType;

    @Column(name = "aggregate_id", length = 64, nullable = false)
    private String aggregateId;

    @Column(name = "occurred_at", nullable = false)
    private Instant occurredAt;

    @Column(name = "actor_user_id", length = 64)
    private String actorUserId;

    @Column(name = "payload_json", columnDefinition = "json", nullable = false)
    private String payloadJson;

    protected DomainEventJpaEntity() {
    }

    public DomainEventJpaEntity(
            String eventId,
            String eventType,
            int schemaVersion,
            String aggregateType,
            String aggregateId,
            Instant occurredAt,
            String actorUserId,
            String payloadJson
    ) {
        this.eventId = eventId;
        this.eventType = eventType;
        this.schemaVersion = schemaVersion;
        this.aggregateType = aggregateType;
        this.aggregateId = aggregateId;
        this.occurredAt = occurredAt;
        this.actorUserId = actorUserId;
        this.payloadJson = payloadJson;
    }

    public String getEventId() {
        return eventId;
    }
}
