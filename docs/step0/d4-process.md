# D4 — 공정순서 (`ProcessSequenceInfo` → `PSI_MT`)

> Step 0 산출물 · **확정 v0.1** (7필드 Modal · plan SoT · `public_code` FK 정합)  
> SSOT (레거시 감사): `KIT_ERP/BasisInformation/MasterInfoRecordRUD.cs` `m_FieldName[4]` (17필드)  
> 화면: `ProcessSequenceInfo.aspx` / `ProcessSequenceInfo.aspx.cs`  
> SmartManager: `process_sequence` (plan) + `real_process_sequence` (actual)  
> 내용키(UK): `(item_id, public_code_id, process_sequence)` 활성 1건  
> PK: `id` ← `ProcessSequenceInfoIndex`

**관련:** [`step0-plan-D1-D4.md`](./step0-plan-D1-D4.md) · [`domain-event-projector-matrix.md`](./domain-event-projector-matrix.md) §2·§4·§5·§10 · [`d4-item.md`](./d4-item.md) · [`d4-work-center.md`](./d4-work-center.md) · [`공용코드 문서`](./공용코드-콤보박스-매핑-초안-code-group-field-binding.md) §4.5 · [시스템 컬럼 규칙](../../.cursor/rules/master-audit-fields.mdc) · TO-BE [`results/sample/basis-process-sequence-spec.md`](../../results/sample/basis-process-sequence-spec.md)

### v0.1 확정 요약

| 항목 | 레거시 | SmartManager v0.1 |
|------|--------|-------------------|
| 운영 SoT | `PSI_MT` (plan) | **`process_sequence`** variant=`plan` |
| 적용 품목 | 전 품목 입력 가능 | **`제품`·`공정품`만** (원자재·상품 거부) |
| UI 입력 | 9+ 필드·하단 그리드 | **Modal 7필드** (품목·순번·공정·작업구분·작업장·발주%·진척%) |
| 공정코드 | `ProcessCode` 8자리 PUC | **`public_code_id`** FK — `code_group` **`PROCESS_CODE`** 검증 |
| 작업장 | `WCName` 문자열 | **`work_center_id`** FK (`WorkCenterSelect`) |
| 작업구분 | `사내`/`외주`/`자가·외주` | ENUM **`INHOUSE` / `OUTSOURCE` / `SPLIT`** |
| 리드타임·기타 | 화면 입력 | **1차 비노출** (`lead_time`, `etc_text` NULL) |
| 등록 부수효과 | `PS_MT` 무조건 생성 | **`WipBalanceProjector.ensure`** — 완료 공정 WIP만 Lazy |
| 진공정 | `RPSI_MT` | `real_process_sequence` — compare·actual, **창고 이벤트 없음** |

---

## 1. 분류 범례

| 분류 | 의미 |
|------|------|
| **Keep (UI)** | Modal·API 사용자 입력 |
| **시스템(자동)** | PK·감사·소프트삭제 |
| **Phase2** | `lead_time`·`etc_text` Modal, PRD SPLIT 수량 분할 |
| **Drop (UI)** | 1차 화면·Request DTO 미포함 |

---

## 2. SmartManager 스키마 — `process_sequence` (plan)

### 2.1 UI 입력 7필드

| # | UI 라벨 | React field | DB 컬럼 | API (권장) | 필수 | 비고 |
|---|---------|-------------|---------|------------|------|------|
| 1 | 품목 | itemId | item_id | `itemNum` → resolve | Y | **제품·공정품**만 |
| 2 | 순서번호 | processSequenceNum | process_sequence | `processSequenceNum` | Y | **99 금지**, 권장 10 단위 |
| 3 | 공정 | processCodeId | public_code_id | `processCodeId` | Y | FK → `public_code.id` |
| 4 | 작업구분 | workDistinction | work_distinction | `workDistinction` | Y | ENUM (§2.3) |
| 5 | 작업장 | workCenterId | work_center_id | `workCenterId` | 조건부 | INHOUSE·SPLIT 시 **필수** |
| 6 | 발주비율 | outsideOrderRate | outside_order_rate | `outsideOrderRate` | 조건부 | SPLIT 시 **0~100** (0=전량 자가, 100=전량 외주) |
| 7 | 진척비율 | progressRate | progress_rate | `progressRate` | Y | 기본 **100** |

**공정 콤보 (`code_group` 조회 · `public_code` PK 저장):**

- React: `<CodeSelect codeGroup="PROCESS_CODE" excludeReserved valueField="id" />`
- 옵션 API: `GET /api/code-groups/PROCESS_CODE/options` → `[{ id, value, label }]`
  - `id` — **`public_code.id`** (저장·요청용)
  - `value` — 8자리 `small_code` (그리드·레거시 표시)
  - `label` — `small_name`
- `exclude_codes`: `14000000`, `14009999` ([공용코드 문서 §3·§4.5·§6·§8](./공용코드-콤보박스-매핑-초안-code-group-field-binding.md))
- **저장:** `public_code_id` ← option `id` ([domain-event §10.2·§10.4](./domain-event-projector-matrix.md))

> `?usageType=PROCESS` API는 **미채택**. 콤보는 `code_group`, 저장은 **`public_code.id` FK** (sample `basis-process-sequence-spec` §5.8과 동일).

**작업장 콤보:**

- React: `<WorkCenterSelect />` — `GET /api/v1/basis/work-centers/search`
- 저장: `work_center_id` ← `work_center.id`
- `OUTSOURCE` → `workCenterId` **생략 또는 null**

### 2.2 내용키 · UK

| 구분 | 레거시 | SmartManager v0.1 |
|------|--------|-------------------|
| 레거시 UK | `(ItemNum, ProcessSequenceNum)` | 순번만으로 유일 |
| TO-BE UK | — | **`(item_id, public_code_id, process_sequence)`** 활성(`recording_state=1`) 구간 유일 |

동일 품목에 **같은 순번·같은 공정** 조합은 1건만 허용 ([sample §3.2](../../results/sample/basis-process-sequence-spec.md)).

### 2.3 `work_distinction` (ENUM — PUC 아님)

| 코드 | 화면 라벨 | `work_center_id` | `outside_order_rate` |
|------|-----------|:----------------:|:--------------------:|
| `INHOUSE` | 자가 | **필수** | `0` (입력 비활성) |
| `OUTSOURCE` | 외주 | **null** | `0` (입력값 무시·저장 시 0) |
| `SPLIT` | 자가/외주 | **필수** | **0~100** (%) |

> 레거시 index 1·2·3 → 위 3코드로 통일. SPLIT 수량 분할 **실행**은 PRD-W (마스터는 비율만 저장).

### 2.4 1차 비노출 (DB 유지)

| DB 컬럼 | v0.1 | Phase2 |
|---------|------|--------|
| lead_time | NULL | OUTSOURCE·SPLIT 필수 검토 |
| etc_text | NULL | Modal 확장 |

### 2.5 시스템 필드

| 컬럼 | 분류 |
|------|------|
| id, recording_state, created_*, updated_* | 시스템(자동) — [공통 규칙](../../.cursor/rules/master-audit-fields.mdc) |

### 2.6 품목 자산분류 제약 (d4-item 4종)

| 역할 | 허용 | 비고 |
|------|------|------|
| 라우팅 대상 | `제품`, `공정품` | POST/PUT 시 `item_id` 검증 |
| 적용 제외 | `원자재`, `상품` | 화면·API **400 거부** |

---

## 3. 이벤트 · Projector

| 이벤트 | Projector | Read Model | 레거시 |
|--------|-----------|------------|--------|
| `ProcessRegistered` | `WipBalanceProjector.ensure` | `inventory_balance` location=`WIP` OUTPUT | WorkDistinction 무관 `PS_MT` 항상 |
| `ProcessUpdated` | `WipBalanceProjector.reconcile` | WIP 행 갱신·비활성 | — |
| `ProcessDeleted` | `WipBalanceProjector` + `OutsourceInputBalanceProjector` + cascade | WIP·OUTSOURCE 비활성 + `work_standard` | `WSI_MT` 연쇄 |

**WorkDistinction별 SmartManager (개선)**

| 구분 | 완료 잔고 (`WIP` OUTPUT) | 투입 잔고 (`OUTSOURCE` INPUT) |
|------|--------------------------|-------------------------------|
| INHOUSE | `ProcessRegistered` 시 ensure | — |
| OUTSOURCE | 외주 **완료 공정** WIP ensure | `OutsourceUnitPriceRegistered` 시 ensure |
| SPLIT | 완료 공정 WIP ensure | 외주분 투입은 단가 등록 시 |

상세: [domain-event §4·§4.5](./domain-event-projector-matrix.md)

### 3.1 Real variant (`RPSI_MT`)

| variant | 테이블 | 용도 |
|---------|--------|------|
| `plan` | `process_sequence` | **운영 SoT** — MRP·생산·Capa 참조 |
| `actual` | `real_process_sequence` | 현장 확인·**compare** 전용 |

| 이벤트 | Projector |
|--------|-----------|
| `RealProcessRegistered` / `Updated` / `Deleted` | **창고 부수효과 없음** (domain-event §2) |

> actual CRUD·compare는 Step 1 이후 (P2). v0.1은 plan 우선.

---

## 4. API · 화면 (v0.1)

Base: `/api/v1/basis/processes/plan`

| Method | Endpoint | 설명 |
|--------|----------|------|
| GET | `/plan?itemNum=` | 목록 (`itemNum` 생략 시 제품·공정품 전체) |
| GET | `/plan/{itemNum}` | 품목별 라우팅 |
| POST | `/plan` | 등록 (7필드) |
| PUT | `/plan/{id}` | 수정 (품목·순번·공정 읽기 전용 권장) |
| DELETE | `/plan/{id}` | 소프트 삭제 |
| POST | `/plan/copy` | 공정순서 복사 (품목→품목) |
| GET | `/compare?itemNum=` | plan vs actual (Phase2) |

**POST 예시**

```json
{
  "itemNum": "P-1000",
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

**검증**

- `item_id` → 활성 품목, `property_classification` ∈ {`제품`, `공정품`}
- `public_code_id` — `PROCESS_CODE` 그룹 활성 `public_code`, `exclude_codes`·`system_reserved` 제외
- `process_sequence` ≠ 99
- 활성 UK `(item_id, public_code_id, process_sequence)` 중복 불가
- `INHOUSE`·`SPLIT` → `work_center_id` 필수
- `OUTSOURCE` → `work_center_id` null
- `SPLIT` → `outside_order_rate` 0~100
- `OUTSOURCE`/`INHOUSE` → 발주비율 입력 무시 후 0 저장
- 삭제: `work_standard`·생산 실적 참조 시 정책 검토

### 4.1 UI (`/basis/processes/plan`)

- 패턴: **품목 필터 + 그리드 + Modal** + 툴바 **[공정복사]**
- 품목 필터: `ItemSelect` — **제품·공정품**만, 비우면 전체 목록
- 그리드: **품목번호**, **품목명**, 순번, 공정코드, 공정명(`public_code` join), 작업구분, 작업장, 발주%, 진척%
- Modal: §2.1 7필드 — 작업구분에 따라 작업장·발주% 동적 활성/비활성
- **하단 부가 그리드 없음**

**공정복사 body**

```json
{
  "sourceItemNum": "P-1000",
  "targetItemNum": "P-2000"
}
```

대상도 제품·공정품만. 활성 UK 충돌 행은 스킵.

---

## 5. 레거시 17필드 대조표

| # | 레거시 컬럼 | v0.1 | 비고 |
|---|------------|------|------|
| 1 | ItemNum | **Keep (UI)** | → `item_id` |
| 2 | ProcessSequenceNum | **Keep (UI)** | UK 일부 · 99 금지 |
| 3 | ProcessCode | **Keep (UI)** | → `public_code_id` FK |
| 4 | WorkDistinction | **Keep (UI)** | ENUM 3코드 |
| 5 | WCName | **Keep (UI)** | → `work_center_id` FK |
| 6 | OutsideOrderRate | **Keep (UI)** | SPLIT만 |
| 7 | ProgressRate | **Keep (UI)** | 기본 100 |
| 8 | LeadTime | Phase2 | 1차 NULL |
| 9 | EtcText | Phase2 | 1차 NULL |
| 10~16 | RecodingState, 감사 | 시스템(자동) | |
| 17 | ProcessSequenceInfoIndex | `id` PK | |

---

## 6. `results/` 문서와의 관계

| 문서 | 채택 |
|------|------|
| `sample/basis-process-sequence-spec.md` v1.7 | 제품·공정품·UK·7필드·`SPLIT`·`processCodeId`·WIP ensure·공정복사 (**콤보 API만 `code_group`로 대체**) |
| `260624_기준정보_공정.md` (있을 경우) | 검증·Capa 연계 참고 |
| `sample/basis-information-api-spec.md` §5.5 | endpoint·variant 명명 참고 |

**Step 0 vs sample 차이**

| 항목 | sample | Step 0 v0.1 |
|------|--------|-------------|
| 공정 콤보 API | `?usageType=PROCESS` | **`/api/code-groups/PROCESS_CODE/options`** |
| 공정 저장 | `processCodeId` → `public_code_id` | **동일** |
| 품목 요청 | `itemId` | **`itemNum` → resolve** (BOM 패턴) |
| 복사 | `sourceItemId` / `targetItemId` | **`sourceItemNum` / `targetItemNum`** |

---

## 7. Cut-over · Phase2

| 항목 | 처리 |
|------|------|
| `PSI_MT` | `process_sequence` + `item_id`·`public_code_id`·`work_center_id` FK |
| `ProcessCode` | `PUC_MT` 조인 → `public_code_id` |
| `WorkDistinction` | 1·2·3 → `INHOUSE`·`OUTSOURCE`·`SPLIT` 매핑 |
| `RPSI_MT` | `real_process_sequence` — compare용 |
| 원자재·상품 `PSI` 행 | 마이그레이션 시 정리 또는 비활성 |
| Phase2 | `lead_time`·`etc_text`, actual/compare, PRD SPLIT 분할 |

---

## 8. 체크리스트

- [x] v0.1 7필드 Modal·plan SoT·제품·공정품 범위 확정
- [x] **`public_code_id` FK** · `processCodeId` API (domain-event §10 정합)
- [x] 콤보 `code_group` **`PROCESS_CODE`** (sample `usageType` 미채택)
- [x] domain-event `WipBalanceProjector`·WorkDistinction 정합
- [ ] Step 1 Flyway + plan CRUD + copy + WIP ensure E2E
- [ ] actual/compare (P2)
- [ ] `PUC_MT` export → `code_group` §3 `large_code` 확정

---

*v0.1: 2026-07-02 · FK 정합 · TO-BE: `results/sample/basis-process-sequence-spec` · 레거시: `MasterInfoRecordRUD.cs` m_FieldName[4]*