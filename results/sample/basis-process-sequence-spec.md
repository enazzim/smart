# 제품·공정품 공정순서 확정 스펙

> **문서 버전:** 1.7  
> **작성일:** 2026-06-22  
> **상태:** 확정안 (구현 대기)  
> **관련 Wave:** B2(현행) → **B2-R** (공정순서 UI·API·검증 정비)  
> **관련 문서:**  
> - [기준정보 API·필드 매핑](./basis-information-api-spec.md) **§5.5 (필드·API SoT)**  
> - [품목 기준정보 확정 스펙](./basis-item-spec.md) §4.2  
> - [작업장 기준정보 확정 스펙](./basis-work-center-spec.md)  
> - [공용코드 확정 스펙](./basis-public-code-spec.md)  
> - [기준정보 구현 설계](./basis-information-implementation-spec.md)

---

## 1. 문서 목적·적용 범위

본 문서는 **`property_classification` 이 `제품` 또는 `공정품` 인 품목**에 대해서만 공정순서(Routing, `process_sequence`)를 정의한다.

| 구분 | 내용 |
|------|------|
| **적용 대상** | `제품`, `공정품` — 생산·가공 라우팅이 필요한 품목 |
| **적용 제외** | `원자재`, `상품` — 공정순서 행을 **두지 않음** (화면·API 모두) |
| **API·필드 SoT** | [basis-information-api-spec.md](./basis-information-api-spec.md) **§5.5** 와 동일 |
| **본 문서 역할** | §5.5에 없는 **적용 품목·검증·UI·Wave** 확정 |
| **운영 variant** | **`plan`** (`process_sequence`). `actual`(`real_process_sequence`)은 compare·확인용 |

레거시 `PSI_MT` / `RPSI_MT`. React는 `ProcessesPage` 단일 컴포넌트로 `plan` / `actual` 전환.

---

## 2. 기준정보 공통 UI 원칙

[거래처 스펙](./basis-company-spec.md) §2와 동일.

| 항목 | 확정 |
|------|------|
| 레거시 하단 부가 그리드 | **구현하지 않음** |
| 화면 패턴 | **제품·공정품 품목 선택** + 공정 그리드 + **등록/수정 Modal** + 행별 삭제 |
| 시스템 필드 | `id`, 등록/수정, `recoding_state` — 화면 입력 없음 |
| 삭제 | `recoding_state = 0` 소프트 삭제 |

---

## 3. 업무 정의

### 3.1 제품·공정품 라우팅

| 자산분류 | 공정순서 | 비고 |
|----------|:--------:|------|
| **제품** | ● | 완제품 가공 경로. 최종 공정 후 영업창고(`SALES`) 등 |
| **공정품** | ● | 반제·공정 중간품 경로. WIP 슬롯·상위 BOM 소비 |
| **원자재** | ✕ | BOM **자품**으로만 소비. `PSI` 행 없음 |
| **상품** | ✕ | 매입·판매 품목. 사내 가공 라우팅 없음 |

### 3.2 식별자·내용키·공정코드

| 구분 | 내용 |
|------|------|
| **PK** | **`id`** (BIGINT AUTO_INCREMENT) |
| **내용키 (UK)** | **`(item_id, public_code_id, process_sequence)`** — 활성(`recoding_state = 1`) 구간에서 유일 |
| **순번 `99`** | **사용 금지** ([§5.5](./basis-information-api-spec.md)) |
| **`public_code_id`** | [공용코드](./basis-public-code-spec.md) **`usage_type = PROCESS`** 활성 소분류 → `public_code.id` FK |
| **공정 콤보** | `GET /api/v1/basis/public-codes?usageType=PROCESS` — 라벨 **`small_name`**, 요청 **`processCodeId`** |
| **더미 공정** | `14000000`, `14009999` — 등록·콤보 **제외** |

**내용키 해석**

- `item_id` — 라우팅 대상 품목 (`제품`·`공정품`)
- `public_code_id` — 공정 마스터(공용코드 PROCESS 소분류)
- `process_sequence` — 해당 품목 내 공정 진행 순서(10, 20, 30 …)

동일 품목에 **같은 순번·같은 공정코드** 조합은 1건만 허용한다.

### 3.3 PK · 하위 FK

| 구분 | 규칙 |
|------|------|
| **PK** | `process_sequence.id` — API·PUT/DELETE **`id`** / `/{id}` |
| **신규 POST** | `id` **미포함** — INSERT 후 응답 `id` |
| **공정 마스터** | 요청 **`processCodeId`** → `public_code_id` (PK와 별개) |
| **하위 FK** | `work_standard.process_sequence_id` → **`process_sequence.id`**; `work_standard_id` → **`work_standard.id`** |

> **현행 DB:** `uk_process_sequence_item_seq (item_id, process_sequence)` 만 존재 — B2-R에서 **내용키 UK로 교체** ([§9.1](#91-b2-r)).

---

## 4. API·필드 (§5.5 준수)

**엔드포인트·필드 정의는 [basis-information-api-spec.md §5.5](./basis-information-api-spec.md) 를 따른다.** 아래는 KIT_ERP DB·Modal 매핑만 보충한다.

### 4.1 REST API (§5.5 인용)

| Method | Endpoint | plan | actual |
|--------|----------|:----:|:------:|
| GET | `/api/v1/basis/processes/{variant}/{itemNum}` | ● | ● |
| GET | `/api/v1/basis/processes/{variant}?itemNum=` | ● (B2-R) | ● (B2-R) |
| POST | `/api/v1/basis/processes/{variant}` | ● | △ |
| PUT | `/api/v1/basis/processes/{variant}/{id}` | ● | △ |
| DELETE | `/api/v1/basis/processes/{variant}/{id}` | ● | △ |
| POST | `/api/v1/basis/processes/plan/copy` | ● | — |
| GET | `/api/v1/basis/processes/compare?itemNum=` | — | ● |

### 4.2 화면 입력 필드 (§5.5 ↔ DB)

| 화면 라벨 | 필수 | API 필드 (요청) | API 필드 (응답) | DB (`process_sequence`) | Modal |
|-----------|:----:|-----------------|-----------------|-------------------------|:-----:|
| (시스템) | | — | **`id`** | **`id`** (PK) | — |
| 품목 | ● | `itemId` | `itemId`, `itemNum`, `itemName` | `item_id` (FK) | 등록·수정 — `ItemSelect`, 값=**id** |
| 순서번호 | ● | `processSequenceNum` | `processSequenceNum` | `process_sequence` | 등록·수정 |
| 공정 | ● | `processCodeId` | `processCodeId`, `processCode`, `processName` | `public_code_id` (FK) | `ProcessCodeSelect`, 값=**id** (§5.8) |
| 작업구분 | ● | `workDistinction` | `workDistinction` | `work_distinction` | 등록·수정 |
| 작업장 | ●* | `workCenterId` | `workCenterId`, `wcName` | `work_center_id` (FK) | 조건부 — `WorkCenterSelect` (§5.7) |
| 발주비율 | ● | `outsideOrderRate` | `outsideOrderRate` | `outside_order_rate` | 조건부 (§5.2) |
| 진척비율 | ● | `progressRate` | `progressRate` | `progress_rate` | 등록·수정 (기본 100) |
| 리드타임 | | `leadTime` | `leadTime` | `lead_time` | **1차 비노출** |
| 기타 | | `etcText` | `etcText` | `etc_text` | **1차 비노출** |

> **요청 body** FK: `itemId`, **`processCodeId`**, `workCenterId`. **`id`** 는 응답·PUT/DELETE `/{id}` 용.

\* §5.5: **외주 시 `workCenterId` 생략/null**. 혼합(`SPLIT`)·자가(`INHOUSE`) 시 작업장 **필수** — §5.2·§5.7.

`real_process_sequence` (actual) 컬럼 구조 동일. actual CRUD는 B4 compare 전까지 **제한** (현행 원칙).

---

## 5. §5.5 외 확정 사항 (검증·코드값)

### 5.1 품목 자산분류 (적용 범위 강제)

| 계층 | 규칙 |
|------|------|
| **화면** | 품목 선택·검색 API — `property_classification IN ('제품','공정품')` **고정** |
| **서버** | POST/PUT 시 `itemId` → 활성 `item`, `property_classification` 이 `제품`·`공정품` 아니면 **400 거부** |
| **데이터** | 원자재·상품에 기존 `process_sequence` 행이 있으면 B2-R 마이그레이션 시 정리 |

### 5.2 `work_distinction` 코드값 (§5.5 보완)

API 스펙은 필드명만 정의. **저장 코드값**은 아래로 통일한다.

| 코드 | 화면 라벨 | `workCenterId` | `outsideOrderRate` | `leadTime` (2차) |
|------|-----------|:--------------:|:------------------:|:----------------:|
| `INHOUSE` | 자가 | **필수** (콤보) | `0` (입력 비활성) | — |
| `OUTSOURCE` | 외주 | **생략/null** | `0` (입력 비활성) | 필수(2차) |
| `SPLIT` | 자가/외주 | **필수** (콤보) | **1~99** (발주비율 %) | 필수(2차) |

> 현행 UI `사내`/`외주`·테스트 `INHOUSE`/`OUTSOURCE` → B2-R에서 위 3코드로 통일.  
> `SPLIT` 은 §5.5 `outsideOrderRate`·`workCenterId` 조합의 **업무 해석**이며, API 필드 추가는 없음.

### 5.3 공정순번

- `process_sequence` ≠ **99**
- 권장: **10 단위** (10, 20, 30 …) — 중간 삽입(15 등) 여지

### 5.4 공정코드·내용키 검증

- `PublicCodeValidator.assertActiveProcessId(processCodeId)` — `usage_type = PROCESS` 활성 소분류
- 활성 건에서 **`(item_id, public_code_id, process_sequence)` 중복 불가** (§3.2 내용키)
- 등록·수정 시 `public_code_id`·`process_sequence` 변경으로 UK 충돌 시 거부

### 5.5 자가/외주 수량 분할 (PRD — 마스터만)

`work_distinction = SPLIT`, `outside_order_rate = R`(%)일 때, **작업계획·작업지시** 단계에서 1공정 → 자가·외주 2지시로 분할한다.

**예:** 총 200개, 외주비율 40% → 자가 120, 외주 80.

- B2-R: **비율 저장만**. 분할 실행은 **PRD-W** Wave.

### 5.6 WIP 재고 (plan 등록·수정)

`plan` 저장 시 `ProcessDefinedEvent` → `inventory_balance` WIP 슬롯 ensure (`제품`·`공정품`, `원자재` 제외).  
actual 등록 시 이벤트 **미발행** (현행).

### 5.7 작업장 (콤보 · FK)

| 계층 | 규칙 |
|------|------|
| **화면** | 등록·수정 Modal — **`WorkCenterSelect`** (SearchableSelect). `INHOUSE`·`SPLIT` 일 때만 활성 |
| **데이터** | `GET /api/v1/basis/work-centers` — 활성 작업장 |
| **표시** | 작업장명 `wc_name` (라벨). 그리드·응답에도 `wcName` join |
| **저장** | 선택한 **`work_center.id`** → `process_sequence.work_center_id` FK |
| **API 요청** | `workCenterId` (BIGINT). `OUTSOURCE` → **생략 또는 null** |
| **API 응답** | `workCenterId`, `wcName` (join 표시) |
| **검증** | `workCenterId` 가 있으면 활성 `work_center` 존재. `INHOUSE`·`SPLIT` 에서 **필수** |

```
[Modal WorkCenterSelect] → workCenterId → process_sequence.work_center_id FK
[그리드 / Response]      ← work_center join → wcName 표시
```

> §5.5 레거시 필드명 `wcName` 은 **응답·표시**용. **등록·수정 body**는 **`workCenterId`**. [작업장 스펙](./basis-work-center-spec.md)·[설비 스펙](./basis-equipment-spec.md) §5.3과 동일 패턴.

### 5.8 공정 마스터 (공용코드 · FK)

| 계층 | 규칙 |
|------|------|
| **화면** | **`ProcessCodeSelect`** — `usageType=PROCESS`, 더미 `14000000`/`14009999` 제외 |
| **표시** | 소분류명 **`small_name`** |
| **저장** | `public_code.id` → `process_sequence.public_code_id` FK |
| **API 요청** | **`processCodeId`** (BIGINT, 필수) |
| **API 응답** | `processCodeId`, `processCode`, `processName`, **`id`** (PK) |

---

## 6. 화면 (`/basis/processes/plan`)

### 6.1 목록 — 제품·공정품만

```
┌────────────────────────────────────────────────────────────────────────────────────┐
│ 제품·공정품 공정순서 (plan)                            [공정복사] [등록]         │
│ 품목 [SearchableSelect — 제품·공정품만, 비우면 전체]                             │
├────────────────────────────────────────────────────────────────────────────────────┤
│ 품목번호 │ 품목명 │ 순번 │ 공정코드 │ 공정명(소분류명) │ 작업구분 │ 작업장 │ …   │
└────────────────────────────────────────────────────────────────────────────────────┘
```

| 요소 | 동작 |
|------|------|
| 상단 품목 필터 | **제품·공정품** Select. **비우면** 해당 분류 **전체** 공정순서 |
| 품목 입력 시 | `itemNum` 조건 **AND** — 해당 품목 라우팅만 |
| 목록 API | `GET .../processes/plan?itemNum=` (`itemNum` 생략 시 전체, B2-R) 또는 `GET .../plan/{itemNum}` |
| 그리드 컬럼 | **품목번호**, **품목명**, 순번, 공정코드, 공정명(`public_code.small_name` join), 작업구분, 작업장, 발주%, 진척% |
| 정렬 | `item_num` → `process_sequence` **오름차순** |
| 등록/수정 | Modal — §4.2 필드, §5.2 동적 활성/비활성 |
| 공정복사 | Modal — `POST .../plan/copy` (§4.1) |
| `actual` | 동일 UI, badge「확인용」— plan 대비 제한적 수정 |

내용키 `(item_id, public_code_id, process_sequence)` 이므로 그리드에 **품목번호·품목명·공정명** 컬럼 **필수**.

품목 API에 다중 분류 필터가 없으면 B2-R에서 `propertyClassifications=제품,공정품` 쿼리 추가.

### 6.2 등록 Modal (§5.5 필드)

| 필드 | 비고 |
|------|------|
| 품목 | `ItemSelect` — 제품·공정품, 값=**`itemId`**. 수정 시 읽기 전용 |
| 순서번호 | 기본 `10` |
| 공정 | `ProcessCodeSelect` — 라벨 `small_name`, 값 **`processCodeId`** (§5.8) |
| 작업구분 | `INHOUSE` / `OUTSOURCE` / `SPLIT` |
| 작업장 | `WorkCenterSelect` — **콤보**, 값=`workCenterId`, 라벨=`wcName`. INHOUSE·SPLIT만 (§5.7) |
| 발주비율 | SPLIT만 (§5.5 `outsideOrderRate`) |
| 진척비율 | 기본 `100` |

`leadTime`, `etcText` — 1차 Modal **미포함** (§4.2).

---

## 7. API 요청 예시 (§5.5 필드)

**POST** `/api/v1/basis/processes/plan`

```json
{
  "itemId": 50,
  "processSequenceNum": 10,
  "processCodeId": 201,
  "workDistinction": "SPLIT",
  "workCenterId": 3,
  "outsideOrderRate": 40,
  "progressRate": 100
}
```

**Response (요약)**

```json
{
  "id": 101,
  "itemId": 50,
  "itemNum": "P-1000",
  "itemName": "완제품A",
  "processSequenceNum": 10,
  "processCodeId": 201,
  "processCode": "14000001",
  "processName": "절단",
  "workDistinction": "SPLIT",
  "workCenterId": 3,
  "wcName": "절단라인",
  "outsideOrderRate": 40,
  "progressRate": 100
}
```

- `itemId` 품목이 `원자재`·`상품`이면 거부.
- `workDistinction`: `OUTSOURCE` → `workCenterId` 생략 또는 `null`.

**공정복사** — §5.5 `POST .../plan/copy`

```json
{
  "sourceItemId": 50,
  "targetItemId": 60
}
```

대상 품목도 **제품·공정품**만. 대상에 동일 **내용키** `(public_code_id, process_sequence)` 활성 행 있으면 스킵.

---

## 8. 현행 구현과의 차이 (갭)

| 항목 | 현행 | TO-BE |
|------|------|-------|
| 목록 | 품목 필터 필수·그리드에 품목 없음 | **품목번호·품목명** 그리드 컬럼, 필터 비움 시 전체 |
| 작업구분 | `사내`/`외주`, `INHOUSE`/`OUTSOURCE` 혼재 | **+ `SPLIT`**, 3코드 통일 |
| 내용키 UK | `(item_id, process_sequence)` 만 | **`(item_id, public_code_id, process_sequence)`** |
| 공정 FK | `processCode` 문자열 | **`processCodeId`** → `public_code_id`; 응답 **`id`** (PK) |
| 품목 요청 | `itemNum` 문자열 | **`itemId` FK** |
| 작업장 | `wcName` 문자열 입력 | **`WorkCenterSelect`** + `workCenterId` FK |
| 공정복사 | `sourceItemNum` / `targetItemNum` | **`sourceItemId` / `targetItemId`** |
| 공정코드 | `1400` 하드코드 | `usageType=PROCESS` |
| 품목 분류 검증 | 없음 | POST/PUT **`itemId`** 기준 **필수** |
| `leadTime`/`etcText` | UI null | 1차 비노출 유지 |

---

## 9. 구현·후속 Wave

### 9.1 B2-R

- [ ] Flyway: PK **`id`** 유지; `public_code_id` FK; UK `(item_id, public_code_id, process_sequence)`
- [ ] `GET .../processes/plan?itemNum=` — 미입력 시 제품·공정품 **전체** 목록 (`itemNum`·`itemName` 포함)
- [ ] 품목 API — `propertyClassifications` 필터 (제품·공정품)
- [ ] `ProcessSequenceRequest` — `itemId`, **`processCodeId`**, `workCenterId`; 복사 `sourceItemId` / `targetItemId`
- [ ] `ProcessSequenceService` — `itemId`·`processCodeId`·`workCenterId` resolve·자산분류·UK·검증
- [ ] `work_distinction` 3코드, SPLIT·§5.2 동적 검증
- [ ] 프론트 `ProcessesPage` — 품목 필터·**그리드 품목 컬럼**, `WorkCenterSelect`, Modal, `usageType=PROCESS`
- [ ] 툴바 **공정복사** Modal
- [ ] 통합테스트 갱신
- [ ] 본 문서와 §5.5 정합 유지 (필드 변경 시 **api-spec 먼저**)

### 9.2 후속

- [ ] `leadTime` Modal (OUTSOURCE·SPLIT 필수)
- [ ] PRD-W — SPLIT 수량 분할
- [ ] 삭제 시 작업표준·생산 참조 검사

---

## 10. 의사결정 요약

| 항목 | 확정 |
|------|------|
| **문서 주제** | **제품·공정품** 공정순서만 |
| API·필드 SoT | **basis-information-api-spec §5.5** |
| variant | 운영 **plan** |
| 제외 품목 | **원자재·상품** — 행·화면 없음 |
| **내용키** | `(item_id, public_code_id, process_sequence)` |
| **PK** | **`id`** |
| **하위 FK** | `work_standard.process_sequence_id` → **`process_sequence.id`** |
| 작업구분 | `INHOUSE` / `OUTSOURCE` / `SPLIT` (§5.5 필드 조합) |
| 작업장 | **`WorkCenterSelect`**, 요청 `workCenterId` → `work_center_id` FK |
| **품목 (요청)** | POST/PUT **`itemId`** → `item_id` FK |
| UI | 제품·공정품 품목 필터 + **그리드에 품목번호·품목명** + Modal |

---

## 11. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-06-22 | 제품·공정품 전용 — §5.5 SoT, 적용범위·SPLIT·UI 고정 필터 |
| 1.1 | 2026-06-22 | §3.2 내용키 `(item_id, process_code, process_sequence)`, 공정 콤보 `small_name`/`small_code` |
| 1.2 | 2026-06-22 | §6.1 그리드 **품목번호·품목명** 컬럼, 품목 필터 비움 시 전체 목록 |
| 1.3 | 2026-06-24 | POST/PUT·복사 — `itemNum` → **`itemId`**, `sourceItemId` / `targetItemId` |
| 1.4 | 2026-06-24 | §5.7 작업장 — `WorkCenterSelect`, 요청 `workCenterId` FK (`wcName` 응답·표시만) |
| 1.5 | 2026-06-24 | §3.3·§5.8 — 요청 `processCodeId` FK; UK `public_code_id` |
| 1.6 | 2026-06-24 | PK `process_id` (→ v1.7에서 **`id`** 로 환원) |
| 1.7 | 2026-06-24 | PK **`id`** 통일; 하위 FK `process_sequence_id` → `process_sequence.id` |
