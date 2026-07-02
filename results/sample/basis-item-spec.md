# 품목 기준정보 확정 스펙

> **문서 버전:** 1.0  
> **작성일:** 2026-06-22  
> **상태:** 확정안 (구현 대기)  
> **관련 Wave:** B1(현행) → **B1-R** (품목·창고 시드 리팩터)  
> **관련 문서:**  
> - [거래처 기준정보 확정 스펙](./basis-company-spec.md)  
> - [기준정보 API·필드 매핑](./basis-information-api-spec.md) §5.2  
> - [기준정보 구현 설계](./basis-information-implementation-spec.md)  
> - [재고·원장 설계](./inventory-ledger-spec.md) §3  
> - [품목구성(BOM) 확정 스펙](./basis-item-composition-spec.md)

---

## 1. 문서 목적

기준정보 정비 2단계로 **품목(`item`)** 의 업무 정의, 데이터 모델, 창고 연계, 화면·API, 현행 갭·후속 Wave를 정리한다.

---

## 2. 기준정보 공통 UI 원칙

[거래처 스펙](./basis-company-spec.md)과 동일하게 **모든 기준정보 화면**에서 아래를 따른다.

| 항목 | 확정 |
|------|------|
| 레거시 하단 부가 그리드 | **구현하지 않음** — 품목정보테이블, 자료실테이블, 제목 등 |
| 목록 화면 | 상단 검색 + 그리드 + 우측 상단 **등록(Modal)** + 행별 **수정·삭제** |
| 시스템 필드 | `id`, 등록/수정, `recoding_state` — 화면 입력 없음 |
| 삭제 | `recoding_state = 0` 소프트 삭제 |

---

## 3. 업무 정의

품목은 생산·구매·판매·재고·BOM·공정의 **공통 식별 마스터**이다.

| 원칙 | 내용 |
|------|------|
| 내용키(UK) | `item_num` (품목번호) |
| PK | `id` (BIGINT) |
| 창고 | 품목별 물리 창고 테이블 **생성 안 함** — `inventory_location` 시드 + `inventory_balance` Lazy |
| 레거시 | 품목 등록 시 RMS/BS/DS 등 **창고 행 일괄 생성 패턴 폐기** |

---

## 4. 데이터 모델 — `item`

### 4.1 업무 필드 (Modal 입력 · 테이블에 이것만 유지)

| # | 화면 라벨 | 컬럼 | API 필드 | 비고 |
|---|-----------|------|----------|------|
| 1 | 품목번호 | `item_num` | `itemNum` | UK, 수정 불가 |
| 2 | 품목명 | `item_name` | `itemName` | |
| 3 | 자산분류 | `property_classification` | `propertyClassification` | 콤보 4종 (§4.2) |
| 4 | 단위 | `unit` | `unit` | |
| 5 | 규격 | `standard` | `standard` | |
| 6 | 기준단가 | `standard_unit_cost` | `standardUnitCost` | |
| 7 | 검사구분 | `check_distinction` | `checkDistinction` | 구매입고 검사·POST 분기 |
| 8 | 리드타임 | `lead_time` | `leadTime` | MRP·발주 (일) |
| 9 | 안전재고량 | `safety_stock_quantity` | `safetyStockQuantity` | |
| 10 | 발주간격수량 | `order_interval_quantity` | `orderIntervalQuantity` | |
| 11 | 최소발주량 | `min_order_quantity` | `minOrderQuantity` | |

### 4.2 자산분류 (4종)

| 값 | 재고 location (기본) |
|----|----------------------|
| `원자재` | `RAW` (원자재창고) |
| `제품` | `SALES` (영업창고) — 완제. 공정 등록 시 `WIP` 슬롯 추가 |
| `상품` | `SALES` (영업창고) |
| `공정품` | `WIP` (생산창고) — 공정별 슬롯 |

> 현행 코드·테스트의 `RAW`, `FG` 등 영문 코드는 **B1-R에서 위 4한글 값(또는 확정 코드表)으로 통일**한다.

### 4.3 시스템 필드 (화면 입력 없음)

| 컬럼 | 설명 |
|------|------|
| `id` | PK |
| `created_by` / `created_at` | 등록자·등록일 |
| `updated_by` / `updated_at` | 수정자·수정일 |
| `recoding_state` | `1`=유효, `0`=삭제 |

### 4.4 TO-BE에서 제거할 컬럼 (현행 `item`에 잔존)

- `item_draw_num`, `supplementary_value_tax_rate`
- `stock_unit`, `bom_unit`, `purchase_unit`, `sale_unit`
- `io_checkable`, `stock_managable`, `order_plan`
- `item_type`, `material_quality`, `item_state`
- `item_classification1` ~ `item_classification4`
- `main_purchase_company_id`, `main_outsource_company_id`, `main_sales_company_id`
- `charge_person`

실거래 단가는 **`unit_cost`** 마스터(판매/구매/외주)에서 관리. 품목의 `standard_unit_cost`는 기준·발주 초기값.

---

## 5. 창고 (`inventory_location`) 연계

### 5.1 영업창고 1개

| 확정 | 내용 |
|------|------|
| 영업창고 | **`SALES` 1개만** — 레거시 `SALES_1`~`3` 중 2·3 **제거** |
| 시드 | `RAW`, `SALES`, `DELIVERY`, `WIP`, `OUTSOURCE` |

### 5.2 품목 등록 시 창고 처리

품목 테이블에 `warehouse_id` FK를 두지 않는다.

```
inventory_location (고정 시드)
    → inventory_balance (품목 × location × 회계연도 × [WIP 공정키] × [외주 partner_id])
    → stock_movement (입출고 이력)
```

| 정책 | 설명 |
|------|------|
| **기본 (권장)** | 품목 등록 시 잔고 행 **생성 안 함** — 첫 입출고·공정 정의 시 `ensureBalance()` |
| **선택** | `ItemRegisteredEvent` → 자산분류별 **기본 location 빈 슬롯 1건** (`stock_qty=0`) |

| 자산분류 | 첫 트랜잭션 시 location | 추가 슬롯 |
|----------|-------------------------|-----------|
| 원자재 | `RAW` | — |
| 제품 | `SALES` / `DELIVERY` / `WIP` | `ProcessDefinedEvent` → WIP |
| 상품 | `SALES` / `DELIVERY` | WIP 없음(일반) |
| 공정품 | `WIP` | 공정별 (`process_sequence`, `process_code`) |

`resolveInventoryLocation()` 등 하드코딩 `SALES_1` → **`SALES`** 로 통일 (B1-R).

---

## 6. 화면 (`/basis/items`)

### 6.1 목록

| 요소 | 동작 |
|------|------|
| 상단 검색 | **품목번호**, **품목명** 각각 입력 — 둘 다 있으면 **AND** 필터 |
| 그리드 컬럼 | 품목번호, 품목명, 자산분류, 단위, 규격, 기준단가, 검사구분, (선택) 등록일 |
| 우측 상단 | **등록** → Modal |
| 행 우측 | **수정** / **삭제** |
| 하단 부가 영역 | **없음** |

### 6.2 등록·수정 Modal

§4.1 업무 필드 **11개**만 노출.

- **품목번호**: 수정 시 변경 불가
- **자산분류**: Select 4종
- **리드타임·안전재고·발주간격·최소발주**: 숫자 입력 (기본 0 허용 여부는 구현 시 통일)

---

## 7. API (TO-BE)

| Method | Endpoint | 설명 |
|--------|----------|------|
| GET | `/api/v1/basis/items?itemNum=&itemName=` | 목록·검색 (`recoding_state=1`) |
| GET | `/api/v1/basis/items/{itemNum}` | 상세 |
| POST | `/api/v1/basis/items` | 등록 |
| PUT | `/api/v1/basis/items/{itemNum}` | 수정 |
| DELETE | `/api/v1/basis/items/{itemNum}` | 소프트 삭제 |

**POST body 예시**

```json
{
  "itemNum": "P-1001",
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

**검증**

- §4.1 필드 NOT NULL (숫자 0 허용)
- `item_num` UK
- BOM·트랜잭션 참조 중 삭제 시 비활성만 허용 검토

---

## 8. 현행 구현과의 차이 (갭)

| 항목 | 현행 | TO-BE |
|------|------|-------|
| 업무 컬럼 | 20+ | **11개** |
| MRP 수량·리드타임 | DB 없음 | 컬럼 **추가** |
| 자산분류 | 한글 6종 UI / 테스트 `RAW`·`FG` | **4종** 통일 |
| 영업창고 | `SALES_1`~`3` 시드 | **`SALES` 1개** |
| 프론트 | 10필드 전후, 단일 `?q=` 검색 | Modal 11필드, **품목번호·품목명** 검색 |
| 품목 등록 시 창고 | Lazy (설계) | 동일 + location 코드 정리 |

---

## 9. 구현·후속 Wave (할 일)

### 9.1 B1-R — 품목·창고 (거래처 B1-R 이후 또는 병행)

- [ ] Flyway: `item` 컬럼 정리 — §4.4 제거, §4.1 추가 컬럼(`lead_time` 등) 반영
- [ ] Flyway: `inventory_location` — `SALES_2`/`SALES_3` 제거, `SALES_1` → `SALES` rename 또는 코드 통일
- [ ] 기존 데이터 `property_classification` 값 이관 (`RAW`→`원자재`, `FG`→`제품` 등)
- [ ] `ItemService` / `ItemRequest`·`ItemResponse` — 11필드만
- [ ] `PurchaseReceiptService.resolveInventoryLocation` — `SALES` + 4분류 매핑
- [ ] `InventoryBalanceService.ensureWipForProcess` — `원자재` 제외 정책
- [ ] `MrpService` — 안전재고·리드타임·발주간격·최소발주량 반영 (고도화)
- [ ] `GET /items?itemNum=&itemName=` 검색
- [ ] 프론트 `ItemsPage` — UI 확정안 반영
- [ ] 통합테스트 전역 `propertyClassification` 값 갱신

### 9.2 기타 후속

- [ ] (선택) `ItemRegisteredEvent` → 기본 location 빈 슬롯 ensure
- [ ] `io_checkable` 제거에 따른 MRP/BOM 전개 규칙 — **자산분류·BOM** 기준으로 대체 검토
- [ ] `item_state` 제거 — 소프트 삭제만으로 사용/중지 통제
- [ ] 본 문서를 [basis-information-api-spec.md](./basis-information-api-spec.md) §5.2와 동기화

---

## 10. 의사결정 요약

| 항목 | 확정 |
|------|------|
| 업무 필드 | 11개 (§4.1) |
| 자산분류 | 원자재 · 제품 · 상품 · 공정품 |
| 영업창고 | **1개** (`SALES`) |
| 창고 모델 | `inventory_balance` Lazy, 품목에 warehouse FK 없음 |
| UI | 거래처와 동일 패턴, 검색=품목번호+품목명, 하단 부가 영역 없음 |
| 레거시 품목 창고 테이블 | 생성 안 함 |

---

## 11. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-06-22 | 품목 확정안 — 11필드, 4분류, 영업창고 1개, 창고 Lazy, UI |
