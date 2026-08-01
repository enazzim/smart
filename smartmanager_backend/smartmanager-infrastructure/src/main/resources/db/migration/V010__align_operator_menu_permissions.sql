-- 메뉴·권한 정렬: 생산→품질검사, 구매→재고·원장

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('quality:inspection:read', 'quality:inspection:complete')
WHERE r.role_code = 'PRODUCTION_OPERATOR' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'inventory:ledger:read'
WHERE r.role_code = 'PURCHASE_OPERATOR' AND r.recording_state = 1;
