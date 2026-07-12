-- 기준정보: 도면 마스터·이력

CREATE TABLE IF NOT EXISTS drawing_master (
  id              VARCHAR(36)  NOT NULL PRIMARY KEY,
  part_no         VARCHAR(50)  NOT NULL,
  part_name       VARCHAR(100) NOT NULL,
  model_group     VARCHAR(50)  NOT NULL,
  recording_state TINYINT      NOT NULL DEFAULT 1,
  created_by      VARCHAR(100) NULL,
  created_by_id   VARCHAR(100) NULL,
  created_at      DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100) NULL,
  updated_by_id   VARCHAR(100) NULL,
  updated_at      DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_drawing_part_no (part_no, recording_state),
  INDEX idx_drawing_model_group (model_group, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS drawing_history (
  id                VARCHAR(36)     NOT NULL PRIMARY KEY,
  drawing_master_id VARCHAR(36)     NOT NULL,
  drawing_type      ENUM('DEV','PROD') NOT NULL,
  major_version     INT             NOT NULL,
  minor_version     INT             NOT NULL,
  file_path         VARCHAR(500)    NOT NULL,
  file_size_bytes   BIGINT          NOT NULL,
  is_latest         CHAR(1)         NOT NULL DEFAULT 'Y',
  change_type       VARCHAR(20)     NULL,
  change_reason     TEXT            NULL,
  created_at        DATETIME(3)     NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  INDEX idx_dh_master (drawing_master_id, is_latest),
  INDEX idx_dh_type_latest (drawing_type, is_latest),
  CONSTRAINT fk_dh_master FOREIGN KEY (drawing_master_id) REFERENCES drawing_master (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
