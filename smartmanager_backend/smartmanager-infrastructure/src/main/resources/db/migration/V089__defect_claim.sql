-- 불량변상 (레거시 PCL_HT). 불량현상/원인 코드 없이 사유 텍스트만 사용.

CREATE TABLE IF NOT EXISTS defect_claim (
  id              BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  partner_id      BIGINT         NOT NULL,
  item_id         BIGINT         NOT NULL,
  receipt_date    DATE           NOT NULL,
  claim_qty       DECIMAL(18, 4) NOT NULL,
  amount          DECIMAL(18, 2) NOT NULL,
  reason          VARCHAR(500)   NOT NULL,
  fiscal_year     SMALLINT       NOT NULL,
  fiscal_month    TINYINT        NOT NULL,
  recognition     ENUM('PENDING', 'APPROVED') NOT NULL DEFAULT 'PENDING',
  approved_at     DATETIME(3)    NULL,
  approved_by_user_id BIGINT     NULL,
  approval_cancelled_at DATETIME(3) NULL,
  approval_cancelled_by_user_id BIGINT NULL,
  recording_state TINYINT        NOT NULL DEFAULT 1,
  created_by      VARCHAR(100)   NULL,
  created_by_id   VARCHAR(100)   NULL,
  created_at      DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100)   NULL,
  updated_by_id   VARCHAR(100)   NULL,
  updated_at      DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  INDEX idx_defect_claim_partner_date (partner_id, receipt_date, recording_state),
  INDEX idx_defect_claim_item_date (item_id, receipt_date, recording_state),
  INDEX idx_defect_claim_receipt_date (receipt_date, recording_state),
  INDEX idx_defect_claim_recognition_fiscal (recognition, fiscal_year, fiscal_month, recording_state),
  CONSTRAINT fk_defect_claim_partner FOREIGN KEY (partner_id) REFERENCES company (id),
  CONSTRAINT fk_defect_claim_item FOREIGN KEY (item_id) REFERENCES item (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('purchase:defect-claim:read', '불량변상 조회'),
('purchase:defect-claim:write', '불량변상 등록·수정·삭제')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('purchase:defect-claim:read', 'purchase:defect-claim:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PURCHASE_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'purchase:defect-claim:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
