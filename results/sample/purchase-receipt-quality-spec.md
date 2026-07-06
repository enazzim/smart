# 구매입고·품질검사 연동 설계서 (TX1-R)

> **문서 버전:** 1.1  
> **작성일:** 2026-07-05  
> **상태:** TO-BE 확정 — **구현 대기** (Wave 순서대로 진행)  
> **대상 스택:** Spring Boot 3.5 + MariaDB + React (Vite)  
> **레거시:** `BuyingDelivery.aspx`, `BD_HT`, `BO_HT`, `B_HT`, `SH_HT`, `QI_HT`  
> **관련 문서:**  
> - [업무 흐름 TO-BE](./business-workflow-revision.md)  
> - [재고·원장 설계서](./inventory-ledger-spec.md)  
> - [품목 기준정보](./basis-item-spec.md) §4.1 `check_distinction`  
> - [MRP·작업계획 구현 계획](./mrp-work-plan-implementation-plan.md)  
> - [거래처·원장](./basis-company-spec.md) §5.2

---

## 1. 문서 목적

구매발주(`purchase_order`) 이후 **구매입고 E2E**와 **품질검사 연동**을 정의한다.

**확정 원칙 (TO-BE · [business-workflow-revision.md](./business-workflow-revision.md) §2.1):**

| # | 원칙 |
|---|------|
| 0 | **1액션** — 무검사 입고: **[입고 등록] 1번 = 창고·원장 반영**. 별도 「전기(POST)」 UI **없음** |
| 1 | **납품/입고 UI 분리 없음** — 메뉴 **「구매입고」** 1개 |
| 2 | 대상: **입고 미완료** 발주 라인 (`order_qty − received_qty > 0`) |
| 3 | **무검사** (`NONE`) → 입고 등록과 **동시에** RAW/SALES 반영 |
| 4 | **검사품** (`INSPECTION`) → 입고 등록 + QI `PENDING` → **검사완료** 시 합격분만 창고 (유일한 2단계) |
| 5 | `quality_inspection` — **검사품만** INSERT (무검사 QI 자동 기록 **안 함**) |
| 6 | 구매·발주 **확정(CONFIRMED)** UI·가드 **미사용** |
| 7 | 회계월·마감: `FiscalCalendarService` + `MonthClosingService.assertTransactionOpen()` |

**현재 구현 상태:** `purchase_order` ✅ / `purchase_receipt`·품질검사·재고 수량 반영 ❌

---

## 2. 업무 흐름

### 2.1 TO-BE (구매)

```text
구매발주
  → [구매입고] 미입고 라인 · 수량 입력 · [입고 등록] 1액션
       ├─ purchase_receipt INSERT
       ├─ NONE     → 즉시 창고·purchase_history·미지급
       └─ INSPECTION → QI PENDING (창고 보류)
  → [품질검사] PENDING 만 · [검사완료] → 합격분 창고
```

### 2.2 레거시 원장 대응

| TO-BE 원장 | 신규 테이블 | 레거시 | 비고 |
|------------|-------------|--------|------|
| 구매발주원장 | `purchase_order` (+ line) | `BO_HT` | ✅ 구현됨 |
| 구매납품원장 | `purchase_receipt` (+ line) | `BD_HT` | 입고 **등록** 건 |
| 입고원장 (창고) | `stock_movement` | `SH_HT`, `BOS_HT` | **실창고** 반영 시점 |
| 매입원장 (통계) | `purchase_history` | `B_HT` | 창고 반영 시 |
| 품질검사원장 | `quality_inspection` | `QI_HT` | **검사품만** |
| 거래처 미지급 | `partner_ledger_monthly` | `BSI_MT` | 창고 반영 시 |

> 화면 명칭 **「구매입고」** 는 업무 통칭이다. DB·원장상으로는 **등록(`purchase_receipt`)** 과 **창고 반영(`stock_movement`)** 이 검사품에서 시점이 갈린다.

### 2.3 발주 「완료」 정의

| 용어 | 의미 | 구현 |
|------|------|------|
| 발주 **입고 완료** | 라인별 `received_qty >= order_qty` | 구매입고 화면 목록에서 **제외** |
| 발주 **확정** (`CONFIRMED`) | 헤더 status | UI 미사용 — **입고 가드에 사용하지 않음** (DRAFT 발주도 입고 허용, 레거시 동일) |
| 발주 **취소** (`CANCELLED`) | — | 입고 **불가** |

발주 라인 잔량:

```text
remain_qty = order_qty − received_qty
received_qty = Σ(창고 반영된 입고 수량)
waiting_qty  = Σ(receipt_qty where inspection PENDING)  -- 선택: UI 표시용
```

---

## 3. 검사구분 분기 (`item.check_distinction`)

| 값 | QI 원장 | 창고·매입·미지급 | 품질 화면 |
|----|---------|------------------|-----------|
| `NONE` | **없음** | **입고 등록 즉시** | — |
| `INSPECTION` | `PENDING` → `COMPLETED` | **검사완료 시** (합격 수량) | 대기·완료 |

### 3.1 무검사품 (NONE) — 단일 트랜잭션

```text
1. purchase_receipt (+ line) INSERT
2. stock_movement IN → resolveLocation (원자재→RAW, 상품→SALES)
3. inventory_balance / monthly 갱신
4. purchase_history INSERT
5. partner_ledger_monthly.purchase_amount += amount
6. purchase_order_line.received_qty += receipt_qty
7. PurchaseReceiptStockAppliedEvent (내부)
```

### 3.2 검사품 (INSPECTION) — 업무상 2단계

**1단계: [입고 등록]** (창고 없음)

```text
1. purchase_receipt (+ line) INSERT
2. quality_inspection INSERT — request_qty, status=PENDING
3. purchase_order_line.waiting_inspection_qty += receipt_qty
```

**2단계: [검사완료]** (`POST /quality/inspections/{id}/complete`)

```text
1. passed_qty + failed_qty = request_qty 검증
2. passed_qty > 0 → stock_movement IN, purchase_history, 미지급
3. purchase_order_line.received_qty += passed_qty, waiting 감소
4. quality_inspection.status = COMPLETED
```

**부적합 전량:** `passed_qty = 0` → 창고 미반영, 검사원장만 완료. 발주 잔량은 **입고 등록 수량 기준으로 이미 소진**할지, **대기 취소 후 잔량 복구**할지는 §8.2 정책 참고.

---

## 4. 창고 결정 (`resolveInventoryLocation`)

`basis-item-spec.md` §4.2·§5 기준:

| `property_classification` | location_code |
|---------------------------|---------------|
| `원자재` | `RAW` |
| `상품` | `SALES` |
| `제품` | `SALES` (구매입고는 일반적으로 원자재·상품; 제품 직구매 시 동일 규칙) |
| `공정품` | `WIP` (+ process 키 — 구매입고 MVP에서는 **제외** 또는 오류) |

MVP: `원자재`, `상품`만 허용. 그 외는 400 + 안내.

---

## 5. 데이터 모델 (Flyway 초안)

### 5.1 선행 INF (미구현 — TX1-R 선행)

```sql
-- V028__inventory_movement_monthly.sql (요약)
CREATE TABLE stock_movement (
  id, item_id, location_id, fiscal_year, fiscal_month,
  movement_type ENUM('IN','OUT','ADJUST'),
  qty, amount,
  reference_type VARCHAR(50),   -- 'PURCHASE_RECEIPT', 'QUALITY_INSPECTION', ...
  reference_id BIGINT,
  movement_date DATE,
  recording_state, audit columns
);

CREATE TABLE inventory_balance_monthly (
  id, inventory_balance_id, month_num,
  in_qty, in_amount, out_qty, out_amount, stock_qty,
  recording_state, ...
);

-- inventory_balance 에 stock_qty 컬럼 추가 여부:
--   A안) monthly 만 사용 (설계서 원안)
--   B안) balance.stock_qty + monthly 동기화 (조회 단순)
-- 구현 시 A안 우선, 화면 성능 이슈 시 B안 검토
```

### 5.2 구매입고

```sql
-- V029__purchase_receipt.sql
CREATE TABLE purchase_receipt (
  id BIGINT PK,
  receipt_no VARCHAR(30) NOT NULL,
  partner_id BIGINT NOT NULL FK → company,
  receipt_date DATE NOT NULL,
  purchase_order_id BIGINT NULL FK,      -- 헤더 연결 (선택)
  status ENUM('REGISTERED','PARTIALLY_POSTED','POSTED','CANCELLED') DEFAULT 'REGISTERED',
  recording_state TINYINT DEFAULT 1,
  audit columns,
  UK (receipt_no, recording_state)
);

CREATE TABLE purchase_receipt_line (
  id BIGINT PK,
  purchase_receipt_id BIGINT NOT NULL FK,
  line_no SMALLINT NOT NULL,
  purchase_order_line_id BIGINT NOT NULL FK,
  item_id BIGINT NOT NULL FK,
  receipt_qty DECIMAL(18,4) NOT NULL,
  posted_qty DECIMAL(18,4) NOT NULL DEFAULT 0,   -- 창고 반영 누적
  unit_price DECIMAL(18,2) NOT NULL,
  amount DECIMAL(18,2) NOT NULL,
  recording_state TINYINT DEFAULT 1,
  UK (purchase_receipt_id, line_no, recording_state)
);

-- purchase_order_line 확장
ALTER TABLE purchase_order_line
  ADD COLUMN received_qty DECIMAL(18,4) NOT NULL DEFAULT 0,
  ADD COLUMN waiting_inspection_qty DECIMAL(18,4) NOT NULL DEFAULT 0;
```

### 5.3 매입 통계 이력

```sql
-- V030__purchase_history.sql
CREATE TABLE purchase_history (
  id BIGINT PK,
  company_id BIGINT NOT NULL,
  item_id BIGINT NOT NULL,
  purchase_qty DECIMAL(18,4) NOT NULL,
  unit_price DECIMAL(18,2) NOT NULL,
  amount DECIMAL(18,2) NOT NULL,
  history_date DATE NOT NULL,
  source_type ENUM('PURCHASE_RECEIPT','QUALITY_INSPECTION') NOT NULL,
  source_id BIGINT NOT NULL,           -- receipt_line_id or inspection_id
  fiscal_year SMALLINT NOT NULL,
  fiscal_month TINYINT NOT NULL,
  recording_state TINYINT DEFAULT 1,
  audit columns
);
```

### 5.4 품질검사원장

```sql
-- V031__quality_inspection.sql
CREATE TABLE quality_inspection (
  id BIGINT PK,
  inspection_no VARCHAR(30) NULL,      -- 자동채번 (선택)
  source_type ENUM('PURCHASE','OUTSOURCE') NOT NULL,
  source_receipt_line_id BIGINT NOT NULL,  -- purchase_receipt_line.id (외주는 outsource_receipt_line)
  item_id BIGINT NOT NULL,
  company_id BIGINT NOT NULL,
  request_qty DECIMAL(18,4) NOT NULL,
  passed_qty DECIMAL(18,4) NOT NULL DEFAULT 0,
  failed_qty DECIMAL(18,4) NOT NULL DEFAULT 0,
  status ENUM('PENDING','COMPLETED','CANCELLED') NOT NULL,
  inspection_decision_code_id BIGINT NULL FK → public_code,  -- QC_INSPECTION_DECISION
  unsuitability_cause_code_id BIGINT NULL,
  unsuitability_status_code_id BIGINT NULL,
  completed_at DATETIME(3) NULL,
  recording_state TINYINT DEFAULT 1,
  audit columns,
  INDEX idx_qi_status (status, source_type, recording_state),
  INDEX idx_qi_receipt_line (source_receipt_line_id, recording_state)
);
```

---

## 6. API 설계

Base path: `/api/v1/purchase` (발주와 동일 prefix) · 품질은 `/api/v1/quality`

### 6.1 구매입고

| Method | Path | 설명 |
|--------|------|------|
| GET | `/purchase/receipt-candidates` | 미입고 발주 라인 목록 (§6.3) |
| GET | `/purchase/receipts` | 입고 이력 목록·필터 |
| GET | `/purchase/receipts/{id}` | 입고 상세 |
| POST | `/purchase/receipts` | 입고 등록 (§6.4) |
| POST | `/purchase/receipts/{id}/cancel` | 등록 취소 (PENDING 검사만, 마감 전) |

### 6.2 품질검사

| Method | Path | 설명 |
|--------|------|------|
| GET | `/quality/inspections` | `status=PENDING` 기본, `sourceType` 필터 |
| GET | `/quality/inspections/{id}` | 상세 |
| POST | `/quality/inspections/{id}/complete` | 검사 완료 → 창고 반영 (§6.5) |

### 6.3 `GET /purchase/receipt-candidates` 쿼리

| 파라미터 | 설명 |
|----------|------|
| `partnerName` | 거래처명 LIKE |
| `orderNo` | 발주번호 |
| `orderDateFrom`, `orderDateTo` | 발주일 |
| `itemNum`, `itemName` | 품목 |

**응답 행 (발주 라인 단위):**

```json
{
  "purchaseOrderId": 1,
  "purchaseOrderLineId": 10,
  "orderNo": "PO-20260705-001",
  "orderDate": "2026-07-05",
  "partnerId": 3,
  "partnerName": "aaa",
  "itemId": 5,
  "itemNum": "M-001",
  "itemName": "볼트",
  "checkDistinction": "INSPECTION",
  "orderQty": 100,
  "receivedQty": 40,
  "remainQty": 60,
  "waitingInspectionQty": 10,
  "unitPrice": 500,
  "requestedDeliveryDate": "2026-07-12"
}
```

필터: `status != CANCELLED` AND `remain_qty > 0`.

### 6.4 `POST /purchase/receipts` 요청

```json
{
  "receiptDate": "2026-07-05",
  "lines": [
    {
      "purchaseOrderLineId": 10,
      "receiptQty": 25
    }
  ]
}
```

**서버 검증:**

- `receiptQty > 0`
- `receiptQty <= remain_qty` (검사 대기 중 수량 정책에 따라 §8.2)
- `MonthClosingService.assertTransactionOpen(receiptDate)`
- 품목·거래처·발주 라인 정합성

**응답:** 생성된 `purchase_receipt` + 라인별 `qualityInspectionId`, `postedImmediately: true|false`

### 6.5 `POST /quality/inspections/{id}/complete` 요청

```json
{
  "passedQty": 23,
  "failedQty": 2,
  "inspectionDecisionCodeId": null,
  "unsuitabilityCauseCodeId": null,
  "unsuitabilityStatusCodeId": null,
  "completedDate": "2026-07-06"
}
```

검증: `passedQty + failedQty = request_qty`, `passedQty >= 0`, 마감 가드, PENDING 상태만.

---

## 7. 서비스·이벤트

### 7.1 패키지 배치

```text
application/purchase/
  PurchaseReceiptService
  PurchaseReceiptRepository
application/quality/
  QualityInspectionService
application/inventory/
  InventoryBalanceService      -- ensureBalance, recordMovement, updateMonthly
application/ledger/
  PartnerLedgerService         -- addPurchaseAmount
application/closing/
  MonthClosingService          -- 기존
```

### 7.2 이벤트

| 이벤트 | 발행 시점 | 리스너 |
|--------|-----------|--------|
| `PurchaseReceiptStockAppliedEvent` | 무검사 입고 / 검사완료 후 | 재고·원장 리스너 |
| `QualityInspectionCompletedEvent` | 검사완료 | 동일 (passed_qty > 0) |

**금지:** `basis`·`item` 패키지에서 창고·원장 직접 UPDATE.

### 7.3 트랜잭션 경계

- `POST /purchase/receipts` — **1 트랜잭션** (receipt + inspection + 무검사 시 재고·원장)
- `POST /quality/inspections/{id}/complete` — **1 트랜잭션**

---

## 8. 정책·엣지 케이스

### 8.1 입고 수량 vs 발주 잔량

- 기본: `receipt_qty <= order_qty - received_qty`
- **검사 대기 중**인 수량을 잔량에서 차감할지:
  - **권장(레거시 `InStoreWaitingQuantity` 유사):** 등록 시 `waiting_inspection_qty` 증가, `remain_qty` 계산에서 **대기 수량 포함 차감** → 이중 등록 방지
  - 검사 완료 시: `waiting` 감소, `received` 증가(합격분)

### 8.2 전량 부적합

- `passed_qty = 0`: 창고·매입원장 없음
- 발주 잔량: 등록 수량만큼 **소진된 것으로 간주** (재입고는 신규 receipt)

### 8.3 부분 합격

- `passed_qty < request_qty`: 합격분만 창고·원장, 불량분은 이력만

### 8.4 입고 등록 취소

- **가드:** 해당 입고일이 속한 회계월이 **미마감**이면 취소 허용 (`MonthClosingService.assertTransactionOpen`). `posted_qty`·검사완료 여부와 무관.
- **무검사(즉시반영):** 창고 `OUT` 역전기, `purchase_history` 소프트 비활성, 거래처 미지급 차감, 발주 `received_qty` 복구.
- **검사 대기(PENDING):** QI `CANCELLED`, `waiting_inspection_qty` 복구, 창고·원장 없음.
- **검사 완료(COMPLETED):** 합격분에 대해 창고·원장 역전기, `received_qty` 복구, QI `CANCELLED`.
- receipt 헤더·라인 `CANCELLED`, 영향 발주 헤더 status 재계산 (`IN_PROGRESS` / `RECEIVED` / `CONFIRMED`).

### 8.5 발주 헤더 입고 상태

| 조건 | `purchase_order.status` |
|------|-------------------------|
| 모든 라인 `received_qty >= order_qty` | `RECEIVED` (입고완료) |
| 일부 입고·검사대기 존재 | `IN_PROGRESS` (입고중) |
| 입고 실적 없음, 작성중 아님 | `CONFIRMED` 유지 |
| 입고 실적 없음, 작성중 | `DRAFT` |

### 8.6 Lot (후속)

- `docs/step0/d5-lot-traceability.md` — MVP에서는 `lot_no` nullable
- LOT Wave에서 receipt·inspection DTO 확장

---

## 9. 화면 설계 (프론트)

### 9.1 메뉴

| 변경 | 내용 |
|------|------|
| **유지** | 구매 > **구매입고** (`purchase-receipt`) |
| **제거** | 구매 > 납품 (`purchase-delivery`) — Placeholder 삭제, 단일 화면으로 통합 |
| **연계** | 품질 > **품질검사** (`quality-inspection`) — PENDING 목록 |

### 9.2 `PurchaseReceiptPage` (구매입고)

**상단:** 필터 (거래처, 발주번호, 발주일, 품목)

**본문 그리드:** `receipt-candidates` — 발주 라인 단위

| 컬럼 | 비고 |
|------|------|
| 발주번호, 발주일, 거래처 | |
| 품목번호, 품목명 | |
| 검사구분 | NONE / INSPECTION 뱃지 |
| 발주수량, 기입고, 검사대기, **잔량** | |
| **입고수량** | 체크 시 잔량 자동 세팅, 수동 수정 가능 |
| 단가, 금액 | 발주 단가 기준 자동 계산 |

**선택 UX:** 행 체크박스·헤더 전체선택. 체크 시 `입고수량 = 잔량`. **다거래처** 라인도 한 번에 등록 가능(서버가 거래처별 입고전표 분할).

**하단:** 입고일, [입고 등록] 버튼 — 체크된 행 일괄

**하단 탭:** 최근 입고 이력 (`GET /purchase/receipts`) — **취소** 버튼 (`POST /receipts/{id}/cancel`)

### 9.3 `QualityInspectionPage` (품질검사)

- 기본 필터: `status=PENDING`, `sourceType=PURCHASE`
- 컬럼: 입고일, 거래처, 품목, 의뢰수량, 발주번호
- 상세 Modal: 합격/불량 수량, 판정 콤보 (`QC_INSPECTION_DECISION` 등), [검사완료]

무검사품은 **품질검사 화면에 표시되지 않음** (QI 레코드 없음).

### 9.4 품질 통계 (불량률·검사현황)

- SoT: `quality_inspection` (**검사품만**)
- 무검사 입고 건수: `purchase_receipt` / `purchase_receipt_line` 조회
- 통합 리포트 필요 시 DB **VIEW** 로 UNION (구현 시)

---

## 10. 권한 (시드 초안)

| permission | 설명 |
|------------|------|
| `purchase:receipt:read` | 후보·이력 조회 |
| `purchase:receipt:write` | 입고 등록·취소 |
| `quality:inspection:read` | 검사 대기 조회 |
| `quality:inspection:complete` | 검사 완료 |

---

## 11. 구현 Wave (지시 시 순서)

| 순서 | Wave | 내용 | 산출물 |
|------|------|------|--------|
| 1 | **INF-1b** | `stock_movement`, `inventory_balance_monthly`, `InventoryBalanceService` | V028, 서비스·테스트 |
| 2 | **TX1-R1** | `purchase_receipt`, `purchase_history`, 발주 라인 `received_qty` | V029~V030, API |
| 3 | **TX1-R2** | `quality_inspection`, 구매입고 연동, NONE 분기 | V031, PurchaseReceiptService |
| 4 | **TX1-R3** | 품질검사 API·화면, INSPECTION 완료 → 창고 | QualityInspectionService |
| 5 | **TX1-R4** | `PurchaseReceiptPage`, `QualityInspectionPage`, 메뉴 정리 | 프론트 |
| 6 | **TX1-R5** | 통합 테스트, MRP `orderedQty`와 잔량 정합 | |

**외주(INF-4):** `outsource_receipt` + `quality_inspection.source_type=OUTSOURCE` — 동일 패턴 재사용.

---

## 12. 테스트 시나리오 (E2E)

1. 발주 100 → 무검사 입고 60 → RAW +60, `received=60`, QI **0건**
2. 잔량 40 → 검사품 입고 40 → QI `PENDING`, 재고 변화 없음
3. 검사완료 합격 38 → RAW +38, `received=98`
4. 잔량 2 → 무검사 입고 2 → `received=100`, 후보 제거
5. 마감 회계월 입고 → 오류
6. 입고 수량 > 잔량 → 400
7. 상품 무검사 입고 → **SALES** +수량

---

## 13. 문서·코드 동기화

구현 착수 전 본 문서 승인 후:

- [inventory-ledger-spec.md](./inventory-ledger-spec.md) §6 보강 (QI 동시 기록)
- [mrp-work-plan-implementation-plan.md](./mrp-work-plan-implementation-plan.md) 체크리스트 링크
- `menuConfig.tsx` — `purchase-delivery` 제거는 **TX1-R4**에서

---

## 14. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-07-05 | 초안 |
| 1.1 | 2026-07-05 | TO-BE 1액션, QI 검사품만, 확정/전기 UI 없음, SALES 상품 입고 |
