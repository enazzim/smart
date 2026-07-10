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
