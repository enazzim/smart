-- 기준정보: 도면관리 권한

INSERT INTO permission (permission_code, description) VALUES
('basis:drawing:read', '도면 조회'),
('basis:drawing:write', '도면 등록·수정·삭제')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('basis:drawing:read', 'basis:drawing:write')
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('basis:drawing:read', 'basis:drawing:write')
WHERE r.role_code = 'BASIS_MANAGER' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'basis:drawing:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'basis:drawing:read'
WHERE r.role_code IN ('SALES_OPERATOR', 'PRODUCTION_OPERATOR', 'PURCHASE_OPERATOR')
  AND r.recording_state = 1;
