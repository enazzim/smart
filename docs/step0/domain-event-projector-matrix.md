# SmartManager ? 기준정보 Domain Event · Projector 매핑표

> Step 0 산출물. 운영 메뉴 순서(1~11) 기준.  
> 계획·D1~D4: [step0-plan-D1-D4.md](./step0-plan-D1-D4.md)  
> 레거시 근거: `KIT_ERP/BasisInformation/MasterInfoRecordRUD.cs` → `RelatedTableRegistration` / `RelationTableUpdate` / `RelatedTableDelete`  
> 설계 원칙: **이벤트 = 사실 기록**, **프로젝션 = UI·SP·집계용 조회 상태**  
> 참조 원칙: **FK = 부모 테이블 PK만** (§10)  
> 재고 원칙: **영업창고 1개** (§3.1) · **Lazy `inventory_balance`** (마스터 등록 시 창고 미생성)  
> 원장 원칙: **`partner_ledger_account` + `partner_ledger_monthly`** (구 `BSI_MT` 폐기 ? §2.1)  
> TO-BE: [`results/sample/inventory-ledger-spec.md`](../../results/sample/inventory-ledger-spec.md) · [`basis-company-spec.md`](../../results/sample/basis-company-spec.md) · 품목 11필드 [`basis-item-spec.md`](../../results/sample/basis-item-spec.md)

---

## 0. 선행 ? 공통코드 (`PublicUseCode`)

| 항목 | 내용 |
|------|------|
| Legacy | `PublicUseCode.aspx` → `PUC_MT` |
| 등록 부수효과 | 없음 (`RelatedTableRegistration` default) |
| Domain Event | `PublicCodeCreated` / `PublicCodeUpdated` / `PublicCodeDeleted` |
| Projector | 없음 (마스터만 유지) |
| Read Model | `public_code` (구 `PUC_MT`) |

---

## 1. 메뉴별 Domain Event 목록

| # | 메뉴 | Legacy Page | 마스터 테이블 | Created | Updated | Deleted |
|---|------|-------------|--------------|---------|---------|---------|
| 1 | 거래처 | `CompanyInfo.aspx` | `CI_MT` | `CompanyRegistered` | `CompanyUpdated` | `CompanyDeleted` |
| 2 | 품목 | `ItemInfo.aspx` | `II_MT` | `ItemRegistered` | `ItemUpdated` | `ItemDeleted` |
| 3 | 품목구성 | `ItemOrganizationInfo.aspx` | `IOI_MT` | `BomLineRegistered` | `BomLineUpdated` | `BomLineDeleted` |
| 4 | 작업장 | `WCInfo.aspx` | `WCI_MT` | `WorkCenterRegistered` | `WorkCenterUpdated` | `WorkCenterDeleted` |
| 5 | 공정 | `ProcessSequenceInfo.aspx` | `PSI_MT` | `ProcessRegistered` | `ProcessUpdated` | `ProcessDeleted` |
| 6 | 설비 | `EquipmentInfo.aspx` | `EI_MT` | `EquipmentRegistered` | `EquipmentUpdated` | `EquipmentDeleted` |
| 7 | 작업표준 | `WorkStandardInfo.aspx` | `WSI_MT` | `WorkStandardRegistered` | `WorkStandardUpdated` | `WorkStandardDeleted` |
| 8a | 판매단가 | `SaleUnitCodeInfo.aspx` | `UCI_MT` | `SaleUnitPriceRegistered` | `SaleUnitPriceUpdated` | `SaleUnitPriceDeleted` |
| 8b | 구매단가 | `BuyingUnitCodeInfo.aspx` | `UCI_MT` | `PurchaseUnitPriceRegistered` | `PurchaseUnitPriceUpdated` | `PurchaseUnitPriceDeleted` |
| 8c | 외주단가 | `OutSideOrderUnitCodeInfo.aspx` | `UCI_MT` | `OutsourceUnitPriceRegistered` | `OutsourceUnitPriceUpdated` | `OutsourceUnitPriceDeleted` |
| 9 | 기본생산달력 | `StandardProductionCalendarInfo.aspx` | `PCI_MT` | `StandardCalendarDayRegistered` | `StandardCalendarDayUpdated` | `StandardCalendarDayDeleted` |
| 10 | WC생산달력 | `WCProductionCalendarInfo.aspx` | `PCI_MT` | `WorkCenterCalendarDayRegistered` | `WorkCenterCalendarDayUpdated` | `WorkCenterCalendarDayDeleted` |
| 11 | 사용자 | `UserInfo.aspx` | `UI_MT` | `UserRegistered` | `UserUpdated` | `UserDeleted` |

### Real 3종 (설계 3종과 동일 UI, 별도 테이블)

| 메뉴 | Legacy Page | 마스터 테이블 | Domain Event (recordType=`REAL`) |
|------|-------------|--------------|----------------------------------|
| 진품목구성 | `RealItemOrganizationInfo.aspx` | `RIOI_MT` | `RealBomLineRegistered` / `Updated` / `Deleted` |
| 진공정 | `RealProcessSequenceInfo.aspx` | `RPSI_MT` | `RealProcessRegistered` / `Updated` / `Deleted` |
| 진작업표준 | `RealWorkStandardInfo.aspx` | `RWSI_MT` | `RealWorkStandardRegistered` / `Updated` / `Deleted` |

> Real 공정·작업표준: 레거시 `RelatedTableRegistration`에 **창고 부수효과 없음**. 진품목구성만 `UPRIOI_HT` 이력 기록.

---

## 2. 이벤트 → Projector → Read Model (등록 시)

| # | Trigger Event | Projector (Handler) | Read Model / Legacy 테이블 | 레거시 동작 | SmartManager 제안 |
|---|---------------|---------------------|---------------------------|------------|-------------------|
| 1 | `CompanyRegistered` / `CompanyUpdated` | `PartnerLedgerProjector.ensureAccounts` | `partner_ledger_account` (구 `BSI_MT` **헤더**) | 역할 조건 시 `BSI_MT` flags+Year만 INSERT | **TO-BE**: `SALES`→`SALES` 계정, `PURCHASE`/`OUTSOURCE`→`PURCHASE` 계정(공유). **금액 0**. `COST`만 단독 시 미생성 ([d4-company §2.3](./d4-company.md)) |
| 2 | `ItemRegistered` | — | — | 자산분류별 RMS/BS/DS/PS **즉시 생성** | **TO-BE**: 마스터 등록 시 **창고·잔고 미생성**. 첫 입출고·공정 정의 시 `InventoryBalanceService.ensureBalance()` Lazy (§3) |
| 3 | `BomLineRegistered` | `BomHistoryProjector` | `bom_change_log` (구 `UPIOI_HT`) | `IOI_MT` INSERT 후 이력 1건 | **동일**. 창고 슬롯 **미생성** |
| 4 | `WorkCenterRegistered` | — | — | `ProductionCalendarTable` — 기준작업장 `PCI_MT` 복사 | **TO-BE (B5-R): 부수효과 없음** — [d4-work-center §3](./d4-work-center.md), [생산달력 §5.1](../../results/sample/basis-production-calendar-spec.md) |
| 5 | `ProcessRegistered` | `WipBalanceProjector.ensure` | `inventory_balance` location=`WIP` (구 `PS_MT`) | WorkDistinction 무관 항상 `PS_MT` | **Lazy**: 자가·외주 **완료 공정** WIP만 ensure (§4.5). 자가-only 불필요 행 **미생성** |
| 6 | `EquipmentRegistered` | — | — | 부수효과 없음 | 동일 |
| 7 | `WorkStandardRegistered` | — | — | 부수효과 없음 | 동일 |
| 8a | `SaleUnitPriceRegistered` | `UnitPriceHistoryProjector` | `unit_price_change_log` (구 `UPUCI_HT`) | 단가 이력 INSERT | 동일 |
| 8b | `PurchaseUnitPriceRegistered` | `UnitPriceHistoryProjector` | `unit_price_change_log` | 단가 이력 INSERT | 동일 |
| 8c | `OutsourceUnitPriceRegistered` | `OutsourceInputBalanceProjector` + `UnitPriceHistoryProjector` | `inventory_balance` location=`OUTSOURCE` (구 `OS_MT`) | BOM·공정 역추적 → 투입 OS 슬롯 | **Lazy** 외주 **투입** 잔고만 (§4.5). 단가 등록 시 ensure ? 품목 등록 시 **아님** |
| 9 | `StandardCalendarDayRegistered` | — | `production_calendar` | `PCI_MT` 직접 INSERT | 동일 |
| 10 | `WorkCenterCalendarDayRegistered` | — | `production_calendar` | `PCI_MT` 직접 INSERT | 동일 |
| 11 | `UserRegistered` | — | `user` (구 `UI_MT`) | 부수효과 없음 | 동일 |

**Projector 실행 시점 (권장)**

| 구분 | 시점 | 대상 |
|------|------|------|
| 동기 (같은 TX 또는 AFTER_COMMIT 즉시) | 등록·수정 직후 UI 반영 | 거래처 원장(1), 공정 WIP(5), 단가·외주(8), 달력(9~10) |
| 동기 | 외주 BOM walk (건수 적을 때) | 8c |
| 비동기 (선택) | 대량 재계산·리플레이 | 마이그레이션·규칙 변경 시 |

### 2.1 거래처 원장 ? `partner_ledger_*` (v0.3 확정)

레거시 `BSI_MT` wide 72컬럼을 **계정 헤더 + 월 12행**으로 분리합니다. `financial_partner_year` 설계는 **폐기**.

| `company_role` | `partner_ledger_account.ledger_type` |
|----------------|--------------------------------------|
| `SALES` | `SALES` |
| `PURCHASE` | `PURCHASE` |
| `OUTSOURCE` | `PURCHASE` (기존 행 재사용) |
| `COST` (단독) | account **미생성** |

| 테이블 | 역할 | 갱신 주체 |
|--------|------|-----------|
| `partner_ledger_account` | 연도·계정 유형 헤더 | `PartnerLedgerProjector` (거래처 저장 TX) |
| `partner_ledger_monthly` | 월별 발생·수금·매입·지급 | TX Listener (`PurchaseReceiptPosted`, `OutsourceDeliveryPosted`, 수금·지급 등) |

**금지:** `basis` 패키지에서 `partner_ledger_monthly` 금액 직접 UPDATE ? [`inventory-ledger-spec §7`](../../results/sample/inventory-ledger-spec.md)

**Cut-over:** `BSI_MT` 월컬럼 → `partner_ledger_monthly` unpivot·차분 ([inventory-ledger-spec §9](../../results/sample/inventory-ledger-spec.md))

---

## 3. `ItemRegistered` ? 재고 Lazy ensure (TO-BE)

> 품목 마스터: **11필드 슬림** ([`basis-item-spec`](../../results/sample/basis-item-spec.md)). 등록 시 **재고 부수효과 없음**.

| 자산분류 | 레거시 등록 시 | SmartManager (v0.3) | `inventory_location` / ensure 시점 |
|----------|---------------|---------------------|-----------------------------------|
| 원자재 | `RMS_MT` 1건 | **미생성** | 첫 구매입고 등 → `RAW` |
| 제품 | `BS_MT`×3 + `DS_MT` | **미생성** | 첫 영업 입출고 → `SALES` **1건** + `DELIVERY` |
| 상품 | 위 + `PS_MT` seq=99 | **미생성** | 위 + 공정99 WIP 필요 시 `ProcessRegistered` 또는 첫 생산 TX |
| 반제품 | 미생성 | **미생성** | 공정 등록·생산 TX 시 `WIP` |
| 부자재·소모품 | 미생성 | **미생성** | 원자재와 동일 정책 검토 |

**모델:** `inventory_location`(시드) + `inventory_balance` + `inventory_balance_monthly` + `stock_movement` ? [`inventory-ledger-spec §3`](../../results/sample/inventory-ledger-spec.md)

> 레거시 버그: `RMS_MT_ExistingRecord`가 `ItemNum` 대신 `Standard`로 중복 체크 → SmartManager에서는 `item_id + fiscal_year + location` UK로 통일.

### 3.1 영업창고 ? SmartManager 확정 (1개)

| 항목 | 레거시 | SmartManager |
|------|--------|--------------|
| 영업창고 수 | `BS_MT` **3건** (`BusinessStorehouseNum` 1·2·3) | **`SALES` 슬롯 1건** (품목·연도·최종공정 `14009999`당 1행) |
| 납품창고 | `DS_MT` 1건 | `DELIVERY` 1건 (**유지**) |
| 창고이동 | 영업1↔2↔3 (`StorehouseMoving` / `SM_MT`) | **미사용** (단일 영업창고). 필요 시 추후 `location` 세분화 검토 |
| ensure 시점 | `BusinessStorehouse` 루프 3회 | 첫 TX 시 `SALES` **1건** Lazy |
| SP 이식 | `BusinessStorehouseNum` 컬럼 | 프로젝션 뷰에서 `warehouse_no = 1` 고정 또는 컬럼 제거 |
| 마이그레이션 | 3행 × 월별 컬럼 | **§3.2** 합산 규칙으로 `SALES` 1행 적재 |

`inventory_balance` 예: `location=SALES`, `warehouse_no` 없음(또는 항상 `1`), `process_code_id`→최종공정.

### 3.2 마이그레이션 ? 영업1·2·3 → 단일 `SALES` 합산 규칙

> Step 0 확정. Cut-over 시 레거시 `BS_MT` 최대 3행을 SmartManager `inventory_balance`(또는 동등 read model) **1행**으로 적재합니다.

#### 3.2.1 대상·그룹 키

| 항목 | 값 |
|------|-----|
| 소스 테이블 | `BS_MT` |
| 필터 | `RecodingState = 1`, `ProcessCode = '14009999'` (공정99·최종공정) |
| 그룹 키 (레거시) | `ItemNum` + `[Year]` |
| 그룹 키 (SmartManager) | `item_id` + `fiscal_year` + `location_type = SALES` |

`BusinessStorehouseNum` 1·2·3은 **합산 대상**이며, 슬롯 식별자에는 포함하지 않습니다.

#### 3.2.2 합산 공식 (확정)

월별 수량·원가(`StockQuantity1`~`12`, `StockCost1`~`12`)와 전년이월(`LastYearTransferQuantity`, `LastYearTransferCost`) 모두 **창고 번호별 합계**로 산출합니다.

```
SALES.StockQuantity{m}  = SUM(BS_MT.StockQuantity{m})   -- m = 1..12, BusinessStorehouseNum IN (1,2,3)
SALES.StockCost{m}        = SUM(BS_MT.StockCost{m})
SALES.opening_qty         = SUM(BS_MT.LastYearTransferQuantity)
SALES.opening_cost        = SUM(BS_MT.LastYearTransferCost)
```

- `DECIMAL` 합산, `NULL`은 `0` 처리.
- 동일 `(ItemNum, Year)`에 활성 행이 3건 미만이어도 **존재하는 행만** 합산 (부분 생성 품목 대응).

#### 3.2.3 레거시 근거 (합산 정당성)

| 근거 | 설명 |
|------|------|
| 출고 가용재 조회 | `SPOutStoreQuantity` / `SPOutStoreQuantityExcel` ? `sum(StockQuantity12)` (창고 번호 **미지정**) |
| 일부 입출고·검사 | `BusinessStorehouseNum = 1` 고정 업데이트 다수 → 2·3은 주로 **창고이동**(`SM_HT` + `SM_MTTableUpdate`)으로 증감 |
| 창고이동 | 영업1↔2↔3 이동은 원·이동 창고 각각 `BS_MT` 행을 증감 → 합산 시 **총 영업 재고 보존** |
| 연말 창고 생성 | `SPCreateStore` ? 차년 `BS_MT` 3행 복제, **이월 수량은 영업1만** 반영 → 합산 시 2·3 이월 `0` + 1 이월 = 실질 총 이월과 일치 |

> 일부 화면·SP는 영업1만 갱신하지만, 마이그레이션 SSOT는 **`BS_MT` 3창고 합산 잔량**입니다. Cut-over 후 SmartManager 단일 `SALES`와 운영 재고가 일치합니다.

#### 3.2.4 적재 절차 (권장 순서)

1. `item` 마스터 이전 완료 (`ItemNum` → `item_id`)
2. `BS_MT` 그룹별 합산 스테이징 뷰/테이블 생성
3. `inventory_slot` ? `SALES` 슬롯 1건 INSERT (`SalesSlotProjector`와 동일 키)
4. `inventory_balance` ? 합산 수량·원가·월별 컬럼 적재
5. **검증** (§3.2.5) 통과 후 cut-over
6. (선택) `LegacyInventoryMigrated` 도메인 이벤트 ? payload에 `source_table: BS_MT`, `merged_warehouse_nos: [1,2,3]`, 월별 합산 스냅샷

#### 3.2.5 검증 SQL (레거시 vs SmartManager)

```sql
-- 품목·연도별: 레거시 BS 합계 vs SmartManager SALES (예: 12월 수량)
SELECT
  b.ItemNum,
  b.[Year],
  SUM(b.StockQuantity12) AS legacy_bs_qty12,
  sm.qty12               AS smart_sales_qty12
FROM BS_MT b
LEFT JOIN migration_sales_balance sm
  ON sm.item_no = b.ItemNum AND sm.fiscal_year = b.[Year]
WHERE b.RecodingState = 1
  AND b.ProcessCode = '14009999'
GROUP BY b.ItemNum, b.[Year], sm.qty12
HAVING ABS(SUM(b.StockQuantity12) - ISNULL(sm.qty12, 0)) > 0.001;
```

검증 실패 시: 해당 품목의 `SM_HT`(창고이동)·`RecodingState`·연도 불일치를 우선 조사.

#### 3.2.6 예외·비범위

| 항목 | 처리 |
|------|------|
| `RecodingState = 0` | 마이그레이션 제외 |
| `DS_MT` (납품) | **별도** `DELIVERY` 잔고 — 본 규칙 **미적용** |
| `SM_HT` (창고이동 이력) | 단일 SALES에서는 **아카이브 이전만**; 잔량 재계산 입력으로는 사용 안 함 (`BS_MT` 합산이 SSOT) |
| 상품 `PS_MT` seq=99 | 영업 합산과 **무관** — `WIP` 별도 마이그레이션 |

---

## 4. `ProcessRegistered` ? WorkDistinction별 Projector (SmartManager 개선안)

| 작업구분 (`WorkDistinction`) | UI index | 레거시 `PS_MT` | SmartManager 완료 잔고 (`WIP` OUTPUT) | SmartManager 투입 잔고 (`OUTSOURCE`) |
|-----------------------------|----------|---------------|----------------------------------------|---------------------------------------------|
| 자가 | 1 | 항상 생성 | `WipBalanceProjector.ensure` — 완료 공정 WIP | — |
| 외주 | 2 | 항상 생성 (불필요) | 외주 **완료 공정** WIP만 ensure | `OutsourceUnitPriceRegistered` 시 투입 (§4.5) |
| 자가/외주 | 3 | 항상 생성 | 자가·외주 완료 공정 WIP | 외주분 투입은 단가 등록 시 |

---

## 4.5 외주 재고 잔고 2종 (투입 / 완료)

> **공정 = 작업**, **창고 수량 = 해당 작업이 끝난 공정품 수량**.  
> 외주는 **투입(이전 공정품·원자재 @ 거래처)** 과 **완료(외주 공정 종료품 @ 우리)** 가 분리됩니다.  
> 레거시: 투입=`OS_MT`, 완료 입고=`PS_MT` (`Register.cs` 외주출고·외주납품).

### 4.5.1 잔고 정의 · ensure Projector

| 구분 | location | role | Legacy | 생성 트리거 | Projector | UK 키 (FK는 §10) |
|------|---------------|------|--------|------------|-----------|-------------------|
| **투입** | `OUTSOURCE` | `INPUT` | `OS_MT` | `OutsourceUnitPriceRegistered` | `OutsourceInputBalanceProjector.ensure` | `item_id`, `company_id`, `input_process_id` |
| **완료** | `WIP` | `OUTPUT` | `PS_MT` (외주 공정) | `ProcessRegistered` (외주·혼합의 외주 공정) | `WipBalanceProjector.ensure` | `item_id`, `output_process_id` |

**`OutsourceInputBalanceProjector` (레거시 `OutSideStoreTable` 이식)**

1. 외주단가의 시작·종료 공정 기준으로 **앞 공정** 역순 탐색  
2. `WorkDistinction`이 자가(≠외주)인 공정 → 해당 **투입 공정품** `OUTSOURCE` INPUT 잔고 ensure  
3. 앞 공정 없음 → BOM 하위 품목 탐색, 원자재면 `(child_item_id, vendor, process_id=소재공정)`  
4. **외주 공정 자체**는 `OUTSOURCE` INPUT 잔고에 넣지 않음  

### 4.5.2 트랜잭션 이벤트 → 잔량 Projector (참고)

| 트랜잭션 Event | FROM (감소) | TO (증가) | 레거시 |
|----------------|------------|----------|--------|
| `OutsourceShipmentPosted` (외주출고) | `RAW` 또는 `WIP`(투입 공정) | `OUTSOURCE` INPUT (동일 투입 공정, vendor) | `OutSideStorehouseRegister` |
| `OutsourceDeliveryPosted` (외주납품) | `OUTSOURCE` INPUT (투입재 소진) | `WIP` OUTPUT (외주 완료 공정) | `OutSideDelivertOutStoreTable` + `OutSideDeliveryInStoreProductionTable` |

### 4.5.3 §2 등록 표 보완 (외주 2종 요약)

| Trigger Event | 투입 잔고 (`OUTSOURCE` INPUT) | 완료 잔고 (`WIP` OUTPUT) |
|---------------|------------------------------|----------------------------------|
| `OutsourceUnitPriceRegistered` | **ensure** (`OutsourceInputBalanceProjector`) | — |
| `ProcessRegistered` (외주 공정) | — | **ensure** (`WipBalanceProjector`) |
| `ProcessRegistered` (자가 공정) | — | **ensure** (자가 완료 공정 WIP) |

---

## 5. 수정·삭제 이벤트 → Projector

| # | Update Event | Projector | Read Model 변경 |
|---|--------------|-----------|------------------|
| 1 | `CompanyUpdated` | `PartnerLedgerProjector.ensureAccounts` | `partner_ledger_account` upsert·비활성 (roles 변경) |
| 2 | `ItemUpdated` | — (자산분류 변경 시 `InventoryBalanceService.reconcile` 정책) | Lazy 잔고만 영향 — 마스터 등록 시 생성 없음 |
| 3 | `BomLineUpdated` | `BomHistoryProjector` | `UPIOI_HT` |
| 5 | `ProcessUpdated` | `WipBalanceProjector.reconcile` | `inventory_balance` WIP 행 갱신·비활성 |
| 8a~c | `*UnitPriceUpdated` | `UnitPriceHistoryProjector` | `UPUCI_HT` |
| 8c | `OutsourceUnitPriceUpdated` | `OutsourceInputBalanceProjector.rebuild` | 투입 `OUTSOURCE` 잔고 재계산 |

| # | Delete Event | Projector | Read Model 변경 (레거시) |
|---|--------------|-----------|-------------------------|
| 1 | `CompanyDeleted` | `PartnerLedgerProjector.deactivate` | `partner_ledger_account` 비활성 + 외주 잔고 cascade (INF-4) |
| 2 | `ItemDeleted` | `InventoryBalanceService.deactivateAll` + cascade | 활성 `inventory_balance` 비활성 (마스터 등록 시 생성 없었을 수 있음) |
| 3 | `BomLineDeleted` | `BomHistoryProjector` | `UPIOI_HT` |
| 4 | `WorkCenterDeleted` | — (Override `work_center_calendar` **유지**) | sparse Override 행 자동 삭제 안 함 |
| 5 | `ProcessDeleted` | `WipBalanceProjector` + `OutsourceInputBalanceProjector` + cascade | WIP·OUTSOURCE 잔고 비활성 + `WSI_MT` |
| 8c | `OutsourceUnitPriceDeleted` | `OutsourceInputBalanceProjector.deactivate` + history | OUTSOURCE 잔고 + `UPUCI_HT` |

---

## 6. UI Read Model ? 화면별 조회 소스

| 화면 | Primary Read Model | Event Timeline (이력 탭) |
|------|-------------------|-------------------------|
| 거래처 목록/상세 | `company` + `company_role` + `partner_ledger_account` | `CompanyRegistered` … |
| 품목 | `item` (11필드) + `inventory_balance` 요약 (있을 때만) | `ItemRegistered` … |
| 품목구성 | `IOI_MT` | `bom_change_log` (`UPIOI_HT`) |
| 작업장 | `work_center` | `WorkCenterRegistered` … |
| 공정 | `process_sequence` + `inventory_balance` WIP/OUTSOURCE 요약 | `ProcessRegistered` … |
| 설비 | `equipment` | — |
| 작업표준 | `work_standard` | — |
| 단가 3종 | `UCI_MT` | `unit_price_change_log` |
| 생산달력 | `production_calendar` | — |
| 사용자 | `user` | — |

---

## 7. `domain_event` 테이블 ? 공통 payload 필드 (권장)

| 필드 | 타입 | 설명 |
|------|------|------|
| `event_id` | UUID | PK |
| `event_type` | VARCHAR | 예: `ItemRegistered` |
| `schema_version` | INT | payload 버전 |
| `aggregate_type` | VARCHAR | `Company`, `Item`, `Process` … |
| `aggregate_id` | VARCHAR | **PK 문자열** (품번 아님 ? §10) |
| `occurred_at` | DATETIME | 발생 시각 |
| `actor_user_id` | VARCHAR | 등록자 |
| `payload` | JSON | 스냅샷·diff (**내용키 스냅샷 허용**, FK 아님) |
| `correlation_id` | UUID | 한 화면 저장 = 한 correlation |

---

## 8. Phase 1 구현 범위 (코딩 착수 시)

| 우선순위 | Event + Projector | 이유 |
|---------|-------------------|------|
| P0 | `CompanyRegistered` → `PartnerLedgerProjector.ensureAccounts` | 원장 account만, 금액 TX 위임 |
| P0 | `ItemRegistered` → **부수효과 없음** | 11필드 마스터만 ? 재고 Lazy |
| P0 | `ProcessRegistered` → `WipBalanceProjector.ensure` | 완료 공정 WIP 잔고 |
| P0 | `OutsourceUnitPriceRegistered` → `OutsourceInputBalanceProjector` | 외주 **투입** 잔고, BOM walk |
| P1 | BOM·단가 이력 Projector | `UPIOI_HT`, `UPUCI_HT` |
| P1 | 생산달력·WC Override — 작업장 등록과 **분리** | `production_calendar` / `work_center_calendar` 별도 메뉴 |
| P2 | Delete cascade Projector | 품목·공정 삭제 연쇄 |
| P3 | Real 3종 | `recordType` 파라미터로 handler 공유 |

---

## 9. 레거시 vs SmartManager ? 한 줄 대조

| 관심사 | 레거시 | SmartManager (이 문서) |
|--------|--------|------------------------|
| 진실의 원천 | `_MT` 테이블 직접 INSERT | `domain_event` + Projector |
| 공정 → 생산창고 | WorkDistinction 무관 항상 | 자가·외주 **완료 공정품**만 PS |
| 외주 투입 창고 | `OS_MT` (외주단가 등록 시) | `OUTSOURCE` INPUT `inventory_balance`, 동일 트리거 |
| 외주 완료 입고 | `PS_MT` (외주납품) | `WIP` OUTPUT |
| 영업창고 | `BS_MT` ×3 (영업1·2·3) | **`SALES` 1개** (§3.1, 마이그레이션 합산 §3.2) |
| 거래처 원장 | `BSI_MT` 72컬럼 | `partner_ledger_account` + `monthly` 12행 |
| 품목 등록→창고 | 즉시 RMS/BS/DS/PS | **Lazy** `inventory_balance` |
| 테이블 참조 | 내용키(품번·공정코드) 혼용 | **FK = PK만** (§10) |
| 감사/재처리 | 이력 테이블 일부 | 전 이벤트 스트림 + replay |
| SP 호환 | `_MT` 필수 | Projector가 동등 스키마 유지 |

---

## 10. FK 정책 (SmartManager 확정)

레거시는 **PK(`*Index`)와 내용키(품번·사업자번호·공정코드 등)** 를 병행하고, 자식·이력·재고·SP 조인에서 **내용키를 논리 FK처럼** 사용합니다.  
SmartManager에서는 **관계형 FK는 부모 테이블 PK(대리키)만** 사용합니다.

### 10.1 원칙

```
자식.*_id  →  부모.id  (PK만)
내용키      →  해당 마스터 테이블에만 존재 (UNIQUE 비즈니스 키, FK 아님)
```

| 구분 | 역할 | 예 (레거시 → SmartManager) |
|------|------|---------------------------|
| **PK** | 행 식별·**FK 대상** | `ItemInfoIndex` → `item.id` |
| **내용키** | 화면·검색·중복 방지 | `ItemNum` → `item.item_no` (UNIQUE, FK 금지) |
| **코드값** | 표시·레거시 호환 | `SmallClassificationCode` → `public_code_id` FK |

### 10.2 레거시 → SmartManager 매핑 예

| 레거시 (내용키 참조) | SmartManager (PK FK) |
|---------------------|----------------------|
| `IOI_MT.ParentItemNum` / `ChildItemNum` | `bom_line.parent_item_id` / `child_item_id` |
| `PSI_MT.ItemNum` + `ProcessCode` | `process_sequence.item_id` + `process_code_id` |
| `UCI_MT` 품번·거래처번호·공정코드 | `unit_price.item_id`, `company_id`, `begin_process_id`, `end_process_id` |
| `RMS_MT` / `PS_MT` / `OS_MT` `ItemNum` | `inventory_balance.item_id`, `location_id`, `process_sequence`, `partner_id`(외주) |
| `UPIOI_HT.ItemOrganizationInfoIndex` | `bom_change_log.bom_line_id` |
| PUC 조인 `ProcessCode = SmallClassificationCode` | `process_code_id` → `public_code.id` |

### 10.3 예외·허용

| 항목 | FK | 비고 |
|------|-----|------|
| 마스터 간 관계 | **PK만** | JPA `@ManyToOne` → `*_id` |
| `domain_event.payload` | FK 없음 | 감사용 품번·코드 **스냅샷** JSON 허용 |
| 레거시 SP 호환 뷰 | 논리 컬럼 | `item_no`, `process_code`는 **뷰/조회 DTO**에서 JOIN으로 노출 |
| 소프트 삭제 | PK FK 유지 | `deleted_at` / `recording_state`; 활성 행만 참조 허용 (앱·DB 정책) |

### 10.4 D4 · API 규칙

- 자식 테이블 컬럼명: `*_id` (BIGINT/UUID)  
- 내용키 컬럼: 마스터에만 `item_no`, `business_reg_no` 등  
- API 요청: 화면은 내용키 입력 가능 → 서비스에서 **PK 해석 후 저장**  
- **금지**: 자식 테이블에 `item_no`, `process_code` 등을 FK 대용으로 저장  

---

*작성 기준: KIT_ERP `MasterInfoRecordRUD.cs`, `Register.cs` (외주출고·외주납품)*