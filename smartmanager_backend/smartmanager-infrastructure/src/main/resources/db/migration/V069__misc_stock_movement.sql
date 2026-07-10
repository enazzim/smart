-- 기타 입출고 TX

CREATE TABLE IF NOT EXISTS misc_stock_movement (
  id                  BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  movement_no         VARCHAR(50)    NOT NULL,
  movement_date       DATE           NOT NULL,
  movement_direction  ENUM('IN','OUT') NOT NULL,
  item_id             BIGINT         NOT NULL,
  location_code       VARCHAR(20)    NOT NULL,
  output_process_id   BIGINT         NULL,
  qty                 DECIMAL(18, 4) NOT NULL,
  reason_code_id      BIGINT         NULL,
  note                VARCHAR(500)   NULL,
  status              ENUM('REGISTERED','CANCELLED') NOT NULL DEFAULT 'REGISTERED',
  recording_state     TINYINT        NOT NULL DEFAULT 1,
  created_by          VARCHAR(100)   NULL,
  created_by_id       VARCHAR(100)   NULL,
  created_at          DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by          VARCHAR(100)   NULL,
  updated_by_id       VARCHAR(100)   NULL,
  updated_at          DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_misc_stock_movement_no (movement_no, recording_state),
  INDEX idx_misc_stock_movement_item (item_id, movement_date, recording_state),
  INDEX idx_misc_stock_movement_date (movement_date, recording_state),
  CONSTRAINT fk_misc_stock_movement_item FOREIGN KEY (item_id) REFERENCES item (id),
  CONSTRAINT fk_misc_stock_movement_process FOREIGN KEY (output_process_id) REFERENCES process_sequence (id),
  CONSTRAINT fk_misc_stock_movement_reason FOREIGN KEY (reason_code_id) REFERENCES public_code (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO public_code (large_code, large_name, small_code, small_name, usage_type, created_by)
SELECT '1500', '입출고사유', NULL, NULL, 'GENERIC', 'seed'
FROM DUAL
WHERE NOT EXISTS (
  SELECT 1 FROM public_code
  WHERE large_code = '1500' AND small_code IS NULL AND recording_state = 1
);

INSERT INTO public_code (large_code, large_name, small_code, small_name, usage_type, created_by)
SELECT v.large_code, v.large_name, v.small_code, v.small_name, 'GENERIC', 'seed'
FROM (
  SELECT '1500' AS large_code, '입출고사유' AS large_name, '150001' AS small_code, '실사조정' AS small_name UNION ALL
  SELECT '1500', '입출고사유', '150002', '폐기' UNION ALL
  SELECT '1500', '입출고사유', '150003', '샘플' UNION ALL
  SELECT '1500', '입출고사유', '150004', '기타'
) v
WHERE NOT EXISTS (
  SELECT 1 FROM public_code pc
  WHERE pc.large_code = v.large_code
    AND pc.small_code = v.small_code
    AND pc.recording_state = 1
);

INSERT INTO permission (permission_code, description) VALUES
('inventory:misc-movement:read', '기타 입출고 조회'),
('inventory:misc-movement:write', '기타 입출고 등록·취소')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('inventory:misc-movement:read', 'inventory:misc-movement:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PURCHASE_OPERATOR', 'PRODUCTION_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'inventory:misc-movement:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
