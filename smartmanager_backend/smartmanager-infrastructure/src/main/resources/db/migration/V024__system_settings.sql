-- S0: 시스템 기능 설정 (SCS_T)

CREATE TABLE IF NOT EXISTS system_settings (
  id              BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  setting_key     VARCHAR(100) NOT NULL,
  value_json      JSON         NOT NULL,
  recording_state TINYINT      NOT NULL DEFAULT 1,
  created_by      VARCHAR(100) NULL,
  created_by_id   VARCHAR(100) NULL,
  created_at      DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100) NULL,
  updated_by_id   VARCHAR(100) NULL,
  updated_at      DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_system_settings_key (setting_key, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO system_settings (setting_key, value_json, created_by, created_by_id)
VALUES ('mrp.grouping_mode', '"BY_PLAN"', 'system', 'system');
