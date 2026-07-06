-- 작업지시 취소: 소프트 삭제(recording_state=0) 잔존 행 정리 (UK 충돌 방지)
DELETE wr FROM work_report wr
INNER JOIN work_order wo ON wo.id = wr.work_order_id
WHERE wo.recording_state = 0;

DELETE FROM work_order WHERE recording_state = 0;
