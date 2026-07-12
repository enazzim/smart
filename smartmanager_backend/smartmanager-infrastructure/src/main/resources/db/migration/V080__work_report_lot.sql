-- LOT-3: 작업일보 투입·산출 Lot FK
ALTER TABLE work_report_consumption_line
  ADD COLUMN lot_id BIGINT NULL COMMENT 'lot_tracked 투입 Lot' AFTER source_process_id,
  ADD KEY idx_wr_consumption_lot (lot_id),
  ADD CONSTRAINT fk_wr_consumption_lot FOREIGN KEY (lot_id) REFERENCES inventory_lot (id);

ALTER TABLE work_report
  ADD COLUMN output_lot_id BIGINT NULL COMMENT 'lot_tracked 산출 Lot' AFTER worker_name,
  ADD KEY idx_work_report_output_lot (output_lot_id),
  ADD CONSTRAINT fk_work_report_output_lot FOREIGN KEY (output_lot_id) REFERENCES inventory_lot (id);
