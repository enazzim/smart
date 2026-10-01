-- 업무일지 결재자: CEO 역할(조회·결재) 추가, SYSTEM_ADMIN 결재 권한 회수
-- 실제 결재자는 system_settings 'workdiary.approver_user_id'에 지정된 1명

INSERT INTO role (role_code, role_name) VALUES ('CEO', 'CEO')
ON DUPLICATE KEY UPDATE role_name = VALUES(role_name);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('community:workdiary:read', 'community:workdiary:approve')
WHERE r.role_code = 'CEO' AND r.recording_state = 1;

DELETE rp
FROM role_permission rp
JOIN role r ON r.id = rp.role_id
JOIN permission p ON p.id = rp.permission_id
WHERE r.role_code = 'SYSTEM_ADMIN'
  AND p.permission_code = 'community:workdiary:approve';
