# PRD-W2 통합 E2E 검증 결과

> **검증일:** 2026-07-06  
> **대상:** `WO-20260706-2` (품목 `abc`, 공정 `조립`, 지시수량 500)  
> **흐름:** 입고(기존 RAW) → **자재투입** → **작업일보** (PRD-W2 분리 TX)

---

## 1. 시나리오

```text
[사전] 무검사 구매입고(PR-20260706-004) → RAW abc-r 1000, abc-t 500
[①]  자재투입 MI — 양품 500 기준 BOM 출고
[②]  작업일보 WR — 양품 500 실적 (투입 충족 가드 통과)
```

---

## 2. 체크리스트

| ID | 항목 | 결과 | 근거 |
|----|------|------|------|
| **E1** | 입고 후 RAW 가용재고 충분 | **PASS** | `issue-on-hand`: abc-r **1000**, abc-t **500** (PR-20260706-004 반영) |
| **E2a** | 투입 없이 일보 등록 차단 (V2 가드) | **PASS** | `400` — `자재투입이 부족합니다: abc-r (필요 200, 투입 0)` |
| **E2b** | 자재투입 등록 → RAW OUT | **PASS** | `MI-20260706-1` — abc-r·abc-t 각 **0** (1000+500 출고) |
| **E2c** | 투입 후 `consumption-status` 충족 | **PASS** | `allSatisfied=true`, issued r=1000 t=500 |
| **E3** | 작업일보 등록 → 지시 실적 | **PASS** | `WR-20260706-5` — reported=**500**, remaining=**0** |
| **E4** | 최종공정 → SALES IN | **PASS** | `abc` SALES 5000→**5500** (+500) |

**PRD-W2 E2E 종합: PASS**

---

## 3. 검증 중 조치

### 3.1 첫 공정 WIP OUT (409 → 수정)

- **증상:** 자재투입 후 일보 등록 시 `409` — `재고 부족 itemId=3, location=WIP`
- **원인:** PRD-W2에서 첫 사내공정 WIP OUT skip 제거 → 단일공정 품목(`abc`)은 모품목 WIP 잔고 0인데 OUT 시도
- **조치:** `WorkReportInventoryService` — **첫 사내공정은 모품목 WIP OUT 생략** 복원 (자재투입은 하위품만 출고)
- **재검증:** E3·E4 **PASS**

### 3.2 API 재기동

- Flyway **V043** (`material_issue`) 적용 확인
- `restart-api.bat` 후 health `UP`

---

## 4. 최종 DB 상태 (검증 종료)

| 항목 | 값 |
|------|-----|
| 작업지시 | reported=500, remaining=0 |
| 자재투입 | `MI-20260706-2` (ISSUED) |
| 작업일보 | `WR-20260706-6` (REGISTERED) |
| RAW | abc-r **0**, abc-t **0** |
| SALES | abc **5500** |

---

## 5. 재실행 (요약)

```powershell
# 로그인 후
# 1) GET material-issues/issue-on-hand?workOrderId=4
# 2) POST material-issues { lines: abc-r 1000, abc-t 500 }
# 3) GET work-reports/consumption-status?pendingGoodQty=500 → allSatisfied
# 4) POST work-reports { goodQty: 500 }
```

---

## 6. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-07-06 | PRD-W2 E2E — 자재투입·일보 분리·가드 검증 PASS |
