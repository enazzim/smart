-- INF-4c: 외주입고(납품) + outsource_history + QI FK 확장

ALTER TABLE outsourcing_order_line
  ADD COLUMN waiting_inspection_qty DECIMAL(18, 4) NOT NULL DEFAULT 0 AFTER received_qty;

CREATE TABLE IF NOT EXISTS outsourcing_receipt (
  id                    BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  receipt_no            VARCHAR(30)  NOT NULL,
  partner_id            BIGINT       NOT NULL,
  receipt_date          DATE         NOT NULL,
  outsourcing_order_id  BIGINT       NULL,
  status                ENUM('REGISTERED','PARTIALLY_POSTED','POSTED','CANCELLED') NOT NULL DEFAULT 'REGISTERED',
  recording_state       TINYINT      NOT NULL DEFAULT 1,
  created_by            VARCHAR(100) NULL,
  created_by_id         VARCHAR(100) NULL,
  created_at            DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by            VARCHAR(100) NULL,
  updated_by_id         VARCHAR(100) NULL,
  updated_at            DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_outsourcing_receipt_no (receipt_no, recording_state),
  INDEX idx_outsourcing_receipt_partner (partner_id, receipt_date, recording_state),
  CONSTRAINT fk_outsourcing_receipt_partner FOREIGN KEY (partner_id) REFERENCES company (id),
  CONSTRAINT fk_outsourcing_receipt_order FOREIGN KEY (outsourcing_order_id) REFERENCES outsourcing_order (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS outsourcing_receipt_line (
  id                        BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  outsourcing_receipt_id    BIGINT         NOT NULL,
  line_no                   SMALLINT       NOT NULL,
  outsourcing_order_line_id BIGINT         NOT NULL,
  item_id                   BIGINT         NOT NULL,
  receipt_qty               DECIMAL(18, 4) NOT NULL,
  posted_qty                DECIMAL(18, 4) NOT NULL DEFAULT 0,
  unit_price                DECIMAL(18, 2) NOT NULL,
  amount                    DECIMAL(18, 2) NOT NULL,
  recording_state           TINYINT        NOT NULL DEFAULT 1,
  created_by                VARCHAR(100)   NULL,
  created_by_id             VARCHAR(100)   NULL,
  created_at                DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by                VARCHAR(100)   NULL,
  updated_by_id             VARCHAR(100)   NULL,
  updated_at                DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_outsourcing_receipt_line (outsourcing_receipt_id, line_no, recording_state),
  INDEX idx_outsourcing_receipt_line_order (outsourcing_order_line_id, recording_state),
  CONSTRAINT fk_outsourcing_receipt_line_header FOREIGN KEY (outsourcing_receipt_id) REFERENCES outsourcing_receipt (id),
  CONSTRAINT fk_outsourcing_receipt_line_order FOREIGN KEY (outsourcing_order_line_id) REFERENCES outsourcing_order_line (id),
  CONSTRAINT fk_outsourcing_receipt_line_item FOREIGN KEY (item_id) REFERENCES item (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS outsource_history (
  id            BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  company_id    BIGINT         NOT NULL,
  item_id       BIGINT         NOT NULL,
  outsource_qty DECIMAL(18, 4) NOT NULL,
  unit_price    DECIMAL(18, 2) NOT NULL,
  amount        DECIMAL(18, 2) NOT NULL,
  history_date  DATE           NOT NULL,
  source_type   ENUM('OUTSOURCING_RECEIPT','QUALITY_INSPECTION') NOT NULL,
  source_id     BIGINT         NOT NULL,
  fiscal_year   SMALLINT       NOT NULL,
  fiscal_month  TINYINT        NOT NULL,
  recording_state TINYINT      NOT NULL DEFAULT 1,
  created_by    VARCHAR(100)   NULL,
  created_by_id VARCHAR(100)   NULL,
  created_at    DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  INDEX idx_outsource_history_company_date (company_id, history_date, recording_state),
  INDEX idx_outsource_history_source (source_type, source_id, recording_state),
  CONSTRAINT fk_outsource_history_company FOREIGN KEY (company_id) REFERENCES company (id),
  CONSTRAINT fk_outsource_history_item FOREIGN KEY (item_id) REFERENCES item (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

ALTER TABLE quality_inspection DROP FOREIGN KEY fk_qi_receipt_line;

INSERT INTO permission (permission_code, description) VALUES
('outsource:receipt:read', '외주입고 조회'),
('outsource:receipt:write', '외주입고 등록·취소')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('outsource:receipt:read', 'outsource:receipt:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PRODUCTION_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'outsource:receipt:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
