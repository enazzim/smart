-- 작업일보 투입(하위품목) 이력 — 취소 시 역전기용

CREATE TABLE IF NOT EXISTS work_report_consumption_line (
  id                  BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  work_report_id      BIGINT         NOT NULL,
  line_no             SMALLINT       NOT NULL,
  item_id             BIGINT         NOT NULL,
  item_composition_id BIGINT         NULL,
  issue_qty           DECIMAL(18, 4) NOT NULL,
  location_code       VARCHAR(20)    NOT NULL,
  source_process_id   BIGINT         NULL,
  recording_state     TINYINT        NOT NULL DEFAULT 1,
  created_by          VARCHAR(100)   NULL,
  created_by_id       VARCHAR(100)   NULL,
  created_at          DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by          VARCHAR(100)   NULL,
  updated_by_id       VARCHAR(100)   NULL,
  updated_at          DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  INDEX idx_wr_consumption_report (work_report_id, recording_state),
  CONSTRAINT fk_wr_consumption_report FOREIGN KEY (work_report_id) REFERENCES work_report (id),
  CONSTRAINT fk_wr_consumption_item FOREIGN KEY (item_id) REFERENCES item (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
