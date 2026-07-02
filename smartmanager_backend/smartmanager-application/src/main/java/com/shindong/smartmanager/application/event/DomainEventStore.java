package com.shindong.smartmanager.application.event;

import com.shindong.smartmanager.domain.event.DomainEvent;

/**
 * 도메인 이벤트 영속화 포트.
 */
public interface DomainEventStore {

    void append(DomainEvent event);
}
