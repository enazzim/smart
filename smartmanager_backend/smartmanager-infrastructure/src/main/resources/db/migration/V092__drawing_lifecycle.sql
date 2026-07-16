-- 도면 lifecycle 단계·거래처 선수신·품목 연결 시각

ALTER TABLE drawing_master
  ADD COLUMN lifecycle_stage ENUM(
    'RECEIVED',
    'SAMPLE',
    'PARTNER_REVIEW',
    'MASS_PROD_READY',
    'ITEM_LINKED',
    'ARCHIVED'
  ) NOT NULL DEFAULT 'RECEIVED' AFTER model_type,
  ADD COLUMN source_partner_id BIGINT NULL AFTER lifecycle_stage,
  ADD COLUMN item_linked_at DATETIME(3) NULL AFTER item_id;

ALTER TABLE drawing_master
  ADD INDEX idx_drawing_source_partner (source_partner_id);

ALTER TABLE drawing_master
  ADD CONSTRAINT fk_drawing_source_partner
    FOREIGN KEY (source_partner_id) REFERENCES company (id);

UPDATE drawing_master dm
INNER JOIN drawing_history dh
  ON dh.drawing_master_id = dm.id AND dh.is_latest = 'Y'
SET dm.lifecycle_stage = CASE
  WHEN dm.item_id IS NOT NULL THEN 'ITEM_LINKED'
  WHEN dh.drawing_type = 'PROD' THEN 'MASS_PROD_READY'
  ELSE 'SAMPLE'
END;

UPDATE drawing_master
SET item_linked_at = COALESCE(updated_at, created_at)
WHERE item_id IS NOT NULL AND item_linked_at IS NULL;
