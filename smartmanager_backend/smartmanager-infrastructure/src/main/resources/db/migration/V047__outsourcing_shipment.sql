-- INF-4b: 외주출고

CREATE TABLE IF NOT EXISTS outsourcing_shipment (
  id              BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  shipment_no     VARCHAR(30)  NOT NULL,
  shipment_date   DATE         NOT NULL,
  status          ENUM('ISSUED','CANCELLED') NOT NULL DEFAULT 'ISSUED',
  recording_state TINYINT      NOT NULL DEFAULT 1,
  created_by      VARCHAR(100) NULL,
  created_by_id   VARCHAR(100) NULL,
  created_at      DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100) NULL,
  updated_by_id   VARCHAR(100) NULL,
  updated_at      DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_outsourcing_shipment_no (shipment_no, recording_state),
  INDEX idx_outsourcing_shipment_date (shipment_date, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS outsourcing_shipment_line (
  id                        BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  outsourcing_shipment_id   BIGINT         NOT NULL,
  line_no                   SMALLINT       NOT NULL,
  outsourcing_order_line_id BIGINT         NOT NULL,
  shipment_qty              DECIMAL(18, 4) NOT NULL,
  recording_state           TINYINT        NOT NULL DEFAULT 1,
  created_by                VARCHAR(100)   NULL,
  created_by_id             VARCHAR(100)   NULL,
  created_at                DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by                VARCHAR(100)   NULL,
  updated_by_id             VARCHAR(100)   NULL,
  updated_at                DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_outsourcing_shipment_line (outsourcing_shipment_id, line_no, recording_state),
  INDEX idx_outsourcing_shipment_line_order (outsourcing_order_line_id, recording_state),
  CONSTRAINT fk_outsourcing_shipment_line_header FOREIGN KEY (outsourcing_shipment_id) REFERENCES outsourcing_shipment (id),
  CONSTRAINT fk_outsourcing_shipment_line_order FOREIGN KEY (outsourcing_order_line_id) REFERENCES outsourcing_order_line (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS outsourcing_shipment_input_line (
  id                          BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  outsourcing_shipment_line_id BIGINT        NOT NULL,
  line_no                     SMALLINT       NOT NULL,
  item_id                     BIGINT         NOT NULL,
  item_composition_id         BIGINT         NULL,
  issue_qty                   DECIMAL(18, 4) NOT NULL,
  source_location_code        VARCHAR(20)    NOT NULL,
  source_process_id           BIGINT         NULL,
  input_process_id            BIGINT         NOT NULL,
  recording_state             TINYINT        NOT NULL DEFAULT 1,
  CONSTRAINT fk_osil_shipment_line FOREIGN KEY (outsourcing_shipment_line_id) REFERENCES outsourcing_shipment_line (id),
  CONSTRAINT fk_osil_item FOREIGN KEY (item_id) REFERENCES item (id),
  CONSTRAINT fk_osil_bom FOREIGN KEY (item_composition_id) REFERENCES item_composition (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('outsource:shipment:read', '외주출고 조회'),
('outsource:shipment:write', '외주출고 등록·취소')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('outsource:shipment:read', 'outsource:shipment:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PRODUCTION_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'outsource:shipment:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
