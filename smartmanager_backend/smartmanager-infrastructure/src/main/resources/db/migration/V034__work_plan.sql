-- PRD-W1a: 작업계획 원장

CREATE TABLE IF NOT EXISTS work_plan (
  id                    BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  production_plan_id    BIGINT         NOT NULL,
  process_sequence_id   BIGINT         NOT NULL,
  work_center_id        BIGINT         NULL,
  work_distinction      ENUM('INHOUSE','OUTSOURCE') NOT NULL,
  planned_qty           DECIMAL(18, 4) NOT NULL,
  plan_start_date       DATE           NULL,
  plan_end_date         DATE           NULL,
  setup_time            INT            NOT NULL DEFAULT 0,
  standard_time         INT            NOT NULL DEFAULT 0,
  status                ENUM('PLANNED','CANCELLED') NOT NULL DEFAULT 'PLANNED',
  recording_state       TINYINT        NOT NULL DEFAULT 1,
  created_by            VARCHAR(100)   NULL,
  created_by_id         VARCHAR(100)   NULL,
  created_at            DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by            VARCHAR(100)   NULL,
  updated_by_id         VARCHAR(100)   NULL,
  updated_at            DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_work_plan (production_plan_id, process_sequence_id, work_distinction, recording_state),
  INDEX idx_work_plan_plan (production_plan_id, recording_state),
  INDEX idx_work_plan_wc (work_center_id, plan_start_date, recording_state),
  CONSTRAINT fk_work_plan_production_plan FOREIGN KEY (production_plan_id) REFERENCES production_plan (id),
  CONSTRAINT fk_work_plan_process FOREIGN KEY (process_sequence_id) REFERENCES process_sequence (id),
  CONSTRAINT fk_work_plan_work_center FOREIGN KEY (work_center_id) REFERENCES work_center (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('production:work-plan:read', '작업계획 조회'),
('production:work-plan:write', '작업계획 수립')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('production:work-plan:read', 'production:work-plan:write')
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'production:work-plan:read'
WHERE r.role_code = 'PRODUCTION_OPERATOR' AND r.recording_state = 1;
