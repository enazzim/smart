-- INF-2: 회계월 마감 (month_closing)

CREATE TABLE IF NOT EXISTS month_closing (
  id              BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  fiscal_year     SMALLINT     NOT NULL,
  fiscal_month    TINYINT      NOT NULL,
  closed_at       DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  closed_by       VARCHAR(100) NOT NULL,
  closed_by_id    VARCHAR(100) NULL,
  recording_state TINYINT      NOT NULL DEFAULT 1,
  UNIQUE KEY uk_month_closing_period (fiscal_year, fiscal_month, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('system:month-closing:read', '월마감 조회')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'system:month-closing:read'
WHERE r.role_code IN ('SYSTEM_ADMIN', 'BASIS_MANAGER') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'system:month-closing:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
