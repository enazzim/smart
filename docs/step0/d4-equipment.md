# D4 — 설비 (`EquipmentInfo` → `EI_MT`)

> Step 0 산출물 · **확정 v0.1** (6필드 Modal + 샷 표시 2 · `public_code` FK 정합)  
> SSOT (레거시 감사): `KIT_ERP/BasisInformation/MasterInfoRecordRUD.cs` `m_FieldName[5]` (42필드)  
> 화면: `EquipmentInfo.aspx` / `EquipmentInfo.aspx.cs`  
> SmartManager: `equipment`  
> 내용키: `equipment_num` ← `EquipmentNum` (UNIQUE, 활성)  
> PK: `id` ← `EquipmentInfoIndex`

**관련:** [`step0-plan-D1-D4.md`](./step0-plan-D1-D4.md) · [`domain-event-projector-matrix.md`](./domain-event-projector-matrix.md) §2 · [`d4-work-center.md`](./d4-work-center.md) · [`공용코드 문서`](./공용코드-콤보박스-매핑-초안-code-group-field-binding.md) §4.6 · [시스템 컬럼 규칙](../../.cursor/rules/master-audit-fields.mdc) · TO-BE [`results/sample/basis-equipment-spec.md`](../../results/sample/basis-equipment-spec.md)

### v0.1 확정 요약

| 항목 | 레거시 (42필드) | SmartManager v0.1 |
|------|----------------|-------------------|
| UI 입력 | 20+ 필드·부속 그리드 | **Modal 6필드** + 작업샷·누계샷 **표시만** |
| 설비분류 | `EquipmentClassification` (자유텍스트·PUC 혼재) | **`equipment_category_id`** FK — `code_group` **`EQUIPMENT_CLASS`** |
| 작업장 | `WCName` 문자열 | **`work_center_id`** FK — **NULL 허용** (`WorkCenterSelect`) |
| 샷 | `DesignShot`·`FirstShot`·`WorkShot`·`TotalShot` | **`design_shot`·`initial_shot`·`work_shot`·`accumulated_shot`** |
| 전력·시간당비용 | `ElectricCapacity`·`UnitTimeUseCost` | **Drop** (작업장에서 이관 검토했으나 1차 **미포함**) |
| 현상태 `EquipmentState` | 사용/중지 | **Drop** — `recording_state`만 |
| 등록 부수효과 | 없음 | **없음** (domain-event §2) |
| 이력 `REI_MT` | — | Phase2 `equipment_history` |

---

## 1. 분류 범례

| 분류 | 의미 |
|------|------|
| **Keep (UI)** | Modal·API 사용자 입력 |
| **표시만** | DB 유지, Modal 읽기 전용 — 생산 연동으로 갱신 |
| **시스템(자동)** | PK·감사·소프트삭제·샷 누계 연산 |
| **Drop** | SmartManager 미구현·미이전 |
| **Phase2** | `equipment_history`, `work_shot` 생산실적 연동 |

---

## 2. SmartManager 스키마 — `equipment`

### 2.1 Modal 입력 6필드

| # | UI 라벨 | React field | DB 컬럼 | API | 필수 | 비고 |
|---|---------|-------------|---------|-----|------|------|
| 1 | 설비번호 | equipmentNum | equipment_num | `equipmentNum` | Y | UK(활성) · **수정 불가** |
| 2 | 설비명 | equipmentName | equipment_name | `equipmentName` | Y | |
| 3 | 설비분류 | equipmentCategoryId | equipment_category_id | `equipmentCategoryId` | Y | FK → `public_code.id` |
| 4 | 작업장 | workCenterId | work_center_id | `workCenterId` | N | FK → `work_center.id`, **null 허용** |
| 5 | 설계샷 | designShot | design_shot | `designShot` | Y | 수명 한계, ≥ 0 |
| 6 | 초기샷 | initialShot | initial_shot | `initialShot` | Y | 기본 **0** |

### 2.2 표시만 (Modal·그리드)

| UI 라벨 | DB 컬럼 | v0.1 | 비고 |
|---------|---------|------|------|
| 작업샷 | work_shot | 등록 시 **0**, 생산 TX 후 증가 (Phase2) | Modal 읽기 전용 |
| 누계샷 | accumulated_shot | **`initial_shot + work_shot`** 서버 연산 | Modal·그리드 표시 |
| 교체 필요 | (계산) | `design_shot > 0` ∧ `accumulated_shot >= design_shot` | 응답 `replacementDue` |

```
accumulated_shot = initial_shot + work_shot
```

- `initial_shot` 수정 시 `accumulated_shot` **재계산**
- `design_shot = 0` → 교체 판정 비활성

### 2.3 설비분류 콤보 (`code_group` 조회 · `public_code` PK 저장)

- React: `<CodeSelect codeGroup="EQUIPMENT_CLASS" excludeReserved valueField="id" />`
- API: `GET /api/code-groups/EQUIPMENT_CLASS/options` → `[{ id, value, label }]`
- `code_group`: `large_code=0720`, `exclude_codes`: `07200010`, `07200020` (공구·치구 예약)
- **저장:** `equipment_category_id` ← option `id` ([domain-event §10](./domain-event-projector-matrix.md))

> `?usageType=EQUIPMENT` API는 **미채택**. sample `equipmentClassificationId` 명칭은 **`equipmentCategoryId`** 로 Step 0 통일 (의미 동일).

### 2.4 작업장 콤보

- React: `<WorkCenterSelect />` — `GET /api/v1/basis/work-centers/search`
- 저장: `work_center_id` ← `work_center.id`
- 미선택 → `workCenterId` **null**

### 2.5 시스템 필드

| 컬럼 | 분류 |
|------|------|
| id, recording_state, created_*, updated_* | 시스템(자동) — [공통 규칙](../../.cursor/rules/master-audit-fields.mdc) |

---

## 3. 이벤트 · Projector

| 이벤트 | v0.1 Projector | 비고 |
|--------|----------------|------|
| `EquipmentRegistered` | **— (부수효과 없음)** | |
| `EquipmentUpdated` | — | `initial_shot` 변경 시 `accumulated_shot` 재계산은 **서비스 레이어** |
| `EquipmentDeleted` | — | 생산·정비 참조 검사 Phase2 |

---

## 4. API · 화면 (v0.1)

Base: `/api/v1/basis/equipment`

| Method | Endpoint | 설명 |
|--------|----------|------|
| GET | `/?q=` | 목록 — 설비번호·설비명 검색 |
| GET | `/{id}` | 상세 |
| POST | `/` | 등록 (6필드) |
| PUT | `/{id}` | 수정 (설비번호 읽기 전용) |
| DELETE | `/{id}` | 소프트 삭제 |

**POST / PUT body**

```json
{
  "equipmentNum": "EQ-001",
  "equipmentName": "1500톤 사출기",
  "equipmentCategoryId": 42,
  "workCenterId": 3,
  "designShot": 500000,
  "initialShot": 12000
}
```

**Response (요약)**

```json
{
  "id": 1,
  "equipmentNum": "EQ-001",
  "equipmentName": "1500톤 사출기",
  "equipmentCategoryId": 42,
  "equipmentCategoryName": "사출기",
  "categoryCode": "07200030",
  "workCenterId": 3,
  "wcName": "사출라인",
  "designShot": 500000,
  "initialShot": 12000,
  "workShot": 0,
  "accumulatedShot": 12000,
  "replacementDue": false
}
```

**검증**

- `equipment_num` UK (활성)
- `equipment_category_id` — `EQUIPMENT_CLASS` 활성 `public_code`, `exclude_codes` 제외
- `work_center_id` — 있으면 활성 `work_center`
- `design_shot`, `initial_shot`, `work_shot` ≥ 0 (정수)
- 등록: `work_shot = 0`, `accumulated_shot = initial_shot`

### 4.1 UI (`/basis/equipment`)

- 패턴: **검색 + 그리드 + Modal** (거래처·작업장과 동일)
- 그리드: 설비번호, 설비명, 설비분류명, 작업장, 설계샷, 누계샷, 작업샷, **교체 필요** 배지
- **하단 부가 그리드 없음**

---

## 5. 레거시 42필드 대조표

| # | 레거시 컬럼 | v0.1 | 비고 |
|---|------------|------|------|
| 1 | EquipmentNum | **Keep (UI)** | 내용키 |
| 2 | EquipmentName | **Keep (UI)** | |
| 3 | EquipmentClassification | **Keep (UI)** | → `equipment_category_id` FK |
| 4 | Standard | **Drop** | |
| 5 | Capacity | **Drop** | |
| 6 | ElectricCapacity | **Drop** | 작업장 Drop 항목 — 1차 설비에도 미포함 |
| 7 | UnitTimeUseCost | **Drop** | 동일 |
| 8 | Unit | **Drop** | `UNIT_GENERAL` Phase2 검토 |
| 9 | WCName | **Keep (UI)** | → `work_center_id` FK (nullable) |
| 10 | InjectionStaffNum | **Drop** | |
| 11 | Location | **Drop** | `EQUIPMENT_LOCATION` Phase2 |
| 12 | OccupancyArea | **Drop** | |
| 13 | BuyingDate | **Drop** | |
| 14 | BuyingCost | **Drop** | |
| 15 | InstrumentNum | **Drop** | |
| 16 | EquipmentState | **Drop** | `recording_state` |
| 17 | CheckDate | **Drop** | |
| 18 | ValidPeriod | **Drop** | |
| 19 | CheckAgency | **Drop** | |
| 20 | ReadyTime | **Drop** | |
| 21 | Cavity | **Drop** | |
| 22 | ValidityYear | **Drop** | |
| 23 | DesignShot | **Keep (UI)** | `design_shot` |
| 24 | FirstShot | **Keep (UI)** | `initial_shot` |
| 25 | TotalShot | 표시만 | → `accumulated_shot` 이관·연산 방식 변경 |
| 26 | WorkShot | 표시만 | `work_shot` |
| 27~34 | ManagePeriod/Date 1~4 | **Drop** | |
| 35~41 | RecodingState, 감사 | 시스템(자동) | |
| 42 | EquipmentInfoIndex | `id` PK | |

---

## 6. `results/` 문서와의 관계

| 문서 | 채택 |
|------|------|
| `sample/basis-equipment-spec.md` v1.2 | **SSOT** — 8핵심·샷 연산·교체 알림·작업장 NULL (**콤보 API만 `code_group`로 대체**) |
| `d4-work-center.md` | 작업장 FK·`WorkCenterSelect` 패턴 |
| `sample/basis-information-api-spec.md` §5.6 | endpoint 명명 참고 |

**Step 0 vs sample 차이**

| 항목 | sample | Step 0 v0.1 |
|------|--------|-------------|
| 설비분류 API 필드명 | `equipmentClassificationId` | **`equipmentCategoryId`** (DB `equipment_category_id`와 정합) |
| 설비분류 콤보 API | `?usageType=EQUIPMENT` | **`/api/code-groups/EQUIPMENT_CLASS/options`** |
| 설비분류 저장 | `equipment_category_id` FK | **동일** |

---

## 7. Cut-over · Phase2

| 항목 | 처리 |
|------|------|
| `EI_MT` | `equipment` + FK |
| `EquipmentClassification` | PUC `0720` 조인 → `equipment_category_id` |
| `WCName` | `work_center_id` (없으면 NULL) |
| `FirstShot` / `TotalShot` / `WorkShot` | `initial_shot` / `accumulated_shot` / `work_shot` — Cut-over 시 `accumulated = initial + work` 재계산 |
| Drop 컬럼 | 미이전 |
| Phase2 | `REI_MT` → `equipment_history`, `work_shot` 생산실적, `location`·`unit` |

---

## 8. 체크리스트

- [x] v0.1 6필드 Modal + 샷 표시·교체 판정 확정
- [x] **`equipment_category_id` FK** · `code_group` **`EQUIPMENT_CLASS`**
- [x] `work_center_id` FK nullable
- [x] domain-event 부수효과 없음 정합
- [x] Step 1 Flyway + equipment CRUD
- [ ] `PUC_MT` export → `EQUIPMENT_CLASS` `large_code` 확정

---

*v0.1: 2026-07-02 · TO-BE: `results/sample/basis-equipment-spec` · 레거시: `MasterInfoRecordRUD.cs` m_FieldName[5]*