-- =============================================================================
-- V006__outsource_sales_fulfill.sql — 외주·출고·매출·수금·수동계획·선출고
-- Squash of former V046–V055 (chronological concat, 2026-08-01)
-- =============================================================================


-- ---------------------------------------------------------------------------
-- BEGIN former V046__outsourcing_order.sql
-- ---------------------------------------------------------------------------

-- INF-4a: 외주발주

CREATE TABLE IF NOT EXISTS outsourcing_order (
  id              BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  order_no        VARCHAR(30)  NOT NULL,
  partner_id      BIGINT       NOT NULL,
  order_date      DATE         NOT NULL,
  source_type     ENUM('WORK_PLAN','MANUAL') NOT NULL DEFAULT 'MANUAL',
  status          ENUM('CONFIRMED','IN_PROGRESS','RECEIVED','CANCELLED') NOT NULL DEFAULT 'CONFIRMED',
  recording_state TINYINT      NOT NULL DEFAULT 1,
  created_by      VARCHAR(100) NULL,
  created_by_id   VARCHAR(100) NULL,
  created_at      DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100) NULL,
  updated_by_id   VARCHAR(100) NULL,
  updated_at      DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_outsourcing_order_no (order_no, recording_state),
  INDEX idx_outsourcing_order_partner (partner_id, recording_state),
  INDEX idx_outsourcing_order_date (order_date, recording_state),
  CONSTRAINT fk_outsourcing_order_partner FOREIGN KEY (partner_id) REFERENCES company (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS outsourcing_order_line (
  id                      BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  outsourcing_order_id    BIGINT         NOT NULL,
  line_no                 SMALLINT       NOT NULL,
  item_id                 BIGINT         NOT NULL,
  process_sequence_id     BIGINT         NOT NULL,
  begin_process_code_id   BIGINT         NOT NULL,
  end_process_code_id     BIGINT         NOT NULL,
  work_plan_id            BIGINT         NULL,
  order_qty               DECIMAL(18, 4) NOT NULL,
  shipped_qty             DECIMAL(18, 4) NOT NULL DEFAULT 0,
  received_qty            DECIMAL(18, 4) NOT NULL DEFAULT 0,
  unit_price              DECIMAL(18, 2) NOT NULL DEFAULT 0,
  amount                  DECIMAL(18, 2) NOT NULL DEFAULT 0,
  requested_delivery_date DATE           NULL,
  recording_state         TINYINT        NOT NULL DEFAULT 1,
  created_by              VARCHAR(100)   NULL,
  created_by_id           VARCHAR(100)   NULL,
  created_at              DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by              VARCHAR(100)   NULL,
  updated_by_id           VARCHAR(100)   NULL,
  updated_at              DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_outsourcing_order_line (outsourcing_order_id, line_no, recording_state),
  INDEX idx_outsourcing_order_line_item (item_id, recording_state),
  INDEX idx_outsourcing_order_line_work_plan (work_plan_id, recording_state),
  CONSTRAINT fk_outsourcing_order_line_order FOREIGN KEY (outsourcing_order_id) REFERENCES outsourcing_order (id),
  CONSTRAINT fk_outsourcing_order_line_item FOREIGN KEY (item_id) REFERENCES item (id),
  CONSTRAINT fk_outsourcing_order_line_process FOREIGN KEY (process_sequence_id) REFERENCES process_sequence (id),
  CONSTRAINT fk_outsourcing_order_line_begin_process FOREIGN KEY (begin_process_code_id) REFERENCES public_code (id),
  CONSTRAINT fk_outsourcing_order_line_end_process FOREIGN KEY (end_process_code_id) REFERENCES public_code (id),
  CONSTRAINT fk_outsourcing_order_line_work_plan FOREIGN KEY (work_plan_id) REFERENCES work_plan (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('outsource:order:read', '외주발주 조회'),
('outsource:order:write', '외주발주 등록·취소')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('outsource:order:read', 'outsource:order:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PRODUCTION_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'outsource:order:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;

-- END former V046__outsourcing_order.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V047__outsourcing_shipment.sql
-- ---------------------------------------------------------------------------

-- INF-4b: 외주출고

CREATE TABLE IF NOT EXISTS outsourcing_shipment (
  id              BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  shipment_no     VARCHAR(30)  NOT NULL,
  shipment_date   DATE         NOT NULL,
  status          ENUM('ISSUED','CANCELLED') NOT NULL DEFAULT 'ISSUED',
  recording_state TINYINT      NOT NULL DEFAULT 1,
  created_by      VARCHAR(100) NULL,
  created_by_id   VARCHAR(100) NULL,
  created_at      DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100) NULL,
  updated_by_id   VARCHAR(100) NULL,
  updated_at      DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_outsourcing_shipment_no (shipment_no, recording_state),
  INDEX idx_outsourcing_shipment_date (shipment_date, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS outsourcing_shipment_line (
  id                        BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  outsourcing_shipment_id   BIGINT         NOT NULL,
  line_no                   SMALLINT       NOT NULL,
  outsourcing_order_line_id BIGINT         NOT NULL,
  shipment_qty              DECIMAL(18, 4) NOT NULL,
  recording_state           TINYINT        NOT NULL DEFAULT 1,
  created_by                VARCHAR(100)   NULL,
  created_by_id             VARCHAR(100)   NULL,
  created_at                DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by                VARCHAR(100)   NULL,
  updated_by_id             VARCHAR(100)   NULL,
  updated_at                DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_outsourcing_shipment_line (outsourcing_shipment_id, line_no, recording_state),
  INDEX idx_outsourcing_shipment_line_order (outsourcing_order_line_id, recording_state),
  CONSTRAINT fk_outsourcing_shipment_line_header FOREIGN KEY (outsourcing_shipment_id) REFERENCES outsourcing_shipment (id),
  CONSTRAINT fk_outsourcing_shipment_line_order FOREIGN KEY (outsourcing_order_line_id) REFERENCES outsourcing_order_line (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS outsourcing_shipment_input_line (
  id                          BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  outsourcing_shipment_line_id BIGINT        NOT NULL,
  line_no                     SMALLINT       NOT NULL,
  item_id                     BIGINT         NOT NULL,
  item_composition_id         BIGINT         NULL,
  issue_qty                   DECIMAL(18, 4) NOT NULL,
  source_location_code        VARCHAR(20)    NOT NULL,
  source_process_id           BIGINT         NULL,
  input_process_id            BIGINT         NOT NULL,
  recording_state             TINYINT        NOT NULL DEFAULT 1,
  CONSTRAINT fk_osil_shipment_line FOREIGN KEY (outsourcing_shipment_line_id) REFERENCES outsourcing_shipment_line (id),
  CONSTRAINT fk_osil_item FOREIGN KEY (item_id) REFERENCES item (id),
  CONSTRAINT fk_osil_bom FOREIGN KEY (item_composition_id) REFERENCES item_composition (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('outsource:shipment:read', '외주출고 조회'),
('outsource:shipment:write', '외주출고 등록·취소')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('outsource:shipment:read', 'outsource:shipment:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PRODUCTION_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'outsource:shipment:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;

-- END former V047__outsourcing_shipment.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V048__inventory_allow_negative_stock_setting.sql
-- ---------------------------------------------------------------------------

-- 재고: 마이너스 재고 허용 여부 (기본 예)

INSERT INTO system_settings (setting_key, value_json, created_by, created_by_id)
VALUES ('inventory.allow_negative_stock', '"YES"', 'system', 'system')
ON DUPLICATE KEY UPDATE value_json = VALUES(value_json);

-- END former V048__inventory_allow_negative_stock_setting.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V049__outsourcing_receipt.sql
-- ---------------------------------------------------------------------------

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

-- END former V049__outsourcing_receipt.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V050__sales_shipment.sql
-- ---------------------------------------------------------------------------

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

-- END former V050__sales_shipment.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V051__sales_revenue.sql
-- ---------------------------------------------------------------------------

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

-- END former V051__sales_revenue.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V052__sales_collection.sql
-- ---------------------------------------------------------------------------

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

-- END former V052__sales_collection.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V053__inventory_location_wip_label.sql
-- ---------------------------------------------------------------------------

-- WIP 창고 표시명: 생산창고 → 공정창고 (UI 표준)

UPDATE inventory_location
SET location_name = '공정창고'
WHERE location_code = 'WIP';

-- END former V053__inventory_location_wip_label.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V054__production_plan_manual.sql
-- ---------------------------------------------------------------------------

-- Wave②: 수주 없이 생산계획 수립 (MANUAL)

ALTER TABLE production_plan
  ADD COLUMN source_type ENUM('SALES_ORDER', 'MANUAL') NOT NULL DEFAULT 'SALES_ORDER' AFTER plan_no;

ALTER TABLE production_plan
  MODIFY COLUMN sales_order_id BIGINT NULL,
  MODIFY COLUMN sales_order_line_id BIGINT NULL;

-- END former V054__production_plan_manual.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V055__outsourcing_shipment_advance.sql
-- ---------------------------------------------------------------------------

-- Wave③: 외주 선출고 (발주 연동 없이·발주 있어도 별도 출고)

ALTER TABLE outsourcing_shipment
  ADD COLUMN shipment_type ENUM('ORDER', 'ADVANCE') NOT NULL DEFAULT 'ORDER' AFTER shipment_date,
  ADD COLUMN partner_id BIGINT NULL AFTER shipment_type,
  ADD INDEX idx_outsourcing_shipment_partner (partner_id, recording_state),
  ADD CONSTRAINT fk_outsourcing_shipment_partner FOREIGN KEY (partner_id) REFERENCES company (id);

ALTER TABLE outsourcing_shipment_line
  MODIFY COLUMN outsourcing_order_line_id BIGINT NULL,
  ADD COLUMN parent_item_id BIGINT NULL AFTER outsourcing_order_line_id,
  ADD COLUMN begin_process_code_id BIGINT NULL AFTER parent_item_id,
  ADD COLUMN end_process_code_id BIGINT NULL AFTER begin_process_code_id,
  ADD CONSTRAINT fk_osl_parent_item FOREIGN KEY (parent_item_id) REFERENCES item (id);

-- END former V055__outsourcing_shipment_advance.sql

