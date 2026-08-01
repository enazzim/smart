-- =============================================================================
-- V007__community_payment_system.sql — 커뮤니티·업무일지·지급·시스템설정
-- Squash of former V056–V071 (chronological concat, 2026-08-01)
-- =============================================================================


-- ---------------------------------------------------------------------------
-- BEGIN former V056__community_board.sql
-- ---------------------------------------------------------------------------

-- COMM-1: 공통 게시판 엔진 (board_post, board_attachment)

CREATE TABLE IF NOT EXISTS board_post (
  id               BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  board_type       ENUM('NOTICE','PRESIDENT_NOTICE','PRODUCT','LASER','INSTITUTE','SALES_QC') NOT NULL,
  post_kind        ENUM('TOP','REPLY') NOT NULL,
  parent_post_id   BIGINT         NULL,
  thread_root_id   BIGINT         NULL,
  title            VARCHAR(500)   NULL,
  content          MEDIUMTEXT     NULL,
  author_user_id   BIGINT         NULL,
  view_count       INT            NOT NULL DEFAULT 0,
  pinned           TINYINT        NOT NULL DEFAULT 0,
  recording_state  TINYINT        NOT NULL DEFAULT 1,
  created_by       VARCHAR(100)   NULL,
  created_by_id    VARCHAR(100)   NULL,
  created_at       DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by       VARCHAR(100)   NULL,
  updated_by_id    VARCHAR(100)   NULL,
  updated_at       DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  INDEX idx_board_post_list (board_type, post_kind, recording_state, created_at),
  INDEX idx_board_post_thread (thread_root_id, created_at),
  INDEX idx_board_post_parent (parent_post_id),
  CONSTRAINT fk_board_post_author_user FOREIGN KEY (author_user_id) REFERENCES `user` (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 게시글 1건당 첨부파일 여러 개 (1:N). 파일 1개당 최대 100MB는 API·yml에서 검증.
CREATE TABLE IF NOT EXISTS board_attachment (
  id                 BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  post_id            BIGINT         NOT NULL,
  original_file_name VARCHAR(500)   NOT NULL,
  stored_file_name   VARCHAR(500)   NOT NULL,
  content_type       VARCHAR(100)   NULL,
  file_size          BIGINT         NOT NULL,
  recording_state    TINYINT        NOT NULL DEFAULT 1,
  created_at         DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  INDEX idx_board_attachment_post (post_id, recording_state),
  CONSTRAINT fk_board_attachment_post FOREIGN KEY (post_id) REFERENCES board_post (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('community:board:read', '게시판 조회'),
('community:board:write', '게시판 작성·수정'),
('community:board:moderate', '게시판 글 고정·삭제')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('community:board:read','community:board:write','community:board:moderate')
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'community:board:read'
WHERE r.role_code IN ('VIEWER','SALES_OPERATOR','PRODUCTION_OPERATOR','PURCHASE_OPERATOR','BASIS_MANAGER') 
  AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'community:board:write'
WHERE r.role_code IN ('BASIS_MANAGER','SALES_OPERATOR','PRODUCTION_OPERATOR','PURCHASE_OPERATOR')
  AND r.recording_state = 1;

-- END former V056__community_board.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V057__work_diary.sql
-- ---------------------------------------------------------------------------

-- COMM-3: 업무일지 (그룹별 템플릿 + 일지 엔트리)
-- Ref: docs/step0/community-board-wave-design.md

CREATE TABLE IF NOT EXISTS work_diary_template (
  id                   BIGINT        NOT NULL AUTO_INCREMENT PRIMARY KEY,
  template_code        CHAR(2)       NOT NULL,
  work_diary_group_id  BIGINT        NOT NULL,
  template_name        VARCHAR(100)  NOT NULL,
  field_schema         JSON          NOT NULL,
  recording_state      TINYINT       NOT NULL DEFAULT 1,
  created_by           VARCHAR(100)  NULL,
  created_by_id        VARCHAR(100)  NULL,
  created_at           DATETIME(3)   NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by           VARCHAR(100)  NULL,
  updated_by_id        VARCHAR(100)  NULL,
  updated_at           DATETIME(3)   NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_work_diary_template_group_code (work_diary_group_id, template_code),
  INDEX idx_work_diary_template_active (recording_state, template_code),
  CONSTRAINT fk_work_diary_template_group FOREIGN KEY (work_diary_group_id) REFERENCES public_code (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS work_diary_entry (
  id                   BIGINT                  NOT NULL AUTO_INCREMENT PRIMARY KEY,
  work_date            DATE                    NOT NULL,
  author_user_id       BIGINT                  NOT NULL,
  work_diary_group_id  BIGINT                  NOT NULL,
  template_code        CHAR(2)                 NOT NULL,
  work_date_title      VARCHAR(200)            NULL,
  field_values         JSON                    NOT NULL,
  directive_note       VARCHAR(1000)           NULL,
  listed               TINYINT                 NOT NULL DEFAULT 1,
  closing_note         VARCHAR(500)            NULL,
  status               ENUM('DRAFT','SUBMITTED','APPROVED','REJECTED') NOT NULL DEFAULT 'DRAFT',
  submitted_at         DATETIME(3)             NULL,
  approved_at          DATETIME(3)             NULL,
  approved_by_user_id  BIGINT                  NULL,
  approval_canceled_at DATETIME(3)             NULL,
  approval_canceled_by_user_id BIGINT          NULL,
  recording_state      TINYINT                 NOT NULL DEFAULT 1,
  created_by           VARCHAR(100)            NULL,
  created_by_id        VARCHAR(100)            NULL,
  created_at           DATETIME(3)             NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by           VARCHAR(100)            NULL,
  updated_by_id        VARCHAR(100)            NULL,
  updated_at           DATETIME(3)             NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_work_diary_entry_author_date_active (author_user_id, work_date, recording_state),
  INDEX idx_work_diary_entry_list (work_date, listed, recording_state),
  INDEX idx_work_diary_entry_group_date (work_diary_group_id, work_date),
  CONSTRAINT fk_work_diary_entry_author FOREIGN KEY (author_user_id) REFERENCES `user` (id),
  CONSTRAINT fk_work_diary_entry_group FOREIGN KEY (work_diary_group_id) REFERENCES public_code (id),
  CONSTRAINT fk_work_diary_entry_approved_by FOREIGN KEY (approved_by_user_id) REFERENCES `user` (id),
  CONSTRAINT fk_work_diary_entry_approval_canceled_by FOREIGN KEY (approval_canceled_by_user_id) REFERENCES `user` (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 01~16 그룹 템플릿 시드 (레거시 WorkReportList.aspx.cs 기반)
INSERT INTO work_diary_template
  (template_code, work_diary_group_id, template_name, field_schema, recording_state, created_by)
SELECT
  t.template_code,
  pc.id AS work_diary_group_id,
  t.template_name,
  t.field_schema,
  1,
  'seed'
FROM (
  SELECT '01' AS template_code, '19000020' AS small_code, '업무일지 01 그룹' AS template_name,
         JSON_OBJECT('legacyFields', JSON_OBJECT(
           '01', '1. 출장내용',
           '02', '2. 기타 보고사항 및 건의 사항',
           '06', '4. 지시사항',
           '07', '3. 외근사항',
           '10', '* 로더 공정별 확인'
         )) AS field_schema
  UNION ALL
  SELECT '02','19000030','업무일지 02 그룹',
         JSON_OBJECT('legacyFields', JSON_OBJECT(
           '01','1. 업무현황',
           '02','2. 실린더 현황',
           '03','3. 기타 보고사항 및 건의 사항',
           '06','4. 지시사항'
         ))
  UNION ALL
  SELECT '03','19000040','업무일지 03 그룹',
         JSON_OBJECT('legacyFields', JSON_OBJECT(
           '01','1. 인원 현황',
           '02','2. 업무현황(납기독촉사항)',
           '03','3. 기타 보고사항 및 건의 사항',
           '06','5. 지시사항',
           '07','4. 외근사항',
           '10','* 로더 공정별 확인'
         ))
  UNION ALL
  SELECT '04','19000050','업무일지 04 그룹',
         JSON_OBJECT('legacyFields', JSON_OBJECT(
           '01','1. 매출현황',
           '02','2. 업무현황(납기독촉사항)',
           '03','3. 기타 보고사항 및 건의 사항',
           '06','5. 지시사항',
           '07','4. 외근사항',
           '10','* 로더 공정별 확인'
         ))
  UNION ALL
  SELECT '05','19000060','업무일지 05 그룹',
         JSON_OBJECT('legacyFields', JSON_OBJECT(
           '01','1. 공문수발 현황',
           '02','2. 대동 EDI 공문내용',
           '03','3. 기타 보고사항 및 건의 사항',
           '06','5. 지시사항',
           '07','4. 외근사항',
           '10','* 로더 공정별 확인'
         ))
  UNION ALL
  SELECT '06','19000070','업무일지 06 그룹',
         JSON_OBJECT('legacyFields', JSON_OBJECT(
           '01','1. 출장자 현황',
           '02','2. 거래처 협의사항 및 방문자',
           '03','3. 설비이상 사항',
           '04','4. 개발 관련 사항, 납품독촉사항',
           '05','5. 주요현안 사항',
           '06','9. 지시사항',
           '07','8. 외근사항',
           '08','6. 공장최종점검내역',
           '09','7. 조치사항',
           '10','* 로더 공정별 확인'
         ))
  UNION ALL
  SELECT '07','19000080','업무일지 07 그룹',
         JSON_OBJECT('legacyFields', JSON_OBJECT(
           '01','1. 고객 품질문제 통보사항',
           '02','2. 개발 업무 현황',
           '03','3. 외주품 입고검사 부적합/반송',
           '04','4. 일일 불량 Check List',
           '05','5. 기타 보고사항 및 건의 사항',
           '06','7. 지시사항',
           '07','6. 외근사항',
           '10','* 로더 공정별 확인'
         ))
  UNION ALL
  SELECT '08','19000090','업무일지 08 그룹',
         JSON_OBJECT('legacyFields', JSON_OBJECT(
           '01','1. 자재관리 관련 사항(구매/외주)',
           '02','2. 생산관리 관련 사항',
           '03','3. 전산 관련 사항',
           '04','4. 기타 현안 사항',
           '06','6. 지시사항',
           '07','5. 외근사항',
           '10','* 로더 공정별 확인'
         ))
  UNION ALL
  SELECT '09','19000100','업무일지 09 그룹',
         JSON_OBJECT('legacyFields', JSON_OBJECT(
           '01','1. 주요현안사항',
           '02','2. 외국인숙소점검 사항',
           '06','4. 지시사항',
           '07','3. 외근사항',
           '10','* 로더 공정별 확인'
         ))
  UNION ALL
  SELECT '10','19000110','업무일지 10 그룹',
         JSON_OBJECT('legacyFields', JSON_OBJECT(
           '01','1. 개발업무 관련사항',
           '02','2. 개정업무 관련사항',
           '03','3. 기타 보고사항 및 건의사항',
           '06','5. 지시사항',
           '07','4. 외근사항',
           '10','* 로더 공정별 확인'
         ))
  UNION ALL
  SELECT '11','19000120','업무일지 11 그룹',
         JSON_OBJECT('legacyFields', JSON_OBJECT(
           '01','1. 주요 작업 내용',
           '02','2. 부적합 내용',
           '03','3. 기타 보고사항 및 건의 사항',
           '04','4. 공장최종점검내역',
           '05','5. 조치사항',
           '06','7. 지시사항',
           '07','6. 외근사항',
           '10','* 로더 공정별 확인'
         ))
  UNION ALL
  SELECT '12','19000130','업무일지 12 그룹',
         JSON_OBJECT('legacyFields', JSON_OBJECT(
           '01','1. 수출관련 업무',
           '02','2. 통합메일 수신',
           '03','3. 기타 보고사항 및 건의 사항',
           '06','5. 지시사항',
           '07','4. 외근사항',
           '10','* 로더 공정별 확인'
         ))
  UNION ALL
  SELECT '13','19000140','업무일지 13 그룹',
         JSON_OBJECT('legacyFields', JSON_OBJECT(
           '01','1. 주요 작업 내용',
           '02','2. 부적합 내용',
           '03','3. 차량 운행 현황',
           '04','4. 기타보고 및 건사항',
           '06','6. 지시사항',
           '07','5. 외근사항',
           '10','* 로더 공정별 확인'
         ))
  UNION ALL
  SELECT '14','19000150','업무일지 14 그룹',
         JSON_OBJECT('legacyFields', JSON_OBJECT(
           '01','1. 레이져 작업 내역',
           '02','2. 레이저불량내역',
           '03','3. 레이져 절단기 체크LIST',
           '06','5. 지시사항',
           '07','4. 외근사항',
           '10','* 로더 공정별 확인'
         ))
  UNION ALL
  SELECT '15','19000160','업무일지 15 그룹',
         JSON_OBJECT('legacyFields', JSON_OBJECT(
           '01','1. 생산관리 관련 현황',
           '02','2. 일일 체크LIST',
           '03','3. 기타 보고사항',
           '06','4. 지시사항',
           '07','',
           '10',''
         ))
  UNION ALL
  SELECT '16','19000170','업무일지 16 그룹',
         JSON_OBJECT('legacyFields', JSON_OBJECT(
           '01','1. 주요 작업 내용(3RD Function 밸브 검수)',
           '02','2. 부적합 내용',
           '03','3. 기타 보고사항 및 건의 사항',
           '04','4. 공장최종점검내역',
           '05','5. 조치사항',
           '06','7. 지시사항',
           '07','6. 외근사항',
           '10','* 로더 공정별 확인'
         ))
) t
JOIN public_code pc
  ON pc.usage_type = 'WORK_DIARY_GROUP'
 AND pc.small_code = t.small_code
 AND pc.recording_state = 1
ON DUPLICATE KEY UPDATE
  template_name = VALUES(template_name),
  field_schema = VALUES(field_schema),
  recording_state = VALUES(recording_state),
  updated_at = CURRENT_TIMESTAMP(3);

INSERT INTO permission (permission_code, description) VALUES
('community:workdiary:read', '업무일지 조회'),
('community:workdiary:write', '업무일지 작성·수정·삭제'),
('community:workdiary:approve', '업무일지 결재·결재취소')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('community:workdiary:read','community:workdiary:write','community:workdiary:approve')
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('community:workdiary:read','community:workdiary:write')
WHERE r.role_code IN ('VIEWER','SALES_OPERATOR','PRODUCTION_OPERATOR','PURCHASE_OPERATOR','BASIS_MANAGER')
  AND r.recording_state = 1;

-- END former V057__work_diary.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V058__work_diary_template_remove_writer_directive.sql
-- ---------------------------------------------------------------------------

-- 작성자 양식에서 지시사항(레거시 필드 06) 제거 — 결재자 directive_note 전용
UPDATE work_diary_template
SET field_schema = JSON_REMOVE(field_schema, '$.legacyFields."06"'),
    updated_at = CURRENT_TIMESTAMP(3)
WHERE recording_state = 1
  AND JSON_EXTRACT(field_schema, '$.legacyFields."06"') IS NOT NULL;

-- END former V058__work_diary_template_remove_writer_directive.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V059__basis_user_read_for_operators.sql
-- ---------------------------------------------------------------------------

-- 업무 담당 역할도 기준정보 > 사용자(본인 조회) 화면 접근 가능
INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.permission_code = 'basis:user:read' AND p.recording_state = 1
WHERE r.role_code IN ('SALES_OPERATOR', 'PRODUCTION_OPERATOR', 'PURCHASE_OPERATOR')
  AND r.recording_state = 1;

-- END former V059__basis_user_read_for_operators.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V060__partner_payment.sql
-- ---------------------------------------------------------------------------

-- INF-5: 지급 (구매·외주 미지급 정리)

ALTER TABLE partner_ledger_monthly
  ADD COLUMN paid_amount DECIMAL(18, 2) NOT NULL DEFAULT 0 AFTER purchase_amount;

CREATE TABLE IF NOT EXISTS partner_payment (
  id               BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  payment_no       VARCHAR(30)    NOT NULL,
  partner_id       BIGINT         NOT NULL,
  payment_date     DATE           NOT NULL,
  cost_category    ENUM('PURCHASE','OUTSOURCE') NOT NULL,
  supply_amount    DECIMAL(18, 2) NOT NULL,
  vat_amount       DECIMAL(18, 2) NOT NULL DEFAULT 0,
  total_amount     DECIMAL(18, 2) NOT NULL,
  payment_method   VARCHAR(50)    NULL,
  remark           VARCHAR(500)   NULL,
  status           ENUM('ISSUED','CANCELLED') NOT NULL DEFAULT 'ISSUED',
  recording_state  TINYINT        NOT NULL DEFAULT 1,
  created_by       VARCHAR(100)   NULL,
  created_by_id    VARCHAR(100)   NULL,
  created_at       DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by       VARCHAR(100)   NULL,
  updated_by_id    VARCHAR(100)   NULL,
  updated_at       DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_partner_payment_no (payment_no, recording_state),
  INDEX idx_partner_payment_partner (partner_id, payment_date, recording_state),
  CONSTRAINT fk_partner_payment_partner FOREIGN KEY (partner_id) REFERENCES company (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('purchase:payment:read', '지급 조회'),
('purchase:payment:write', '지급 등록·취소')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('purchase:payment:read', 'purchase:payment:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PURCHASE_OPERATOR', 'PRODUCTION_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'purchase:payment:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;

-- END former V060__partner_payment.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V061__system_backup_permissions.sql
-- ---------------------------------------------------------------------------

-- S1: 데이터 백업·복구 권한

INSERT INTO permission (permission_code, description) VALUES
('system:backup:read', '데이터 백업 목록 조회'),
('system:backup:execute', '데이터 백업·복구 실행')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('system:backup:read', 'system:backup:execute')
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

-- END former V061__system_backup_permissions.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V062__item_model_type.sql
-- ---------------------------------------------------------------------------

-- 품목 마스터: 기종(모델) 필드 추가

ALTER TABLE item
  ADD COLUMN model_type VARCHAR(100) NULL AFTER property_classification;

-- END former V062__item_model_type.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V063__etc_purchase.sql
-- ---------------------------------------------------------------------------

-- 기타구매발주·입고 + purchase_history 기타구매 연동

ALTER TABLE purchase_history
  MODIFY COLUMN item_id BIGINT NULL,
  ADD COLUMN item_name VARCHAR(200) NULL AFTER item_id;

ALTER TABLE purchase_history
  MODIFY COLUMN source_type ENUM('PURCHASE_RECEIPT', 'QUALITY_INSPECTION', 'ETC_PURCHASE_RECEIPT') NOT NULL;

CREATE TABLE IF NOT EXISTS etc_purchase_order (
  id                      BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  order_no                VARCHAR(30)    NOT NULL,
  item_name               VARCHAR(200)   NOT NULL,
  partner_id              BIGINT         NOT NULL,
  unit_price              DECIMAL(18, 2) NOT NULL DEFAULT 0,
  order_qty               DECIMAL(18, 4) NOT NULL,
  amount                  DECIMAL(18, 2) NOT NULL DEFAULT 0,
  remain_qty              DECIMAL(18, 4) NOT NULL,
  requested_delivery_date DATE           NOT NULL,
  category_code_id        BIGINT         NULL,
  status                  ENUM('WAITING', 'IN_PROGRESS', 'COMPLETED') NOT NULL DEFAULT 'WAITING',
  order_date              DATE           NOT NULL,
  recording_state         TINYINT        NOT NULL DEFAULT 1,
  created_by              VARCHAR(100)   NULL,
  created_by_id           VARCHAR(100)   NULL,
  created_at              DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by              VARCHAR(100)   NULL,
  updated_by_id             VARCHAR(100)   NULL,
  updated_at              DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_etc_purchase_order_no (order_no, recording_state),
  INDEX idx_etc_purchase_order_partner (partner_id, recording_state),
  INDEX idx_etc_purchase_order_status (status, recording_state),
  INDEX idx_etc_purchase_order_delivery (requested_delivery_date, recording_state),
  CONSTRAINT fk_etc_purchase_order_partner FOREIGN KEY (partner_id) REFERENCES company (id),
  CONSTRAINT fk_etc_purchase_order_category FOREIGN KEY (category_code_id) REFERENCES public_code (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS etc_purchase_receipt (
  id                    BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  receipt_no            VARCHAR(30)    NOT NULL,
  etc_purchase_order_id BIGINT         NOT NULL,
  partner_id            BIGINT         NOT NULL,
  item_name             VARCHAR(200)   NOT NULL,
  receipt_qty           DECIMAL(18, 4) NOT NULL,
  unit_price            DECIMAL(18, 2) NOT NULL,
  amount                DECIMAL(18, 2) NOT NULL,
  receipt_date          DATE           NOT NULL,
  fiscal_year           SMALLINT       NOT NULL,
  fiscal_month          TINYINT        NOT NULL,
  recording_state       TINYINT        NOT NULL DEFAULT 1,
  created_by            VARCHAR(100)   NULL,
  created_by_id         VARCHAR(100)   NULL,
  created_at            DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by            VARCHAR(100)   NULL,
  updated_by_id         VARCHAR(100)   NULL,
  updated_at            DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_etc_purchase_receipt_no (receipt_no, recording_state),
  INDEX idx_etc_purchase_receipt_order (etc_purchase_order_id, recording_state),
  INDEX idx_etc_purchase_receipt_partner (partner_id, receipt_date, recording_state),
  CONSTRAINT fk_etc_purchase_receipt_order FOREIGN KEY (etc_purchase_order_id) REFERENCES etc_purchase_order (id),
  CONSTRAINT fk_etc_purchase_receipt_partner FOREIGN KEY (partner_id) REFERENCES company (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO public_code (large_code, large_name, small_code, small_name, usage_type, created_by)
SELECT '1800', '기타구매', NULL, NULL, 'GENERIC', 'seed'
FROM DUAL
WHERE NOT EXISTS (
  SELECT 1 FROM public_code
  WHERE large_code = '1800' AND small_code IS NULL AND recording_state = 1
);

INSERT INTO permission (permission_code, description) VALUES
('purchase:etc-order:read', '기타구매발주 조회'),
('purchase:etc-order:write', '기타구매발주 등록·수정·삭제'),
('purchase:etc-receipt:read', '기타구매입고 조회'),
('purchase:etc-receipt:write', '기타구매입고 등록·수정·취소')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN (
    'purchase:etc-order:read', 'purchase:etc-order:write',
    'purchase:etc-receipt:read', 'purchase:etc-receipt:write'
  )
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PURCHASE_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('purchase:etc-order:read', 'purchase:etc-receipt:read')
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;

-- END former V063__etc_purchase.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V064__dashboard_community_write_all.sql
-- ---------------------------------------------------------------------------

-- 대시보드(게시판): 로그인 사용자 전원 작성·수정·삭제
INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('community:board:write', 'community:board:moderate')
WHERE r.role_code IN ('VIEWER', 'BASIS_MANAGER', 'SALES_OPERATOR', 'PRODUCTION_OPERATOR', 'PURCHASE_OPERATOR')
  AND r.recording_state = 1;

-- END former V064__dashboard_community_write_all.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V065__board_moderate_admin_only.sql
-- ---------------------------------------------------------------------------

-- 게시판 moderate: SYSTEM_ADMIN만 유지 (타인 글 삭제·고정 방지)
DELETE rp
FROM role_permission rp
JOIN role r ON r.id = rp.role_id
JOIN permission p ON p.id = rp.permission_id
WHERE p.permission_code = 'community:board:moderate'
  AND r.role_code <> 'SYSTEM_ADMIN'
  AND r.recording_state = 1;

-- END former V065__board_moderate_admin_only.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V066__work_diary_hard_delete.sql
-- ---------------------------------------------------------------------------

-- 업무일지: 소프트삭제 잔여 데이터 정리 + 물리 삭제 정책용 유니크 키 변경
-- uk_work_diary_entry_author_date_active 는 author_user_id FK가 사용하므로 대체 인덱스를 먼저 추가한다.

DELETE FROM work_diary_entry WHERE recording_state = 0;

CREATE INDEX idx_work_diary_entry_author_user ON work_diary_entry (author_user_id);

ALTER TABLE work_diary_entry
  DROP INDEX uk_work_diary_entry_author_date_active;

ALTER TABLE work_diary_entry
  ADD UNIQUE KEY uk_work_diary_entry_author_date (author_user_id, work_date);

-- END former V066__work_diary_hard_delete.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V067__payable_approval.sql
-- ---------------------------------------------------------------------------

-- TX1-PA: 입고 지급 승인 (구매·외주·기타구매)

ALTER TABLE purchase_history
  ADD COLUMN approval_status ENUM('PENDING','APPROVED') NOT NULL DEFAULT 'PENDING' AFTER fiscal_month,
  ADD COLUMN approved_at DATETIME(3) NULL AFTER approval_status,
  ADD COLUMN approved_by_user_id BIGINT NULL AFTER approved_at,
  ADD COLUMN approval_cancelled_at DATETIME(3) NULL AFTER approved_by_user_id,
  ADD COLUMN approval_cancelled_by_user_id BIGINT NULL AFTER approval_cancelled_at,
  ADD INDEX idx_purchase_history_approval (approval_status, history_date, recording_state),
  ADD CONSTRAINT fk_purchase_history_approved_by FOREIGN KEY (approved_by_user_id) REFERENCES `user` (id),
  ADD CONSTRAINT fk_purchase_history_approval_cancelled_by FOREIGN KEY (approval_cancelled_by_user_id) REFERENCES `user` (id);

ALTER TABLE outsource_history
  ADD COLUMN approval_status ENUM('PENDING','APPROVED') NOT NULL DEFAULT 'PENDING' AFTER fiscal_month,
  ADD COLUMN approved_at DATETIME(3) NULL AFTER approval_status,
  ADD COLUMN approved_by_user_id BIGINT NULL AFTER approved_at,
  ADD COLUMN approval_cancelled_at DATETIME(3) NULL AFTER approved_by_user_id,
  ADD COLUMN approval_cancelled_by_user_id BIGINT NULL AFTER approval_cancelled_at,
  ADD INDEX idx_outsource_history_approval (approval_status, history_date, recording_state),
  ADD CONSTRAINT fk_outsource_history_approved_by FOREIGN KEY (approved_by_user_id) REFERENCES `user` (id),
  ADD CONSTRAINT fk_outsource_history_approval_cancelled_by FOREIGN KEY (approval_cancelled_by_user_id) REFERENCES `user` (id);

-- 기존 운영 데이터: 이미 원장 반영된 건은 승인 완료로 간주
UPDATE purchase_history SET approval_status = 'APPROVED' WHERE recording_state = 1;
UPDATE outsource_history SET approval_status = 'APPROVED' WHERE recording_state = 1;

INSERT INTO permission (permission_code, description) VALUES
('purchase:payable-approval:read', '입고 지급 승인 조회'),
('purchase:payable-approval:write', '입고 지급 승인·취소')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('purchase:payable-approval:read', 'purchase:payable-approval:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PURCHASE_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'purchase:payable-approval:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;

-- END former V067__payable_approval.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V068__work_diary_checklist_schema.sql
-- ---------------------------------------------------------------------------

-- COMM-3-R: 업무일지 field_schema v2 (체크리스트) — 07·14·15 그룹 시드

UPDATE work_diary_template
SET field_schema = JSON_OBJECT(
  'version', 2,
  'fields', JSON_ARRAY(
    JSON_OBJECT('key', '01', 'label', '1. 고객 품질문제 통보사항', 'type', 'textarea'),
    JSON_OBJECT('key', '02', 'label', '2. 개발 업무 현황', 'type', 'textarea'),
    JSON_OBJECT('key', '03', 'label', '3. 외주품 입고검사 부적합/반송', 'type', 'textarea'),
    JSON_OBJECT('key', '04', 'label', '4. 일일 불량 Check List', 'type', 'checklist',
      'options', JSON_ARRAY('이상무', '이상있음'), 'items', JSON_ARRAY()),
    JSON_OBJECT('key', '05', 'label', '5. 기타 보고사항 및 건의 사항', 'type', 'textarea'),
    JSON_OBJECT('key', '07', 'label', '6. 외근사항', 'type', 'textarea'),
    JSON_OBJECT('key', '10', 'label', '* 로더 공정별 확인', 'type', 'textarea')
  )
),
updated_at = CURRENT_TIMESTAMP(3)
WHERE template_code = '07' AND recording_state = 1;

UPDATE work_diary_template
SET field_schema = JSON_OBJECT(
  'version', 2,
  'fields', JSON_ARRAY(
    JSON_OBJECT('key', '01', 'label', '1. 레이져 작업 내역', 'type', 'textarea'),
    JSON_OBJECT('key', '02', 'label', '2. 레이저불량내역', 'type', 'textarea'),
    JSON_OBJECT('key', '03', 'label', '3. 레이져 절단기 체크LIST', 'type', 'checklist',
      'options', JSON_ARRAY('이상무', '이상있음'),
      'items', JSON_ARRAY(
        JSON_OBJECT('id', 'chk-1401', 'group', '아마다 절단기', 'text', '빔 자바라 상태 점검', 'sortOrder', 1),
        JSON_OBJECT('id', 'chk-1402', 'group', '아마다 절단기', 'text', '현재 출력 상태(kW)', 'sortOrder', 2),
        JSON_OBJECT('id', 'chk-1403', 'group', '아마다 절단기', 'text', '이상소음 현상', 'sortOrder', 3),
        JSON_OBJECT('id', 'chk-1404', 'group', '아마다 절단기', 'text', '가공시간(일일/전체누적)', 'sortOrder', 4),
        JSON_OBJECT('id', 'chk-1405', 'group', '바이스트로닉 절단기', 'text', '빔 자바라 상태 점검', 'sortOrder', 5),
        JSON_OBJECT('id', 'chk-1406', 'group', '바이스트로닉 절단기', 'text', '현재 출력 상태(kW)', 'sortOrder', 6),
        JSON_OBJECT('id', 'chk-1407', 'group', '바이스트로닉 절단기', 'text', '이상소음 현상', 'sortOrder', 7),
        JSON_OBJECT('id', 'chk-1408', 'group', '바이스트로닉 절단기', 'text', '가공시간(일일/전체누적)', 'sortOrder', 8),
        JSON_OBJECT('id', 'chk-1409', 'group', '플라즈마 절단기', 'text', '집진기 가동 상태', 'sortOrder', 9),
        JSON_OBJECT('id', 'chk-1410', 'group', '플라즈마 절단기', 'text', '빔 출력 상태', 'sortOrder', 10),
        JSON_OBJECT('id', 'chk-1411', 'group', '플라즈마 절단기', 'text', '가스/에어배관/냉각수 상태', 'sortOrder', 11),
        JSON_OBJECT('id', 'chk-1412', 'group', '플라즈마 절단기', 'text', '가공시간(일일/전체누적)', 'sortOrder', 12)
      )
    ),
    JSON_OBJECT('key', '07', 'label', '4. 외근사항', 'type', 'textarea'),
    JSON_OBJECT('key', '10', 'label', '* 로더 공정별 확인', 'type', 'textarea')
  )
),
updated_at = CURRENT_TIMESTAMP(3)
WHERE template_code = '14' AND recording_state = 1;

UPDATE work_diary_template
SET field_schema = JSON_OBJECT(
  'version', 2,
  'fields', JSON_ARRAY(
    JSON_OBJECT('key', '01', 'label', '1. 생산관리 관련 현황', 'type', 'textarea'),
    JSON_OBJECT('key', '02', 'label', '2. 일일 체크LIST', 'type', 'checklist',
      'options', JSON_ARRAY('이상무', '이상있음'),
      'items', JSON_ARRAY(
        JSON_OBJECT('id', 'chk-1501', 'group', '전기', 'text', '1공장 분전반 차단기 파손 및 트립 유무', 'sortOrder', 1),
        JSON_OBJECT('id', 'chk-1502', 'group', '전기', 'text', '2공장 분전반 차단기 파손 및 트립 유무', 'sortOrder', 2),
        JSON_OBJECT('id', 'chk-1503', 'group', '콤프레셔', 'text', '1공장(50hp) 이상소음 유무(가동시간)', 'sortOrder', 3),
        JSON_OBJECT('id', 'chk-1504', 'group', '콤프레셔', 'text', '2공장(75hp) 이상소음 유무(가동시간)', 'sortOrder', 4),
        JSON_OBJECT('id', 'chk-1505', 'group', '콤프레셔', 'text', '2공장(50hp) 이상소음 유무(가동시간)', 'sortOrder', 5),
        JSON_OBJECT('id', 'chk-1506', 'group', '도장라인', 'text', '파손 및 청소 상태', 'sortOrder', 6),
        JSON_OBJECT('id', 'chk-1507', 'group', '도장라인', 'text', '컨베어체인 오일 자동주유 여부', 'sortOrder', 7),
        JSON_OBJECT('id', 'chk-1508', 'group', '쇼트기', 'text', '파손 및 청소 상태', 'sortOrder', 8),
        JSON_OBJECT('id', 'chk-1509', 'group', '실린더 세척기', 'text', '파손, 누유 및 청소 상태', 'sortOrder', 9),
        JSON_OBJECT('id', 'chk-1510', 'group', '호이스트', 'text', '파손, 단락 및 작동 여부(1공장/2공장)', 'sortOrder', 10)
      )
    ),
    JSON_OBJECT('key', '03', 'label', '3. 기타 보고사항', 'type', 'textarea')
  )
),
updated_at = CURRENT_TIMESTAMP(3)
WHERE template_code = '15' AND recording_state = 1;

-- END former V068__work_diary_checklist_schema.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V069__misc_stock_movement.sql
-- ---------------------------------------------------------------------------

-- 기타 입출고 TX

CREATE TABLE IF NOT EXISTS misc_stock_movement (
  id                  BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  movement_no         VARCHAR(50)    NOT NULL,
  movement_date       DATE           NOT NULL,
  movement_direction  ENUM('IN','OUT') NOT NULL,
  item_id             BIGINT         NOT NULL,
  location_code       VARCHAR(20)    NOT NULL,
  output_process_id   BIGINT         NULL,
  qty                 DECIMAL(18, 4) NOT NULL,
  reason_code_id      BIGINT         NULL,
  note                VARCHAR(500)   NULL,
  status              ENUM('REGISTERED','CANCELLED') NOT NULL DEFAULT 'REGISTERED',
  recording_state     TINYINT        NOT NULL DEFAULT 1,
  created_by          VARCHAR(100)   NULL,
  created_by_id       VARCHAR(100)   NULL,
  created_at          DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by          VARCHAR(100)   NULL,
  updated_by_id       VARCHAR(100)   NULL,
  updated_at          DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_misc_stock_movement_no (movement_no, recording_state),
  INDEX idx_misc_stock_movement_item (item_id, movement_date, recording_state),
  INDEX idx_misc_stock_movement_date (movement_date, recording_state),
  CONSTRAINT fk_misc_stock_movement_item FOREIGN KEY (item_id) REFERENCES item (id),
  CONSTRAINT fk_misc_stock_movement_process FOREIGN KEY (output_process_id) REFERENCES process_sequence (id),
  CONSTRAINT fk_misc_stock_movement_reason FOREIGN KEY (reason_code_id) REFERENCES public_code (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO public_code (large_code, large_name, small_code, small_name, usage_type, created_by)
SELECT '1500', '입출고사유', NULL, NULL, 'GENERIC', 'seed'
FROM DUAL
WHERE NOT EXISTS (
  SELECT 1 FROM public_code
  WHERE large_code = '1500' AND small_code IS NULL AND recording_state = 1
);

INSERT INTO public_code (large_code, large_name, small_code, small_name, usage_type, created_by)
SELECT v.large_code, v.large_name, v.small_code, v.small_name, 'GENERIC', 'seed'
FROM (
  SELECT '1500' AS large_code, '입출고사유' AS large_name, '150001' AS small_code, '실사조정' AS small_name UNION ALL
  SELECT '1500', '입출고사유', '150002', '폐기' UNION ALL
  SELECT '1500', '입출고사유', '150003', '샘플' UNION ALL
  SELECT '1500', '입출고사유', '150004', '기타'
) v
WHERE NOT EXISTS (
  SELECT 1 FROM public_code pc
  WHERE pc.large_code = v.large_code
    AND pc.small_code = v.small_code
    AND pc.recording_state = 1
);

INSERT INTO permission (permission_code, description) VALUES
('inventory:misc-movement:read', '기타 입출고 조회'),
('inventory:misc-movement:write', '기타 입출고 등록·취소')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('inventory:misc-movement:read', 'inventory:misc-movement:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PURCHASE_OPERATOR', 'PRODUCTION_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'inventory:misc-movement:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;

-- END former V069__misc_stock_movement.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V070__sync_viewer_read_permissions.sql
-- ---------------------------------------------------------------------------

-- VIEWER 역할에 누락된 조회(:read) 권한 일괄 부여
-- V012 이후 추가된 permission은 개별 마이그레이션에서 VIEWER 누락이 있을 수 있음

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code LIKE '%:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;

-- END former V070__sync_viewer_read_permissions.sql


-- ---------------------------------------------------------------------------
-- BEGIN former V071__closing_fiscal_cutover_day_setting.sql
-- ---------------------------------------------------------------------------

-- 매입마감일(회계월 기준일): 거래일 day <= N → 해당 월, day > N → 익월 회계월

INSERT INTO system_settings (setting_key, value_json, created_by, created_by_id)
VALUES ('closing.fiscal_cutover_day', '"25"', 'system', 'system');

-- END former V071__closing_fiscal_cutover_day_setting.sql

