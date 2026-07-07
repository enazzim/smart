-- 테스트 DB 정리: 기준정보(마스터) 제외 트랜잭션 데이터만 삭제
-- 유지: company, item, BOM, process, unit_price, work_center, work_standard,
--       equipment, calendar, user/role/permission, public_code, system_settings,
--       inventory_location, partner_ledger_account, flyway_schema_history 등

SET FOREIGN_KEY_CHECKS = 0;

TRUNCATE TABLE sales_collection;
TRUNCATE TABLE sales_revenue_line;
TRUNCATE TABLE sales_revenue;
TRUNCATE TABLE sales_history;
TRUNCATE TABLE sales_shipment_line;
TRUNCATE TABLE sales_shipment;
TRUNCATE TABLE sales_order_line;
TRUNCATE TABLE sales_order;

TRUNCATE TABLE outsourcing_receipt_line;
TRUNCATE TABLE outsourcing_receipt;
TRUNCATE TABLE outsource_history;
TRUNCATE TABLE outsourcing_shipment_input_line;
TRUNCATE TABLE outsourcing_shipment_line;
TRUNCATE TABLE outsourcing_shipment;
TRUNCATE TABLE outsourcing_order_line;
TRUNCATE TABLE outsourcing_order;

TRUNCATE TABLE quality_inspection;
TRUNCATE TABLE purchase_history;
TRUNCATE TABLE purchase_receipt_line;
TRUNCATE TABLE purchase_receipt;
TRUNCATE TABLE purchase_order_line;
TRUNCATE TABLE purchase_order;

TRUNCATE TABLE material_issue_line;
TRUNCATE TABLE material_issue;
TRUNCATE TABLE work_report_consumption_line;
TRUNCATE TABLE work_report_history;
TRUNCATE TABLE work_report;
TRUNCATE TABLE work_order;
TRUNCATE TABLE work_plan;
TRUNCATE TABLE material_requirement_line;
TRUNCATE TABLE mrp_run;
TRUNCATE TABLE production_plan;

TRUNCATE TABLE stock_movement;
TRUNCATE TABLE inventory_balance_monthly;
TRUNCATE TABLE inventory_balance;

TRUNCATE TABLE partner_ledger_monthly;
TRUNCATE TABLE month_closing;
TRUNCATE TABLE domain_event;

SET FOREIGN_KEY_CHECKS = 1;
