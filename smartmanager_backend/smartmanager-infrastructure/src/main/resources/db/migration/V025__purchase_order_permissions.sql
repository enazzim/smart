-- TX1: 구매발주 권한

INSERT INTO permission (permission_code, description) VALUES
('purchase:order:read', '구매발주 조회'),
('purchase:order:write', '구매발주 등록·수정'),
('purchase:order:confirm', '구매발주 확정')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('purchase:order:read', 'purchase:order:write', 'purchase:order:confirm')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PURCHASE_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'purchase:order:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
