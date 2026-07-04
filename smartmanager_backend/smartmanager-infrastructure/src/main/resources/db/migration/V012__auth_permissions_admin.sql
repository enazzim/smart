-- S0: RBAC permissions and role mappings

INSERT INTO permission (permission_code, description) VALUES
('system:public-code:read', '공용코드 조회'),
('system:public-code:write', '공용코드 등록·수정·삭제'),
('system:tenant:read', '기업정보 조회'),
('system:tenant:write', '기업정보 수정'),
('system:settings:read', '시스템 설정 조회'),
('system:settings:write', '시스템 설정 수정'),
('system:role:read', '역할 조회'),
('system:role:write', '역할·권한 관리'),
('system:import:execute', '초기정보 일괄입력'),
('system:month-closing:execute', '월마감 실행'),
('basis:company:read', '거래처 조회'),
('basis:company:write', '거래처 등록·수정·삭제'),
('basis:item:read', '품목 조회'),
('basis:item:write', '품목 등록·수정·삭제'),
('basis:process:read', '공정 조회'),
('basis:process:write', '공정 등록·수정·삭제'),
('basis:unit-price:read', '단가 조회'),
('basis:unit-price:write', '단가 등록·수정·삭제'),
('basis:work-center:read', '작업장 조회'),
('basis:work-center:write', '작업장 등록·수정·삭제'),
('basis:work-standard:read', '작업표준 조회'),
('basis:work-standard:write', '작업표준 등록·수정·삭제'),
('basis:equipment:read', '설비 조회'),
('basis:equipment:write', '설비 등록·수정·삭제'),
('basis:user:read', '사용자 조회'),
('basis:user:write', '사용자 등록·수정·삭제'),
('basis:production-calendar:read', '생산달력 조회'),
('basis:production-calendar:write', '생산달력 등록·수정·삭제'),
('basis:public-code:read', '공용코드(기준) 조회'),
('purchase:receipt:read', '구매입고 조회'),
('purchase:receipt:post', '구매입고 전기')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code LIKE '%:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND (p.permission_code LIKE 'basis:%'
       OR p.permission_code IN ('system:public-code:read', 'system:public-code:write', 'system:role:read'))
WHERE r.role_code = 'BASIS_MANAGER' AND r.recording_state = 1;
