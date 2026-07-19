-- TX1-PP: 품목 단위 선지급 · 승인 FIFO 상계
-- docs/step0/partner-prepaid-design.md

ALTER TABLE partner_payment
  ADD COLUMN payment_kind ENUM('NORMAL', 'PREPAID') NOT NULL DEFAULT 'NORMAL'
    AFTER cost_category;

CREATE TABLE IF NOT EXISTS partner_payment_line (
  id                          BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  payment_id                  BIGINT         NOT NULL,
  item_id                     BIGINT         NOT NULL,
  purchase_order_line_id      BIGINT         NULL,
  outsourcing_order_line_id   BIGINT         NULL,
  supply_amount               DECIMAL(18, 2) NOT NULL,
  vat_amount                  DECIMAL(18, 2) NOT NULL DEFAULT 0,
  total_amount                DECIMAL(18, 2) NOT NULL,
  recording_state             TINYINT        NOT NULL DEFAULT 1,
  created_by                  VARCHAR(100)   NULL,
  created_by_id               VARCHAR(100)   NULL,
  created_at                  DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by                  VARCHAR(100)   NULL,
  updated_by_id               VARCHAR(100)   NULL,
  updated_at                  DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  INDEX idx_ppl_payment (payment_id, recording_state),
  INDEX idx_ppl_bucket (item_id, recording_state),
  INDEX idx_ppl_po_line (purchase_order_line_id, recording_state),
  INDEX idx_ppl_os_line (outsourcing_order_line_id, recording_state),
  CONSTRAINT fk_ppl_payment FOREIGN KEY (payment_id) REFERENCES partner_payment (id),
  CONSTRAINT fk_ppl_item FOREIGN KEY (item_id) REFERENCES item (id),
  CONSTRAINT fk_ppl_po_line FOREIGN KEY (purchase_order_line_id) REFERENCES purchase_order_line (id),
  CONSTRAINT fk_ppl_os_line FOREIGN KEY (outsourcing_order_line_id) REFERENCES outsourcing_order_line (id),
  CONSTRAINT chk_ppl_order_line_xor CHECK (
    purchase_order_line_id IS NULL OR outsourcing_order_line_id IS NULL
  )
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS partner_prepaid_offset (
  id                BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  payment_line_id   BIGINT         NOT NULL,
  ledger_kind       ENUM('PURCHASE_HISTORY', 'OUTSOURCE_HISTORY') NOT NULL,
  history_id        BIGINT         NOT NULL,
  amount            DECIMAL(18, 2) NOT NULL,
  recording_state   TINYINT        NOT NULL DEFAULT 1,
  created_by        VARCHAR(100)   NULL,
  created_by_id     VARCHAR(100)   NULL,
  created_at        DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by        VARCHAR(100)   NULL,
  updated_by_id     VARCHAR(100)   NULL,
  updated_at        DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  INDEX idx_ppo_line (payment_line_id, recording_state),
  INDEX idx_ppo_history (ledger_kind, history_id, recording_state),
  CONSTRAINT fk_ppo_payment_line FOREIGN KEY (payment_line_id) REFERENCES partner_payment_line (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
