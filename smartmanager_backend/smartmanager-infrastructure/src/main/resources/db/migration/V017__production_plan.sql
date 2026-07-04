-- PRD-W0: 생산계획 (production_plan)

CREATE TABLE IF NOT EXISTS production_plan (
  id                      BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  plan_no                 VARCHAR(30)    NOT NULL,
  sales_order_id          BIGINT         NOT NULL,
  sales_order_line_id     BIGINT         NOT NULL,
  item_id                 BIGINT         NOT NULL,
  planned_qty             DECIMAL(18, 4) NOT NULL,
  produced_qty            DECIMAL(18, 4) NOT NULL DEFAULT 0,
  requested_delivery_date DATE           NULL,
  status                  ENUM('PLANNED','IN_PROGRESS','COMPLETED','CANCELLED') NOT NULL DEFAULT 'PLANNED',
  recording_state         TINYINT        NOT NULL DEFAULT 1,
  created_by              VARCHAR(100)   NULL,
  created_by_id           VARCHAR(100)   NULL,
  created_at              DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by              VARCHAR(100)   NULL,
  updated_by_id           VARCHAR(100)   NULL,
  updated_at              DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_production_plan_no (plan_no, recording_state),
  UNIQUE KEY uk_production_plan_line (sales_order_line_id, recording_state),
  INDEX idx_production_plan_order (sales_order_id, recording_state),
  INDEX idx_production_plan_status (status, recording_state),
  CONSTRAINT fk_production_plan_order FOREIGN KEY (sales_order_id) REFERENCES sales_order (id),
  CONSTRAINT fk_production_plan_line FOREIGN KEY (sales_order_line_id) REFERENCES sales_order_line (id),
  CONSTRAINT fk_production_plan_item FOREIGN KEY (item_id) REFERENCES item (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('production:plan:read', '생산계획 조회'),
('production:plan:write', '생산계획 수립')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('production:plan:read', 'production:plan:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PRODUCTION_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'production:plan:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
