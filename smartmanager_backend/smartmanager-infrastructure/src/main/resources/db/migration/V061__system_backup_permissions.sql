-- S1: 데이터 백업·복구 권한

INSERT INTO permission (permission_code, description) VALUES
('system:backup:read', '데이터 백업 목록 조회'),
('system:backup:execute', '데이터 백업·복구 실행')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('system:backup:read', 'system:backup:execute')
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;
