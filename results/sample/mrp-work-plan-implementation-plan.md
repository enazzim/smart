# 자재소요(MRP)·작업계획 구현 계획

> **문서 버전:** 1.1  
> **작성일:** 2026-07-05  
> **상태:** 구현 계획 — **MRP 취소 3단계·정책 A 코드 반영** (MRP-R1a~c)  
> **관련 문서:**  
> - [업무 흐름 TO-BE](./business-workflow-revision.md)  
> - [생산실적→작업일보 매핑](./production-work-report-mapping.md)  
> - [품목구성(BOM) 스펙](./basis-item-composition-spec.md)  
> - [작업표준 스펙](./basis-work-standard-spec.md)  
> - [생산달력·Capa 스펙](./basis-production-calendar-spec.md)  
> - [시스템정보 스펙](./system-information-spec.md)

---

## 1. 문서 목적

생산계획 이후 **자재소요(MRP)** · **작업계획(work_plan)** · **작업장 Capa(2차 뷰)** 의 설계 원칙, 시스템 설정, API·화면 구현 순서를 정리한다.

**현재 코드 반영 상태 (2026-07-05):**

| 기능 | 상태 |
|------|------|
| 생산계획 수립/취소 | ✅ |
| MRP 산출 (`calculate`) | ✅ MVP |
| MRP 산출 취소 | ✅ run / plan / line 3단계 + **정책 A** (§3) |
| MRP 자재별 집계 모드 | ✅ `mrp.grouping_mode` + grouped API/UI (§4) |
| 시스템 설정 UI | ✅ `system_settings` + 시스템정보 > 시스템 설정 |
| 작업계획 원장 | ✅ PRD-W1a (`work_plan`, 수립 API·화면) |
| 작업장 부하·Capa 2차 뷰 | ❌ 미구현 (본 문서 §6) |

---

## 2. 공통 설계 원칙

### 2.1 1차 저장(SoT) vs 2차 집계(뷰)

| 레이어 | 역할 | DB |
|--------|------|-----|
| **1차** | 업무 추적·원장 | 상세 라인/행 **반드시 저장** |
| **2차** | 구매·일정·Capa 판단 | **별도 집계 테이블 없음** — 조회 API·화면에서 SUM/GROUP BY |

MRP와 작업계획 모두 이 원칙을 따른다. **집계 방식 변경 시 1차 데이터를 재생성하지 않는다.**

### 2.2 MRP vs 작업계획 — 집계 대상이 다름

| | **자재소요(MRP)** | **작업계획(work_plan)** |
|--|-------------------|-------------------------|
| **1차 단위** | 생산계획 × **자재( component )** | 생산계획 × **공정** |
| **질문** | 같은 자재를 몇 개 사야 하나? | 이 계획·공정을 언제·얼마나? |
| **집계(2차)** | **자재별** `SUM(gross_qty)` (구매·발주) | **작업장×일자** 부하(분) (Capa) |
| **계획 간 merge** | 설정으로 **가능** (`mrp.grouping_mode`) | **불가** — UK `(production_plan_id, process_sequence_id)` |

작업계획에 MRP와 동일한 `BY_PLAN / BY_COMPONENT` 설정은 **두지 않는다.**

---

## 3. MRP — 산출 취소

> **확정 (2026-07-05):** 취소 **3단계**(run / plan / line) 모두 지원.  
> `production_plan.mrp_status`는 **정책 A** (잔여 line 있으면 `CALCULATED` 유지).

### 3.1 현행

- `POST /api/v1/production/mrp/calculate` 만 존재
- 산출 후 `production_plan.mrp_status = CALCULATED` → **생산계획 취소도 차단**

### 3.2 취소 API (3단계 — 확정)

| 단위 | API | 범위 |
|------|-----|------|
| **Run** | `POST /api/v1/production/mrp/runs/{runId}/cancel` | run 전체 line + run 삭제, 포함 **모든** 생산계획 `NOT_CALCULATED` |
| **Plan** | `POST /api/v1/production/mrp/plans/{productionPlanId}/cancel` | 해당 계획의 line **전부** 삭제, 계획 `NOT_CALCULATED`, run 잔여 line 없으면 run 삭제 |
| **Line** | `POST /api/v1/production/mrp/lines/{lineId}/cancel` | 소요 line **1행** 삭제, [정책 A](#33-mrp_status-정책-a--확정) 로 plan 상태 갱신 |

권한: `production:mrp:write` (공통)

### 3.3 `mrp_status` 정책 A — 확정

| 상황 | `production_plan.mrp_status` |
|------|------------------------------|
| 해당 계획에 **활성** `material_requirement_line` **1건 이상** | `CALCULATED` 유지 (**부분 산출** 허용) |
| 해당 계획에 **활성 line 0건** | `NOT_CALCULATED` 복원 → 재산출·생산계획 취소 가능 |

- **`PARTIAL` enum 추가 없음** — UI는 「산출완료」 + 잔여 line 건수/목록으로 부분 상태 표현
- 생산계획 **취소** 가드: line이 1건이라도 남으면 `CALCULATED` → **생산계획 취소 불가** (현행과 동일, 의도적)
- line 단위 취소 후 **재산출**: `NOT_CALCULATED`가 된 계획만 `calculate` 대상

**정책 A를 선택한 이유**

- DB·enum 변경 최소 (`NOT_CALCULATED` / `CALCULATED` 유지)
- 자재 1행만 잘못 산출된 경우 **전체 run 되돌리기 없이** 수정 가능
- TX1 발주는 **line FK** 단위로 가드 가능

### 3.4 취소 가능 조건 (공통 + 단위별)

**대상 생산계획** (run/plan/line 모두 해당 plan 검증):

| 조건 | 이유 |
|------|------|
| `work_plan_status == NOT_PLANNED` | 작업계획 수립 후 소요 되돌리기 불가 |
| `produced_qty == 0` | 생산 실적 있으면 불가 |
| `status` ∈ {`PLANNED`, `IN_PROGRESS`} | 완료·취소 제외 |

**Line 단위 추가**

| 조건 | 이유 |
|------|------|
| 해당 line에 구매발주 FK 없음 | `purchase_order_line.requirement_line_id` |

**Run / Plan 단위**

| 조건 | 이유 |
|------|------|
| 취소 대상 scope 내 **모든** plan이 위 조건 충족 | 하나라도 실패 → 400 |
| scope 내 **어떤 line도** 발주 FK 없음 | run/plan 일괄 취소 시 (`MrpService` + `PurchaseOrderRepository`) |

### 3.5 처리 흐름

#### Run

```text
1. mrp_run 조회
2. run 소속 line → production_plan_id 수집
3. 각 plan 취소 조건 검증
4. scope 내 발주 FK 가드 (하나라도 참조 시 400)
5. line 전부 DELETE → run DELETE
6. 각 plan.mrp_status → NOT_CALCULATED
```

#### Plan

```text
1. plan_id 로 line 목록 조회
2. plan 취소 조건 검증
3. plan scope 발주 FK 가드
4. 해당 plan line 전부 DELETE
5. plan.mrp_status → NOT_CALCULATED
6. run_id 별 잔여 line 0건이면 run DELETE
```

#### Line

```text
1. line 조회 → production_plan_id
2. plan 취소 조건 + line FK 발주 가드
3. line 1건 DELETE
4. plan 잔여 active line count:
     > 0 → mrp_status CALCULATED 유지
     = 0 → mrp_status NOT_CALCULATED
5. run 잔여 line 0건이면 run DELETE
```

### 3.6 UI (3단계)

| 화면 | 동작 |
|------|------|
| **산출 이력** | run 행 **「취소」** + `cancellable` |
| **소요 자재 목록** (또는 계획별 그룹) | line 행 **「취소」** + plan 단위 **「이 계획 소요 전체 취소」** |
| **생산계획 목록** | (선택) MRP 산출완료 plan **「소요 취소」** → plan API |

부분 산출 시: 계획 행에 `mrpStatusLabel` 「산출완료」 + tooltip/보조문구 「잔여 N건」.

### 3.7 구현 Wave

| 순서 | 내용 |
|------|------|
| MRP-R1a | Run 취소 |
| MRP-R1b | Plan 취소 + 빈 run 정리 |
| MRP-R1c | Line 취소 + 정책 A (`syncMrpStatusAfterLineChange`) |

헬퍼 예: `MrpRepository.countActiveLinesByPlanId(planId)`, `deleteLinesByPlanId`, `deleteLineById`, `deleteRunIfEmpty(runId)`.

---

## 4. MRP — 자재별 집계 (시스템 설정)

### 4.1 정책

- **저장:** 항상 `material_requirement_line` — **생산계획 × 자재** 상세 유지
- **집계:** `system_settings` 에 따라 **조회·구매발주(TX1)** 단계에서 적용

### 4.2 설정 키

| 키 | 값 | 의미 |
|----|-----|------|
| `mrp.grouping_mode` | `BY_PLAN` | 생산계획별 표시 (기본·현재 MVP) |
| | `BY_COMPONENT` | 동일 `component_item_id` 합산 표시·발주 |

네임스페이스: [system-information-spec.md](./system-information-spec.md) §3.5 `mrp.*` (`include_on_hand`, `include_safety_stock` 등과 동일).

### 4.3 집계 시점

| 방식 | 권장 |
|------|------|
| 산출 시 집계 저장 | **비권장** — 설정 변경 시 재산출 필요, 상세 이력 상실 |
| 상세 저장 + 조회/발주 시 `GROUP BY` | **권장** |

```sql
-- BY_COMPONENT 조회 예
SELECT component_item_id, SUM(gross_qty)
FROM material_requirement_line
WHERE mrp_run_id = ? AND recording_state = 1
GROUP BY component_item_id;
```

### 4.4 UI

- `BY_PLAN`: 계획번호 | 모품목 | 자재 | 총소요량
- `BY_COMPONENT`: 자재 | 합산 총소요량 | (펼치기) 계획별 내역

---

## 5. 작업계획 — 1차 원장 (Phase 1)

### 5.1 데이터 모델

[production-work-report-mapping.md](./production-work-report-mapping.md) §4.1:

```sql
-- 요약
work_plan (
  production_plan_id,
  process_sequence_id,
  planned_qty,
  plan_start_date, plan_end_date,
  status,
  -- UK: (production_plan_id, process_sequence_id)
)
```

- `production_plan.work_plan_status`: `NOT_PLANNED` → `PLANNED`

### 5.2 수립 API

```http
POST /api/v1/production/work-plans
Body: { "productionPlanIds": [1, 2] }
```

**`WorkPlanService.createPlans()` 흐름:**

```text
1. 생산계획 (권장: mrp_status=CALCULATED, work_plan_status=NOT_PLANNED)
2. 품목 process_sequence (INHOUSE/SPLIT만)
3. work_standard → work_center_id, setup_time, standard_time
4. SPLIT 공정 → 발주비율 기준 자가/외주 2행 ([basis-process-sequence-spec](./basis-process-sequence-spec.md))
5. work_plan INSERT + work_plan_status = PLANNED
```

**공정 소요시간** ([basis-work-standard-spec.md](./basis-work-standard-spec.md) §5.4):

\[
T_{\text{sec}} = (\text{setupTime} \times 60) + (\text{standardTime} \times Q)
\]

### 5.3 화면 — 생산 > 작업계획 (1차)

MRP `MrpPage` 패턴:

- 수립 대상: MRP 산출완료 + 작업계획 미수립
- 목록: **계획×공정** 상세 (합산 없음)
- **취소**: line `POST .../work-plans/{id}/cancel`, plan `POST .../work-plans/production-plans/{id}/cancel` → `work_plan_status` 복원
- **목록 검색**: 품목번호·품목명·공정명·작업장·시작일(범위)

### 5.4 시스템 설정 (MRP와 별도)

| 키 | 용도 |
|----|------|
| `production.work_plan.creation_mode` | `MANUAL` / `AUTO_ON_MRP` |
| `production.work_plan.split_outsource` | 자가/외주 2행 분할 (단가 발주비율 연동) |

---

## 6. 작업장 Capa — 2차 뷰 (Phase 2)

### 6.1 원칙

- **새 집계 테이블 없음**
- `work_plan` + `work_standard` + 기존 Capa 서비스로 **조회 전용 API**

### 6.2 재사용 코드 (현행)

| 컴포넌트 | 역할 |
|----------|------|
| `WorkCenterCapaService.resolveCapa()` | 일자별 **공급 Capa(분)** |
| `WorkCenterCalendarService.resolveEffectiveMinutes()` | 작업장 Override + 기본달력 |
| `EffectiveMinutesResolver` | 유효 가동분 |
| `WorkCenterCalendarPage` | 작업장별 달력 UI (부하 오버레이 후보) |

### 6.3 부하(Load) 계산

```text
for each work_plan in date range:
  ws = work_standard(item, process_sequence)
  demandMinutes = f(setup_time, standard_time, planned_qty)
  aggregate Map<(workCenterId, date), sumMinutes>

for each (wc, date):
  supply = WorkCenterCapaService.resolveCapa(wc, date).capaMinutes()
  loadRate = demandMinutes / supply
```

### 6.4 API (2차 전용)

```http
GET /api/v1/production/scheduling/work-center-load
  ?workCenterId=&from=&to=
```

응답 필드 예: `demandMinutes`, `capaMinutes`, `loadRate`, `workPlanCount`, `details[]`(plan×공정 상세).

### 6.5 UI

- **옵션 A:** 생산 메뉴 `prod-schedule` — 「작업장 부하」
- **옵션 B:** 기준정보 **WC달력**에 부하 % 오вер레이

일자 클릭 Modal → 해당일 `work_plan` 상세 목록 (1차 원장).

경고선 예: `production.schedule.warn_load_threshold` = 0.8 (80%).

---

## 7. Phase 3 — (후속) 자동 일정 배치

- `WorkPlanScheduler`: Capa 여유 (work_center, date)에 `plan_start_date` / `plan_end_date` 배치
- **MVP:** 시작일 수동·납기 역산, Capa는 **경고만**

---

## 8. 구현 Wave·순서

> **운영 우선순위(2026-07-06 확정):** [implementation-roadmap-and-exceptions.md](./implementation-roadmap-and-exceptions.md) §2 — **E2E 검증 → PRD-W2 → 본 절 로드맵**  
> **예외 업무(선입고·선출고·수주 없는 생산 등):** 동 문서 §3

| 순서 | Wave | 작업 | 산출물 |
|------|------|------|--------|
| 1 | MRP-R1a | Run 산출 취소 | `POST .../mrp/runs/{id}/cancel` |
| 2 | MRP-R1b | Plan 산출 취소 + 빈 run 정리 | `POST .../mrp/plans/{id}/cancel` |
| 3 | MRP-R1c | Line 산출 취소 + **정책 A** | `POST .../mrp/lines/{id}/cancel` |
| 4 | MRP-R2 | `mrp.grouping_mode` + 목록 집계 API/UI | ✅ `system_settings`, grouped API, SystemSettingsPage |
| 5 | PRD-W1a | `work_plan` 테이블·수립 API·화면 | ✅ `V034__work_plan.sql`, `WorkPlanPage` |
| 6 | PRD-W1b | `WorkCenterLoadQueryService` + Capa API | `SchedulingController`, 부하 화면 |
| 7 | PRD-W1c | `work_order` / `work_report` (등록=재고, SALES) | ✅ [production-work-report-mapping](./production-work-report-mapping.md) v1.1 |
| 7b | **PRD-W2** | **자재투입 TX** + 작업일보 BOM 투입현황 | [work-report-material-issue-spec](./work-report-material-issue-spec.md) |
| 8 | TX1 | 구매발주 MRP | ✅ 발주 CRUD + MRP 후보 |
| 9 | **INF-1b** | 재고 수량 인프라 | `stock_movement`, `inventory_balance_monthly` |
| 10 | **TX1-R** | 구매입고·품질검사 | [purchase-receipt-quality-spec](./purchase-receipt-quality-spec.md) Wave 1~6 |

---

## 9. 코드 배치 (패턴)

```text
domain/          ProductionPlanMrpStatus, WorkPlanStatus
application/     MrpService, WorkPlanService, WorkCenterLoadQueryService
infrastructure/  Jpa*Repository, *ApplicationService (@Transactional)
api/             MrpController, WorkPlanController, SchedulingController
frontend/        MrpPage, WorkPlanPage, workCenterLoad API·화면
```

생산계획·MRP MVP와 동일한 **Application Service + Flyway + permission 시드** 패턴.

---

## 10. 체크리스트 (구현 시)

### MRP

- [x] run / plan / line **3단계** 산출 취소 API·UI
- [x] **정책 A**: plan 잔여 line 0건일 때만 `NOT_CALCULATED`
- [x] `syncMrpStatusAfterLineChange`, 빈 run 정리
- [x] `mrp.grouping_mode` (`BY_PLAN` / `BY_COMPONENT`) + `GET /mrp/lines/grouped`
- [x] TX1 MVP: `purchase_order` CRUD + MRP `from-mrp` + 발주 FK 가드
- [x] TX1 MRP 발주 UI: 후보 행 펼치기 → 구매단가·거래처 하위 목록 + 거래처별 다건 발주
- [x] TX1 부분 발주: 발주비율 분할·잔여소요량·다중 거래처 동시 선택 + 납기요구일(품목 리드타임)
- [x] TX1 발주서 출력: `GET /orders/{id}/print` HTML + 발주 직후·목록 필터·재출력 UI (확정 버튼 UI 미노출)
- [ ] TX1 후속: tenant-profile → 발주서 발신 정보, `purchase:order:print` 권한(선택)
- [ ] **INF-1b** — `stock_movement`, `inventory_balance_monthly` ([inventory-ledger-spec.md](./inventory-ledger-spec.md))
- [ ] **TX1-R** — 구매입고·품질검사 ([purchase-receipt-quality-spec.md](./purchase-receipt-quality-spec.md)) — Wave 1→6 순서
- [ ] Phase2: `mrp.include_on_hand`, `include_safety_stock`, NET 소요

### 작업계획

- [x] `work_plan` UK `(production_plan_id, process_sequence_id, work_distinction)`
- [x] MRP 산출완료 후 수립 가드 (정책)
- [x] SPLIT 공정 자가/외주 2행
- [x] PRD-W1a: `work_plan` 수립 API·`WorkPlanPage` (1차)
- [x] PRD-W1a 보완: 작업계획 취소(line/plan) + 목록 검색
- [x] PRD-W1b: 작업장×일자 부하 API + Capa 비교 UI
- [x] PRD-W1c: `work_order`, `work_report` — 등록=재고, 최종→SALES
- [ ] **PRD-W2** — 자재투입 TX + 작업일보 BOM 투입현황 ([work-report-material-issue-spec.md](./work-report-material-issue-spec.md))
- [ ] (후속) `production_result` archive
