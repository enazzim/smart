# Step 1 Part 08 — 작업표준 9필드 CRUD + 표준복사

> **완료일:** 2026-07-04  
> **범위:** 작업표준 plan CRUD · `POST /plan/copy` · `ProcessDeleted` cascade · React 작업표준 화면  
> **설계 SSOT:** [`docs/step0/d4-work-standard.md`](../step0/d4-work-standard.md) v0.1  
> **다음 파트:** 사용자 CRUD · 생산달력

---

## 1. 요약

| 항목 | 결과 |
|------|------|
| Flyway | `V008__work_standard.sql` |
| API | `POST/GET/PUT/DELETE /api/v1/basis/work-standards/plan` + `POST .../plan/copy` |
| UI 입력 | 9필드 (품목·공정 FK, 작업장, 우선순위, 공구, 셋업·표준시간) |
| 적용 | 제품·공정품 · plan 공정 `INHOUSE`·`SPLIT`만 |
| UK | `(item_id, process_sequence_id, priority_order)` 활성 유일 |
| 부수효과 | **없음** · 공정 삭제 시 작업표준 cascade soft delete |
| 설비·작업자 | DB nullable · **Step 1 후속**(설비·사용자 CRUD) 전까지 null만 허용 |
| Gradle·프론트 빌드 | ✅ 성공 (2026-07-04) |

---

## 2. API 명세

Base: `/api/v1/basis/work-standards/plan`

### 2.1 등록

```json
{
  "itemNum": "P-1000",
  "processSequenceId": 101,
  "workCenterId": 3,
  "priorityOrder": 1,
  "toolName": "다이스세트 A",
  "setupTime": 10,
  "standardTime": 30
}
```

### 2.2 목록·조회

| 메서드 | 경로 | 설명 |
|--------|------|------|
| `GET` | `/plan?itemNum=` | 활성 목록 (품목번호 부분 검색, 생략 시 전체) |
| `GET` | `/plan/{id}` | ID 조회 |

### 2.3 수정·삭제

| 메서드 | 경로 | 규칙 |
|--------|------|------|
| `PUT` | `/plan/{id}` | 품목·공정 읽기 전용, UK 중복 거부 |
| `DELETE` | `/plan/{id}` | 소프트 삭제 |

### 2.4 표준복사

```
POST /api/v1/basis/work-standards/plan/copy
```

```json
{
  "sourceItemNum": "P-1000",
  "targetItemNum": "P-2000"
}
```

대상 품목의 **동일 UK 공정순서**(public_code_id + process_sequence)로 `process_sequence_id` 재매핑. UK 충돌·공정 미매칭 행 **스킵**.

응답: `{ "copied": 3 }`

### 2.5 검증

- 품목: 활성, `property_classification` ∈ {`제품`, `공정품`}
- 공정: 활성 plan, 품목 일치, `work_distinction` ∈ {`INHOUSE`, `SPLIT`}
- 작업장: 활성 FK 필수
- `setup_time`·`standard_time` ≥ 0, `priority_order` ≥ 1

---

## 3. 이벤트

| 이벤트 | Projector |
|--------|-----------|
| `WorkStandardRegistered/Updated/Deleted` | — (부수효과 없음) |

공정 삭제 시 `ProcessService.delete` → 연결 `work_standard` cascade soft delete.

---

## 4. 프론트엔드

| 파일 | 역할 |
|------|------|
| `src/api/workStandard.ts` | CRUD + copy |
| `src/pages/WorkStandardPage.tsx` | 등록·수정·품목 필터·표준복사 |
| `src/App.tsx` | **작업표준** 탭 |

공정 콤보: `GET /processes/plan?itemId=` 응답 중 INHOUSE·SPLIT 필터.

---

## 5. 수동 검증 체크리스트

- [ ] 작업표준 등록 (자가 공정)
- [ ] 외주 공정 선택 불가(콤보 제외)
- [ ] UK 중복 거부
- [ ] 수정 — 우선순위·시간 변경
- [ ] 표준복사 — 대상 품목 동일 공정 구조
- [ ] 공정 삭제 → 작업표준 cascade

---

*Part 08 · SmartManager Step 1*
