-- 생산: 자재투입 여부 시스템 설정 (기본 아니오 = 작업일보 등록 시 백플러시)

INSERT INTO system_settings (setting_key, value_json, created_by, created_by_id)
VALUES ('production.material_issue.enabled', '"NO"', 'system', 'system');
