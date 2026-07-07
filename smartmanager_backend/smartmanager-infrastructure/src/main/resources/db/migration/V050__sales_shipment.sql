-- TX-S1: 출고·납품 (SALES → DELIVERY)

ALTER TABLE sales_order_line
  ADD COLUMN shipped_qty DECIMAL(18, 4) NOT NULL DEFAULT 0 AFTER delivery_status;

CREATE TABLE IF NOT EXISTS sales_shipment (
  id              BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  shipment_no     VARCHAR(30)  NOT NULL,
  partner_id      BIGINT       NOT NULL,
  shipment_date   DATE         NOT NULL,
  sales_order_id  BIGINT       NULL,
  status          ENUM('ISSUED','CANCELLED') NOT NULL DEFAULT 'ISSUED',
  recording_state TINYINT      NOT NULL DEFAULT 1,
  created_by      VARCHAR(100) NULL,
  created_by_id   VARCHAR(100) NULL,
  created_at      DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100) NULL,
  updated_by_id   VARCHAR(100) NULL,
  updated_at      DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_sales_shipment_no (shipment_no, recording_state),
  INDEX idx_sales_shipment_partner (partner_id, shipment_date, recording_state),
  CONSTRAINT fk_sales_shipment_partner FOREIGN KEY (partner_id) REFERENCES company (id),
  CONSTRAINT fk_sales_shipment_order FOREIGN KEY (sales_order_id) REFERENCES sales_order (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS sales_shipment_line (
  id                  BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  sales_shipment_id   BIGINT         NOT NULL,
  line_no             SMALLINT       NOT NULL,
  sales_order_line_id BIGINT         NOT NULL,
  item_id             BIGINT         NOT NULL,
  shipment_qty        DECIMAL(18, 4) NOT NULL,
  unit_price          DECIMAL(18, 2) NOT NULL,
  amount              DECIMAL(18, 2) NOT NULL,
  recording_state     TINYINT        NOT NULL DEFAULT 1,
  created_by          VARCHAR(100)   NULL,
  created_by_id       VARCHAR(100)   NULL,
  created_at          DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by          VARCHAR(100)   NULL,
  updated_by_id       VARCHAR(100)   NULL,
  updated_at          DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_sales_shipment_line (sales_shipment_id, line_no, recording_state),
  INDEX idx_sales_shipment_line_order (sales_order_line_id, recording_state),
  CONSTRAINT fk_sales_shipment_line_header FOREIGN KEY (sales_shipment_id) REFERENCES sales_shipment (id),
  CONSTRAINT fk_sales_shipment_line_order FOREIGN KEY (sales_order_line_id) REFERENCES sales_order_line (id),
  CONSTRAINT fk_sales_shipment_line_item FOREIGN KEY (item_id) REFERENCES item (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('sales:shipment:read', '출고·납품 조회'),
('sales:shipment:write', '출고·납품 등록·취소')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('sales:shipment:read', 'sales:shipment:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'SALES_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'sales:shipment:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
