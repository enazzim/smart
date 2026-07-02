# 단가 기준정보 확정 스펙

> **문서 버전:** 1.0  
> **작성일:** 2026-06-24  
> **상태:** 확정안 (구현 대기)  
> **관련 Wave:** B3(현행) → **B3-R** (단가 UI·검증·API 정비)  
> **관련 문서:**  
> - [기준정보 API·필드 매핑](./basis-information-api-spec.md) **§5.8 (필드·API SoT)**  
> - [품목 기준정보 확정 스펙](./basis-item-spec.md) §4.2  
> - [거래처 기준정보 확정 스펙](./basis-company-spec.md) §4.2·§6.4  
> - [공정순서 확정 스펙](./basis-process-sequence-spec.md) §5.1  
> - [공용코드 확정 스펙](./basis-public-code-spec.md)  
> - [기준정보 구현 설계](./basis-information-implementation-spec.md)  
> - [재고·원장 설계](./inventory-ledger-spec.md) — 외주단가·창고 연동 금지

**업무 원본:** `260624_기준정보_단가.md` (PRODEV, 2026-06-24)

---

## 1. 문서 목적·적용 범위

구매(원자재 매입)·영업(완제품/상품 매출)·생산(임가공 외주) 전반의 **기준 단가 마스터(`unit_cost`, 레거시 `UCI_MT`)** 와 **변동 이력(`unit_cost_history`, `UPUCI_HT`)** 의 업무 정의, 구분별 검증, 발주비율 규칙, 화면·API, 현행 B3 구현과의 갭·후속 Wave를 정리한다.

| 구분 | 내용 |
|------|------|
| **통합 테이블** | `구매단가` · `판매단가` · `외주단가` — **단일 `unit_cost`**, `cost_type` 으로 구분 |
| **API·필드 SoT** | [basis-information-api-spec.md](./basis-information-api-spec.md) **§5.8** |
| **본 문서 역할** | §5.8에 없는 **자산분류 제한·발주비율·락·이력·3탭 UI·MRP 소비 규칙** 확정 |
| **레거시 2분할 UI** | 좌 목록 / 우 상세·이력 타임라인 — **미구현**. 공통 **목록 + Modal** 패턴 사용 |
| **외주창고 자동 연동** | 레거시 `OutSideStoreTable` 후처리 — **금지** ([구현 설계](./basis-information-implementation-spec.md)) |

레거시는 화면 3종(`SaleUnitCodeInfo` / `BuyingUnitCodeInfo` / `OutSideOrderUnitCodeInfo`)이었으나, React는 **`UnitCostsPage` 1 Route + 3탭**으로 통합한다.

---

## 2. 기준정보 공통 UI 원칙

[거래처 스펙](./basis-company-spec.md) §2와 동일.

| 항목 | 확정 |
|------|------|
| 레거시 하단 부가 그리드 | **구현하지 않음** |
| 목록 화면 | **3탭** + (선택) 검색 + 그리드 + **등록(Modal)** + 행별 **수정·삭제** |
| 2분할(좌/우) 상세 패널 | **사용하지 않음** |
| 이력 타임라인 | **1차 미노출** — DB·API 이력만 유지. 2차에서 수정 Modal 하단 접이식 그리드 검토 |
| 시스템 필드 | `id`, 등록/수정, `recoding_state` — 화면 입력 없음 |
| 삭제 | `recoding_state = 0` 소프트 삭제 |

---

## 3. 업무 정의

### 3.1 단가 구분 (`cost_type`)

| API `type` | DB `cost_type` | 레거시 `UnitCostDivision` | 용도 |
|------------|----------------|---------------------------|------|
| `SALE` | `SALE` | 판매단가 | 수주·매출 기준가 |
| `PURCHASE` | `PURCHASE` | 구매단가 | 구매발주·매입 기준가 |
| `OUTSOURCE` | `OUTSOURCE` | 외주단가 | 임가공 외주 발주 기준가 |

### 3.2 식별자·내용키

| 구분 | 내용 |
|------|------|
| **PK** | `id` (BIGINT AUTO_INCREMENT) — PUT/DELETE **`/{id}`** |
| **내용키 (UK)** | **`(cost_type, item_id, company_id, begin_date, begin_process_code_id, end_process_code_id)`** — 활성(`recoding_state = 1`) 구간 유일 |
| **품목 FK** | `item_id` → `item.id` |
| **거래처 FK** | `company_id` → `company.id` |
| **외주 공정 FK** | `begin_process_code_id`, `end_process_code_id` → `public_code.id` (`usage_type = PROCESS`). 판매·구매는 **NULL** |

> **현행 B3:** UK에 `begin_process_code` / `end_process_code` **VARCHAR** 사용 — B3-R에서 **`public_code.id` FK** 로 교체 ([§9.1](#91-b3-r--단가)).

동일 품목·동일 거래처·동일 적용 시작일·(외주) 동일 공정구간에 **기간·단가가 다른 계약**은 UK가 다르면 **여러 건** 등록 가능.

### 3.3 이력 (`unit_cost_history`)

| 규칙 | 내용 |
|------|------|
| **관계** | `unit_cost` 1 : N `unit_cost_history` |
| **생성 시점** | **수정(PUT)** 직전 — 변경 전 스냅샷 INSERT |
| **변경 사유** | 수정 시 **`update_reason` 필수** — blank 시 **400** |
| **신규(POST)** | 이력 행 **생성하지 않음** |
| **API** | `GET /api/v1/basis/unit-costs/{id}/history` |

---

## 4. API·필드

**엔드포인트·필드 정의는 [basis-information-api-spec.md §5.8](./basis-information-api-spec.md) 를 따른다.** 아래는 KIT_ERP DB·Modal·B3-R 매핑이다.

### 4.1 REST API (§5.8)

| Method | Endpoint | 비고 |
|--------|----------|------|
| GET | `/api/v1/basis/unit-costs?type=SALE\|PURCHASE\|OUTSOURCE` | 탭별 목록 |
| GET | `/api/v1/basis/unit-costs/{id}` | 상세 |
| GET | `/api/v1/basis/unit-costs/by-item/{itemNum}?type=` | 품목별 (보조) |
| POST | `/api/v1/basis/unit-costs` | `type` body 포함 |
| PUT | `/api/v1/basis/unit-costs/{id}` | `updateReason` **필수** |
| DELETE | `/api/v1/basis/unit-costs/{id}` | 소프트 삭제 |
| GET | `/api/v1/basis/unit-costs/{id}/history` | 변경 이력 |

### 4.2 화면 입력 필드

| # | 화면 라벨 | 필수 | API (요청) | API (응답) | DB | Modal |
|---|-----------|:----:|------------|------------|-----|:-----:|
| — | (시스템) | | — | **`id`** | `id` (PK) | — |
| — | 단가 구분 | ● | `type` | `type` | `cost_type` | 탭 고정 / 등록 시 탭값 |
| 1 | 품목 | ● | `itemId` | `itemId`, `itemNum`, `itemName`, `propertyClassification` | `item_id` (FK) | `ItemSelect` — 탭별 필터 (§5.1) |
| 2 | 거래처 | ● | `companyId` | `companyId`, `companyName`, `businessRegistrationNum` | `company_id` (FK) | `CompanySelect` — 탭별 `partnerType` (§5.2) |
| 3 | 시작공정 | ●* | `beginProcessCodeId` | `beginProcessCodeId`, `processCode`, `processName` | `begin_process_code_id` (FK) | `ProcessCodeSelect` — **외주만** |
| 4 | 종료공정 | ●* | `endProcessCodeId` | `endProcessCodeId`, … | `end_process_code_id` (FK) | **외주만** |
| 5 | 발주비율 (%) | ●* | `orderRate` | `orderRate` | `order_rate` | 구매·외주 활성; 판매 **0 고정·비활성** |
| 6 | 기준단가 | ● | `standardUnitCost` | `standardUnitCost` | `standard_unit_cost` | DECIMAL(18,4) |
| 7 | 할인단가 | | `discountUnitCost` | `discountUnitCost` | `discount_unit_cost` | 기본 null / 0 |
| 8 | 적용시작일 | ● | `beginDate` | `beginDate` | `begin_date` | DATE |
| 9 | 적용종료일 | | `endDate` | `endDate` | `end_date` | DATE, null=무기한 |
| 10 | 변경 사유 | ●** | `updateReason` | — | `unit_cost_history.update_reason` | **수정 Modal만** 필수 |

\* 외주단가만 필수. 판매·구매는 공정 FK **NULL**, 요청 body에 **미포함**.  
\** POST body의 `updateReason` — **무시**.

> **요청 body** FK: `itemId`, `companyId`, (외주) `beginProcessCodeId`, `endProcessCodeId`. **행 식별**은 **`id`**.

### 4.3 §5.8·현행 B3와의 스키마 차이 (B3-R)

| 현행 B3 | TO-BE (B3-R) |
|---------|--------------|
| `begin_process_code`, `end_process_code` VARCHAR | **`begin_process_code_id`, `end_process_code_id`** FK → `public_code.id` |
| API `itemNum`, `businessRegistrationNum` | **`itemId`, `companyId`** |
| API `beginProcessCode`, `endProcessCode` | **`beginProcessCodeId`, `endProcessCodeId`** |
| `updateReason` nullable | 수정 시 **필수** |
| 자산분류·발주비율 검증 없음 | §5.3·§5.4 적용 |

### 4.4 시스템 필드 (화면 입력 없음)

| 컬럼 | API | 설명 |
|------|-----|------|
| `id` | `id` | PK |
| `created_by` / `created_at` | — | 등록 |
| `updated_by` / `updated_at` | — | 수정 |
| `recoding_state` | — | `1`=유효, `0`=삭제 |

---

## 5. §5.8 외 확정 사항

### 5.1 품목 자산분류 — 구분별 등록 제한

| `cost_type` | 허용 `property_classification` | 비고 |
|-------------|----------------------------------|------|
| **PURCHASE** (구매) | `원자재`, `상품` | |
| **SALE** (판매) | `제품`, `상품` | |
| **OUTSOURCE** (외주) | `제품`, `공정품` | 추가: plan `process_sequence` 에 **`work_distinction IN ('INHOUSE','SPLIT')`** 공정 **1건 이상** ([공정순서](./basis-process-sequence-spec.md) §5.1) |

| 계층 | 규칙 |
|------|------|
| **백엔드** | POST/PUT 시 위 표 불일치 → **400** |
| **프론트** | `ItemSelect` 옵션을 활성 탭 기준 **사전 필터** (우회 방지용 서버 검증 필수) |
| **품목 `standard_unit_cost`** | 품목 마스터 기준가 — **단가 마스터와 별개**. 발주 초기값·표시용 ([품목 스펙](./basis-item-spec.md) §4) |

### 5.2 거래처 — 구분별 역할

| `cost_type` | 거래처 조건 | 콤보 API |
|-------------|------------|----------|
| **SALE** | `partner_type` 에 **수주(SALES)** 매핑 | `GET /companies?partnerType=SALES` |
| **PURCHASE** | **구매(PURCHASE)** | `GET /companies?partnerType=PURCHASE` |
| **OUTSOURCE** | **외주(OUTSOURCE)** | `GET /companies?partnerType=OUTSOURCE` |

현행 `UnitCostService.validateCompanyRole` (boolean 플래그) → B3-R에서 **`partnerType` 필터·검증**과 [거래처 스펙](./basis-company-spec.md) 정렬.

### 5.3 구분별 필드·NULL 규칙

| 필드 | 구매 | 판매 | 외주 |
|------|:----:|:----:|:----:|
| `begin_process_code_id` | NULL | NULL | **필수** |
| `end_process_code_id` | NULL | NULL | **필수** |
| `order_rate` | **필수** (다매입처 시) | **0 고정** | **필수** |

- 판매·구매: 공정 FK가 body에 있으면 **400**.
- 외주: 시작·종료 공정 누락 → **400**. `public_code.usage_type = PROCESS`, 더미 공정(`14000000`, `14009999`) **제외** ([공용코드](./basis-public-code-spec.md)).

### 5.4 발주비율 (`order_rate`) 정합성

동일 **`(cost_type, item_id)`** 의 **활성** 단가 행 집합을 대상으로 한다. (외주는 동일 품목·**동일 공정구간** `(begin_process_code_id, end_process_code_id)` 별로 집계 — B3-R에서 명시 UK·집계 키 확정)

| 시점 | 규칙 | HTTP |
|------|------|------|
| **단가 등록·수정** | 활성 행 `order_rate` **합계 > 100%** → **차단** | 400 |
| | 합계 **≤ 100%** → **허용** (60%만 등록된 중간 상태 OK) | |
| **구매발주·외주 작업계획 확정** | 해당 품목(·공정구간) 활성 단가 합계 **= 100%** 아니면 확정 **차단** | 400 |
| **0% 거래처** | 단가 마스터 **등록은 허용** | |
| **MRP·발주 수량 분할** | `order_rate = 0` 인 행은 **분할 대상에서 제외** — 요청 레코드 **미생성** | 후속 PRD/MRP |

**예:** A 원자재 1,000개 구매 계획 — B사 60%, C사 40%, D사 0% → B 600개·C 400개 요청만 생성, D **제외**.

#### 5.4.1 동시성 (비관적 락)

동일 `(cost_type, item_id)` (외주는 + 공정구간) 에 대한 단가 등록·수정 시:

1. 트랜잭션 시작
2. 해당 키의 **활성 `unit_cost` 행에 `PESSIMISTIC_WRITE` 락**
3. 합계 검증 후 INSERT/UPDATE
4. 커밋

→ 다중 사용자 동시 등록으로 합계 100% 초과하는 **레이스 컨디션 방지**.

### 5.5 유효기간

| 규칙 | 내용 |
|------|------|
| `begin_date` | **필수** |
| `end_date` | null = 무기한. `end_date < begin_date` → **400** |
| 트랜잭션 적용 | 발주·매출·외주 전표는 **전표일 ∈ [begin_date, end_date]**** 인 활성 단가 우선 (동일 조건 다건 시 UK·최신 begin_date — 후속 TX 모듈) |

### 5.6 외주단가 — 창고·BOM 연동 금지

| 금지 | 대체 |
|------|------|
| 외주단가 등록 시 `OS_MT`·외주창고 **자동 생성** | **단가만 저장** |
| BOM 역추적로 창고 연결 | [inventory-ledger-spec](./inventory-ledger-spec.md) — **외주 입고/발주 TX** 에서 처리 |

---

## 6. 화면 (`/basis/unit-costs`)

### 6.1 3탭 + 목록 (공통 패턴)

```
┌──────────────────────────────────────────────────────────────────────────────┐
│ 단가                                                                         │
│  [ 판매단가 ]  [ 구매단가 ]  [ 외주단가 ]                    [등록]            │
│  (선택) 품목·거래처 검색                                                      │
├──────────────────────────────────────────────────────────────────────────────┤
│ 품목번호 │ 품목명 │ 거래처 │ 기준단가 │ 할인단가 │ 발주비율 │ 시작일 │ 종료일 │ … │
└──────────────────────────────────────────────────────────────────────────────┘
```

| 요소 | 동작 |
|------|------|
| **Route** | `/basis/unit-costs` **단일** |
| **탭** | `SALE` / `PURCHASE` / `OUTSOURCE` — 전환 시 `GET ?type=` 재조회 |
| **탭 UI** | 버튼형 탭 (`BomPage` `bom-tabs` 스타일). 현행 `FormSelect` → B3-R 교체 |
| **그리드** | 활성 탭 `type` 고정. 외주 탭에 **시작·종료 공정명** 컬럼 추가 |
| **등록** | Modal — **현재 탭 `type` 고정** |
| **수정** | Modal — `type`·품목·거래처·UK 키 **읽기 전용**, **변경 사유** 필수 |
| **이력 UI** | **1차 없음** (API만) |

### 6.2 등록·수정 Modal — 탭별 동적 필드

| 탭 | 시작·종료 공정 | 발주비율 | 할인단가·종료일 |
|----|----------------|----------|-----------------|
| **판매** | 숨김 | **비활성, 0** | 입력 가능 |
| **구매** | 숨김 | **활성** | 입력 가능 |
| **외주** | `ProcessCodeSelect` **필수** | **활성** | 입력 가능 |

| 필드 | 기본값 |
|------|--------|
| `orderRate` | 구매·외주 **100** (단일 거래처). 판매 **0** |
| `standardUnitCost` | **0** |
| `beginDate` | 오늘 |
| `discountUnitCost` | null |
| `endDate` | null |

---

## 7. API 요청·응답 예시

**POST** `/api/v1/basis/unit-costs` — 구매단가

```json
{
  "type": "PURCHASE",
  "itemId": 10,
  "companyId": 5,
  "orderRate": 60,
  "standardUnitCost": 1250.5,
  "discountUnitCost": 1200,
  "beginDate": "2026-06-01",
  "endDate": null
}
```

**POST** — 외주단가

```json
{
  "type": "OUTSOURCE",
  "itemId": 50,
  "companyId": 8,
  "beginProcessCodeId": 301,
  "endProcessCodeId": 305,
  "orderRate": 100,
  "standardUnitCost": 3500,
  "beginDate": "2026-06-01"
}
```

**PUT** `/api/v1/basis/unit-costs/42`

```json
{
  "type": "PURCHASE",
  "itemId": 10,
  "companyId": 5,
  "orderRate": 40,
  "standardUnitCost": 1280,
  "discountUnitCost": null,
  "beginDate": "2026-06-01",
  "endDate": "2026-12-31",
  "updateReason": "단가 인상 협의 반영"
}
```

**Response (목록·상세)**

```json
{
  "id": 42,
  "type": "PURCHASE",
  "itemId": 10,
  "itemNum": "M-100",
  "itemName": "원자재A",
  "propertyClassification": "원자재",
  "companyId": 5,
  "companyName": "○○철강",
  "businessRegistrationNum": "123-45-67890",
  "orderRate": 60,
  "standardUnitCost": 1250.5,
  "discountUnitCost": 1200,
  "beginDate": "2026-06-01",
  "endDate": null,
  "beginProcessCodeId": null,
  "endProcessCodeId": null
}
```

---

## 8. 현행 B3 구현과의 차이 (갭)

| 항목 | 현행 B3 | TO-BE (본 스펙) |
|------|---------|-----------------|
| UI | toolbar `FormSelect` + Modal | **3탭 버튼** + Modal |
| API 식별 | `itemNum`, `businessRegistrationNum` | **`itemId`, `companyId`** |
| 외주 공정 | `beginProcessCode` VARCHAR | **`beginProcessCodeId` FK** |
| 자산분류 검증 | 없음 | §5.1 |
| 발주비율 100% | 없음 | §5.4 |
| 비관적 락 | 없음 | §5.4.1 |
| 수정 변경 사유 | nullable | **필수** |
| 할인단가·종료일 | 스키마만, UI 미노출 | Modal **노출** |
| 판매 발주비율 | 100 기본 입력 가능 | **0 고정·비활성** |
| 0% MRP 제외 | 없음 | 후속 TX/PRD |
| 외주창고 연동 | api-spec 레거시 문구 | **금지** |

---

## 9. 구현·후속 Wave

### 9.1 B3-R — 단가

- [ ] Flyway: `begin_process_code_id`, `end_process_code_id` FK → `public_code(id)`; VARCHAR 공정코드 **제거**
- [ ] UK 컬럼을 FK id 기준으로 **재정의**
- [ ] `UnitCostService` — §5.1 자산분류·§5.2 거래처·§5.3 NULL·§5.4 발주비율·§5.4.1 락
- [ ] `UnitCostRequest` — `itemId`, `companyId`, `beginProcessCodeId`, `endProcessCodeId`; PUT `updateReason` 필수
- [ ] `UnitCostRepository` — `(cost_type, item_id)` 활성 행 합계 쿼리 + `@Lock`
- [ ] 프론트 — 3탭, Modal 할인·종료일·변경사유, 탭별 Item/Company/Process 필터
- [ ] [basis-information-api-spec.md](./basis-information-api-spec.md) §5.8 동기화
- [ ] [basis-public-code-spec.md](./basis-public-code-spec.md) §6.3 — `unit_cost.begin_process_code_id` 참조

### 9.2 후속 (TX / PRD)

- [ ] 구매발주·외주계획 **확정 시** 발주비율 **= 100%** 검증
- [ ] MRP·발주 수량 분할 — `order_rate = 0` **제외**
- [ ] 전표일 기준 **유효 단가** resolve
- [ ] (선택) 수정 Modal 하단 **이력 접이식** 그리드

---

## 10. 의사결정 요약

| 항목 | 확정 |
|------|------|
| 테이블 | `unit_cost` + `unit_cost_history` 통합 3구분 |
| PK | `id` |
| UK | `(cost_type, item_id, company_id, begin_date, begin/end_process_code_id)` |
| UI | **3탭 1화면** + 목록 + Modal (**2분할 없음**) |
| 이력 UI | **1차 없음** — 수정 시 DB 이력 + 변경 사유 필수 |
| 발주비율 | 등록 **≤100%**, 확정 **=100%**, **0% MRP 제외** |
| 외주창고 | 단가 등록 시 **연동 금지** |
| API SoT | **basis-information-api-spec §5.8** |

---

## 11. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-06-24 | 초안 — `260624_기준정보_단가.md` + 3탭·Modal·B3-R 검증 확정 |
