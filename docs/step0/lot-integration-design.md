# SmartManager — LOT 연동 설계서

> **문서 버전:** 2.5  
> **작성일:** 2026-07-10  
> **개정:** 2026-07-12 — §8.3 BOM 정전개 Lot 일괄 ON(3단계)  
> **상태:** TO-BE — **LOT-5 완료** · BOM 정전개 Lot 표시·행별·일괄 ON  
> **관련 문서:**  
> - [D5 Lot 추적 개요](./d5-lot-traceability.md)  
> - [재고·원장 SSOT](../../results/sample/inventory-ledger-spec.md)  
> - [TX 취소·삭제 정책](./tx-cancel-delete-policy.md)  
> - [Domain Event · Projector 매핑](./domain-event-projector-matrix.md)  
> - 스키마 초안 [`schema-drafts/V007__lot_traceability.sql`](./schema-drafts/V007__lot_traceability.sql) · [`V078`](./schema-drafts/V078__inventory_lot.sql) · [`V079`](./schema-drafts/V079__stock_movement_lot_and_genealogy.sql)  
> - 레거시 스키마 [`results/mariadb_schema.sql`](../../results/mariadb_schema.sql) (`LotLedger`, `LotNum` 컬럼)

---

## 1. 문서 목적

본 문서는 **현재 SmartManager 프로젝트**(슬롯 집계 재고 운영 중)에 **LOT(배치) 추적**을 어떻게 연결할지 정의한다.

| 구분 | 내용 |
|------|------|
| **대상** | 백엔드(Spring Boot 3.5), DB(Flyway), 프론트엔드(React) |
| **범위** | 데이터 모델, 재고 진입점 확장, TX별 Lot 생성·이동·소비, API·화면, 마이그레이션 Wave |
| **범위 외** | 유효기간·시험성적서 UI, Lot 분할·병합 화면, 글로벌 Lot 번호 체계 |

---

## 2. 현재 상태 (AS-IS)

### 2.1 구현된 것

SmartManager는 **품목 × 창고 슬롯 × 수량** 단위 재고를 운영한다.

```text
inventory_location (RAW, SALES, DELIVERY, WIP, OUTSOURCE)
        ↓
inventory_balance          ← 슬롯 현재고 (UK: item×location×연도×공정×거래처)
        ↓
stock_movement (V028)      ← append-only 입출고 원장
inventory_balance_monthly  ← 슬롯별 월별 입·출·잔
```

- **단일 진입점:** `InventoryBalanceService.recordMovement(RecordStockMovementCommand)`
- **모든 TX 재고 반영:** `*InventoryService`가 위 메서드를 동기 호출
- **취소 정책:** 역방향 `stock_movement` INSERT + `*_CANCEL` reference_type ([tx-cancel-delete-policy](./tx-cancel-delete-policy.md))

### 2.2 구현 현황 (LOT-5 기준)

| 항목 | 상태 |
|------|------|
| `item.lot_tracked` | ✅ V077 · 품목/일괄등록 UI (기본 0) |
| `item.model_type` 필수 | ✅ V077 · API/화면 필수 |
| `drawing_master.model_type` | ✅ V077 (`model_group` 개명) — 품목 기종과 동일 개념 |
| `inventory_lot`, `inventory_lot_balance`, `lot_genealogy` | ✅ V078 · V079 |
| `stock_movement.lot_id` | ✅ V079 |
| Lot CRUD API | ✅ `/api/v1/inventory/lots` · available · genealogy · movements |
| Lot 조회·관리 화면 | ✅ 재고·원장 Lot 탭 · Lot 마스터 (`inventory-lot`) |
| 구매입고·품질·작업실적·외주·영업·기타입출고 Lot | ✅ LOT-2~4 TX 연동 |
| 레거시 `LotLedger` ETL | **N/A** — 레거시 Lot 미운영 (§11) |

**결론:** `lot_tracked = 1` 품목은 **신규 TX부터** Lot 생성·이동·계보·원장 조회가 가능하다.  
과거 레거시 재고는 슬롯 집계만 유지한다(ETL 스킵).  
의도적 비범위: Lot 분할·병합 UI, FIFO 자동 배정, 입출고 이력 탭 `lotNo` 컬럼(후속).  
BOM 정전개 Lot 표시·행별·일괄 ON: §8.3.

### 2.3 기타구매입고

`EtcPurchaseReceiptService`는 **재고·Lot 반영 대상이 아니다** (매입·미지급 원장만). 본 설계의 LOT Wave에 포함하지 않는다.

---

## 3. 연동 목표 (TO-BE)

### 3.1 비즈니스 목표

1. **선택적 Lot 추적** — 품목별 `lot_tracked = true`일 때만 Lot 필수
2. **입고 시 Lot 식별** — 구매입고·외주입고·생산 산출 시 Lot 생성 또는 지정
3. **출고·투입 시 Lot 지정** — 작업실적 투입, 외주출고, 영업출고에서 소비 Lot 선택
4. **양방향 계보** — 원자재 Lot → 공정 투입 → 완제품 Lot (`lot_genealogy`)
5. **감사 가능** — Lot 이동은 `stock_movement`에 남기고 물리 DELETE 금지

### 3.2 레거시 대응

| 레거시 | SmartManager TO-BE |
|--------|-------------------|
| `LotLedger` (Lot 마스터 화면) | `inventory_lot` + 관리 API |
| `BD_HT`, `SH_HT` 등의 `LotNum` | TX 시점 `inventory_lot` 생성 + `stock_movement.lot_id` |
| `LotNumberManagement.aspx` | 품목 화면 `lot_tracked` + (선택) Lot 수동 등록 API |

레거시 `LotLedger`는 입출고 이력과 FK가 없었다. SmartManager는 **TX와 Lot를 1:1로 묶어** 추적성을 확보한다.

---

## 4. 핵심 설계 원칙

### 4.1 두 계층 분리

```text
┌──────────────────────────────────────────────────────────────┐
│  슬롯 집계 (기존)                                              │
│  inventory_balance + inventory_balance_monthly                 │
│  → 품목×창고×공정×거래처 합계 (Lot 무관)                        │
└──────────────────────────────────────────────────────────────┘
                              ↑
                    SUM(lot_balance) ≈ 슬롯 검증
                              ↑
┌──────────────────────────────────────────────────────────────┐
│  Lot 계층 (신규)                                               │
│  inventory_lot → inventory_lot_balance → stock_movement.lot_id │
│  lot_genealogy (부모↔자식)                                     │
└──────────────────────────────────────────────────────────────┘
```

- **Projector(기준정보)** 는 슬롯만 만든다 (`WipBalanceProjector`, `OutsourceInputBalanceProjector`).
- **Lot 생성·이동은 TX 전용**이다. Projector가 Lot를 만들지 않는다.

### 4.2 단일 진입점 유지

현재 코드베이스는 TX 재고를 **동기 `*InventoryService` → `recordMovement`** 로 처리한다.  
Lot 연동도 **동일 패턴**을 따른다. (D5 초안의 `InventoryOn*Listener`는 채택하지 않음)

```text
TX Service
  → *InventoryService (기존)
      → LotInventoryService.applyLotMovement()  ← 신규 (lot_tracked 검증·잔량)
      → InventoryBalanceService.recordMovement() ← 기존 (슬롯 집계)
```

이벤트 기반 비동기 Listener는 Wave 2 이후 리플레이·대량 재계산 시에만 검토한다.

### 4.3 취소·역전

- Lot 잔량도 **append-only** — 취소 TX는 반대 방향 `stock_movement` + `inventory_lot_balance` 감산
- Lot 상태 `DEPLETED` 복구: 취소 시 `ACTIVE`로 되돌림 (잔량 > 0일 때)
- [tx-cancel-delete-policy](./tx-cancel-delete-policy.md)와 동일: `stock_movement` 물리 DELETE 금지

---

## 5. 데이터 모델

### 5.1 신규·변경 테이블

> **주의:** `schema-drafts/V007`은 `stock_movement` 전체 CREATE 스케치이다.  
> **실제 적용 시 V028이 이미 존재**하므로 아래처럼 **ALTER + 신규 테이블**로 분리한다.

#### (1) `item` — Lot 추적 플래그

```sql
ALTER TABLE item
  ADD COLUMN lot_tracked TINYINT(1) NOT NULL DEFAULT 0
    COMMENT '1=입출고 시 lot_id 필수';
```

#### (2) `inventory_lot` — Lot 마스터

| 컬럼 | 설명 |
|------|------|
| `item_id`, `lot_no` | UK `(item_id, lot_no, recording_state)` — **품목 내 유일** |
| `status` | `ACTIVE` \| `BLOCKED` \| `DEPLETED` |
| `origin_type` | `PURCHASE`, `PRODUCTION`, `MANUAL`, `SPLIT`, `MERGE`, `ADJUSTMENT` |
| `origin_doc_type`, `origin_doc_id` | 최초 생성 TX 참조 |
| `expiry_date`, `certificate_ref` | v1.0 컬럼만, UI 후순위 |

#### (3) `inventory_lot_balance` — Lot × 슬롯 현재고

| UK | `(lot_id, inventory_balance_id, recording_state)` |
|----|---------------------------------------------------|
| `qty_on_hand` | Lot별 슬롯 잔량 (음수 금지) |

#### (4) `lot_genealogy` — 계보

| `link_type` | 용도 |
|-------------|------|
| `CONSUME` | 작업실적 투입 — 부모 Lot 소비 |
| `PRODUCE` | 작업실적 산출 — 자식 Lot 생성 |
| `SPLIT` / `MERGE` | Wave 2+ |

#### (5) `stock_movement` — 컬럼 추가 (V028 확장)

```sql
ALTER TABLE stock_movement
  ADD COLUMN lot_id BIGINT NULL COMMENT 'lot_tracked 품목은 NOT NULL',
  ADD INDEX idx_stock_movement_lot (lot_id, movement_date, recording_state),
  ADD CONSTRAINT fk_stock_movement_lot
    FOREIGN KEY (lot_id) REFERENCES inventory_lot (id);
```

#### (6) `lot_number_sequence` — 자동 번호 (Wave 2, 선택)

`{item_no}-{yyyyMMdd}-{seq}` 규칙용.

### 5.2 정합성 규칙 (애플리케이션)

```text
∀ 슬롯 B:
  SUM(inventory_lot_balance.qty_on_hand WHERE balance_id = B)
    = inventory_balance.stock_qty        (현재고)
    ≈ inventory_balance_monthly.stock_qty  (해당 월)

∀ lot_tracked 품목의 movement M:
  M.lot_id IS NOT NULL

∀ non-lot_tracked 품목:
  M.lot_id IS NULL
  inventory_lot_balance 행 없음
```

일일 배치 또는 TX 종료 시 슬롯·Lot 합계 불일치를 로깅한다 (v1.0: 예외 throw, v1.1: 관리 화면).

---

## 6. 애플리케이션 아키텍처

### 6.1 신규 컴포넌트

| 컴포넌트 | 패키지 | 역할 |
|----------|--------|------|
| `LotService` | `application.inventory` | Lot CRUD, 번호 중복 검증, 자동 채번 |
| `LotInventoryService` | `application.inventory` | `inventory_lot_balance` 갱신, OUT 시 잔량 검증 |
| `LotRepository` | `application.inventory` | JPA 인프라 |
| `RecordStockMovementCommand` | 기존 확장 | `Long lotId` (nullable) 추가 |
| `InventoryBalanceService` | 기존 확장 | `lot_tracked`이면 `LotInventoryService` 선행 호출 |

### 6.2 `RecordStockMovementCommand` 확장

```java
public record RecordStockMovementCommand(
        long itemId,
        String locationCode,
        LocalDate movementDate,
        StockMovementType movementType,
        BigDecimal qty,
        BigDecimal amount,
        String referenceType,
        long referenceId,
        Long outputProcessId,
        Long inputProcessId,
        Long partnerId,
        Long lotId,           // 신규 — lot_tracked 품목 필수
        String actorUserId
) { ... }
```

### 6.3 `LotService` 주요 API (애플리케이션)

| 메서드 | 설명 |
|--------|------|
| `createOnReceipt(itemId, lotNo, originType, docType, docId)` | 구매·외주 입고 시 Lot 생성 |
| `createOnProduction(itemId, lotNo, parentLotIds, qty)` | 작업실적 산출 Lot + genealogy |
| `resolveOrCreate(itemId, lotNo, autoGenerate)` | 수동 입력 또는 자동 채번 |
| `assertSufficientLotQty(lotId, balanceId, qty)` | 출고·투입 전 검증 |
| `findAvailableLots(itemId, locationCode, processId)` | 출고·투입 Lot 선택. 영업출고 공정품은 `WIP`+최종공정Id |

### 6.4 REST API (신규)

Base: `/api/v1/inventory/lots`

| Method | Endpoint | 권한 | 설명 |
|--------|----------|------|------|
| `GET` | `/` | `inventory:lot:read` | 품목·창고·상태 필터 |
| `GET` | `/available` | `inventory:lot:read` | 출고·투입 가능 Lot (`qty>0`, ACTIVE) |
| `GET` | `/{id}` | `inventory:lot:read` | 상세 + 슬롯별 잔량 |
| `GET` | `/{id}/genealogy` | `inventory:lot:read` | `direction=UP\|DOWN` 계보 (V079+) |
| `GET` | `/{id}/movements` | `inventory:lot:read` | `stock_movement` 페이지 (V079+) |
| `POST` | `/` | `inventory:lot:write` | 수동 Lot 등록 (관리자) |
| `PUT` | `/{id}` | `inventory:lot:write` | 상태·비고 등 수정 |
| `DELETE` | `/{id}` | `inventory:lot:write` | 소프트 삭제 (`recording_state=0`, 잔량 0만) |

기존 TX API는 라인 DTO에 아래 필드를 **선택 추가**한다.

```typescript
type LotLineInput = {
  lotNo?: string;           // 수동
  autoGenerateLot?: boolean; // 자동 채번
  lotId?: number;           // 출고·투입 시 기존 Lot 지정
};
```

---

## 7. 트랜잭션별 LOT 연동

### 7.1 연동 맵 (전체)

| # | 화면 | TX | Inventory Service | Lot 동작 | Wave |
|---|------|-----|-------------------|----------|------|
| 1 | 구매입고 | `PurchaseReceiptService` | `PurchaseReceiptService` 내 직접 | 입고 시 Lot **생성** + RAW IN | LOT-2 |
| 2 | 품질검사 | `QualityInspectionService` | 동일 | 검사품: 합격 시 Lot 생성·IN (입고 시 미생성이면) | LOT-2 |
| 3 | 자재투입 | `MaterialIssueInventoryService` | RAW OUT | 투입 Lot **소비** | LOT-3 |
| 4 | 작업실적 | `WorkReportInventoryService` | WIP/SALES 이동 | 산출 Lot **생성** + PRODUCE genealogy | LOT-3 |
| 5 | 작업실적 투입 | `WorkReportConsumptionInventoryService` | RAW/WIP OUT | 투입 Lot CONSUME + genealogy | LOT-3 |
| 6 | 외주출고 | `OutsourcingShipmentInventoryService` | OUTSOURCE/WIP | Lot **이동** (슬롯 간) | LOT-4 |
| 7 | 외주입고 | `OutsourcingReceiptInventoryService` | WIP IN | Lot 생성 또는 기존 Lot 입고 | LOT-4 |
| 8 | 영업출고 | `SalesShipmentInventoryService` | **(A)** 상품·제품: SALES OUT + DELIVERY IN<br>**(B)** 공정품: WIP(최종공정) OUT + DELIVERY IN | 원창고→DELIVERY **Lot 슬롯 이동** (동일 `lot_id`) | LOT-4 |
| 9 | 매출인식 | `SalesRevenueInventoryService` | DELIVERY OUT | DELIVERY Lot 잔량↓ · 전 슬롯 0이면 `DEPLETED` | LOT-4 |
| 10 | 기타입출고 | `MiscStockMovementInventoryService` | IN/OUT | Lot 수동 지정 (관리자) | LOT-4 |
| — | 기타구매입고 | `EtcPurchaseReceiptService` | **없음** | **미적용** | — |

### 7.2 구매입고 (LOT-2) — 시퀀스

```mermaid
sequenceDiagram
    participant UI as PurchaseReceiptPage
    participant TX as PurchaseReceiptService
    participant Lot as LotService
    participant Inv as InventoryBalanceService

    UI->>TX: 입고 등록 (lines[].lotNo)
    TX->>TX: purchase_receipt INSERT
    alt 무검사 NONE
        TX->>Lot: createOnReceipt(itemId, lotNo)
        Lot-->>TX: lotId
        TX->>Inv: recordMovement(IN, lotId)
    else 검사품 INSPECTION
        TX->>TX: quality_inspection PENDING
        Note over TX: 창고·Lot 보류
    end
    Note over UI,Inv: 검사완료 시
    TX->>Lot: createOnReceipt(...)
    TX->>Inv: recordMovement(IN, lotId)
```

**UI 변경 (`PurchaseReceiptPage`):**
- `lot_tracked` 품목 라인에 `Lot 번호` 입력 또는 `자동생성` 체크
- 무검사: 입고 등록 시 필수
- 검사품: 검사완료 화면에서 Lot 입력 (또는 입고 시 예약 번호)

### 7.3 작업실적 (LOT-3) — 시퀀스

```mermaid
sequenceDiagram
    participant UI as WorkReportPage
    participant TX as WorkReportService
    participant Lot as LotService
    participant Inv as InventoryBalanceService

    UI->>TX: 실적 등록 (투입 lots[], 산출 lotNo)
    loop 투입 라인
        TX->>Lot: assertSufficientLotQty + CONSUME
        TX->>Inv: recordMovement(OUT, lotId)
    end
    TX->>Lot: createOnProduction + PRODUCE genealogy
    TX->>Inv: recordMovement(IN, childLotId)
```

**UI 변경 (`WorkReportPage`):**
- 투입 탭: `lot_tracked` 자재에 **보유 Lot 목록** 드롭다운 (창고·공정 필터)
- 산출 탭: 완제품 `lot_tracked`이면 산출 Lot 번호 입력/자동

### 7.4 영업출고 (LOT-4)

AS-IS 재고 경로(`SalesShipmentInventoryService`, `PropertyClassification`)와 **동일하게** Lot를 맞춘다.

| 품목 분류 | 출고 원창고 | 도착 | 코드 기준 |
|-----------|-------------|------|-----------|
| **상품·제품** | `SALES` | `DELIVERY` IN | 기본 경로 |
| **공정품** | `WIP` + **최종 사내 공정** (`process_id`) | `DELIVERY` IN | `shipmentFromWipFinalProcess() == true` |

```text
(A) 상품·제품
  Lot @ SALES  ──OUT──►  (잔량↓)     DELIVERY ──IN──►  Lot @ DELIVERY (동일 lot_id 이동)
(B) 공정품
  Lot @ WIP(최종공정) ──OUT──►       DELIVERY ──IN──►  Lot @ DELIVERY (동일 lot_id 이동)
```

- **Lot 의미:** “소비(소멸)”가 아니라 **원창고 → 납품창고 슬롯 이동**. 매출인식(`SalesRevenueInventoryService`)에서 `DELIVERY` OUT 시 최종 소진·`DEPLETED` 후보.
- `SalesShipmentPage`: 출고 라인별 Lot 선택. 후보 조회는 분류에 따라  
  - 상품·제품 → `findAvailableLots(itemId, SALES, processId=null)`  
  - 공정품 → `findAvailableLots(itemId, WIP, processId=최종공정Id)`
- 출고 수량 ≤ 선택 Lot의 **해당 슬롯 잔량** 검증
- 원창고 슬롯 잔량 0이 되어도 Lot 마스터는 DELIVERY에 잔량이 있으면 `ACTIVE` 유지. `DEPLETED`는 **전 슬롯 잔량 합 = 0**일 때(통상 매출인식 후)
- FIFO 기본 정렬 옵션 — v1.1

취소 시 AS-IS와 동일하게 역방향: DELIVERY OUT + (SALES|WIP) IN, 동일 `lot_id`.

### 7.5 취소 TX

각 `*InventoryService`의 cancel 메서드에서:
1. 원본 `stock_movement`의 `lot_id` 조회
2. 역방향 movement INSERT (`reference_type = *_CANCEL`)
3. `inventory_lot_balance` 역갱신
4. genealogy 역링크는 v1.0에서 **소프트 무효화** (`recording_state = 0`) — 물리 DELETE 금지

---

## 8. 화면·API 변경 요약

| 화면 | 변경 |
|------|------|
| **품목** (`ItemPage`) | `Lot 추적` 체크박스 (`lot_tracked`) |
| **재고·원장** (`InventoryLedgerPage`) | 탭 추가: **Lot 잔량** / Lot 이력·계보 drill-down (LOT-5 UI) |
| **Lot 마스터** (`LotMasterPage`) | 목록·수동 등록·상태 변경·잔량0 삭제 (LOT-5 UI) |
| **품목구성** (`ItemCompositionPage`) | 정전개 Lot 표시·행별·일괄 ON (§8.3) |
| **구매입고** | 라인별 Lot 입력 |
| **품질검사** | 검사완료 시 Lot (검사품) |
| **작업실적** | 투입 Lot 선택, 산출 Lot 생성 |
| **외주출고·입고** | Lot 이동·생성 |
| **영업출고** | Lot 선택 출고 — 상품·제품은 SALES, 공정품은 WIP(최종공정) 후보 |
| **기타입출고** | Lot 수동 지정 (선택) |

### 8.1 재고·원장 — Lot 탭 (LOT-5 UI)

**목적:** 슬롯 잔고(재고 잔고 탭)와 별도로, **Lot 단위 잔량·이력·계보**를 한 화면에서 조회한다.  
등록·상태 변경은 §8.2 **Lot 마스터** 메뉴에서 수행한다.

| 구분 | 내용 |
|------|------|
| 메뉴 | 기존 `inventory-ledger` (재고·원장) 3번째 탭 **Lot** |
| 권한 | `inventory:lot:read` (목록·상세·이력·계보) — MANAGER/ADMIN은 V078에서 ledger·lot 동시 부여 |
| 필터 | 품번(`itemNo`), Lot번호(`lotNo`), 창고(`locationCode`), 상태(`ACTIVE`/`BLOCKED`/`DEPLETED`) |
| 목록 | `GET /api/v1/inventory/lots` — Lot 행 + `balances[]` 슬롯 잔량. 표시용 **총잔량** = `sum(qtyOnHand)` |
| drill-down | 행 선택 시 패널: (1) 슬롯 잔량 (2) Lot 입출고 이력 (3) 계보 UP/DOWN |
| 이력 API | `GET /api/v1/inventory/lots/{id}/movements` — `stock_movement.lot_id` 기준, 최대 500건, 일자·id DESC |
| 계보 API | `GET /api/v1/inventory/lots/{id}/genealogy?direction=UP\|DOWN` (기존) |
| 비범위 | Lot 병합·분할 UI, FIFO 자동 배정, 입출고 이력 탭의 `lotNo` 컬럼(후속), 수동 등록·상태 변경(→ §8.2) |

**조회 흐름**

```text
[필터] → GET /lots → 목록 선택
                      ├─ balances (응답 내)
                      ├─ GET /lots/{id}/movements
                      └─ GET /lots/{id}/genealogy?direction=UP|DOWN
                         └─ 계보 행 클릭 → 해당 lotId로 재선택(drill)
```

### 8.2 Lot 마스터 화면 (선택 · LOT-5 UI)

**목적:** 레거시 `LotNumberManagement` / `LotLedger`에 대응하는 **Lot 마스터 CRUD**.  
TX 중 자동 생성되지 않은 Lot를 사전 등록하거나, 품질 이슈 등으로 **상태(ACTIVE/BLOCKED/DEPLETED)**·비고를 변경한다.

| 구분 | 내용 |
|------|------|
| 메뉴 | 재고 → **Lot 마스터** (`inventory-lot`) |
| 권한 | 조회 `inventory:lot:read` · 등록/수정/삭제 `inventory:lot:write` |
| 필터 | 품번, Lot번호, 창고, 상태 (재고·원장 Lot 탭과 동일) |
| 목록 | `GET /api/v1/inventory/lots` — 선택 시 수정 폼 로드 |
| 등록 | `POST /api/v1/inventory/lots` — 품목(`lot_tracked=1`만), Lot번호 수동 또는 `autoGenerate`, origin=`MANUAL` |
| 수정 | `PUT /api/v1/inventory/lots/{id}` — 상태·P1/P2·유효기한·성적서·비고 (품목·Lot번호 변경 불가) |
| 삭제 | `DELETE /api/v1/inventory/lots/{id}` — 전 슬롯 잔량 0일 때만 소프트 삭제 |
| 비범위 | 재고 수량 직접 조정(기타입출고·TX 경로), 병합·분할 UI, 계보 편집 |

**등록 규칙**

```text
품목.lot_tracked = 1 필수 (백엔드·UI 검증)
lotNo 수동 입력 XOR autoGenerate=true
품목+lotNo UK — 중복 시 거부
생성 직후 status=ACTIVE, inventory_lot_balance 없음(잔량 0)
```

**상태 변경 가이드**

| 상태 | 의미 | UI |
|------|------|-----|
| `ACTIVE` | 출고·투입 가능 | 기본 |
| `BLOCKED` | 품질 등 보류 — available 조회에서 제외 | 수동 전환 |
| `DEPLETED` | 전 슬롯 잔량 0 (시스템도 자동 설정 가능) | 수동 표시 가능, 잔량 있으면 비권장 |

재고·원장 Lot 탭과의 역할: **원장 Lot 탭 = 조회·추적**, **Lot 마스터 = 등록·유지보수**.

### 8.3 BOM 정전개 — Lot 추적 표시·행별·일괄 설정

**목적:** 모품목 정전개 시 BOM 트리 각 노드의 `item.lot_tracked`를 확인하고, 품목 단위로 Lot 추적을 켠다.

| 구분 | 내용 |
|------|------|
| 화면 | 기준정보 → 품목구성 → **정전개** 모달 |
| API 조회 | `GET .../explosion` — `BomTreeNode.itemId`, `lotTracked` |
| API 행별 | `PATCH /api/v1/basis/items/{id}/lot-tracked` |
| API 일괄 미리보기 | `POST .../plan/{itemNum}/lot-tracked/enable-preview` |
| API 일괄 적용 | `POST .../plan/{itemNum}/lot-tracked/enable` — `{ "itemIds": [...] }` (`basis:item:write`) |
| **1단계** | Lot추적 컬럼·엑셀 표시 |
| **2단계** | 행별 설정/해제 (해제 시 확인) |
| **3단계** | **트리 일괄 ON만** — 미리보기(대상·이미 ON 제외)·**공유 경고**(정전개 트리 밖의 모품목) |
| 일괄 OFF | **하지 않음** (실수·잔량 리스크) |

**공유 경고 정의:** 자품목의 활성 BOM 모품목 중, **현재 정전개 트리에 포함되지 않은** 모품목 번호 목록.

```text
정전개 → enable-preview(루트)
       → 대상 목록 + otherParentItemNos[]
       → 사용자 확인·선택 → enable(itemIds)
       → 정전개 재조회
```

---

## 9. Flyway 마이그레이션 전략

> **번호 충돌 주의:** 저장소에 이미 `V070`~`V076`이 존재한다  
> (VIEWER 권한·마감컷오버·도면 V072~V076). Lot는 **`V077`부터** 부여한다.  
> 초안 파일명 `schema-drafts/V007__*` 는 역사적 스케치이며, 실적용 시 아래 번호로 분할·복사한다.

| 실적용 버전 | 내용 | 비고 |
|-------------|------|------|
| **V077** | `item.lot_tracked`, `item.model_type` NOT NULL, `drawing_master.model_group`→`model_type` | ✅ 적용 (품목·도면 기종 통일) |
| **V078** | `inventory_lot`, `inventory_lot_balance`, `lot_number_sequence`, `inventory:lot:read\|write` | ✅ 적용 (Lot 마스터 + CRUD API) |
| **V079** | `stock_movement.lot_id` FK, `lot_genealogy` | ✅ 적용 (genealogy 링크·조회 API 포함) |
| **V080** | `work_report_consumption_line.lot_id`, `work_report.output_lot_id` | ✅ 적용 (작업일보 투입 Lot UI·산출 Lot) |
| **V081** | TX 라인 `lot_id` (sales_shipment/revenue, outsourcing input/receipt, misc) | ✅ 적용 (LOT-4) |

`schema-drafts/V007` 전체를 그대로 적용하지 않는다. V028 `stock_movement` 스키마(`movement_type`, `reference_type`, `fiscal_month` 등)를 유지한다.

적용 전: `db/migration` 최신 번호가 `V076`인지 재확인. 도면 등 후속 마이그레이션이 더 있으면 Lot 시작 번호를 그에 맞게 올린다.

---

## 10. 구현 Wave

```text
LOT-0  문서 확정 (본 문서 + d5) — v1.1 Flyway·출고 경로 보정 포함
  ↓
LOT-1  Flyway V077(품목 lot_tracked·기종필수·도면 model_type) · 이어서 V078 Lot 테이블 · LotService · Lot CRUD API
  ↓
LOT-2  구매입고 + 품질검사 Lot 생성·RAW 입고
  ↓
LOT-3  작업실적 투입/산출 + lot_genealogy
  ↓
LOT-4  외주·영업(상품·제품 SALES / 공정품 WIP)·기타입출고 Lot 이동·소비
  ↓
LOT-5  재고 원장 Lot 탭 · Lot 마스터 화면 · 레거시 ETL N/A(스킵)
```

### 선행 조건 (이미 충족)

- [x] `stock_movement` / `inventory_balance_monthly` (V028)
- [x] 구매입고·작업실적·출고 TX + `*InventoryService`
- [x] `InventoryBalanceService` 단일 진입점

### Wave별 완료 기준

| Wave | 완료 기준 |
|------|-----------|
| LOT-1 | Lot 수동 등록 API, 품목 `lot_tracked` 저장, 빈 DB 마이그레이션 성공 |
| LOT-2 | 무검사 구매입고 E2E: Lot 생성 → RAW 잔량 → 원장 조회 |
| LOT-3 | 작업실적 1건: 투입 Lot 소비 + 산출 Lot 생성 + genealogy(CONSUME/PRODUCE) + 조회 API |
| LOT-4 | 영업출고 1건(상품·제품 SALES) + 공정품 1건(WIP 최종공정) → DELIVERY Lot 잔량 확인 |
| LOT-5 | 재고·원장 Lot 탭 ✅ · Lot 마스터 화면 ✅ · 레거시 ETL **N/A(스킵)** ✅ |

---

## 11. 레거시 ETL (LOT-5) — **N/A (스킵)**

> **결정 (2026-07-12):** 레거시 운영에서 Lot가 **구현·사용되지 않았다.**  
> 스키마에 `LotLedger`·TX `LotNum` 컬럼은 존재하나 실데이터가 없거나 미사용이므로  
> **Cut-over ETL·검증 리포트를 수행하지 않는다.**

| 항목 | 조치 |
|------|------|
| `LotLedger` → `inventory_lot` | **스킵** |
| `BD_HT.LotNum` 등 TX 소급 | **스킵** |
| `stock_movement.lot_id` backfill | **스킵** |
| `lot_tracked` Cut-over 일괄 ON | **스킵** — 품목 마스터에서 필요 시 개별 설정 (기본 0) |

**운영 방침:** Lot 추적은 SmartManager **신규 TX부터** 적용한다.  
과거 재고는 슬롯 집계(`inventory_balance`)만 유지하며, Lot 잔량·계보는 이관하지 않는다.

**(참고) 만일 나중에 레거시 Lot 실사용이 확인되면** 아래 매핑을 재개한다.

| 레거시 | TO-BE |
|--------|-------|
| `LotLedger` | `inventory_lot` (`origin_type = MANUAL`) |
| `BD_HT.LotNum` 등 TX | `inventory_lot` + `stock_movement` 소급 (가능한 경우) |
| `LotLedger.P1`, `P2` | `inventory_lot.p1`, `p2` |

---

## 12. Lot 번호 정책 (v1.0)

| 방식 | 규칙 | 적용 |
|------|------|------|
| **수동** | 사용자 입력, 품목 내 UK 검증 | LOT-2 |
| **자동** | `{item_no}-{yyyyMMdd}-{seq:3}` | LOT-3+ (`lot_number_sequence`) |

레거시 `P1`/`P2`는 자동 규칙 확장용으로 컬럼만 보존한다.

---

## 13. 미결정 · v1.1 검토

| # | 항목 | v1.0 가정 |
|---|------|-----------|
| 1 | 출고 Lot 선택 기본 정렬 | 수동 선택만 (FIFO 자동 배정 없음) |
| 2 | 동일 Lot의 다중 슬롯 보유 | 허용 (`inventory_lot_balance` 복수 행) |
| 3 | Lot 병합·분할 UI | API·genealogy만, 화면 없음 |
| 4 | 품질검사 불합격 Lot | `BLOCKED` 상태, 창고 미반영 |
| 5 | 기타구매입고 Lot | 미적용 (재고 없음) |
| 6 | 이벤트 Listener 전환 | 하지 않음 — 동기 InventoryService 유지 |

---

## 14. 체크리스트 (구현 시)

- [x] Flyway V077 (`lot_tracked` · `model_type` 필수 · 도면 `model_type` 통일)
- [x] `item.lot_tracked` API·UI · 일괄등록
- [x] 품목 `modelType` 필수 (등록·수정·일괄)
- [x] Flyway V078 (`inventory_lot` · balance · sequence · `inventory:lot:*`)
- [x] `LotService` · `/api/v1/inventory/lots` CRUD·조회·available
- [x] Flyway V079 (`stock_movement.lot_id` · `lot_genealogy`)
- [x] `RecordStockMovementCommand.lotId` + `LotInventoryService`
- [x] `PurchaseReceiptService` / 품질검사 Lot 연동 (무검사·검사완료)
- [x] `WorkReport*InventoryService` + genealogy (CONSUME/PRODUCE · 취소 소프트 무효화 · `GET /lots/{id}/genealogy`)
- [x] SalesShipmentInventoryService Lot 이동 (SALES|WIP → DELIVERY)
- [x] SalesRevenueInventoryService DELIVERY Lot OUT
- [x] OutsourcingShipmentInventoryService / OutsourcingReceiptInventoryService Lot 이동·입고 생성
- [x] MiscStockMovementInventoryService Lot 지정
- [x] Flyway V081 TX lot_id 컬럼
- [x] `domain-event-projector-matrix.md` §11 Lot 절 추가
- [x] `inventory-ledger-spec.md` §3.4 `lot_id` 반영
- [x] 통합 테스트: 입고 → 투입 → 산출 → 출고 E2E (`LotTraceabilityFlowTest`)
- [x] 재고·원장 Lot 탭: 잔량 목록 + 슬롯/이력/계보 drill-down · `GET /lots/{id}/movements`
- [x] Lot 마스터 화면: 목록·수동 등록·상태 변경·잔량0 삭제 (`inventory-lot`)
- [x] 레거시 `LotLedger`·TX `LotNum` ETL — **N/A(스킵)** (레거시 Lot 미운영, §11)
- [x] BOM 정전개 Lot 추적 컬럼 표시 (§8.3 1단계)
- [x] BOM 정전개 행별 Lot 설정 (§8.3 2단계 · `PATCH .../lot-tracked`)
- [x] BOM 정전개 트리 Lot 일괄 ON (§8.3 3단계 · preview/enable)

---

## 15. 참고 — 왜 Event Listener가 아닌가

D5 초안은 `PurchaseReceiptPosted` 등 **도메인 이벤트 Listener**를 제안했으나, 현재 SmartManager TX는:

1. 같은 `@Transactional` 내에서 `*InventoryService`가 **동기** 호출된다.
2. `PurchaseReceiptPosted` 같은 이벤트 타입이 **아직 정의·발행되지 않았다**.
3. Lot 검증 실패 시 TX 전체 롤백이 필요하다.

따라서 v1.0은 **기존 InventoryService 확장**이 일관되고 구현 비용이 낮다.  
이벤트 발행은 Lot 연동 **이후** 감사·외부 연동 목적으로 추가할 수 있다.

---

## 16. 변경 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-07-10 | LOT 연동 초안 확정 |
| 1.1 | 2026-07-12 | Flyway **V077–V079**로 재부여(기존 V070–V076과 충돌 해소). 영업출고 Lot를 AS-IS 이중 경로(상품·제품 SALES / 공정품 WIP 최종공정 → DELIVERY)에 맞춤. 출고 시 DEPLETED는 전 슬롯 잔량 0 기준으로 정정 |
| 1.2 | 2026-07-12 | V077 적용: `item.lot_tracked`·`model_type` 필수·도면 `model_group`→`model_type`. 품목/일괄등록 UI 반영. Lot 테이블은 V078로 순연 |
| 1.3 | 2026-07-12 | V078 적용: `inventory_lot`·balance·sequence·권한. `LotService`·`/api/v1/inventory/lots` CRUD·available. genealogy·TX 연동은 V079/LOT-2+ |
| 1.4 | 2026-07-12 | V079·LOT-2: `stock_movement.lot_id`·`lot_genealogy`·`LotInventoryService`. 구매입고/품질검사 Lot 생성·RAW/SALES IN·취소 역분개 |
| 1.5 | 2026-07-12 | V080·작업일보 투입/산출 Lot UI·WIP 슬롯 정합 |
| 1.6 | 2026-07-12 | LOT-3 genealogy: 작업실적 CONSUME/PRODUCE 기록·취소 소프트 무효화·`GET /lots/{id}/genealogy` |
| 1.7 | 2026-07-12 | LOT-4: V081 TX `lot_id`, 영업출고·매출·외주출고/입고·기타입출고 Inventory/API/FE Lot 연동 |
| 1.8 | 2026-07-12 | 체크리스트 잔여: projector-matrix §11 · inventory-ledger §3.4 · `LotTraceabilityFlowTest` E2E |
| 1.9 | 2026-07-12 | LOT-5 UI: 재고·원장 Lot 탭 · `GET /lots/{id}/movements` · §8.1 설계 |
| 2.0 | 2026-07-12 | LOT-5 UI: Lot 마스터 화면 · §8.2 · 메뉴 `inventory-lot` |
| 2.1 | 2026-07-12 | §11 레거시 Lot ETL **N/A(스킵)** · 체크리스트·LOT-5 완료 기준 반영 |
| 2.2 | 2026-07-12 | §2.2 Gap 표 → **구현 현황**으로 정정 (LOT-5 완료와 문서 일치) |
| 2.3 | 2026-07-12 | §8.3 BOM 정전개 `lotTracked` 표시(1단계) · API/UI/엑셀 |
| 2.4 | 2026-07-12 | §8.3 2단계: 행별 Lot 설정 · `PATCH /items/{id}/lot-tracked` |
| 2.5 | 2026-07-12 | §8.3 3단계: 트리 Lot 일괄 ON · enable-preview / enable |

---

*v2.5 · Git commit은 사용자 요청 시*
