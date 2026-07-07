-- Wave②: 수주 없이 생산계획 수립 (MANUAL)

ALTER TABLE production_plan
  ADD COLUMN source_type ENUM('SALES_ORDER', 'MANUAL') NOT NULL DEFAULT 'SALES_ORDER' AFTER plan_no;

ALTER TABLE production_plan
  MODIFY COLUMN sales_order_id BIGINT NULL,
  MODIFY COLUMN sales_order_line_id BIGINT NULL;
