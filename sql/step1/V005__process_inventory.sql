-- Ref: docs/step0/d4-process.md, inventory-ledger-spec.md, d4-work-center.md

CREATE TABLE IF NOT EXISTS inventory_location (
  id            BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  location_code VARCHAR(20)  NOT NULL,
  location_name VARCHAR(100) NOT NULL,
  UNIQUE KEY uk_inventory_location_code (location_code)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO inventory_location (location_code, location_name) VALUES
('RAW', '원자재창고'),
('SALES', '영업창고'),
('DELIVERY', '납품창고'),
('WIP', '생산창고'),
('OUTSOURCE', '외주창고')
ON DUPLICATE KEY UPDATE location_name = VALUES(location_name);

CREATE TABLE IF NOT EXISTS work_center (
  id                     BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  wc_name                VARCHAR(100) NOT NULL,
  main_process_code_id   BIGINT       NOT NULL,
  operation_time         INT          NOT NULL DEFAULT 480,
  retention_staff        INT          NOT NULL DEFAULT 1,
  capacity_distinction   VARCHAR(20)  NOT NULL DEFAULT 'TIME',
  recording_state        TINYINT      NOT NULL DEFAULT 1,
  created_by             VARCHAR(100) NULL,
  created_by_id          VARCHAR(100) NULL,
  created_at             DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by             VARCHAR(100) NULL,
  updated_by_id          VARCHAR(100) NULL,
  updated_at             DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_work_center_name (wc_name, recording_state),
  CONSTRAINT fk_work_center_process_code FOREIGN KEY (main_process_code_id) REFERENCES public_code(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO work_center (wc_name, main_process_code_id, operation_time, created_by, created_by_id, created_at, updated_by, updated_by_id, updated_at)
SELECT '절단라인', pc.id, 480, 'seed', 'seed', CURRENT_TIMESTAMP(3), 'seed', 'seed', CURRENT_TIMESTAMP(3)
FROM public_code pc
WHERE pc.small_code = '14000010' AND pc.recording_state = 1
LIMIT 1;

CREATE TABLE IF NOT EXISTS process_sequence (
  id                   BIGINT      NOT NULL AUTO_INCREMENT PRIMARY KEY,
  item_id              BIGINT      NOT NULL,
  process_sequence     SMALLINT    NOT NULL,
  public_code_id       BIGINT      NOT NULL,
  work_distinction     ENUM('INHOUSE','OUTSOURCE','SPLIT') NOT NULL,
  work_center_id       BIGINT      NULL,
  outside_order_rate   TINYINT     NOT NULL DEFAULT 0,
  progress_rate        SMALLINT    NOT NULL DEFAULT 100,
  lead_time            INT         NULL,
  etc_text             VARCHAR(500) NULL,
  variant              ENUM('plan','actual') NOT NULL DEFAULT 'plan',
  recording_state      TINYINT     NOT NULL DEFAULT 1,
  created_by           VARCHAR(100) NULL,
  created_by_id        VARCHAR(100) NULL,
  created_at           DATETIME(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by           VARCHAR(100) NULL,
  updated_by_id        VARCHAR(100) NULL,
  updated_at           DATETIME(3) NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_process_sequence (item_id, public_code_id, process_sequence, recording_state),
  CONSTRAINT fk_process_sequence_item FOREIGN KEY (item_id) REFERENCES item(id),
  CONSTRAINT fk_process_sequence_public_code FOREIGN KEY (public_code_id) REFERENCES public_code(id),
  CONSTRAINT fk_process_sequence_work_center FOREIGN KEY (work_center_id) REFERENCES work_center(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS inventory_balance (
  id                  BIGINT  NOT NULL AUTO_INCREMENT PRIMARY KEY,
  item_id             BIGINT  NOT NULL,
  location_id         BIGINT  NOT NULL,
  fiscal_year         SMALLINT NOT NULL,
  output_process_id   BIGINT  NULL,
  partner_id          BIGINT  NULL,
  recording_state     TINYINT NOT NULL DEFAULT 1,
  created_by          VARCHAR(100) NULL,
  created_by_id       VARCHAR(100) NULL,
  created_at          DATETIME(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by          VARCHAR(100) NULL,
  updated_by_id       VARCHAR(100) NULL,
  updated_at          DATETIME(3) NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_inventory_balance (item_id, location_id, fiscal_year, output_process_id, partner_id, recording_state),
  CONSTRAINT fk_inventory_balance_item FOREIGN KEY (item_id) REFERENCES item(id),
  CONSTRAINT fk_inventory_balance_location FOREIGN KEY (location_id) REFERENCES inventory_location(id),
  CONSTRAINT fk_inventory_balance_process FOREIGN KEY (output_process_id) REFERENCES process_sequence(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
