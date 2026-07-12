-- 매입마감일(회계월 기준일): 거래일 day <= N → 해당 월, day > N → 익월 회계월

INSERT INTO system_settings (setting_key, value_json, created_by, created_by_id)
VALUES ('closing.fiscal_cutover_day', '"25"', 'system', 'system');
