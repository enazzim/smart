-- 기타구매발주·입고 + purchase_history 기타구매 연동

ALTER TABLE purchase_history
  MODIFY COLUMN item_id BIGINT NULL,
  ADD COLUMN item_name VARCHAR(200) NULL AFTER item_id;

ALTER TABLE purchase_history
  MODIFY COLUMN source_type ENUM('PURCHASE_RECEIPT', 'QUALITY_INSPECTION', 'ETC_PURCHASE_RECEIPT') NOT NULL;

CREATE TABLE IF NOT EXISTS etc_purchase_order (
  id                      BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  order_no                VARCHAR(30)    NOT NULL,
  item_name               VARCHAR(200)   NOT NULL,
  partner_id              BIGINT         NOT NULL,
  unit_price              DECIMAL(18, 2) NOT NULL DEFAULT 0,
  order_qty               DECIMAL(18, 4) NOT NULL,
  amount                  DECIMAL(18, 2) NOT NULL DEFAULT 0,
  remain_qty              DECIMAL(18, 4) NOT NULL,
  requested_delivery_date DATE           NOT NULL,
  category_code_id        BIGINT         NULL,
  status                  ENUM('WAITING', 'IN_PROGRESS', 'COMPLETED') NOT NULL DEFAULT 'WAITING',
  order_date              DATE           NOT NULL,
  recording_state         TINYINT        NOT NULL DEFAULT 1,
  created_by              VARCHAR(100)   NULL,
  created_by_id           VARCHAR(100)   NULL,
  created_at              DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by              VARCHAR(100)   NULL,
  updated_by_id             VARCHAR(100)   NULL,
  updated_at              DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_etc_purchase_order_no (order_no, recording_state),
  INDEX idx_etc_purchase_order_partner (partner_id, recording_state),
  INDEX idx_etc_purchase_order_status (status, recording_state),
  INDEX idx_etc_purchase_order_delivery (requested_delivery_date, recording_state),
  CONSTRAINT fk_etc_purchase_order_partner FOREIGN KEY (partner_id) REFERENCES company (id),
  CONSTRAINT fk_etc_purchase_order_category FOREIGN KEY (category_code_id) REFERENCES public_code (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS etc_purchase_receipt (
  id                    BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  receipt_no            VARCHAR(30)    NOT NULL,
  etc_purchase_order_id BIGINT         NOT NULL,
  partner_id            BIGINT         NOT NULL,
  item_name             VARCHAR(200)   NOT NULL,
  receipt_qty           DECIMAL(18, 4) NOT NULL,
  unit_price            DECIMAL(18, 2) NOT NULL,
  amount                DECIMAL(18, 2) NOT NULL,
  receipt_date          DATE           NOT NULL,
  fiscal_year           SMALLINT       NOT NULL,
  fiscal_month          TINYINT        NOT NULL,
  recording_state       TINYINT        NOT NULL DEFAULT 1,
  created_by            VARCHAR(100)   NULL,
  created_by_id         VARCHAR(100)   NULL,
  created_at            DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by            VARCHAR(100)   NULL,
  updated_by_id         VARCHAR(100)   NULL,
  updated_at            DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_etc_purchase_receipt_no (receipt_no, recording_state),
  INDEX idx_etc_purchase_receipt_order (etc_purchase_order_id, recording_state),
  INDEX idx_etc_purchase_receipt_partner (partner_id, receipt_date, recording_state),
  CONSTRAINT fk_etc_purchase_receipt_order FOREIGN KEY (etc_purchase_order_id) REFERENCES etc_purchase_order (id),
  CONSTRAINT fk_etc_purchase_receipt_partner FOREIGN KEY (partner_id) REFERENCES company (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO public_code (large_code, large_name, small_code, small_name, usage_type, created_by)
SELECT '1800', '기타구매', NULL, NULL, 'GENERIC', 'seed'
FROM DUAL
WHERE NOT EXISTS (
  SELECT 1 FROM public_code
  WHERE large_code = '1800' AND small_code IS NULL AND recording_state = 1
);

INSERT INTO permission (permission_code, description) VALUES
('purchase:etc-order:read', '기타구매발주 조회'),
('purchase:etc-order:write', '기타구매발주 등록·수정·삭제'),
('purchase:etc-receipt:read', '기타구매입고 조회'),
('purchase:etc-receipt:write', '기타구매입고 등록·수정·취소')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN (
    'purchase:etc-order:read', 'purchase:etc-order:write',
    'purchase:etc-receipt:read', 'purchase:etc-receipt:write'
  )
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PURCHASE_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('purchase:etc-order:read', 'purchase:etc-receipt:read')
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
