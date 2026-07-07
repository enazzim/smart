-- 작성자 양식에서 지시사항(레거시 필드 06) 제거 — 결재자 directive_note 전용
UPDATE work_diary_template
SET field_schema = JSON_REMOVE(field_schema, '$.legacyFields."06"'),
    updated_at = CURRENT_TIMESTAMP(3)
WHERE recording_state = 1
  AND JSON_EXTRACT(field_schema, '$.legacyFields."06"') IS NOT NULL;
