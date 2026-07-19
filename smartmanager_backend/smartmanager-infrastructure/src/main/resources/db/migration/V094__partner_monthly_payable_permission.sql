-- 통계및 지표: 월별 실지급액(거래처) 조회 권한

INSERT INTO permission (permission_code, description) VALUES
('stats:partner-monthly-payable:read', '월별 실지급액 조회')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'stats:partner-monthly-payable:read'
WHERE r.recording_state = 1
  AND r.role_code IN (
    'SYSTEM_ADMIN', 'BASIS_MANAGER', 'SALES_OPERATOR',
    'PRODUCTION_OPERATOR', 'PURCHASE_OPERATOR', 'VIEWER'
  );
