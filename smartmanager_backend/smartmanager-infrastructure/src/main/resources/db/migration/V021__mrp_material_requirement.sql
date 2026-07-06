-- PRD-W0: 자재소요(MRP) 산출 결과

CREATE TABLE IF NOT EXISTS mrp_run (
  id              BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  run_no          VARCHAR(30)  NOT NULL,
  recording_state TINYINT      NOT NULL DEFAULT 1,
  created_by      VARCHAR(100) NULL,
  created_by_id   VARCHAR(100) NULL,
  created_at      DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100) NULL,
  updated_by_id   VARCHAR(100) NULL,
  updated_at      DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_mrp_run_no (run_no, recording_state),
  INDEX idx_mrp_run_created (created_at, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS material_requirement_line (
  id                  BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  mrp_run_id          BIGINT         NOT NULL,
  production_plan_id  BIGINT         NOT NULL,
  parent_item_id      BIGINT         NOT NULL,
  component_item_id   BIGINT         NOT NULL,
  bom_unit_qty        DECIMAL(18, 4) NOT NULL,
  planned_qty         DECIMAL(18, 4) NOT NULL,
  gross_qty           DECIMAL(18, 4) NOT NULL,
  recording_state     TINYINT        NOT NULL DEFAULT 1,
  created_by          VARCHAR(100)   NULL,
  created_by_id       VARCHAR(100)   NULL,
  created_at          DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by          VARCHAR(100)   NULL,
  updated_by_id       VARCHAR(100)   NULL,
  updated_at          DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  INDEX idx_mrp_line_run (mrp_run_id, recording_state),
  INDEX idx_mrp_line_plan (production_plan_id, recording_state),
  INDEX idx_mrp_line_component (component_item_id, recording_state),
  CONSTRAINT fk_mrp_line_run FOREIGN KEY (mrp_run_id) REFERENCES mrp_run (id),
  CONSTRAINT fk_mrp_line_plan FOREIGN KEY (production_plan_id) REFERENCES production_plan (id),
  CONSTRAINT fk_mrp_line_parent_item FOREIGN KEY (parent_item_id) REFERENCES item (id),
  CONSTRAINT fk_mrp_line_component_item FOREIGN KEY (component_item_id) REFERENCES item (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('production:mrp:read', '자재소요 조회'),
('production:mrp:write', '자재소요 산출')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('production:mrp:read', 'production:mrp:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PRODUCTION_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'production:mrp:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
