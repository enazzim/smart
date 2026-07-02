# D4 — 작업표준 (`WorkStandardInfo` → `WSI_MT`)

> Step 0 산출물 · **확정 v0.1** (9필드 Modal · plan SoT · `process_sequence` FK)  
> SSOT (레거시 감사): `KIT_ERP/BasisInformation/MasterInfoRecordRUD.cs` `m_FieldName[6]` (28필드)  
> 화면: `WorkStandardInfo.aspx` / `WorkStandardInfo.aspx.cs`  
> SmartManager: `work_standard` (plan) + `real_work_standard` (actual)  
> 내용키(UK): `(item_id, process_sequence_id, priority_order)` 활성 1건  
> PK: `id` ← `WorkStandardInfoIndex`

**관련:** [`step0-plan-D1-D4.md`](./step0-plan-D1-D4.md) · [`domain-event-projector-matrix.md`](./domain-event-projector-matrix.md) §2·§5 · [`d4-item.md`](./d4-item.md) · [`d4-process.md`](./d4-process.md) · [`d4-work-center.md`](./d4-work-center.md) · [`d4-equipment.md`](./d4-equipment.md) · [`공용코드 문서`](./공용코드-콤보박스-매핑-초안-code-group-field-binding.md) §4.7 · [시스템 컬럼 규칙](../../.cursor/rules/master-audit-fields.mdc) · TO-BE [`results/sample/basis-work-standard-spec.md`](../../results/sample/basis-work-standard-spec.md)

### v0.1 확정 요약

| 항목 | 레거시 (28필드) | SmartManager v0.1 |
|------|----------------|-------------------|
| 운영 SoT | `WSI_MT` (plan) | **`work_standard`** variant=`plan` |
| 적용 품목 | 전 품목 | **`제품`·`공정품`만** |
| 적용 공정 | 작업구분 무관 | plan `process_sequence` 중 **`INHOUSE`·`SPLIT`만** |
| UI 입력 | 15+ 필드·2분할 | **Modal 9필드** (슬림 10 − 시스템 `id`) |
| 공정 참조 | `ProcessSequenceNum` + `ProcessCode` | **`process_sequence_id`** FK → `process_sequence.id` |
| 작업장·설비·작업자 | 문자열·혼재 | **`work_center_id`·`equipment_id`·`main_worker_user_id`** FK |
| 공구·치구 | `ToolName1~3` + `JigName1~3` | **`tool_name` 1개** |
| 시간 | DECIMAL 혼재 | **`setup_time` 분(INT)** · **`standard_time` 초(INT)** |
| 우선순위 | `PriorityOrder` | **`priority_order`** — **1 = 최우선**, 동일 공정 복수 표준 허용 |
| 등록 부수효과 | 없음 | **없음** (domain-event §2) |
| 작업일보 대입 | 없음 | Phase2 PRD-W (`work_standard_id` 자동 선택) |
| 진작업표준 | `RWSI_MT` | `real_work_standard` — compare·actual, **창고 이벤트 없음** |

---

## 1. 분류 범례

| 분류 | 의미 |
|------|------|
| **Keep (UI)** | Modal·API 사용자 입력 |
| **시스템(자동)** | PK·감사·소프트삭제 |
| **Drop** | SmartManager 미구현·미이전 |
| **Phase2** | 작업일보 자동 대입·Capa 리드타임·`actual` compare |

---

## 2. SmartManager 스키마 — `work_standard` (plan)

### 2.1 Modal 입력 9필드

| # | UI 라벨 | React field | DB 컬럼 | API (권장) | 필수 | 비고 |
|---|---------|-------------|---------|------------|------|------|
| 1 | 품목 | itemId | item_id | `itemNum` → resolve | Y | **제품·공정품**만 |
| 2 | 공정 | processSequenceId | process_sequence_id | `processSequenceId` | Y | FK → `process_sequence.id` |
| 3 | 작업장 | workCenterId | work_center_id | `workCenterId` | Y | FK → `work_center.id` |
| 4 | 사용설비 | equipmentId | equipment_id | `equipmentId` | N | FK → `equipment.id`, null 허용 |
| 5 | 우선순위 | priorityOrder | priority_order | `priorityOrder` | Y | ≥ 1, 기본 **1** |
| 6 | 주작업자 | mainWorkerId | main_worker_user_id | `mainWorkerId` | N | FK → `user.id`, null 허용 |
| 7 | 사용공구 | toolName | tool_name | `toolName` | N | 자유 텍스트 |
| 8 | 셋업시간 | setupTime | setup_time | `setupTime` | Y | **분**, INT ≥ 0, 기본 0 |
| 9 | 표준시간 | standardTime | standard_time | `standardTime` | Y | **초**, INT ≥ 0, 기본 0 |

### 2.2 공정 선택 (`ProcessSelect` — PUC 콤보 아님)

공정은 **`process_sequence` 행 FK**로 연결. `PROCESS_CODE` `CodeSelect` **사용 안 함**.

- React: `<ProcessSelect itemId={...} workDistinctions={['INHOUSE','SPLIT']} />`
- API: `GET /api/v1/basis/processes/plan/{itemNum}` 또는 `?itemNum=` — 응답에서 **INHOUSE·SPLIT**만 필터
- 표시: `순번 · 공정명(public_code join) · 작업구분`
- 저장: `process_sequence_id` ← `process_sequence.id`
- 검증: `process_sequence.item_id` = 요청 `item_id`, `work_distinction` ∈ {`INHOUSE`, `SPLIT`}

> 공정명·코드는 `process_sequence` → `public_code` JOIN으로 **응답·그리드**에만 노출 ([공용코드 §4.7](./공용코드-콤보박스-매핑-초안-code-group-field-binding.md)).

### 2.3 마스터 FK 콤보

| 필드 | React | API | 저장 |
|------|-------|-----|------|
| 작업장 | `WorkCenterSelect` | `GET /api/v1/basis/work-centers/search` | `work_center_id` |
| 설비 | `EquipmentSelect` | `GET /api/v1/basis/equipment?q=` | `equipment_id` (nullable) |
| 주작업자 | `UserSelect` | `GET /api/v1/basis/users/search` | `main_worker_user_id` (nullable) |

### 2.4 내용키 · UK · 우선순위

| 구분 | 레거시 | SmartManager v0.1 |
|------|--------|-------------------|
| 레거시 UK | `(ItemNum, ProcessSequenceNum, PriorityOrder)` | 순번+우선순위 |
| TO-BE UK | — | **`(item_id, process_sequence_id, priority_order)`** 활성 구간 유일 |

- **`priority_order = 1`** 이 최우선(1순위)
- 동일 품목·동일 공정(`process_sequence_id`)에 **우선순위만 다른** 표준 **복수 등록** 허용
- 작업일보·스케줄 기본 채택: **priority 최소 1건** (Phase2 §5.5)

### 2.5 소요 시간 연산 (읽기 전용 규칙)

수량 \(Q\)에 대한 총 소요시간(초):

\[
T = (\text{setup\_time} \times 60) + (\text{standard\_time} \times Q)
\]

예: 셋업 10분, 표준 10초, 수량 400 → \(600 + 4000 = 4600\)초.

### 2.6 시스템 필드

| 컬럼 | 분류 |
|------|------|
| id, recording_state, created_*, updated_* | 시스템(자동) — [공통 규칙](../../.cursor/rules/master-audit-fields.mdc) |

---

## 3. 이벤트 · Projector

| 이벤트 | v0.1 Projector | 비고 |
|--------|----------------|------|
| `WorkStandardRegistered` | **— (부수효과 없음)** | |
| `WorkStandardUpdated` | — | |
| `WorkStandardDeleted` | — | 작업지시·작업일보 참조 검사 Phase2 |

**연쇄 (상위 삭제)**

| 상위 이벤트 | 영향 |
|------------|------|
| `ProcessDeleted` | 해당 `process_sequence_id` 작업표준 cascade ([domain-event §5](./domain-event-projector-matrix.md)) |

### 3.1 Real variant (`RWSI_MT`)

| variant | 테이블 | 용도 |
|---------|--------|------|
| `plan` | `work_standard` | **운영 SoT** |
| `actual` | `real_work_standard` | compare·확인용 |

| 이벤트 | Projector |
|--------|-----------|
| `RealWorkStandardRegistered` / `Updated` / `Deleted` | **창고 부수효과 없음** |

---

## 4. API · 화면 (v0.1)

Base: `/api/v1/basis/work-standards/plan`

| Method | Endpoint | 설명 |
|--------|----------|------|
| GET | `/plan?itemNum=` | 목록 (`itemNum` 생략 시 전체) |
| GET | `/plan/{itemNum}` | 품목별 |
| POST | `/plan` | 등록 (9필드) |
| PUT | `/plan/{id}` | 수정 (품목·공정 읽기 전용) |
| DELETE | `/plan/{id}` | 소프트 삭제 |
| POST | `/plan/copy` | 표준복사 |
| GET | `/compare?itemNum=` | plan vs actual (Phase2) |

**POST 예시**

```json
{
  "itemNum": "P-1000",
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

**Response (요약)**

```json
{
  "id": 1,
  "itemId": 50,
  "itemNum": "P-1000",
  "itemName": "완제품A",
  "processSequenceId": 101,
  "processSequenceNum": 10,
  "processCodeId": 201,
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

**검증**

- `item_id` → 활성 품목, `property_classification` ∈ {`제품`, `공정품`}
- `process_sequence_id` → 활성 plan 행, `item_id` 일치, `work_distinction` ∈ {`INHOUSE`, `SPLIT`}
- `OUTSOURCE` 공정 → **400 거부**
- 활성 UK `(item_id, process_sequence_id, priority_order)` 중복 불가
- `work_center_id` 필수·활성
- `equipment_id`·`main_worker_user_id` — 있으면 활성 FK
- `setup_time`·`standard_time` ≥ 0

### 4.1 UI (`/basis/work-standards/plan`)

- 패턴: **품목 필터 + 그리드 + Modal** + **[표준복사]** (공정순서형 평면 목록)
- 품목 필터: **제품·공정품**만
- 그리드: 품목번호, 품목명, 순번, 공정명, 작업장, 설비, 우선순위, 셋업(분), 표준(초)
- Modal: §2.1 — 품목 변경 시 `ProcessSelect` 재로드
- **하단 부가 그리드 없음**

**표준복사**

```json
{
  "sourceItemNum": "P-1000",
  "targetItemNum": "P-2000"
}
```

대상 품목의 **동일 UK 공정순서**로 `process_sequence_id` 재매핑. UK 충돌 행 스킵.

---

## 5. 레거시 28필드 대조표

| # | 레거시 컬럼 | v0.1 | 비고 |
|---|------------|------|------|
| 1 | ItemNum | **Keep (UI)** | → `item_id` |
| 2 | ProcessSequenceNum | **Keep (UI)** | → `process_sequence_id` FK (순번은 join) |
| 3 | ProcessCode | Drop (UI) | `process_sequence` → `public_code` join |
| 4 | WCName | **Keep (UI)** | → `work_center_id` FK |
| 5 | PriorityOrder | **Keep (UI)** | UK 일부 |
| 6 | MainWorkerID | **Keep (UI)** | → `main_worker_user_id` FK |
| 7 | MainWorker | Drop | join 표시 |
| 8 | ToolName1 | **Keep (UI)** | → `tool_name` (단일) |
| 9~13 | JigName1~3, ToolName2~3 | **Drop** | |
| 14 | SetupTime | **Keep (UI)** | 분 INT |
| 15 | RealProcessingTime | **Drop** | |
| 16 | SpaceTime | **Drop** | |
| 17 | StandardTime | **Keep (UI)** | 초 INT |
| 18 | WaitTime | **Drop** | |
| 19 | LotSize | **Drop** | |
| 20 | Cavity | **Drop** | |
| 21~27 | RecodingState, 감사 | 시스템(자동) | |
| 28 | WorkStandardInfoIndex | `id` PK | |

---

## 6. `results/` 문서와의 관계

| 문서 | 채택 |
|------|------|
| `sample/basis-work-standard-spec.md` v1.4 | **SSOT** — UK·9필드·시간 단위·INHOUSE/SPLIT·작업일보 연계 정의 |
| `sample/basis-process-sequence-spec.md` | `ProcessSelect` 데이터 소스 |
| `sample/production-work-report-mapping.md` | Phase2 `work_standard_id` 대입 |

**Step 0 vs sample 차이**

| 항목 | sample | Step 0 v0.1 |
|------|--------|-------------|
| 품목 요청 | `itemId` | **`itemNum` → resolve** (BOM·공정 패턴) |
| 복사 | `sourceItemId` / `targetItemId` | **`sourceItemNum` / `targetItemNum`** |
| FK·UK·시간·공정 선택 | 동일 | **동일** |

---

## 7. Cut-over · Phase2

| 항목 | 처리 |
|------|------|
| `WSI_MT` | `work_standard` + FK |
| `ProcessSequenceNum`+`ProcessCode` | `process_sequence_id` (품목·순번·공정코드로 매핑) |
| `WCName` | `work_center_id` |
| `MainWorkerID` | `main_worker_user_id` |
| Tool/Jig 다중 | `tool_name` 1개 또는 첫 번째만 이관 |
| 시간 | `setup_time` 분, `standard_time` 초로 환산 규칙 정의 |
| `RWSI_MT` | `real_work_standard` |
| Phase2 | 작업일보 자동 대입, actual/compare, 삭제 참조 검사 |

---

## 8. 체크리스트

- [x] v0.1 9필드 Modal·plan SoT·UK 확정
- [x] `process_sequence_id` FK · INHOUSE/SPLIT만
- [x] 작업장·설비·작업자 PK FK 정합
- [x] domain-event 부수효과 없음·ProcessDeleted cascade 정합
- [ ] Step 1 Flyway + plan CRUD + copy
- [ ] PRD-W 작업일보 `work_standard_id` 대입
- [ ] actual/compare (P2)

---

*v0.1: 2026-07-02 · TO-BE: `results/sample/basis-work-standard-spec` · 레거시: `MasterInfoRecordRUD.cs` m_FieldName[6]*