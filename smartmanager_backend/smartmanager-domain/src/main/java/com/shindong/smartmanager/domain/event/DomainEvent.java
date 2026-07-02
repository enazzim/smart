package com.shindong.smartmanager.domain.event;

import java.time.Instant;
import java.util.Objects;
import java.util.UUID;

/**
 * 도메인 이벤트 envelope (저장소·리플레이 공통).
 * Ref: docs/step0/domain-event-projector-matrix.md §7
 */
public final class DomainEvent {

    private final UUID eventId;
    private final String eventType;
    private final int schemaVersion;
    private final String aggregateType;
    private final String aggregateId;
    private final Instant occurredAt;
    private final String actorUserId;
    private final String payloadJson;

    public DomainEvent(
            UUID eventId,
            String eventType,
            int schemaVersion,
            String aggregateType,
            String aggregateId,
            Instant occurredAt,
            String actorUserId,
            String payloadJson
    ) {
        this.eventId = Objects.requireNonNull(eventId, "eventId");
        this.eventType = Objects.requireNonNull(eventType, "eventType");
        this.schemaVersion = schemaVersion;
        this.aggregateType = Objects.requireNonNull(aggregateType, "aggregateType");
        this.aggregateId = Objects.requireNonNull(aggregateId, "aggregateId");
        this.occurredAt = Objects.requireNonNull(occurredAt, "occurredAt");
        this.actorUserId = actorUserId;
        this.payloadJson = Objects.requireNonNull(payloadJson, "payloadJson");
    }

    public static DomainEvent create(
            String eventType,
            int schemaVersion,
            String aggregateType,
            String aggregateId,
            String actorUserId,
            String payloadJson
    ) {
        return new DomainEvent(
                UUID.randomUUID(),
                eventType,
                schemaVersion,
                aggregateType,
                aggregateId,
                Instant.now(),
                actorUserId,
                payloadJson
        );
    }

    public UUID getEventId() {
        return eventId;
    }

    public String getEventType() {
        return eventType;
    }

    public int getSchemaVersion() {
        return schemaVersion;
    }

    public String getAggregateType() {
        return aggregateType;
    }

    public String getAggregateId() {
        return aggregateId;
    }

    public Instant getOccurredAt() {
        return occurredAt;
    }

    public String getActorUserId() {
        return actorUserId;
    }

    public String getPayloadJson() {
        return payloadJson;
    }
}
