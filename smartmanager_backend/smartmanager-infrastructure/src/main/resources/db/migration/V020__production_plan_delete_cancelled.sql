-- 취소 상태로 남아 있던 생산계획 정리 (취소 = 레코드 삭제 정책으로 전환)

DELETE FROM production_plan
WHERE status = 'CANCELLED';
