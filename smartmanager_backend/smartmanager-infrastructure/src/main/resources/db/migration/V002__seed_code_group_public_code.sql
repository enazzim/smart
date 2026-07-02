-- Seed code_group + public_code headers (Step 0 data/puc-large-distinct.csv)

INSERT INTO code_group (code_group_key, large_code, large_name, usage_type, editable_level, exclude_codes, description) VALUES
('ITEM_CLASSIFICATION_1', '0210', '품목분류1', 'GENERIC', 'SMALL_ONLY', NULL, NULL),
('ITEM_CLASSIFICATION_2', '0220', '품목분류2', 'GENERIC', 'SMALL_ONLY', NULL, NULL),
('ITEM_CLASSIFICATION_3', '0230', '품목분류3', 'GENERIC', 'SMALL_ONLY', NULL, NULL),
('ITEM_CLASSIFICATION_4', '0240', '품목분류4', 'GENERIC', 'SMALL_ONLY', NULL, NULL),
('COMPANY_TRADE_CLASS_1', '0110', '거래처분류1', 'GENERIC', 'SMALL_ONLY', NULL, 'hypothesis — DB 검증'),
('COMPANY_TRADE_CLASS_2', '0120', '거래처분류2', 'GENERIC', 'SMALL_ONLY', NULL, 'hypothesis'),
('COMPANY_TRADE_CLASS_3', '0130', '거래처분류3', 'GENERIC', 'SMALL_ONLY', NULL, 'hypothesis'),
('UNIT_GENERAL', '0400', '단위', 'UNIT', 'SMALL_ONLY', NULL, NULL),
('UNIT_SPEC_1', '0410', '규격단위1', 'UNIT', 'SMALL_ONLY', NULL, NULL),
('UNIT_SPEC_2', '0420', '규격단위2', 'UNIT', 'SMALL_ONLY', NULL, NULL),
('UNIT_SPEC_3', '0430', '규격단위3', 'UNIT', 'SMALL_ONLY', NULL, NULL),
('UNIT_STOCK', '0510', '재고단위', 'UNIT', 'SMALL_ONLY', NULL, NULL),
('UNIT_BOM', '0520', 'BOM단위', 'UNIT', 'SMALL_ONLY', NULL, NULL),
('UNIT_PURCHASE', '0530', '구매단위', 'UNIT', 'SMALL_ONLY', NULL, NULL),
('UNIT_SALE', '0540', '판매단위', 'UNIT', 'SMALL_ONLY', NULL, NULL),
('ITEM_TYPE', '0610', '품목타입', 'GENERIC', 'SMALL_ONLY', NULL, NULL),
('ITEM_MATERIAL', '0620', '재질', 'GENERIC', 'SMALL_ONLY', NULL, NULL),
('ITEM_STATE', '0630', '품목상태', 'GENERIC', 'SMALL_ONLY', JSON_ARRAY('06300010'), NULL),
('BOM_SUPPLY_DIVISION', '0640', '조달구분', 'GENERIC', 'SMALL_ONLY', NULL, NULL),
('PROCESS_CODE', '1400', '공정명', 'PROCESS', 'SMALL_ONLY', JSON_ARRAY('14000000','14009999'), NULL),
('EQUIPMENT_CLASS', '0720', '설비분류', 'GENERIC', 'SMALL_ONLY', JSON_ARRAY('07200010','07200020'), NULL),
('QC_DEFECT_CAUSE', '1010', '부적합원인', 'NC_REASON', 'SMALL_ONLY', NULL, NULL),
('QC_DEFECT_PHENOMENON', '1000', '부적합현상', 'NC_DETAIL', 'SMALL_ONLY', NULL, NULL),
('QC_INSPECTION_DECISION', '1310', '검사판정', 'GENERIC', 'SMALL_ONLY', NULL, NULL),
('PAYMENT_PLAN_TYPE', '0910', '지급계획', 'GENERIC', 'SMALL_ONLY', NULL, NULL),
('PAYMENT_RESULT_TYPE', '0920', '지급실적', 'GENERIC', 'SMALL_ONLY', NULL, NULL),
('WORK_DIARY_GROUP', '1900', '업무일지그룹', 'WORK_DIARY_GROUP', 'SMALL_ONLY', NULL, NULL)
ON DUPLICATE KEY UPDATE large_name = VALUES(large_name), usage_type = VALUES(usage_type);

INSERT INTO public_code (large_code, large_name, small_code, small_name, usage_type, created_by) VALUES
('0210', '품목분류1', NULL, NULL, 'GENERIC', 'seed'),
('0220', '품목분류2', NULL, NULL, 'GENERIC', 'seed'),
('0230', '품목분류3', NULL, NULL, 'GENERIC', 'seed'),
('0240', '품목분류4', NULL, NULL, 'GENERIC', 'seed'),
('0110', '거래처분류1', NULL, NULL, 'GENERIC', 'seed'),
('0120', '거래처분류2', NULL, NULL, 'GENERIC', 'seed'),
('0130', '거래처분류3', NULL, NULL, 'GENERIC', 'seed'),
('0400', '단위', NULL, NULL, 'UNIT', 'seed'),
('1400', '공정명', NULL, NULL, 'PROCESS', 'seed'),
('0720', '설비분류', NULL, NULL, 'GENERIC', 'seed'),
('0640', '조달구분', NULL, NULL, 'GENERIC', 'seed'),
('1900', '업무일지그룹', NULL, NULL, 'WORK_DIARY_GROUP', 'seed');

INSERT INTO public_code (large_code, large_name, small_code, small_name, usage_type, created_by) VALUES
('1400', '공정명', '14000010', '절단', 'PROCESS', 'seed'),
('1400', '공정명', '14000020', '성형', 'PROCESS', 'seed'),
('1400', '공정명', '14000030', '용접', 'PROCESS', 'seed');

INSERT INTO public_code (large_code, large_name, small_code, small_name, usage_type, created_by) VALUES
('1400', '공정명', '14000000', '소재공정', 'PROCESS', 'seed'),
('1400', '공정명', '14009999', '최종공정', 'PROCESS', 'seed');

INSERT INTO public_code (large_code, large_name, small_code, small_name, usage_type, created_by) VALUES
('1900', '업무일지그룹', '19000010', '00 그룹', 'WORK_DIARY_GROUP', 'seed'),
('1900', '업무일지그룹', '19000020', '01 그룹', 'WORK_DIARY_GROUP', 'seed'),
('1900', '업무일지그룹', '19000030', '02 그룹', 'WORK_DIARY_GROUP', 'seed'),
('1900', '업무일지그룹', '19000040', '03 그룹', 'WORK_DIARY_GROUP', 'seed'),
('1900', '업무일지그룹', '19000050', '04 그룹', 'WORK_DIARY_GROUP', 'seed'),
('1900', '업무일지그룹', '19000060', '05 그룹', 'WORK_DIARY_GROUP', 'seed'),
('1900', '업무일지그룹', '19000070', '06 그룹', 'WORK_DIARY_GROUP', 'seed'),
('1900', '업무일지그룹', '19000080', '07 그룹', 'WORK_DIARY_GROUP', 'seed'),
('1900', '업무일지그룹', '19000090', '08 그룹', 'WORK_DIARY_GROUP', 'seed'),
('1900', '업무일지그룹', '19000100', '09 그룹', 'WORK_DIARY_GROUP', 'seed'),
('1900', '업무일지그룹', '19000110', '10 그룹', 'WORK_DIARY_GROUP', 'seed'),
('1900', '업무일지그룹', '19000120', '11 그룹', 'WORK_DIARY_GROUP', 'seed'),
('1900', '업무일지그룹', '19000130', '12 그룹', 'WORK_DIARY_GROUP', 'seed'),
('1900', '업무일지그룹', '19000140', '13 그룹', 'WORK_DIARY_GROUP', 'seed'),
('1900', '업무일지그룹', '19000150', '14 그룹', 'WORK_DIARY_GROUP', 'seed'),
('1900', '업무일지그룹', '19000160', '15 그룹', 'WORK_DIARY_GROUP', 'seed'),
('1900', '업무일지그룹', '19000170', '16 그룹', 'WORK_DIARY_GROUP', 'seed');
