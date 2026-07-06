-- PRD-W1c: 작업지시

CREATE TABLE IF NOT EXISTS work_order (
  id              BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  work_plan_id    BIGINT         NOT NULL,
  order_num       VARCHAR(50)    NOT NULL,
  ordered_qty     DECIMAL(18, 4) NOT NULL,
  reported_qty    DECIMAL(18, 4) NOT NULL DEFAULT 0,
  status          ENUM('ISSUED', 'CANCELLED') NOT NULL DEFAULT 'ISSUED',
  recording_state TINYINT        NOT NULL DEFAULT 1,
  created_by      VARCHAR(100)   NULL,
  created_by_id   VARCHAR(100)   NULL,
  created_at      DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100)   NULL,
  updated_by_id   VARCHAR(100)   NULL,
  updated_at      DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_work_order_plan (work_plan_id, recording_state),
  UNIQUE KEY uk_work_order_num (order_num, recording_state),
  INDEX idx_work_order_status (status, recording_state),
  CONSTRAINT fk_work_order_plan FOREIGN KEY (work_plan_id) REFERENCES work_plan (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('production:work-order:read', '작업지시 조회'),
('production:work-order:write', '작업지시 발행')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('production:work-order:read', 'production:work-order:write')
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'production:work-order:read'
WHERE r.role_code = 'PRODUCTION_OPERATOR' AND r.recording_state = 1;
