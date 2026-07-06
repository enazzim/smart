-- V037 보완: work-plan:read 역할에 scheduling:read 동기화 (기존 DB 호환)

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'production:scheduling:read'
JOIN permission wp ON wp.recording_state = 1 AND wp.permission_code = 'production:work-plan:read'
JOIN role_permission rp ON rp.role_id = r.id AND rp.permission_id = wp.id
WHERE r.recording_state = 1;
