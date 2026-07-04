-- Ref: docs/step0/d4-user.md v0.1

CREATE TABLE IF NOT EXISTS role (
  id                BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  role_code         VARCHAR(64)  NOT NULL,
  role_name         VARCHAR(100) NOT NULL,
  recording_state   TINYINT      NOT NULL DEFAULT 1,
  created_at        DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_role_code (role_code, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS permission (
  id                BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  permission_code   VARCHAR(128) NOT NULL,
  description       VARCHAR(255) NULL,
  recording_state   TINYINT      NOT NULL DEFAULT 1,
  UNIQUE KEY uk_permission_code (permission_code, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS role_permission (
  role_id           BIGINT NOT NULL,
  permission_id     BIGINT NOT NULL,
  PRIMARY KEY (role_id, permission_id),
  CONSTRAINT fk_role_permission_role FOREIGN KEY (role_id) REFERENCES role (id),
  CONSTRAINT fk_role_permission_permission FOREIGN KEY (permission_id) REFERENCES permission (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `user` (
  id                    BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  login_id              VARCHAR(100) NOT NULL,
  password_hash         VARCHAR(255) NOT NULL,
  name                  VARCHAR(100) NOT NULL,
  contact               VARCHAR(50)  NULL,
  email                 VARCHAR(200) NULL,
  work_diary_group_id   BIGINT       NULL,
  recording_state       TINYINT      NOT NULL DEFAULT 1,
  created_by            VARCHAR(100) NULL,
  created_by_id         VARCHAR(100) NULL,
  created_at            DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by            VARCHAR(100) NULL,
  updated_by_id         VARCHAR(100) NULL,
  updated_at            DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_user_login_id (login_id, recording_state),
  CONSTRAINT fk_user_work_diary_group FOREIGN KEY (work_diary_group_id) REFERENCES public_code (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS user_role (
  user_id BIGINT NOT NULL,
  role_id BIGINT NOT NULL,
  PRIMARY KEY (user_id, role_id),
  CONSTRAINT fk_user_role_user FOREIGN KEY (user_id) REFERENCES `user` (id),
  CONSTRAINT fk_user_role_role FOREIGN KEY (role_id) REFERENCES role (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

ALTER TABLE work_standard
  ADD CONSTRAINT fk_work_standard_main_worker FOREIGN KEY (main_worker_user_id) REFERENCES `user` (id);

INSERT INTO role (role_code, role_name) VALUES
('SYSTEM_ADMIN', '시스템 관리자'),
('BASIS_MANAGER', '기준정보 관리자'),
('PURCHASE_OPERATOR', '구매 담당'),
('PRODUCTION_OPERATOR', '생산 담당'),
('SALES_OPERATOR', '영업 담당'),
('VIEWER', '조회 전용')
ON DUPLICATE KEY UPDATE role_name = VALUES(role_name);
