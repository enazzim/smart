-- TX1-PA: 입고 지급 승인 (구매·외주·기타구매)

ALTER TABLE purchase_history
  ADD COLUMN approval_status ENUM('PENDING','APPROVED') NOT NULL DEFAULT 'PENDING' AFTER fiscal_month,
  ADD COLUMN approved_at DATETIME(3) NULL AFTER approval_status,
  ADD COLUMN approved_by_user_id BIGINT NULL AFTER approved_at,
  ADD COLUMN approval_cancelled_at DATETIME(3) NULL AFTER approved_by_user_id,
  ADD COLUMN approval_cancelled_by_user_id BIGINT NULL AFTER approval_cancelled_at,
  ADD INDEX idx_purchase_history_approval (approval_status, history_date, recording_state),
  ADD CONSTRAINT fk_purchase_history_approved_by FOREIGN KEY (approved_by_user_id) REFERENCES `user` (id),
  ADD CONSTRAINT fk_purchase_history_approval_cancelled_by FOREIGN KEY (approval_cancelled_by_user_id) REFERENCES `user` (id);

ALTER TABLE outsource_history
  ADD COLUMN approval_status ENUM('PENDING','APPROVED') NOT NULL DEFAULT 'PENDING' AFTER fiscal_month,
  ADD COLUMN approved_at DATETIME(3) NULL AFTER approval_status,
  ADD COLUMN approved_by_user_id BIGINT NULL AFTER approved_at,
  ADD COLUMN approval_cancelled_at DATETIME(3) NULL AFTER approved_by_user_id,
  ADD COLUMN approval_cancelled_by_user_id BIGINT NULL AFTER approval_cancelled_at,
  ADD INDEX idx_outsource_history_approval (approval_status, history_date, recording_state),
  ADD CONSTRAINT fk_outsource_history_approved_by FOREIGN KEY (approved_by_user_id) REFERENCES `user` (id),
  ADD CONSTRAINT fk_outsource_history_approval_cancelled_by FOREIGN KEY (approval_cancelled_by_user_id) REFERENCES `user` (id);

-- 기존 운영 데이터: 이미 원장 반영된 건은 승인 완료로 간주
UPDATE purchase_history SET approval_status = 'APPROVED' WHERE recording_state = 1;
UPDATE outsource_history SET approval_status = 'APPROVED' WHERE recording_state = 1;

INSERT INTO permission (permission_code, description) VALUES
('purchase:payable-approval:read', '입고 지급 승인 조회'),
('purchase:payable-approval:write', '입고 지급 승인·취소')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('purchase:payable-approval:read', 'purchase:payable-approval:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PURCHASE_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'purchase:payable-approval:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
