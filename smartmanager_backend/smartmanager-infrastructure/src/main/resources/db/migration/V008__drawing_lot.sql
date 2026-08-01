-- =============================================================================
-- V008__drawing_lot.sql — 도면·Lot
-- Squash of former V072–V081 (chronological concat, 2026-08-01)
-- =============================================================================


-- ---------------------------------------------------------------------------
-- BEGIN former V072__drawing.sql
-- ---------------------------------------------------------------------------

-- 기준정보: 도면 마스터·이력

CREATE TABLE IF NOT EXISTS drawing_master (
  id              VARCHAR(36)  NOT NULL PRIMARY KEY,
  part_no         VARCHAR(50)  NOT NULL,
  part_name       VARCHAR(100) NOT NULL,
  model_group     VARCHAR(50)  NOT NULL,
  recording_state TINYINT      NOT NULL DEFAULT 1,
  created_by      VARCHAR(100) NULL,
  created_by_id   VARCHAR(100) NULL,
  created_at      DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100) NULL,
  updated_by_id   VARCHAR(100) NULL,
  updated_at      DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_drawing_part_no (part_no, recording_state),
  INDEX idx_drawing_model_group (model_group, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS drawing_history (
  id                VARCHAR(36)     NOT NULL PRIMARY KEY,
  drawing_master_id VARCHAR(36)     NOT NULL,
  drawing_type      ENUM('DEV','PROD') NOT NULL,
  major_version     INT             NOT NULL,
  minor_version     INT             NOT NULL,
  file_path         VARCHAR(500)    NOT NULL,
  file_size_bytes   BIGINT          NOT NULL,
  is_latest         CHAR(1)         NOT NULL DEFAULT 'Y',
  change_type       VARCHAR(20)     NULL,
  change_reason     TEXT            NULL,
  created_at        DATETIME(3)     NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  INDEX idx_dh_master (drawing_master_id, is_latest),
  INDEX idx_dh_type_latest (drawing_type, is_latest),
  CONSTRAINT fk_dh_master FOREIGN KEY (drawing_master_id) REFERENCES drawing_master (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- END former V072__drawing.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V073__drawing_permissions.sql
-- ---------------------------------------------------------------------------

-- 기준정보: 도면관리 권한

INSERT INTO permission (permission_code, description) VALUES
('basis:drawing:read', '도면 조회'),
('basis:drawing:write', '도면 등록·수정·삭제')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('basis:drawing:read', 'basis:drawing:write')
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('basis:drawing:read', 'basis:drawing:write')
WHERE r.role_code = 'BASIS_MANAGER' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'basis:drawing:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'basis:drawing:read'
WHERE r.role_code IN ('SALES_OPERATOR', 'PRODUCTION_OPERATOR', 'PURCHASE_OPERATOR')
  AND r.recording_state = 1;

-- END former V073__drawing_permissions.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V074__drawing_item_link.sql
-- ---------------------------------------------------------------------------

-- 도면 마스터 ↔ 품목 마스터 선택 연동

ALTER TABLE drawing_master
  ADD COLUMN item_id BIGINT NULL AFTER model_group;

ALTER TABLE drawing_master
  ADD INDEX idx_drawing_item_id (item_id);

ALTER TABLE drawing_master
  ADD CONSTRAINT fk_drawing_item FOREIGN KEY (item_id) REFERENCES item (id);

-- END former V074__drawing_item_link.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V075__drawing_hard_delete_permission.sql
-- ---------------------------------------------------------------------------

-- 도면 영구 삭제: SYSTEM_ADMIN 전용 (논리 삭제·복구는 basis:drawing:write)

INSERT INTO permission (permission_code, description) VALUES
('basis:drawing:hard-delete', '도면 영구 삭제 (휴지통)')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'basis:drawing:hard-delete'
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

-- END former V075__drawing_hard_delete_permission.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V076__drawing_reference.sql
-- ---------------------------------------------------------------------------

-- 도면 history 간 참조 (버전 pin)

CREATE TABLE IF NOT EXISTS drawing_reference (
  id                      VARCHAR(36)  NOT NULL PRIMARY KEY,
  parent_history_id       VARCHAR(36)  NOT NULL COMMENT '조립(상위) 도면 history',
  child_history_id        VARCHAR(36)  NOT NULL COMMENT '부품(하위) 도면 history — Rev pin',
  ref_role                VARCHAR(30)  NOT NULL DEFAULT 'COMPONENT'
                          COMMENT 'COMPONENT | RELATED | SPEC',
  sort_order              INT          NOT NULL DEFAULT 0,
  remark                  VARCHAR(500) NULL,
  recording_state         TINYINT      NOT NULL DEFAULT 1,
  created_by              VARCHAR(100) NULL,
  created_by_id           VARCHAR(100) NULL,
  created_at              DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by              VARCHAR(100) NULL,
  updated_by_id           VARCHAR(100) NULL,
  updated_at              DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_drawing_ref_parent_child
    (parent_history_id, child_history_id, recording_state),
  KEY idx_drawing_ref_child (child_history_id, recording_state),
  CONSTRAINT fk_drawing_ref_parent
    FOREIGN KEY (parent_history_id) REFERENCES drawing_history (id),
  CONSTRAINT fk_drawing_ref_child
    FOREIGN KEY (child_history_id) REFERENCES drawing_history (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- END former V076__drawing_reference.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V077__item_lot_tracked_and_model_type.sql
-- ---------------------------------------------------------------------------

-- LOT-1: 품목 Lot 추적 플래그 + 기종(model_type) 필수화 + 도면 기종 컬럼명 통일
-- Ref: docs/step0/lot-integration-design.md v1.1 · d4-item

ALTER TABLE item
  ADD COLUMN lot_tracked TINYINT(1) NOT NULL DEFAULT 0
    COMMENT '1=입출고 시 lot_id 필수'
  AFTER min_order_quantity;

UPDATE item
SET model_type = '-'
WHERE model_type IS NULL OR TRIM(model_type) = '';

ALTER TABLE item
  MODIFY COLUMN model_type VARCHAR(100) NOT NULL
    COMMENT '기종 (도면 model_type 과 동일 개념)';

ALTER TABLE drawing_master
  CHANGE COLUMN model_group model_type VARCHAR(50) NOT NULL
    COMMENT '기종 (item.model_type 과 동일 개념)';

ALTER TABLE drawing_master
  DROP INDEX idx_drawing_model_group,
  ADD INDEX idx_drawing_model_type (model_type, recording_state);

-- END former V077__item_lot_tracked_and_model_type.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V078__inventory_lot.sql
-- ---------------------------------------------------------------------------

-- LOT-1: Lot 마스터·슬롯 잔량·채번 + 권한
-- Ref: docs/step0/lot-integration-design.md v1.2 §9 V078

CREATE TABLE IF NOT EXISTS inventory_lot (
  id                  BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  item_id             BIGINT       NOT NULL,
  lot_no              VARCHAR(100) NOT NULL,
  status              ENUM('ACTIVE','BLOCKED','DEPLETED') NOT NULL DEFAULT 'ACTIVE',
  origin_type         ENUM('MANUAL','PURCHASE','PRODUCTION','SPLIT','MERGE','ADJUSTMENT')
                      NOT NULL DEFAULT 'MANUAL',
  origin_doc_type     VARCHAR(50)  NULL COMMENT 'purchase_receipt, work_report, ...',
  origin_doc_id       BIGINT       NULL,
  p1                  VARCHAR(100) NULL COMMENT '레거시 LotLedger.P1',
  p2                  VARCHAR(100) NULL COMMENT '레거시 LotLedger.P2',
  expiry_date         DATE         NULL,
  certificate_ref     VARCHAR(200) NULL,
  remark              VARCHAR(500) NULL,
  recording_state     TINYINT      NOT NULL DEFAULT 1,
  created_by          VARCHAR(100) NULL,
  created_by_id       VARCHAR(100) NULL,
  created_at          DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by          VARCHAR(100) NULL,
  updated_by_id       VARCHAR(100) NULL,
  updated_at          DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_inventory_lot_item_lot (item_id, lot_no, recording_state),
  KEY idx_inventory_lot_status (status, recording_state),
  KEY idx_inventory_lot_item (item_id, recording_state),
  CONSTRAINT fk_inventory_lot_item FOREIGN KEY (item_id) REFERENCES item (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS inventory_lot_balance (
  id                    BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  lot_id                BIGINT         NOT NULL,
  inventory_balance_id  BIGINT         NOT NULL,
  qty_on_hand           DECIMAL(18,4)  NOT NULL DEFAULT 0,
  recording_state       TINYINT        NOT NULL DEFAULT 1,
  created_by            VARCHAR(100)   NULL,
  created_by_id         VARCHAR(100)   NULL,
  created_at            DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by            VARCHAR(100)   NULL,
  updated_by_id         VARCHAR(100)   NULL,
  updated_at            DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_lot_balance (lot_id, inventory_balance_id, recording_state),
  KEY idx_lot_balance_inventory (inventory_balance_id, recording_state),
  CONSTRAINT fk_lot_balance_lot FOREIGN KEY (lot_id) REFERENCES inventory_lot (id),
  CONSTRAINT fk_lot_balance_inventory FOREIGN KEY (inventory_balance_id) REFERENCES inventory_balance (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS lot_number_sequence (
  id                    BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  item_id               BIGINT       NOT NULL,
  sequence_date         DATE         NOT NULL,
  last_seq              INT          NOT NULL DEFAULT 0,
  UNIQUE KEY uk_lot_seq (item_id, sequence_date),
  CONSTRAINT fk_lot_number_sequence_item FOREIGN KEY (item_id) REFERENCES item (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('inventory:lot:read', 'Lot 조회'),
('inventory:lot:write', 'Lot 등록·수정·삭제')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('inventory:lot:read', 'inventory:lot:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PURCHASE_OPERATOR', 'PRODUCTION_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'inventory:lot:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;

-- END former V078__inventory_lot.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V079__stock_movement_lot_and_genealogy.sql
-- ---------------------------------------------------------------------------

-- LOT-2: stock_movement.lot_id + lot_genealogy
-- Ref: docs/step0/lot-integration-design.md v1.3 §9 V079

ALTER TABLE stock_movement
  ADD COLUMN lot_id BIGINT NULL COMMENT 'lot_tracked 품목 필수' AFTER partner_id,
  ADD KEY idx_stock_movement_lot (lot_id, movement_date),
  ADD CONSTRAINT fk_stock_movement_lot FOREIGN KEY (lot_id) REFERENCES inventory_lot (id);

CREATE TABLE IF NOT EXISTS lot_genealogy (
  id                    BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  parent_lot_id         BIGINT         NOT NULL,
  child_lot_id          BIGINT         NOT NULL,
  link_type             ENUM('CONSUME','PRODUCE','SPLIT','MERGE') NOT NULL,
  qty                   DECIMAL(18,4)  NOT NULL,
  stock_movement_id     BIGINT         NULL COMMENT '근거 movement',
  source_doc_type       VARCHAR(50)    NULL,
  source_doc_id         BIGINT         NULL,
  recording_state       TINYINT        NOT NULL DEFAULT 1,
  created_by            VARCHAR(100)   NULL,
  created_by_id         VARCHAR(100)   NULL,
  created_at            DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  KEY idx_lot_genealogy_parent (parent_lot_id, recording_state),
  KEY idx_lot_genealogy_child (child_lot_id, recording_state),
  CONSTRAINT fk_lot_genealogy_parent FOREIGN KEY (parent_lot_id) REFERENCES inventory_lot (id),
  CONSTRAINT fk_lot_genealogy_child FOREIGN KEY (child_lot_id) REFERENCES inventory_lot (id),
  CONSTRAINT fk_lot_genealogy_movement FOREIGN KEY (stock_movement_id) REFERENCES stock_movement (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- END former V079__stock_movement_lot_and_genealogy.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V080__work_report_lot.sql
-- ---------------------------------------------------------------------------

-- LOT-3: 작업일보 투입·산출 Lot FK
ALTER TABLE work_report_consumption_line
  ADD COLUMN lot_id BIGINT NULL COMMENT 'lot_tracked 투입 Lot' AFTER source_process_id,
  ADD KEY idx_wr_consumption_lot (lot_id),
  ADD CONSTRAINT fk_wr_consumption_lot FOREIGN KEY (lot_id) REFERENCES inventory_lot (id);

ALTER TABLE work_report
  ADD COLUMN output_lot_id BIGINT NULL COMMENT 'lot_tracked 산출 Lot' AFTER worker_name,
  ADD KEY idx_work_report_output_lot (output_lot_id),
  ADD CONSTRAINT fk_work_report_output_lot FOREIGN KEY (output_lot_id) REFERENCES inventory_lot (id);

-- END former V080__work_report_lot.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V081__lot4_tx_lot_id.sql
-- ---------------------------------------------------------------------------

-- LOT-4: TX 라인·기타입출고 lot_id (영업출고·매출·외주출고 투입·외주입고·기타입출고)
ALTER TABLE sales_shipment_line
  ADD COLUMN lot_id BIGINT NULL COMMENT 'lot_tracked 출고 Lot (SALES|WIP → DELIVERY 동일 lot)' AFTER invoiced_qty,
  ADD KEY idx_sales_shipment_line_lot (lot_id),
  ADD CONSTRAINT fk_sales_shipment_line_lot FOREIGN KEY (lot_id) REFERENCES inventory_lot (id);

ALTER TABLE sales_revenue_line
  ADD COLUMN lot_id BIGINT NULL COMMENT 'lot_tracked 매출 Lot (DELIVERY OUT)' AFTER amount,
  ADD KEY idx_sales_revenue_line_lot (lot_id),
  ADD CONSTRAINT fk_sales_revenue_line_lot FOREIGN KEY (lot_id) REFERENCES inventory_lot (id);

ALTER TABLE outsourcing_shipment_input_line
  ADD COLUMN lot_id BIGINT NULL COMMENT 'lot_tracked 투입 Lot (원창고 → OUTSOURCE 동일 lot)' AFTER input_process_id,
  ADD KEY idx_os_shipment_input_lot (lot_id),
  ADD CONSTRAINT fk_os_shipment_input_lot FOREIGN KEY (lot_id) REFERENCES inventory_lot (id);

ALTER TABLE outsourcing_receipt_line
  ADD COLUMN lot_id BIGINT NULL COMMENT 'lot_tracked 외주입고 산출 Lot (WIP|SALES IN)' AFTER amount,
  ADD KEY idx_os_receipt_line_lot (lot_id),
  ADD CONSTRAINT fk_os_receipt_line_lot FOREIGN KEY (lot_id) REFERENCES inventory_lot (id);

ALTER TABLE misc_stock_movement
  ADD COLUMN lot_id BIGINT NULL COMMENT 'lot_tracked 기타입출고 Lot' AFTER output_process_id,
  ADD KEY idx_misc_stock_movement_lot (lot_id),
  ADD CONSTRAINT fk_misc_stock_movement_lot FOREIGN KEY (lot_id) REFERENCES inventory_lot (id);

-- END former V081__lot4_tx_lot_id.sql

