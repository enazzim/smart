-- VIEWER 역할에 누락된 조회(:read) 권한 일괄 부여
-- V012 이후 추가된 permission은 개별 마이그레이션에서 VIEWER 누락이 있을 수 있음

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code LIKE '%:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
