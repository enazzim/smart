-- DRAFT mirror of V077 (applied in db/migration)
-- item.lot_tracked, item.model_type NOT NULL, drawing_master.model_group → model_type

ALTER TABLE item
  ADD COLUMN lot_tracked TINYINT(1) NOT NULL DEFAULT 0
    COMMENT '1=입출고 시 lot_id 필수'
  AFTER min_order_quantity;

UPDATE item
SET model_type = '-'
WHERE model_type IS NULL OR TRIM(model_type) = '';

ALTER TABLE item
  MODIFY COLUMN model_type VARCHAR(100) NOT NULL
    COMMENT '기종 (도면 model_type 과 동일 개념)';

ALTER TABLE drawing_master
  CHANGE COLUMN model_group model_type VARCHAR(50) NOT NULL
    COMMENT '기종 (item.model_type 과 동일 개념)';

ALTER TABLE drawing_master
  DROP INDEX idx_drawing_model_group,
  ADD INDEX idx_drawing_model_type (model_type, recording_state);
