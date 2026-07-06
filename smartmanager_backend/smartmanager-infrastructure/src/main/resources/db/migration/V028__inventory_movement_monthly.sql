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
