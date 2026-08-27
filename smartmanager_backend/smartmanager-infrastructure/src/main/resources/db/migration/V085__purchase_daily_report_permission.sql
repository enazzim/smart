-- 통계및 지표: 매입일보 조회 권한

INSERT INTO permission (permission_code, description) VALUES
('stats:purchase-daily:read', '매입일보 조회')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'stats:purchase-daily:read'
WHERE r.recording_state = 1
  AND r.role_code IN (
    'SYSTEM_ADMIN', 'BASIS_MANAGER', 'SALES_OPERATOR',
    'PRODUCTION_OPERATOR', 'PURCHASE_OPERATOR', 'VIEWER'
  );
