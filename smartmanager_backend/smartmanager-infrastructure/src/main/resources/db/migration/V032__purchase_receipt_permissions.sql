-- TX1-R: 구매입고·품질검사 권한

INSERT INTO permission (permission_code, description) VALUES
('purchase:receipt:read', '구매입고 조회'),
('purchase:receipt:write', '구매입고 등록·취소'),
('quality:inspection:read', '품질검사 조회'),
('quality:inspection:complete', '품질검사 완료')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN (
    'purchase:receipt:read', 'purchase:receipt:write',
    'quality:inspection:read', 'quality:inspection:complete'
  )
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PURCHASE_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('purchase:receipt:read', 'quality:inspection:read')
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
