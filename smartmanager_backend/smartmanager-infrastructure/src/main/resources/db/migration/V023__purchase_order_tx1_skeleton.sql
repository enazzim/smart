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
