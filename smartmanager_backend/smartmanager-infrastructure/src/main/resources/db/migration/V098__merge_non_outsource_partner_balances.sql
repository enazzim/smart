-- V098: RAW/WIP/SALES/DELIVERY 잔고는 partner_id 없이 통합 (외주창고만 거래처별 슬롯)
-- UK: (item_id, location_id, fiscal_year, output_process_id, partner_id, recording_state)
--   → 비외주 병합 시 input_process_id 는 키에서 제외하고 NULL 로 맞춘다.
-- MariaDB: UPDATE 대상 테이블을 같은 문 NOT EXISTS 로 참조하지 않는다.

-- 1) partner 없는 대상 슬롯 생성 (UK 기준)
INSERT INTO inventory_balance (
  item_id, location_id, fiscal_year, output_process_id, input_process_id, partner_id,
  stock_qty, stock_amount, recording_state, created_at, updated_at
)
SELECT
  src.item_id,
  src.location_id,
  src.fiscal_year,
  src.output_process_id,
  NULL,
  NULL,
  0,
  0,
  1,
  CURRENT_TIMESTAMP(3),
  CURRENT_TIMESTAMP(3)
FROM inventory_balance src
INNER JOIN inventory_location loc ON loc.id = src.location_id
WHERE src.recording_state = 1
  AND src.partner_id IS NOT NULL
  AND loc.location_code <> 'OUTSOURCE'
  AND NOT EXISTS (
    SELECT 1
    FROM inventory_balance tgt
    WHERE tgt.item_id = src.item_id
      AND tgt.location_id = src.location_id
      AND tgt.fiscal_year = src.fiscal_year
      AND tgt.recording_state = 1
      AND tgt.partner_id IS NULL
      AND (
        (tgt.output_process_id IS NULL AND src.output_process_id IS NULL)
        OR tgt.output_process_id = src.output_process_id
      )
  )
GROUP BY
  src.item_id,
  src.location_id,
  src.fiscal_year,
  src.output_process_id;

-- 2) source → target 매핑
CREATE TEMPORARY TABLE IF NOT EXISTS tmp_ib_partner_merge (
  source_id BIGINT NOT NULL PRIMARY KEY,
  target_id BIGINT NOT NULL
);

DELETE FROM tmp_ib_partner_merge;

INSERT INTO tmp_ib_partner_merge (source_id, target_id)
SELECT src.id, MIN(tgt.id)
FROM inventory_balance src
INNER JOIN inventory_location loc ON loc.id = src.location_id
INNER JOIN inventory_balance tgt
  ON tgt.item_id = src.item_id
 AND tgt.location_id = src.location_id
 AND tgt.fiscal_year = src.fiscal_year
 AND tgt.recording_state = 1
 AND tgt.partner_id IS NULL
 AND (
   (tgt.output_process_id IS NULL AND src.output_process_id IS NULL)
   OR tgt.output_process_id = src.output_process_id
 )
WHERE src.recording_state = 1
  AND src.partner_id IS NOT NULL
  AND loc.location_code <> 'OUTSOURCE'
  AND src.id <> tgt.id
GROUP BY src.id;

-- 3) 수량·금액 합산
UPDATE inventory_balance tgt
INNER JOIN (
  SELECT m.target_id AS tid,
         COALESCE(SUM(src.stock_qty), 0) AS add_qty,
         COALESCE(SUM(src.stock_amount), 0) AS add_amt
  FROM tmp_ib_partner_merge m
  INNER JOIN inventory_balance src ON src.id = m.source_id
  GROUP BY m.target_id
) x ON x.tid = tgt.id
SET tgt.stock_qty = tgt.stock_qty + x.add_qty,
    tgt.stock_amount = tgt.stock_amount + x.add_amt,
    tgt.updated_at = CURRENT_TIMESTAMP(3);

-- 4) 월별: 대상에 있으면면 합산
UPDATE inventory_balance_monthly tgt
INNER JOIN (
  SELECT m.target_id AS tid,
         sm.month_num AS mnum,
         COALESCE(SUM(sm.in_qty), 0) AS add_in_qty,
         COALESCE(SUM(sm.in_amount), 0) AS add_in_amt,
         COALESCE(SUM(sm.out_qty), 0) AS add_out_qty,
         COALESCE(SUM(sm.out_amount), 0) AS add_out_amt,
         COALESCE(SUM(sm.stock_qty), 0) AS add_stock_qty
  FROM tmp_ib_partner_merge m
  INNER JOIN inventory_balance_monthly sm
    ON sm.inventory_balance_id = m.source_id
   AND sm.recording_state = 1
  GROUP BY m.target_id, sm.month_num
) x ON x.tid = tgt.inventory_balance_id
   AND x.mnum = tgt.month_num
   AND tgt.recording_state = 1
SET tgt.in_qty = tgt.in_qty + x.add_in_qty,
    tgt.in_amount = tgt.in_amount + x.add_in_amt,
    tgt.out_qty = tgt.out_qty + x.add_out_qty,
    tgt.out_amount = tgt.out_amount + x.add_out_amt,
    tgt.stock_qty = tgt.stock_qty + x.add_stock_qty,
    tgt.updated_at = CURRENT_TIMESTAMP(3);

-- 5) 월별: 대상에 없는 month 만 이동 (동일 테이블 NOT EXISTS 회피)
CREATE TEMPORARY TABLE IF NOT EXISTS tmp_ib_monthly_move (
  monthly_id BIGINT NOT NULL PRIMARY KEY,
  target_id BIGINT NOT NULL
);

DELETE FROM tmp_ib_monthly_move;

INSERT INTO tmp_ib_monthly_move (monthly_id, target_id)
SELECT sm.id, m.target_id
FROM inventory_balance_monthly sm
INNER JOIN tmp_ib_partner_merge m ON m.source_id = sm.inventory_balance_id
LEFT JOIN inventory_balance_monthly tgt
  ON tgt.inventory_balance_id = m.target_id
 AND tgt.month_num = sm.month_num
 AND tgt.recording_state = 1
WHERE sm.recording_state = 1
  AND tgt.id IS NULL;

UPDATE inventory_balance_monthly sm
INNER JOIN tmp_ib_monthly_move mv ON mv.monthly_id = sm.id
SET sm.inventory_balance_id = mv.target_id,
    sm.updated_at = CURRENT_TIMESTAMP(3);

-- 6) source 월별(합산된 것) 비활성
UPDATE inventory_balance_monthly sm
INNER JOIN tmp_ib_partner_merge m ON m.source_id = sm.inventory_balance_id
SET sm.recording_state = 0,
    sm.updated_at = CURRENT_TIMESTAMP(3)
WHERE sm.recording_state = 1;

-- 7) Lot: 대상에 동일 lot 있으면면 합산
UPDATE inventory_lot_balance tgt
INNER JOIN tmp_ib_partner_merge m ON m.target_id = tgt.inventory_balance_id
INNER JOIN inventory_lot_balance src
  ON src.lot_id = tgt.lot_id
 AND src.inventory_balance_id = m.source_id
 AND src.recording_state = 1
SET tgt.qty_on_hand = tgt.qty_on_hand + src.qty_on_hand,
    tgt.updated_at = CURRENT_TIMESTAMP(3)
WHERE tgt.recording_state = 1;

UPDATE inventory_lot_balance src
INNER JOIN tmp_ib_partner_merge m ON m.source_id = src.inventory_balance_id
INNER JOIN inventory_lot_balance tgt
  ON tgt.lot_id = src.lot_id
 AND tgt.inventory_balance_id = m.target_id
 AND tgt.recording_state = 1
SET src.recording_state = 0,
    src.updated_at = CURRENT_TIMESTAMP(3)
WHERE src.recording_state = 1;

-- 8) Lot: 대상에 없으면 이동
CREATE TEMPORARY TABLE IF NOT EXISTS tmp_ib_lot_move (
  lot_balance_id BIGINT NOT NULL PRIMARY KEY,
  target_id BIGINT NOT NULL
);

DELETE FROM tmp_ib_lot_move;

INSERT INTO tmp_ib_lot_move (lot_balance_id, target_id)
SELECT src.id, m.target_id
FROM inventory_lot_balance src
INNER JOIN tmp_ib_partner_merge m ON m.source_id = src.inventory_balance_id
LEFT JOIN inventory_lot_balance tgt
  ON tgt.lot_id = src.lot_id
 AND tgt.inventory_balance_id = m.target_id
 AND tgt.recording_state = 1
WHERE src.recording_state = 1
  AND tgt.id IS NULL;

UPDATE inventory_lot_balance src
INNER JOIN tmp_ib_lot_move mv ON mv.lot_balance_id = src.id
SET src.inventory_balance_id = mv.target_id,
    src.updated_at = CURRENT_TIMESTAMP(3);

-- 9) 수불 이력 balance FK 재연결
UPDATE stock_movement sm
INNER JOIN tmp_ib_partner_merge m ON m.source_id = sm.inventory_balance_id
SET sm.inventory_balance_id = m.target_id;

-- 10) source 잔고 비활성
UPDATE inventory_balance src
INNER JOIN tmp_ib_partner_merge m ON m.source_id = src.id
SET src.recording_state = 0,
    src.stock_qty = 0,
    src.stock_amount = 0,
    src.updated_at = CURRENT_TIMESTAMP(3);

DROP TEMPORARY TABLE IF EXISTS tmp_ib_lot_move;
DROP TEMPORARY TABLE IF EXISTS tmp_ib_monthly_move;
DROP TEMPORARY TABLE IF EXISTS tmp_ib_partner_merge;
