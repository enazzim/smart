# D4 — 단가 3종 (`SaleUnitCodeInfo` / `BuyingUnitCodeInfo` / `OutSideOrderUnitCodeInfo` → `UCI_MT`)

> Step 0 산출물 · **확정 v0.1** (3탭 1화면 · Modal 8~10필드 · `unit_price` 통합)  
> SSOT (레거시 감사): `MasterInfoRecordRUD.cs` `m_FieldName[7·8·9]` (각 19필드, 구조 동일)  
> 화면: `SaleUnitCodeInfo.aspx` · `BuyingUnitCodeInfo.aspx` · `OutSideOrderUnitCodeInfo.aspx`  
> SmartManager: **`unit_price`** (`cost_type` 구분) + **`unit_price_change_log`** (구 `UPUCI_HT`)  
> 내용키(UK): `(cost_type, item_id, company_id, begin_date, begin_process_code_id, end_process_code_id)` 활성 1건  
> PK: `id` ← `UnitCostInfoIndex`

**관련:** [`step0-plan-D1-D4.md`](./step0-plan-D1-D4.md) · [`domain-event-projector-matrix.md`](./domain-event-projector-matrix.md) §2·§4.5·§5 · [`d4-item.md`](./d4-item.md) · [`d4-company.md`](./d4-company.md) · [`d4-process.md`](./d4-process.md) · [`공용코드 문서`](./공용코드-콤보박스-매핑-초안-code-group-field-binding.md) §4.8 · [시스템 컬럼 규칙](../../.cursor/rules/master-audit-fields.mdc) · TO-BE [`results/sample/basis-unit-cost-spec.md`](../../results/sample/basis-unit-cost-spec.md)

> sample 문서명은 `unit_cost`이나 Step 0 테이블명은 domain-event 정합 **`unit_price`** 를 사용한다.

### v0.1 확정 요약

| 항목 | 레거시 | SmartManager v0.1 |
|------|--------|-------------------|
| UI | 화면 3종·2분할 | **1 Route `/basis/unit-prices` + 3탭** (`SALE` / `PURCHASE` / `OUTSOURCE`) |
| 테이블 | `UCI_MT` × 구분값 | **`unit_price`** 단일 + `cost_type` |
| 품목·거래처 | `ItemNum`·`BusinessRegistrationNum` | **`item_id`·`company_id`** FK |
| 외주 공정 | `BeginProcessCode`·`EndProcessCode` 8자리 | **`begin_process_code_id`·`end_process_code_id`** → `public_code.id` |
| 발주비율 | `OrderRate` | **`order_rate`** — 판매 **0 고정**, 구매·외주 합계 ≤100% |
| 기간 | `BeginDate`·`EndDate` | **Modal 입력** (할인단가·종료일 포함) |
| 변경사유 | `UpdateReason` | **PUT 시 필수** → `unit_price_change_log` |
| 이력 UI | 우측 타임라인 | **1차 없음** (API만) |
| 외주 등록 부수효과 | 레거시 `OutSideStoreTable` | **`OutsourceInputBalanceProjector.ensure`** (투입 `OUTSOURCE` Lazy) — 레거시 일괄 창고 생성 **아님** |

---

## 1. 분류 범례

| 분류 | 의미 |
|------|------|
| **Keep (UI)** | Modal·API 사용자 입력 |
| **탭고정** | `cost_type` — 현재 탭값, 등록 시 자동 |
| **조건부** | 외주 탭만 공정 FK·발주비율 활성 |
| **시스템(자동)** | PK·감사·소프트삭제 |
| **Drop (UI)** | 2분할 상세·이력 타임라인 |
| **Phase2** | 이력 UI·발주 확정 100% 검증·MRP 0% 제외 |

---

## 2. `cost_type` 3종

| 탭 | `cost_type` | 레거시 화면 | `UnitCostDistinction` |
|----|-------------|------------|------------------------|
| 판매단가 | `SALE` | `SaleUnitCodeInfo.aspx` | 판매단가 |
| 구매단가 | `PURCHASE` | `BuyingUnitCodeInfo.aspx` | 구매단가 |
| 외주단가 | `OUTSOURCE` | `OutSideOrderUnitCodeInfo.aspx` | 외주단가 |

---

## 3. SmartManager 스키마 — `unit_price`

### 3.1 Modal 입력 필드 (탭 공통 + 조건부)

| # | UI 라벨 | React field | DB 컬럼 | API | 필수 | 비고 |
|---|---------|-------------|---------|-----|------|------|
| — | 단가구분 | costType | cost_type | `type` | Y | 탭 고정 |
| 1 | 품목 | itemId | item_id | `itemNum` → resolve | Y | §3.2 자산분류 |
| 2 | 거래처 | companyId | company_id | `companyId` | Y | `CompanySelect` — §3.3 |
| 3 | 시작공정 | beginProcessCodeId | begin_process_code_id | `beginProcessCodeId` | 외주 | FK → `public_code.id` |
| 4 | 종료공정 | endProcessCodeId | end_process_code_id | `endProcessCodeId` | 외주 | FK → `public_code.id` |
| 5 | 발주비율(%) | orderRate | order_rate | `orderRate` | 조건부 | 판매 **0 고정** |
| 6 | 기준단가 | standardUnitCost | standard_unit_cost | `standardUnitCost` | Y | DECIMAL(18,4) |
| 7 | 할인단가 | discountUnitCost | discount_unit_cost | `discountUnitCost` | N | |
| 8 | 적용시작일 | beginDate | begin_date | `beginDate` | Y | DATE |
| 9 | 적용종료일 | endDate | end_date | `endDate` | N | null=무기한 |
| 10 | 변경사유 | updateReason | (이력) | `updateReason` | PUT만 | `unit_price_change_log` |

**외주 공정 콤보 (`code_group` + `public_code` PK):**

- React: `<CodeSelect codeGroup="PROCESS_CODE" excludeReserved valueField="id" />`
- API: `GET /api/code-groups/PROCESS_CODE/options`
- 저장: `begin_process_code_id` / `end_process_code_id` ← option `id`
- 판매·구매: 공정 FK **NULL**, body에 공정 필드 **포함 시 400**

### 3.2 품목 자산분류 제한

| `cost_type` | 허용 `property_classification` | 추가 |
|-------------|----------------------------------|------|
| `PURCHASE` | `원자재`, `상품` | |
| `SALE` | `제품`, `상품` | |
| `OUTSOURCE` | `제품`, `공정품` | plan `process_sequence`에 `INHOUSE`·`SPLIT` **1건 이상** |

### 3.3 거래처 역할 (`d4-company` `company_role`)

| `cost_type` | `CompanySelect` 필터 |
|-------------|------------------------|
| `SALE` | `partnerType=SALES` |
| `PURCHASE` | `partnerType=PURCHASE` |
| `OUTSOURCE` | `partnerType=OUTSOURCE` |

### 3.4 구분별 NULL 규칙

| 필드 | 구매 | 판매 | 외주 |
|------|:----:|:----:|:----:|
| `begin_process_code_id` | NULL | NULL | **필수** |
| `end_process_code_id` | NULL | NULL | **필수** |
| `order_rate` | **필수** | **0 고정** | **필수** |

### 3.5 발주비율 (`order_rate`)

| 시점 | 규칙 |
|------|------|
| 등록·수정 | 동일 `(cost_type, item_id)` 활성 행 합계 **> 100%** → **400** |
| 외주 | 동일 품목·**동일 공정구간** `(begin_process_code_id, end_process_code_id)` 별 집계 |
| 동시성 | 해당 키 활성 행 **`PESSIMISTIC_WRITE`** 락 후 검증 ([sample §5.4.1](../../results/sample/basis-unit-cost-spec.md)) |
| 발주 확정 | 합계 **= 100%** (Phase2 TX) |
| `order_rate = 0` | 등록 허용, MRP 분할 **제외** (Phase2) |

### 3.6 UK

**`(cost_type, item_id, company_id, begin_date, begin_process_code_id, end_process_code_id)`** — 활성(`recording_state=1`) 구간 유일.

> 레거시 UK는 구분별 상이(판매·구매: 품목+구분+거래처, 외주: +공정코드). TO-BE는 **시작일·FK 공정** 포함으로 확장.

### 3.7 이력 (`unit_price_change_log`)

| 규칙 | 내용 |
|------|------|
| POST | 이력 행 **생성 안 함** |
| PUT | 변경 전 스냅샷 INSERT + **`updateReason` 필수** |
| API | `GET /api/v1/basis/unit-prices/{id}/history` |

### 3.8 시스템 필드

| 컬럼 | 분류 |
|------|------|
| id, recording_state, created_*, updated_* | 시스템(자동) — [공통 규칙](../../.cursor/rules/master-audit-fields.mdc) |

---

## 4. 이벤트 · Projector

| 이벤트 | Projector | Read Model |
|--------|-----------|------------|
| `SaleUnitPriceRegistered` | — | |
| `PurchaseUnitPriceRegistered` | — | |
| `OutsourceUnitPriceRegistered` | **`OutsourceInputBalanceProjector.ensure`** | `inventory_balance` `OUTSOURCE` INPUT |
| `*UnitPriceUpdated` | `UnitPriceHistoryProjector` | `unit_price_change_log` |
| `OutsourceUnitPriceUpdated` | `OutsourceInputBalanceProjector.rebuild` | 투입 잔고 재계산 |
| `*UnitPriceDeleted` | `UnitPriceHistoryProjector` (정책) | |
| `OutsourceUnitPriceDeleted` | `OutsourceInputBalanceProjector.deactivate` | OUTSOURCE 잔고 비활성 |

> 외주: [domain-event §4.5](./domain-event-projector-matrix.md) — 단가 등록 시 **투입** `OUTSOURCE` 잔고만 Lazy ensure. 레거시 `OS_MT` 일괄 생성·sample §5.6 「창고 자동 연동 금지」는 **무분별 후처리 금지** 의미.

---

## 5. API · 화면 (v0.1)

Base: `/api/v1/basis/unit-prices`

| Method | Endpoint | 설명 |
|--------|----------|------|
| GET | `/?type=SALE\|PURCHASE\|OUTSOURCE&q=` | 탭별 목록 |
| GET | `/{id}` | 상세 |
| GET | `/by-item/{itemNum}?type=` | 품목별 보조 |
| POST | `/` | 등록 (`type` 포함) |
| PUT | `/{id}` | 수정 (`updateReason` 필수) |
| DELETE | `/{id}` | 소프트 삭제 |
| GET | `/{id}/history` | 변경 이력 |

**POST — 구매단가**

```json
{
  "type": "PURCHASE",
  "itemNum": "M-100",
  "companyId": 5,
  "orderRate": 60,
  "standardUnitCost": 1250.5,
  "discountUnitCost": 1200,
  "beginDate": "2026-06-01",
  "endDate": null
}
```

**POST — 외주단가**

```json
{
  "type": "OUTSOURCE",
  "itemNum": "P-1000",
  "companyId": 8,
  "beginProcessCodeId": 301,
  "endProcessCodeId": 305,
  "orderRate": 100,
  "standardUnitCost": 3500,
  "beginDate": "2026-06-01"
}
```

**PUT**

```json
{
  "orderRate": 40,
  "standardUnitCost": 1280,
  "beginDate": "2026-06-01",
  "endDate": "2026-12-31",
  "updateReason": "단가 인상 협의 반영"
}
```

(품목·거래처·UK 키·`type` — **읽기 전용**)

### 5.1 UI (`/basis/unit-prices`)

- **3탭** 버튼: 판매 / 구매 / 외주
- 검색 + 그리드 + Modal (거래처형 평면 목록)
- 외주 탭: 그리드에 **시작·종료 공정명** 컬럼
- Modal: 탭별 동적 필드 (§3.4)
- **2분할·이력 타임라인 없음**

---

## 6. 레거시 19필드 대조표 (3화면 공통)

| # | 레거시 컬럼 | v0.1 | 비고 |
|---|------------|------|------|
| 1 | ItemNum | **Keep (UI)** | → `item_id` |
| 2 | UnitCostDistinction | **탭고정** | → `cost_type` |
| 3 | BusinessRegistrationNum | **Keep (UI)** | → `company_id` |
| 4 | BeginProcessCode | 조건부 | → `begin_process_code_id` FK (외주) |
| 5 | EndProcessCode | 조건부 | → `end_process_code_id` FK (외주) |
| 6 | OrderRate | **Keep (UI)** | 판매 0 고정 |
| 7 | StandardUnitCost | **Keep (UI)** | |
| 8 | DiscountUnitCost | **Keep (UI)** | |
| 9 | BeginDate | **Keep (UI)** | UK 일부 |
| 10 | EndDate | **Keep (UI)** | |
| 11 | UpdateReason | **PUT만** | 이력 |
| 12~18 | RecodingState, 감사 | 시스템(자동) | |
| 19 | UnitCostInfoIndex | `id` PK | |

---

## 7. `results/` 문서와의 관계

| 문서 | 채택 |
|------|------|
| `sample/basis-unit-cost-spec.md` v1.0 | **SSOT** — 3탭·UK·발주비율·자산분류·이력·락 (**테이블명 `unit_price`로 Step 0화**) |
| `sample/basis-public-code-spec.md` | 외주 공정 FK 예외 |
| `inventory-ledger-spec.md` | OUTSOURCE INPUT ensure 맥락 |

**Step 0 vs sample 차이**

| 항목 | sample | Step 0 v0.1 |
|------|--------|-------------|
| 테이블명 | `unit_cost` | **`unit_price`** (domain-event) |
| 이력 테이블 | `unit_cost_history` | **`unit_price_change_log`** |
| 품목 요청 | `itemId` | **`itemNum` → resolve** |
| 거래처 | `companyId` | **동일** (`CompanySelect`) |
| 공정 FK·콤보 API | `processCodeId` / `usageType` | **`beginProcessCodeId`** + **`PROCESS_CODE` code_group** |
| 외주 창고 | §5.6 연동 금지 | **`OutsourceInputBalanceProjector`** Lazy만 (§4) |

---

## 8. Cut-over · Phase2

| 항목 | 처리 |
|------|------|
| `UCI_MT` | `unit_price` + `cost_type` |
| `UPUCI_HT` | `unit_price_change_log` |
| 공정코드 VARCHAR | `public_code.id` FK |
| 판매 레거시 `14009999` | FK NULL (구간 개념 폐기) |
| Phase2 | 발주 100% 확정, MRP 0% 제외, 이력 UI, 전표일 단가 resolve |

---

## 9. 체크리스트

- [x] v0.1 3탭·Modal·UK·발주비율 확정
- [x] **`public_code_id` FK** (외주 공정) · `code_group` **`PROCESS_CODE`**
- [x] `OutsourceInputBalanceProjector` domain-event 정합
- [ ] Step 1 Flyway + unit-prices CRUD + history + 락
- [ ] 발주·MRP 연동 (TX)

---

*v0.1: 2026-07-02 · TO-BE: `results/sample/basis-unit-cost-spec` · 레거시: `MasterInfoRecordRUD.cs` m_FieldName[7·8·9]*