# D4 — 품목 (`ItemInfo` → `II_MT`)

> Step 0 산출물 · **확정 v0.3** (기종 필수 · Lot 추적 플래그 · sample 정합)  
> SSOT (레거시 감사): `KIT_ERP/BasisInformation/MasterInfoRecordRUD.cs` `m_FieldName[1]` (59필드)  
> 화면: `ItemInfo.aspx` / `ItemInfo.aspx.cs`  
> SmartManager: `item` + `inventory_location`(시드) + `inventory_balance` Lazy  
> 내용키: `item_no` ← `ItemNum` (UNIQUE)  
> PK: `id` ← `ItemInfoIndex`

**관련:** [`step0-plan-D1-D4.md`](./step0-plan-D1-D4.md) · [`domain-event-projector-matrix.md`](./domain-event-projector-matrix.md) §3 · [시스템 컬럼 규칙](../../.cursor/rules/master-audit-fields.mdc) · TO-BE [`results/sample/basis-item-spec.md`](../../results/sample/basis-item-spec.md) · [`inventory-ledger-spec.md`](../../results/sample/inventory-ledger-spec.md) · Lot: [`lot-integration-design.md`](./lot-integration-design.md)

### v0.3 변경 요약 (2026-07-12)

| 항목 | 내용 |
|------|------|
| 기종 | `modelType` / `model_type` **필수** (도면 `model_type`과 동일 개념) |
| Lot 추적 | `lotTracked` / `lot_tracked` 추가 · 기본 `false`/`0` · 수정 가능 |
| 일괄등록 | 품목 템플릿에 기종 필수 · `Lot추적` 컬럼(Y/N, 기본 N) |
| Flyway | `V077__item_lot_tracked_and_model_type.sql` |

### v0.2 변경 요약 (확정)

| 항목 | v0.1 | v0.2 |
|------|------|------|
| 업무 필드 | 59필드 매트릭스 | **11필드 슬림** (+기종은 V062, v0.3에서 필수화) |
| 자산분류 | 레거시 6종 | **4종** (원자재·제품·상품·공정품) |
| 등록 부수효과 | `InventorySlotProjector` | **없음** — 첫 TX·공정 시 `ensureBalance()` |
| 영업창고 | SALES 1 (유지) | **SALES 1** (`inventory_location` 시드) |
| 도면번호·분류1~4 등 | Keep | **Drop** (Cut-over 미이전 또는 이력 참고만) |

---

## 1. 분류 범례

| 분류 | 의미 |
|------|------|
| **Keep** | UI·API·DB 유지 (사용자 입력) |
| **시스템(자동)** | 프로그램만 설정 — 사용자 입력 금지 |
| **Drop** | SmartManager 미구현·미이전 |
| **감사** | 레거시 59필드 대조용 (§4) |

**등록 부수효과:** `ItemRegistered` → **없음**. 재고는 [domain-event §3](./domain-event-projector-matrix.md) Lazy 정책.

| 자산분류 (v0.2) | 첫 `inventory_balance` ensure 시점 |
|-----------------|-------------------------------------|
| 원자재 | 첫 구매입고 → `RAW` |
| 제품 | 첫 영업 입출고 → `SALES` 1건 + `DELIVERY` |
| 상품 | 동일 (`SALES` + `DELIVERY`) |
| 공정품 | `ProcessRegistered` 또는 첫 생산 TX → `WIP` (공정별) |

---

## 2. SmartManager 스키마 — `item` (업무 필드)

| # | UI 라벨 | React field | DB 컬럼 | 타입 | 필수 | 비고 |
|---|---------|-------------|---------|------|------|------|
| 1 | 품목번호 | itemNo | item_no | string | Y | 내용키 · 수정 불가 |
| 2 | 품목명 | itemName | item_name | string | Y | |
| 3 | 자산분류 | propertyClassification | property_classification | enum | Y | §2.1 |
| 4 | 기종 | modelType | model_type | string | Y | 도면 `drawing_master.model_type`과 동일 개념 (V062·V077) |
| 5 | 단위 | unit | unit | string/code | Y | PUC `0400` 또는 자유입력 |
| 6 | 규격 | standard | standard | string | N | |
| 7 | 기준단가 | standardUnitCost | standard_unit_cost | decimal | N | 단가 마스터와 별도 |
| 8 | 검사구분 | checkDistinction | check_distinction | enum | N | 구매입고 POST 분기 |
| 9 | 리드타임 | leadTime | lead_time | int | N | 일 단위 · MRP |
| 10 | 안전재고량 | safetyStockQuantity | safety_stock_quantity | decimal | N | |
| 11 | 발주간격수량 | orderIntervalQuantity | order_interval_quantity | decimal | N | |
| 12 | 최소발주량 | minOrderQuantity | min_order_quantity | decimal | N | |
| 13 | Lot 추적 | lotTracked | lot_tracked | boolean | Y(기본0) | V077 · `1`이면 입출고 시 lot 필수 |
| — | (PK) | id | id | bigint | 시스템 | |
| — | 레코드상태 | recordingState | recording_state | boolean | 시스템 | 소프트 삭제 |
| — | 감사 4+4 | createdAt… | created_* / updated_* | | 시스템 | [공통 규칙](../../.cursor/rules/master-audit-fields.mdc) |

### 2.1 자산분류 (4종 — v0.2 확정)

| 값 | 기본 location | 비고 |
|----|---------------|------|
| `원자재` | `RAW` | |
| `제품` | `SALES` + `DELIVERY` | WIP는 공정 등록 시 |
| `상품` | `SALES` + `DELIVERY` | WIP 일반 없음 |
| `공정품` | `WIP` | 공정별 `process_sequence` + `public_code_id` |

> 레거시 6종(반제품·부자재·소모품)은 Cut-over 시 4종으로 **매핑 테이블** 적재. 신규 등록은 4종만 허용.

### 2.2 창고 연계 (품목 테이블에 warehouse FK 없음)

```
inventory_location (시드: RAW, SALES, DELIVERY, WIP, OUTSOURCE)
  → inventory_balance (item_id × location × fiscal_year × [WIP키] × [partner_id])
  → inventory_balance_monthly
  → stock_movement
```

**금지:** 품목 등록 API에서 `inventory_balance` 직접 INSERT.

---

## 3. API · 이벤트 (v0.2)

| 항목 | 규칙 |
|------|------|
| `GET /api/v1/basis/items?itemNo=&itemName=` | 목록 (`recording_state=1`) |
| `GET /api/v1/basis/items/{id}` | 상세 |
| `GET /api/v1/basis/items/by-no/{itemNo}` | 내용키 조회 |
| `POST /api/v1/basis/items` | 업무 필드 · `modelType` 필수 · `lotTracked` 기본 false · 시스템 컬럼 거부/무시 |
| `PUT /api/v1/basis/items/{id}` | `item_no` 변경 불가 |
| `DELETE` | `recording_state=0` |
| 이벤트 | `ItemRegistered` / `Updated` / `Deleted` — **재고 Projector 없음** |

**요청 예**

```json
{
  "itemNo": "P-1001",
  "itemName": "샘플 제품",
  "propertyClassification": "제품",
  "unit": "EA",
  "standard": "100x50",
  "standardUnitCost": 5000,
  "checkDistinction": "NONE",
  "leadTime": 7,
  "safetyStockQuantity": 10,
  "orderIntervalQuantity": 0,
  "minOrderQuantity": 1
}
```

---

## 4. 레거시 59필드 — Drop 요약 (감사·Cut-over)

| 구분 | 레거시 대표 컬럼 | v0.2 |
|------|-----------------|------|
| Keep→11필드 | ItemNum, ItemName, PropertyClassification, Unit, Standard, StandardUnitCost, CheckDistinction, SafetyStockQuantity, OrderIntervalQuantity, MinOrderQuantity, CompleteLeadTime→`lead_time` | 매핑 |
| Drop | ItemDrawNum, Texture, SupplementaryValueTaxRate, StockUnit~SaleUnit, IOChackable, StockManagable, OrderPlan, ItemType, MateralQuality, ItemState, Maker, ItemClassification1~4, Standard1~4/Unit4, 열처리 6필드, TariffRate, Main*Company 3종, ChargePerson | **미이전** |
| 시스템 | RecodingState, 감사, ItemInfoIndex | 시스템(자동) |

전체 59행 매트릭스는 v0.1 Git/이력 참고. Step 1 스키마는 §2만 구현.

---

## 5. Cut-over · 마이그레이션

| 항목 | 처리 |
|------|------|
| `II_MT` | §2 11필드 + 시스템 컬럼만 |
| 자산분류 6→4 | 매핑 스크립트 (예: 반제품→공정품) |
| `RMS_MT`/`BS_MT`/`DS_MT`/`PS_MT` | `inventory_balance` + [domain-event §3.2](./domain-event-projector-matrix.md) (영업 1·2·3 합산) |
| 도면번호 UK | Drop — `item_no`만 UK |

---

## 6. 체크리스트

- [x] 11필드 슬림 확정
- [x] 자산분류 4종
- [x] 등록 시 창고 미생성 (Lazy)
- [x] domain-event §3 정합
- [ ] Step 1 Flyway + 품목 CRUD
- [ ] 레거시 6종→4종 이관 스크립트

---

*v0.1→v0.2: 2026-07-02 · TO-BE: `results/sample/basis-item-spec` 정합*