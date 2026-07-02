# 공용코드 콤보박스 매핑 초안 (Code Group · Field Binding)

> Step 0 산출물 — D4 보조 문서  
> 레거시: `PUC_MT`, `PublicUseCode.aspx`, 각 화면 `.aspx.cs` 하드코딩  
> SmartManager: `code_group` + `field_binding` + `GET /api/code-groups/{key}/options`

---

## 1. 목적

레거시는 콤보박스마다 `LargeClassificationCode`(4자리) 또는 `LargeClassificationName`을 **소스에 직접 박아** PUC를 조회합니다.  
SmartManager에서는 **화면 필드 ↔ 코드 그룹** 연결을 메타데이터로 관리하고, React는 `<CodeSelect codeGroup="..." />`만 사용합니다.

**운영자:** 소분류 추가·수정 (레거시와 동일)  
**개발/관리자:** 코드 그룹 정의, 화면 필드 바인딩, 신규 대분류(드물게)

---

## 2. 레거시 코드 체계 요약

| 항목 | 규칙 |
|------|------|
| 소분류 코드 | 8자리 = **대분류 4자리 + 소분류 4자리** |
| 대분류 | DB 시드 고정, UI에서 **선택만** (`PublicUseCode.aspx`) |
| 소분류 | 운영자 등록, 자동 채번(`+10` 증분) |
| 레거시 저장값 | DB 컬럼에 **`SmallClassificationCode`** (8자리) 저장 |
| SmartManager 저장 | **`public_code.id` FK** (`*_id`). `small_code`는 마스터·조회 DTO에서만 ([domain-event §10](./domain-event-projector-matrix.md)) |
| 조회 | `RecodingState = 1` 만 유효 |

**레거시 연결 방식 2종 (혼재 — SmartManager에서 통일 필요)**

| 방식 | 예 | 비고 |
|------|-----|------|
| 대분류 **코드** | `LargeClassificationCode = '0210'` | ItemInfo, ProcessSequenceInfo |
| 대분류 **이름** | `LargeClassificationName = '거래처분류1'` | CompanyInfo, UserInfo, SP |

---

## 3. 코드 그룹 마스터 (대분류 일람 · v0.2)

> **SoT:** [`data/puc-large-distinct.csv`](./data/puc-large-distinct.csv) (2026-07-02 · KIT_ERP 코드 감사 + 가설)  
> **운영 DB 최종 검증:** `export-step0-data.ps1` + `$env:SMARTMANAGER_ERP_DSN` → `*-from-db.csv`로 교체  
> `0110~0130` 거래처분류는 **가설**(confidence=low) — DB export 전까지 `LargeClassificationName` 조회와 병행 검증.

| code_group_key | large_code | large_name | usage_type | editable | exclude_codes | 비고 |
|----------------|------------|------------|------------|----------|---------------|------|
| `ITEM_CLASSIFICATION_1` | `0210` | 품목분류1 | GENERIC | small_only | — | SP·현황 다수 |
| `ITEM_CLASSIFICATION_2` | `0220` | 품목분류2 | GENERIC | small_only | — | |
| `ITEM_CLASSIFICATION_3` | `0230` | 품목분류3 | GENERIC | small_only | — | |
| `ITEM_CLASSIFICATION_4` | `0240` | 품목분류4 | GENERIC | small_only | — | |
| `COMPANY_TRADE_CLASS_1` | `0110`* | 거래처분류1 | GENERIC | small_only | — | *DB 검증 |
| `COMPANY_TRADE_CLASS_2` | `0120`* | 거래처분류2 | GENERIC | small_only | — | *DB 검증 |
| `COMPANY_TRADE_CLASS_3` | `0130`* | 거래처분류3 | GENERIC | small_only | — | *DB 검증 |
| `UNIT_GENERAL` | `0400` | 단위 | UNIT | small_only | — | 품목 v0.2 Drop |
| `UNIT_SPEC_1` | `0410` | 규격단위1 | UNIT | small_only | — | |
| `UNIT_SPEC_2` | `0420` | 규격단위2 | UNIT | small_only | — | |
| `UNIT_SPEC_3` | `0430` | 규격단위3 | UNIT | small_only | — | |
| `UNIT_STOCK` | `0510` | 재고단위 | UNIT | small_only | — | |
| `UNIT_BOM` | `0520` | BOM단위 | UNIT | small_only | — | 품목구성 |
| `UNIT_PURCHASE` | `0530` | 구매단위 | UNIT | small_only | — | |
| `UNIT_SALE` | `0540` | 판매단위 | UNIT | small_only | — | |
| `ITEM_TYPE` | `0610` | 품목타입 | GENERIC | small_only | — | |
| `ITEM_MATERIAL` | `0620` | 재질 | GENERIC | small_only | — | |
| `ITEM_STATE` | `0630` | 품목상태 | GENERIC | small_only | `06300010` | 양산품 예약 |
| `BOM_SUPPLY_DIVISION` | `0640` | 조달구분 | GENERIC | small_only | — | 품목구성 |
| `PROCESS_CODE` | `1400` | 공정명 | PROCESS | small_only | `14000000`,`14009999` | Tier2 삭제 검사 |
| `EQUIPMENT_CLASS` | `0720` | 설비분류 | GENERIC | small_only | `07200010`,`07200020` | |
| `EQUIPMENT_LOCATION` | `0800` | 설비위치 | GENERIC | small_only | — | d4-equipment Drop |
| `QC_DEFECT_CAUSE` | `1010` | 부적합원인 | NC_REASON | small_only | — | Phase2 |
| `QC_DEFECT_PHENOMENON` | `1000` | 부적합현상 | NC_DETAIL | small_only | — | Phase2 |
| `QC_INSPECTION_DECISION` | `1310` | 검사판정 | GENERIC | small_only | — | Phase2 |
| `PAYMENT_PLAN_TYPE` | `0910` | 지급계획 | GENERIC | small_only | — | Phase2 |
| `PAYMENT_RESULT_TYPE` | `0920` | 지급실적 | GENERIC | small_only | — | Phase2 |
| `WORK_DIARY_GROUP` | `1900` | 업무일지그룹 | WORK_DIARY_GROUP | small_only | — | **신규** — [`work-diary-group-seed.csv`](./data/work-diary-group-seed.csv) |

> Drop: `USER_DEPARTMENT`(부서) — [`d4-user.md`](./d4-user.md) v0.1  
> Phase2 only (code_group 미정): `0330` 구매의뢰원천, `1210` 비작업사유, `1500` 입출고사유, `1700` 보용품입고, `1800` 기타구매

---

## 4. 기준정보 화면별 필드 ↔ 코드 그룹 매핑 (Step 0 핵심)

### 4.1 거래처 — `CompanyInfo.aspx` → `CI_MT`

| UI 라벨 | React field | DB 컬럼 | code_group_key | 레거시 조회 조건 | 저장 타입 |
|---------|-------------|---------|----------------|-----------------|----------|
| 거래처분류1 | tradeClassification1 | TradeClassification1 | `COMPANY_TRADE_CLASS_1` | `LargeClassificationName='거래처분류1'` | PUC 8자리 |
| 거래처분류2 | tradeClassification2 | TradeClassification2 | `COMPANY_TRADE_CLASS_2` | `LargeClassificationName='거래처분류2'` | PUC 8자리 |
| 거래처분류3 | tradeClassification3 | TradeClassification3 | `COMPANY_TRADE_CLASS_3` | `LargeClassificationName='거래처분류3'` | PUC 8자리 |
| 현재거래상태 | currentTradeState | CurrentTradeState | — | aspx 고정 (1/0) | boolean-like |
| 부가세여부 | supplementaryValueTax | SupplementaryValueTax | — | aspx 고정 (1/0) | boolean-like |
| 어음승인기준 | billApprovalStandard | BillApprovalStandard | — | aspx 1~12 | number |

### 4.2 품목 — `ItemInfo.aspx` → `II_MT`

| UI 라벨 | React field | DB 컬럼 | code_group_key | large_code | 저장 타입 |
|---------|-------------|---------|----------------|------------|----------|
| 자산분류 | propertyClassification | PropertyClassification | **ENUM** (PUC 아님) | — | 한글 텍스트 |
| 단위 | unit | Unit | `UNIT_GENERAL` | `0400` | v0.1 Drop | PUC 8자리 |
| 재고단위 | stockUnit | StockUnit | `UNIT_STOCK` | `0510` | PUC 8자리 |
| BOM단위 | bomUnit | BOMUnit | `UNIT_BOM` | `0520` | PUC 8자리 |
| 구매단위 | purchaseUnit | PurchaseUnit | `UNIT_PURCHASE` | `0530` | PUC 8자리 |
| 판매단위 | saleUnit | SaleUnit | `UNIT_SALE` | `0540` | PUC 8자리 |
| 단위1 | unit1 | Unit1 | `UNIT_SPEC_1` | `0410` | PUC 8자리 |
| 단위2 | unit2 | Unit2 | `UNIT_SPEC_2` | `0420` | PUC 8자리 |
| 단위3 | unit3 | Unit3 | `UNIT_SPEC_3` | `0430` | PUC 8자리 |
| 품목타입 | itemType | ItemType | `ITEM_TYPE` | `0610` | PUC 8자리 |
| 재질 | materialQuality | MateralQuality | `ITEM_MATERIAL` | `0620` | PUC 8자리 |
| 품목상태 | itemState | ItemState | `ITEM_STATE` | `0630` | PUC 8자리 |
| 품목분류1 | itemClassification1 | ItemClassification1 | `ITEM_CLASSIFICATION_1` | `0210` | PUC 8자리 |
| 품목분류2 | itemClassification2 | ItemClassification2 | `ITEM_CLASSIFICATION_2` | `0220` | PUC 8자리 |
| 품목분류3 | itemClassification3 | ItemClassification3 | `ITEM_CLASSIFICATION_3` | `0230` | PUC 8자리 |
| 품목분류4 | itemClassification4 | ItemClassification4 | `ITEM_CLASSIFICATION_4` | `0240` | PUC 8자리 |
| 과세여부 | texture | Texture | — | aspx 1/0 | boolean-like |
| 자재산출 | ioCheckable | IOChackable | — | aspx 1/0 | |
| 재고관리여부 | stockManagable | StockManagable | — | aspx 1/0 | |
| 검사구분 | checkDistinction | CheckDistinction | — | aspx 검사/무검사 | |
| 발주방침 | orderPlan | OrderPlan | — | aspx 계산/임의 | |

### 4.3 품목구성 — `ItemOrganizationInfo.aspx` → `IOI_MT`

| UI 라벨 | React field | DB 컬럼 | code_group_key | large_code |
|---------|-------------|---------|----------------|------------|
| BOM단위 | bomUnit | BOMUnit | `UNIT_BOM` | `0520` |
| 조달구분 | supplyDivision | SupplyDivision | `BOM_SUPPLY_DIVISION` | `0640` |
| 공정관리 | processManagement | ProcessManagement | — | aspx 고정 (추정) |
| 세부구분 | subDivision | SubDivision | — | aspx 고정 (추정) |

### 4.4 작업장 — `WCInfo.aspx` → `WCI_MT`

| UI 라벨 | React field | DB 컬럼 | code_group_key | large_code | exclude |
|---------|-------------|---------|----------------|------------|---------|
| 대표공정 | mainProcessCodeId | `main_process_code_id` | `PROCESS_CODE` | `1400` | `14000000`,`14009999` |

### 4.5 공정 — `ProcessSequenceInfo.aspx` → `PSI_MT`

| UI 라벨 | React field | DB 컬럼 | code_group_key | large_code | exclude |
|---------|-------------|---------|----------------|------------|---------|
| 공정코드 | processCodeId | `public_code_id` | `PROCESS_CODE` | `1400` | `14000000`,`14009999` |
| 작업구분 | workDistinction | WorkDistinction | **ENUM** | — | 자가/외주/자가·외주 |
| 작업장 | wcName | WCName | **WC 마스터** | — | `WCI_MT` 조회 |

### 4.6 설비 — `EquipmentInfo.aspx` → `EI_MT`

| UI 라벨 | React field | DB 컬럼 | code_group_key | large_code | exclude |
|---------|-------------|---------|----------------|------------|---------|
| 설비분류 | equipmentCategoryId | `equipment_category_id` | `EQUIPMENT_CLASS` | `0720` | `07200010`,`07200020` |

> v0.1 Drop: `UNIT_GENERAL`(단위), `EQUIPMENT_LOCATION`(위치) — [`d4-equipment.md`](./d4-equipment.md)

### 4.7 작업표준 — `WorkStandardInfo.aspx` → `WSI_MT`

| UI 라벨 | React field | DB 컬럼 | 참조 | 비고 |
|---------|-------------|---------|------|------|
| 공정 | processSequenceId | `process_sequence_id` | **`process_sequence` FK** | `ProcessSelect` — INHOUSE/SPLIT만 |
| 작업장 | workCenterId | `work_center_id` | `work_center` | |
| 사용설비 | equipmentId | `equipment_id` | `equipment` | nullable |
| 주작업자 | mainWorkerId | `main_worker_user_id` | `user` | nullable |

### 4.8 단가 — `UCI_MT` (판매/구매/외주 → `unit_price`)

| UI 라벨 | React field | DB 컬럼 | code_group_key | 비고 |
|---------|-------------|---------|----------------|------|
| 시작공정 | beginProcessCodeId | `begin_process_code_id` | `PROCESS_CODE` | **외주만** FK → `public_code.id` |
| 종료공정 | endProcessCodeId | `end_process_code_id` | `PROCESS_CODE` | **외주만** FK |

> 판매·구매: 공정 FK **NULL**. 레거시 `14009999` 고정 — **미채택** ([`d4-unit-price.md`](./d4-unit-price.md))

### 4.9 사용자 — `UserInfo.aspx` → `UI_MT`

| UI 라벨 | React field | DB 컬럼 | 참조 | 비고 |
|---------|-------------|---------|------|------|
| 사용권한 | roleIds | `user_role` | **`role`** (RBAC) | PUC `AuthorityIndex` **미채택** |
| 업무일지그룹 | workDiaryGroupId | `work_diary_group_id` | `WORK_DIARY_GROUP` | FK → `public_code.id` |

> v0.1 Drop: `USER_DEPARTMENT`(부서), `PostCode`·`Responsibility`, 거래처 `BusinessRegistrationNum` — [`d4-user.md`](./d4-user.md)

### 4.10 Real 3종 (설계와 동일 UI)

| 화면 | PUC 사용 필드 | code_group_key |
|------|--------------|----------------|
| `RealItemOrganizationInfo` | BOM단위, 조달구분 | `UNIT_BOM`, `BOM_SUPPLY_DIVISION` |
| `RealProcessSequenceInfo` | 공정코드 | `PROCESS_CODE` |
| `RealWorkStandardInfo` | (공정 join) | `PROCESS_CODE` |

---

## 5. PUC 미사용 · Domain Enum (SmartManager 유지 권장)

| 화면 | 필드 | 값 | 유지 이유 |
|------|------|-----|----------|
| ItemInfo | propertyClassification | 원자재, 반제품, 제품, 상품, 부자재, 소모품 | 창고 Projector·업무 규칙과 직결 |
| ProcessSequenceInfo | workDistinction | 자가, 외주, 자가/외주 | PS/OS 슬롯 분기 |
| CompanyInfo / ItemInfo | Y/N, 1/0 콤보 | 거래상태, 과세, 검사 등 | 단순 boolean |
| WCInfo / 공정 | wcName | WCI_MT | 마스터 FK |

---

## 6. 시스템 예약 소분류 (수정 불가 — `PublicUseCode.aspx.cs`)

| SmallClassificationCode | large (앞4) | 용도 |
|-------------------------|-------------|------|
| `14000000` | `1400` | 소재공정 (원자재창고 기본) |
| `14009999` | `1400` | 최종공정 (영업·납품·상품) |
| `07200010` | `0720` | 공구 |
| `07200020` | `0720` | 치구 |
| `03200010` | `0320` | 사업계획 — 정상 |
| `03200020` | `0320` | 사업계획 — 실행계획 |
| `03200030` | `0320` | 사업계획 — 기종생산 |
| `03100010` | `0310` | 실행계획 — 정상 |
| `03300010` | `0330` | (관련) 정상 |
| `16000000` | `1600` | 이력 — 초기등록 |
| `16000010` | `1600` | 이력 — 삭제 |
| `06300010` | `0630` | 양산품코드 |

SmartManager `code_group`에 `system_reserved_codes[]` 로 등록하고 API에서 **옵션 제외 또는 readonly** 처리.

---

## 7. SmartManager 메타 테이블 설계 (초안)

### 7.1 `code_group`

```sql
-- 개념 스키마 (MariaDB)
CREATE TABLE code_group (
  code_group_key   VARCHAR(64) PRIMARY KEY,
  large_code       CHAR(4) NOT NULL,
  large_name       VARCHAR(100) NOT NULL,
  editable_level   ENUM('SYSTEM','SMALL_ONLY','FULL') DEFAULT 'SMALL_ONLY',
  usage_type       VARCHAR(20) NOT NULL,  -- public_code 헤더와 동기
  exclude_codes    JSON NULL,
  description      VARCHAR(255)
);
```

### 7.2 `field_code_binding`

```sql
CREATE TABLE field_code_binding (
  id               BIGINT PRIMARY KEY AUTO_INCREMENT,
  screen_id        VARCHAR(64) NOT NULL,   -- e.g. ItemInfo
  field_id         VARCHAR(64) NOT NULL,   -- e.g. itemClassification1
  db_table         VARCHAR(32),
  db_column        VARCHAR(64),
  code_group_key   VARCHAR(64),            -- NULL이면 ENUM/마스터 FK
  binding_type     ENUM('PUC','ENUM','MASTER_FK') NOT NULL,
  UNIQUE (screen_id, field_id)
);
```

### 7.3 확장 절차 (신규 대분류)

1. `code_group` 행 추가 (관리자)
2. `PUC_MT` 대분류 시드 (또는 마이그레이션)
3. `field_code_binding` 에 화면 필드 연결
4. SP·Projector 영향 분석
5. **일반 사용자는 2단계(소분류)만**

---

## 8. API · React (초안)

```
GET /api/code-groups                          -- 그룹 목록 (관리)
GET /api/code-groups/{codeGroupKey}/options   -- options [{id,value,label}]
  -- id: public_code.id (FK)  value: small_code  label: small_name
GET /api/screens/{screenId}/field-bindings    -- D4 자동 생성용
```

```tsx
// React 공통 컴포넌트
<CodeSelect
  codeGroup="ITEM_CLASSIFICATION_1"
  valueField="id"
  value={form.itemClassification1Id}
  onChange={...}
  excludeReserved   // default true
/>
```


### 8.1 PUC 바인딩 공통 규칙

| 계층 | 규칙 |
|------|------|
| 옵션 조회 | `code_group_key` → `GET /api/code-groups/{key}/options` |
| 화면·요청 | `CodeSelect` 값 = **id** (`public_code.id`) |
| DB 저장 | `*_id` BIGINT FK → `public_code.id` |
| 표시·응답 | JOIN으로 `small_code`, `small_name` 노출 |
| 금지 | 자식 테이블에 `small_code` 문자열 FK 대용 저장 ([domain-event §10.4](./domain-event-projector-matrix.md)) |

---

## 9. 운영 DB 확인 (Tier B)

**interim 완료 (2026-07-02):** [`data/puc-large-distinct.csv`](./data/puc-large-distinct.csv) — KIT_ERP `LargeClassificationCode` 정적 감사 30종 + TO-BE `1900`.

**DB export (최종):** [`data/export-step0-data.ps1`](./data/export-step0-data.ps1)

```powershell
$env:SMARTMANAGER_ERP_DSN = "Server=...;Database=Sindong_ERP;...;TrustServerCertificate=True"
.\docs\step0\data\export-step0-data.ps1
```

```sql
-- B2
SELECT DISTINCT LargeClassificationCode, LargeClassificationName
FROM PUC_MT WHERE RecodingState = 1 ORDER BY LargeClassificationCode;

-- B3
SELECT Num, Title, EngTitle, [Group]
FROM CT_T WHERE [Group] = N'기준정보' ORDER BY Num;

-- B6 검증 (거래처분류)
SELECT DISTINCT LargeClassificationCode, LargeClassificationName
FROM PUC_MT WHERE RecodingState = 1
  AND LargeClassificationName IN (N'거래처분류1', N'거래처분류2', N'거래처분류3');
```

- `0110~0130` 가설이 틀리면 §3·CSV 갱신
- Real 3종 `CT_T.Num` 실측 후 D2 §2.2 보완

---

## 10. 부록 — 기준정보 외 공통 검색조건 (참고)

동일 `code_group_key`를 **현황·구매·생산** 화면에서 재사용:

| code_group_key | 재사용 화면 예 (레거시) |
|----------------|------------------------|
| `ITEM_CLASSIFICATION_1` | 구매발주, 외주납품, 판매일보, 입출고현황 등 |
| `ITEM_CLASSIFICATION_2~4` | 판매일보, 입고실적 등 |
| `PROCESS_CODE` | 창고수불, 공정별 재고 |
| `QC_*` | 품질검사 SP (`PageLoad_GetDataSource`) |

> Step 0 범위는 **기준정보 11메뉴** 우선. 부록은 Phase 2 D4 확장 시 일괄 매핑.

---

## 11. 관련 문서

- [`step0-plan-D1-D4.md`](./step0-plan-D1-D4.md) — Step 0 D1~D4 계획·일정·완료 기준
- [`step0-closure-checklist.md`](./step0-closure-checklist.md) — Step 0 마무리 Tier·게이트
- [`domain-event-projector-matrix.md`](./domain-event-projector-matrix.md) — 등록 부수효과
- [d4-company.md](./d4-company.md) — 거래처 D4 초안
- [d4-item.md](./d4-item.md) — 품목 D4 초안
- [d4-public-code.md](./d4-public-code.md) — 공용코드 관리 화면 D4 v0.1
- D4 나머지 — d4-*.md (§4.2 순서)

---

*작성 기준: `ItemInfo.aspx.cs`, `CompanyInfo.aspx.cs`, `ProcessSequenceInfo.aspx.cs`, `ItemOrganizationInfo.aspx.cs`, `EquipmentInfo.aspx.cs`, `WCInfo.aspx.cs`, `UserInfo.aspx.cs`, `PublicUseCode.aspx.cs`, `dbo.PageLoad_GetDataSource`*