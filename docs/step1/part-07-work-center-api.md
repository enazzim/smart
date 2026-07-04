# Step 1 Part 07 — 작업장 3필드 CRUD

> **완료일:** 2026-07-04  
> **범위:** 작업장 CRUD REST API · `WorkCenterRegistered/Updated/Deleted` · React 작업장 화면  
> **설계 SSOT:** [`docs/step0/d4-work-center.md`](../step0/d4-work-center.md) v0.2  
> **다음 파트:** 작업표준 CRUD·copy, 설비 CRUD

---

## 1. 요약

| 항목 | 결과 |
|------|------|
| Flyway | `V005__process_inventory.sql` — `work_center` (Part 04에서 생성, 시드 `절단라인`) |
| API | `POST/GET/PUT/DELETE /api/v1/basis/work-centers` |
| UI 입력 | `wcName`, `mainProcessCodeId`, `operationTime`(분, 기본 480) |
| 서버 기본 | `retention_staff=1`, `capacity_distinction=TIME` |
| 부수효과 | **없음** (생산달력 복사 폐지, B5-R) |
| 이벤트 | `WorkCenterRegistered` / `WorkCenterUpdated` / `WorkCenterDeleted` |
| Gradle·프론트 빌드 | ✅ 성공 (2026-07-04) |

---

## 2. API 명세

Base: `/api/v1/basis/work-centers`

### 2.1 등록

```
POST /api/v1/basis/work-centers
```

```json
{
  "wcName": "절단라인",
  "mainProcessCodeId": 14000010,
  "operationTime": 480
}
```

### 2.2 목록·조회

| 메서드 | 경로 | 설명 |
|--------|------|------|
| `GET` | `/api/v1/basis/work-centers?q=` | 활성 목록, 작업장명 부분 검색 |
| `GET` | `/api/v1/basis/work-centers/{id}` | ID 조회 |

**Response (요약)** — `public_code` JOIN

```json
{
  "id": 1,
  "wcName": "절단라인",
  "mainProcessCodeId": 14000010,
  "mainProcessCode": "14000010",
  "mainProcessName": "절단",
  "operationTime": 480,
  "createdAt": "2026-07-02T00:00:00Z"
}
```

### 2.3 수정·삭제

| 메서드 | 경로 | 규칙 |
|--------|------|------|
| `PUT` | `/api/v1/basis/work-centers/{id}` | UK `wc_name`(활성) 중복 거부 |
| `DELETE` | `/api/v1/basis/work-centers/{id}` | `recording_state=0` 소프트 삭제 |

### 2.4 검증 규칙

- `wcName` — 필수, 활성 행 UK
- `mainProcessCodeId` — `PROCESS_CODE` 그룹 활성 `public_code.id`, `14000000`·`14009999` 제외
- `operationTime` — 1~1440 (분)
- 삭제 — `process_sequence`에서 참조 중이면 거부

### 2.5 보조 API (공정 콤보)

| 메서드 | 경로 | 설명 |
|--------|------|------|
| `GET` | `/api/v1/basis/code-groups/PROCESS_CODE/options` | 대표공정 콤보 |

---

## 3. 도메인 · 이벤트

| 이벤트 | Aggregate | Projector |
|--------|-----------|-----------|
| `WorkCenterRegistered` | `WorkCenter` | — (부수효과 없음) |
| `WorkCenterUpdated` | `WorkCenter` | — |
| `WorkCenterDeleted` | `WorkCenter` | — |

---

## 4. 백엔드 구조

| 레이어 | 주요 클래스 |
|--------|-------------|
| application | `WorkCenterService`, `WorkCenterCommand`, `WorkCenterView`, `WorkCenterRepository` |
| infrastructure | `JpaWorkCenterRepository`, `WorkCenterJpaEntity`, `WorkCenterApplicationService` |
| api | `WorkCenterController`, `CreateWorkCenterRequest`, `WorkCenterResponse` |
| config | `WorkCenterApplicationConfig` |

기존 `JpaWorkCenterLookup` — 공정 CRUD용 `WorkCenterLookup` (exists/name) 유지.

---

## 5. 프론트엔드

| 파일 | 역할 |
|------|------|
| `src/api/workCenter.ts` | CRUD API 클라이언트 |
| `src/pages/WorkCenterPage.tsx` | 등록·수정·검색·그리드 |
| `src/App.tsx` | **작업장** 탭 추가 |

공정 화면 콤보용 `fetchWorkCenters()` (`src/api/process.ts`) — 동일 엔드포인트, 응답 필드 확장 호환.

---

## 6. 수동 검증 체크리스트

- [ ] 작업장 등록 — 3필드, 기본 가동 480분
- [ ] 목록·`?q=` 검색
- [ ] 수정 — 대표공정·가동시간 변경
- [ ] 삭제 — 미참조 작업장
- [ ] 삭제 거부 — 공정 계획에 연결된 작업장
- [ ] UK — 동일 작업장명 중복 등록 거부
- [ ] 공정 화면 — 작업장 콤보 정상

---

## 7. 관련 문서

- [`d4-work-center.md`](../step0/d4-work-center.md) — v0.2 3필드·FK·달력 복사 폐지
- [`part-04-process-api.md`](./part-04-process-api.md) — `work_center` 테이블·공정 연동

---

*Part 07 · SmartManager Step 1*
