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
