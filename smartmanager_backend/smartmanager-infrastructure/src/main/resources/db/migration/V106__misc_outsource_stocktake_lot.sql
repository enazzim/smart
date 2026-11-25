-- 재고실사 2차. 외주 슬롯 키와 실사 행의 거래처·투입공정·Lot.

ALTER TABLE misc_stock_movement
  ADD COLUMN input_process_id BIGINT NULL AFTER output_process_id,
  ADD COLUMN partner_id BIGINT NULL AFTER input_process_id,
  ADD CONSTRAINT fk_misc_stock_movement_input_process
    FOREIGN KEY (input_process_id) REFERENCES process_sequence (id),
  ADD CONSTRAINT fk_misc_stock_movement_partner
    FOREIGN KEY (partner_id) REFERENCES company (id);

ALTER TABLE stocktake_line
  ADD COLUMN partner_id BIGINT NULL AFTER output_process_id,
  ADD COLUMN input_process_id BIGINT NULL AFTER partner_id,
  ADD COLUMN lot_id BIGINT NULL AFTER input_process_id,
  ADD CONSTRAINT fk_stocktake_line_partner
    FOREIGN KEY (partner_id) REFERENCES company (id),
  ADD CONSTRAINT fk_stocktake_line_input_process
    FOREIGN KEY (input_process_id) REFERENCES process_sequence (id),
  ADD CONSTRAINT fk_stocktake_line_lot
    FOREIGN KEY (lot_id) REFERENCES inventory_lot (id);
