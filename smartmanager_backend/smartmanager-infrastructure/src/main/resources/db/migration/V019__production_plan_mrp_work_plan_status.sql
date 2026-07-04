-- 생산계획: 자재소요·작업계획 상태

ALTER TABLE production_plan
  ADD COLUMN mrp_status ENUM('NOT_CALCULATED','CALCULATED') NOT NULL DEFAULT 'NOT_CALCULATED' AFTER status,
  ADD COLUMN work_plan_status ENUM('NOT_PLANNED','PLANNED') NOT NULL DEFAULT 'NOT_PLANNED' AFTER mrp_status;

CREATE INDEX idx_production_plan_mrp ON production_plan (mrp_status, recording_state);
CREATE INDEX idx_production_plan_work_plan ON production_plan (work_plan_status, recording_state);
