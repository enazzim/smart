-- MRP 소요 취소 시 취소된 발주 라인 FK 잔존 방지: ON DELETE SET NULL + 기존 데이터 정리

UPDATE purchase_order_line pol
JOIN purchase_order po ON po.id = pol.purchase_order_id
SET pol.requirement_line_id = NULL
WHERE po.status = 'CANCELLED'
  AND pol.requirement_line_id IS NOT NULL
  AND pol.recording_state = 1;

ALTER TABLE purchase_order_line
  DROP FOREIGN KEY fk_purchase_order_line_requirement;

ALTER TABLE purchase_order_line
  ADD CONSTRAINT fk_purchase_order_line_requirement
    FOREIGN KEY (requirement_line_id) REFERENCES material_requirement_line (id)
    ON DELETE SET NULL;
