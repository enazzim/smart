# D4 — 거래처 (`CompanyInfo` → `CI_MT`)

> Step 0 산출물 · **확정 v0.3** (원장 TO-BE · 역할 필수 · sample 정합)  
> SSOT: `KIT_ERP/BasisInformation/MasterInfoRecordRUD.cs` `m_FieldName[0]` (레거시 36필드)  
> 화면: `CompanyInfo.aspx` / `CompanyInfo.aspx.cs`  
> SmartManager: `company` + `company_role` + `partner_ledger_account` / `partner_ledger_monthly`  
> 내용키: `business_reg_no` ← `BusinessRegistrationNum` (UNIQUE)  
> PK: `id` ← `CompanyInfoIndex`

**관련:** [`step0-plan-D1-D4.md`](./step0-plan-D1-D4.md) · [`domain-event-projector-matrix.md`](./domain-event-projector-matrix.md) · [기준정보 시스템 컬럼 규칙](../../.cursor/rules/master-audit-fields.mdc) · TO-BE [`results/sample/basis-company-spec.md`](../../results/sample/basis-company-spec.md) · [`inventory-ledger-spec.md`](../../results/sample/inventory-ledger-spec.md)

### v0.3 변경 요약 (확정)

| 항목 | v0.2 | v0.3 |
|------|------|------|
| 원장 | `financial_partner_year` (BSI 1행) | **`partner_ledger_account` + `partner_ledger_monthly`** |
| Projector | `FinancialPartnerProjector` | **`PartnerLedgerProjector.ensureAccounts`** |
| 등록 시 금액 | (미정) | **account 껍데기만**, 월별 금액은 TX 이벤트에서 갱신 |
| 외주 | `buy_flag` on 단일 행 | **`PURCHASE` 원장 공유** + `outsource_history` 통계 분리 (INF-5) |
| 역할 | 최소 1개 **필수** | **유지** (현업 정책) |
| 레거시 BSI 72컬럼 | — | **폐기** → monthly 12행 정규화 |

---

## 1. 분류 범례

| 분류 | 의미 |
|------|------|
| **Keep** | UI·API·DB 유지 (사용자 입력) |
| **시스템(자동)** | DB·API 유지, **프로그램만 설정** — 사용자 입력 금지 |
| **Drop** | SmartManager 미구현·미이전 (v0.2 확정) |
| **Phase2** | 영업·수금 모듈 이후 |

**저장 부수효과 (1 트랜잭션):** `company` + `company_role` 동기화 후 `PartnerLedgerProjector`가 **당해 연도** `partner_ledger_account`만 ensure ([§2.3](./d4-company.md), [domain-event §2](./domain-event-projector-matrix.md))

| `company_role` | `partner_ledger_account.ledger_type` |
|----------------|--------------------------------------|
| `SALES` | `SALES` 계정 생성 |
| `PURCHASE` | `PURCHASE` 계정 생성 |
| `OUTSOURCE` | `PURCHASE` 계정 생성 (**이미 있으면 재사용**) |
| `COST` | 원장 **미생성** (`COST`만 단독이면 전체 미생성) |

> 레거시 `BSI_MT`의 `SaleDistinction`/`BuyingDistinction` wide 컬럼 패턴은 **폐기**. 월별 금액은 `partner_ledger_monthly` + 구매입고·외주납품·수금·지급 이벤트가 갱신.

---

## 2. SmartManager 스키마 (v0.3)

### 2.1 `company`

| UI 라벨 | React field | DB 컬럼 | 타입 | 분류 | 필수 | 비고 |
|---------|-------------|---------|------|------|------|------|
| 상호 | companyName | company_name | string | Keep | Y | |
| 대표자 | presidentName | president_name | string | Keep | Y | |
| 사업자번호 | businessRegNo | business_reg_no | string | Keep | Y | 내용키 · 수정 불가 |
| 법인등록번호 | corporationRegNo | corporation_reg_no | string | Keep | N | |
| 사업장주소 | businessAddress | business_address | string | Keep | Y | 우편번호 검색 |
| 홈페이지 | homepageUrl | homepage_url | string | Keep | N | |
| 업태 | businessType | business_type | string | Keep | N | |
| 종목 | businessItem | business_item | string | Keep | N | |
| 전화번호 | telephone | telephone | string | Keep | N | |
| 팩스번호 | fax | fax | string | Keep | N | |
| 매출기준일 | saleStandardDay | sale_standard_day | int | Keep | N | 1~31 · Phase2 수금 연계 |
| 어음승인기준 | billApprovalStandard | bill_approval_standard | int | Keep | N | 1~12 |
| 정기수금일1 | fixCollectDay1 | fix_collect_day_1 | int | Keep | N | |
| 담당자 | contactName | contact_name | string | Keep | N | |
| 이메일 | contactEmail | contact_email | string | Keep | N | |
| (PK) | id | id | bigint | 시스템(자동) | — | |
| 레코드상태 | recordingState | recording_state | boolean | 시스템(자동) | — | 소프트 삭제 |
| 등록일시 | createdAt | created_at | datetime | 시스템(자동) | — | |
| 등록자 | createdBy | created_by | string | 시스템(자동) | — | |
| 등록자ID | createdById | created_by_id | string | 시스템(자동) | — | |
| 수정일시 | updatedAt | updated_at | datetime | 시스템(자동) | — | |
| 수정자 | updatedBy | updated_by | string | 시스템(자동) | — | |
| 수정자ID | updatedById | updated_by_id | string | 시스템(자동) | — | |

### 2.2 `company_role`

| 컬럼 | 타입 | 비고 |
|------|------|------|
| `id` | bigint PK | 자동 |
| `company_id` | bigint FK → `company.id` | |
| `role_type` | ENUM | `SALES`, `OUTSOURCE`, `PURCHASE`, `COST` |
| UNIQUE | | (`company_id`, `role_type`) |

- UI: 체크박스 4종 → API `roles: ["SALES","PURCHASE"]`
- 검증: **최소 1개** 역할 (현업 확정)
- 트랜잭션 API: 해당 `role_type` 매핑 없으면 **거부**

### 2.3 `partner_ledger_account` (구 `BSI_MT` 헤더)

| 컬럼 | 타입 | 비고 |
|------|------|------|
| `id` | bigint PK | 자동 |
| `company_id` | bigint FK | |
| `fiscal_year` | smallint | 당해 연도 (저장 시 `Year.now()`) |
| `ledger_type` | ENUM | `SALES` \| `PURCHASE` |
| `prior_sale_carryover` | decimal(18,2) | 전년 이월 매출잔 (`LastYearSaleTransferCost`) · 기본 0 |
| `prior_buy_carryover` | decimal(18,2) | 전년 이월 매입잔 (`LastYearBuyingTransferCost`) · 기본 0 |
| `recording_state` + 감사 | | [시스템 컬럼](../../.cursor/rules/master-audit-fields.mdc) |

- UK: `(company_id, fiscal_year, ledger_type)`
- **등록·수정 시:** account 행만 INSERT/활성화, **금액 컬럼 없음**
- `COST`만 단독 거래처 → account **미생성**

### 2.4 `partner_ledger_monthly` (구 BSI 월별 72컬럼 대체)

| 컬럼 | 타입 | 비고 |
|------|------|------|
| `id` | bigint PK | |
| `partner_ledger_account_id` | bigint FK | |
| `month` | tinyint | 1~12 |
| `sale_amount` | decimal(18,2) | SALES 계정: 당월 매출 발생 |
| `collected_amount` | decimal(18,2) | SALES: 당월 수금 |
| `purchase_amount` | decimal(18,2) | PURCHASE: 당월 매입(구매+외주 합산) |
| `paid_amount` | decimal(18,2) | PURCHASE: 당월 지급 |

- UK: `(partner_ledger_account_id, month)`
- **기준정보 저장 시:** 12행 생성 가능(전부 0) 또는 **첫 TX 시 Lazy ensure** — 구현 시 하나 선택, Cut-over는 12행 unpivot 적재
- 미수·미지급: 잔액 뷰 또는 `prior_*_carryover` + 누적 합산으로 계산

### 2.5 원장 2층 구조 (구매 / 외주)

| 층 | 목적 |
|----|------|
| **거래처 원장** (`partner_ledger_*`) | 거래처별 미수·미지급 **합산** — 외주도 `PURCHASE` 계정 |
| **비용 이력** (TX 모듈) | 통계 분리 — `purchase_history` / `outsource_history` (INF-5) |

---

## 3. 레거시 36필드 대조표 (이전·감사용)

| # | 레거시 컬럼 | v0.3 | 비고 |
|---|------------|------|------|
| 1~4 | ReceiveingOrderCompany … CostCompany | **→ `company_role`** | boolean Drop |
| 5~7 | CompanyName, PresidentName, BusinessRegistrationNum | Keep | |
| 8 | CorporationRegistrationNum | Keep | |
| 9 | BusinessCompanyAddress | Keep | |
| 10 | TaxBillAddress | **Drop** | |
| 11~13 | Homepage, 업태, 종목 | Keep | |
| 14 | CurrentTradeState | **Drop** | |
| 15~16 | SupplementaryValueTax, Rate | **Drop** | |
| 17~18 | Telephone, Fax | Keep | |
| 19~21 | TradeClassification1~3 | **Drop** | |
| 22 | SaleStandardDate | Keep | |
| 23 | SaleStandardBillDate | **Drop** | |
| 24 | BillApprovalStandard | Keep | |
| 25 | FixPeriodCollectMoneyDate1 | Keep | |
| 26 | FixPeriodCollectMoneyDate2 | **Drop** | |
| 27~28 | 담당자, Email | Keep | |
| 29~36 | RecodingState, 감사, CompanyInfoIndex | **시스템(자동)** | |
| BSI 월별 72컬럼 | TotalSaleCost1~12 … | **→ `partner_ledger_monthly`** | unpivot·차분 이전 |

---

## 4. API · 이벤트 (v0.3)

| 항목 | 규칙 |
|------|------|
| `POST /api/v1/basis/companies` | 사용자 필드 + `roles[]` (최소 1개). 시스템 컬럼 **거부/무시** |
| `PUT /api/v1/basis/companies/{id}` | `business_reg_no` 변경 불가 · roles 동기화 |
| `DELETE` | `recording_state=0` + 원장 account 비활성 |
| 저장 TX | ① `company` ② `company_role` ③ `PartnerLedgerProjector.ensureAccounts` |
| 이벤트 | `CompanyRegistered` / `CompanyUpdated` / `CompanyDeleted` (또는 `CompanyPartnerTypeChanged` 페이로드 포함) |
| 금지 | `basis` 패키지에서 `partner_ledger_monthly` 금액 직접 INSERT — **TX Listener만** |

**요청 예**

```json
{
  "businessRegNo": "123-45-67890",
  "companyName": "신동공업",
  "presidentName": "홍길동",
  "businessAddress": "…",
  "roles": ["SALES", "PURCHASE"]
}
```

**ensureAccounts 결과 예 (2026년 저장, 위 roles)**

| ledger_type | 생성 |
|-------------|------|
| `SALES` | 1행 (금액 0) |
| `PURCHASE` | 1행 (금액 0) |

---

## 5. Cut-over · Phase2

| 항목 | 처리 |
|------|------|
| Drop 9컬럼 | MariaDB **미이전** |
| 4 플래그 | `company_role` 행 |
| `BSI_MT` | `partner_ledger_account` + `partner_ledger_monthly` unpivot ([inventory-ledger-spec §9](../../results/sample/inventory-ledger-spec.md)) |
| 커뮤니티 첨부 그리드 | Phase2 |
| `COMPANY_TRADE_CLASS_*` | Drop — code_group **불필요** |
| `outsource_history` | INF-5 |

---

## 6. 체크리스트

- [x] v0.2 Drop 9필드 확정
- [x] `company_role` + 역할 최소 1개 필수
- [x] v0.3 `partner_ledger_*` 원장 확정 (`financial_partner_year` **폐기**)
- [x] 시스템 컬럼 자동화
- [ ] Step 1 Flyway + 거래처 CRUD + `PartnerLedgerProjector`
- [ ] `PUC_MT` / `CT_T` export (우선순위 낮음)

---

*v0.2→v0.3: 2026-07-02 · TO-BE: `results/sample/basis-company-spec`, `inventory-ledger-spec` 정합*