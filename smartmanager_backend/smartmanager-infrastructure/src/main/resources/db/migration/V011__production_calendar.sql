-- Ref: docs/step0/d4-calendar.md v0.1

CREATE TABLE IF NOT EXISTS production_calendar (
  id                BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  calendar_date     DATE         NOT NULL,
  work_time         INT          NOT NULL DEFAULT 480,
  content           VARCHAR(255) NULL,
  recording_state   TINYINT      NOT NULL DEFAULT 1,
  created_by        VARCHAR(100) NULL,
  created_by_id     VARCHAR(100) NULL,
  created_at        DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by        VARCHAR(100) NULL,
  updated_by_id     VARCHAR(100) NULL,
  updated_at        DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_production_calendar_date (calendar_date, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS work_center_calendar (
  id                BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  work_center_id    BIGINT       NOT NULL,
  calendar_date     DATE         NOT NULL,
  work_time         INT          NOT NULL,
  content           VARCHAR(255) NULL,
  recording_state   TINYINT      NOT NULL DEFAULT 1,
  created_by        VARCHAR(100) NULL,
  created_by_id     VARCHAR(100) NULL,
  created_at        DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by        VARCHAR(100) NULL,
  updated_by_id     VARCHAR(100) NULL,
  updated_at        DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_work_center_calendar (work_center_id, calendar_date, recording_state),
  CONSTRAINT fk_work_center_calendar_wc FOREIGN KEY (work_center_id) REFERENCES work_center (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
