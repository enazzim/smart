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
