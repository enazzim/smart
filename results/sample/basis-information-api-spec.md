# KIT_ERP 기준정보 API·필드 매핑 설계서

> **문서 버전:** 1.13  
> **작성일:** 2026-06-22  
> **기준:** KIT_ERP `BasisInformation/` + 신동공업 기준정보관리 화면  
> **대상 스택:** React (Vite) + Spring Boot 3.5 + MariaDB  
> **레거시 DB:** MSSQL `Sindong_ERP`

**관련 구현 문서 (v1.0, 2026-06-22):**

- [기준정보 구현 설계서](./basis-information-implementation-spec.md) ? Wave, PK/FK, 금지사항  
- [시스템정보 설계서](./system-information-spec.md) ? 공용코드·RBAC·마감  
- [재고·원장·구매 설계서](./inventory-ledger-spec.md)  
- [전체 구현 절차](../backend/docs/IMPLEMENTATION_PROCEDURE.md)  
- [문서 목록](./README.md)

---

## 목차

1. [문서 목적](#1-문서-목적)
2. [기준정보 도메인 개요](#2-기준정보-도메인-개요)
3. [plan / actual 운영 모델](#3-plan--actual-운영-모델)
4. [공통 API 규칙](#4-공통-api-규칙)
5. [도메인별 REST API 및 필드 매핑](#5-도메인별-rest-api-및-필드-매핑)
6. [React 화면·메뉴 설계](#6-react-화면메뉴-설계)
7. [마이그레이션 우선순위](#7-마이그레이션-우선순위)
8. [MariaDB 이관 주의사항](#8-mariadb-이관-주의사항)
9. [문서 이력](#9-문서-이력)

---

## 1. 문서 목적

본 문서는 KIT_ERP 기준정보 14개 도메인을 신규 풀스택으로 이관할 때 사용하는 **REST API 초안**, **화면↔DB 필드 매핑**, **plan/actual 이중 구조 운영 원칙**을 정의한다.

**운영 원칙 (한 줄):**

> **운영 SoT는 사용 품목구성·공정·작업표준(plan)이며, 진(actual)은 현실 대비 확인용 참고 데이터로 트랜잭션에 사용하지 않는다.**

---

## 2. 기준정보 도메인 개요

### 2.1 14개 도메인 매핑

| # | 업무 명칭 | 레거시 화면 | 테이블 | 비즈니스 키 |
|---|-----------|-------------|--------|-------------|
| 1 | 거래처 | `CompanyInfo.aspx` | `CI_MT` | `BusinessRegistrationNum` |
| 2 | 품목 | `ItemInfo.aspx` | `II_MT` | `ItemNum` |
| 3 | 품목구성 (사용) | `ItemOrganizationInfo.aspx` | `IOI_MT` | `ParentItemNum` + `ChildItemNum` |
| 4 | 작업장 | `WCInfo.aspx` | `WCI_MT` | `WCName` |
| 5 | 공정 (사용) | `ProcessSequenceInfo.aspx` | `PSI_MT` | `ItemNum` + `ProcessSequenceNum` |
| 6 | 설비 | `EquipmentInfo.aspx` | `EI_MT` | `EquipmentNum` |
| 7 | 작업표준 (사용) | `WorkStandardInfo.aspx` | `WSI_MT` | `ItemNum` + `ProcessSequenceNum` |
| 8 | 판매단가 | `SaleUnitCodeInfo.aspx` | `UCI_MT` | `UnitCostDistinction = '판매단가'` |
| 9 | 외주단가 | `OutSideOrderUnitCodeInfo.aspx` | `UCI_MT` | `UnitCostDistinction = '외주단가'` |
| 10 | 구매단가 | `BuyingUnitCodeInfo.aspx` | `UCI_MT` | `UnitCostDistinction = '구매단가'` |
| 11 | 사용자 | `UserInfo.aspx` | `UI_MT` | `ID` |
| 12 | 진품목구성 (실제) | `RealItemOrganizationInfo.aspx` | `RIOI_MT` | plan과 동일 구조 |
| 13 | 진공정 (실제) | `RealProcessSequenceInfo.aspx` | `RPSI_MT` | plan과 동일 구조 |
| 14 | 진작업표준 (실제) | `RealWorkStandardInfo.aspx` | `RWSI_MT` | plan과 동일 구조 |

### 2.2 plan / actual 테이블 대응

| 도메인 | variant=plan (운영) | variant=actual (확인용) | 이력 테이블 |
|--------|---------------------|-------------------------|-------------|
| 품목구성 | `IOI_MT` | `RIOI_MT` | `UPIOI_HT` 등 |
| 공정 | `PSI_MT` | `RPSI_MT` | 참고보기 |
| 작업표준 | `WSI_MT` | `RWSI_MT` | 참고보기 |

단가 3종은 `UCI_MT` 단일 테이블, `UnitCostDistinction` 값으로 구분한다.

### 2.3 레거시 공통 처리

- **소프트 삭제:** `RecodingState = 1` (사용) / `0` (삭제)
- **무기한 종료일:** `EndDate = '2076-06-06'` → API 응답 시 `null` 처리
- **CRUD 엔진:** `MasterInfoRecordRUD.cs` → Spring 도메인 Service로 분해
- **공용코드:** `PUC_MT` (`SystemInfoManagement/PublicUseCode`) ? 전 도메인 선행 인프라

---

## 3. plan / actual 운영 모델

### 3.1 업무 배경

품목별 공정·품목구성·작업표준이 **실제보다 훨씬 많고 복잡**하여, ERP에서는 **단순화한 plan**만 사용하고 **실제에 가까운 상세 데이터는 actual**에 보관·확인한다.

| 구분 | plan (사용) | actual (진) |
|------|-------------|-------------|
| 목적 | ERP **운영**용 단순화 기준 | **현실 반영** 확인·대조용 |
| 데이터량 | 적음 (단순화) | 많음 (상세) |
| 트랜잭션 | 영업·생산·구매·외주 **기준 SoT** | **사용 안 함** |
| 화면 | 일상 편집 (CRUD) | **조회·대조** 중심 |
| plan↔actual 동기화 | **없음** (의도적 단순화) | |

### 3.2 시스템 의존 관계

```
[영업 / 생산 / 구매 / 외주]  ──?  plan (IOI, PSI, WSI)  only

[진 메뉴]  ──?  actual (RIOI, RPSI, RWSI)  ──?  compare API로 plan과 대조
```

**금지:** 트랜잭션 모듈에서 `actual` API/테이블 참조.

### 3.3 API·화면 역할 분리

| 기능 | plan | actual |
|------|------|--------|
| 목록·상세 조회 | ● | ● |
| 등록·수정·삭제 | ● 일상 업무 | △ 관리자만 또는 1차 생략 |
| BOM 전개·역전개 | ● 운영용 | ● 확인용 |
| plan↔actual 대조 | - | ● **핵심** |
| actual→plan 복사 | **비권장** | - |
| 품목 간 복사 | ● (plan) | 선택 |

### 3.4 대조 API (actual 메뉴 핵심)

```
GET /api/v1/basis/item-composition/compare?itemNum={itemNum}
GET /api/v1/basis/processes/compare?itemNum={itemNum}
GET /api/v1/basis/work-standards/compare?itemNum={itemNum}
```

응답 개념:

```json
{
  "itemNum": "A-001",
  "planCount": 12,
  "actualCount": 47,
  "onlyInPlan": [],
  "onlyInActual": [],
  "differences": []
}
```

---

## 4. 공통 API 규칙

### 4.1 Base URL

```
/api/v1/basis
```

### 4.2 공통 규칙

| 항목 | 규칙 |
|------|------|
| 인증 | `Authorization: Bearer {token}` |
| 목록 | `page`, `size`, `sort` |
| 삭제 | `RecodingState = 0` 소프트 삭제 |
| 감사 필드 | `registrationPerson`, `registrationPersonId`, `registrationDate`, `updatingPerson`, `updatingPersonId`, `updatingDate` |
| PK (API 통일) | 레거시 `*InfoIndex` → 응답 필드 `id` |

### 4.3 공통 조회 API

| Method | Endpoint | 설명 |
|--------|----------|------|
| GET | `/api/v1/basis/items/search` | 품목 검색 |
| GET | `/api/v1/basis/companies/search` | 거래처 검색 |
| GET | `/api/v1/basis/work-centers/search` | 작업장 검색 |
| GET | `/api/v1/basis/public-codes` | 공용코드 (`PUC_MT`) |
| GET | `/api/v1/basis/users/search` | 사용자 검색 |

### 4.4 variant path 규칙

품목구성·공정·작업표준은 URL path에 variant를 포함한다.

```
/api/v1/basis/item-composition/{variant}/...
/api/v1/basis/processes/{variant}/...
/api/v1/basis/work-standards/{variant}/...

variant: plan | actual
```

---

## 5. 도메인별 REST API 및 필드 매핑

### 5.1 거래처 (CI_MT)

**화면:** `CompanyInfo.aspx`

| Method | Endpoint | 설명 |
|--------|----------|------|
| GET | `/api/v1/basis/companies` | 목록·검색 |
| GET | `/api/v1/basis/companies/{id}` | 상세 |
| GET | `/api/v1/basis/companies/by-brn/{brn}` | 사업자번호 조회 |
| POST | `/api/v1/basis/companies` | 등록 |
| PUT | `/api/v1/basis/companies/{id}` | 수정 |
| DELETE | `/api/v1/basis/companies/{id}` | 삭제 |
| GET | `/api/v1/basis/companies/{id}/references` | 참고보기 |

| 화면 라벨 | 필수 | API 필드 | DB 컬럼 | 비고 |
|-----------|:----:|----------|---------|------|
| 수주거래처 | ● | `receivingOrderCompany` | `ReceiveingOrderCompany` | 4구분 중 1개 이상 |
| 외주거래처 | ● | `outsourcingCompany` | `OutSideOrderCompany` | |
| 구매거래처 | ● | `buyingCompany` | `BuyingCompany` | |
| 비용거래처 | ● | `costCompany` | `CostCompany` | |
| 거래처명 | ● | `companyName` | `CompanyName` | |
| 대표자명 | ● | `presidentName` | `PresidentName` | |
| 사업자번호 | ● | `businessRegistrationNum` | `BusinessRegistrationNum` | UK |
| 법인번호 | | `corporationRegistrationNum` | `CorporationRegistrationNum` | |
| 사업장주소 | ● | `businessAddress` | `BusinessCompanyAddress` | |
| 계산서주소 | ● | `taxBillAddress` | `TaxBillAddress` | |
| Homepage | | `homepageAddress` | `HomepageAddress` | |
| 업태 / 종목 | | `businessClassification` / `businessItem` | 동명 | |
| 거래상태 | | `currentTradeState` | `CurrentTradeState` | PUC |
| 부가세처리 / 세율 | | `supplementaryValueTax` / `supplementaryValueTaxRate` | 동명 | 기본 10% |
| 전화 / 팩스 | | `telephoneNum` / `faxNum` | 동명 | |
| 분류1~3 | | `tradeClassification1~3` | 동명 | PUC |
| 판매기준일 / 어음일 | | `saleStandardDate` / `saleStandardBillDate` | 동명 | |
| 어음결제기준 | | `billApprovalStandard` | `BillApprovalStandard` | |
| 정기수금일1·2 | | `fixPeriodCollectMoneyDate1~2` | 동명 | |
| 거래처담당자 | | `companyPersonInCharge` | `CompanyPersonInCharge` | |
| E-Mail | | `email` | `Email` | |

등록 시 `MasterInfoRecordRUD`가 매입매출 테이블(`BSI_MT` 등) 자동 생성 ? Spring에서 동일 후처리 필요.

---

### 5.2 품목 (II_MT)

**화면:** `ItemInfo.aspx`

| Method | Endpoint | 설명 |
|--------|----------|------|
| GET | `/api/v1/basis/items` | 목록·검색 |
| GET | `/api/v1/basis/items/{itemNum}` | 상세 |
| POST | `/api/v1/basis/items` | 등록 |
| PUT | `/api/v1/basis/items/{itemNum}` | 수정 |
| DELETE | `/api/v1/basis/items/{itemNum}` | 삭제 |
| PATCH | `/api/v1/basis/items/batch` | 일괄수정 |
| POST | `/api/v1/basis/items/import` | Excel 일괄등록 |

| 화면 라벨 | 필수 | API 필드 | DB 컬럼 |
|-----------|:----:|----------|---------|
| 품목번호 | ● | `itemNum` | `ItemNum` |
| 도면번호 | | `itemDrawNum` | `ItemDrawNum` |
| 품목명 | | `itemName` | `ItemName` |
| 자산분류 | ● | `propertyClassification` | `PropertyClassification` |
| 단위 | ● | `unit` | `Unit` |
| 규격 | | `standard` | `Standard` |
| 부가세율 | | `supplementaryValueTaxRate` | `SupplementaryValueTaxRate` |
| 재고/BOM/구매/판매단위 | | `stockUnit`, `bomUnit`, `purchaseUnit`, `saleUnit` | 동명 |
| 자재산출 | ● | `ioCheckable` | `IOChackable` |
| 재고관리여부 | | `stockManagable` | `StockManagable` |
| 검사구분 | ● | `checkDistinction` | `CheckDistinction` |
| 발주방침 | | `orderPlan` | `OrderPlan` |
| 품목타입 / 재질 | | `itemType`, `materalQuality` | 동명 |
| 품목상태 | ● | `itemState` | `ItemState` |
| 품목분류1~4 | ●①②③ | `itemClassification1~4` | 동명 |
| 규격1~4 / 단위1~4 | | `standard1~4`, `unit1~4` | 동명 |
| 제품·소재중량 | | `productWeight`, `materialWeight` | 동명 |
| 조달기간 / 완성리드타임 | | `supplyTerm`, `leadTime` | 동명 |
| 내외자구분 | | `domesticImportDistinction` | `DomesticImportDistinction` |
| 안전재고 / 발주간격 / 최소발주 | | `safetyStockQuantity` 등 | 동명 |
| 열처리·경화깊이 등 | | `stiffenDeep` 등 | 동명 |
| 관세율 | | `tariffRate` | `TariffRate` |
| 기준단가 | ● | `standardUnitCost` | `StandardUnitCost` |
| 주구입처/주외주처/주판매처 | | `mainPurchaseCompany` 등 | 동명 |
| 담당자 | | `chargePerson` | `ChargePerson` |

자산분류에 따라 창고 테이블 자동 생성 (원자재→원자재창고, 제품/상품→영업·납품창고 등).

---

### 5.3 품목구성 (IOI_MT / RIOI_MT)

**화면:** `ItemOrganizationInfo.aspx` / `RealItemOrganizationInfo.aspx` (React **동일 컴포넌트**)

| Method | Endpoint | plan | actual |
|--------|----------|:----:|:------:|
| GET | `/api/v1/basis/item-composition/{variant}` | ● | ● |
| GET | `/api/v1/basis/item-composition/{variant}/{parentItemNum}` | ● | ● |
| POST | `/api/v1/basis/item-composition/{variant}` | ● | △ |
| PUT | `/api/v1/basis/item-composition/{variant}/{id}` | ● | △ |
| DELETE | `/api/v1/basis/item-composition/{variant}/{id}` | ● | △ |
| GET | `.../{variant}/{itemNum}/explosion` | ● | ● |
| GET | `.../{variant}/{itemNum}/reverse` | ● | ● |
| GET | `.../{variant}/{itemNum}/materials` | ● | ● |
| GET | `.../{variant}/{id}/history` | ● | ● |
| POST | `.../plan/copy` | ● | - |
| GET | `/api/v1/basis/item-composition/compare` | - | ● |

| 화면 라벨 | 필수 | API 필드 | DB 컬럼 |
|-----------|:----:|----------|---------|
| 모품목번호 | ● | `parentItemNum` | `ParentItemNum` |
| 자품목번호 | ● | `childItemNum` | `ChildItemNum` |
| 소요량분자 | ● | `needQuantityNumerator` | `NeedQuantityNumerator` |
| 소요량분모 | ● | `needQuantityDenominator` | `NeedQuantityDenominator` |
| 공정관리 | | `processManagement` | `ProcessManagement` | 1=예 |
| 하위구분 | | `subDivision` | `SubDivision` | |
| 조달구분 | | `supplyDivision` | `SupplyDivision` | |
| BOM단위 | | `bomUnit` | `BOMUnit` |
| 적용시작일 | ● | `beginDate` | `BeginDate` |
| 적용종료일 | | `endDate` | `EndDate` |
| 변경사유 | | `updateReason` | 이력 `UPIOI_HT` |

---

### 5.4 작업장 (WCI_MT)

**화면:** `WorkCentersPage` — `/basis/work-centers`  
**상세 스펙:** [basis-work-center-spec.md](./basis-work-center-spec.md) (B1-R)

| Method | Endpoint |
|--------|----------|
| GET/POST/PUT/DELETE | `/api/v1/basis/work-centers` |

| 화면 라벨 | 필수 | API 필드 | DB 컬럼 |
|-----------|:----:|----------|---------|
| 작업장명 | ● | `wcName` | `wc_name` |
| 대표공정 | ● | `mainProcessCode` | `main_process_code` |
| 일일 가동(분) | ● | `operationTime` | `operation_time` (INT) |

> B1-R: 3필드 Modal. Capa·생산달력 — [생산달력 스펙](./basis-production-calendar-spec.md). **B5-R:** 작업장 등록 시 달력 **복사 없음**.

---

### 5.4a 기본생산달력 (BPC_T)

**화면:** `ProductionCalendarsPage` — `/basis/production-calendars`  
**상세 스펙:** [basis-production-calendar-spec.md](./basis-production-calendar-spec.md) (B5-R)

| Method | Endpoint |
|--------|----------|
| GET | `/api/v1/basis/production-calendars?year=&month=` |
| GET | `/api/v1/basis/production-calendars/{id}` |
| PUT | `/api/v1/basis/production-calendars/by-date/{calendarDate}` |
| DELETE | `/api/v1/basis/production-calendars/by-date/{calendarDate}` |

| 화면 라벨 | 필수 | API 필드 (요청) | API 필드 (응답) | DB (`production_calendar`) |
|-----------|:----:|-----------------|-----------------|----------------------------|
| (시스템) | | — | `id` | `id` |
| 일자 | ● | `calendarDate` | `calendarDate` | `calendar_date` (UK) |
| 가동시간(분) | ● | `workTime` | `workTime` | `work_time` INT, 기본 480 |
| 비고 | | `content` | `content` | `content` |

---

### 5.4b 작업장별 생산달력 — Override (WCPC_T)

**화면:** `WorkCenterCalendarsPage` — `/basis/work-center-calendars`  
**상세 스펙:** [basis-production-calendar-spec.md](./basis-production-calendar-spec.md) §3.2·§6.2 (B5-R)

| Method | Endpoint |
|--------|----------|
| GET | `/api/v1/basis/work-center-calendars/overrides?workCenterId=&year=&month=` |
| GET | `/api/v1/basis/work-center-calendars/effective?workCenterId=&year=&month=` |
| PUT | `/api/v1/basis/work-center-calendars/overrides` |
| DELETE | `/api/v1/basis/work-center-calendars/overrides?workCenterId=&calendarDate=` |

| 화면 라벨 | 필수 | API 필드 (요청) | API 필드 (응답) | DB (`work_center_calendar`) |
|-----------|:----:|-----------------|-----------------|----------------------------|
| (시스템) | | — | `id` | `id` |
| 작업장 | ● | `workCenterId` | `workCenterId`, `wcName` | `work_center_id` (FK) |
| 일자 | ● | `calendarDate` | `calendarDate` | `calendar_date` |
| 가동시간(분) | ● | `workTime` | `workTime` | `work_time` INT |
| 비고 | | `content` | `content` | `content` |

**effective 응답:** `effectiveWorkTime`, `isOverride`, `baseWorkTime`.

> B5-R: **sparse Override** — UK `(work_center_id, calendar_date)`. `work_time` **분**. Override 없으면 §5.4a 상속. **Deprecated:** `GET .../standard`, `wcName` 요청, year/month/day 분리 UK.

---

### 5.5 공정 (PSI_MT / RPSI_MT)

**화면:** `ProcessSequenceInfo.aspx` / `RealProcessSequenceInfo.aspx` (React **동일 컴포넌트**)  
**상세 스펙:** [basis-process-sequence-spec.md](./basis-process-sequence-spec.md)

| Method | Endpoint | plan | actual |
|--------|----------|:----:|:------:|
| GET | `/api/v1/basis/processes/{variant}/{itemNum}` | ● | ● |
| GET | `/api/v1/basis/processes/{variant}?itemNum=` | ● (B2-R) | ● (B2-R) |
| POST/PUT/DELETE | `/api/v1/basis/processes/{variant}/{id}` | ● | △ |
| POST | `.../plan/copy` | ● | - |
| GET | `/api/v1/basis/processes/compare?itemNum=` | - | ● |

| 화면 라벨 | 필수 | API 필드 (요청) | API 필드 (응답) | DB 컬럼 |
|-----------|:----:|-----------------|-----------------|---------|
| (시스템) | | — | **`id`** | **`id`** (PK) |
| 품목 | ● | `itemId` | `itemId`, `itemNum`, `itemName` | `item_id` (FK) |
| 순서번호 | ● | `processSequenceNum` | `processSequenceNum` | `process_sequence` | 99 금지 |
| 공정 | ● | `processCodeId` | `processCodeId`, `processCode`, `processName` | `public_code_id` (FK) |
| 작업구분 | ● | `workDistinction` | `workDistinction` | `work_distinction` |
| 작업장 | ●* | `workCenterId` | `workCenterId`, `wcName` | `work_center_id` (FK) | *외주 시 null |
| 발주비율 | ● | `outsideOrderRate` | `outsideOrderRate` | `outside_order_rate` |
| 진척비율 | ● | `progressRate` | `progressRate` | `progress_rate` |
| 리드타임 | | `leadTime` | `leadTime` | `lead_time` |
| 기타 | | `etcText` | `etcText` | `etc_text` |

> B2-R: PK **`id`**. **요청** `itemId`·`processCodeId`·`workCenterId`. **응답** `id`. 작업표준 FK → `process_sequence.id`. [공정순서 스펙](./basis-process-sequence-spec.md) §3.3.

---

### 5.6 설비 (EI_MT)

**화면:** `EquipmentInfo.aspx`  
**상세 스펙:** [basis-equipment-spec.md](./basis-equipment-spec.md) (B5-R 슬림 8필드)

| Method | Endpoint |
|--------|----------|
| GET/POST/PUT/DELETE | `/api/v1/basis/equipment` |

목록 `GET` — 선택 쿼리 `?q=` (설비번호·설비명 부분 일치).

| 화면 라벨 | 필수 | API 필드 (요청) | API 필드 (응답) | DB 컬럼 |
|-----------|:----:|-----------------|-----------------|---------|
| 설비번호 | ● | `equipmentNum` | `equipmentNum` | `equipment_num` |
| 설비명 | ● | `equipmentName` | `equipmentName` | `equipment_name` |
| 설비분류 | ● | `equipmentClassificationId` | `equipmentClassificationId`, `equipmentClassificationName`, `smallCode` | `equipment_category_id` (FK → `public_code.id`) |
| 작업장 | | `workCenterId` | `workCenterId`, `wcName` | `work_center_id` (FK, NULL 허용) |
| 설계샷 | ● | `designShot` | `designShot` | `design_shot` |
| 초기샷 | ● | `initialShot` | `initialShot` | `initial_shot` |
| 작업샷 | | — (읽기 전용) | `workShot` | `work_shot` |
| 누계샷 | | — (읽기 전용) | `accumulatedShot` | `accumulated_shot` |
| 교체 필요 | | — | `replacementDue` | (연산) |

> B5-R에서 레거시 20+ 컬럼(`equipmentClassification` 문자열, `wcName` 직접 입력, `equipmentState` 등) **제거**. 설비분류 콤보는 `usageType=EQUIPMENT` 공용코드 **소분류명** 표시, **`public_code.id` FK** 저장.

---

### 5.7 작업표준 (WSI_MT / RWSI_MT)

**화면:** `WorkStandardInfo.aspx` / `RealWorkStandardInfo.aspx` (React **동일 컴포넌트**)  
**상세 스펙:** [basis-work-standard-spec.md](./basis-work-standard-spec.md) (B2-R 슬림)

| Method | Endpoint | plan | actual |
|--------|----------|:----:|:------:|
| GET | `/api/v1/basis/work-standards/{variant}/{itemNum}` | ● | ● |
| GET | `/api/v1/basis/work-standards/{variant}?itemNum=` | ● (B2-R) | ● (B2-R) |
| POST | `/api/v1/basis/work-standards/{variant}` | ● | △ |
| PUT | `/api/v1/basis/work-standards/{variant}/{id}` | ● | △ |
| DELETE | `/api/v1/basis/work-standards/{variant}/{id}` | ● | △ |
| POST | `.../plan/copy` | ● | - |
| GET | `/api/v1/basis/work-standards/compare` | - | ● |

| 화면 라벨 | 필수 | API 필드 (요청) | API 필드 (응답) | DB 컬럼 |
|-----------|:----:|-----------------|-----------------|---------|
| (시스템) | | — | **`id`** | **`id`** (PK) |
| 품목 | ● | `itemId` | `itemId`, `itemNum`, `itemName` | `item_id` (FK) |
| 공정 | ● | `processSequenceId` | `processSequenceId`, `processSequenceNum`, `processCode`, `processName` | `process_sequence_id` (FK → **`process_sequence.id`**) |
| 작업장 | ● | `workCenterId` | `workCenterId`, `wcName` | `work_center_id` (FK) |
| 사용설비 | | `equipmentId` | `equipmentId`, `equipmentName` | `equipment_id` (FK, NULL) |
| 우선순위 | ● | `priorityOrder` | `priorityOrder` | `priority_order` |
| 주작업자 | | `mainWorkerId` | `mainWorkerId`, `mainWorkerName` | `main_worker_user_id` (FK, NULL) |
| 사용공구 | | `toolName` | `toolName` | `tool_name` |
| 셋업시간 | ● | `setupTime` | `setupTime` | `setup_time` (INT, **분**) |
| 표준시간 | ● | `standardTime` | `standardTime` | `standard_time` (INT, **초**) |

> B2-R: PK **`id`**. UK `(item_id, process_sequence_id, priority_order)`. PUT/DELETE **`/{id}`**. [작업표준 스펙](./basis-work-standard-spec.md) §3.2.

---

### 5.8 단가 (UCI_MT / UPUCI_HT)

**화면:** `UnitCostsPage` — `/basis/unit-costs` (3탭 통합)  
**상세 스펙:** [basis-unit-cost-spec.md](./basis-unit-cost-spec.md) (B3-R)

| Method | Endpoint |
|--------|----------|
| GET | `/api/v1/basis/unit-costs?type=SALE\|PURCHASE\|OUTSOURCE` |
| GET | `/api/v1/basis/unit-costs/{id}` |
| GET | `/api/v1/basis/unit-costs/by-item/{itemNum}?type=` |
| POST | `/api/v1/basis/unit-costs` |
| PUT | `/api/v1/basis/unit-costs/{id}` |
| DELETE | `/api/v1/basis/unit-costs/{id}` |
| GET | `/api/v1/basis/unit-costs/{id}/history` |

| API `type` | DB `cost_type` | 레거시 |
|------------|----------------|--------|
| `SALE` | `SALE` | 판매단가 |
| `PURCHASE` | `PURCHASE` | 구매단가 |
| `OUTSOURCE` | `OUTSOURCE` | 외주단가 |

| 화면 라벨 | 필수 | API 필드 (요청) | API 필드 (응답) | DB 컬럼 |
|-----------|:----:|-----------------|-----------------|---------|
| (시스템) | | — | **`id`** | `id` (PK) |
| 단가 구분 | ● | `type` | `type` | `cost_type` |
| 품목 | ● | `itemId` | `itemId`, `itemNum`, `itemName`, `propertyClassification` | `item_id` (FK) |
| 거래처 | ● | `companyId` | `companyId`, `companyName`, `businessRegistrationNum` | `company_id` (FK) |
| 시작공정 | ●* | `beginProcessCodeId` | `beginProcessCodeId`, `processCode`, `processName` | `begin_process_code_id` (FK) |
| 종료공정 | ●* | `endProcessCodeId` | `endProcessCodeId`, … | `end_process_code_id` (FK) |
| 발주비율 | ●* | `orderRate` | `orderRate` | `order_rate` |
| 기준단가 | ● | `standardUnitCost` | `standardUnitCost` | `standard_unit_cost` |
| 할인단가 | | `discountUnitCost` | `discountUnitCost` | `discount_unit_cost` |
| 적용시작일 | ● | `beginDate` | `beginDate` | `begin_date` |
| 적용종료일 | | `endDate` | `endDate` | `end_date` |
| 변경 사유 | ●** | `updateReason` | — | `unit_cost_history.update_reason` |

\* **외주단가만** 시작·종료 공정 FK 필수. 판매·구매는 NULL.  
\** **PUT(수정)만** 필수. POST 무시.

> B3-R: PK **`id`**. 요청 **`itemId`·`companyId`**. 외주 **`beginProcessCodeId`/`endProcessCodeId`** → `public_code.id`. 발주비율·자산분류·락·변경사유 — [단가 스펙](./basis-unit-cost-spec.md) §5. **외주창고 자동 연동 금지.**

---

### 5.9 사용자 (UI_MT / app_user)

**화면:** `UsersPage` — `/basis/users`  
**상세 스펙:** [basis-user-spec.md](./basis-user-spec.md) (B0-R)

| Method | Endpoint |
|--------|----------|
| GET | `/api/v1/basis/users` |
| GET | `/api/v1/basis/users?q=` |
| GET | `/api/v1/basis/users/{id}` |
| GET | `/api/v1/basis/users/check-login-id?loginId=` |
| POST | `/api/v1/basis/users` |
| PUT | `/api/v1/basis/users/{id}` |
| DELETE | `/api/v1/basis/users/{id}` |

| # | 화면 라벨 | 필수 | API 필드 (요청) | API 필드 (응답) | DB |
|---|-----------|:----:|-----------------|-----------------|-----|
| — | (시스템) | | — | `id` | `id` |
| 1 | 로그인 아이디 | ● | `loginId` | `loginId` | `login_id` (UK) |
| 2 | 비밀번호 | ●* | `password` | — | `password_hash` (bcrypt) |
| 3 | 사원명 | ● | `name` | `name` | `name` |
| 4 | 연락처 | | `contact` | `contact` | `contact` |
| 5 | 사용권한 | ● | `roleIds` | `roleIds`, `roleCodes` | `user_role` |
| 6 | 업무일지그룹 | | `workDiaryGroupId` | `workDiaryGroupId`, `workDiaryGroupName` | `work_diary_group_id` (FK) |
| 7 | 이메일 | | `email` | `email` | `email` |

\* POST `password` 필수. PUT blank → 유지.

> B0-R: 7필드 슬림. 권한은 **RBAC `roleIds`** ([system-information-spec §3.6](./system-information-spec.md)). `workDiaryGroupId` → `public_code` (`usage_type=WORK_DIARY_GROUP`). **Deprecated:** 부서·직책·주민번호·photo API.

---

## 6. React 화면·메뉴 설계

### 6.1 메뉴 (6개) ↔ 화면 (3개 공용)

| 메뉴 | Route | 컴포넌트 | variant |
|------|-------|----------|---------|
| 품목 구성 정보 | `/basis/item-composition/plan` | `ItemCompositionPage` | plan |
| 진품목구성정보 | `/basis/item-composition/actual` | 동일 | actual |
| 공정 정보 | `/basis/processes/plan` | `ProcessPage` | plan |
| 진공정정보 | `/basis/processes/actual` | 동일 | actual |
| 작업표준 정보 | `/basis/work-standards/plan` | `WorkStandardPage` | plan |
| 진작업표준정보 | `/basis/work-standards/actual` | 동일 | actual |

### 6.2 variant별 UI 차이

| UI | plan | actual |
|----|------|--------|
| 상단 배지 | `운영 기준` | `확인용 (실제)` |
| 등록/수정/삭제 | 표시 | 기본 숨김 (관리자만) |
| BOM 전개·역전개 | ● | ● |
| plan과 비교 | - | ● |
| React Query cache key | `['item-composition', 'plan', ...]` | `['item-composition', 'actual', ...]` |

### 6.3 Spring Boot 패키지 (권장)

```
com.kit.erp.basis
├── company          (CI_MT)
├── item             (II_MT)
├── workcenter       (WCI_MT)
├── equipment        (EI_MT)
├── user             (UI_MT)
├── itemcomposition  (IOI_MT / RIOI_MT)  ← variant 전략
├── process          (PSI_MT / RPSI_MT)
├── workstandard     (WSI_MT / RWSI_MT)
├── unitcost         (UCI_MT)
└── publiccode       (PUC_MT)
```

---

## 7. 마이그레이션 우선순위

### 7.1 필수 모듈 (프로젝트 범위)

기준정보, 영업, 생산, 구매, 외주, 백업, 게시판, 업무일지

### 7.2 기준정보 Wave

| Wave | 내용 |
|------|------|
| B0 | 공용코드(`PUC_MT`) + 사용자 + 로그인 |
| B1 | 거래처, 품목, 작업장 |
| B2 | 설비, **plan** 품목구성·공정·작업표준 |
| B3 | 판매·구매·외주 단가 |
| B4 | **actual** 3종 조회 + compare API |
| B5 | actual 제한 편집, Copy·이력 |

### 7.3 도메인 의존 순서

```
PUC → 사용자 → 거래처 → 품목 → 작업장 → 설비
  → plan(품목구성·공정·작업표준) → 단가
  → actual(조회·대조)
```

영업·생산·구매·외주 개발 시 **plan 기준정보만** 선행 완료하면 된다.

---

## 8. MariaDB 이관 주의사항

| 항목 | 내용 |
|------|------|
| 문자셋 | EUC-KR → `utf8mb4` |
| 무기한 날짜 | `2076-06-06` DTO 변환 규칙 통일 |
| 예약어 | `` `Index` `` 등 백틱 |
| IDENTITY | `AUTO_INCREMENT` + `LAST_INSERT_ID()` |
| 비밀번호 | `UI_MT.Password` → bcrypt |
| plan/actual | 6테이블 모두 이관, **FK·조인은 plan만** |
| 트랜잭션 후처리 | 거래처→매입매출, 품목→창고, 작업장→달력, 단가→이력 |

---

## 9. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-06-22 | 14개 도메인 API·필드 매핑 초안 |
| 1.1 | 2026-06-22 | plan/actual 통합 화면, actual=확인용·compare API, 운영 SoT 명시 |
| 1.2 | 2026-06-22 | §5.6 설비 B5-R 슬림 — `equipmentClassificationId` FK, `workCenterId`, 샷 3컬럼 |
| 1.3 | 2026-06-24 | §5.7 작업표준 B2-R — `itemId`·`processId` FK, UK, 작업일보 priority 대입 |
| 1.4 | 2026-06-24 | §5.5 공정순서 B2-R — POST/PUT·복사 `itemId`, `sourceItemId`/`targetItemId` |
| 1.5 | 2026-06-24 | §5.5 공정순서 — `workCenterId` 콤보 FK (`wcName` 응답·표시만) |
| 1.6 | 2026-06-24 | §5.5 — 요청 `processCodeId` FK; 응답 `processId`; UK `public_code_id` |
| 1.7 | 2026-06-24 | §5.5·§5.7 — PK `process_id`; 작업표준 FK `process_sequence.process_id` |
| 1.8 | 2026-06-24 | §5.7 — 작업표준 PK `id`; PUT/DELETE `/{id}` |
| 1.9 | 2026-06-24 | §5.7 — PK **`work_standard_id`** (→ v1.10에서 **`id`** 환원) |
| 1.10 | 2026-06-24 | §5.5·§5.7 — PK **`id`** 통일; 작업표준 FK **`process_sequence_id`** |
| 1.11 | 2026-06-24 | §5.8 단가 — [basis-unit-cost-spec.md](./basis-unit-cost-spec.md) 연동; `itemId`·공정 FK id |
| 1.12 | 2026-06-24 | §5.4·§5.4a·§5.4b 생산달력 — [basis-production-calendar-spec.md](./basis-production-calendar-spec.md); Override·분 |
| 1.13 | 2026-06-24 | §5.9 사용자 — [basis-user-spec.md](./basis-user-spec.md); 7필드·RBAC·check-login-id |

---

## 부록: 레거시 화면 ↔ 신규 매핑 요약

| 신규 React Route | 레거시 ASPX | 테이블 |
|------------------|-------------|--------|
| `/basis/companies` | `CompanyInfo.aspx` | `CI_MT` |
| `/basis/items` | `ItemInfo.aspx` | `II_MT` |
| `/basis/item-composition/plan` | `ItemOrganizationInfo.aspx` | `IOI_MT` |
| `/basis/item-composition/actual` | `RealItemOrganizationInfo.aspx` | `RIOI_MT` |
| `/basis/work-centers` | `WCInfo.aspx` | `WCI_MT` |
| `/basis/production-calendars` | (BPC_T) | `production_calendar` |
| `/basis/work-center-calendars` | (WCPC_T Override) | `work_center_calendar` |
| `/basis/processes/plan` | `ProcessSequenceInfo.aspx` | `PSI_MT` |
| `/basis/processes/actual` | `RealProcessSequenceInfo.aspx` | `RPSI_MT` |
| `/basis/equipment` | `EquipmentInfo.aspx` | `EI_MT` |
| `/basis/work-standards/plan` | `WorkStandardInfo.aspx` | `WSI_MT` |
| `/basis/work-standards/actual` | `RealWorkStandardInfo.aspx` | `RWSI_MT` |
| `/basis/unit-costs` (3탭: SALE/PURCHASE/OUTSOURCE) | `SaleUnitCodeInfo` / `BuyingUnitCodeInfo` / `OutSideOrderUnitCodeInfo` | `UCI_MT` |
| `/basis/users` | `UserInfo.aspx` | `UI_MT` |
