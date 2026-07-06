-- PRD-W1b: 작업장 부하·Capa 조회 권한 및 경고 임계값 시드

INSERT INTO permission (permission_code, description) VALUES
('production:scheduling:read', '작업장 부하·일정 조회')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'production:scheduling:read'
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'production:scheduling:read'
WHERE r.role_code = 'PRODUCTION_OPERATOR' AND r.recording_state = 1;

INSERT INTO system_settings (setting_key, value_json, created_by, created_by_id)
VALUES ('production.schedule.warn_load_threshold', '0.8', 'system', 'system')
ON DUPLICATE KEY UPDATE value_json = value_json;
