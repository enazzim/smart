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
