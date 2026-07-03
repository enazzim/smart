# Step 1 Part 04 — 공정(계획) 7필드 CRUD + WIP 잔고

> **완료일:** 2026-07-02  
> **범위:** 공정 계획 CRUD REST API · `ProcessRegistered/Updated/Deleted` · `WipBalanceProjector` · React 공정 화면  
> **설계 SSOT:** [`docs/step0/d4-process.md`](../step0/d4-process.md) v0.1  
> **다음 파트:** Part 05 — 외주단가 + `OutsourceInputBalanceProjector` ✅ [`part-05-unit-price-api.md`](./part-05-unit-price-api.md)

---

## 1. 요약

| 항목 | 결과 |
|------|------|
| Flyway | `V005__process_inventory.sql` — `inventory_location`, `work_center`, `process_sequence`, `inventory_balance` |
| API | `POST/GET/PUT/DELETE /api/v1/basis/processes/plan` |
| 보조 API | `GET /api/v1/basis/code-groups/PROCESS_CODE/options`, `GET /api/v1/basis/work-centers` |
| 부수효과 | `ProcessRegistered` → `WipBalanceProjector.ensure` → `inventory_balance` (location=`WIP`) |
| 이벤트 | `ProcessRegistered` / `ProcessUpdated` / `ProcessDeleted` |
| Gradle·프론트 빌드 | 성공 |
| E2E | 등록·목록·수정(SPLIT)·삭제 + WIP 잔고 생성·비활성 확인 |

---

## 2. API 명세

### 2.1 공정 등록

```
POST /api/v1/basis/processes/plan
```

```json
{
  "itemId": 1,
  "processSequenceNum": 10,
  "processCodeId": 12,
  "workDistinction": "INHOUSE",
  "workCenterId": 1,
  "outsideOrderRate": 0,
  "progressRate": 100
}
```

### 2.2 목록·조회

| 메서드 | 경로 | 설명 |
|--------|------|------|
| `GET` | `/api/v1/basis/processes/plan?itemId=` | 품목별 활성 공정 목록 (순번 오름차순) |
| `GET` | `/api/v1/basis/processes/plan/{id}` | ID 조회 |

### 2.3 수정·삭제

| 메서드 | 경로 | 규칙 |
|--------|------|------|
| `PUT` | `/api/v1/basis/processes/plan/{id}` | UK `(item_id, public_code_id, process_sequence)` 중복 거부 |
| `DELETE` | `/api/v1/basis/processes/plan/{id}` | `recording_state=0` + WIP 잔고 비활성 |

### 2.4 작업구분 (`workDistinction`)

| 코드 | 작업장 | 발주비율 |
|------|--------|----------|
| `INHOUSE` | 필수 | 0 |
| `OUTSOURCE` | null | 0 |
| `SPLIT` | 필수 | 1~99 |

### 2.5 검증 규칙

- 품목 `property_classification` ∈ {`제품`, `공정품`}
- `processSequenceNum` ≠ 99, 1~98
- 공정코드: `PROCESS` 활성 소분류, `14000000`·`14009999` 제외

### 2.6 보조 API

| 메서드 | 경로 | 설명 |
|--------|------|------|
| `GET` | `/api/v1/basis/code-groups/PROCESS_CODE/options` | 공정 콤보 (exclude_codes 반영) |
| `GET` | `/api/v1/basis/work-centers` | 작업장 콤보 |

---

## 3. DB (V005)

| 테이블 | 역할 |
|--------|------|
| `inventory_location` | RAW/SALES/DELIVERY/WIP/OUTSOURCE 시드 |
| `work_center` | 작업장 마스터 + `절단라인` 시드 |
| `process_sequence` | 계획 공정 (`variant=plan`) |
| `inventory_balance` | WIP OUTPUT 잔고 (`output_process_id` FK) |

---

## 4. 주요 파일

### domain

| 파일 | 설명 |
|------|------|
| `domain/process/WorkDistinction.java` | INHOUSE / OUTSOURCE / SPLIT |
| `domain/process/ProcessVariant.java` | plan / actual |
| `domain/event/EventTypes.java` | `PROCESS_*` 이벤트 상수 |

### application

| 파일 | 설명 |
|------|------|
| `application/process/ProcessService.java` | 검증·CRUD·이벤트 |
| `application/process/WipBalanceProjector.java` | WIP 잔고 ensure / deactivate |
| `application/code/CodeGroupOptionsRepository.java` | 공정코드 옵션 |

### infrastructure · api

| 레이어 | 패키지 |
|--------|--------|
| infrastructure | `persistence/process/*`, `persistence/inventory/*`, `persistence/workcenter/*`, `persistence/code/*` |
| api | `web/process/ProcessController`, `web/code/CodeGroupController`, `web/workcenter/WorkCenterController` |

### 프론트

| 파일 | 설명 |
|------|------|
| `src/App.tsx` | 거래처 / 품목 / **공정** 탭 |
| `src/pages/ProcessPage.tsx` | 7필드 CRUD + 품목 필터 |
| `src/api/process.ts` | API 클라이언트 |

---

## 5. 검증 (2026-07-02)

| 시나리오 | 결과 |
|----------|------|
| POST INHOUSE 공정 | `process_sequence` 1행, `inventory_balance` WIP 1행, `ProcessRegistered` |
| PUT → SPLIT (발주 30%) | `ProcessUpdated`, 필드 반영 |
| DELETE | 목록 0건, WIP `recording_state=0`, `ProcessDeleted` |
| 원자재 품목 등록 시도 | 400 거부 |
| 순번 99 등록 시도 | 400 거부 |

---

## 6. 실행

```powershell
cd smartmanager_backend
.\gradlew.bat :smartmanager-api:bootRun

cd smartmanager_frontend
npm run dev
```

브라우저 `http://localhost:5173` → **공정** 탭

---

## 7. 미구현 (후속)

| 항목 | 비고 |
|------|------|
| `POST /processes/plan/copy` | 설계에 있음, Part 04 범위 외 |
| `actual` variant CRUD | B4 compare 전까지 제한 |
| 거래처 목록 검색 `?q=` | Part 02 백로그 |
