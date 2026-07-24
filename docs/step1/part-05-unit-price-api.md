# Step 1 Part 05 — 외주단가 + `OutsourceInputBalanceProjector`

> **완료일:** 2026-07-03  
> **범위:** 단가 3종 CRUD REST API · `OutsourceUnitPriceRegistered/Updated/Deleted` · `OutsourceInputBalanceProjector` · React 단가 화면(3탭)  
> **설계 SSOT:** [`docs/step0/d4-unit-price.md`](../step0/d4-unit-price.md) v0.1  
> **다음 파트:** [Part 06 — Projector 정합](./part-06-projector-alignment.md) (외주 400·BOM) · Lot는 [`d5-lot-traceability.md`](../step0/d5-lot-traceability.md) v0.1 초안만 — **구현은 Part 06 이후**

---

## 1. 요약

| 항목 | 결과 |
|------|------|
| Flyway | `V006__unit_price.sql` — `unit_price`, `unit_price_change_log`, `inventory_balance.input_process_id` |
| API | `POST/GET/PUT/DELETE /api/v1/basis/unit-prices` + `GET /{id}/history` |
| 부수효과 | `OutsourceUnitPriceRegistered` → `OutsourceInputBalanceProjector.ensure` → `inventory_balance` (location=`OUTSOURCE`, 투입) |
| 이력 | PUT 시 `unit_price_change_log` 스냅샷 (`updateReason` 필수) |
| Projector 1차 | 앞 공정 역탐색(INHOUSE·SPLIT)만 구현 — **BOM walk는 Step 2** |
| Gradle·프론트 빌드 | 성공 |
| API 스모크 | `GET /unit-prices?type=OUTSOURCE` → 200 |

---

## 2. API 명세

Base: `/api/v1/basis/unit-prices`

| Method | Endpoint | 설명 |
|--------|----------|------|
| `GET` | `/?type=SALE\|PURCHASE\|OUTSOURCE&q=` | 탭별 목록·검색 |
| `GET` | `/{id}` | 상세 |
| `GET` | `/by-item/{itemNum}?type=` | 품목별 보조 |
| `POST` | `/` | 등록 (`itemNum` → `item_id` resolve) |
| `PUT` | `/{id}` | 수정 (`updateReason` 필수, UK 키 읽기전용) |
| `DELETE` | `/{id}` | 소프트 삭제 |
| `GET` | `/{id}/history` | 변경 이력 |

### 2.1 외주단가 등록 예시

```json
POST /api/v1/basis/unit-prices
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

### 2.2 수정 예시

```json
PUT /api/v1/basis/unit-prices/{id}
{
  "orderRate": 40,
  "standardUnitCost": 1280,
  "beginDate": "2026-06-01",
  "endDate": "2026-12-31",
  "updateReason": "단가 인상 협의 반영"
}
```

---

## 3. 검증 규칙

| 구분 | 규칙 |
|------|------|
| 품목 자산분류 | PURCHASE: 원자재·상품·부자재 / SALE: 제품·상품·공정품 / OUTSOURCE: 제품·공정품 |
| 거래처 역할 | SALE→`SALES`, PURCHASE→`PURCHASE`, OUTSOURCE→`OUTSOURCE` |
| 외주 공정 | `beginProcessCodeId`·`endProcessCodeId` 필수, 품목 plan에 존재·순번 유효 |
| 외주 품목 | plan에 `OUTSOURCE` 또는 `SPLIT` 공정 1건 이상 (시작·종료 공정도 외주·혼합만) |
| 발주비율 | SALE=0 고정 / 동일 `(type,item_id)` 합계 ≤100% / 외주는 공정구간별 합계 ≤100% |
| UK | `(cost_type, item_id, company_id, begin_date, begin/end_process_code_id)` 활성 1건 |

---

## 4. `OutsourceInputBalanceProjector`

Ref: [`domain-event-projector-matrix.md`](../step0/domain-event-projector-matrix.md) §4.5

1. 외주단가의 **시작공정** 순번 기준, 그 이전 공정을 역순 탐색
2. `WorkDistinction` ≠ `OUTSOURCE` 인 공정 → `OUTSOURCE` INPUT 잔고 ensure  
   UK: `(item_id, company_id, input_process_id)`
3. 앞 공정 없음 → BOM 하위 품목 탐색 — **[Part 06](./part-06-projector-alignment.md)에서 구현**
4. 공정·BOM 모두 없음 → **400** 「하위 출고 공정 또는 원자재를 찾을 수 없음」

| 이벤트 | 동작 |
|--------|------|
| `OutsourceUnitPriceRegistered` | `ensure` |
| `OutsourceUnitPriceUpdated` | `rebuild` (기존 투입 슬롯 비활성 + 재 ensure) |
| `OutsourceUnitPriceDeleted` | `deactivate` |

---

## 5. 프론트엔드

- 네비 **단가** 탭 추가 (`UnitPricePage.tsx`)
- 서브탭: 판매 / 구매 / **외주** (기본 선택)
- `ItemSearchField` — 탭별 자산분류 필터 (`allowedClassifications`)
- `CompanySearchField` — 탭별 `partnerType` 필터
- 외주 탭: 시작·종료 공정 콤보 (`PROCESS_CODE` options)

---

## 6. 주요 파일

| 영역 | 경로 |
|------|------|
| 마이그레이션 | `V006__unit_price.sql` |
| 서비스 | `application/unitprice/UnitPriceService.java` |
| Projector | `OutsourceInputBalanceProjector.java`, `UnitPriceHistoryProjector.java` |
| API | `api/web/unitprice/UnitPriceController.java` |
| 프론트 | `pages/UnitPricePage.tsx`, `api/unitPrice.ts` |

---

## 7. E2E 수동 검증 체크리스트

1. 백엔드 재기동 (`V006` 적용 확인)
2. 거래처에 `OUTSOURCE` 역할 부여
3. 품목 plan: `INHOUSE` + `OUTSOURCE` 공정 등록
4. 외주단가 등록 → `inventory_balance`에 `OUTSOURCE`·`input_process_id` 행 생성 확인
5. 수정(변경사유) → `unit_price_change_log` 행 추가
6. 삭제 → 단가 `recording_state=0`, 투입 잔고 비활성

**검증 결과 (2026-07-03):** API 기준 PASS — 상세·케이스 B/C 전제는 [Part 06 §6](./part-06-projector-alignment.md#6-e2e-수동-검증-체크리스트) 참고.

---

*Part 05 완료*
