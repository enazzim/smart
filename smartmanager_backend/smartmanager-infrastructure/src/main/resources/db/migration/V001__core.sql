-- =============================================================================
-- V001__core.sql — 코어·공용코드·domain_event
-- Squash of former V001–V003 (chronological concat, 2026-08-01)
-- =============================================================================


-- ---------------------------------------------------------------------------
-- BEGIN former V001__smartmanager_core.sql
-- ---------------------------------------------------------------------------

-- SmartManager Step 1 core (TO-BE) — coexists with legacy tables in kit_erp
-- Ref: docs/step0/d4-public-code.md, d4-company.md, domain-event-projector-matrix.md

CREATE TABLE IF NOT EXISTS domain_event (
  event_id            VARCHAR(36)  NOT NULL PRIMARY KEY,
  event_type          VARCHAR(64)  NOT NULL,
  schema_version      INT          NOT NULL DEFAULT 1,
  aggregate_type      VARCHAR(64)  NOT NULL,
  aggregate_id        VARCHAR(64)  NOT NULL,
  occurred_at         DATETIME(3)  NOT NULL,
  actor_user_id       VARCHAR(64)  NULL,
  payload_json        JSON         NOT NULL,
  INDEX idx_domain_event_type_time (event_type, occurred_at),
  INDEX idx_domain_event_aggregate (aggregate_type, aggregate_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS code_group (
  code_group_key   VARCHAR(64)  NOT NULL PRIMARY KEY,
  large_code       CHAR(4)      NOT NULL,
  large_name       VARCHAR(100) NOT NULL,
  usage_type       VARCHAR(20)  NOT NULL,
  editable_level   ENUM('SYSTEM','SMALL_ONLY','FULL') NOT NULL DEFAULT 'SMALL_ONLY',
  exclude_codes    JSON         NULL,
  description      VARCHAR(255) NULL,
  UNIQUE KEY uk_code_group_large_code (large_code)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS public_code (
  id               BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  large_code       VARCHAR(20)  NOT NULL,
  large_name       VARCHAR(100) NOT NULL,
  small_code       VARCHAR(20)  NULL,
  small_name       VARCHAR(100) NULL,
  usage_type       VARCHAR(20)  NOT NULL,
  recording_state  TINYINT      NOT NULL DEFAULT 1,
  created_by       VARCHAR(100) NULL,
  created_at       DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by       VARCHAR(100) NULL,
  updated_at       DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  INDEX idx_public_code_large (large_code, recording_state),
  INDEX idx_public_code_usage (usage_type, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS company (
  id                    BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  company_name          VARCHAR(200) NOT NULL,
  president_name        VARCHAR(100) NOT NULL,
  business_reg_no       VARCHAR(20)  NOT NULL,
  corporation_reg_no    VARCHAR(20)  NULL,
  business_address      VARCHAR(500) NOT NULL,
  homepage_url          VARCHAR(500) NULL,
  business_type         VARCHAR(100) NULL,
  business_item         VARCHAR(100) NULL,
  telephone             VARCHAR(50)  NULL,
  fax                   VARCHAR(50)  NULL,
  sale_standard_day     TINYINT      NULL,
  bill_approval_standard TINYINT     NULL,
  fix_collect_day_1     TINYINT      NULL,
  contact_name          VARCHAR(100) NULL,
  contact_email         VARCHAR(200) NULL,
  recording_state       TINYINT      NOT NULL DEFAULT 1,
  created_by            VARCHAR(100) NULL,
  created_by_id         VARCHAR(100) NULL,
  created_at            DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by            VARCHAR(100) NULL,
  updated_by_id         VARCHAR(100) NULL,
  updated_at            DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_company_business_reg_no (business_reg_no, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS company_role (
  id          BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  company_id  BIGINT NOT NULL,
  role_type   ENUM('SALES','OUTSOURCE','PURCHASE','COST') NOT NULL,
  UNIQUE KEY uk_company_role (company_id, role_type),
  CONSTRAINT fk_company_role_company FOREIGN KEY (company_id) REFERENCES company(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS partner_ledger_account (
  id                    BIGINT        NOT NULL AUTO_INCREMENT PRIMARY KEY,
  company_id            BIGINT        NOT NULL,
  fiscal_year           SMALLINT      NOT NULL,
  ledger_type           ENUM('SALES','PURCHASE') NOT NULL,
  prior_sale_carryover  DECIMAL(18,2) NOT NULL DEFAULT 0,
  prior_buy_carryover   DECIMAL(18,2) NOT NULL DEFAULT 0,
  recording_state       TINYINT       NOT NULL DEFAULT 1,
  created_by            VARCHAR(100)  NULL,
  created_by_id         VARCHAR(100)  NULL,
  created_at            DATETIME(3)   NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by            VARCHAR(100)  NULL,
  updated_by_id         VARCHAR(100)  NULL,
  updated_at            DATETIME(3)   NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_partner_ledger (company_id, fiscal_year, ledger_type),
  CONSTRAINT fk_partner_ledger_company FOREIGN KEY (company_id) REFERENCES company(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS partner_ledger_monthly (
  id                  BIGINT        NOT NULL AUTO_INCREMENT PRIMARY KEY,
  ledger_account_id   BIGINT        NOT NULL,
  month_num           TINYINT       NOT NULL,
  sale_amount         DECIMAL(18,2) NOT NULL DEFAULT 0,
  purchase_amount     DECIMAL(18,2) NOT NULL DEFAULT 0,
  recording_state     TINYINT       NOT NULL DEFAULT 1,
  created_at          DATETIME(3)   NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_at          DATETIME(3)   NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_partner_ledger_monthly (ledger_account_id, month_num),
  CONSTRAINT fk_partner_ledger_monthly_account FOREIGN KEY (ledger_account_id) REFERENCES partner_ledger_account(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- END former V001__smartmanager_core.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V002__seed_code_group_public_code.sql
-- ---------------------------------------------------------------------------

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
('1400', '공정명', '14000010', '블랭킹/피어싱', 'PROCESS', 'seed'),
('1400', '공정명', '14000020', '벤딩', 'PROCESS', 'seed'),
('1400', '공정명', '14000030', '조립', 'PROCESS', 'seed'),
('1400', '공정명', '14000040', '용접', 'PROCESS', 'seed'),
('1400', '공정명', '14000050', '도장', 'PROCESS', 'seed'),
('1400', '공정명', '14000060', '1차벤딩', 'PROCESS', 'seed'),
('1400', '공정명', '14000070', '2차벤딩', 'PROCESS', 'seed'),
('1400', '공정명', '14000080', '트리밍', 'PROCESS', 'seed'),
('1400', '공정명', '14000090', '절단', 'PROCESS', 'seed'),
('1400', '공정명', '14000100', '샤링', 'PROCESS', 'seed'),
('1400', '공정명', '14000110', '스폿용접', 'PROCESS', 'seed'),
('1400', '공정명', '14000120', 'CO2 용접', 'PROCESS', 'seed'),
('1400', '공정명', '14000130', '전착', 'PROCESS', 'seed'),
('1400', '공정명', '14000140', '포밍', 'PROCESS', 'seed'),
('1400', '공정명', '14000150', '포장', 'PROCESS', 'seed'),
('1400', '공정명', '14000160', '블랭킹', 'PROCESS', 'seed'),
('1400', '공정명', '14000170', '도금', 'PROCESS', 'seed'),
('1400', '공정명', '14000180', '레이저절단', 'PROCESS', 'seed'),
('1400', '공정명', '14000190', '밀링', 'PROCESS', 'seed'),
('1400', '공정명', '14000200', '놋칭', 'PROCESS', 'seed'),
('1400', '공정명', '14000210', '절곡', 'PROCESS', 'seed'),
('1400', '공정명', '14000220', '피어싱', 'PROCESS', 'seed'),
('1400', '공정명', '14000230', 'MCT', 'PROCESS', 'seed'),
('1400', '공정명', '14000240', '고주파열처리', 'PROCESS', 'seed'),
('1400', '공정명', '14000250', '다지기', 'PROCESS', 'seed'),
('1400', '공정명', '14000260', '가상재고', 'PROCESS', 'seed'),
('1400', '공정명', '14000270', '공정없음', 'PROCESS', 'seed'),
('1400', '공정명', '14000280', 'NC가공', 'PROCESS', 'seed'),
('1400', '공정명', '14000290', 'TAP', 'PROCESS', 'seed'),
('1400', '공정명', '14000300', '코킹', 'PROCESS', 'seed'),
('1400', '공정명', '14000310', '드릴링', 'PROCESS', 'seed'),
('1400', '공정명', '14000320', '피막', 'PROCESS', 'seed'),
('1400', '공정명', '14000330', '열처리', 'PROCESS', 'seed'),
('1400', '공정명', '14000340', '연삭', 'PROCESS', 'seed'),
('1400', '공정명', '14000350', '검사', 'PROCESS', 'seed'),
('1400', '공정명', '14000360', '드로잉', 'PROCESS', 'seed'),
('1400', '공정명', '14000370', '쇼트', 'PROCESS', 'seed'),
('1400', '공정명', '14000380', '사상', 'PROCESS', 'seed'),
('1400', '공정명', '14000390', '엠보싱', 'PROCESS', 'seed'),
('1400', '공정명', '14000400', '가접', 'PROCESS', 'seed'),
('1400', '공정명', '14000410', '프레스', 'PROCESS', 'seed'),
('1400', '공정명', '14000420', '놋칭/피어싱', 'PROCESS', 'seed'),
('1400', '공정명', '14000430', '교정', 'PROCESS', 'seed'),
('1400', '공정명', '14000440', '피팅', 'PROCESS', 'seed'),
('1400', '공정명', '14000450', '수압시험', 'PROCESS', 'seed'),
('1400', '공정명', '14000460', '스웨이징', 'PROCESS', 'seed'),
('1400', '공정명', '14000470', '각도절단', 'PROCESS', 'seed'),
('1400', '공정명', '14000480', '블레이징', 'PROCESS', 'seed'),
('1400', '공정명', '14000490', '소재열처리', 'PROCESS', 'seed'),
('1400', '공정명', '14000500', '선삭1', 'PROCESS', 'seed'),
('1400', '공정명', '14000510', '선삭2', 'PROCESS', 'seed'),
('1400', '공정명', '14000520', 'SRB가공', 'PROCESS', 'seed'),
('1400', '공정명', '14000530', '용접1', 'PROCESS', 'seed'),
('1400', '공정명', '14000540', '용접2', 'PROCESS', 'seed'),
('1400', '공정명', '14000550', '조립2', 'PROCESS', 'seed'),
('1400', '공정명', '14000560', '세척', 'PROCESS', 'seed'),
('1400', '공정명', '14000570', '자리파기', 'PROCESS', 'seed'),
('1400', '공정명', '14000580', '용접(콘넥트1)', 'PROCESS', 'seed'),
('1400', '공정명', '14000590', '용접(콘넥트2)', 'PROCESS', 'seed'),
('1400', '공정명', '14000600', '용접(브라켓)', 'PROCESS', 'seed'),
('1400', '공정명', '14000610', '전조', 'PROCESS', 'seed'),
('1400', '공정명', '14000620', '연마', 'PROCESS', 'seed'),
('1400', '공정명', '14000630', '기어 가공', 'PROCESS', 'seed'),
('1400', '공정명', '14000640', '브로치 가공', 'PROCESS', 'seed');

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

-- END former V002__seed_code_group_public_code.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V003__domain_event_event_id_varchar.sql
-- ---------------------------------------------------------------------------

-- Align domain_event.event_id with JPA VARCHAR(36) mapping (was CHAR(36) in early V001)
ALTER TABLE domain_event
  MODIFY COLUMN event_id VARCHAR(36) NOT NULL;

-- END former V003__domain_event_event_id_varchar.sql

