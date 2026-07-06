-- PRD-W2: 자재투입 TX (작업일보와 분리)

CREATE TABLE IF NOT EXISTS material_issue (
  id                  BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  issue_num           VARCHAR(50)    NOT NULL,
  work_order_id       BIGINT         NOT NULL,
  issue_date          DATE           NOT NULL,
  status              ENUM('ISSUED','CANCELLED') NOT NULL DEFAULT 'ISSUED',
  recording_state     TINYINT        NOT NULL DEFAULT 1,
  created_by          VARCHAR(100)   NULL,
  created_by_id       VARCHAR(100)   NULL,
  created_at          DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by          VARCHAR(100)   NULL,
  updated_by_id       VARCHAR(100)   NULL,
  updated_at          DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_material_issue_num (issue_num, recording_state),
  INDEX idx_material_issue_wo (work_order_id, recording_state),
  CONSTRAINT fk_material_issue_wo FOREIGN KEY (work_order_id) REFERENCES work_order (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS material_issue_line (
  id                      BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  material_issue_id       BIGINT         NOT NULL,
  line_no                 SMALLINT       NOT NULL,
  item_id                 BIGINT         NOT NULL,
  item_composition_id     BIGINT         NULL,
  issue_qty               DECIMAL(18, 4) NOT NULL,
  source_location_code    VARCHAR(20)    NOT NULL,
  source_process_id       BIGINT         NULL,
  recording_state         TINYINT        NOT NULL DEFAULT 1,
  CONSTRAINT fk_mil_header FOREIGN KEY (material_issue_id) REFERENCES material_issue (id),
  CONSTRAINT fk_mil_item FOREIGN KEY (item_id) REFERENCES item (id),
  CONSTRAINT fk_mil_bom FOREIGN KEY (item_composition_id) REFERENCES item_composition (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('production:material-issue:read', '자재투입 조회'),
('production:material-issue:write', '자재투입 등록')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('production:material-issue:read', 'production:material-issue:write')
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'production:material-issue:read'
WHERE r.role_code = 'PRODUCTION_OPERATOR' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'production:material-issue:write'
WHERE r.role_code = 'PRODUCTION_OPERATOR' AND r.recording_state = 1;
