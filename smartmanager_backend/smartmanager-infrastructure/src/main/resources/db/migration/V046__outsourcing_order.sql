-- INF-4a: 외주발주

CREATE TABLE IF NOT EXISTS outsourcing_order (
  id              BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  order_no        VARCHAR(30)  NOT NULL,
  partner_id      BIGINT       NOT NULL,
  order_date      DATE         NOT NULL,
  source_type     ENUM('WORK_PLAN','MANUAL') NOT NULL DEFAULT 'MANUAL',
  status          ENUM('CONFIRMED','IN_PROGRESS','RECEIVED','CANCELLED') NOT NULL DEFAULT 'CONFIRMED',
  recording_state TINYINT      NOT NULL DEFAULT 1,
  created_by      VARCHAR(100) NULL,
  created_by_id   VARCHAR(100) NULL,
  created_at      DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100) NULL,
  updated_by_id   VARCHAR(100) NULL,
  updated_at      DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_outsourcing_order_no (order_no, recording_state),
  INDEX idx_outsourcing_order_partner (partner_id, recording_state),
  INDEX idx_outsourcing_order_date (order_date, recording_state),
  CONSTRAINT fk_outsourcing_order_partner FOREIGN KEY (partner_id) REFERENCES company (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS outsourcing_order_line (
  id                      BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  outsourcing_order_id    BIGINT         NOT NULL,
  line_no                 SMALLINT       NOT NULL,
  item_id                 BIGINT         NOT NULL,
  process_sequence_id     BIGINT         NOT NULL,
  begin_process_code_id   BIGINT         NOT NULL,
  end_process_code_id     BIGINT         NOT NULL,
  work_plan_id            BIGINT         NULL,
  order_qty               DECIMAL(18, 4) NOT NULL,
  shipped_qty             DECIMAL(18, 4) NOT NULL DEFAULT 0,
  received_qty            DECIMAL(18, 4) NOT NULL DEFAULT 0,
  unit_price              DECIMAL(18, 2) NOT NULL DEFAULT 0,
  amount                  DECIMAL(18, 2) NOT NULL DEFAULT 0,
  requested_delivery_date DATE           NULL,
  recording_state         TINYINT        NOT NULL DEFAULT 1,
  created_by              VARCHAR(100)   NULL,
  created_by_id           VARCHAR(100)   NULL,
  created_at              DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by              VARCHAR(100)   NULL,
  updated_by_id           VARCHAR(100)   NULL,
  updated_at              DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_outsourcing_order_line (outsourcing_order_id, line_no, recording_state),
  INDEX idx_outsourcing_order_line_item (item_id, recording_state),
  INDEX idx_outsourcing_order_line_work_plan (work_plan_id, recording_state),
  CONSTRAINT fk_outsourcing_order_line_order FOREIGN KEY (outsourcing_order_id) REFERENCES outsourcing_order (id),
  CONSTRAINT fk_outsourcing_order_line_item FOREIGN KEY (item_id) REFERENCES item (id),
  CONSTRAINT fk_outsourcing_order_line_process FOREIGN KEY (process_sequence_id) REFERENCES process_sequence (id),
  CONSTRAINT fk_outsourcing_order_line_begin_process FOREIGN KEY (begin_process_code_id) REFERENCES public_code (id),
  CONSTRAINT fk_outsourcing_order_line_end_process FOREIGN KEY (end_process_code_id) REFERENCES public_code (id),
  CONSTRAINT fk_outsourcing_order_line_work_plan FOREIGN KEY (work_plan_id) REFERENCES work_plan (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('outsource:order:read', '외주발주 조회'),
('outsource:order:write', '외주발주 등록·취소')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('outsource:order:read', 'outsource:order:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PRODUCTION_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'outsource:order:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
