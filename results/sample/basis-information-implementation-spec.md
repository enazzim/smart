# KIT_ERP 기준정보 구현 설계서

> **문서 버전:** 1.0  
> **작성일:** 2026-06-22  
> **대상 스택:** Spring Boot 3.5 + MariaDB + React (Vite)  
> **레거시:** `BasisInformation/`, `MasterInfoRecordRUD.cs`  
> **관련 문서:**  
> - [기준정보 API·필드 매핑](./basis-information-api-spec.md) ? REST·필드 상세  
> - [시스템정보 설계서](./system-information-spec.md) ? S0 선행  
> - [재고·원장·구매](./inventory-ledger-spec.md)  
> - [전체 구현 절차](../backend/docs/IMPLEMENTATION_PROCEDURE.md)

---

## 1. 문서 목적

기준정보 14도메인을 **구현 순서**, **아키텍처**, **키 설계**, **금지사항** 관점에서 정의한다.  
API 필드 상세는 `basis-information-api-spec.md`를 따른다.

**운영 원칙:**

> plan(IOI, PSI, WSI) = 운영 SoT. actual(RIOI, RPSI, RWSI) = 확인·대조만. 트랜잭션은 plan만 참조.

---

## 2. 도메인 목록 (14)

| # | 도메인 | variant | 레거시 | 비즈니스 UK (내용키) |
|---|--------|---------|--------|----------------------|
| 1 | 거래처 | ? | CI_MT | `business_registration_num` |
| 2 | 품목 | ? | II_MT | `item_num` |
| 3 | 품목구성 | plan | IOI_MT | `parent_item_id` + `child_item_id` |
| 4 | 작업장 | ? | WCI_MT | `wc_name` |
| 5 | 공정순서 | plan | PSI_MT | `item_id` + `process_sequence` |
| 6 | 설비 | ? | EI_MT | `equipment_num` |
| 7 | 작업표준 | plan | WSI_MT | `item_id` + `process_sequence` + `priority_order` |
| 8 | 판매단가 | ? | UCI_MT | distinction + item + partner + 기간 (+ 외주 공정구간) |
| 9 | 외주단가 | ? | UCI_MT | 동일 |
| 10 | 구매단가 | ? | UCI_MT | 동일 |
| 11 | 사용자 | ? | UI_MT | `login_id` |
| 12 | 품목구성 | actual | RIOI_MT | plan과 동일 구조 |
| 13 | 공정순서 | actual | RPSI_MT | plan과 동일 |
| 14 | 작업표준 | actual | RWSI_MT | plan과 동일 |

### 2.1 용어

| 용어 | 의미 | 예 |
|------|------|-----|
| 품목구성 (BOM) | 모품목?자품목?소요량 | A = 볼트 + 판재 |
| 공정순서 (라우팅) | 품목이 거치는 작업 순서 | A: 절단(10)→밴딩(20)→피막(30) |

---

## 3. PK·FK·내용키 설계 (필수)

### 3.1 공통 규칙

| 구분 | 신규 | 레거시 |
|------|------|--------|
| **PK** | 모든 테이블 `id` (BIGINT AUTO_INCREMENT) | `*InfoIndex` |
| **FK** | `{entity}_id` 로만 참조 | `ItemNum`, `BusinessRegistrationNum` 문자열 조인 |
| **내용키** | **UNIQUE 제약** + 검색·Import·화면 표시 | `m_KeyFieldName[]` |
| **API 상세** | `/resources/{id}` | Index 기반 |
| **API 검색** | `?itemNum=`, `by-brn/` 보조 | 내용키 직접 PK 사용 ? |

### 3.2 도메인별 PK·UK

| 테이블 | PK | UK (내용키) |
|--------|-----|-------------|
| `company` | `id` | `business_registration_num` |
| `item` | `id` | `item_num` |
| `item_composition` | `id` | `(parent_item_id, child_item_id)` |
| `work_center` | `id` | `wc_name` |
| `production_calendar` | `id` | `calendar_date` |
| `work_center_calendar` | `id` | `(work_center_id, calendar_date)` — Override sparse |
| `process_sequence` | `id` | `(item_id, public_code_id, process_sequence)` |
| `work_standard` | `id` | `(item_id, process_sequence_id, priority_order)` |
| `equipment` | `id` | `equipment_num` |
| `unit_cost` | `id` | 복합 UK (구분·품목·거래처·기간·외주공정) |
| `app_user` | `id` | `login_id` |

### 3.3 공정 키 (계층별 구분)

레거시 `m_KeyFieldName[4]`는 **`ItemNum` + `ProcessSequenceNum`** 2개만 사용한다.  
`ProcessCode`는 행 속성(PUC 참조)이며 마스터 UK에는 포함하지 않는다.

| 계층 | UK / 식별 | 비고 |
|------|-----------|------|
| **공정 마스터** `process_sequence` | `(item_id, public_code_id, process_sequence)` | B2-R UK |
| | `id` | PK (AUTO_INCREMENT). API `id` |
| | `public_code_id` | PROCESS 소분류 FK |
| **WIP 재고** `inventory_balance` | `(item_id, WIP, fiscal_year, process_sequence, process_code)` | 현장·생산창고 식별 |
| **작업표준** | **`work_standard.id`** (PK) | UK `(item_id, process_sequence_id, priority_order)` |
| | `work_standard.process_sequence_id` | FK → **`process_sequence.id`** |

**작업표준** — PK **`id`**. 작업지시·작업일보 FK 컬럼명 **`work_standard_id`** → **`work_standard.id`** 참조. [작업표준 스펙](./basis-work-standard-spec.md) §3.2·§5.5.

### 3.4 API 요청 예

```json
{
  "parentItemId": 101,
  "childItemId": 205,
  "needQuantityNumerator": 1,
  "needQuantityDenominator": 1
}
```

Import Excel은 `itemNum` 컬럼 허용 → 서버에서 `item.id` resolve 후 저장.

---

## 4. 레거시 대비 핵심 변경

| 레거시 (`MasterInfoRecordRUD`) | 신규 |
|--------------------------------|------|
| 거래처 등록 → `BSI_MT` INSERT | `CompanyRegisteredEvent` → ledger account만 |
| 품목 등록 → RMS/BS/DS/PS INSERT | **품목 등록 시 창고 생성 안 함** |
| 공정 등록 → `PS_MT` INSERT | `ProcessDefinedEvent` → WIP ensure (Lazy 가능) |
| 외주단가 → `OS_MT` BOM역추적 | **단가만 저장** |
| 더미 공정 14009999/14000000 | `inventory_location` 코드 |
| 연말 SP + 작년 DELETE | Lazy + `fiscal_year_opening` + 아카이브 |
| actual → 트랜잭션 | **금지** |
| 문자열 내용키 FK | `*_id` FK |

---

## 5. Backend 구조

```text
com.kit.erp.basis
├── company/
├── item/
├── itemcomposition/    # plan | actual
├── workcenter/
├── process/            # plan | actual
├── workstandard/       # plan | actual
├── equipment/
├── unitcost/
├── user/
└── compare/
```

- Base URL: `/api/v1/basis`
- variant: `item-composition/{plan|actual}`, `processes/{plan|actual}`, `work-standards/{plan|actual}`
- 소프트 삭제: `recoding_state`
- `EndDate = 2076-06-06` → API `null`
- 레거시 `*InfoIndex` → API `id`

---

## 6. MariaDB 테이블

| 신규 | 레거시 |
|------|--------|
| `company` | CI_MT |
| `item` | II_MT |
| `item_composition` | IOI_MT |
| `real_item_composition` | RIOI_MT |
| `work_center` | WCI_MT |
| `process_sequence` | PSI_MT |
| `real_process_sequence` | RPSI_MT |
| `equipment` | EI_MT |
| `work_standard` | WSI_MT |
| `real_work_standard` | RWSI_MT |
| `unit_cost` | UCI_MT |
| `app_user` | UI_MT |

이력: `item_composition_history`, `unit_cost_history` (선택)

---

## 7. 공정·BOM·재고 연계

### 7.1 공정순서 예시

```text
item_id | process_sequence | process_code | wc_name
   A    | 10               | 절단(코드)   | WC1
   A    | 20               | 밴딩         | WC2
   A    | 30               | 피막         | WC3
```

### 7.2 WIP 슬롯

- 반제품·제품: 공정별 WIP 필요
- 생성: 공정 등록 시 ensure 또는 첫 생산 트랜잭션 Lazy
- 수량: `stock_movement` + `inventory_balance` + `inventory_balance_monthly`

---

## 8. 구현 Wave

### B0 (S0와 동시)
- [x] JWT, `GET /basis/public-codes`, `CRUD /basis/users`
- [ ] **B0-R:** [사용자 스펙](./basis-user-spec.md) — 7필드 슬림, `contact`, `work_diary_group_id`, `check-login-id`

### B1
- [ ] companies + `CompanyRegisteredEvent`
- [ ] items, work-centers (**창고 훅 없음**)
- [ ] 공통 검색 API

### B2 (plan)
- [ ] item-composition/plan, processes/plan, work-standards/plan
- [ ] BOM 이력
- [ ] ProcessDefinedEvent → WIP ensure (정책)

### B3
- [ ] 단가 3종 (**외주단가 → 창고 연동 금지**)

### B4 (actual)
- [ ] actual CRUD (1차 읽기 위주)
- [ ] compare API 3종

### B5
- [x] equipment, work_center_calendar (PCI_MT, B5) — **B5-R:** `production_calendar` + Override sparse, 분(INT)
- [x] 작업장 등록 시 기준작업장 달력 복사 (B5) — **B5-R에서 폐지** ([생산달력](./basis-production-calendar-spec.md) §5.1)

---

## 9. Service 설계

| 레거시 | 신규 |
|--------|------|
| `MasterInfoRecordRUD` | 도메인별 `XxxService` |
| `RelatedTableRegistration` | **삭제** → Event → inventory/ledger |
| `View.cs` | QueryService / DTO |

---

## 10. React 화면 (별도 승인)

```text
/basis/companies, /items, /work-centers
/basis/item-composition/{plan|actual}
/basis/processes/{plan|actual}
/basis/work-standards/{plan|actual}
/basis/equipment, /unit-costs/*, /users
```

plan/actual: 공통 컴포넌트 + `variant` prop, actual은 compare 탭.

---

## 11. 테스트 시나리오

| # | 검증 |
|---|------|
| 1 | 거래처 등록 → BSI/창고 행 없음, ledger account만 |
| 2 | 원자재 품목 등록 → RMS 행 없음 |
| 3 | 공정 등록 → WIP ensure (정책 시) |
| 4 | 외주단가 → OS_MT 없음 |
| 5 | actual로 트랜잭션 → 거부 |
| 6 | compare API diff |
| 7 | FK가 `item_id`인지 (문자열 ItemNum 조인 없음) |

---

## 12. 문서 역할 분담

| 문서 | 내용 |
|------|------|
| `basis-information-api-spec.md` | Endpoint, 필드 매핑 |
| **본 문서** | Wave, 키 설계, 금지사항, 재고 연계 |
| `inventory-ledger-spec.md` | 재고·원장·구매입고 |

---

## 13. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-06-22 | 초안 ? PK/FK, 공정 키 계층, 14도메인, Wave |
