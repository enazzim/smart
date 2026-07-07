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

