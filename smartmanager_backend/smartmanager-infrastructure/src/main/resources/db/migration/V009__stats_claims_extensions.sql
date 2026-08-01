-- =============================================================================
-- V009__stats_claims_extensions.sql — 통계권한·클레임·선지급·품목속성확장
-- Squash of former V082–V095 (chronological concat, 2026-08-01)
-- =============================================================================


-- ---------------------------------------------------------------------------
-- BEGIN former V082__stats_report_permissions.sql
-- ---------------------------------------------------------------------------

-- 통계및 지표: 조회 권한 (전 역할 공통)

INSERT INTO permission (permission_code, description) VALUES
('stats:vendor-purchase:read', '매입처별 집계 조회'),
('stats:warehouse-io:read', '창고별 수불현황 조회'),
('stats:item-io:read', '품목별 수불현황 조회')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN (
    'stats:vendor-purchase:read',
    'stats:warehouse-io:read',
    'stats:item-io:read'
  )
WHERE r.recording_state = 1
  AND r.role_code IN (
    'SYSTEM_ADMIN',
    'BASIS_MANAGER',
    'SALES_OPERATOR',
    'PRODUCTION_OPERATOR',
    'PURCHASE_OPERATOR',
    'VIEWER'
  );

-- END former V082__stats_report_permissions.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V083__quality_inspection_failure_reason.sql
-- ---------------------------------------------------------------------------

-- 품질검사 불량사유(자유 입력)

ALTER TABLE quality_inspection
  ADD COLUMN failure_reason VARCHAR(500) NULL AFTER unsuitability_status_code_id;

-- END former V083__quality_inspection_failure_reason.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V084__vendor_purchase_status_permission.sql
-- ---------------------------------------------------------------------------

-- 통계및 지표: 매입처별 매입현황 조회 권한

INSERT INTO permission (permission_code, description) VALUES
('stats:vendor-purchase-status:read', '매입처별 매입현황 조회')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'stats:vendor-purchase-status:read'
WHERE r.recording_state = 1
  AND r.role_code IN (
    'SYSTEM_ADMIN', 'BASIS_MANAGER', 'SALES_OPERATOR',
    'PRODUCTION_OPERATOR', 'PURCHASE_OPERATOR', 'VIEWER'
  );

-- END former V084__vendor_purchase_status_permission.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V085__purchase_daily_report_permission.sql
-- ---------------------------------------------------------------------------

-- 통계및 지표: 매입일보 조회 권한

INSERT INTO permission (permission_code, description) VALUES
('stats:purchase-daily:read', '매입일보 조회')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'stats:purchase-daily:read'
WHERE r.recording_state = 1
  AND r.role_code IN (
    'SYSTEM_ADMIN', 'BASIS_MANAGER', 'SALES_OPERATOR',
    'PRODUCTION_OPERATOR', 'PURCHASE_OPERATOR', 'VIEWER'
  );

-- END former V085__purchase_daily_report_permission.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V086__etc_claim.sql
-- ---------------------------------------------------------------------------

-- 기타공제등록 (레거시 ECL_HT)

CREATE TABLE IF NOT EXISTS etc_claim (
  id              BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  partner_id      BIGINT         NOT NULL,
  receipt_date    DATE           NOT NULL,
  reason          VARCHAR(500)   NOT NULL,
  amount          DECIMAL(18, 2) NOT NULL,
  recognition     ENUM('PENDING', 'APPROVED') NOT NULL DEFAULT 'PENDING',
  recording_state TINYINT        NOT NULL DEFAULT 1,
  created_by      VARCHAR(100)   NULL,
  created_by_id   VARCHAR(100)   NULL,
  created_at      DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100)   NULL,
  updated_by_id   VARCHAR(100)   NULL,
  updated_at      DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  INDEX idx_etc_claim_partner_date (partner_id, receipt_date, recording_state),
  INDEX idx_etc_claim_receipt_date (receipt_date, recording_state),
  INDEX idx_etc_claim_created_at (created_at, recording_state),
  CONSTRAINT fk_etc_claim_partner FOREIGN KEY (partner_id) REFERENCES company (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('purchase:etc-claim:read', '기타공제 조회'),
('purchase:etc-claim:write', '기타공제 등록·수정·삭제')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('purchase:etc-claim:read', 'purchase:etc-claim:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PURCHASE_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'purchase:etc-claim:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;

-- END former V086__etc_claim.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V087__etc_claim_approval_fiscal.sql
-- ---------------------------------------------------------------------------

-- 기타공제 승인·매입년월 연동

ALTER TABLE etc_claim
  ADD COLUMN fiscal_year SMALLINT NULL AFTER amount,
  ADD COLUMN fiscal_month TINYINT NULL AFTER fiscal_year,
  ADD COLUMN approved_at DATETIME(3) NULL AFTER recognition,
  ADD COLUMN approved_by_user_id BIGINT NULL AFTER approved_at,
  ADD COLUMN approval_cancelled_at DATETIME(3) NULL AFTER approved_by_user_id,
  ADD COLUMN approval_cancelled_by_user_id BIGINT NULL AFTER approval_cancelled_at;

UPDATE etc_claim
SET fiscal_year = YEAR(receipt_date),
    fiscal_month = MONTH(receipt_date);

ALTER TABLE etc_claim
  MODIFY COLUMN fiscal_year SMALLINT NOT NULL,
  MODIFY COLUMN fiscal_month TINYINT NOT NULL;

CREATE INDEX idx_etc_claim_recognition_fiscal
  ON etc_claim (recognition, fiscal_year, fiscal_month, recording_state);

-- END former V087__etc_claim_approval_fiscal.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V088__item_property_classification_sub_material.sql
-- ---------------------------------------------------------------------------

-- 자산구분: 부자재 추가 (창고 재고 관리 대상 아님, 구매단가·발주·입고만 사용)
ALTER TABLE item
  MODIFY COLUMN property_classification ENUM('원자재','제품','상품','공정품','부자재') NOT NULL;

-- END former V088__item_property_classification_sub_material.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V089__defect_claim.sql
-- ---------------------------------------------------------------------------

-- 불량변상 (레거시 PCL_HT). 불량현상/원인 코드 없이 사유 텍스트만 사용.

CREATE TABLE IF NOT EXISTS defect_claim (
  id              BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  partner_id      BIGINT         NOT NULL,
  item_id         BIGINT         NOT NULL,
  receipt_date    DATE           NOT NULL,
  claim_qty       DECIMAL(18, 4) NOT NULL,
  amount          DECIMAL(18, 2) NOT NULL,
  reason          VARCHAR(500)   NOT NULL,
  fiscal_year     SMALLINT       NOT NULL,
  fiscal_month    TINYINT        NOT NULL,
  recognition     ENUM('PENDING', 'APPROVED') NOT NULL DEFAULT 'PENDING',
  approved_at     DATETIME(3)    NULL,
  approved_by_user_id BIGINT     NULL,
  approval_cancelled_at DATETIME(3) NULL,
  approval_cancelled_by_user_id BIGINT NULL,
  recording_state TINYINT        NOT NULL DEFAULT 1,
  created_by      VARCHAR(100)   NULL,
  created_by_id   VARCHAR(100)   NULL,
  created_at      DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by      VARCHAR(100)   NULL,
  updated_by_id   VARCHAR(100)   NULL,
  updated_at      DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  INDEX idx_defect_claim_partner_date (partner_id, receipt_date, recording_state),
  INDEX idx_defect_claim_item_date (item_id, receipt_date, recording_state),
  INDEX idx_defect_claim_receipt_date (receipt_date, recording_state),
  INDEX idx_defect_claim_recognition_fiscal (recognition, fiscal_year, fiscal_month, recording_state),
  CONSTRAINT fk_defect_claim_partner FOREIGN KEY (partner_id) REFERENCES company (id),
  CONSTRAINT fk_defect_claim_item FOREIGN KEY (item_id) REFERENCES item (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('purchase:defect-claim:read', '불량변상 조회'),
('purchase:defect-claim:write', '불량변상 등록·수정·삭제')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('purchase:defect-claim:read', 'purchase:defect-claim:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PURCHASE_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'purchase:defect-claim:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;

-- END former V089__defect_claim.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V090__order_vs_receipt_permission.sql
-- ---------------------------------------------------------------------------

-- 통계및 지표: 발주대비입고 조회 권한

INSERT INTO permission (permission_code, description) VALUES
('stats:order-vs-receipt:read', '발주대비입고 조회')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'stats:order-vs-receipt:read'
WHERE r.recording_state = 1
  AND r.role_code IN (
    'SYSTEM_ADMIN', 'BASIS_MANAGER', 'SALES_OPERATOR',
    'PRODUCTION_OPERATOR', 'PURCHASE_OPERATOR', 'VIEWER'
  );

-- END former V090__order_vs_receipt_permission.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V091__notice_board_post_read.sql
-- ---------------------------------------------------------------------------

-- 공지사항(NOTICE) 원글 열람자 추적

CREATE TABLE IF NOT EXISTS board_post_read (
  id              BIGINT      NOT NULL AUTO_INCREMENT PRIMARY KEY,
  post_id         BIGINT      NOT NULL,
  reader_user_id  BIGINT      NOT NULL,
  read_at         DATETIME(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_board_post_read_post_reader (post_id, reader_user_id),
  INDEX idx_board_post_read_post (post_id, read_at),
  CONSTRAINT fk_board_post_read_post FOREIGN KEY (post_id) REFERENCES board_post (id),
  CONSTRAINT fk_board_post_read_user FOREIGN KEY (reader_user_id) REFERENCES `user` (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- END former V091__notice_board_post_read.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V092__drawing_lifecycle.sql
-- ---------------------------------------------------------------------------

-- 도면 lifecycle 단계·거래처 선수신·품목 연결 시각

ALTER TABLE drawing_master
  ADD COLUMN lifecycle_stage ENUM(
    'RECEIVED',
    'SAMPLE',
    'PARTNER_REVIEW',
    'MASS_PROD_READY',
    'ITEM_LINKED',
    'ARCHIVED'
  ) NOT NULL DEFAULT 'RECEIVED' AFTER model_type,
  ADD COLUMN source_partner_id BIGINT NULL AFTER lifecycle_stage,
  ADD COLUMN item_linked_at DATETIME(3) NULL AFTER item_id;

ALTER TABLE drawing_master
  ADD INDEX idx_drawing_source_partner (source_partner_id);

ALTER TABLE drawing_master
  ADD CONSTRAINT fk_drawing_source_partner
    FOREIGN KEY (source_partner_id) REFERENCES company (id);

UPDATE drawing_master dm
INNER JOIN drawing_history dh
  ON dh.drawing_master_id = dm.id AND dh.is_latest = 'Y'
SET dm.lifecycle_stage = CASE
  WHEN dm.item_id IS NOT NULL THEN 'ITEM_LINKED'
  WHEN dh.drawing_type = 'PROD' THEN 'MASS_PROD_READY'
  ELSE 'SAMPLE'
END;

UPDATE drawing_master
SET item_linked_at = COALESCE(updated_at, created_at)
WHERE item_id IS NOT NULL AND item_linked_at IS NULL;

-- END former V092__drawing_lifecycle.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V093__partner_prepaid.sql
-- ---------------------------------------------------------------------------

-- TX1-PP: 품목 단위 선지급 · 승인 FIFO 상계
-- docs/step0/partner-prepaid-design.md

ALTER TABLE partner_payment
  ADD COLUMN payment_kind ENUM('NORMAL', 'PREPAID') NOT NULL DEFAULT 'NORMAL'
    AFTER cost_category;

CREATE TABLE IF NOT EXISTS partner_payment_line (
  id                          BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  payment_id                  BIGINT         NOT NULL,
  item_id                     BIGINT         NOT NULL,
  purchase_order_line_id      BIGINT         NULL,
  outsourcing_order_line_id   BIGINT         NULL,
  supply_amount               DECIMAL(18, 2) NOT NULL,
  vat_amount                  DECIMAL(18, 2) NOT NULL DEFAULT 0,
  total_amount                DECIMAL(18, 2) NOT NULL,
  recording_state             TINYINT        NOT NULL DEFAULT 1,
  created_by                  VARCHAR(100)   NULL,
  created_by_id               VARCHAR(100)   NULL,
  created_at                  DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by                  VARCHAR(100)   NULL,
  updated_by_id               VARCHAR(100)   NULL,
  updated_at                  DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  INDEX idx_ppl_payment (payment_id, recording_state),
  INDEX idx_ppl_bucket (item_id, recording_state),
  INDEX idx_ppl_po_line (purchase_order_line_id, recording_state),
  INDEX idx_ppl_os_line (outsourcing_order_line_id, recording_state),
  CONSTRAINT fk_ppl_payment FOREIGN KEY (payment_id) REFERENCES partner_payment (id),
  CONSTRAINT fk_ppl_item FOREIGN KEY (item_id) REFERENCES item (id),
  CONSTRAINT fk_ppl_po_line FOREIGN KEY (purchase_order_line_id) REFERENCES purchase_order_line (id),
  CONSTRAINT fk_ppl_os_line FOREIGN KEY (outsourcing_order_line_id) REFERENCES outsourcing_order_line (id),
  CONSTRAINT chk_ppl_order_line_xor CHECK (
    purchase_order_line_id IS NULL OR outsourcing_order_line_id IS NULL
  )
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS partner_prepaid_offset (
  id                BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  payment_line_id   BIGINT         NOT NULL,
  ledger_kind       ENUM('PURCHASE_HISTORY', 'OUTSOURCE_HISTORY') NOT NULL,
  history_id        BIGINT         NOT NULL,
  amount            DECIMAL(18, 2) NOT NULL,
  recording_state   TINYINT        NOT NULL DEFAULT 1,
  created_by        VARCHAR(100)   NULL,
  created_by_id     VARCHAR(100)   NULL,
  created_at        DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by        VARCHAR(100)   NULL,
  updated_by_id     VARCHAR(100)   NULL,
  updated_at        DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  INDEX idx_ppo_line (payment_line_id, recording_state),
  INDEX idx_ppo_history (ledger_kind, history_id, recording_state),
  CONSTRAINT fk_ppo_payment_line FOREIGN KEY (payment_line_id) REFERENCES partner_payment_line (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- END former V093__partner_prepaid.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V094__partner_monthly_payable_permission.sql
-- ---------------------------------------------------------------------------

-- 통계및 지표: 월별 실지급액(거래처) 조회 권한

INSERT INTO permission (permission_code, description) VALUES
('stats:partner-monthly-payable:read', '월별 실지급액 조회')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'stats:partner-monthly-payable:read'
WHERE r.recording_state = 1
  AND r.role_code IN (
    'SYSTEM_ADMIN', 'BASIS_MANAGER', 'SALES_OPERATOR',
    'PRODUCTION_OPERATOR', 'PURCHASE_OPERATOR', 'VIEWER'
  );

-- END former V094__partner_monthly_payable_permission.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V095__item_property_classification_phantom.sql
-- ---------------------------------------------------------------------------

-- 자산분류: 팬텀 추가 (BOM 구성용, 일괄등록 전용 — 화면 등록 UI에서는 비노출)
ALTER TABLE item
  MODIFY COLUMN property_classification ENUM('원자재','제품','상품','공정품','부자재','팬텀') NOT NULL;

-- END former V095__item_property_classification_phantom.sql

