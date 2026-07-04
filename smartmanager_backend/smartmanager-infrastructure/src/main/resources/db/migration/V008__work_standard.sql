-- Ref: docs/step0/d4-work-standard.md v0.1

CREATE TABLE IF NOT EXISTS work_standard (
  id                    BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  item_id               BIGINT       NOT NULL,
  process_sequence_id   BIGINT       NOT NULL,
  work_center_id        BIGINT       NOT NULL,
  equipment_id          BIGINT       NULL,
  priority_order        INT          NOT NULL DEFAULT 1,
  main_worker_user_id   BIGINT       NULL,
  tool_name             VARCHAR(200) NULL,
  setup_time            INT          NOT NULL DEFAULT 0,
  standard_time         INT          NOT NULL DEFAULT 0,
  recording_state       TINYINT      NOT NULL DEFAULT 1,
  created_by            VARCHAR(100) NULL,
  created_by_id         VARCHAR(100) NULL,
  created_at            DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by            VARCHAR(100) NULL,
  updated_by_id         VARCHAR(100) NULL,
  updated_at            DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_work_standard (item_id, process_sequence_id, priority_order, recording_state),
  CONSTRAINT fk_work_standard_item FOREIGN KEY (item_id) REFERENCES item (id),
  CONSTRAINT fk_work_standard_process FOREIGN KEY (process_sequence_id) REFERENCES process_sequence (id),
  CONSTRAINT fk_work_standard_work_center FOREIGN KEY (work_center_id) REFERENCES work_center (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
