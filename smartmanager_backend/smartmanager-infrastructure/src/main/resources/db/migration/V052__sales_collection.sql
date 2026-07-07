-- TX-S3: 수금 (매출 잔액 회수)

ALTER TABLE partner_ledger_monthly
  ADD COLUMN collected_amount DECIMAL(18, 2) NOT NULL DEFAULT 0 AFTER sale_amount;

CREATE TABLE IF NOT EXISTS sales_collection (
  id               BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  collection_no    VARCHAR(30)    NOT NULL,
  partner_id       BIGINT         NOT NULL,
  collection_date  DATE           NOT NULL,
  supply_amount    DECIMAL(18, 2) NOT NULL,
  vat_amount       DECIMAL(18, 2) NOT NULL DEFAULT 0,
  total_amount     DECIMAL(18, 2) NOT NULL,
  payment_method   VARCHAR(50)    NULL,
  remark           VARCHAR(500)   NULL,
  status           ENUM('ISSUED','CANCELLED') NOT NULL DEFAULT 'ISSUED',
  recording_state  TINYINT        NOT NULL DEFAULT 1,
  created_by       VARCHAR(100)   NULL,
  created_by_id    VARCHAR(100)   NULL,
  created_at       DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by       VARCHAR(100)   NULL,
  updated_by_id    VARCHAR(100)   NULL,
  updated_at       DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_sales_collection_no (collection_no, recording_state),
  INDEX idx_sales_collection_partner (partner_id, collection_date, recording_state),
  CONSTRAINT fk_sales_collection_partner FOREIGN KEY (partner_id) REFERENCES company (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('sales:collection:read', '수금 조회'),
('sales:collection:write', '수금 등록·취소')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('sales:collection:read', 'sales:collection:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'SALES_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'sales:collection:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
