-- =============================================================================
-- V003__sales_production_mrp.sql — 수주·생산계획·MRP
-- Squash of former V015–V022 (chronological concat, 2026-08-01)
-- =============================================================================


-- ---------------------------------------------------------------------------
-- BEGIN former V015__sales_order.sql
-- ---------------------------------------------------------------------------

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

-- END former V015__sales_order.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V016__sales_order_line_status.sql
-- ---------------------------------------------------------------------------

-- 수주 라인 이행·납품 상태

ALTER TABLE sales_order_line
  ADD COLUMN fulfillment_status ENUM('WAITING','IN_PROGRESS','COMPLETED','FORCE_COMPLETED') NOT NULL DEFAULT 'WAITING' AFTER fulfillment_route,
  ADD COLUMN delivery_status ENUM('NOT_STARTED','IN_PROGRESS','COMPLETED') NOT NULL DEFAULT 'NOT_STARTED' AFTER fulfillment_status;

CREATE INDEX idx_sales_order_line_fulfillment ON sales_order_line (fulfillment_status, recording_state);
CREATE INDEX idx_sales_order_line_delivery ON sales_order_line (delivery_status, recording_state);

INSERT INTO permission (permission_code, description) VALUES
('sales:order:progress', '수주 이행상태 변경'),
('sales:order:force-complete', '수주 강제완료')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('sales:order:progress', 'sales:order:force-complete')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'SALES_OPERATOR') AND r.recording_state = 1;

-- END former V016__sales_order_line_status.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V017__production_plan.sql
-- ---------------------------------------------------------------------------

-- PRD-W0: 생산계획 (production_plan)

CREATE TABLE IF NOT EXISTS production_plan (
  id                      BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  plan_no                 VARCHAR(30)    NOT NULL,
  sales_order_id          BIGINT         NOT NULL,
  sales_order_line_id     BIGINT         NOT NULL,
  item_id                 BIGINT         NOT NULL,
  planned_qty             DECIMAL(18, 4) NOT NULL,
  produced_qty            DECIMAL(18, 4) NOT NULL DEFAULT 0,
  requested_delivery_date DATE           NULL,
  status                  ENUM('PLANNED','IN_PROGRESS','COMPLETED','CANCELLED') NOT NULL DEFAULT 'PLANNED',
  recording_state         TINYINT        NOT NULL DEFAULT 1,
  created_by              VARCHAR(100)   NULL,
  created_by_id           VARCHAR(100)   NULL,
  created_at              DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by              VARCHAR(100)   NULL,
  updated_by_id           VARCHAR(100)   NULL,
  updated_at              DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_production_plan_no (plan_no, recording_state),
  UNIQUE KEY uk_production_plan_line (sales_order_line_id, recording_state),
  INDEX idx_production_plan_order (sales_order_id, recording_state),
  INDEX idx_production_plan_status (status, recording_state),
  CONSTRAINT fk_production_plan_order FOREIGN KEY (sales_order_id) REFERENCES sales_order (id),
  CONSTRAINT fk_production_plan_line FOREIGN KEY (sales_order_line_id) REFERENCES sales_order_line (id),
  CONSTRAINT fk_production_plan_item FOREIGN KEY (item_id) REFERENCES item (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('production:plan:read', '생산계획 조회'),
('production:plan:write', '생산계획 수립')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('production:plan:read', 'production:plan:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PRODUCTION_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'production:plan:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;

-- END former V017__production_plan.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V018__sync_system_admin_permissions.sql
-- ---------------------------------------------------------------------------

-- SYSTEM_ADMIN에 신규 권한(생산계획 등) 재동기화 — V012 이후 추가된 permission 누락 방지

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

-- END former V018__sync_system_admin_permissions.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V019__production_plan_mrp_work_plan_status.sql
-- ---------------------------------------------------------------------------

-- 생산계획: 자재소요·작업계획 상태

ALTER TABLE production_plan
  ADD COLUMN mrp_status ENUM('NOT_CALCULATED','CALCULATED') NOT NULL DEFAULT 'NOT_CALCULATED' AFTER status,
  ADD COLUMN work_plan_status ENUM('NOT_PLANNED','PLANNED') NOT NULL DEFAULT 'NOT_PLANNED' AFTER mrp_status;

CREATE INDEX idx_production_plan_mrp ON production_plan (mrp_status, recording_state);
CREATE INDEX idx_production_plan_work_plan ON production_plan (work_plan_status, recording_state);

-- END former V019__production_plan_mrp_work_plan_status.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V020__production_plan_delete_cancelled.sql
-- ---------------------------------------------------------------------------

-- 취소 상태로 남아 있던 생산계획 정리 (취소 = 레코드 삭제 정책으로 전환)

DELETE FROM production_plan
WHERE status = 'CANCELLED';

-- END former V020__production_plan_delete_cancelled.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V021__mrp_material_requirement.sql
-- ---------------------------------------------------------------------------

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

-- END former V021__mrp_material_requirement.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V022__sync_mrp_admin_permissions.sql
-- ---------------------------------------------------------------------------

-- SYSTEM_ADMIN에 MRP 권한 재동기화

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

-- END former V022__sync_mrp_admin_permissions.sql

