-- 구매 담당: 지급(선지급) 거래처·단가 조회
-- 기준정보 메뉴는 열리지 않으며, 등록·수정 권한은 부여하지 않는다.

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('basis:company:read', 'basis:unit-price:read')
WHERE r.role_code = 'PURCHASE_OPERATOR'
  AND r.recording_state = 1;
