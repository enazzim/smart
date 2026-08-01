-- =============================================================================
-- V004__purchase_receipt_qi.sql — 구매발주·입고·품질검사
-- Squash of former V023–V033 (chronological concat, 2026-08-01)
-- =============================================================================


-- ---------------------------------------------------------------------------
-- BEGIN former V023__purchase_order_tx1_skeleton.sql
-- ---------------------------------------------------------------------------

-- TX1 skeleton: 구매발주 (MRP material_requirement_line 연동)

CREATE TABLE IF NOT EXISTS purchase_order (
  id              BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  order_no        VARCHAR(30)  NOT NULL,
  partner_id      BIGINT       NOT NULL,
  order_date      DATE         NOT NULL,
  source_type     ENUM('MRP','SALES_ORDER','MANUAL') NOT NULL DEFAULT 'MANUAL',
  status          ENUM('DRAFT','CONFIRMED','CANCELLED') NOT NULL DEFAULT 'DRAFT',
  recording_state TINYINT      NOT NULL DEFAULT 1,
  created_by      VARCHAR(100) NULL,
  created_by_id   VARCHAR(100) NULL,
  created_at      DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100) NULL,
  updated_by_id   VARCHAR(100) NULL,
  updated_at      DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_purchase_order_no (order_no, recording_state),
  INDEX idx_purchase_order_partner (partner_id, recording_state),
  INDEX idx_purchase_order_source (source_type, recording_state),
  CONSTRAINT fk_purchase_order_partner FOREIGN KEY (partner_id) REFERENCES company (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS purchase_order_line (
  id                    BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  purchase_order_id     BIGINT         NOT NULL,
  line_no               SMALLINT       NOT NULL,
  item_id               BIGINT         NOT NULL,
  order_qty             DECIMAL(18, 4) NOT NULL,
  unit_price            DECIMAL(18, 2) NOT NULL DEFAULT 0,
  amount                DECIMAL(18, 2) NOT NULL DEFAULT 0,
  requirement_line_id   BIGINT         NULL,
  recording_state       TINYINT        NOT NULL DEFAULT 1,
  created_by            VARCHAR(100)   NULL,
  created_by_id         VARCHAR(100)   NULL,
  created_at            DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by            VARCHAR(100)   NULL,
  updated_by_id         VARCHAR(100)   NULL,
  updated_at            DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_purchase_order_line (purchase_order_id, line_no, recording_state),
  INDEX idx_purchase_order_line_item (item_id, recording_state),
  INDEX idx_purchase_order_line_requirement (requirement_line_id, recording_state),
  CONSTRAINT fk_purchase_order_line_order FOREIGN KEY (purchase_order_id) REFERENCES purchase_order (id),
  CONSTRAINT fk_purchase_order_line_item FOREIGN KEY (item_id) REFERENCES item (id),
  CONSTRAINT fk_purchase_order_line_requirement FOREIGN KEY (requirement_line_id)
    REFERENCES material_requirement_line (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- END former V023__purchase_order_tx1_skeleton.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V024__system_settings.sql
-- ---------------------------------------------------------------------------

-- S0: 시스템 기능 설정 (SCS_T)

CREATE TABLE IF NOT EXISTS system_settings (
  id              BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  setting_key     VARCHAR(100) NOT NULL,
  value_json      JSON         NOT NULL,
  recording_state TINYINT      NOT NULL DEFAULT 1,
  created_by      VARCHAR(100) NULL,
  created_by_id   VARCHAR(100) NULL,
  created_at      DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100) NULL,
  updated_by_id   VARCHAR(100) NULL,
  updated_at      DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_system_settings_key (setting_key, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO system_settings (setting_key, value_json, created_by, created_by_id)
VALUES ('mrp.grouping_mode', '"BY_PLAN"', 'system', 'system');

-- END former V024__system_settings.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V025__purchase_order_permissions.sql
-- ---------------------------------------------------------------------------

-- TX1: 구매발주 권한

INSERT INTO permission (permission_code, description) VALUES
('purchase:order:read', '구매발주 조회'),
('purchase:order:write', '구매발주 등록·수정'),
('purchase:order:confirm', '구매발주 확정')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('purchase:order:read', 'purchase:order:write', 'purchase:order:confirm')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PURCHASE_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'purchase:order:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;

-- END former V025__purchase_order_permissions.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V026__sync_purchase_order_admin_permissions.sql
-- ---------------------------------------------------------------------------

-- SYSTEM_ADMIN에 purchase:order 권한 재동기화

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('purchase:order:read', 'purchase:order:write', 'purchase:order:confirm')
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

-- END former V026__sync_purchase_order_admin_permissions.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V027__purchase_order_line_delivery_date.sql
-- ---------------------------------------------------------------------------

-- TX1: 구매발주 라인 납기요구일 (품목 리드타임 기반)

ALTER TABLE purchase_order_line
  ADD COLUMN requested_delivery_date DATE NULL AFTER amount;

-- END former V027__purchase_order_line_delivery_date.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V028__inventory_movement_monthly.sql
-- ---------------------------------------------------------------------------

-- INF-1b: stock_movement, inventory_balance_monthly, 현재고 수량

ALTER TABLE inventory_balance
  ADD COLUMN stock_qty DECIMAL(18, 4) NOT NULL DEFAULT 0 AFTER partner_id,
  ADD COLUMN stock_amount DECIMAL(18, 2) NOT NULL DEFAULT 0 AFTER stock_qty;

CREATE TABLE IF NOT EXISTS stock_movement (
  id                  BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  inventory_balance_id BIGINT        NOT NULL,
  item_id             BIGINT         NOT NULL,
  location_id         BIGINT         NOT NULL,
  fiscal_year         SMALLINT       NOT NULL,
  fiscal_month        TINYINT        NOT NULL,
  movement_type       ENUM('IN','OUT','ADJUST') NOT NULL,
  qty                 DECIMAL(18, 4) NOT NULL,
  amount              DECIMAL(18, 2) NOT NULL DEFAULT 0,
  reference_type      VARCHAR(50)    NOT NULL,
  reference_id        BIGINT         NOT NULL,
  movement_date       DATE           NOT NULL,
  output_process_id   BIGINT         NULL,
  input_process_id    BIGINT         NULL,
  partner_id          BIGINT         NULL,
  recording_state     TINYINT        NOT NULL DEFAULT 1,
  created_by          VARCHAR(100)   NULL,
  created_by_id       VARCHAR(100)   NULL,
  created_at          DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  INDEX idx_stock_movement_item_date (item_id, movement_date, recording_state),
  INDEX idx_stock_movement_reference (reference_type, reference_id, recording_state),
  CONSTRAINT fk_stock_movement_balance FOREIGN KEY (inventory_balance_id) REFERENCES inventory_balance (id),
  CONSTRAINT fk_stock_movement_item FOREIGN KEY (item_id) REFERENCES item (id),
  CONSTRAINT fk_stock_movement_location FOREIGN KEY (location_id) REFERENCES inventory_location (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS inventory_balance_monthly (
  id                    BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  inventory_balance_id  BIGINT         NOT NULL,
  month_num             TINYINT        NOT NULL,
  in_qty                DECIMAL(18, 4) NOT NULL DEFAULT 0,
  in_amount             DECIMAL(18, 2) NOT NULL DEFAULT 0,
  out_qty               DECIMAL(18, 4) NOT NULL DEFAULT 0,
  out_amount            DECIMAL(18, 2) NOT NULL DEFAULT 0,
  stock_qty             DECIMAL(18, 4) NOT NULL DEFAULT 0,
  recording_state       TINYINT        NOT NULL DEFAULT 1,
  created_at            DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_at            DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_inventory_balance_monthly (inventory_balance_id, month_num, recording_state),
  CONSTRAINT fk_inventory_balance_monthly_balance FOREIGN KEY (inventory_balance_id) REFERENCES inventory_balance (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- END former V028__inventory_movement_monthly.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V029__purchase_receipt.sql
-- ---------------------------------------------------------------------------

-- TX1-R1: 구매입고

ALTER TABLE purchase_order_line
  ADD COLUMN received_qty DECIMAL(18, 4) NOT NULL DEFAULT 0 AFTER requested_delivery_date,
  ADD COLUMN waiting_inspection_qty DECIMAL(18, 4) NOT NULL DEFAULT 0 AFTER received_qty;

CREATE TABLE IF NOT EXISTS purchase_receipt (
  id                BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  receipt_no        VARCHAR(30)  NOT NULL,
  partner_id        BIGINT       NOT NULL,
  receipt_date      DATE         NOT NULL,
  purchase_order_id BIGINT       NULL,
  status            ENUM('REGISTERED','PARTIALLY_POSTED','POSTED','CANCELLED') NOT NULL DEFAULT 'REGISTERED',
  recording_state   TINYINT      NOT NULL DEFAULT 1,
  created_by        VARCHAR(100) NULL,
  created_by_id     VARCHAR(100) NULL,
  created_at        DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by        VARCHAR(100) NULL,
  updated_by_id     VARCHAR(100) NULL,
  updated_at        DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_purchase_receipt_no (receipt_no, recording_state),
  INDEX idx_purchase_receipt_partner (partner_id, receipt_date, recording_state),
  CONSTRAINT fk_purchase_receipt_partner FOREIGN KEY (partner_id) REFERENCES company (id),
  CONSTRAINT fk_purchase_receipt_order FOREIGN KEY (purchase_order_id) REFERENCES purchase_order (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS purchase_receipt_line (
  id                      BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  purchase_receipt_id     BIGINT         NOT NULL,
  line_no                 SMALLINT       NOT NULL,
  purchase_order_line_id  BIGINT         NOT NULL,
  item_id                 BIGINT         NOT NULL,
  receipt_qty             DECIMAL(18, 4) NOT NULL,
  posted_qty              DECIMAL(18, 4) NOT NULL DEFAULT 0,
  unit_price              DECIMAL(18, 2) NOT NULL,
  amount                  DECIMAL(18, 2) NOT NULL,
  recording_state         TINYINT        NOT NULL DEFAULT 1,
  created_by              VARCHAR(100)   NULL,
  created_by_id           VARCHAR(100)   NULL,
  created_at              DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by              VARCHAR(100)   NULL,
  updated_by_id           VARCHAR(100)   NULL,
  updated_at              DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_purchase_receipt_line (purchase_receipt_id, line_no, recording_state),
  INDEX idx_purchase_receipt_line_order (purchase_order_line_id, recording_state),
  CONSTRAINT fk_purchase_receipt_line_receipt FOREIGN KEY (purchase_receipt_id) REFERENCES purchase_receipt (id),
  CONSTRAINT fk_purchase_receipt_line_order_line FOREIGN KEY (purchase_order_line_id) REFERENCES purchase_order_line (id),
  CONSTRAINT fk_purchase_receipt_line_item FOREIGN KEY (item_id) REFERENCES item (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- END former V029__purchase_receipt.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V030__purchase_history.sql
-- ---------------------------------------------------------------------------

-- TX1-R1: 매입 통계 이력

CREATE TABLE IF NOT EXISTS purchase_history (
  id            BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  company_id    BIGINT         NOT NULL,
  item_id       BIGINT         NOT NULL,
  purchase_qty  DECIMAL(18, 4) NOT NULL,
  unit_price    DECIMAL(18, 2) NOT NULL,
  amount        DECIMAL(18, 2) NOT NULL,
  history_date  DATE           NOT NULL,
  source_type   ENUM('PURCHASE_RECEIPT','QUALITY_INSPECTION') NOT NULL,
  source_id     BIGINT         NOT NULL,
  fiscal_year   SMALLINT       NOT NULL,
  fiscal_month  TINYINT        NOT NULL,
  recording_state TINYINT      NOT NULL DEFAULT 1,
  created_by    VARCHAR(100)   NULL,
  created_by_id VARCHAR(100)   NULL,
  created_at    DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  INDEX idx_purchase_history_company_date (company_id, history_date, recording_state),
  INDEX idx_purchase_history_source (source_type, source_id, recording_state),
  CONSTRAINT fk_purchase_history_company FOREIGN KEY (company_id) REFERENCES company (id),
  CONSTRAINT fk_purchase_history_item FOREIGN KEY (item_id) REFERENCES item (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- END former V030__purchase_history.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V031__quality_inspection.sql
-- ---------------------------------------------------------------------------

-- TX1-R2: 품질검사원장

CREATE TABLE IF NOT EXISTS quality_inspection (
  id                            BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  inspection_no                 VARCHAR(30)    NULL,
  source_type                   ENUM('PURCHASE','OUTSOURCE') NOT NULL,
  source_receipt_line_id        BIGINT         NOT NULL,
  item_id                       BIGINT         NOT NULL,
  company_id                    BIGINT         NOT NULL,
  request_qty                   DECIMAL(18, 4) NOT NULL,
  passed_qty                    DECIMAL(18, 4) NOT NULL DEFAULT 0,
  failed_qty                    DECIMAL(18, 4) NOT NULL DEFAULT 0,
  status                        ENUM('PENDING','COMPLETED','CANCELLED') NOT NULL,
  inspection_decision_code_id   BIGINT         NULL,
  unsuitability_cause_code_id   BIGINT         NULL,
  unsuitability_status_code_id  BIGINT         NULL,
  completed_at                  DATETIME(3)    NULL,
  recording_state               TINYINT        NOT NULL DEFAULT 1,
  created_by                    VARCHAR(100)   NULL,
  created_by_id                 VARCHAR(100)   NULL,
  created_at                    DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by                    VARCHAR(100)   NULL,
  updated_by_id                 VARCHAR(100)   NULL,
  updated_at                    DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  INDEX idx_qi_status (status, source_type, recording_state),
  INDEX idx_qi_receipt_line (source_receipt_line_id, recording_state),
  CONSTRAINT fk_qi_item FOREIGN KEY (item_id) REFERENCES item (id),
  CONSTRAINT fk_qi_company FOREIGN KEY (company_id) REFERENCES company (id),
  CONSTRAINT fk_qi_receipt_line FOREIGN KEY (source_receipt_line_id) REFERENCES purchase_receipt_line (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- END former V031__quality_inspection.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V032__purchase_receipt_permissions.sql
-- ---------------------------------------------------------------------------

-- TX1-R: 구매입고·품질검사 권한

INSERT INTO permission (permission_code, description) VALUES
('purchase:receipt:read', '구매입고 조회'),
('purchase:receipt:write', '구매입고 등록·취소'),
('quality:inspection:read', '품질검사 조회'),
('quality:inspection:complete', '품질검사 완료')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN (
    'purchase:receipt:read', 'purchase:receipt:write',
    'quality:inspection:read', 'quality:inspection:complete'
  )
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PURCHASE_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('purchase:receipt:read', 'quality:inspection:read')
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;

-- END former V032__purchase_receipt_permissions.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V033__purchase_order_receipt_status.sql
-- ---------------------------------------------------------------------------

-- 발주 입고 진행·완료 상태

ALTER TABLE purchase_order
  MODIFY COLUMN status ENUM('DRAFT','CONFIRMED','IN_PROGRESS','RECEIVED','CANCELLED') NOT NULL DEFAULT 'DRAFT';

-- END former V033__purchase_order_receipt_status.sql

