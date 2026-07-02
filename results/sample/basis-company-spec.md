# 거래처 기준정보 확정 스펙

> **문서 버전:** 1.1  
> **작성일:** 2026-06-22  
> **상태:** 확정안 (구현 대기)  
> **관련 Wave:** B1(현행) → **B1-R** (거래처 리팩터) → INF-5 (외주 비용·원장 보완)  
> **관련 문서:**  
> - [기준정보 API·필드 매핑](./basis-information-api-spec.md) §5.1  
> - [기준정보 구현 설계](./basis-information-implementation-spec.md)  
> - [재고·원장 설계](./inventory-ledger-spec.md) §4  
> - [업무 흐름 TO-BE](./business-workflow-revision.md)  
> - [품목 기준정보 확정 스펙](./basis-item-spec.md)

---

## 1. 문서 목적

기준정보 정비 1단계로 **거래처(`company`)** 의 업무 정의, 데이터 모델, 화면·API, 원장 연계, 미구현 갭·후속 Wave를 한곳에 정리한다.

**관련 확정 문서:** [품목 기준정보](./basis-item-spec.md)

---

## 2. 기준정보 공통 UI 원칙

**모든 기준정보 화면**에 공통으로 적용한다. (품목·작업장·BOM 등 동일)

| 항목 | 확정 |
|------|------|
| 레거시 하단 부가 그리드 | **구현하지 않음** — 품목정보테이블, 자료실테이블, 제목, 거래처 참고보기용 하단 탭 등 |
| 목록 화면 | 상단 검색 + 그리드 + 우측 상단 **등록(Modal)** + 행별 **수정·삭제** |
| 시스템 필드 | `id`, 등록자/등록일, 수정자/수정일, `recoding_state` — **화면 입력 없음** |
| 삭제 | 물리 삭제 금지, `recoding_state = 0` 소프트 삭제 |

---

## 3. 업무 정의

거래처는 수주·구매·외주·비용 등 모든 대외 거래의 상대방 **마스터**이다.

| 원칙 | 내용 |
|------|------|
| 거래처 본문 | 법인·연락 정보만 (`company`) |
| 거래 역할 | `partner_type` + `company_partner_type` 매핑으로 관리 |
| 내용키(UK) | `business_registration_num` (사업자등록번호) |
| PK | `id` (BIGINT, 레거시 `*InfoIndex` 대체) |
| 삭제 | 물리 삭제 금지, `recoding_state = 0` 소프트 삭제 |

레거시 `CI_MT`의 4개 거래구분 체크박스·종류별 `BSI_MT` 자동 생성 패턴은 **폐기**한다.

---

## 4. 데이터 모델

### 4.1 `company` (거래처)

**업무 필드 (화면 입력 · 모두 필수)**

| 컬럼 | API 필드 | 설명 |
|------|----------|------|
| `company_name` | `companyName` | 거래처명 |
| `business_registration_num` | `businessRegistrationNum` | 사업자등록번호 (UK) |
| `business_address` | `businessAddress` | 주소 (발주서·거래 문서용 단일 주소) |
| `president_name` | `presidentName` | 대표자명 |
| `email` | `email` | 이메일 |
| `telephone_num` | `telephoneNum` | 연락처(전화) |

**시스템 필드 (화면 입력 없음 · 서버 자동)**

| 컬럼 | 설명 |
|------|------|
| `id` | PK |
| `created_by` / `created_at` | 등록자·등록일 |
| `updated_by` / `updated_at` | 수정자·수정일 |
| `recoding_state` | `1`=유효, `0`=삭제 |

**TO-BE에서 제거할 컬럼 (현행 `company`에 잔존)**

- `receiving_order_company`, `outsourcing_company`, `buying_company`, `cost_company`
- `tax_bill_address`, `corporation_registration_num`, `homepage_address`
- 업태·종목, 거래상태, 부가세, 팩스, 분류1~3, 수금·어음 조건, `company_person_in_charge` 등

> 세금계산서용 **계산서주소**는 1차에서 **사업장주소 1개로 통일**. TX3(매출) 확장 시 필요하면 `tax_bill_address` 재도입 검토.

### 4.2 `partner_type` (거래처종류) — 시드 4건

| `code` | 명칭 |
|--------|------|
| `SALES` | 수주거래처 |
| `OUTSOURCE` | 외주거래처 |
| `PURCHASE` | 구매거래처 |
| `COST` | 비용거래처 |

운영 중 코드 추가는 거의 없음. `public_code`가 아닌 **전용 마스터**로 둔다.

### 4.3 `company_partner_type` (거래처 ↔ 종류 매핑)

| 컬럼 | 설명 |
|------|------|
| `company_id` | FK → `company` |
| `partner_type_id` | FK → `partner_type` |
| UK | `(company_id, partner_type_id)` |

- 한 거래처에 **복수 종류** 매핑 가능 (예: 구매 + 외주).
- **매핑 0건**으로 거래처만 등록하는 것은 **허용** (나중에 매핑).
- 수주·구매·외주 트랜잭션 저장 시 해당 종류 매핑 **없으면 거부**.

---

## 5. 거래처 원장 (`partner_ledger_*`) 연계

레거시 종류별 `BSI_MT` 대신 **`partner_ledger_account` + `partner_ledger_monthly`** 를 사용한다.

### 5.1 매핑 → 원장 계정 생성 규칙

| `partner_type` 매핑 | `partner_ledger_account.ledger_type` |
|---------------------|-------------------------------------|
| `SALES` | `SALES` 생성 |
| `PURCHASE` | `PURCHASE` 생성 |
| `OUTSOURCE` | **`PURCHASE` 생성** (이미 있으면 재사용) |
| `COST` | 1차 미생성 (비용 Wave에서 검토) |

- UK: `(company_id, fiscal_year, ledger_type)`
- 생성 시점: **거래처 저장 1트랜잭션** 내 매핑 반영 후 `ensureAccount()`
- 이벤트: `CompanyRegisteredEvent` 대신 **`CompanyPartnerTypeChangedEvent`** (또는 저장 API 내부 처리)

### 5.2 미지급 vs 통계 (2층 구조)

| 층 | 목적 | 구매 / 외주 |
|----|------|-------------|
| **거래처 원장** | 거래처별 미지급·지급 **합산** | 둘 다 `PURCHASE` 원장에 반영 |
| **비용 이력** | 기간·품목·거래처 **통계 분리** | `purchase_history` / `outsource_history` |

```
구매입고 POST  → purchase_history INSERT  + PURCHASE 원장 미지급 증가
외주납품 POST  → outsource_history INSERT + PURCHASE 원장 미지급 증가
지급           → PURCHASE 원장 미지급 감소 (지급 통계는 cost_category로 구분 — 후속)
```

---

## 6. 화면 (`/basis/companies`)

> 하단 부가 그리드·자료실 등 **없음** (§2).

### 6.1 목록

| 요소 | 동작 |
|------|------|
| 상단 검색 | 거래처명 **입력 즉시 필터** (디바운스 또는 `?q=`). 목록 좁히기용 — `SearchableSelect` 단건 선택과 구분 |
| 그리드 컬럼 | 거래처명, 사업자번호, 대표자, 연락처, 이메일, (선택) 주소 일부, (선택) 등록일 |
| 우측 상단 | **등록** 버튼 → Modal |
| 행 우측 | **수정** / **삭제** (`RowActions`) |
| 삭제 | 확인 후 `recoding_state = 0`. 이력 참조 시 비활성만 허용 검토 |

### 6.2 등록·수정 Modal

**본문 (필수 6필드)**

- 거래처명, 사업자번호, 주소, 대표자명, 이메일, 연락처

**Modal 제목 줄 우측**

- **[거래처매핑]** 버튼 → 하위 Modal에서 수주/외주/구매/비용 체크

**저장 흐름 (권장: 한 번의 「저장」)**

```
본문 입력 + [거래처매핑]에서 종류 선택 (신규 시 폼 상태로 보관)
  → [저장] 클릭
  → 서버 1 트랜잭션:
       1) company INSERT/UPDATE
       2) company_partner_type 동기화
       3) SALES / PURCHASE 원장 ensure
```

- 하위 Modal **[확인]** ≠ 최종 저장 (신규 등록 시 UI로 구분).
- **수정** 시: 매핑 확인 시 즉시 API 호출도 가능하나, 1차는 저장과 동기화로 통일 권장.
- **사업자번호**: 수정 시 변경 불가 권장.

### 6.3 트랜잭션 화면에서의 거래처 선택

| 화면 | 필터 |
|------|------|
| 수주 | `partnerType=SALES` |
| 구매발주 | `partnerType=PURCHASE` |
| 외주발주 | `partnerType=OUTSOURCE` |
| 비용 | `partnerType=COST` |

발주서·수주서에 필요한 거래처 정보는 마스터 6필드에서 조인.  
**(권장 후속)** 발주 저장 시 헤더에 **스냅샷** 복사 (마스터 변경 시 과거 문서 보존).

---

## 7. API (TO-BE)

| Method | Endpoint | 설명 |
|--------|----------|------|
| GET | `/api/v1/basis/companies?q=` | 목록·검색 (`recoding_state=1`) |
| GET | `/api/v1/basis/companies?partnerType=SALES` | 종류별 필터 (신규) |
| GET | `/api/v1/basis/companies/{id}` | 상세 |
| GET | `/api/v1/basis/companies/by-brn/{brn}` | 사업자번호 조회 |
| POST | `/api/v1/basis/companies` | 등록 — body에 `partnerTypeCodes[]` 포함 |
| PUT | `/api/v1/basis/companies/{id}` | 수정 |
| PUT | `/api/v1/basis/companies/{id}/partner-types` | 매핑만 갱신 (선택) |
| DELETE | `/api/v1/basis/companies/{id}` | 소프트 삭제 |

**POST body 예시**

```json
{
  "companyName": "○○공업",
  "businessRegistrationNum": "1234567890",
  "businessAddress": "경기도 ...",
  "presidentName": "홍길동",
  "email": "buyer@example.com",
  "telephoneNum": "031-000-0000",
  "partnerTypeCodes": ["PURCHASE", "OUTSOURCE"]
}
```

**검증**

- 필수 6필드 NOT NULL
- `partnerTypeCodes` 없어도 등록 가능
- 트랜잭션 API: 기존 boolean 검사 → **매핑 존재 여부**로 교체

---

## 8. 현행 구현과의 차이 (갭)

| 항목 | 현행 | TO-BE |
|------|------|-------|
| 역할 | `company` 4 boolean | `partner_type` + 매핑 |
| 필드 | 30+ 컬럼 | 업무 6 + 시스템 5 |
| 등록 API | boolean 필수 1개 이상 | 매핑 선택적, 저장 시 일괄 |
| 원장 트리거 | `CompanyRegisteredEvent` + boolean | 매핑 기준 ensure |
| 프론트 | 체크박스·세금계산서 주소 | Modal 6필드 + 매핑 버튼 |
| 외주 미지급 | 미연동 | PURCHASE 원장 + `outsource_history` |
| 통계 | `purchase_history`만 | + `outsource_history` |

---

## 9. 구현·후속 Wave (할 일)

### 9.1 B1-R — 거래처 리팩터 (우선)

- [ ] Flyway: `partner_type` 시드, `company_partner_type` 생성
- [ ] Flyway: `company` 컬럼 정리 (boolean·미사용 컬럼 제거 또는 deprecated 마이그레이션)
- [ ] 기존 boolean → 매핑 데이터 이관 스크립트
- [ ] `CompanyService` / Request·Response DTO 정리
- [ ] `PartnerLedgerAccountService.ensure` — 매핑 기반으로 변경
- [ ] `SalesOrderService`, `PurchaseOrderService`, `ProductionPlanService`(외주), `UnitCostService` — 매핑 검증
- [ ] `GET /companies?partnerType=` 필터
- [ ] 프론트 `CompaniesPage` — UI 확정안 반영
- [ ] `B1IntegrationTest` 갱신

### 9.2 INF-5 — 외주 비용·원장 보완 (거래처 다음·외주 본격 사용 전)

**P1 (필수)**

- [ ] `outsource_history` 테이블 (`purchase_history` 대칭)
- [ ] `outsource_receipt`: `amount`, `posted`, `company_id` 등 구매입고 대칭
- [ ] 외주납품 POST → `outsource_history` INSERT
- [ ] `LedgerOnOutsourceReceiptListener` → PURCHASE 미지급 증가

**P2**

- [ ] 지급: `partner_payment` + `cost_category` (`PURCHASE` / `OUTSOURCE`) 또는 `outsource_payment`
- [ ] 통계 API: `GET /reports/costs/summary` (purchase_history + outsource_history)

**P3 (선택)**

- [ ] `partner_ledger_monthly`에 `amount_purchased_material`, `amount_outsource_processing` 컬럼 분리

### 9.3 기타 후속

- [ ] 발주·수주 헤더 **거래처 스냅샷** 컬럼
- [ ] `COST` 매핑 시 원장·비용 모듈 연계
- [ ] 세금계산서용 `tax_bill_address` 재도입 여부 (TX3)
- [ ] 사업자번호 UK + 소프트삭제 시 재등록 정책
- [ ] 본 문서를 [basis-information-api-spec.md](./basis-information-api-spec.md) §5.1과 동기화

---

## 10. 의사결정 요약

| 항목 | 확정 |
|------|------|
| 거래처 본문 | 6필드 + 시스템 5필드 |
| 역할 | `partner_type` + 매핑 (company에서 분리) |
| 저장 | company + mapping + 원장 ensure **1트랜잭션** |
| UI | 상단 필터, Modal 등록/수정, 행별 수정·삭제, Modal 내 매핑 버튼 |
| 하단 부가 영역 | **전 기준정보 미구현** (§2) |
| 미지급 원장 | SALES / PURCHASE (외주도 PURCHASE 합산) |
| 비용 통계 | `purchase_history` vs `outsource_history` 분리 |
| 레거시 BSI_MT | `partner_ledger_*` 로 대체, 종류별 물리 테이블 없음 |

---

## 11. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-06-22 | 거래처 확정안 — 대화·레거시 화면·원장 2층 구조 반영 |
| 1.1 | 2026-06-22 | §2 기준정보 공통 UI(하단 부가 영역 제외), 품목 스펙 문서 링크 |
