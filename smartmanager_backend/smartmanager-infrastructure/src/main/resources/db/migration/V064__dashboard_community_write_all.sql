-- 대시보드(게시판): 로그인 사용자 전원 작성·수정·삭제
INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('community:board:write', 'community:board:moderate')
WHERE r.role_code IN ('VIEWER', 'BASIS_MANAGER', 'SALES_OPERATOR', 'PRODUCTION_OPERATOR', 'PURCHASE_OPERATOR')
  AND r.recording_state = 1;
