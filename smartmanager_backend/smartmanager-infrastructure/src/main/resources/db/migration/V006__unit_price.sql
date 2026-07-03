-- Ref: docs/step0/d4-unit-price.md, domain-event-projector-matrix.md §4.5

ALTER TABLE inventory_balance
  ADD COLUMN input_process_id BIGINT NULL AFTER output_process_id,
  ADD CONSTRAINT fk_inventory_balance_input_process
    FOREIGN KEY (input_process_id) REFERENCES process_sequence(id);

CREATE TABLE IF NOT EXISTS unit_price (
  id                        BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  cost_type                 ENUM('SALE','PURCHASE','OUTSOURCE') NOT NULL,
  item_id                   BIGINT         NOT NULL,
  company_id                BIGINT         NOT NULL,
  begin_process_code_id     BIGINT         NULL,
  end_process_code_id       BIGINT         NULL,
  order_rate                DECIMAL(5,2)   NOT NULL DEFAULT 0,
  standard_unit_cost        DECIMAL(18,4)  NOT NULL,
  discount_unit_cost        DECIMAL(18,4)  NULL,
  begin_date                DATE           NOT NULL,
  end_date                  DATE           NULL,
  recording_state           TINYINT        NOT NULL DEFAULT 1,
  created_by                VARCHAR(100)   NULL,
  created_by_id             VARCHAR(100)   NULL,
  created_at                DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by                VARCHAR(100)   NULL,
  updated_by_id             VARCHAR(100)   NULL,
  updated_at                DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_unit_price_active (
    cost_type, item_id, company_id, begin_date,
    begin_process_code_id, end_process_code_id, recording_state
  ),
  CONSTRAINT fk_unit_price_item FOREIGN KEY (item_id) REFERENCES item(id),
  CONSTRAINT fk_unit_price_company FOREIGN KEY (company_id) REFERENCES company(id),
  CONSTRAINT fk_unit_price_begin_process FOREIGN KEY (begin_process_code_id) REFERENCES public_code(id),
  CONSTRAINT fk_unit_price_end_process FOREIGN KEY (end_process_code_id) REFERENCES public_code(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS unit_price_change_log (
  id                        BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  unit_price_id             BIGINT         NOT NULL,
  cost_type                 ENUM('SALE','PURCHASE','OUTSOURCE') NOT NULL,
  item_id                   BIGINT         NOT NULL,
  company_id                BIGINT         NOT NULL,
  begin_process_code_id     BIGINT         NULL,
  end_process_code_id       BIGINT         NULL,
  order_rate                DECIMAL(5,2)   NOT NULL,
  standard_unit_cost        DECIMAL(18,4)  NOT NULL,
  discount_unit_cost        DECIMAL(18,4)  NULL,
  begin_date                DATE           NOT NULL,
  end_date                  DATE           NULL,
  update_reason             VARCHAR(500)   NOT NULL,
  changed_by                VARCHAR(100)   NULL,
  changed_by_id             VARCHAR(100)   NULL,
  changed_at                DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  CONSTRAINT fk_unit_price_change_log_unit_price FOREIGN KEY (unit_price_id) REFERENCES unit_price(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
