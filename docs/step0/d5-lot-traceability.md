# D5 — Lot 추적 (`LotLedger` → `inventory_lot`)

> Step 0 산출물 · **초안 v0.1** (문서·스키마만 — **구현은 Part 06·외주단가 Projector 정합 이후**)  
> 레거시: `LotNumberManagement.aspx` · `DSLoad.xsd` `LotLedger`  
> SmartManager: `inventory_lot` + `inventory_lot_balance` + `stock_movement.lot_id` + `lot_genealogy`  
> **관련:** [`inventory-ledger-spec.md`](../../results/sample/inventory-ledger-spec.md) · [`domain-event-projector-matrix.md`](./domain-event-projector-matrix.md) · [`d4-item.md`](./d4-item.md) · 스키마 초안 [`schema-drafts/V007__lot_traceability.sql`](./schema-drafts/V007__lot_traceability.sql)

---

## 0. 문서 상태 · 구현 순서

| 단계 | 내용 | 상태 |
|------|------|------|
| **A** | 본 문서 + `schema-drafts/` SQL 초안 | **v0.1 초안** |
| **B** | Part 06 — `OutsourceInputBalanceProjector` 400·BOM·문서 정합 | **다음 작업 (Lot 이전)** |
| **C** | `stock_movement` · `inventory_balance_monthly` Flyway (sample INF-1) | Lot 선행 |
| **D** | `V007` Flyway 적용 · Lot API·TX 연동 (TX1 구매입고부터) | B·C 이후 |

**원칙:** Projector가 만드는 것은 **재고 슬롯**(`inventory_balance`)이며, Lot 번호·계보는 **트랜잭션(TX) + `stock_movement`** 에서만 생성·이동한다.  
외주단가 등록(`OutsourceInputBalanceProjector`)과 Lot는 **독립**이다.

---

## 1. 레거시 감사 — `LotLedger`

| 레거시 컬럼 | 타입 | SmartManager v0.1 |
|-------------|------|-------------------|
| `ID` | int PK | `inventory_lot.id` |
| `LotNumber` | string | `inventory_lot.lot_no` (품목 내 UK) |
| `P1`, `P2` | string | `lot_no` 자동생성 규칙 파라미터 — v0.1 **선택 필드** (`p1`, `p2`) |
| `RawMaterial` | bool | **Drop** — `item.property_classification` + `lot_tracked` 로 대체 |
| `Active` | bool | `inventory_lot.status` (`ACTIVE` \| `BLOCKED` \| `DEPLETED`) |
| `Remark` | bool (스키마 오류 추정) | `inventory_lot.remark` TEXT |
| 감사 필드 | — | [master-audit-fields](../../.cursor/rules/master-audit-fields.mdc) |

레거시 `LotLedger`는 **마스터 등록 화면** 성격이며, 입출고 이력(`SH_HT` 등)과 **FK로 직접 연결되지 않음**.  
SmartManager는 **TX 시점 Lot 생성** + **이동 이력**으로 역추적·정추적을 통합한다.

---

## 2. 설계 목표

1. **선택적 Lot 추적** — 품목별 `item.lot_tracked = true` 일 때만 Lot 필수
2. **슬롯 집계와 Lot 잔량 분리** — `inventory_balance`(월별 집계) ≠ Lot 단위 잔량
3. **양방향 계보** — 원자재 Lot → 생산 투입 → 완제품 Lot (`lot_genealogy`)
4. **감사 가능 이력** — 모든 Lot 이동은 `stock_movement` 1행 이상 (삭제 금지, `recording_state`만)

---

## 3. 계층 모델 (3층)

```text
┌─────────────────────────────────────────────────────────────┐
│  TX (구매입고·작업실적·외주입출고·영업출고)                    │
│    → Domain Event → InventoryMovementListener               │
└──────────────────────────┬──────────────────────────────────┘
                           │
         ┌─────────────────┼─────────────────┐
         ▼                 ▼                 ▼
  stock_movement    inventory_lot_balance   lot_genealogy
  (건별 이력)        (Lot×슬롯 현재고)       (부모↔자식)
         │                 │
         └────────┬────────┘
                  ▼
         inventory_balance + inventory_balance_monthly
         (슬롯·월별 집계 — Lot 무관 합계)
```

| 계층 | 테이블 | Lot 관련 |
|------|--------|----------|
| 이력 | `stock_movement` | `lot_id` NULLable — Lot 품목은 **NOT NULL** |
| Lot 현재고 | `inventory_lot_balance` | `(lot_id, inventory_balance_id)` UK |
| Lot 마스터 | `inventory_lot` | 번호·상태·출처 TX |
| 계보 | `lot_genealogy` | CONSUME / PRODUCE / SPLIT / MERGE |
| 슬롯 집계 | `inventory_balance` | 기존 UK 유지 — Lot 잔량 합 = 슬롯 검증(앱) |

**정합 규칙 (애플리케이션):**

```text
SUM(inventory_lot_balance.qty_on_hand WHERE balance_id = B)
  ≈ inventory_balance_monthly.stock_qty (해당 월·슬롯)
```

Lot 미추적 품목은 `lot_id = NULL` 이동만 기록하고 `inventory_lot_balance` 행 없음.

---

## 4. 품목 플래그 — `item.lot_tracked`

| 필드 | 타입 | 기본 | 설명 |
|------|------|------|------|
| `lot_tracked` | TINYINT(1) | 0 | 1이면 해당 품목 입출고 시 `lot_id` **필수** |

- v0.1: 품목 등록·수정 API에서 설정 (UI: 품목 화면 체크박스 — **Lot 구현 Wave에서 추가**)
- 자산분류별 기본값 정책은 **미확정** (원자재만 기본 true 등 — Cut-over 시 ETL 규칙)

---

## 5. Lot 번호 정책 (v0.1 제안)

| 방식 | 설명 | 우선 Wave |
|------|------|-----------|
| **수동** | 구매입고·작업실적 화면에서 `lot_no` 입력 | TX1 |
| **자동** | `{item_no}-{yyyyMMdd}-{seq}` — `lot_number_sequence` 테이블 | TX2+ |

레거시 `P1`/`P2`는 자동 생성 규칙 placeholder로 보존 가능. v0.1은 **수동 + 중복 검증**만 구현.

**UK:** `UNIQUE (item_id, lot_no, recording_state)` — 동일 품목 내 Lot 번호 유일.

---

## 6. Domain Event · Listener (초안)

Projector(기준정보)와 구분 — **TX Listener**만 Lot를 다룬다.

| Trigger Event | Listener | Lot 동작 |
|---------------|----------|----------|
| `PurchaseReceiptPosted` | `InventoryOnPurchaseReceiptListener` | RAW 입고 — Lot **생성** + `stock_movement` IN |
| `WorkReportPosted` | `InventoryOnWorkReportListener` | 투입 Lot **CONSUME** · 산출 Lot **PRODUCE** + genealogy |
| `OutsourceShipmentPosted` | `InventoryOnOutsourceListener` | OUTSOURCE 슬롯 Lot 이동 |
| `SalesShipmentPosted` | `InventoryOnSalesListener` | SALES/DELIVERY Lot 출고 · `DEPLETED` |

**Lot 전용 이벤트 (선택):** `LotCreated` / `LotMerged` — v0.1에서는 `stock_movement` INSERT로 충분, 별도 Outbox 이벤트는 Wave 2에서 검토.

---

## 7. API 스케치 (구현 Wave — 참고만)

Base: `/api/v1/inventory/lots`

| Method | Endpoint | 설명 |
|--------|----------|------|
| `GET` | `/` | 품목·슬롯·상태 필터 목록 |
| `GET` | `/{id}` | Lot 상세 + 현재 슬롯별 잔량 |
| `GET` | `/{id}/genealogy?direction=UP\|DOWN` | 계보 트리 |
| `GET` | `/{id}/movements` | `stock_movement` 페이지 |
| `POST` | `/` | 수동 Lot 등록 (레거시 `LotNumberManagement` 대체 — **관리자**) |

TX API(구매입고 등)는 요청 DTO에 `lotNo` 또는 `autoGenerateLot: true` 포함.

---

## 8. Flyway · schema-drafts

| 파일 | 위치 | 비고 |
|------|------|------|
| `V007__lot_traceability.sql` | `docs/step0/schema-drafts/` | **미적용** — 검토 후 `db/migration/` 복사 |
| 선행 | `stock_movement` 본 테이블 | sample §3 — Lot 없이도 INF Wave에서 먼저 가능 |

`V007`은 `stock_movement` 정의를 **Lot 컬럼 포함 한 번에** 스케치한다.  
실제 적용 시 `stock_movement`만 먼저 분리(`V007`) → Lot(`V008`)로 쪼개도 됨.

---

## 9. 구현 Wave (Lot)

| Wave | 내용 | 선행 |
|------|------|------|
| **LOT-0** | 본 문서·schema-drafts 확정 | — |
| **LOT-1** | Flyway · `LotService` · 수동 Lot CRUD | Part 06, `stock_movement` |
| **LOT-2** | TX1 구매입고 Lot 생성·RAW 잔량 | LOT-1 |
| **LOT-3** | 작업실적 투입/산출 + genealogy | BOM·공정 TX |
| **LOT-4** | 외주·영업 출고 Lot 이동 | OUTSOURCE TX |
| **LOT-5** | ETL `LotLedger` → `inventory_lot` | Cut-over |

---

## 10. 미결정 · v0.2 검토

| # | 항목 | v0.1 가정 |
|---|------|-----------|
| 1 | Lot 번호 전역 UK vs 품목 내 UK | **품목 내 UK** |
| 2 | 유효기간·시험성적 | 컬럼만 (`expiry_date`, `certificate_ref`) — UI 후순위 |
| 3 | 음수 Lot 잔량 | **금지** — TX 검증 |
| 4 | Lot 병합/분할 UI | genealogy + API만, 화면 Wave 2 |
| 5 | `inventory_balance`에 Lot UK 추가 여부 | **하지 않음** — 별도 `inventory_lot_balance` |

---

## 11. Step 0 체크리스트

- [x] 레거시 `LotLedger` 필드 매핑
- [x] 슬롯 vs Lot 계층 정의
- [x] schema-drafts SQL 초안
- [x] TX Event · Listener 초안
- [ ] Flyway `db/migration` 반영 (Part 06 이후)
- [ ] `domain-event-projector-matrix.md` §5 Lot 절 추가 (구현 시)
- [ ] `inventory-ledger-spec.md` §3 `lot_id` 반영 (구현 시)

---

*v0.1 초안 · Git commit은 사용자 요청 시*
