-- 품목 마스터: 기종(모델) 필드 추가

ALTER TABLE item
  ADD COLUMN model_type VARCHAR(100) NULL AFTER property_classification;
