# D4 — 작업장 (`WCInfo` → `WCI_MT`)

> Step 0 산출물 · **확정 v0.2** (3필드 Modal · Capa 분 · `public_code` FK 정합)  
> SSOT (레거시 감사): `KIT_ERP/BasisInformation/MasterInfoRecordRUD.cs` `m_FieldName[3]` (17필드)  
> 화면: `WCInfo.aspx` / `WCInfo.aspx.cs`  
> SmartManager: `work_center`  
> 내용키: `wc_name` ← `WCName` (UNIQUE, 활성)  
> PK: `id` ← `WCInfoIndex`

**관련:** [`step0-plan-D1-D4.md`](./step0-plan-D1-D4.md) · [`domain-event-projector-matrix.md`](./domain-event-projector-matrix.md) §10 · [`공용코드 문서`](./공용코드-콤보박스-매핑-초안-code-group-field-binding.md) §4.4 · [시스템 컬럼 규칙](../../.cursor/rules/master-audit-fields.mdc) · TO-BE [`results/sample/basis-work-center-spec.md`](../../results/sample/basis-work-center-spec.md) · [`basis-production-calendar-spec.md`](../../results/sample/basis-production-calendar-spec.md)

### v0.2 변경 (v0.1 대비)

| 항목 | v0.1 (오류) | v0.2 |
|------|-------------|------|
| 대표공정 저장 | `main_process_code` 8자리 문자열 | **`main_process_code_id`** → `public_code.id` FK |
| API 요청 | `mainProcessCode` | **`mainProcessCodeId`** |
| 콤보 저장값 | option `value` (small_code) | option **`id`** (`public_code.id`) |

### v0.2 확정 요약

| 항목 | 레거시 | SmartManager v0.2 |
|------|--------|-------------------|
| UI 입력 | 7+ 필드·2분할 | **Modal 3필드** |
| 가동시간 | `OperationTime` (시간 소수) | **`operation_time` INT 분**, 기본 **480** |
| 대표공정 | `MainProcessCode` (PUC 소분류) | **`main_process_code_id`** FK — `code_group` **`PROCESS_CODE`** 검증 |
| 보유인원·Capa구분 | 화면/DB | **서버 기본** (`retention_staff=1`, `capacity_distinction=TIME`) |
| 현상태 `State` | 사용/중지 | **Drop** — `recording_state`만 |
| 전력·시간당비용·정렬 | DB 컬럼 | **Drop** → 설비 마스터 |
| 등록 부수효과 | `ProductionCalendarTable` (기준작업장 달력 복사) | **없음** (B5-R) |

---

## 1. 분류 범례

| 분류 | 의미 |
|------|------|
| **Keep (UI)** | Modal·API 사용자 입력 |
| **서버기본** | DB 유지, 1차 UI 없음 — POST 시 서버 설정 |
| **시스템(자동)** | PK·감사·소프트삭제 |
| **Drop** | SmartManager 미구현·미이전 |
| **Phase2** | Capa `TIME_WORKERS`·보유인원 UI |

---

## 2. SmartManager 스키마 — `work_center`

### 2.1 UI 입력 3필드

| # | UI 라벨 | React field | DB 컬럼 | 타입 | 필수 | 비고 |
|---|---------|-------------|---------|------|------|------|
| 1 | 작업장명 | wcName | wc_name | string | Y | UK(활성) · 수정 가능 |
| 2 | 대표공정 | mainProcessCodeId | main_process_code_id | bigint FK | Y | → `public_code.id` |
| 3 | 일일 가동시간 | operationTime | operation_time | int | Y | **분**, 기본 480 |

**대표공정 콤보 (`code_group` 조회 · `public_code` PK 저장):**

- React: `<CodeSelect codeGroup="PROCESS_CODE" excludeReserved valueField="id" />`
- 옵션 API: `GET /api/code-groups/PROCESS_CODE/options` → `[{ id, value, label }]`
  - `id` — **`public_code.id`** (저장·요청용)
  - `value` — 8자리 `small_code` (표시·레거시 호환)
  - `label` — `small_name`
- `code_group`: `large_code=1400`, `exclude_codes`: `14000000`, `14009999` ([공용코드 문서 §3·§4.4·§6·§8](./공용코드-콤보박스-매핑-초안-code-group-field-binding.md))
- **저장:** `main_process_code_id` ← option `id` ([domain-event §10.4](./domain-event-projector-matrix.md) — 자식·마스터의 PUC 참조는 **PK FK만**)

> `results/sample`의 `?usageType=PROCESS` API·`main_process_code` VARCHAR 저장은 **미채택**. 콤보는 `code_group`, 저장은 **`public_code.id` FK**.

### 2.2 서버 기본값 (Request 미수신)

| DB 컬럼 | v0.2 값 | 비고 |
|---------|---------|------|
| retention_staff | 1 | Capa `TIME` 시 미사용 |
| capacity_distinction | `TIME` | Phase2: `TIME_WORKERS` |

### 2.3 시스템 필드

| 컬럼 | 분류 |
|------|------|
| id, recording_state, created_*, updated_* | 시스템(자동) — [공통 규칙](../../.cursor/rules/master-audit-fields.mdc) |

### 2.4 Capa 연계 (읽기 전용 설명)

일별 유효 가동분 `EffectiveMinutes(W,D)`:

1. `work_center_calendar` Override (있으면)
2. `production_calendar` 해당 일
3. **`work_center.operation_time` 폴백**

Capa (`capacity_distinction=TIME`): `Capa = EffectiveMinutes`

상세: [`basis-production-calendar-spec` §3.3](../../results/sample/basis-production-calendar-spec.md)

---

## 3. 이벤트 · Projector

| 이벤트 | v0.2 Projector | 레거시 |
|--------|----------------|--------|
| `WorkCenterRegistered` | **— (부수효과 없음)** | `ProductionCalendarTable` — 기준작업장 `PCI_MT` 복사 |
| `WorkCenterUpdated` | — | — |
| `WorkCenterDeleted` | — (Override 행 자동 삭제 **안 함**) | `PCI_MT` 유지 |

> B5-R 확정: 작업장 등록 시 **생산달력 복사 폐지**. 달력은 `/basis/production-calendars`, `/basis/work-center-calendars`에서 별도 관리.

---

## 4. API · 화면 (v0.2)

Base: `/api/v1/basis/work-centers`

| Method | Endpoint | 설명 |
|--------|----------|------|
| GET | `/?q=` | 목록·작업장명 검색 |
| GET | `/search?q=` | 타 화면 콤보용 |
| GET | `/{id}` | 상세 |
| POST | `/` | 등록 (3필드 + 서버 기본값) |
| PUT | `/{id}` | 수정 |
| DELETE | `/{id}` | 소프트 삭제 |

**POST / PUT body**

```json
{
  "wcName": "절단라인",
  "mainProcessCodeId": 201,
  "operationTime": 480
}
```

**Response (요약)** — `public_code` JOIN

```json
{
  "id": 3,
  "wcName": "절단라인",
  "mainProcessCodeId": 201,
  "mainProcessCode": "14000001",
  "mainProcessName": "절단",
  "operationTime": 480
}
```

**검증**

- `wc_name` UK (활성)
- `main_process_code_id` — `PROCESS_CODE` 그룹 활성 `public_code`, `exclude_codes`·`system_reserved` 제외
- `operation_time` ≥ 1 (권장 상한 1440)
- 삭제: `process_sequence`·`equipment` 참조 시 정책 검토

### 4.1 UI (`/basis/work-centers`)

- 거래처·품목과 동일: **검색 + 그리드 + Modal**
- 그리드: 작업장명, 대표공정(코드+명 join), 일일가동(분)
- **하단 부가 그리드 없음**

---

## 5. 레거시 17필드 대조표

| # | 레거시 컬럼 | v0.2 | 비고 |
|---|------------|------|------|
| 1 | WCName | **Keep (UI)** | 내용키 |
| 2 | MainProcessCode | **Keep (UI)** | → `main_process_code_id` FK |
| 3 | RetentionStaff | 서버기본 | 1 |
| 4 | State | **Drop** | recording_state로 통일 |
| 5 | CapacityDistinction | 서버기본 | `TIME` |
| 6 | OperationTime | **Keep (UI)** | 분 INT, ×60 이관 |
| 7 | ElectricCapacity | **Drop** | → `equipment` |
| 8 | UnitTimeUseCost | **Drop** | → `equipment` |
| 9 | Sorting | **Drop** | 정렬 `wc_name` ASC |
| 10~16 | RecodingState, 감사 | 시스템(자동) | |
| 17 | WCInfoIndex | `id` PK | |

---

## 6. `results/` 문서와의 관계

| 문서 | 채택 |
|------|------|
| `sample/basis-work-center-spec.md` v1.2 | 3필드·분·달력 복사 폐지 (**대표공정은 `public_code_id` FK로 대체** — sample VARCHAR 미채택) |
| `260624_기준정보_작업장.md` | Capa 수식·중복검증 — UI는 sample Modal 패턴 |
| `sample/basis-production-calendar-spec.md` | EffectiveMinutes·Override sparse |

---

## 7. Cut-over · Phase2

| 항목 | 처리 |
|------|------|
| `operation_time` | `ROUND(legacy_hours * 60)` → 분 |
| `MainProcessCode` | `PUC_MT` 조인 → `main_process_code_id` (`public_code.id`) |
| Drop 컬럼 | 미이전 |
| `PCI_MT` 작업장별 행 | Override만 `work_center_calendar` — 전량 복사 **안 함** |
| Phase2 | `TIME_WORKERS`, 보유인원 Modal |

---

## 8. 체크리스트

- [x] v0.2 3필드·분(480) 확정
- [x] 대표공정 **`main_process_code_id` FK** (domain-event §10 정합)
- [x] 콤보 `code_group` **`PROCESS_CODE`** + 저장 `public_code.id`
- [x] 등록 시 달력 복사 **폐지** (domain-event §2·§5 정합)
- [ ] Step 1 Flyway + work-centers CRUD
- [ ] `PUC_MT` export → `code_group` §3 `large_code` 확정 (공용코드 문서 §9)

---

*v0.2: 2026-07-02 · FK 정합 · TO-BE: `results/sample/basis-work-center-spec` (저장 방식 Step 0화) · 레거시: `MasterInfoRecordRUD.cs` m_FieldName[3]*