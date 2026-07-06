# SmartManager 구현 로드맵·예외 업무 정책

> **문서 버전:** 1.0  
> **작성일:** 2026-07-06  
> **상태:** **확정** — 이후 구현·리뷰 시 본 문서 우선  
> **관련 문서:**  
> - [business-workflow-revision.md](./business-workflow-revision.md) — TO-BE 메인 흐름  
> - [mrp-work-plan-implementation-plan.md](./mrp-work-plan-implementation-plan.md) — MRP·작업계획 Wave  
> - [work-report-material-issue-spec.md](./work-report-material-issue-spec.md) — PRD-W2  
> - [purchase-receipt-quality-spec.md](./purchase-receipt-quality-spec.md) — TX1-R  
> - [inventory-ledger-spec.md](./inventory-ledger-spec.md) — 재고·원장

---

## 1. 문서 목적

1. **구현 우선순위**를 팀·에이전트가 동일하게 따르도록 고정한다.  
2. 메인 TO-BE 흐름(§3 [business-workflow-revision.md](./business-workflow-revision.md)) **이외**에 현장에서 필수인 **예외·선행 업무**를 명시하고, 구현 시 반드시 허용해야 함을 기록한다.

> **원칙:** 정상 흐름만 막고 예외를 막지 않는다. 상위 문서(FK·가드)는 **nullable + `source_type`** 으로 예외를 수용한다.

---

## 2. 구현 우선순위 (확정, 2026-07-06)

| 단계 | 범위 | 내용 | 산출·완료 기준 |
|------|------|------|----------------|
| **1** | **통합 시나리오 검증** | E2E 1사이클 수동·자동 검증 | 아래 §2.1 체크리스트 통과 ✅ [결과](./e2e-step1-verification-results.md) |
| **2** | **PRD-W2 잔여** | [work-report-material-issue-spec.md](./work-report-material-issue-spec.md) §10~12 | 자재투입 TX 분리·일보 가드·WIP skip 정리 등 |
| **3** | **로드맵 순** | [mrp-work-plan-implementation-plan.md](./mrp-work-plan-implementation-plan.md) §8 | PRD-W1b → INF-1b → TX1-R ✅ [결과](./e2e-step3-verification-results.md) |

### 2.1 단계 1 — 통합 시나리오 검증 (즉시)

```text
구매발주 → 무검사 구매입고(RAW/SALES)
  → 작업지시 대상 작업일보 등록(양품·불량·투입요소 체크)
  → RAW 감소 · 최종공정 시 SALES 증가
  → 작업일보 취소 시 재고 역전기
```

| # | 검증 항목 |
|---|-----------|
| E1 | 무검사 입고 후 RAW `현재고수량` = 입고 누적 (슬롯: `RAW`, `output_process_id` NULL) |
| E2 | 일보 등록 시 체크한 투입요소만 RAW/WIP OUT |
| E3 | 작업지시 `reported_qty`·잔량 = 양품 누적 기준 |
| E4 | 최종 사내공정 일보 후 SALES IN |
| E5 | 일보 취소 시 투입·완성품 재고 원복 |

**현행 구현 메모 (2026-07-06):** 작업일보 등록 시 투입 차감을 **일보 TX에 통합**해 두었음. PRD-W2(단계 2)에서 **자재투입 TX 분리** 여부를 설계서와 재정렬한다.

### 2.2 단계 2 — PRD-W2 잔여 (다음)

[work-report-material-issue-spec.md](./work-report-material-issue-spec.md) 체크리스트 기준:

- [x] `material_issue` 테이블·API·화면 (자재투입 TX)
- [x] 작업일보 투입 충족 가드 (V2)
- [x] `WorkReportInventoryService` 첫 공정 skip 제거
- [ ] 작업일보 — 투입 충족 가드 (누적 투입 ≥ 누적 양품 소요)
- [ ] `WorkReportInventoryService` 첫 공정 WIP skip 제거 (투입 선행 전제)
- [ ] 일보 BOM 그리드 — 투입은 조회·현재고 표시, 편집은 자재투입 화면 (정책 확정 시)

### 2.3 단계 3 — 로드맵 순 (이후)

| 순서 | Wave | 참고 |
|------|------|------|
| 1 | PRD-W1b | 작업장 부하·Capa API·화면 | ✅ |
| 2 | INF-1b | `stock_movement`·월별 잔고 조회 UI | ✅ [e2e-step3](./e2e-step3-verification-results.md) |
| 3 | TX1-R 잔여 | 검사품 품질검사 2단계 | ✅ E2E |
| 4 | Phase 3+ | 자동 일정 배치, MRP NET 소요 등 |

---

## 3. 예외·선행 업무 (반드시 허용)

메인 흐름: `수주 → 생산계획 → …` / `MRP → 발주 → 입고` 이지만, 아래는 **정상 케이스**로 취급하며 API·FK·UI에서 **차단하지 않는다**.

### 3.1 수주 근거 없는 생산계획

| 항목 | 정책 |
|------|------|
| **업무** | 안전재고·수동 계획·긴급 생산 등 |
| **데이터** | `production_plan.sales_order_id` / `sales_order_line_id` **NULL 허용** |
| **출처** | `source_type = MANUAL` \| `STOCK_REPLENISH` \| `MRP` (수주 없이 run만 있는 경우) |
| **가드** | 수주 FK 없음을 이유로 저장·작업지시·일보 **거부 금지** |

### 3.2 수주·생산계획 근거 없는 자재소요 산출 / 구매발주

| 항목 | 정책 |
|------|------|
| **업무** | 원자재 단독 구매, 안전재고 발주, 비수주 MRP |
| **데이터** | `mrp_run` / `production_plan_id` **nullable**; 발주 `source_type = MANUAL` \| `MRP` |
| **가드** | [business-workflow-revision.md](./business-workflow-revision.md) §3.2 — **원자재 구매는 수주 없이 가능** (이미 확정) |
| **UI** | 발주·입고 화면에서 “근거 없음” 수동 등록 경로 유지 |

### 3.3 생산계획 없는 공정·작업계획

| 항목 | 정책 |
|------|------|
| **업무** | 공정 단위 단독 작업, 레거시 이관, 수리·재작업 |
| **데이터** | `work_plan.production_plan_id` **NULL 허용** (또는 별도 `work_plan_source_type`) |
| **연계** | 작업지시·작업일보는 `work_plan` 기준으로 동작; 생산계획 없어도 실적·재고 반영 |
| **가드** | `production_plan` 없음만으로 work_plan / work_order 생성 **거부 금지** |

### 3.4 구매품 선입고 (발주·소요 선행 입고)

| 항목 | 정책 |
|------|------|
| **업무** | 발주 잔량 초과 입고, 발주 전 입고(선입고), 긴급 입고 |
| **데이터** | 입고는 발주 라인 FK **권장**이나 선입고 시 `purchase_order_line_id` nullable 또는 **별도 입고 유형** `receipt_type = ADVANCE` |
| **재고** | 무검사 즉시 RAW/SALES 반영 (기존 TX1-R 동일) |
| **가드** | “발주 잔량 0”만으로 입고 **일괄 거부 금지** — 정책 플래그 또는 경고+확인 |
| **MRP** | 선입고 수량은 `include_on_hand`·NET 소요(후속)에서 차감 |

### 3.5 외주 출고 중 선출고

| 항목 | 정책 |
|------|------|
| **업무** | 외주 발주 잔량보다 먼저 OUTSOURCE 창고로 출고 |
| **데이터** | 외주출고 `outsourcing_shipment` — 발주 라인 대비 초과 허용 옵션 |
| **재고** | OUTSOURCE OUT (partner별 슬롯) |
| **가드** | 발주 잔량 초과 시 **경고**; 현장 정책에 따라 허용(`allow_advance_shipment`) |

### 3.6 수주 이전 선납품 (출고·납품)

| 항목 | 정책 |
|------|------|
| **업무** | 수주 등록 전 SALES→DELIVERY 이동, 견적·샘플 출하 |
| **데이터** | `sales_order_id` **NULL** 인 출고·납품 TX 허용; `delivery_type = PRE_ORDER` \| `SAMPLE` |
| **재고** | SALES OUT → DELIVERY IN ([inventory-ledger-spec.md](./inventory-ledger-spec.md) §3.1a) |
| **후속** | 수주 등록 후 **선납품과 수주 라인 매칭**(링크) 가능하면 제공 — 필수는 아님 |
| **가드** | 수주 없음을 이유로 출고 **거부 금지** (재고 부족 등 물리 가드만) |

---

## 4. 구현 시 공통 패턴

| 패턴 | 적용 |
|------|------|
| **`source_type` / `receipt_type` / `delivery_type`** | 정상·예외 출처 구분 (감사·리포트) |
| **Nullable FK** | 상위 문서 없이 하위 TX 생성 허용 |
| **Hard reject vs soft warn** | 재고 부족·마감월 = **reject**; 근거 FK 없음·잔량 초과 = **warn + 정책** |
| **시스템 설정** | `allow_advance_receipt`, `allow_orphan_production_plan` 등 tenant 단위 (후속) |
| **1액션=재고** | 예외 TX도 [business-workflow-revision.md](./business-workflow-revision.md) §2.1 유지 (검사품 입고만 2단계) |

---

## 5. 현행 구현 vs 갭 (스냅샷 2026-07-06)

| 예외 | 현행 | 갭 |
|------|------|-----|
| 수주 없는 생산계획 | 부분 지원 여부 코드 확인 필요 | FK nullable·UI 수동 생성 명시 |
| 수주 없는 발주 | `source_type`·MRP 수동 | 선입고 유형·초과 입고 |
| 생산계획 없는 작업계획 | 미확인 | `work_plan` orphan 허용 |
| 선입고 | 발주 라인 기준 입고만 | ADVANCE 입고·초과 |
| 외주 선출고 | INF-4 미완 | 모듈 전체 |
| 선납품 | 영업 출고 미완 | 수주 없는 출고 TX |

> 단계 1(E2E) 통과 후, 예외별로 **가드 audit**를 수행하고 위 갭을 Wave에 편성한다.

---

## 6. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-07-06 | 구현 우선순위(1→2→3) 확정; 예외 업무 6종 정책 기록 |
