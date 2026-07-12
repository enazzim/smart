-- 도면 영구 삭제: SYSTEM_ADMIN 전용 (논리 삭제·복구는 basis:drawing:write)

INSERT INTO permission (permission_code, description) VALUES
('basis:drawing:hard-delete', '도면 영구 삭제 (휴지통)')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'basis:drawing:hard-delete'
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;
