-- =============================================================================
-- V005__work_inventory.sql — 작업계획·지시·일보·자재·재고권한
-- Squash of former V034–V045 (chronological concat, 2026-08-01)
-- =============================================================================


-- ---------------------------------------------------------------------------
-- BEGIN former V034__work_plan.sql
-- ---------------------------------------------------------------------------

-- PRD-W1a: 작업계획 원장

CREATE TABLE IF NOT EXISTS work_plan (
  id                    BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  production_plan_id    BIGINT         NOT NULL,
  process_sequence_id   BIGINT         NOT NULL,
  work_center_id        BIGINT         NULL,
  work_distinction      ENUM('INHOUSE','OUTSOURCE') NOT NULL,
  planned_qty           DECIMAL(18, 4) NOT NULL,
  plan_start_date       DATE           NULL,
  plan_end_date         DATE           NULL,
  setup_time            INT            NOT NULL DEFAULT 0,
  standard_time         INT            NOT NULL DEFAULT 0,
  status                ENUM('PLANNED','CANCELLED') NOT NULL DEFAULT 'PLANNED',
  recording_state       TINYINT        NOT NULL DEFAULT 1,
  created_by            VARCHAR(100)   NULL,
  created_by_id         VARCHAR(100)   NULL,
  created_at            DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by            VARCHAR(100)   NULL,
  updated_by_id         VARCHAR(100)   NULL,
  updated_at            DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_work_plan (production_plan_id, process_sequence_id, work_distinction, recording_state),
  INDEX idx_work_plan_plan (production_plan_id, recording_state),
  INDEX idx_work_plan_wc (work_center_id, plan_start_date, recording_state),
  CONSTRAINT fk_work_plan_production_plan FOREIGN KEY (production_plan_id) REFERENCES production_plan (id),
  CONSTRAINT fk_work_plan_process FOREIGN KEY (process_sequence_id) REFERENCES process_sequence (id),
  CONSTRAINT fk_work_plan_work_center FOREIGN KEY (work_center_id) REFERENCES work_center (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('production:work-plan:read', '작업계획 조회'),
('production:work-plan:write', '작업계획 수립')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('production:work-plan:read', 'production:work-plan:write')
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'production:work-plan:read'
WHERE r.role_code = 'PRODUCTION_OPERATOR' AND r.recording_state = 1;

-- END former V034__work_plan.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V035__purchase_order_line_requirement_on_delete_set_null.sql
-- ---------------------------------------------------------------------------

-- MRP 소요 취소 시 취소된 발주 라인 FK 잔존 방지: ON DELETE SET NULL + 기존 데이터 정리

UPDATE purchase_order_line pol
JOIN purchase_order po ON po.id = pol.purchase_order_id
SET pol.requirement_line_id = NULL
WHERE po.status = 'CANCELLED'
  AND pol.requirement_line_id IS NOT NULL
  AND pol.recording_state = 1;

ALTER TABLE purchase_order_line
  DROP FOREIGN KEY fk_purchase_order_line_requirement;

ALTER TABLE purchase_order_line
  ADD CONSTRAINT fk_purchase_order_line_requirement
    FOREIGN KEY (requirement_line_id) REFERENCES material_requirement_line (id)
    ON DELETE SET NULL;

-- END former V035__purchase_order_line_requirement_on_delete_set_null.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V036__work_plan_cancel_recording_state.sql
-- ---------------------------------------------------------------------------

-- 작업계획 취소 건이 uk_work_plan(recording_state=1)에 남아 재수립이 막히는 문제 정리

UPDATE work_plan
SET recording_state = 0
WHERE status = 'CANCELLED'
  AND recording_state = 1;

-- END former V036__work_plan_cancel_recording_state.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V037__scheduling_permissions.sql
-- ---------------------------------------------------------------------------

-- PRD-W1b: 작업장 부하·Capa 조회 권한 및 경고 임계값 시드

INSERT INTO permission (permission_code, description) VALUES
('production:scheduling:read', '작업장 부하·일정 조회')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'production:scheduling:read'
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'production:scheduling:read'
WHERE r.role_code = 'PRODUCTION_OPERATOR' AND r.recording_state = 1;

INSERT INTO system_settings (setting_key, value_json, created_by, created_by_id)
VALUES ('production.schedule.warn_load_threshold', '0.8', 'system', 'system')
ON DUPLICATE KEY UPDATE value_json = value_json;

-- END former V037__scheduling_permissions.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V038__scheduling_read_from_work_plan.sql
-- ---------------------------------------------------------------------------

-- V037 보완: work-plan:read 역할에 scheduling:read 동기화 (기존 DB 호환)

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'production:scheduling:read'
JOIN permission wp ON wp.recording_state = 1 AND wp.permission_code = 'production:work-plan:read'
JOIN role_permission rp ON rp.role_id = r.id AND rp.permission_id = wp.id
WHERE r.recording_state = 1;

-- END former V038__scheduling_read_from_work_plan.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V039__work_order.sql
-- ---------------------------------------------------------------------------

-- PRD-W1c: 작업지시

CREATE TABLE IF NOT EXISTS work_order (
  id              BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  work_plan_id    BIGINT         NOT NULL,
  order_num       VARCHAR(50)    NOT NULL,
  ordered_qty     DECIMAL(18, 4) NOT NULL,
  reported_qty    DECIMAL(18, 4) NOT NULL DEFAULT 0,
  status          ENUM('ISSUED', 'CANCELLED') NOT NULL DEFAULT 'ISSUED',
  recording_state TINYINT        NOT NULL DEFAULT 1,
  created_by      VARCHAR(100)   NULL,
  created_by_id   VARCHAR(100)   NULL,
  created_at      DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100)   NULL,
  updated_by_id   VARCHAR(100)   NULL,
  updated_at      DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_work_order_plan (work_plan_id, recording_state),
  UNIQUE KEY uk_work_order_num (order_num, recording_state),
  INDEX idx_work_order_status (status, recording_state),
  CONSTRAINT fk_work_order_plan FOREIGN KEY (work_plan_id) REFERENCES work_plan (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('production:work-order:read', '작업지시 조회'),
('production:work-order:write', '작업지시 발행')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('production:work-order:read', 'production:work-order:write')
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'production:work-order:read'
WHERE r.role_code = 'PRODUCTION_OPERATOR' AND r.recording_state = 1;

-- END former V039__work_order.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V040__work_report.sql
-- ---------------------------------------------------------------------------

-- PRD-W1c: 작업일보 + 실적 이력

CREATE TABLE IF NOT EXISTS work_report (
  id              BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  report_num      VARCHAR(50)    NOT NULL,
  work_order_id   BIGINT         NOT NULL,
  report_date     DATE           NOT NULL,
  good_qty        DECIMAL(18, 4) NOT NULL,
  scrap_qty       DECIMAL(18, 4) NOT NULL DEFAULT 0,
  setup_time      DECIMAL(10, 2) NULL,
  run_time        DECIMAL(10, 2) NULL,
  worker_id       VARCHAR(50)    NULL,
  worker_name     VARCHAR(100)   NULL,
  status          ENUM('REGISTERED', 'CANCELLED') NOT NULL DEFAULT 'REGISTERED',
  stock_applied   TINYINT        NOT NULL DEFAULT 1,
  recording_state TINYINT        NOT NULL DEFAULT 1,
  created_by      VARCHAR(100)   NULL,
  created_by_id   VARCHAR(100)   NULL,
  created_at      DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100)   NULL,
  updated_by_id   VARCHAR(100)   NULL,
  updated_at      DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_work_report_num (report_num, recording_state),
  INDEX idx_work_report_order (work_order_id, recording_state),
  INDEX idx_work_report_date (report_date, recording_state),
  CONSTRAINT fk_work_report_order FOREIGN KEY (work_order_id) REFERENCES work_order (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS work_report_history (
  id                  BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  item_id             BIGINT         NOT NULL,
  production_plan_id  BIGINT         NOT NULL,
  process_sequence_id BIGINT         NOT NULL,
  good_qty            DECIMAL(18, 4) NOT NULL,
  scrap_qty           DECIMAL(18, 4) NOT NULL DEFAULT 0,
  history_date        DATE           NOT NULL,
  source_type         ENUM('WORK_REPORT', 'WORK_REPORT_CANCEL') NOT NULL,
  source_id           BIGINT         NOT NULL,
  fiscal_year         SMALLINT       NOT NULL,
  fiscal_month        TINYINT        NOT NULL,
  recording_state     TINYINT        NOT NULL DEFAULT 1,
  created_by          VARCHAR(100)   NULL,
  created_by_id       VARCHAR(100)   NULL,
  created_at          DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  INDEX idx_work_report_history_date (history_date, recording_state),
  INDEX idx_work_report_history_source (source_type, source_id, recording_state),
  CONSTRAINT fk_work_report_history_item FOREIGN KEY (item_id) REFERENCES item (id),
  CONSTRAINT fk_work_report_history_plan FOREIGN KEY (production_plan_id) REFERENCES production_plan (id),
  CONSTRAINT fk_work_report_history_process FOREIGN KEY (process_sequence_id) REFERENCES process_sequence (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('production:work-report:read', '작업일보 조회'),
('production:work-report:write', '작업일보 등록')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('production:work-report:read', 'production:work-report:write')
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'production:work-report:read'
WHERE r.role_code = 'PRODUCTION_OPERATOR' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'production:work-report:write'
WHERE r.role_code = 'PRODUCTION_OPERATOR' AND r.recording_state = 1;

-- END former V040__work_report.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V041__work_order_cancel_purge_inactive.sql
-- ---------------------------------------------------------------------------

-- 작업지시 취소: 소프트 삭제(recording_state=0) 잔존 행 정리 (UK 충돌 방지)
DELETE wr FROM work_report wr
INNER JOIN work_order wo ON wo.id = wr.work_order_id
WHERE wo.recording_state = 0;

DELETE FROM work_order WHERE recording_state = 0;

-- END former V041__work_order_cancel_purge_inactive.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V042__work_report_consumption_line.sql
-- ---------------------------------------------------------------------------

-- 작업일보 투입(하위품목) 이력 — 취소 시 역전기용

CREATE TABLE IF NOT EXISTS work_report_consumption_line (
  id                  BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  work_report_id      BIGINT         NOT NULL,
  line_no             SMALLINT       NOT NULL,
  item_id             BIGINT         NOT NULL,
  item_composition_id BIGINT         NULL,
  issue_qty           DECIMAL(18, 4) NOT NULL,
  location_code       VARCHAR(20)    NOT NULL,
  source_process_id   BIGINT         NULL,
  recording_state     TINYINT        NOT NULL DEFAULT 1,
  created_by          VARCHAR(100)   NULL,
  created_by_id       VARCHAR(100)   NULL,
  created_at          DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by          VARCHAR(100)   NULL,
  updated_by_id       VARCHAR(100)   NULL,
  updated_at          DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  INDEX idx_wr_consumption_report (work_report_id, recording_state),
  CONSTRAINT fk_wr_consumption_report FOREIGN KEY (work_report_id) REFERENCES work_report (id),
  CONSTRAINT fk_wr_consumption_item FOREIGN KEY (item_id) REFERENCES item (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- END former V042__work_report_consumption_line.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V043__material_issue.sql
-- ---------------------------------------------------------------------------

-- PRD-W2: 자재투입 TX (작업일보와 분리)

CREATE TABLE IF NOT EXISTS material_issue (
  id                  BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  issue_num           VARCHAR(50)    NOT NULL,
  work_order_id       BIGINT         NOT NULL,
  issue_date          DATE           NOT NULL,
  status              ENUM('ISSUED','CANCELLED') NOT NULL DEFAULT 'ISSUED',
  recording_state     TINYINT        NOT NULL DEFAULT 1,
  created_by          VARCHAR(100)   NULL,
  created_by_id       VARCHAR(100)   NULL,
  created_at          DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by          VARCHAR(100)   NULL,
  updated_by_id       VARCHAR(100)   NULL,
  updated_at          DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_material_issue_num (issue_num, recording_state),
  INDEX idx_material_issue_wo (work_order_id, recording_state),
  CONSTRAINT fk_material_issue_wo FOREIGN KEY (work_order_id) REFERENCES work_order (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS material_issue_line (
  id                      BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  material_issue_id       BIGINT         NOT NULL,
  line_no                 SMALLINT       NOT NULL,
  item_id                 BIGINT         NOT NULL,
  item_composition_id     BIGINT         NULL,
  issue_qty               DECIMAL(18, 4) NOT NULL,
  source_location_code    VARCHAR(20)    NOT NULL,
  source_process_id       BIGINT         NULL,
  recording_state         TINYINT        NOT NULL DEFAULT 1,
  CONSTRAINT fk_mil_header FOREIGN KEY (material_issue_id) REFERENCES material_issue (id),
  CONSTRAINT fk_mil_item FOREIGN KEY (item_id) REFERENCES item (id),
  CONSTRAINT fk_mil_bom FOREIGN KEY (item_composition_id) REFERENCES item_composition (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('production:material-issue:read', '자재투입 조회'),
('production:material-issue:write', '자재투입 등록')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('production:material-issue:read', 'production:material-issue:write')
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'production:material-issue:read'
WHERE r.role_code = 'PRODUCTION_OPERATOR' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'production:material-issue:write'
WHERE r.role_code = 'PRODUCTION_OPERATOR' AND r.recording_state = 1;

-- END former V043__material_issue.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V044__inventory_ledger_permissions.sql
-- ---------------------------------------------------------------------------

-- INF-1b: 재고·원장 조회 권한

INSERT INTO permission (permission_code, description) VALUES
('inventory:ledger:read', '재고·원장 조회')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'inventory:ledger:read'
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'inventory:ledger:read'
WHERE r.role_code = 'PRODUCTION_OPERATOR' AND r.recording_state = 1;

-- END former V044__inventory_ledger_permissions.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V045__production_material_issue_setting.sql
-- ---------------------------------------------------------------------------

-- 생산: 자재투입 여부 시스템 설정 (기본 아니오 = 작업일보 등록 시 백플러시)

INSERT INTO system_settings (setting_key, value_json, created_by, created_by_id)
VALUES ('production.material_issue.enabled', '"NO"', 'system', 'system');

-- END former V045__production_material_issue_setting.sql

