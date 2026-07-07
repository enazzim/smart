-- 업무 담당 역할도 기준정보 > 사용자(본인 조회) 화면 접근 가능
INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.permission_code = 'basis:user:read' AND p.recording_state = 1
WHERE r.role_code IN ('SALES_OPERATOR', 'PRODUCTION_OPERATOR', 'PURCHASE_OPERATOR')
  AND r.recording_state = 1;
