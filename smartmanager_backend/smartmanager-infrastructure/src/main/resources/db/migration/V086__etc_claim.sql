-- 기타공제등록 (레거시 ECL_HT)

CREATE TABLE IF NOT EXISTS etc_claim (
  id              BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  partner_id      BIGINT         NOT NULL,
  receipt_date    DATE           NOT NULL,
  reason          VARCHAR(500)   NOT NULL,
  amount          DECIMAL(18, 2) NOT NULL,
  recognition     ENUM('PENDING', 'APPROVED') NOT NULL DEFAULT 'PENDING',
  recording_state TINYINT        NOT NULL DEFAULT 1,
  created_by      VARCHAR(100)   NULL,
  created_by_id   VARCHAR(100)   NULL,
  created_at      DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100)   NULL,
  updated_by_id   VARCHAR(100)   NULL,
  updated_at      DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  INDEX idx_etc_claim_partner_date (partner_id, receipt_date, recording_state),
  INDEX idx_etc_claim_receipt_date (receipt_date, recording_state),
  INDEX idx_etc_claim_created_at (created_at, recording_state),
  CONSTRAINT fk_etc_claim_partner FOREIGN KEY (partner_id) REFERENCES company (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('purchase:etc-claim:read', '기타공제 조회'),
('purchase:etc-claim:write', '기타공제 등록·수정·삭제')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('purchase:etc-claim:read', 'purchase:etc-claim:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PURCHASE_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'purchase:etc-claim:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
