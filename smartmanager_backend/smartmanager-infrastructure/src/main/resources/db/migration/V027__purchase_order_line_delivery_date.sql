-- TX1: 구매발주 라인 납기요구일 (품목 리드타임 기반)

ALTER TABLE purchase_order_line
  ADD COLUMN requested_delivery_date DATE NULL AFTER amount;
