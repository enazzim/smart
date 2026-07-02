-- Ref: docs/step0/d4-item.md v0.2

CREATE TABLE IF NOT EXISTS item (
  id                      BIGINT        NOT NULL AUTO_INCREMENT PRIMARY KEY,
  item_no                 VARCHAR(50)   NOT NULL,
  item_name               VARCHAR(200)  NOT NULL,
  property_classification ENUM('원자재','제품','상품','공정품') NOT NULL,
  unit                    VARCHAR(20)   NOT NULL,
  standard                VARCHAR(200)  NULL,
  standard_unit_cost      DECIMAL(18,2) NULL,
  check_distinction       ENUM('NONE','INSPECTION') NULL,
  lead_time               INT           NULL,
  safety_stock_quantity   DECIMAL(18,4) NULL,
  order_interval_quantity DECIMAL(18,4) NULL,
  min_order_quantity      DECIMAL(18,4) NULL,
  recording_state         TINYINT       NOT NULL DEFAULT 1,
  created_by              VARCHAR(100)  NULL,
  created_by_id           VARCHAR(100)  NULL,
  created_at              DATETIME(3)   NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by              VARCHAR(100)  NULL,
  updated_by_id           VARCHAR(100)  NULL,
  updated_at              DATETIME(3)   NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_item_no (item_no, recording_state),
  INDEX idx_item_name (item_name, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
