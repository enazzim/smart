-- WIP 창고 표시명: 생산창고 → 공정창고 (UI 표준)

UPDATE inventory_location
SET location_name = '공정창고'
WHERE location_code = 'WIP';
