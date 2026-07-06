# 단계 3 E2E 검증 결과 (PRD-W1b · INF-1b · TX1-R)

> **검증일:** 2026-07-06  
> **범위:** 로드맵 §2.3 — 작업장 부하 · 재고·원장 조회 · 검사품 입고 2단계

---

## 1. 체크리스트

| ID | Wave | 항목 | 결과 | 근거 |
|----|------|------|------|------|
| **S3-W1** | PRD-W1b | 작업장×일자 부하 API | **PASS** | `GET /scheduling/work-center-load` — 31일, `warnLoadThreshold=0.8` |
| **S3-I1** | INF-1b | `stock_movement` 조회 API | **PASS** | `GET /inventory/stock-movements?itemNo=abc` — 72건 |
| **S3-I2** | INF-1b | `inventory_balance_monthly` 조회 API | **PASS** | `GET /inventory/balances?itemNo=abc` — 3슬롯, 월별 데이터 |
| **S3-I3** | INF-1b | 재고·원장 UI | **구현** | `InventoryLedgerPage` — 구매 메뉴 `재고·원장` |
| **S3-T1** | TX1-R | 검사품 입고 → QI PENDING, RAW 변화 없음 | **PASS** | `test-r` 20입고, RAW 0 유지, QI id=1 |
| **S3-T2** | TX1-R | 검사완료 → 합격분 RAW IN | **PASS** | 합격 18 → RAW **+18** |

**단계 3 종합: PASS**

---

## 2. 신규 구현 (INF-1b UI)

| 산출물 | 경로 |
|--------|------|
| Flyway V044 | `inventory:ledger:read` 권한 |
| API | `GET /api/v1/inventory/stock-movements`, `/balances` |
| 서비스 | `InventoryLedgerQueryService`, `JpaInventoryLedgerRepository` |
| 화면 | `InventoryLedgerPage.tsx`, `api/inventoryLedger.ts` |

---

## 3. 기존 구현 확인

| Wave | 상태 | 비고 |
|------|------|------|
| PRD-W1b | ✅ 기구현 | `WorkCenterLoadPage`, `SchedulingController` |
| TX1-R Wave 1~4 | ✅ 기구현 | `PurchaseReceiptService`, `QualityInspectionService`, 화면 |
| TX1-R5 E2E | ✅ 본 검증 | 검사품 2단계 시나리오 통과 |

---

## 4. TX1-R 검증 상세

```text
1. test-r → check_distinction=INSPECTION (검증용)
2. PO line 29, 입고 20 → PR-20260706-005
3. RAW: 0 → 0 (창고 미반영)
4. QI PENDING request_qty=20
5. 검사완료: 합격 18, 불량 2
6. RAW: 0 → 18
```

---

## 5. 재실행

```powershell
# W1b
GET /api/v1/production/scheduling/work-center-load?workCenterId=2&from=2026-07-01&to=2026-07-31

# INF-1b
GET /api/v1/inventory/stock-movements?itemNo=abc
GET /api/v1/inventory/balances?itemNo=abc&fiscalYear=2026

# TX1-R — 검사품 발주 라인 + 품질검사 화면
```

---

## 6. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-07-06 | 단계 3 E2E — W1b/INF-1b/TX1-R PASS, INF-1b 조회 UI 추가 |
