-- TX-S2: 매출등록 (DELIVERY 감소)

ALTER TABLE sales_shipment_line
  ADD COLUMN invoiced_qty DECIMAL(18, 4) NOT NULL DEFAULT 0 AFTER amount;

CREATE TABLE IF NOT EXISTS sales_revenue (
  id              BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  revenue_no      VARCHAR(30)  NOT NULL,
  partner_id      BIGINT       NOT NULL,
  revenue_date    DATE         NOT NULL,
  sales_shipment_id BIGINT     NULL,
  status          ENUM('ISSUED','CANCELLED') NOT NULL DEFAULT 'ISSUED',
  recording_state TINYINT      NOT NULL DEFAULT 1,
  created_by      VARCHAR(100) NULL,
  created_by_id   VARCHAR(100) NULL,
  created_at      DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100) NULL,
  updated_by_id   VARCHAR(100) NULL,
  updated_at      DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_sales_revenue_no (revenue_no, recording_state),
  INDEX idx_sales_revenue_partner (partner_id, revenue_date, recording_state),
  CONSTRAINT fk_sales_revenue_partner FOREIGN KEY (partner_id) REFERENCES company (id),
  CONSTRAINT fk_sales_revenue_shipment FOREIGN KEY (sales_shipment_id) REFERENCES sales_shipment (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS sales_revenue_line (
  id                      BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  sales_revenue_id        BIGINT         NOT NULL,
  line_no                 SMALLINT       NOT NULL,
  sales_shipment_line_id  BIGINT         NOT NULL,
  item_id                 BIGINT         NOT NULL,
  revenue_qty             DECIMAL(18, 4) NOT NULL,
  unit_price              DECIMAL(18, 2) NOT NULL,
  amount                  DECIMAL(18, 2) NOT NULL,
  recording_state         TINYINT        NOT NULL DEFAULT 1,
  created_by              VARCHAR(100)   NULL,
  created_by_id           VARCHAR(100)   NULL,
  created_at              DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by              VARCHAR(100)   NULL,
  updated_by_id           VARCHAR(100)   NULL,
  updated_at              DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_sales_revenue_line (sales_revenue_id, line_no, recording_state),
  INDEX idx_sales_revenue_line_shipment (sales_shipment_line_id, recording_state),
  CONSTRAINT fk_sales_revenue_line_header FOREIGN KEY (sales_revenue_id) REFERENCES sales_revenue (id),
  CONSTRAINT fk_sales_revenue_line_shipment FOREIGN KEY (sales_shipment_line_id) REFERENCES sales_shipment_line (id),
  CONSTRAINT fk_sales_revenue_line_item FOREIGN KEY (item_id) REFERENCES item (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS sales_history (
  id            BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  company_id    BIGINT         NOT NULL,
  item_id       BIGINT         NOT NULL,
  sales_qty     DECIMAL(18, 4) NOT NULL,
  unit_price    DECIMAL(18, 2) NOT NULL,
  amount        DECIMAL(18, 2) NOT NULL,
  history_date  DATE           NOT NULL,
  source_type   ENUM('SALES_REVENUE') NOT NULL,
  source_id     BIGINT         NOT NULL,
  fiscal_year   SMALLINT       NOT NULL,
  fiscal_month  TINYINT        NOT NULL,
  recording_state TINYINT      NOT NULL DEFAULT 1,
  created_by    VARCHAR(100)   NULL,
  created_by_id VARCHAR(100)   NULL,
  created_at    DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  INDEX idx_sales_history_company_date (company_id, history_date, recording_state),
  INDEX idx_sales_history_source (source_type, source_id, recording_state),
  CONSTRAINT fk_sales_history_company FOREIGN KEY (company_id) REFERENCES company (id),
  CONSTRAINT fk_sales_history_item FOREIGN KEY (item_id) REFERENCES item (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('sales:revenue:read', '매출 조회'),
('sales:revenue:write', '매출 등록·취소')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('sales:revenue:read', 'sales:revenue:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'SALES_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'sales:revenue:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
