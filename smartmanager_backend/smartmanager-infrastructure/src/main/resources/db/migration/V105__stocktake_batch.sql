-- 재고실사 1차. 외주 전표 컬럼은 넣지 않는다.

INSERT INTO public_code (large_code, large_name, small_code, small_name, usage_type)
SELECT '1500', '입출고사유', '150005', '기초재고', 'GENERIC'
FROM DUAL
WHERE NOT EXISTS (
  SELECT 1 FROM public_code
  WHERE large_code = '1500' AND small_code = '150005' AND recording_state = 1
);

CREATE TABLE IF NOT EXISTS misc_movement_no_sequence (
  prefix    VARCHAR(40) NOT NULL PRIMARY KEY,
  last_seq  INT         NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS stocktake_batch_no_sequence (
  prefix    VARCHAR(40) NOT NULL PRIMARY KEY,
  last_seq  INT         NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS stocktake_batch (
  id               BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  batch_no         VARCHAR(40)  NOT NULL,
  stocktake_date   DATE         NOT NULL,
  status           VARCHAR(20)  NOT NULL,
  recording_state  TINYINT      NOT NULL DEFAULT 1,
  created_by_id    BIGINT       NULL,
  created_at       DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by_id    BIGINT       NULL,
  updated_at       DATETIME(3)  NULL,
  UNIQUE KEY uk_stocktake_batch_no (batch_no),
  CONSTRAINT fk_stocktake_batch_created_by FOREIGN KEY (created_by_id) REFERENCES `user` (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS stocktake_line (
  id                      BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  batch_id                BIGINT         NOT NULL,
  item_id                 BIGINT         NOT NULL,
  location_code           VARCHAR(20)    NOT NULL,
  output_process_id       BIGINT         NULL,
  book_qty                DECIMAL(18, 4) NOT NULL,
  counted_qty             DECIMAL(18, 4) NOT NULL,
  misc_stock_movement_id  BIGINT         NULL,
  recording_state         TINYINT        NOT NULL DEFAULT 1,
  created_at              DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  INDEX idx_stocktake_line_batch (batch_id),
  CONSTRAINT fk_stocktake_line_batch FOREIGN KEY (batch_id) REFERENCES stocktake_batch (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('inventory:stocktake:read', '재고실사 조회'),
('inventory:stocktake:write', '재고실사 반영')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('inventory:stocktake:read', 'inventory:stocktake:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PURCHASE_OPERATOR', 'PRODUCTION_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'inventory:stocktake:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
