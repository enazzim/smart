# 설비 기준정보 확정 스펙

> **문서 버전:** 1.2  
> **작성일:** 2026-06-22  
> **상태:** 확정안 (구현 대기)  
> **관련 Wave:** B5(현행) → **B5-R** (설비 UI·API·스키마 정비)  
> **관련 문서:**  
> - [기준정보 API·필드 매핑](./basis-information-api-spec.md) **§5.6 (필드·API SoT)**  
> - [작업장 기준정보 확정 스펙](./basis-work-center-spec.md)  
> - [공용코드 확정 스펙](./basis-public-code-spec.md)  
> - [기준정보 구현 설계](./basis-information-implementation-spec.md)

---

## 1. 문서 목적·적용 범위

공장 내 가공·생산에 사용하는 **설비 마스터(`equipment`, 레거시 `EI_MT`)** 의 업무 정의, 8대 핵심 필드 슬림화, 샷(Shot) 수명 관리, 화면·API, 현행 구현과의 갭·후속 Wave를 정리한다.

| 구분 | 내용 |
|------|------|
| **슬림화** | 레거시 20+ 부속 필드 제거 → **핵심 업무 필드 + 샷 3컬럼** |
| **API·필드 SoT** | [basis-information-api-spec.md](./basis-information-api-spec.md) **§5.6** (엔드포인트·필드명) |
| **본 문서 역할** | §5.6에 없는 **샷 연산·교체 알림·설비분류 공용코드·작업장 NULL·UI** 확정 |
| **이력 `REI_MT`** | 1차 **미구현** — 2차 `equipment_history` 검토 |

---

## 2. 기준정보 공통 UI 원칙

[거래처 스펙](./basis-company-spec.md) §2와 동일.

| 항목 | 확정 |
|------|------|
| 레거시 하단 부가 그리드 | **구현하지 않음** |
| 목록 화면 | 상단 검색 + 그리드 + 우측 상단 **등록(Modal)** + 행별 **수정·삭제** |
| 시스템 필드 | `id`, 등록/수정, `recoding_state` — 화면 입력 없음 |
| 삭제 | `recoding_state = 0` 소프트 삭제 |

---

## 3. 업무 정의

설비는 작업장에 배치(또는 미배치)되어 가동되며, **설계 샷(수명 한계)** 대비 **누계 샷**으로 교체 시기를 판단한다.

| 원칙 | 내용 |
|------|------|
| PK | `id` (BIGINT, 레거시 `Index` 대체) |
| 내용키 (UK) | `equipment_num` (설비번호) — 활성(`recoding_state = 1`) 구간 유일 |
| 작업장 | `work_center_id` FK → `work_center.id` — **NULL 허용**. 화면은 콤보 선택, **id로 저장** (§5.3) |
| 설비분류 | `equipment_category_id` FK → `public_code.id` — **`usage_type = EQUIPMENT`** 소분류만 (§5.1) |
| 현상태 | **`equipment_state` 제거** — 사용/중지는 `recoding_state`만 |

---

## 4. API·필드

**엔드포인트는 §5.6을 따른다.** 아래는 KIT_ERP DB·Modal·§5.6 슬림화 매핑이다.

### 4.1 REST API (§5.6)

| Method | Endpoint |
|--------|----------|
| GET | `/api/v1/basis/equipment` |
| GET | `/api/v1/basis/equipment/{id}` |
| POST | `/api/v1/basis/equipment` |
| PUT | `/api/v1/basis/equipment/{id}` |
| DELETE | `/api/v1/basis/equipment/{id}` |

목록 `GET` — 선택 쿼리 `?q=` (설비번호·설비명 부분 일치, B2-R).

### 4.2 화면 입력 필드 (8대 핵심)

| # | 화면 라벨 | API 필드 | DB 컬럼 | Modal | 비고 |
|---|-----------|----------|---------|:-----:|------|
| 1 | 설비번호 | `equipmentNum` | `equipment_num` | 등록·수정 | UK, **수정 불가** |
| 2 | 설비명 | `equipmentName` | `equipment_name` | 등록·수정 | |
| 3 | 설비분류 | `equipmentClassificationId` | `equipment_category_id` (FK) | 등록·수정 | 콤보 **소분류명**, **`public_code.id` 저장** (§5.1) |
| 4 | 작업장 | `workCenterId` | `work_center_id` (FK) | 등록·수정 | 콤보 선택, **선택 id 저장** — 비우면 NULL (§5.3) |
| 5 | 설계샷 | `designShot` | `design_shot` | 등록·수정 | 수명 한계 횟수, ≥ 0 |
| 6 | 초기샷 | `initialShot` | `initial_shot` | 등록·수정 | 도입 시점 기사용, 기본 **0** |
| 7 | 작업샷 | `workShot` | `work_shot` | **표시만** | 가동 누적, 기본 **0**, 생산 연동으로 증가 |
| 8 | 누계샷 | `accumulatedShot` | `accumulated_shot` | **표시만** | 서버 연산 (§5.2) |

### 4.3 §5.6·현행 DB에서 1차 제거하는 컬럼

레거시 `EI_MT` 부속 필드. B5-R Flyway에서 **DROP 또는 미사용**.

| 제거 대상 |
|-----------|
| `equipment_classification` (VARCHAR) | → **`equipment_category_id`** FK 로 대체 (B5-R Flyway) |
| `capacity`, `electric_capacity`, `unit_time_use_cost` |
| `standard`, `unit`, `location` |
| `injection_staff_num`, `occupancy_area` |
| `buying_date`, `buying_cost`, `instrument_num` |
| **`equipment_state`** |
| `check_date`, `valid_period`, `check_agency` |
| `ready_time`, `cavity` |
| `manage_period1`~`manage_period4`, `manage_date1`~`manage_date4` |

> 작업장 스펙에서 전력·시간당비용을 설비로 이관할 여지가 있었으나, 첨부 확정안 **8필드에 미포함** — 1차 설비에서도 **제외**.

### 4.4 시스템 필드 (화면 입력 없음)

| 컬럼 | 설명 |
|------|------|
| `id` | PK |
| `created_by` / `created_at` | 등록자·등록일 |
| `updated_by` / `updated_at` | 수정자·수정일 |
| `recoding_state` | `1`=유효, `0`=삭제 |

---

## 5. §5.6 외 확정 사항

### 5.1 설비분류 (공용코드 · FK)

| 계층 | 규칙 |
|------|------|
| **대상** | `public_code` — `usage_type = EQUIPMENT`, `small_code IS NOT NULL`, `recoding_state = 1` |
| **화면** | 등록·수정 Modal — **`EquipmentClassificationSelect`** (SearchableSelect) |
| **콤보 데이터** | `GET /api/v1/basis/public-codes?usageType=EQUIPMENT` |
| **표시** | 소분류명 **`small_name`** (라벨). 그리드·응답에도 `equipmentClassificationName` |
| **저장** | 선택 행의 **`public_code.id`** → `equipment.equipment_category_id` FK |
| **API 요청** | `equipmentClassificationId` (BIGINT, **필수**) |
| **API 응답** | `equipmentClassificationId`, `equipmentClassificationName`; 선택 `smallCode` join (표시·레거시 대조용) |
| **검증** | id가 활성 소분류이며 `usage_type = EQUIPMENT` 인지 확인 |

```
[EquipmentClassificationSelect] → equipmentClassificationId → equipment_category_id FK → public_code.id
[그리드 / Response]               ← public_code join → small_name 표시
```

> [공용코드 스펙](./basis-public-code-spec.md)은 일반적으로 `small_code` 문자열 참조이나, **설비분류는 `public_code.id` FK** 로 확정 (작업장 `work_center_id` 와 동일 패턴).  
> §5.6 레거시 API 필드명 `equipmentClassification`(문자열)은 **응답의 `smallCode` join** 또는 B5-R 이관 시 대체.

시드: `usage_type=EQUIPMENT` 대분류 + 소분류 — S0-R/B5-R.

### 5.2 샷(Shot) 연산·교체 판정

첨부 §3.1. **누계 샷 = 초기 샷 + 작업 샷** (초기 샷 ≠ 수명 임계).

```
accumulated_shot = initial_shot + work_shot
```

| 규칙 | 내용 |
|------|------|
| 음수 금지 | `design_shot`, `initial_shot`, `work_shot` ≥ 0 (정수) |
| 등록 시 | `work_shot = 0`, `accumulated_shot = initial_shot` |
| `initial_shot` 변경 | 수정 시 `accumulated_shot` **재계산** |
| `work_shot` 증가 | **생산실적·가동 집계** (PRD/TX2 후속) — 1차는 컬럼·표시만 |
| 교체 임계 | `design_shot > 0` 이고 `accumulated_shot >= design_shot` → **교체 필요** |
| `design_shot = 0` | 교체 판정 **비활성** (한계 미설정) |

응답 필드 `replacementDue` (boolean) — 그리드 배지용.

### 5.3 작업장 (콤보 · FK)

| 계층 | 규칙 |
|------|------|
| **화면** | 등록·수정 Modal — **`WorkCenterSelect`** (SearchableSelect). 활성 작업장 목록 `GET /api/v1/basis/work-centers` |
| **표시** | 작업장명 `wc_name` (라벨). 그리드·응답에도 `wcName` join 표시 |
| **저장** | 선택한 작업장의 **`work_center.id`** → `equipment.work_center_id` FK |
| **API 요청** | `workCenterId` (BIGINT, nullable). 미선택·null → `work_center_id = NULL` |
| **API 응답** | `workCenterId`, `wcName` (표시용 join) |
| **검증** | `workCenterId` 가 있으면 활성 `work_center` 존재 여부 확인 |
| **필수 여부** | **선택** — §5.6 WC 필수(●) 대비 **본 문서에서 완화** |

```
[Modal WorkCenterSelect] → workCenterId → equipment.work_center_id FK
[그리드 / Response]      ← work_center join → wcName 표시
```

> §5.6 레거시 필드명 `wcName` 은 **응답·표시**용으로 유지. **등록·수정 body**는 B5-R에서 **`workCenterId`** 로 통일 (이름 문자열 직접 입력 금지).

### 5.4 설비번호 UK

활성 `equipment_num` 중복 불가. 등록 후 설비번호 **수정 불가**.

---

## 6. 화면 (`/basis/equipment`)

### 6.1 목록

```
┌─────────────────────────────────────────────────────────────────────────────┐
│ 설비                                                               [등록]   │
│ 설비번호·설비명 [________________]                                          │
├─────────────────────────────────────────────────────────────────────────────┤
│ 설비번호 │ 설비명 │ 설비분류 │ 작업장 │ 설계샷 │ 누계샷 │ 작업샷 │ 교체 │ … │
└─────────────────────────────────────────────────────────────────────────────┘
```

| 요소 | 동작 |
|------|------|
| 검색 | 설비번호·설비명 부분 일치 (`?q=`) |
| 그리드 | §4.2 + 설비분류명 join, **교체 필요** 배지 (`replacementDue`) |
| 등록/수정 | Modal — §4.2 입력 필드 |
| 작업장 | `WorkCenterSelect` — **콤보박스**, 값=`workCenterId`, 라벨=`wcName` (§5.3) |
| 설비분류 | `EquipmentClassificationSelect` — 콤보 **`small_name`**, 값=`equipmentClassificationId` (§5.1) |

### 6.2 등록 Modal

| 필드 | 필수 | 기본값 |
|------|:----:|--------|
| 설비번호 | ● | — |
| 설비명 | ● | — |
| 설비분류 | ● | — |
| 작업장 | | — (콤보 **미선택** → `workCenterId` null) |
| 설계샷 | ● | — |
| 초기샷 | ● | **0** |

수정 Modal: 설비번호 읽기 전용, 작업샷·누계샷 **읽기 전용** 표시. 작업장·설비분류는 등록과 동일 **콤보**.

---

## 7. API 요청·응답 예시

**POST / PUT body (1차)**

```json
{
  "equipmentNum": "EQ-001",
  "equipmentName": "1500톤 사출기",
  "equipmentClassificationId": 42,
  "workCenterId": 3,
  "designShot": 500000,
  "initialShot": 12000
}
```

- `workCenterId` 생략 또는 `null` → `work_center_id = NULL`.
- 서버: `workShot = 0`, `accumulatedShot = initial_shot`.

**Response (목록·상세)**

```json
{
  "id": 1,
  "equipmentNum": "EQ-001",
  "equipmentName": "1500톤 사출기",
  "equipmentClassificationId": 42,
  "equipmentClassificationName": "사출기",
  "smallCode": "15000001",
  "workCenterId": 3,
  "wcName": "사출라인",
  "designShot": 500000,
  "initialShot": 12000,
  "workShot": 0,
  "accumulatedShot": 12000,
  "replacementDue": false
}
```

---

## 8. 현행 구현과의 차이 (갭)

| 항목 | 현행 | TO-BE |
|------|------|-------|
| 업무 필드 | 5~6개 UI + null 15개 POST | **8핵심** Modal |
| `initial_shot` / `work_shot` / `accumulated_shot` | 없음 (`design_shot` nullable만) | **3컬럼 추가** |
| 작업장 | `wcName` 문자열 입력·NOT NULL FK | **콤보** + `workCenterId` → **`work_center_id` FK** (NULL 허용) |
| 설비분류 | 자유 텍스트 `일반` / `equipment_classification` VARCHAR | **콤보** + `equipmentClassificationId` → **`equipment_category_id` FK** |
| `equipment_state` | UI `사용` | **제거** |
| 교체 알림 | 없음 | `accumulated_shot >= design_shot` |
| 이력 REI_MT | 없음 | 2차 `equipment_history` |
| §5.6 필드 목록 | 20+ 컬럼 | **슬림 반영 필요** (api-spec 동기화) |

---

## 9. 구현·후속 Wave

### 9.1 B5-R — 설비

- [ ] Flyway: `equipment_category_id` FK → `public_code(id)`; `equipment_classification` VARCHAR **제거**
- [ ] Flyway: `initial_shot`, `work_shot`, `accumulated_shot` NOT NULL DEFAULT 0; `work_center_id` NULL 허용
- [ ] Flyway: §4.3 레거시 컬럼 제거
- [ ] 공용코드 `usage_type=EQUIPMENT` + 설비분류 시드
- [ ] `Equipment` 엔티티 — 8필드 + 샷 컬럼
- [ ] `EquipmentRequest` — `equipmentClassificationId`, `workCenterId` (nullable); Response — id + name join
- [ ] `EquipmentService` — `public_code`·`WorkCenter` id resolve → FK; 샷·`replacementDue`·검증
- [ ] 프론트 — `EquipmentClassificationSelect`(id), `WorkCenterSelect`(id), Modal·그리드
- [ ] [basis-information-api-spec.md](./basis-information-api-spec.md) §5.6 슬림 필드 동기화

### 9.2 후속

- [ ] `equipment_history` (`REI_MT`) — 변경 이력
- [ ] `work_shot` — 생산실적·TX2 연동 증가
- [ ] 삭제 시 생산·정비 참조 검사

---

## 10. 의사결정 요약

| 항목 | 확정 |
|------|------|
| 내용키 | `equipment_num` |
| 핵심 필드 | 번호·명·분류·작업장(선택)·설계샷·초기샷 + 작업샷·누계샷(시스템) |
| 교체 기준 | **누계샷 ≥ 설계샷** (`design_shot > 0`) |
| 설비분류 | **콤보** `small_name` 표시, **`equipment_category_id` → `public_code.id` FK** |
| 작업장 | **콤보** `WorkCenterSelect`, **`work_center_id` FK** (NULL 허용) |
| `equipment_state` | **제거** |
| UI | 거래처형 평면 목록 + Modal |
| API SoT | **basis-information-api-spec §5.6** |

---

## 11. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-06-22 | 설비 확정안 — 8필드 슬림, 샷 연산·교체 알림, EQUIPMENT 공용코드, B5-R |
| 1.1 | 2026-06-22 | §5.3 작업장 — 콤보 선택, `workCenterId` → `work_center_id` FK |
| 1.2 | 2026-06-22 | §5.1 설비분류 — 콤보 `small_name`, `equipmentClassificationId` → `equipment_category_id` FK |
