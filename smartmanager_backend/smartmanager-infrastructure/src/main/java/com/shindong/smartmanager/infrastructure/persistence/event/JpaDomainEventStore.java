package com.shindong.smartmanager.infrastructure.persistence.event;

import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.domain.event.DomainEvent;
import org.springframework.stereotype.Component;
import org.springframework.transaction.annotation.Transactional;

@Component
public class JpaDomainEventStore implements DomainEventStore {

    private final SpringDataDomainEventRepository repository;

    public JpaDomainEventStore(SpringDataDomainEventRepository repository) {
        this.repository = repository;
    }

    @Override
    @Transactional
    public void append(DomainEvent event) {
        repository.save(new DomainEventJpaEntity(
                event.getEventId().toString(),
                event.getEventType(),
                event.getSchemaVersion(),
                event.getAggregateType(),
                event.getAggregateId(),
                event.getOccurredAt(),
                event.getActorUserId(),
                event.getPayloadJson()
        ));
    }
}
