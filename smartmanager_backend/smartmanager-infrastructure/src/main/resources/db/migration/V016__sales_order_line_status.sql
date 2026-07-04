-- 수주 라인 이행·납품 상태

ALTER TABLE sales_order_line
  ADD COLUMN fulfillment_status ENUM('WAITING','IN_PROGRESS','COMPLETED','FORCE_COMPLETED') NOT NULL DEFAULT 'WAITING' AFTER fulfillment_route,
  ADD COLUMN delivery_status ENUM('NOT_STARTED','IN_PROGRESS','COMPLETED') NOT NULL DEFAULT 'NOT_STARTED' AFTER fulfillment_status;

CREATE INDEX idx_sales_order_line_fulfillment ON sales_order_line (fulfillment_status, recording_state);
CREATE INDEX idx_sales_order_line_delivery ON sales_order_line (delivery_status, recording_state);

INSERT INTO permission (permission_code, description) VALUES
('sales:order:progress', '수주 이행상태 변경'),
('sales:order:force-complete', '수주 강제완료')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('sales:order:progress', 'sales:order:force-complete')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'SALES_OPERATOR') AND r.recording_state = 1;
