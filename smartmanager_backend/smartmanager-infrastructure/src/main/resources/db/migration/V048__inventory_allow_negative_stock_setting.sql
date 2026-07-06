-- 재고: 마이너스 재고 허용 여부 (기본 예)

INSERT INTO system_settings (setting_key, value_json, created_by, created_by_id)
VALUES ('inventory.allow_negative_stock', '"YES"', 'system', 'system')
ON DUPLICATE KEY UPDATE value_json = VALUES(value_json);
