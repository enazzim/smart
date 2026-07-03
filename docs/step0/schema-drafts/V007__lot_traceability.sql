-- =============================================================================
-- DRAFT — NOT APPLIED BY FLYWAY
-- Ref: docs/step0/d5-lot-traceability.md v0.1
-- Copy to smartmanager-infrastructure/.../db/migration/ after Part 06 + review
-- =============================================================================

-- -----------------------------------------------------------------------------
-- 0. item — Lot 추적 플래그 (선택)
-- -----------------------------------------------------------------------------
ALTER TABLE item
  ADD COLUMN lot_tracked TINYINT(1) NOT NULL DEFAULT 0
    COMMENT '1=입출고 시 lot_id 필수'
  AFTER min_order_quantity;

-- -----------------------------------------------------------------------------
-- 1. inventory_lot — Lot 마스터 (LotLedger 대체)
-- -----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS inventory_lot (
  id                  BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  item_id             BIGINT       NOT NULL,
  lot_no              VARCHAR(100) NOT NULL,
  status              ENUM('ACTIVE','BLOCKED','DEPLETED') NOT NULL DEFAULT 'ACTIVE',
  origin_type         ENUM('MANUAL','PURCHASE','PRODUCTION','SPLIT','MERGE','ADJUSTMENT')
                      NOT NULL DEFAULT 'MANUAL',
  origin_doc_type     VARCHAR(50)  NULL COMMENT 'purchase_receipt, work_report, ...',
  origin_doc_id       BIGINT       NULL,
  p1                  VARCHAR(100) NULL COMMENT '레거시 LotLedger.P1 — 번호규칙 파라미터',
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
  KEY idx_inventory_lot_status (status),
  CONSTRAINT fk_inventory_lot_item FOREIGN KEY (item_id) REFERENCES item(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- -----------------------------------------------------------------------------
-- 2. stock_movement — 건별 입출고 (SH_HT / BOS_HT 대체, Lot 포함)
--    선행 INF Wave에서 lot_id 없이 먼저 도입 가능 — 본 초안은 Lot 컬럼 포함
-- -----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS stock_movement (
  id                    BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  inventory_balance_id  BIGINT         NOT NULL,
  lot_id                BIGINT         NULL COMMENT 'lot_tracked 품목은 NOT NULL',
  fiscal_year           SMALLINT       NOT NULL,
  movement_date         DATE           NOT NULL,
  direction             ENUM('IN','OUT') NOT NULL,
  qty                   DECIMAL(18,4)  NOT NULL,
  unit_cost             DECIMAL(18,4)  NULL,
  amount                DECIMAL(18,4)  NULL,
  source_doc_type       VARCHAR(50)    NOT NULL COMMENT 'purchase_receipt, work_report, ...',
  source_doc_id         BIGINT         NOT NULL,
  source_line_no        INT            NULL,
  correlation_id        VARCHAR(36)    NULL COMMENT 'UUID — 쌍 이동(창고간) 묶음',
  remark                VARCHAR(500)   NULL,
  recording_state       TINYINT        NOT NULL DEFAULT 1,
  created_by            VARCHAR(100)   NULL,
  created_by_id         VARCHAR(100)   NULL,
  created_at            DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  KEY idx_stock_movement_balance (inventory_balance_id, fiscal_year, movement_date),
  KEY idx_stock_movement_lot (lot_id, movement_date),
  KEY idx_stock_movement_source (source_doc_type, source_doc_id),
  CONSTRAINT fk_stock_movement_balance
    FOREIGN KEY (inventory_balance_id) REFERENCES inventory_balance(id),
  CONSTRAINT fk_stock_movement_lot
    FOREIGN KEY (lot_id) REFERENCES inventory_lot(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- -----------------------------------------------------------------------------
-- 3. inventory_lot_balance — Lot × 슬롯 현재고
-- -----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS inventory_lot_balance (
  id                    BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  lot_id                BIGINT         NOT NULL,
  inventory_balance_id  BIGINT         NOT NULL,
  qty_on_hand           DECIMAL(18,4)  NOT NULL DEFAULT 0,
  recording_state       TINYINT        NOT NULL DEFAULT 1,
  created_at            DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_at            DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_lot_balance (lot_id, inventory_balance_id, recording_state),
  CONSTRAINT fk_lot_balance_lot FOREIGN KEY (lot_id) REFERENCES inventory_lot(id),
  CONSTRAINT fk_lot_balance_inventory FOREIGN KEY (inventory_balance_id) REFERENCES inventory_balance(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- -----------------------------------------------------------------------------
-- 4. lot_genealogy — 부모↔자식 Lot 계보
-- -----------------------------------------------------------------------------
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
  created_at            DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  KEY idx_lot_genealogy_parent (parent_lot_id),
  KEY idx_lot_genealogy_child (child_lot_id),
  CONSTRAINT fk_lot_genealogy_parent FOREIGN KEY (parent_lot_id) REFERENCES inventory_lot(id),
  CONSTRAINT fk_lot_genealogy_child FOREIGN KEY (child_lot_id) REFERENCES inventory_lot(id),
  CONSTRAINT fk_lot_genealogy_movement FOREIGN KEY (stock_movement_id) REFERENCES stock_movement(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- -----------------------------------------------------------------------------
-- 5. lot_number_sequence — 자동 Lot 번호 (선택, Wave 2)
-- -----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS lot_number_sequence (
  id                    BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  item_id               BIGINT       NOT NULL,
  sequence_date         DATE         NOT NULL,
  last_seq              INT          NOT NULL DEFAULT 0,
  UNIQUE KEY uk_lot_seq (item_id, sequence_date),
  CONSTRAINT fk_lot_number_sequence_item FOREIGN KEY (item_id) REFERENCES item(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
