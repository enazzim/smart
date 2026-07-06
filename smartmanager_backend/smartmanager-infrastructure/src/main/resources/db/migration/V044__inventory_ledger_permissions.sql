-- INF-1b: 재고·원장 조회 권한

INSERT INTO permission (permission_code, description) VALUES
('inventory:ledger:read', '재고·원장 조회')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'inventory:ledger:read'
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'inventory:ledger:read'
WHERE r.role_code = 'PRODUCTION_OPERATOR' AND r.recording_state = 1;
