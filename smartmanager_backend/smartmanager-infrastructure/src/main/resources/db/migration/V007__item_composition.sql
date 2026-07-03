-- Ref: docs/step0/d4-bom-line.md v0.1

CREATE TABLE IF NOT EXISTS item_composition (
  id                          BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  parent_item_id              BIGINT         NOT NULL,
  child_item_id               BIGINT         NOT NULL,
  need_quantity_denominator   DECIMAL(18,4)  NOT NULL,
  need_quantity_numerator     DECIMAL(18,4)  NOT NULL,
  process_management          TINYINT        NOT NULL DEFAULT 0,
  sub_division                VARCHAR(50)    NULL,
  supply_division             VARCHAR(50)    NULL,
  bom_unit                    VARCHAR(20)    NULL,
  begin_date                  DATE           NOT NULL,
  end_date                    DATE           NULL,
  recording_state             TINYINT        NOT NULL DEFAULT 1,
  created_by                  VARCHAR(100)   NULL,
  created_by_id               VARCHAR(100)   NULL,
  created_at                  DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by                  VARCHAR(100)   NULL,
  updated_by_id               VARCHAR(100)   NULL,
  updated_at                  DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_item_composition (parent_item_id, child_item_id, recording_state),
  CONSTRAINT fk_item_composition_parent FOREIGN KEY (parent_item_id) REFERENCES item(id),
  CONSTRAINT fk_item_composition_child FOREIGN KEY (child_item_id) REFERENCES item(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS bom_change_log (
  id                          BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  item_composition_id         BIGINT         NOT NULL,
  parent_item_id              BIGINT         NOT NULL,
  child_item_id               BIGINT         NOT NULL,
  need_quantity_denominator   DECIMAL(18,4)  NOT NULL,
  need_quantity_numerator     DECIMAL(18,4)  NOT NULL,
  change_reason               VARCHAR(50)    NOT NULL,
  changed_by                  VARCHAR(100)   NULL,
  changed_by_id               VARCHAR(100)   NULL,
  changed_at                  DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  CONSTRAINT fk_bom_change_log_composition FOREIGN KEY (item_composition_id) REFERENCES item_composition(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
