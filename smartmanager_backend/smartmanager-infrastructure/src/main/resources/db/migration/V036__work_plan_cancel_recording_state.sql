-- 작업계획 취소 건이 uk_work_plan(recording_state=1)에 남아 재수립이 막히는 문제 정리

UPDATE work_plan
SET recording_state = 0
WHERE status = 'CANCELLED'
  AND recording_state = 1;
