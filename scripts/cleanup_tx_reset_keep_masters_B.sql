-- =============================================================================
-- B안: 기준정보·Flyway 시드 유지 + 업무 TX/이력/수불 전량 삭제 + 잔액 0
-- =============================================================================
-- 목적
--   영업·생산·구매·외주·품질 메뉴의 업무 데이터와 재고 수불·이력을 비우고
--   재고수량·거래처 원장(미수/매입 관련 이월·월별)을 0으로 맞춘다.
--
-- 유지 (삭제하지 않음)
--   company, company_role, item, item_composition, bom_change_log,
--   equipment, work_center, process_sequence, work_standard,
--   unit_price, unit_price_change_log,
--   production_calendar, work_center_calendar, public_holiday,
--   user, role, permission, user_role, role_permission,
--   code_group, public_code, inventory_location, system_settings,
--   partner_ledger_account (행 유지, 금액만 0),
--   inventory_balance (슬롯 행 유지, 수량/금액만 0),
--   flyway_schema_history,
--   domain_event 중 occurred_at < 2026-08-20 (이후 건만 DELETE),
--   work_diary_template,
--   도면(drawing_*), 게시판(board_*), 업무일지 엔트리(work_diary_entry)
--   ※ 게시판·도면·업무일지까지 지우려면 하단 OPTIONAL 섹션 참고
--
-- 주의
--   1) 물리 TRUNCATE/DELETE. 반드시 백업 후 실행.
--   2) 앱(API) 중지 권장.
--   3) 트랜잭션으로 감싸려면 TRUNCATE 대신 DELETE로 바꿔야 함
--      (InnoDB에서 TRUNCATE는 암시적 커밋). 본 스크립트는 TRUNCATE 사용.
--   4) 실행 예:
--      mysql -h... -u... -p... smartmanager < scripts/cleanup_tx_reset_keep_masters_B.sql
--      또는: powershell -File scripts/clean-test-db.ps1 -Mode truncate
--         (clean-test-db-except-basis.sql 이 본 B안과 동기화됨)
-- =============================================================================

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- -----------------------------------------------------------------------------
-- 1) 재고 수불 / Lot / 기타수불 (전량)
-- -----------------------------------------------------------------------------
TRUNCATE TABLE lot_genealogy;
TRUNCATE TABLE inventory_lot_balance;
TRUNCATE TABLE stock_movement;
TRUNCATE TABLE misc_stock_movement;
TRUNCATE TABLE inventory_lot;
TRUNCATE TABLE lot_number_sequence;
TRUNCATE TABLE inventory_balance_monthly;

-- -----------------------------------------------------------------------------
-- 2) 영업
-- -----------------------------------------------------------------------------
TRUNCATE TABLE sales_collection;
TRUNCATE TABLE sales_history;
TRUNCATE TABLE sales_revenue_line;
TRUNCATE TABLE sales_revenue;
TRUNCATE TABLE sales_shipment_line;
TRUNCATE TABLE sales_shipment;
TRUNCATE TABLE sales_order_line;
TRUNCATE TABLE sales_order;

-- -----------------------------------------------------------------------------
-- 3) 구매 / 지급 / 기타구매 / 클레임
-- -----------------------------------------------------------------------------
TRUNCATE TABLE partner_prepaid_offset;
TRUNCATE TABLE partner_payment_line;
TRUNCATE TABLE partner_payment;
TRUNCATE TABLE purchase_history;
TRUNCATE TABLE purchase_receipt_line;
TRUNCATE TABLE purchase_receipt;
TRUNCATE TABLE purchase_order_line;
TRUNCATE TABLE purchase_order;
TRUNCATE TABLE etc_purchase_receipt;
TRUNCATE TABLE etc_purchase_order;
TRUNCATE TABLE defect_claim;
TRUNCATE TABLE etc_claim;

-- -----------------------------------------------------------------------------
-- 4) 외주
-- -----------------------------------------------------------------------------
TRUNCATE TABLE outsource_history;
TRUNCATE TABLE outsourcing_receipt_line;
TRUNCATE TABLE outsourcing_receipt;
TRUNCATE TABLE outsourcing_shipment_input_line;
TRUNCATE TABLE outsourcing_shipment_line;
TRUNCATE TABLE outsourcing_shipment;
TRUNCATE TABLE outsourcing_order_line;
TRUNCATE TABLE outsourcing_order;

-- -----------------------------------------------------------------------------
-- 5) 생산 / 자재소요 / 작업
-- -----------------------------------------------------------------------------
TRUNCATE TABLE material_requirement_line;
TRUNCATE TABLE mrp_run;
TRUNCATE TABLE material_issue_line;
TRUNCATE TABLE material_issue;
TRUNCATE TABLE work_report_consumption_line;
TRUNCATE TABLE work_report_history;
TRUNCATE TABLE work_report;
TRUNCATE TABLE work_order;
TRUNCATE TABLE work_plan;
TRUNCATE TABLE production_plan;

-- -----------------------------------------------------------------------------
-- 6) 품질
-- -----------------------------------------------------------------------------
TRUNCATE TABLE quality_inspection;

-- -----------------------------------------------------------------------------
-- 7) 마감 / 도메인 이벤트 / 거래처 월별 원장
-- -----------------------------------------------------------------------------
TRUNCATE TABLE month_closing;
TRUNCATE TABLE partner_ledger_monthly;
-- 기준정보 등록 이벤트(8/20 이전)는 유지. TX·이후 이벤트만 삭제.
DELETE FROM domain_event
WHERE occurred_at >= '2026-08-20 00:00:00.000';

SET FOREIGN_KEY_CHECKS = 1;

-- -----------------------------------------------------------------------------
-- 8) 잔액 0 (슬롯·계정 행은 유지)
-- -----------------------------------------------------------------------------
UPDATE inventory_balance
SET stock_qty = 0,
    stock_amount = 0,
    updated_at = CURRENT_TIMESTAMP(3);

UPDATE partner_ledger_account
SET prior_sale_carryover = 0,
    prior_buy_carryover = 0,
    updated_at = CURRENT_TIMESTAMP(3);

-- =============================================================================
-- OPTIONAL: 게시판·업무일지·도면까지 초기화할 때만 주석 해제
-- =============================================================================
-- SET FOREIGN_KEY_CHECKS = 0;
-- TRUNCATE TABLE board_post_required_reader;
-- TRUNCATE TABLE board_post_read;
-- TRUNCATE TABLE board_attachment;
-- TRUNCATE TABLE board_post;
-- TRUNCATE TABLE work_diary_entry;
-- TRUNCATE TABLE drawing_reference;
-- TRUNCATE TABLE drawing_history;
-- TRUNCATE TABLE drawing_master;
-- SET FOREIGN_KEY_CHECKS = 1;

-- =============================================================================
-- 검증 (결과가 모두 0 / 빈 결과여야 함)
-- =============================================================================
SELECT 'inventory_balance.qty' AS check_name, COALESCE(SUM(stock_qty),0) AS v
FROM inventory_balance WHERE recording_state = 1
UNION ALL
SELECT 'inventory_balance.amt', COALESCE(SUM(stock_amount),0)
FROM inventory_balance WHERE recording_state = 1
UNION ALL
SELECT 'inventory_lot_balance', COALESCE(SUM(qty_on_hand),0)
FROM inventory_lot_balance WHERE recording_state = 1
UNION ALL
SELECT 'stock_movement', COUNT(*) FROM stock_movement
UNION ALL
SELECT 'sales_revenue', COUNT(*) FROM sales_revenue
UNION ALL
SELECT 'sales_collection', COUNT(*) FROM sales_collection
UNION ALL
SELECT 'purchase_order', COUNT(*) FROM purchase_order
UNION ALL
SELECT 'production_plan', COUNT(*) FROM production_plan
UNION ALL
SELECT 'ledger_carryover', COALESCE(SUM(prior_sale_carryover + prior_buy_carryover),0)
FROM partner_ledger_account WHERE recording_state = 1
UNION ALL
SELECT 'ledger_monthly', COALESCE(SUM(sale_amount + purchase_amount),0)
FROM partner_ledger_monthly WHERE recording_state = 1;

-- 미수 후보와 동일 계산: 행이 나오면 안 됨
SELECT c.id, c.company_name,
       COALESCE(rev.amt, 0) - COALESCE(col.amt, 0) AS uncollected
FROM company c
LEFT JOIN (
  SELECT sr.partner_id, SUM(srl.amount) AS amt
  FROM sales_revenue sr
  JOIN sales_revenue_line srl
    ON srl.sales_revenue_id = sr.id AND srl.recording_state = 1
  WHERE sr.recording_state = 1 AND sr.status = 'ISSUED'
  GROUP BY sr.partner_id
) rev ON rev.partner_id = c.id
LEFT JOIN (
  SELECT partner_id, SUM(total_amount) AS amt
  FROM sales_collection
  WHERE recording_state = 1 AND status = 'ISSUED'
  GROUP BY partner_id
) col ON col.partner_id = c.id
HAVING uncollected <> 0;