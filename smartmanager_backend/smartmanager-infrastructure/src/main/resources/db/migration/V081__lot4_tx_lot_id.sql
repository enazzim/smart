-- LOT-4: TX 라인·기타입출고 lot_id (영업출고·매출·외주출고 투입·외주입고·기타입출고)
ALTER TABLE sales_shipment_line
  ADD COLUMN lot_id BIGINT NULL COMMENT 'lot_tracked 출고 Lot (SALES|WIP → DELIVERY 동일 lot)' AFTER invoiced_qty,
  ADD KEY idx_sales_shipment_line_lot (lot_id),
  ADD CONSTRAINT fk_sales_shipment_line_lot FOREIGN KEY (lot_id) REFERENCES inventory_lot (id);

ALTER TABLE sales_revenue_line
  ADD COLUMN lot_id BIGINT NULL COMMENT 'lot_tracked 매출 Lot (DELIVERY OUT)' AFTER amount,
  ADD KEY idx_sales_revenue_line_lot (lot_id),
  ADD CONSTRAINT fk_sales_revenue_line_lot FOREIGN KEY (lot_id) REFERENCES inventory_lot (id);

ALTER TABLE outsourcing_shipment_input_line
  ADD COLUMN lot_id BIGINT NULL COMMENT 'lot_tracked 투입 Lot (원창고 → OUTSOURCE 동일 lot)' AFTER input_process_id,
  ADD KEY idx_os_shipment_input_lot (lot_id),
  ADD CONSTRAINT fk_os_shipment_input_lot FOREIGN KEY (lot_id) REFERENCES inventory_lot (id);

ALTER TABLE outsourcing_receipt_line
  ADD COLUMN lot_id BIGINT NULL COMMENT 'lot_tracked 외주입고 산출 Lot (WIP|SALES IN)' AFTER amount,
  ADD KEY idx_os_receipt_line_lot (lot_id),
  ADD CONSTRAINT fk_os_receipt_line_lot FOREIGN KEY (lot_id) REFERENCES inventory_lot (id);

ALTER TABLE misc_stock_movement
  ADD COLUMN lot_id BIGINT NULL COMMENT 'lot_tracked 기타입출고 Lot' AFTER output_process_id,
  ADD KEY idx_misc_stock_movement_lot (lot_id),
  ADD CONSTRAINT fk_misc_stock_movement_lot FOREIGN KEY (lot_id) REFERENCES inventory_lot (id);
