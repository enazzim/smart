# KIT_ERP 재고·원장·구매 설계서

> **문서 버전:** 1.0  
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

| code | 명칭 | 비고 |
|------|------|------|
| RAW | 원자재창고 | 원자재 |
| SALES_1~3 | 영업창고 | 제품·상품 |
| DELIVERY | 납품창고 | |
| WIP | 생산창고 | 공정별 슬롯 |
| OUTSOURCE | 외주창고 | partner_id 필요 |

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

```text
PurchaseReceiptController
  → PurchaseReceiptService (receipt, history, 발주잔량)
  → PurchaseReceiptPostedEvent
  → InventoryOnPurchaseReceiptListener (movement + balance + monthly)
  → LedgerOnPurchaseReceiptListener (partner_ledger_monthly)
```

### 6.3 테이블

| 신규 | 레거시 |
|------|--------|
| `purchase_order` | BO_HT |
| `purchase_receipt` | BD_HT |
| `purchase_history` | B_HT |

### 6.4 API

- `POST /api/v1/purchasing/receipts`
- 검사품목: `progress_condition=대기`, 이벤트 미발행 → 검사완료 후 재발행

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
| BS_MT | SALES_1~3 |
| DS_MT | DELIVERY |
| PS_MT | WIP + process_sequence/code |
| OS_MT | OUTSOURCE + partner_id |
| BSI_MT 월컬럼 | partner_ledger_monthly unpivot |

더미 공정코드는 ETL 시 `location`만 채우고 `process_code` null 가능.

---

## 10. 구현 Wave

| Wave | 내용 |
|------|------|
| **INF-1** | inventory + ledger 서비스 | ✅ |
| **INF-2** | FiscalCalendar, MonthClosing 연동 | ✅ |
| **TX1** | 구매입고 E2E + 통합 테스트 | ✅ |
| **INF-3** | FiscalYearRolloverJob |
| **INF-4** | 외주 발주/입고 시 OUTSOURCE 잔고 |

---

## 11. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-06-22 | 초안 ? 통합재고, 원장, 구매입고, 연도운영 |
