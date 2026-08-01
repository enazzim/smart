-- =============================================================================
-- V002__basis_auth_calendar.sql — 기준정보·인증·달력·마감
-- Squash of former V004–V014 (chronological concat, 2026-08-01)
-- =============================================================================


-- ---------------------------------------------------------------------------
-- BEGIN former V004__item.sql
-- ---------------------------------------------------------------------------

-- Ref: docs/step0/d4-item.md v0.2

CREATE TABLE IF NOT EXISTS item (
  id                      BIGINT        NOT NULL AUTO_INCREMENT PRIMARY KEY,
  item_no                 VARCHAR(50)   NOT NULL,
  item_name               VARCHAR(200)  NOT NULL,
  property_classification ENUM('원자재','제품','상품','공정품') NOT NULL,
  unit                    VARCHAR(20)   NOT NULL,
  standard                VARCHAR(200)  NULL,
  standard_unit_cost      DECIMAL(18,2) NULL,
  check_distinction       ENUM('NONE','INSPECTION') NULL,
  lead_time               INT           NULL,
  safety_stock_quantity   DECIMAL(18,4) NULL,
  order_interval_quantity DECIMAL(18,4) NULL,
  min_order_quantity      DECIMAL(18,4) NULL,
  recording_state         TINYINT       NOT NULL DEFAULT 1,
  created_by              VARCHAR(100)  NULL,
  created_by_id           VARCHAR(100)  NULL,
  created_at              DATETIME(3)   NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by              VARCHAR(100)  NULL,
  updated_by_id           VARCHAR(100)  NULL,
  updated_at              DATETIME(3)   NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_item_no (item_no, recording_state),
  INDEX idx_item_name (item_name, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- END former V004__item.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V005__process_inventory.sql
-- ---------------------------------------------------------------------------

-- Ref: docs/step0/d4-process.md, inventory-ledger-spec.md, d4-work-center.md

CREATE TABLE IF NOT EXISTS inventory_location (
  id            BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  location_code VARCHAR(20)  NOT NULL,
  location_name VARCHAR(100) NOT NULL,
  UNIQUE KEY uk_inventory_location_code (location_code)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO inventory_location (location_code, location_name) VALUES
('RAW', '원자재창고'),
('SALES', '영업창고'),
('DELIVERY', '납품창고'),
('WIP', '공정창고'),
('OUTSOURCE', '외주창고')
ON DUPLICATE KEY UPDATE location_name = VALUES(location_name);

CREATE TABLE IF NOT EXISTS work_center (
  id                     BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  wc_name                VARCHAR(100) NOT NULL,
  main_process_code_id   BIGINT       NOT NULL,
  operation_time         INT          NOT NULL DEFAULT 480,
  retention_staff        INT          NOT NULL DEFAULT 1,
  capacity_distinction   VARCHAR(20)  NOT NULL DEFAULT 'TIME',
  recording_state        TINYINT      NOT NULL DEFAULT 1,
  created_by             VARCHAR(100) NULL,
  created_by_id          VARCHAR(100) NULL,
  created_at             DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by             VARCHAR(100) NULL,
  updated_by_id          VARCHAR(100) NULL,
  updated_at             DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_work_center_name (wc_name, recording_state),
  CONSTRAINT fk_work_center_process_code FOREIGN KEY (main_process_code_id) REFERENCES public_code(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO work_center (wc_name, main_process_code_id, operation_time, created_by, created_by_id, created_at, updated_by, updated_by_id, updated_at)
SELECT '절단라인', pc.id, 480, 'seed', 'seed', CURRENT_TIMESTAMP(3), 'seed', 'seed', CURRENT_TIMESTAMP(3)
FROM public_code pc
WHERE pc.small_code = '14000010' AND pc.recording_state = 1
LIMIT 1;

CREATE TABLE IF NOT EXISTS process_sequence (
  id                   BIGINT      NOT NULL AUTO_INCREMENT PRIMARY KEY,
  item_id              BIGINT      NOT NULL,
  process_sequence     SMALLINT    NOT NULL,
  public_code_id       BIGINT      NOT NULL,
  work_distinction     ENUM('INHOUSE','OUTSOURCE','SPLIT') NOT NULL,
  work_center_id       BIGINT      NULL,
  outside_order_rate   TINYINT     NOT NULL DEFAULT 0,
  progress_rate        SMALLINT    NOT NULL DEFAULT 100,
  lead_time            INT         NULL,
  etc_text             VARCHAR(500) NULL,
  variant              ENUM('plan','actual') NOT NULL DEFAULT 'plan',
  recording_state      TINYINT     NOT NULL DEFAULT 1,
  created_by           VARCHAR(100) NULL,
  created_by_id        VARCHAR(100) NULL,
  created_at           DATETIME(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by           VARCHAR(100) NULL,
  updated_by_id        VARCHAR(100) NULL,
  updated_at           DATETIME(3) NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_process_sequence (item_id, public_code_id, process_sequence, recording_state),
  CONSTRAINT fk_process_sequence_item FOREIGN KEY (item_id) REFERENCES item(id),
  CONSTRAINT fk_process_sequence_public_code FOREIGN KEY (public_code_id) REFERENCES public_code(id),
  CONSTRAINT fk_process_sequence_work_center FOREIGN KEY (work_center_id) REFERENCES work_center(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS inventory_balance (
  id                  BIGINT  NOT NULL AUTO_INCREMENT PRIMARY KEY,
  item_id             BIGINT  NOT NULL,
  location_id         BIGINT  NOT NULL,
  fiscal_year         SMALLINT NOT NULL,
  output_process_id   BIGINT  NULL,
  partner_id          BIGINT  NULL,
  recording_state     TINYINT NOT NULL DEFAULT 1,
  created_by          VARCHAR(100) NULL,
  created_by_id       VARCHAR(100) NULL,
  created_at          DATETIME(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by          VARCHAR(100) NULL,
  updated_by_id       VARCHAR(100) NULL,
  updated_at          DATETIME(3) NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_inventory_balance (item_id, location_id, fiscal_year, output_process_id, partner_id, recording_state),
  CONSTRAINT fk_inventory_balance_item FOREIGN KEY (item_id) REFERENCES item(id),
  CONSTRAINT fk_inventory_balance_location FOREIGN KEY (location_id) REFERENCES inventory_location(id),
  CONSTRAINT fk_inventory_balance_process FOREIGN KEY (output_process_id) REFERENCES process_sequence(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- END former V005__process_inventory.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V006__unit_price.sql
-- ---------------------------------------------------------------------------

-- Ref: docs/step0/d4-unit-price.md, domain-event-projector-matrix.md §4.5

ALTER TABLE inventory_balance
  ADD COLUMN input_process_id BIGINT NULL AFTER output_process_id,
  ADD CONSTRAINT fk_inventory_balance_input_process
    FOREIGN KEY (input_process_id) REFERENCES process_sequence(id);

CREATE TABLE IF NOT EXISTS unit_price (
  id                        BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  cost_type                 ENUM('SALE','PURCHASE','OUTSOURCE') NOT NULL,
  item_id                   BIGINT         NOT NULL,
  company_id                BIGINT         NOT NULL,
  begin_process_code_id     BIGINT         NULL,
  end_process_code_id       BIGINT         NULL,
  order_rate                DECIMAL(5,2)   NOT NULL DEFAULT 0,
  standard_unit_cost        DECIMAL(18,4)  NOT NULL,
  discount_unit_cost        DECIMAL(18,4)  NULL,
  begin_date                DATE           NOT NULL,
  end_date                  DATE           NULL,
  recording_state           TINYINT        NOT NULL DEFAULT 1,
  created_by                VARCHAR(100)   NULL,
  created_by_id             VARCHAR(100)   NULL,
  created_at                DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by                VARCHAR(100)   NULL,
  updated_by_id             VARCHAR(100)   NULL,
  updated_at                DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_unit_price_active (
    cost_type, item_id, company_id, begin_date,
    begin_process_code_id, end_process_code_id, recording_state
  ),
  CONSTRAINT fk_unit_price_item FOREIGN KEY (item_id) REFERENCES item(id),
  CONSTRAINT fk_unit_price_company FOREIGN KEY (company_id) REFERENCES company(id),
  CONSTRAINT fk_unit_price_begin_process FOREIGN KEY (begin_process_code_id) REFERENCES public_code(id),
  CONSTRAINT fk_unit_price_end_process FOREIGN KEY (end_process_code_id) REFERENCES public_code(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS unit_price_change_log (
  id                        BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  unit_price_id             BIGINT         NOT NULL,
  cost_type                 ENUM('SALE','PURCHASE','OUTSOURCE') NOT NULL,
  item_id                   BIGINT         NOT NULL,
  company_id                BIGINT         NOT NULL,
  begin_process_code_id     BIGINT         NULL,
  end_process_code_id       BIGINT         NULL,
  order_rate                DECIMAL(5,2)   NOT NULL,
  standard_unit_cost        DECIMAL(18,4)  NOT NULL,
  discount_unit_cost        DECIMAL(18,4)  NULL,
  begin_date                DATE           NOT NULL,
  end_date                  DATE           NULL,
  update_reason             VARCHAR(500)   NOT NULL,
  changed_by                VARCHAR(100)   NULL,
  changed_by_id             VARCHAR(100)   NULL,
  changed_at                DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  CONSTRAINT fk_unit_price_change_log_unit_price FOREIGN KEY (unit_price_id) REFERENCES unit_price(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- END former V006__unit_price.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V007__item_composition.sql
-- ---------------------------------------------------------------------------

-- Ref: docs/step0/d4-bom-line.md v0.1

CREATE TABLE IF NOT EXISTS item_composition (
  id                          BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  parent_item_id              BIGINT         NOT NULL,
  child_item_id               BIGINT         NOT NULL,
  need_quantity_denominator   DECIMAL(18,4)  NOT NULL,
  need_quantity_numerator     DECIMAL(18,4)  NOT NULL,
  process_management          TINYINT        NOT NULL DEFAULT 0,
  sub_division                VARCHAR(50)    NULL,
  supply_division             VARCHAR(50)    NULL,
  bom_unit                    VARCHAR(20)    NULL,
  begin_date                  DATE           NOT NULL,
  end_date                    DATE           NULL,
  recording_state             TINYINT        NOT NULL DEFAULT 1,
  created_by                  VARCHAR(100)   NULL,
  created_by_id               VARCHAR(100)   NULL,
  created_at                  DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by                  VARCHAR(100)   NULL,
  updated_by_id               VARCHAR(100)   NULL,
  updated_at                  DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_item_composition (parent_item_id, child_item_id, recording_state),
  CONSTRAINT fk_item_composition_parent FOREIGN KEY (parent_item_id) REFERENCES item(id),
  CONSTRAINT fk_item_composition_child FOREIGN KEY (child_item_id) REFERENCES item(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS bom_change_log (
  id                          BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  item_composition_id         BIGINT         NOT NULL,
  parent_item_id              BIGINT         NOT NULL,
  child_item_id               BIGINT         NOT NULL,
  need_quantity_denominator   DECIMAL(18,4)  NOT NULL,
  need_quantity_numerator     DECIMAL(18,4)  NOT NULL,
  change_reason               VARCHAR(50)    NOT NULL,
  changed_by                  VARCHAR(100)   NULL,
  changed_by_id               VARCHAR(100)   NULL,
  changed_at                  DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  CONSTRAINT fk_bom_change_log_composition FOREIGN KEY (item_composition_id) REFERENCES item_composition(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- END former V007__item_composition.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V008__work_standard.sql
-- ---------------------------------------------------------------------------

-- Ref: docs/step0/d4-work-standard.md v0.1

CREATE TABLE IF NOT EXISTS work_standard (
  id                    BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  item_id               BIGINT       NOT NULL,
  process_sequence_id   BIGINT       NOT NULL,
  work_center_id        BIGINT       NOT NULL,
  equipment_id          BIGINT       NULL,
  priority_order        INT          NOT NULL DEFAULT 1,
  main_worker_user_id   BIGINT       NULL,
  tool_name             VARCHAR(200) NULL,
  setup_time            INT          NOT NULL DEFAULT 0,
  standard_time         INT          NOT NULL DEFAULT 0,
  recording_state       TINYINT      NOT NULL DEFAULT 1,
  created_by            VARCHAR(100) NULL,
  created_by_id         VARCHAR(100) NULL,
  created_at            DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by            VARCHAR(100) NULL,
  updated_by_id         VARCHAR(100) NULL,
  updated_at            DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_work_standard (item_id, process_sequence_id, priority_order, recording_state),
  CONSTRAINT fk_work_standard_item FOREIGN KEY (item_id) REFERENCES item (id),
  CONSTRAINT fk_work_standard_process FOREIGN KEY (process_sequence_id) REFERENCES process_sequence (id),
  CONSTRAINT fk_work_standard_work_center FOREIGN KEY (work_center_id) REFERENCES work_center (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- END former V008__work_standard.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V009__equipment.sql
-- ---------------------------------------------------------------------------

-- Ref: docs/step0/d4-equipment.md v0.1

INSERT INTO public_code (large_code, large_name, small_code, small_name, usage_type, created_by) VALUES
('0720', '설비분류', '07200030', '사출기', 'GENERIC', 'seed'),
('0720', '설비분류', '07200040', '프레스', 'GENERIC', 'seed'),
('0720', '설비분류', '07200050', 'CNC', 'GENERIC', 'seed')
ON DUPLICATE KEY UPDATE small_name = VALUES(small_name);

CREATE TABLE IF NOT EXISTS equipment (
  id                      BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  equipment_num           VARCHAR(50)  NOT NULL,
  equipment_name          VARCHAR(200) NOT NULL,
  equipment_category_id   BIGINT       NOT NULL,
  work_center_id          BIGINT       NULL,
  design_shot             INT          NOT NULL DEFAULT 0,
  initial_shot            INT          NOT NULL DEFAULT 0,
  work_shot               INT          NOT NULL DEFAULT 0,
  accumulated_shot        INT          NOT NULL DEFAULT 0,
  recording_state         TINYINT      NOT NULL DEFAULT 1,
  created_by              VARCHAR(100) NULL,
  created_by_id           VARCHAR(100) NULL,
  created_at              DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by              VARCHAR(100) NULL,
  updated_by_id             VARCHAR(100) NULL,
  updated_at              DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_equipment_num (equipment_num, recording_state),
  CONSTRAINT fk_equipment_category FOREIGN KEY (equipment_category_id) REFERENCES public_code (id),
  CONSTRAINT fk_equipment_work_center FOREIGN KEY (work_center_id) REFERENCES work_center (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

ALTER TABLE work_standard
  ADD CONSTRAINT fk_work_standard_equipment FOREIGN KEY (equipment_id) REFERENCES equipment (id);

-- END former V009__equipment.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V010__user.sql
-- ---------------------------------------------------------------------------

-- Ref: docs/step0/d4-user.md v0.1

CREATE TABLE IF NOT EXISTS role (
  id                BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  role_code         VARCHAR(64)  NOT NULL,
  role_name         VARCHAR(100) NOT NULL,
  recording_state   TINYINT      NOT NULL DEFAULT 1,
  created_at        DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_role_code (role_code, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS permission (
  id                BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  permission_code   VARCHAR(128) NOT NULL,
  description       VARCHAR(255) NULL,
  recording_state   TINYINT      NOT NULL DEFAULT 1,
  UNIQUE KEY uk_permission_code (permission_code, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS role_permission (
  role_id           BIGINT NOT NULL,
  permission_id     BIGINT NOT NULL,
  PRIMARY KEY (role_id, permission_id),
  CONSTRAINT fk_role_permission_role FOREIGN KEY (role_id) REFERENCES role (id),
  CONSTRAINT fk_role_permission_permission FOREIGN KEY (permission_id) REFERENCES permission (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `user` (
  id                    BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  login_id              VARCHAR(100) NOT NULL,
  password_hash         VARCHAR(255) NOT NULL,
  name                  VARCHAR(100) NOT NULL,
  contact               VARCHAR(50)  NULL,
  email                 VARCHAR(200) NULL,
  work_diary_group_id   BIGINT       NULL,
  recording_state       TINYINT      NOT NULL DEFAULT 1,
  created_by            VARCHAR(100) NULL,
  created_by_id         VARCHAR(100) NULL,
  created_at            DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by            VARCHAR(100) NULL,
  updated_by_id         VARCHAR(100) NULL,
  updated_at            DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_user_login_id (login_id, recording_state),
  CONSTRAINT fk_user_work_diary_group FOREIGN KEY (work_diary_group_id) REFERENCES public_code (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS user_role (
  user_id BIGINT NOT NULL,
  role_id BIGINT NOT NULL,
  PRIMARY KEY (user_id, role_id),
  CONSTRAINT fk_user_role_user FOREIGN KEY (user_id) REFERENCES `user` (id),
  CONSTRAINT fk_user_role_role FOREIGN KEY (role_id) REFERENCES role (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

ALTER TABLE work_standard
  ADD CONSTRAINT fk_work_standard_main_worker FOREIGN KEY (main_worker_user_id) REFERENCES `user` (id);

INSERT INTO role (role_code, role_name) VALUES
('SYSTEM_ADMIN', '시스템 관리자'),
('BASIS_MANAGER', '기준정보 관리자'),
('PURCHASE_OPERATOR', '구매 담당'),
('PRODUCTION_OPERATOR', '생산 담당'),
('SALES_OPERATOR', '영업 담당'),
('VIEWER', '조회 전용')
ON DUPLICATE KEY UPDATE role_name = VALUES(role_name);

-- END former V010__user.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V011__production_calendar.sql
-- ---------------------------------------------------------------------------

-- Ref: docs/step0/d4-calendar.md v0.1

CREATE TABLE IF NOT EXISTS production_calendar (
  id                BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  calendar_date     DATE         NOT NULL,
  work_time         INT          NOT NULL DEFAULT 480,
  content           VARCHAR(255) NULL,
  recording_state   TINYINT      NOT NULL DEFAULT 1,
  created_by        VARCHAR(100) NULL,
  created_by_id     VARCHAR(100) NULL,
  created_at        DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by        VARCHAR(100) NULL,
  updated_by_id     VARCHAR(100) NULL,
  updated_at        DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_production_calendar_date (calendar_date, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS work_center_calendar (
  id                BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  work_center_id    BIGINT       NOT NULL,
  calendar_date     DATE         NOT NULL,
  work_time         INT          NOT NULL,
  content           VARCHAR(255) NULL,
  recording_state   TINYINT      NOT NULL DEFAULT 1,
  created_by        VARCHAR(100) NULL,
  created_by_id     VARCHAR(100) NULL,
  created_at        DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by        VARCHAR(100) NULL,
  updated_by_id     VARCHAR(100) NULL,
  updated_at        DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_work_center_calendar (work_center_id, calendar_date, recording_state),
  CONSTRAINT fk_work_center_calendar_wc FOREIGN KEY (work_center_id) REFERENCES work_center (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- END former V011__production_calendar.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V012__auth_permissions_admin.sql
-- ---------------------------------------------------------------------------

-- S0: RBAC permissions and role mappings

INSERT INTO permission (permission_code, description) VALUES
('system:public-code:read', '공용코드 조회'),
('system:public-code:write', '공용코드 등록·수정·삭제'),
('system:tenant:read', '기업정보 조회'),
('system:tenant:write', '기업정보 수정'),
('system:settings:read', '시스템 설정 조회'),
('system:settings:write', '시스템 설정 수정'),
('system:role:read', '역할 조회'),
('system:role:write', '역할·권한 관리'),
('system:import:execute', '초기정보 일괄입력'),
('system:month-closing:execute', '월마감 실행'),
('basis:company:read', '거래처 조회'),
('basis:company:write', '거래처 등록·수정·삭제'),
('basis:item:read', '품목 조회'),
('basis:item:write', '품목 등록·수정·삭제'),
('basis:process:read', '공정 조회'),
('basis:process:write', '공정 등록·수정·삭제'),
('basis:unit-price:read', '단가 조회'),
('basis:unit-price:write', '단가 등록·수정·삭제'),
('basis:work-center:read', '작업장 조회'),
('basis:work-center:write', '작업장 등록·수정·삭제'),
('basis:work-standard:read', '작업표준 조회'),
('basis:work-standard:write', '작업표준 등록·수정·삭제'),
('basis:equipment:read', '설비 조회'),
('basis:equipment:write', '설비 등록·수정·삭제'),
('basis:user:read', '사용자 조회'),
('basis:user:write', '사용자 등록·수정·삭제'),
('basis:production-calendar:read', '생산달력 조회'),
('basis:production-calendar:write', '생산달력 등록·수정·삭제'),
('basis:public-code:read', '공용코드(기준) 조회'),
('purchase:receipt:read', '구매입고 조회'),
('purchase:receipt:post', '구매입고 전기')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code LIKE '%:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND (p.permission_code LIKE 'basis:%'
       OR p.permission_code IN ('system:public-code:read', 'system:public-code:write', 'system:role:read'))
WHERE r.role_code = 'BASIS_MANAGER' AND r.recording_state = 1;

-- END former V012__auth_permissions_admin.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V013__public_holiday.sql
-- ---------------------------------------------------------------------------

CREATE TABLE IF NOT EXISTS public_holiday (
  holiday_date DATE         NOT NULL PRIMARY KEY,
  holiday_name VARCHAR(100) NOT NULL,
  created_at   TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- 2026년 대한민국 공휴일 (주말·공휴일 자동 휴무용)
INSERT INTO public_holiday (holiday_date, holiday_name) VALUES
  ('2026-01-01', '신정'),
  ('2026-02-16', '설날 연휴'),
  ('2026-02-17', '설날'),
  ('2026-02-18', '설날 연휴'),
  ('2026-03-01', '삼일절'),
  ('2026-03-02', '삼일절 대체공휴일'),
  ('2026-05-05', '어린이날'),
  ('2026-05-24', '부처님오신날'),
  ('2026-06-06', '현충일'),
  ('2026-08-15', '광복절'),
  ('2026-08-17', '광복절 대체공휴일'),
  ('2026-09-24', '추석 연휴'),
  ('2026-09-25', '추석'),
  ('2026-09-26', '추석 연휴'),
  ('2026-10-03', '개천절'),
  ('2026-10-05', '개천절 대체공휴일'),
  ('2026-10-09', '한글날'),
  ('2026-12-25', '크리스마스');

-- END former V013__public_holiday.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V014__month_closing.sql
-- ---------------------------------------------------------------------------

-- INF-2: 회계월 마감 (month_closing)

CREATE TABLE IF NOT EXISTS month_closing (
  id              BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  fiscal_year     SMALLINT     NOT NULL,
  fiscal_month    TINYINT      NOT NULL,
  closed_at       DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  closed_by       VARCHAR(100) NOT NULL,
  closed_by_id    VARCHAR(100) NULL,
  recording_state TINYINT      NOT NULL DEFAULT 1,
  UNIQUE KEY uk_month_closing_period (fiscal_year, fiscal_month, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('system:month-closing:read', '월마감 조회')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'system:month-closing:read'
WHERE r.role_code IN ('SYSTEM_ADMIN', 'BASIS_MANAGER') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'system:month-closing:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;

-- END former V014__month_closing.sql

