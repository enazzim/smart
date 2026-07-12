-- 도면 마스터 ↔ 품목 마스터 선택 연동

ALTER TABLE drawing_master
  ADD COLUMN item_id BIGINT NULL AFTER model_group;

ALTER TABLE drawing_master
  ADD INDEX idx_drawing_item_id (item_id);

ALTER TABLE drawing_master
  ADD CONSTRAINT fk_drawing_item FOREIGN KEY (item_id) REFERENCES item (id);
