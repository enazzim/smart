-- 통계및 지표: 조회 권한 (전 역할 공통)

INSERT INTO permission (permission_code, description) VALUES
('stats:vendor-purchase:read', '매입처별 집계 조회'),
('stats:warehouse-io:read', '창고별 수불현황 조회'),
('stats:item-io:read', '품목별 수불현황 조회')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN (
    'stats:vendor-purchase:read',
    'stats:warehouse-io:read',
    'stats:item-io:read'
  )
WHERE r.recording_state = 1
  AND r.role_code IN (
    'SYSTEM_ADMIN',
    'BASIS_MANAGER',
    'SALES_OPERATOR',
    'PRODUCTION_OPERATOR',
    'PURCHASE_OPERATOR',
    'VIEWER'
  );
