-- Wave③: 외주 선출고 (발주 연동 없이·발주 있어도 별도 출고)

ALTER TABLE outsourcing_shipment
  ADD COLUMN shipment_type ENUM('ORDER', 'ADVANCE') NOT NULL DEFAULT 'ORDER' AFTER shipment_date,
  ADD COLUMN partner_id BIGINT NULL AFTER shipment_type,
  ADD INDEX idx_outsourcing_shipment_partner (partner_id, recording_state),
  ADD CONSTRAINT fk_outsourcing_shipment_partner FOREIGN KEY (partner_id) REFERENCES company (id);

ALTER TABLE outsourcing_shipment_line
  MODIFY COLUMN outsourcing_order_line_id BIGINT NULL,
  ADD COLUMN parent_item_id BIGINT NULL AFTER outsourcing_order_line_id,
  ADD COLUMN begin_process_code_id BIGINT NULL AFTER parent_item_id,
  ADD COLUMN end_process_code_id BIGINT NULL AFTER begin_process_code_id,
  ADD CONSTRAINT fk_osl_parent_item FOREIGN KEY (parent_item_id) REFERENCES item (id);
