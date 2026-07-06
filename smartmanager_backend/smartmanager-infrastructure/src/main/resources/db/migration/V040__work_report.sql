-- PRD-W1c: 작업일보 + 실적 이력

CREATE TABLE IF NOT EXISTS work_report (
  id              BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  report_num      VARCHAR(50)    NOT NULL,
  work_order_id   BIGINT         NOT NULL,
  report_date     DATE           NOT NULL,
  good_qty        DECIMAL(18, 4) NOT NULL,
  scrap_qty       DECIMAL(18, 4) NOT NULL DEFAULT 0,
  setup_time      DECIMAL(10, 2) NULL,
  run_time        DECIMAL(10, 2) NULL,
  worker_id       VARCHAR(50)    NULL,
  worker_name     VARCHAR(100)   NULL,
  status          ENUM('REGISTERED', 'CANCELLED') NOT NULL DEFAULT 'REGISTERED',
  stock_applied   TINYINT        NOT NULL DEFAULT 1,
  recording_state TINYINT        NOT NULL DEFAULT 1,
  created_by      VARCHAR(100)   NULL,
  created_by_id   VARCHAR(100)   NULL,
  created_at      DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100)   NULL,
  updated_by_id   VARCHAR(100)   NULL,
  updated_at      DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_work_report_num (report_num, recording_state),
  INDEX idx_work_report_order (work_order_id, recording_state),
  INDEX idx_work_report_date (report_date, recording_state),
  CONSTRAINT fk_work_report_order FOREIGN KEY (work_order_id) REFERENCES work_order (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS work_report_history (
  id                  BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  item_id             BIGINT         NOT NULL,
  production_plan_id  BIGINT         NOT NULL,
  process_sequence_id BIGINT         NOT NULL,
  good_qty            DECIMAL(18, 4) NOT NULL,
  scrap_qty           DECIMAL(18, 4) NOT NULL DEFAULT 0,
  history_date        DATE           NOT NULL,
  source_type         ENUM('WORK_REPORT', 'WORK_REPORT_CANCEL') NOT NULL,
  source_id           BIGINT         NOT NULL,
  fiscal_year         SMALLINT       NOT NULL,
  fiscal_month        TINYINT        NOT NULL,
  recording_state     TINYINT        NOT NULL DEFAULT 1,
  created_by          VARCHAR(100)   NULL,
  created_by_id       VARCHAR(100)   NULL,
  created_at          DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  INDEX idx_work_report_history_date (history_date, recording_state),
  INDEX idx_work_report_history_source (source_type, source_id, recording_state),
  CONSTRAINT fk_work_report_history_item FOREIGN KEY (item_id) REFERENCES item (id),
  CONSTRAINT fk_work_report_history_plan FOREIGN KEY (production_plan_id) REFERENCES production_plan (id),
  CONSTRAINT fk_work_report_history_process FOREIGN KEY (process_sequence_id) REFERENCES process_sequence (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('production:work-report:read', '작업일보 조회'),
('production:work-report:write', '작업일보 등록')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('production:work-report:read', 'production:work-report:write')
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'production:work-report:read'
WHERE r.role_code = 'PRODUCTION_OPERATOR' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'production:work-report:write'
WHERE r.role_code = 'PRODUCTION_OPERATOR' AND r.recording_state = 1;
