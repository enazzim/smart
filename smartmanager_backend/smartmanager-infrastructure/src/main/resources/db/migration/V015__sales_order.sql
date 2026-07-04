-- TX-S1: 수주 (sales_order)

CREATE TABLE IF NOT EXISTS sales_order (
  id                      BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  order_no                VARCHAR(30)  NOT NULL,
  partner_id              BIGINT       NOT NULL,
  order_date              DATE         NOT NULL,
  requested_delivery_date DATE         NULL,
  status                  ENUM('DRAFT','CONFIRMED','CANCELLED') NOT NULL DEFAULT 'DRAFT',
  remark                  VARCHAR(500) NULL,
  recording_state         TINYINT      NOT NULL DEFAULT 1,
  created_by              VARCHAR(100) NULL,
  created_by_id           VARCHAR(100) NULL,
  created_at              DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by              VARCHAR(100) NULL,
  updated_by_id           VARCHAR(100) NULL,
  updated_at              DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  confirmed_at            DATETIME(3)  NULL,
  confirmed_by            VARCHAR(100) NULL,
  confirmed_by_id         VARCHAR(100) NULL,
  UNIQUE KEY uk_sales_order_no (order_no, recording_state),
  INDEX idx_sales_order_partner (partner_id, recording_state),
  INDEX idx_sales_order_status (status, recording_state),
  CONSTRAINT fk_sales_order_partner FOREIGN KEY (partner_id) REFERENCES company(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS sales_order_line (
  id                 BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  sales_order_id     BIGINT         NOT NULL,
  line_no            SMALLINT       NOT NULL,
  item_id            BIGINT         NOT NULL,
  order_qty          DECIMAL(18,4)  NOT NULL,
  unit_price         DECIMAL(18,2)  NOT NULL DEFAULT 0,
  amount             DECIMAL(18,2)  NOT NULL DEFAULT 0,
  delivery_date      DATE           NULL,
  fulfillment_route  ENUM('COMMODITY','MANUFACTURING') NOT NULL,
  recording_state    TINYINT        NOT NULL DEFAULT 1,
  UNIQUE KEY uk_sales_order_line (sales_order_id, line_no, recording_state),
  CONSTRAINT fk_sales_order_line_order FOREIGN KEY (sales_order_id) REFERENCES sales_order(id),
  CONSTRAINT fk_sales_order_line_item FOREIGN KEY (item_id) REFERENCES item(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('sales:order:read', '수주 조회'),
('sales:order:write', '수주 등록·수정'),
('sales:order:confirm', '수주 확정')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('sales:order:read', 'sales:order:write', 'sales:order:confirm')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'SALES_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'sales:order:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
