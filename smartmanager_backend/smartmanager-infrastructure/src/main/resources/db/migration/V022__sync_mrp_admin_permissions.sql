-- SYSTEM_ADMIN에 MRP 권한 재동기화

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;
