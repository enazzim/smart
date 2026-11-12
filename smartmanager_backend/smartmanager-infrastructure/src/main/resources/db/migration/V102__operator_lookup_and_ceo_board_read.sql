-- 보이는 메뉴의 거래처·품목 등 조회, CEO 대시보드 게시판 조회·업무일지 작성
-- 기준정보 등록 권한과 community:board:write 는 부여하지 않는다.

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND (
    (r.role_code = 'SALES_OPERATOR' AND p.permission_code IN (
      'basis:company:read', 'basis:item:read', 'basis:unit-price:read'
    ))
    OR (r.role_code = 'PRODUCTION_OPERATOR' AND p.permission_code IN (
      'basis:company:read', 'basis:item:read', 'basis:process:read', 'basis:work-center:read'
    ))
    OR (r.role_code = 'PURCHASE_OPERATOR' AND p.permission_code IN (
      'basis:item:read', 'basis:process:read'
    ))
    OR (r.role_code = 'CEO' AND p.permission_code IN (
      'community:board:read',
      'community:workdiary:write',
      'basis:company:read',
      'basis:item:read',
      'stats:vendor-purchase:read',
      'stats:vendor-purchase-status:read',
      'stats:purchase-daily:read',
      'stats:partner-monthly-payable:read',
      'stats:warehouse-io:read',
      'stats:item-io:read',
      'stats:order-vs-receipt:read'
    ))
  )
WHERE r.recording_state = 1;