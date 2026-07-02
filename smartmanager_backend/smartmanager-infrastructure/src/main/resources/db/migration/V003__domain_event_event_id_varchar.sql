-- Align domain_event.event_id with JPA VARCHAR(36) mapping (was CHAR(36) in early V001)
ALTER TABLE domain_event
  MODIFY COLUMN event_id VARCHAR(36) NOT NULL;
