-- 업무일지 그룹별 하위 메뉴 허용. 기존 그룹은 현재 메뉴를 모두 허용한 상태로 시작한다.

CREATE TABLE IF NOT EXISTS work_diary_group_menu (
  work_diary_group_id BIGINT       NOT NULL,
  menu_id             VARCHAR(64)  NOT NULL,
  PRIMARY KEY (work_diary_group_id, menu_id),
  CONSTRAINT fk_work_diary_group_menu_group FOREIGN KEY (work_diary_group_id) REFERENCES public_code (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('system:menu-grant:write', '업무일지 그룹별 화면 설정')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'system:menu-grant:write'
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT INTO work_diary_group_menu (work_diary_group_id, menu_id)
SELECT pc.id, m.menu_id
FROM public_code pc
JOIN (
  SELECT 'sales-order' AS menu_id
  UNION ALL SELECT 'sales-shipment'
  UNION ALL SELECT 'sales-revenue'
  UNION ALL SELECT 'sales-collection'
  UNION ALL SELECT 'prod-plan'
  UNION ALL SELECT 'prod-mrp'
  UNION ALL SELECT 'prod-work-plan'
  UNION ALL SELECT 'prod-schedule'
  UNION ALL SELECT 'prod-work-order'
  UNION ALL SELECT 'prod-material-issue'
  UNION ALL SELECT 'prod-work-diary'
  UNION ALL SELECT 'purchase-order'
  UNION ALL SELECT 'purchase-receipt'
  UNION ALL SELECT 'purchase-etc-order'
  UNION ALL SELECT 'purchase-etc-receipt'
  UNION ALL SELECT 'purchase-etc-claim'
  UNION ALL SELECT 'purchase-defect-claim'
  UNION ALL SELECT 'purchase-payable-approval'
  UNION ALL SELECT 'purchase-payment'
  UNION ALL SELECT 'outsource-order'
  UNION ALL SELECT 'outsource-shipment'
  UNION ALL SELECT 'outsource-receipt'
  UNION ALL SELECT 'inventory-misc-movement'
  UNION ALL SELECT 'inventory-ledger'
  UNION ALL SELECT 'inventory-lot'
  UNION ALL SELECT 'inventory-stocktake'
  UNION ALL SELECT 'quality-inspection'
  UNION ALL SELECT 'stats-vendor-purchase-status'
  UNION ALL SELECT 'stats-purchase-daily'
  UNION ALL SELECT 'stats-partner-monthly-payable'
  UNION ALL SELECT 'stats-warehouse-io'
  UNION ALL SELECT 'stats-item-io'
  UNION ALL SELECT 'stats-order-vs-receipt'
  UNION ALL SELECT 'systemSettings'
  UNION ALL SELECT 'masterImport'
  UNION ALL SELECT 'publicCode'
  UNION ALL SELECT 'role'
  UNION ALL SELECT 'monthClosing'
) m
WHERE pc.usage_type = 'WORK_DIARY_GROUP'
  AND pc.small_code IS NOT NULL
  AND pc.recording_state = 1;
