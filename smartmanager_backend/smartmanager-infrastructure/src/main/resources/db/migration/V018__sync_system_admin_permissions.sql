-- SYSTEM_ADMIN에 신규 권한(생산계획 등) 재동기화 — V012 이후 추가된 permission 누락 방지

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;
