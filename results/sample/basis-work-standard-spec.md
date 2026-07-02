# 작업표준 기준정보 확정 스펙

> **문서 버전:** 1.4  
> **작성일:** 2026-06-24  
> **상태:** 확정안 (구현 대기)  
> **관련 Wave:** B2(현행) → **B2-R** (작업표준 UI·API·스키마 정비)  
> **관련 문서:**  
> - [기준정보 API·필드 매핑](./basis-information-api-spec.md) **§5.7 (필드·API SoT)**  
> - [제품·공정품 공정순서 확정 스펙](./basis-process-sequence-spec.md)  
> - [작업장 기준정보 확정 스펙](./basis-work-center-spec.md)  
> - [설비 기준정보 확정 스펙](./basis-equipment-spec.md)  
> - [공용코드 확정 스펙](./basis-public-code-spec.md)  
> - [생산실적→작업일보 매핑](./production-work-report-mapping.md)  
> - [기준정보 구현 설계](./basis-information-implementation-spec.md) §3.3

---

## 1. 문서 목적·적용 범위

**작업표준(`work_standard`, 레거시 `WSI_MT`)** 은 자가 생산 공정별로 작업장·설비·작업자·공구 등 **표준 작업 조건**과 **셋업·표준 가공 시간**을 정의한다. 원가 산출·생산 일정(Scheduling)의 기초 데이터이며, **작업일보 등록 시 기본값으로 대입**된다 (§5.5).

| 구분 | 내용 |
|------|------|
| **적용 대상** | `제품`·`공정품` 품목의 **자가·혼합 공정** (`INHOUSE`, `SPLIT`) |
| **적용 제외** | 순수 **외주** 공정 (`OUTSOURCE`) — 작업표준 행·등록 **없음** |
| **슬림화** | 레거시·§5.7 부속 필드 → **핵심 10필드** (§4.2) |
| **API·필드 SoT** | [basis-information-api-spec.md](./basis-information-api-spec.md) **§5.7** |
| **본 문서 역할** | §5.7에 없는 **UK·process_sequence_id FK·시간 단위·우선순위·작업일보 연계·UI·Wave** 확정 |
| **운영 variant** | **`plan`** (`work_standard`). `actual`(`real_work_standard`)은 compare·확인용 |

레거시 `WSI_MT` / `RWSI_MT`. React는 `WorkStandardPage` 단일 컴포넌트로 `plan` / `actual` 전환.

---

## 2. 기준정보 공통 UI 원칙

[거래처 스펙](./basis-company-spec.md) §2와 동일.

| 항목 | 확정 |
|------|------|
| 레거시 하단 부가 그리드 | **구현하지 않음** |
| 목록 화면 | 상단 검색·필터 + 그리드 + 우측 상단 **등록(Modal)** + 행별 **수정·삭제** |
| 시스템 필드 | `id`, 등록/수정, `recoding_state` — 화면 입력 없음 |
| 삭제 | `recoding_state = 0` 소프트 삭제 |

[공정순서 스펙](./basis-process-sequence-spec.md) §6과 동일한 **평면 목록 + Modal** 패턴. 2분할 트리 UI는 **사용하지 않음**.

---

## 3. 업무 정의

### 3.1 등록 대상 (품목·공정)

| 계층 | 규칙 |
|------|------|
| **품목** | `property_classification IN ('제품','공정품')` — 공정순서 스펙 §5.1과 동일 |
| **공정** | plan `process_sequence` 행 중 **`work_distinction IN ('INHOUSE','SPLIT')`** 만 `process_sequence_id` 로 연결 |
| **외주** | `OUTSOURCE` 공정 — 콤보·POST **제외** |

### 3.2 식별자·내용키

| 구분 | 내용 |
|------|------|
| **PK** | **`id`** (BIGINT AUTO_INCREMENT) — PUT/DELETE **`/{id}`** |
| **내용키 (UK)** | **`(item_id, process_sequence_id, priority_order)`** — 활성 구간 유일 |
| **`process_sequence_id`** | FK → **`process_sequence.id`** |
| **표시용 join** | `processSequenceNum`, `processCode`, `processName` — FK join |

동일 품목·동일 공정(`process_sequence_id`)에 **우선순위만 다른** 작업표준을 **여러 건** 등록할 수 있다.

> **현행 DB:** B2-R에서 **`process_sequence_id` FK**, UK **`(item_id, process_sequence_id, priority_order)`** 로 교체.

### 3.3 우선순위 (기본 표준 선택)

| 규칙 | 내용 |
|------|------|
| **값** | `priority_order` ≥ 1 정수. **1 = 최우선(1순위)** |
| **UK** | 동일 `(item_id, process_sequence_id)` 내 **priority 중복 불가** |
| **기본 채택** | 복수 표준 중 **`priority_order` 가 가장 작은** 행 = **우선순위가 가장 높은** 표준 |

생산 일정·Capa 연산·**작업일보 기본 대입** 모두 위 규칙을 따른다 (§5.5).

---

## 4. API·필드

**엔드포인트·필드 정의는 [basis-information-api-spec.md §5.7](./basis-information-api-spec.md) 를 따른다.** 아래는 KIT_ERP DB·Modal·B2-R 슬림 매핑이다.

### 4.1 REST API (§5.7)

| Method | Endpoint | plan | actual |
|--------|----------|:----:|:------:|
| GET | `/api/v1/basis/work-standards/{variant}/{itemNum}` | ● | ● |
| GET | `/api/v1/basis/work-standards/{variant}?itemNum=` | ● (B2-R) | ● (B2-R) |
| POST | `/api/v1/basis/work-standards/{variant}` | ● | △ |
| PUT | `/api/v1/basis/work-standards/{variant}/{id}` | ● | △ |
| DELETE | `/api/v1/basis/work-standards/{variant}/{id}` | ● | △ |
| POST | `/api/v1/basis/work-standards/plan/copy` | ● | — |
| GET | `/api/v1/basis/work-standards/compare?itemNum=` | — | ● |

### 4.2 화면 입력 필드 (1차 슬림 · 10필드)

| # | 화면 라벨 | 필수 | API 필드 (요청) | API 필드 (응답) | DB 컬럼 | Modal |
|---|-----------|:----:|-----------------|-----------------|---------|:-----:|
| — | (시스템) | | — | **`id`** | **`id`** (PK) | — |
| 1 | 품목 | ● | `itemId` | `itemId`, `itemNum`, `itemName` | `item_id` (FK) | `ItemSelect` (§5.1) |
| 2 | 공정 | ● | `processSequenceId` | `processSequenceId`, `processSequenceNum`, `processCodeId`, `processCode`, `processName` | `process_sequence_id` (FK) | `ProcessSelect` (§5.2) |
| 3 | 작업장 | ● | `workCenterId` | `workCenterId`, `wcName` | `work_center_id` (FK) | 등록·수정 — 콤보 |
| 4 | 사용설비 | | `equipmentId` | `equipmentId`, `equipmentName` | `equipment_id` (FK, NULL) | 등록·수정 — 콤보 |
| 5 | 우선순위 | ● | `priorityOrder` | `priorityOrder` | `priority_order` | 등록·수정, 기본 **1** |
| 6 | 주작업자 | | `mainWorkerId` | `mainWorkerId`, `mainWorkerName` | `main_worker_user_id` (FK, NULL) | 등록·수정 — 콤보 |
| 7 | 사용공구 | | `toolName` | `toolName` | `tool_name` | 등록·수정 |
| 8 | 셋업시간 | ● | `setupTime` | `setupTime` | `setup_time` | 등록·수정, 단위 **분** |
| 9 | 표준시간 | ● | `standardTime` | `standardTime` | `standard_time` | 등록·수정, 단위 **초** |

> **요청 body** FK: `itemId`, `processSequenceId`, `workCenterId` 등. **행 식별**은 **`id`** (PUT/DELETE `/{id}`).

### 4.3 §5.7·현행 DB에서 1차 제거하는 컬럼

| 제거 대상 |
|-----------|
| `process_sequence` (INT), `process_code` (VARCHAR) — **`process_sequence_id` FK** 로 대체 |
| `main_worker` (VARCHAR), `main_worker_id` (VARCHAR) — **`main_worker_user_id`** FK 로 대체 |
| `tool_name2`, `tool_name3`, `jig_name1` ~ `jig_name3` |
| `cavity`, `real_processing_time`, `space_time`, `wait_time`, `lot_size` |
| `setup_time` / `standard_time` 의 **DECIMAL** — **INT** (분/초) |

### 4.4 시스템 필드 (화면 입력 없음)

| 컬럼 | API | 설명 |
|------|-----|------|
| **`id`** | `id` | PK — PUT/DELETE `/{id}` |
| `created_by` / `created_at` | — | 등록 |
| `updated_by` / `updated_at` | — | 수정 |
| `recoding_state` | — | `1`=유효, `0`=삭제 |

---

## 5. §5.7 외 확정 사항

### 5.1 품목·공정 검증

| 계층 | 규칙 |
|------|------|
| **품목** | POST/PUT 시 `itemId` → 활성 `item`, `property_classification` 이 `제품`·`공정품` 아니면 **400** |
| **`process_sequence_id`** | plan `process_sequence` 활성 행이어야 함 |
| **품목 일치** | `process_sequence.item_id` = 요청 `item_id` — 불일치 시 **400** |
| **작업구분** | 해당 공정 `work_distinction` ∈ `{INHOUSE, SPLIT}` — `OUTSOURCE` 이면 **400** |

### 5.2 공정 콤보 (`ProcessSelect`)

| 계층 | 규칙 |
|------|------|
| **데이터** | 선택 품목의 plan `process_sequence` 중 **INHOUSE·SPLIT** 만 |
| **표시** | `순번 · 공정명(small_name) · 작업구분` |
| **값** | `processSequenceId` (= `process_sequence.id`) |
| **API** | `GET /api/v1/basis/processes/plan?itemId=` (B2-R) 또는 선택 품목의 `itemNum` 경로 |

품목 변경 시 공정 콤보 **재로드**. 수정 Modal 에서 품목·공정 **읽기 전용**.

### 5.3 작업장·설비·작업자 (FK 콤보)

| 필드 | 콤보 API | 표시 | 저장 |
|------|----------|------|------|
| 작업장 | `GET /api/v1/basis/work-centers` | `wcName` | `workCenterId` → `work_center_id` |
| 설비 | `GET /api/v1/basis/equipment` | `equipmentName` | `equipmentId` → `equipment_id` (nullable) |
| 주작업자 | `GET /api/v1/basis/users` (활성) | `userName` | `mainWorkerId` → `app_user.id` (nullable) |

### 5.4 시간 단위·소요 시간 연산

| 필드 | 단위 | 타입 | 기본값 |
|------|------|------|--------|
| `setup_time` | **분** | INT ≥ 0 | **0** |
| `standard_time` | **초** | INT ≥ 0 | **0** |

작업지시 수량 \(Q\) 에 대한 총 소요 시간 \(T\) (초):

\[
T = (\text{setupTime} \times 60) + (\text{standardTime} \times Q)
\]

**예:** 표준 10초, 셋업 10분, 수량 400 → \(600 + 4000 = 4600\)초 (약 76.6분).  
후속 PRD-Wave에서 작업장 일일 가동시간·Capa와 결합해 리드타임 일수 산출.

입력칸 단위 가이드: 셋업 **(분)**, 표준 **(초)**.

### 5.5 작업일보 등록 시 작업표준 대입 (PRD 연계)

작업표준은 **마스터**이며, **작업일보(`work_report`)** 등록 시 아래 규칙으로 **기본값을 자동 대입**한다. ([production-work-report-mapping.md](./production-work-report-mapping.md) `work_standard_id` 참조)

| 단계 | 규칙 |
|------|------|
| **대상 조회** | 작업지시의 품목·공정(`process_sequence_id`)에 해당하는 plan `work_standard` 활성 행 |
| **표준 선택** | 그중 **`priority_order` 최소**(= 우선순위 최고) **1건** |
| **대입 필드** | **`work_standard_id`** (= `work_standard.id`), `setup_time`, `standard_time`, … |
| **수정** | 화면에서 사용자가 **변경 가능** — 일보 실적은 입력값 기준 전기 |
| **외주** | `OUTSOURCE` 공정은 작업일보·작업표준 대상 **아님** |

```
[작업일보 신규] → work_order.item + process
              → work_standard WHERE process_sequence_id = ? AND recoding_state = 1
              → ORDER BY priority_order ASC LIMIT 1
              → work_standard_id + 셋업·표준시간·작업자 기본값
```

> B2-R: 작업표준 마스터만 구현. 작업일보 대입 로직은 **PRD-W** Wave에서 `WorkReportService` 등에 구현.

### 5.6 plan / actual·복사

- 운영 SoT: **plan**
- `actual` CRUD는 compare 전까지 **제한** (공정순서와 동일)
- `POST .../plan/copy` — `sourceItemId` / `targetItemId`. `process_sequence_id` 는 대상 품목의 **동일 UK 공정순서 행**으로 **재매핑**

---

## 6. 화면 (`/basis/work-standards/plan`)

### 6.1 목록 — 제품·공정품·자가 공정

```
┌────────────────────────────────────────────────────────────────────────────────────────┐
│ 작업표준 (plan)                                              [표준복사] [등록]         │
│ 품목 [SearchableSelect — 제품·공정품만, 비우면 전체]                                   │
├────────────────────────────────────────────────────────────────────────────────────────┤
│ 품목번호 │ 품목명 │ 순번 │ 공정명 │ 작업장 │ 설비 │ 우선순위 │ 셋업(분) │ 표준(초) │ … │
└────────────────────────────────────────────────────────────────────────────────────────┘
```

| 요소 | 동작 |
|------|------|
| 상단 품목 필터 | **제품·공정품** Select. **비우면** 전체 작업표준 |
| 목록 API | `GET .../work-standards/plan?itemNum=` (B2-R) 또는 `GET .../plan/{itemNum}` |
| 그리드 | §4.2 + 공정명·작업장명 join. **우선순위** 컬럼 표시 |
| 정렬 | `item_num` → `process_sequence` → `priority_order` **오름차순** |
| 등록/수정 | Modal — §4.2. 행 식별·수정·삭제는 **`id`** |
| 표준복사 | Modal — `POST .../plan/copy` |
| `actual` | 동일 UI, badge「확인용」|

UK에 `process_sequence_id`·`item_id` 가 모두 있으므로 그리드에 **품목번호·품목명·공정 순번·공정명** 컬럼 **필수**.

### 6.2 등록 Modal

| 필드 | 필수 | 기본값 |
|------|:----:|--------|
| 품목 | ● | `ItemSelect` — 제품·공정품, 값=**`itemId`** |
| 공정 | ● | `ProcessSelect` — 해당 품목 **INHOUSE·SPLIT** 만 |
| 작업장 | ● | `WorkCenterSelect` |
| 사용설비 | | `EquipmentSelect` |
| 우선순위 | ● | **1** |
| 주작업자 | | `UserSelect` |
| 사용공구 | | — |
| 셋업시간 (분) | ● | **0** |
| 표준시간 (초) | ● | **0** |

수정 Modal: 품목·공정 **읽기 전용**. UK 키 변경 불가 → 우선순위 변경 시 UK 충돌 검증.

---

## 7. API 요청·응답 예시

**POST** `/api/v1/basis/work-standards/plan`

```json
{
  "itemId": 50,
  "processSequenceId": 101,
  "workCenterId": 3,
  "equipmentId": 5,
  "priorityOrder": 1,
  "mainWorkerId": 12,
  "toolName": "다이스세트 A",
  "setupTime": 10,
  "standardTime": 30
}
```

**Response**

```json
{
  "id": 1,
  "itemId": 50,
  "itemNum": "P-1000",
  "itemName": "완제품A",
  "processSequenceId": 101,
  "processSequenceNum": 10,
  "processCode": "14000001",
  "processName": "절단",
  "workCenterId": 3,
  "wcName": "절단라인",
  "equipmentId": 5,
  "equipmentName": "1500톤 사출기",
  "priorityOrder": 1,
  "mainWorkerId": 12,
  "mainWorkerName": "홍길동",
  "toolName": "다이스세트 A",
  "setupTime": 10,
  "standardTime": 30
}
```

**복사** — `POST .../plan/copy`

```json
{
  "sourceItemId": 50,
  "targetItemId": 60
}
```

---

## 8. 현행 구현과의 차이 (갭)

| 항목 | 현행 | TO-BE |
|------|------|-------|
| UI | 품목별 단순 목록 | **공정순서형** 평면 그리드 + 품목 필터 + Modal |
| **UK** | `(item_id, process_sequence, priority_order)` | **`(item_id, process_sequence_id, priority_order)`** |
| 공정 참조 | `process_sequence`+`process_code` 중복 | **`process_sequence_id` FK** → **`process_sequence.id`** |
| 등록 대상 | 작업구분 검증 없음 | **INHOUSE·SPLIT만** |
| 작업장 | `wcName` 문자열 | **`workCenterId` FK** |
| 설비 | 없음 | **`equipmentId` FK** (nullable) |
| 주작업자 | VARCHAR id + 텍스트 | **`mainWorkerId` → `app_user.id` FK** |
| 공구·치구 | `toolName1~3` + jig 3개 | **`toolName` 1개** |
| 시간 | DECIMAL | **셋업 분(INT)·표준 초(INT)** |
| 작업일보 | 미연계 | **최소 priority 표준 자동 대입** (§5.5, PRD-W) |

---

## 9. 구현·후속 Wave

### 9.1 B2-R — 작업표준

- [ ] Flyway: PK **`id`** 유지; `process_sequence_id` FK → `process_sequence(id)`; UK `(item_id, process_sequence_id, priority_order)`
- [ ] Flyway: `equipment_id`, `main_worker_user_id` FK; `setup_time`/`standard_time` INT; §4.3 컬럼 제거
- [ ] `WorkStandardService` — 품목분류·작업구분·`process_sequence_id`·UK·FK 검증
- [ ] `WorkStandardRequest` — `itemId`, `processSequenceId`, `workCenterId`, `equipmentId`, `mainWorkerId`, INT 시간
- [ ] `GET .../work-standards/plan?itemNum=` — 품목 필터 비움 시 전체 목록
- [ ] 프론트 `WorkStandardPage` — 공정순서형 UI, `ProcessSelect`·콤보 3종, Modal
- [ ] [basis-information-api-spec.md](./basis-information-api-spec.md) §5.7 슬림 동기화
- [ ] [basis-information-implementation-spec.md](./basis-information-implementation-spec.md) §3 UK 갱신

### 9.2 후속

- [ ] **PRD-W** — 작업일보 등록 시 §5.5 최우선 표준 자동 대입 (`work_standard_id`)
- [ ] PRD-W — \(T\) 기반 스케줄·Capa 리드타임
- [ ] 삭제 시 작업지시·작업일보 `work_standard_id` 참조 검사

---

## 10. 의사결정 요약

| 항목 | 확정 |
|------|------|
| 대상 품목 | **제품·공정품** |
| 대상 공정 | **자가(`INHOUSE`)·혼합(`SPLIT`)** 만 |
| **UK** | **`(item_id, process_sequence_id, priority_order)`** |
| **PK** | **`id`** |
| **공정 FK** | **`process_sequence_id`** → `process_sequence.id` |
| 우선순위 | **1 = 최우선**; 동일 공정 복수 표준 허용 |
| 작업일보 | 등록 시 **priority 최소(순서 최고) 표준 자동 대입** |
| 시간 | 셋업 **분**, 표준 **초** |
| FK | 작업장·설비·작업자 **id 저장** |
| UI | **거래처·공정순서형** 평면 목록 + Modal |
| API SoT | **basis-information-api-spec §5.7** |

---

## 11. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-06-24 | 확정안 — UK, 슬림 10필드, 작업일보 대입, 평면 UI |
| 1.3 | 2026-06-24 | PK `work_standard_id` (→ v1.4에서 **`id`** 환원) |
| 1.4 | 2026-06-24 | PK **`id`**; FK **`process_sequence_id`**; UK `(item_id, process_sequence_id, priority_order)` |
