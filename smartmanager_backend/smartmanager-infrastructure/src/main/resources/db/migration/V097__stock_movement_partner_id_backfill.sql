-- V097: 기존 stock_movement.partner_id 보정 (참조 전표 기준)
-- 이후 등록분은 InventoryBalanceService.recordMovement가 partner_id를 저장한다.
-- 주의: 별칭 ssl 은 MariaDB 예약어라 사용 금지.

UPDATE stock_movement sm
SET sm.partner_id = (
  SELECT os.partner_id
  FROM outsourcing_shipment os
  WHERE os.id = sm.reference_id
  LIMIT 1
)
WHERE sm.partner_id IS NULL
  AND sm.reference_type IN ('OUTSOURCING_SHIPMENT', 'OUTSOURCING_SHIPMENT_CANCEL')
  AND EXISTS (
    SELECT 1 FROM outsourcing_shipment os
    WHERE os.id = sm.reference_id AND os.partner_id IS NOT NULL
  );

UPDATE stock_movement sm
SET sm.partner_id = (
  SELECT orec.partner_id
  FROM outsourcing_receipt orec
  WHERE orec.id = sm.reference_id
  LIMIT 1
)
WHERE sm.partner_id IS NULL
  AND sm.reference_type IN ('OUTSOURCING_RECEIPT', 'OUTSOURCING_RECEIPT_CANCEL')
  AND EXISTS (
    SELECT 1 FROM outsourcing_receipt orec
    WHERE orec.id = sm.reference_id AND orec.partner_id IS NOT NULL
  );

UPDATE stock_movement sm
SET sm.partner_id = (
  SELECT sh.partner_id
  FROM sales_shipment_line shl
  INNER JOIN sales_shipment sh ON sh.id = shl.sales_shipment_id
  WHERE shl.id = sm.reference_id
    AND shl.recording_state = 1
  LIMIT 1
)
WHERE sm.partner_id IS NULL
  AND sm.reference_type IN ('SALES_SHIPMENT', 'SALES_SHIPMENT_CANCEL')
  AND EXISTS (
    SELECT 1
    FROM sales_shipment_line shl
    INNER JOIN sales_shipment sh ON sh.id = shl.sales_shipment_id
    WHERE shl.id = sm.reference_id
      AND shl.recording_state = 1
      AND sh.partner_id IS NOT NULL
  );

UPDATE stock_movement sm
SET sm.partner_id = (
  SELECT rv.partner_id
  FROM sales_revenue_line rvl
  INNER JOIN sales_revenue rv ON rv.id = rvl.sales_revenue_id
  WHERE rvl.id = sm.reference_id
    AND rvl.recording_state = 1
  LIMIT 1
)
WHERE sm.partner_id IS NULL
  AND sm.reference_type IN ('SALES_REVENUE', 'SALES_REVENUE_CANCEL')
  AND EXISTS (
    SELECT 1
    FROM sales_revenue_line rvl
    INNER JOIN sales_revenue rv ON rv.id = rvl.sales_revenue_id
    WHERE rvl.id = sm.reference_id
      AND rvl.recording_state = 1
      AND rv.partner_id IS NOT NULL
  );

UPDATE stock_movement sm
SET sm.partner_id = (
  SELECT pr.partner_id
  FROM purchase_receipt_line prl
  INNER JOIN purchase_receipt pr ON pr.id = prl.purchase_receipt_id
  WHERE prl.id = sm.reference_id
    AND prl.recording_state = 1
  LIMIT 1
)
WHERE sm.partner_id IS NULL
  AND sm.reference_type IN ('PURCHASE_RECEIPT', 'PURCHASE_RECEIPT_CANCEL')
  AND EXISTS (
    SELECT 1
    FROM purchase_receipt_line prl
    INNER JOIN purchase_receipt pr ON pr.id = prl.purchase_receipt_id
    WHERE prl.id = sm.reference_id
      AND prl.recording_state = 1
      AND pr.partner_id IS NOT NULL
  );

UPDATE stock_movement sm
SET sm.partner_id = (
  SELECT qi.company_id
  FROM quality_inspection qi
  WHERE qi.id = sm.reference_id
  LIMIT 1
)
WHERE sm.partner_id IS NULL
  AND sm.reference_type IN ('QUALITY_INSPECTION', 'QUALITY_INSPECTION_CANCEL')
  AND EXISTS (
    SELECT 1 FROM quality_inspection qi
    WHERE qi.id = sm.reference_id AND qi.company_id IS NOT NULL
  );
