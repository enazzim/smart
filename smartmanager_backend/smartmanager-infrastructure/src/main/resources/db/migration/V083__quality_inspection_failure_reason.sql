-- 품질검사 불량사유(자유 입력)

ALTER TABLE quality_inspection
  ADD COLUMN failure_reason VARCHAR(500) NULL AFTER unsuitability_status_code_id;
