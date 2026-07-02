# 작업장 기준정보 확정 스펙

> **문서 버전:** 1.2  
> **작성일:** 2026-06-22  
> **상태:** 확정안 (구현 대기)  
> **관련 Wave:** B1(현행) → **B1-R** (작업장 UI·API·스키마 정비) · 선행 **S0-R** (공용코드)  
> **관련 문서:**  
> - [거래처 기준정보 확정 스펙](./basis-company-spec.md)  
> - [품목 기준정보 확정 스펙](./basis-item-spec.md)  
> - [공용코드 확정 스펙](./basis-public-code-spec.md) — 대표공정 `usage_type=PROCESS`  
> - [기준정보 API·필드 매핑](./basis-information-api-spec.md) §5.4  
> - [생산달력 확정 스펙](./basis-production-calendar-spec.md) — Capa·Override·§4.7  

---

## 1. 문서 목적

기준정보 정비 4단계로 **작업장(`work_center`)** 의 업무 정의, 데이터 모델, Capa 계산 기초, 화면·API, 현행 구현과의 갭·후속 Wave를 정리한다. **생산달력** 상세는 [생산달력 스펙](./basis-production-calendar-spec.md)을 따른다.

레거시 `WCI_MT`의 전력용량·시간당 사용료·정렬순서·현상태 등 부속 필드를 제거하고, **대표공정 연동·일일 가동시간(분)·Capa 구분** 중심으로 슬림화한다.

---

## 2. 기준정보 공통 UI 원칙

[거래처 스펙](./basis-company-spec.md) §2와 동일.

| 항목 | 확정 |
|------|------|
| 레거시 하단 부가 그리드 | **구현하지 않음** — 작업장정보테이블, 자료실, 제목, 참고보기 등 |
| 목록 화면 | 상단 검색 + 그리드 + 우측 상단 **등록(Modal)** + 행별 **수정·삭제** |
| 시스템 필드 | `id`, 등록/수정, `recoding_state` — 화면 입력 없음 |
| 삭제 | `recoding_state = 0` 소프트 삭제 |

---

## 3. 업무 정의

작업장(Work Center)은 제조 공정에서 **실질적으로 작업을 수행하는 장소**(설비 라인 또는 작업 셀)를 정의하고, 공정별 Capa(생산능력) 및 표준 가동시간 계산의 **기초 마스터**이다.

| 원칙 | 내용 |
|------|------|
| 내용키(UK) | `wc_name` (작업장명) — 활성(`recoding_state = 1`) 구간에서 유일 |
| PK | `id` (BIGINT, 레거시 `Index` 대체) |
| 대표공정 | 공용코드 `public_code` — **`usage_type = PROCESS`** 인 활성 소분류 `small_code` ([공용코드 스펙](./basis-public-code-spec.md) §4.2) |
| 사용/중지 | **`state` 컬럼 없음** — 소프트 삭제(`recoding_state`)만으로 통제 |
| 전력·비용 | 작업장이 아닌 **설비(`equipment`)** 마스터에서 관리 |
| 등록 후처리 | **B5-R:** 작업장 등록 시 달력 **복사 없음** — [생산달력 §5.1](./basis-production-calendar-spec.md) |

---

## 4. 데이터 모델

### 4.1 `work_center` — 화면 입력 필드 (3개)

| # | 화면 라벨 | API 필드 | DB 컬럼 | 타입(TO-BE) | Modal |
|---|-----------|----------|---------|-------------|:-----:|
| 1 | 작업장명 | `wcName` | `wc_name` | `VARCHAR(100)` NOT NULL | 등록·수정 |
| 2 | 대표공정 | `mainProcessCode` | `main_process_code` | `VARCHAR(20)` NOT NULL | 등록·수정 |
| 3 | 일일 가동시간 | `operationTime` | `operation_time` | **`INT` NOT NULL (분)** | 등록·수정 |

- **일일 가동시간:** **분 단위** 정수. 기본값 **480** (8시간).
- **대표공정:** 콤보박스 `ProcessCodeSelect` — `GET /api/v1/basis/public-codes?usageType=PROCESS` (활성 소분류, 더미 `14000000`·`14009999` 제외). 저장값은 `small_code` (예: `14000001`). `large_code`(`1400` 등)는 **하드코드하지 않음** — 시드·운영 대분류에 `usage_type=PROCESS`만 부여. 레거시 `ProcessIndex`(정수 FK) 대신 **코드 문자열** 유지 — 공정순서·작업표준과 동일 패턴.

### 4.2 서버 기본값 (화면 비노출)

1차 Modal에 포함하지 않으며, POST/PUT 시 서버가 설정한다.

| 컬럼 | API (내부) | 1차 기본값 | 비고 |
|------|------------|------------|------|
| `retention_staff` | — (요청 본문 미수신) | **1** | 보유인원. Capa `TIME` 구분에서는 미사용 |
| `capacity_distinction` | — | **`TIME`** | Capa 구분 코드 (§4.6) |

### 4.3 TO-BE에서 제거할 컬럼 (현행 `work_center`에 잔존)

| 컬럼 | 제거 사유 |
|------|-----------|
| `state` | 사용/중지는 `recoding_state`로 통일 (품목 `item_state` 제거와 동일) |
| `electric_capacity` | 설비 `equipment.electric_capacity`로 이관 |
| `unit_time_use_cost` | 설비 `equipment.unit_time_use_cost`로 이관 |
| `sorting` | 1차 목록 정렬 — `wc_name` 오름차순 |

### 4.4 `operation_time` 타입 변경

| 구분 | 현행 | TO-BE |
|------|------|-------|
| DB 타입 | `DECIMAL(10, 2)` | **`INT`** |
| 의미 | 시간(소수, 예: 8.0) | **분(정수, 예: 480)** |
| 기본값 | 없음(앱에서 8) | **480** |

기존 데이터 이관: `ROUND(operation_time * 60)` → 분 정수. (시드·테스트 `8.0` → `480`)

### 4.5 시스템 필드 (화면 입력 없음)

| 컬럼 | 설명 |
|------|------|
| `id` | PK |
| `created_by` / `created_at` | 등록자·등록일 |
| `updated_by` / `updated_at` | 수정자·수정일 |
| `recoding_state` | `1`=유효, `0`=삭제 |

### 4.6 Capa(생산능력) 계산 모델

백엔드 Capa·작업량 계산 엔진(PRD-W3 등)에서 참조한다. **일별 Capa**는 [생산달력 §3.3·§3.4](./basis-production-calendar-spec.md) **`EffectiveMinutes`** 를 사용한다. `operation_time`은 달력 미등록 일 **폴백** 및 마스터 기본값.

| `capacity_distinction` | 화면 라벨 | 계산식 (분) |
|------------------------|-----------|-------------|
| `TIME` | 작업시간 | `Capa = EffectiveMinutes(W, D)` |
| `TIME_WORKERS` | 작업시간×인원 | `Capa = EffectiveMinutes(W, D) × retention_staff` |

- 1차: 항상 `TIME`. `retention_staff = 1` 저장.
- 2차: Capa 구분·보유인원 Modal 노출 시 `TIME_WORKERS` 선택 가능.

### 4.7 생산달력 연계 (B5-R — [생산달력 스펙](./basis-production-calendar-spec.md))

| 항목 | B5 (현행) | B5-R (확정) |
|------|-----------|-------------|
| 트리거 | POST 작업장 → `WorkCenterRegisteredEvent` | **이벤트 제거(no-op)** |
| 동작 | `기준작업장` 달력 **전량 복사** | **복사 없음** — `production_calendar` 상속 + Override만 `work_center_calendar` |
| `operation_time` | 일일 기본 Capa | 달력 **미정의 일** 폴백 + 마스터 기본 **480분** |
| 수정·삭제 | WC 삭제 시 달력 행 유지 | 동일 — Override 행 **자동 삭제하지 않음** |

화면: `/basis/production-calendars`, `/basis/work-center-calendars`.

---

## 5. 유효성 검증

### 5.1 작업장명 중복

신규 등록 또는 작업장명 변경 시, `recoding_state = 1` 인 레코드 중 동일 `wc_name` 이 없어야 한다.

### 5.2 대표공정 유효성

`main_process_code` 는 [공용코드 스펙](./basis-public-code-spec.md) §6.1·§6.4와 동일하게 검증한다.

- `PublicCodeValidator.assertActiveProcess(smallCode)` (또는 `assertActive` + `usage_type = PROCESS`)
- `public_code.recoding_state = 1`, `small_code IS NOT NULL`
- 소분류의 `usage_type = PROCESS` (대분류 헤더와 동일)
- 더미 공정 코드(`14000000`, `14009999`) **제외**

### 5.3 일일 가동시간

- `operation_time` ≥ 1 (분)
- 상한은 운영 정책에 따라 설정 (권장: 1 ~ 1440)

### 5.4 삭제

- 공정순서(`process_sequence.work_center_id`), 설비(`equipment.work_center_id`), 생산달력 등 **참조 중**이면 삭제(비활성) 거부 또는 정책 검토 — 구현 시 FK·업무 규칙 확인.

---

## 6. 화면 (`/basis/work-centers`)

### 6.1 목록

```
┌──────────────────────────────────────────────────────────────┐
│ 작업장                                              [등록]   │
│ 작업장명 [________________]                                  │
├──────────────────────────────────────────────────────────────┤
│ 작업장명 │ 대표공정 │ 일일가동(분) │ [수정][삭제]              │
└──────────────────────────────────────────────────────────────┘
```

| 요소 | 동작 |
|------|------|
| 상단 검색 | **작업장명** — 부분 일치 필터 (비우면 전체 활성) |
| 그리드 | 작업장명, 대표공정(코드+명칭 join), 일일 가동시간(분) |
| 우측 상단 | **등록** → Modal |
| 행 우측 | **수정** / **삭제** (소프트 삭제) |

### 6.2 등록·수정 Modal

| 필드 | 필수 | 기본값 |
|------|:----:|--------|
| 작업장명 | ● | — |
| 대표공정 | ● | — (`ProcessCodeSelect`, `usageType=PROCESS`) |
| 일일 가동시간(분) | ● | **480** |

- **수정 Modal:** 위 3필드 모두 편집 가능.
- Capa 구분·보유인원·현상태·전력·정렬 — **1차 미노출**.

---

## 7. API

**Base:** `/api/v1/basis/work-centers`

| Method | Endpoint | 설명 |
|--------|----------|------|
| GET | `/` | 목록 (`?q=` 작업장명 검색, 선택) |
| GET | `/search?q=` | 타 화면용 간략 검색 |
| GET | `/{id}` | 단건 |
| POST | `/` | 등록 (+ 달력 후처리) |
| PUT | `/{id}` | 수정 |
| DELETE | `/{id}` | 소프트 삭제 |

**POST / PUT body (1차 — 화면 필드만)**

```json
{
  "wcName": "절단라인",
  "mainProcessCode": "14000001",
  "operationTime": 480
}
```

서버 자동 설정: `retentionStaff = 1`, `capacityDistinction = "TIME"` (응답에 포함 가능).

**Response 예시**

```json
{
  "id": 1,
  "wcName": "절단라인",
  "mainProcessCode": "14000001",
  "mainProcessName": "절단",
  "retentionStaff": 1,
  "capacityDistinction": "TIME",
  "operationTime": 480,
  "createdAt": "2026-06-22T00:00:00Z",
  "createdBy": "admin"
}
```

- `mainProcessName`: 목록·상세 표시용 join (선택).
- `state`, `electricCapacity`, `unitTimeUseCost`, `sorting` — **응답에서 제거**.

---

## 8. 현행 구현과의 차이 (갭)

| 항목 | 현행 | TO-BE |
|------|------|-------|
| 업무 컬럼(화면) | 6~7개 + 숨김 null 3개 | **3개** |
| 가동시간 단위 | 시간 `DECIMAL`, UI 기본 `8` | **분 `INT`**, UI 기본 **480** |
| `state` | `사용` / `ACTIVE` 등 | **컬럼 제거** |
| `capacityDistinction` | `일반` / `TIME` 혼재 | **`TIME` 고정** (서버) |
| `retentionStaff` | UI 입력 | **기본 1**, 비노출 |
| 대표공정 | `ProcessCodeSelect` (`largeCode=1400` 고정) | `usageType=PROCESS` ([공용코드 S0-R](./basis-public-code-spec.md)) |
| 전력·시간당비용·정렬 | DB nullable | **작업장에서 제거** → 설비 |
| API Request | 8필드 | **3필드** |
| 테스트 픽스처 | `"operationTime": 8.0`, `"state": "ACTIVE"` | `"operationTime": 480`, `state` 없음 |

---

## 9. 구현·후속 Wave (할 일)

### 9.1 B1-R — 작업장 (품목 B1-R 병행 또는 이후)

- [ ] Flyway: `work_center` — `state`·`electric_capacity`·`unit_time_use_cost`·`sorting` 제거
- [ ] Flyway: `operation_time` → `INT` 분 단위, 기존 값 ×60 이관
- [ ] `WorkCenter` / `WorkCenterRequest`·`WorkCenterResponse` — 3필드 요청, 기본값 서버 설정
- [ ] `WorkCenterService` — `PublicCodeValidator.assertActiveProcess`, `operationTime` 분 검증 (**S0-R 선행**)
- [ ] `ProcessCodeSelect` — `usageType=PROCESS` (`PUBLIC_CODE_LARGE_PROCESS` 상수 제거)
- [ ] `WorkCenterRepository` — 정렬 `wc_name` ASC (`sorting` 제거)
- [ ] 프론트 `WorkCentersPage` — 검색·Modal 3필드, 기본 480분
- [ ] 통합테스트 전역 — `operationTime`, `capacityDistinction`, `state` 갱신
- [ ] `V12` 시드 등 `operation_time` 값 분 단위로 수정

### 9.2 후속

- [ ] Capa 구분 `TIME_WORKERS` · 보유인원 Modal 노출 (2차)
- [ ] 작업장 삭제 시 공정순서·설비 참조 정책 확정·구현
- [ ] PRD-W3 — `EffectiveMinutes`·Capa ([생산달력](./basis-production-calendar-spec.md) §3.4)
- [ ] 본 문서를 [basis-information-api-spec.md](./basis-information-api-spec.md) §5.4와 동기화

---

## 10. 의사결정 요약

| 항목 | 확정 |
|------|------|
| 업무 필드(화면) | 작업장명, 대표공정, 일일 가동시간(분) — **3개** |
| 가동시간 | **분**, 기본 **480** |
| 현상태 `state` | **제거** — `recoding_state`만 사용 |
| 보유인원 | **기본 1**, 1차 화면 **비노출** |
| Capa 구분 | 1차 **`TIME` 고정** (서버) |
| 대표공정 | `public_code.small_code` (`usage_type = PROCESS`) |
| 전력·비용·정렬 | 작업장 **제거**, 설비 마스터 |
| 등록 후처리 | **달력 복사 없음** — [생산달력](./basis-production-calendar-spec.md) Override |
| UI | 거래처·품목과 동일 패턴, 하단 부가 영역 없음 |

---

## 11. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-06-22 | 작업장 확정안 — 3필드 Modal, 가동 분(480), state 제거, 보유인원 기본 1 |
| 1.1 | 2026-06-22 | [공용코드 스펙](./basis-public-code-spec.md) 연동 — 대표공정 `usage_type=PROCESS`, `1400` 하드코드 제거 |
| 1.2 | 2026-06-24 | §4.6 Capa → `EffectiveMinutes`; §4.7 → [생산달력](./basis-production-calendar-spec.md) B5-R (복사 폐지) |
