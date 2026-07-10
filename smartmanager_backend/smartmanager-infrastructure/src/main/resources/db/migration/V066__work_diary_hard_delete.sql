-- 업무일지: 소프트삭제 잔여 데이터 정리 + 물리 삭제 정책용 유니크 키 변경
-- uk_work_diary_entry_author_date_active 는 author_user_id FK가 사용하므로 대체 인덱스를 먼저 추가한다.

DELETE FROM work_diary_entry WHERE recording_state = 0;

CREATE INDEX idx_work_diary_entry_author_user ON work_diary_entry (author_user_id);

ALTER TABLE work_diary_entry
  DROP INDEX uk_work_diary_entry_author_date_active;

ALTER TABLE work_diary_entry
  ADD UNIQUE KEY uk_work_diary_entry_author_date (author_user_id, work_date);
