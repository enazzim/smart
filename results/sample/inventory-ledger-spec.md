# KIT_ERP 재고·원장·구매 설계서

> **문서 버전:** 1.2  
> **작성일:** 2026-06-22  
> **대상 스택:** Spring Boot 3.5 + MariaDB  
> **레거시:** `RMS_MT`, `BS_MT`, `DS_MT`, `PS_MT`, `OS_MT`, `BSI_MT`, `Register.cs` (BuyingDelivery)  
> **관련 문서:**  
> - [기준정보 구현 설계서](./basis-information-implementation-spec.md)  
> - [시스템정보 설계서](./system-information-spec.md)  
> - [업무 흐름 TO-BE](./business-workflow-revision.md)

---

## 1. 문서 목적

레거시 5창고 테이블·`BSI_MT`·연말 SP 패턴을 대체하는 **재고·거래처 원장·구매입고** 모듈을 정의한다.

**UX:** [업무 흐름 TO-BE](./business-workflow-revision.md) §2.1 — **등록 1번 = 재고·원장 반영**. 확정·전기(POST) UI **없음** (검사품 구매입고만 예외 2단계).

---

## 2. 레거시 문제와 신규 방향

| 레거시 | 문제 | 신규 |
|--------|------|------|
| 5창고 + `[Year]` 행 선생성 | 연말 SP, placeholder 폭발 | `inventory_balance` Lazy ensure |
| 월별 가로 컬럼 48+ | 스키마 비대 | `inventory_balance_monthly` (월=행) |
| `BSI_MT` 72컬럼 | 유지보수 어려움 | `partner_ledger_monthly` |
| 외주단가 → `OS_MT` | 도메인 역전 | 외주 **발주/입고** 시 |
| 작년 데이터 DELETE | 감사 불가 | 아카이브, 삭제 금지 |
| 더미 공정 14009999/14000000 | 의미 혼란 | `inventory_location` 코드 |

---

## 3. 재고 모델 (하이브리드)

| 계층 | 테이블 | 역할 |
|------|--------|------|
| 이력 | `stock_movement` | 건별 입출고 (SH_HT, BOS_HT 대체) |
| 현재고 | `inventory_balance` | 오늘 잔고 |
| 월별 | `inventory_balance_monthly` | 입고/출고/재고 (화면용) |

### 3.1 inventory_location (시드)

| code | 명칭 | 사내 보유 재고 | 비고 |
|------|------|----------------|------|
| RAW | 원자재창고 | 원자재 | 구매입고 |
| **SALES** | **영업창고** | **제품·상품** | 생산 최종·상품 구매입고 |
| **DELIVERY** | **납품창고** | **아님** — 출하 후·매출 전 | 제품출고 IN → 매출 OUT |
| WIP | 생산창고 | 공정 중 | 공정별 슬롯 |
| OUTSOURCE | 외주창고 | 외주 | partner_id |

> 레거시 `BS_MT` 1~3 → **`SALES` 1개** ([basis-item-spec.md](./basis-item-spec.md) §5.1).

### 3.1a 재고 이동 (TO-BE)

```text
구매입고(무검사)      → RAW 또는 SALES
작업일보 등록(최종)   → WIP → SALES
제품출고             → SALES OUT, DELIVERY IN
매출등록             → DELIVERY OUT
```

### 3.2 inventory_balance UK

```text
(item_id, location_id, fiscal_year,
 process_sequence NULLABLE,
 process_code NULLABLE,
 partner_id NULLABLE)
```

**WIP:** `item_id` + `process_sequence` + `process_code` (마스터 UK와 별도, 재고 식별용 3요소)

### 3.3 inventory_balance_monthly

```text
(inventory_balance_id, month_num) → in_qty, in_amount, out_qty, out_amount, stock_qty
```

조회 시 1~12월 배열 또는 SQL 피벗. **가로 48컬럼 테이블 사용 안 함.**

---

## 4. 거래처 원장 (BSI_MT 대체)

### 4.1 partner_ledger_account

```text
(partner_id, fiscal_year, ledger_type)  UK
ledger_type: SALES | PURCHASE
```

- 거래처 등록 시 account만 생성 (금액 0)
- 비용거래처만이면 account 생성 안 함 (레거시 동일)

### 4.2 partner_ledger_monthly

```text
(account_id, month_num) → amount_total, amount_collected, amount_outstanding
                        (매입: amount_purchased, amount_paid, amount_payable)
```

---

## 5. 연도 운영 (SP 대체)

| 항목 | 신규 |
|------|------|
| 연말 전량 INSERT | **없음** |
| 첫 트랜잭션 | `ensureBalance()` / `ensureAccount()` |
| 연도 이월 | `FiscalYearRolloverJob` → `fiscal_year_opening` |
| 작년 DELETE | **금지** → archive |
| 회계월 25일 규칙 | `FiscalCalendarService` |

---

## 6. 구매입고 파일럿 (TX1)

레거시: `BuyingDelivery.aspx` → `Register.cs` case `BuyingDelivery`

### 6.1 레거시 처리 (무검사 원자재)

1. `BD_HT` INSERT  
2. `RMS_MT` 월수량 증가  
3. `SH_HT` / `BOS_HT` 이력  
4. `BSI_MT` 미지급 증가 (`SaleTable`)  
5. `B_HT` 매입원장  
6. `BO_HT` 발주잔량 감소  

### 6.2 신규 흐름

> **상세 설계:** [purchase-receipt-quality-spec.md](./purchase-receipt-quality-spec.md) (TX1-R, 2026-07-05)

```text
[구매입고] POST /purchase/receipts  — 입고 등록 1액션
  → PurchaseReceiptService
       ├─ purchase_receipt INSERT (BD_HT)
       ├─ NONE: 즉시 창고·purchase_history·미지급 (동일 TX)
       └─ INSPECTION: quality_inspection PENDING → 검사완료 시 창고

[품질검사] POST /quality/inspections/{id}/complete  — 검사품만

PurchaseReceiptStockAppliedEvent (내부; 사용자 「전기」 없음)
  → InventoryOnPurchaseReceiptListener
  → LedgerOnPurchaseReceiptListener
```

**UI:** 「구매입고」 1화면. `quality_inspection` — **검사품만** INSERT ([purchase-receipt-quality-spec.md](./purchase-receipt-quality-spec.md)).

### 6.3 테이블

| 신규 | 레거시 |
|------|--------|
| `purchase_order` | BO_HT |
| `purchase_receipt` | BD_HT |
| `purchase_history` | B_HT |
| `quality_inspection` | QI_HT |
| `stock_movement` | SH_HT, BOS_HT |

### 6.4 API (요약)

- `GET /api/v1/purchase/receipt-candidates` — 미입고 발주 라인
- `POST /api/v1/purchase/receipts` — **입고 등록 = 무검사 시 즉시 창고 반영**
- `POST /api/v1/quality/inspections/{id}/complete` — 검사품 창고 반영
- 구매·발주 **확정(CONFIRMED) UI 없음** — DRAFT 발주도 입고 허용

### 6.5 생산·영업 (PRD-W1 / 후속 TX)

| 행위 | 재고 |
|------|------|
| 작업일보 **등록** | 즉시 WIP 이동; 최종 → **SALES** IN |
| 제품출고 | SALES OUT, DELIVERY IN |
| 매출등록 | DELIVERY OUT |

상세: [production-work-report-mapping.md](./production-work-report-mapping.md)

---

## 7. 이벤트·서비스 패키지

```text
com.kit.erp.inventory   ? InventoryBalanceService
com.kit.erp.ledger      ? PartnerLedgerService
com.kit.erp.purchasing  ? PurchaseReceiptService
```

**금지:** `basis` 패키지에서 창고·BSI 직접 INSERT

---

## 8. Flyway 순서 (INF)

1. `inventory_location` + 시드  
2. `inventory_balance`, `inventory_balance_monthly`, `stock_movement`  
3. `partner_ledger_account`, `partner_ledger_monthly`  
4. `fiscal_year_opening`  
5. `purchase_order`, `purchase_receipt`, `purchase_history`  

---

## 9. ETL (레거시 → 신규)

| 레거시 | 신규 |
|--------|------|
| RMS_MT | location=RAW |
| BS_MT | SALES (1창고 합산) |
| DS_MT | DELIVERY |
| PS_MT | WIP + process_sequence/code |
| OS_MT | OUTSOURCE + partner_id |
| BSI_MT 월컬럼 | partner_ledger_monthly unpivot |

더미 공정코드는 ETL 시 `location`만 채우고 `process_code` null 가능.

---

## 10. 구현 Wave

| Wave | 내용 |
|------|------|
| **INF-1** | inventory_location, inventory_balance (슬롯) | ✅ |
| **INF-1b** | stock_movement, inventory_balance_monthly, 수량 서비스 | ✅ [TX1-R](./purchase-receipt-quality-spec.md) |
| **INF-2** | FiscalCalendar, MonthClosing 연동 | ✅ |
| **TX1** | 구매발주 (`purchase_order`) | ✅ |
| **TX1-R** | 구매입고·품질검사 E2E | ✅ Wave 1~4 |
| **INF-3** | FiscalYearRolloverJob |
| **INF-4** | 외주 발주/입고 시 OUTSOURCE 잔고 |

---

## 11. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-06-22 | 초안 — 통합재고, 원장, 구매입고, 연도운영 |
| 1.1 | 2026-07-05 | §6 구매입고·QI — [purchase-receipt-quality-spec.md](./purchase-receipt-quality-spec.md) |
| 1.2 | 2026-07-05 | §2.1 1액션, §3.1 SALES/DELIVERY 역할, 생산·영업 이동, QI 검사품만 |
