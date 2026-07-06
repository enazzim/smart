# 단계 1 통합 E2E 검증 결과

> **검증일:** 2026-07-06  
> **대상:** `WO-20260706-2` (품목 `abc`, 공정 `조립`, 지시수량 500)  
> **전제 데이터:** 무검사 구매입고 2건 (`abc-r` 1000, `abc-t` 500 누적)  
> **스크립트:** [scripts/e2e-step1-verify.ps1](../../scripts/e2e-step1-verify.ps1)

---

## 1. 시나리오 요약

```text
[사전] 무검사 구매입고 → RAW (abc-r 1000, abc-t 500)
[검증] 부분 일보(양품 100·불량 10, abc-r만 투입) → 잔량 일보(양품 400) → 일보 취소 → 재고·지시 원복
```

---

## 2. 체크리스트 결과

| ID | 항목 | 결과 | 근거 |
|----|------|------|------|
| **E1** | 무검사 입고 후 RAW 현재고 = 입고 누적 (`RAW`, `output_process_id` NULL) | **PASS** | `issue-on-hand`: abc-r **1000**, abc-t **500** (입고·취소 리셋 후) |
| **E2** | 체크한 투입요소만 RAW 차감 | **PASS** | 부분 일보(abc-r만): abc-r 1000→**800** (−200), abc-t **500 유지** |
| **E3** | 작업지시 `reported_qty`·잔량 = 양품 누적 | **PASS** | 부분 등록 후 `reported=100`, `remaining=400` (작업수량 110 중 양품 100만 반영) |
| **E4** | 최종 사내공정 일보 후 SALES IN | **PASS** | 잔량 400 일보 등록 시 `abc` SALES 4000→**4500** (+500 누적 양품) |
| **E5** | 일보 취소 시 투입·지시 원복 | **PASS** | WR-20260706-4 취소 후 RAW abc-r **1000**·abc-t **500** 복원, `reported=0` `remaining=500` |

**단계 1 종합: PASS** (핵심 재고·지시 연동 정상)

---

## 3. 실행 상세

### 3.1 E1 — 입고·현재고

- 구매입고 이력: `PR-20260706-001`, `PR-20260706-002` (무검사 즉시반영)
- API `GET /work-reports/issue-on-hand?workOrderId=4` → abc-r 1000, abc-t 500

### 3.2 E2 — 선택적 투입 차감

- `POST /work-reports` — `WR-20260706-3`
  - `goodQty=100`, `scrapQty=10`
  - `issueLines`: abc-r만 200 (단위소요 2×100)
- 투입 미체크 abc-t: 재고 변동 없음

### 3.3 E3 — 지시 실적

| 시점 | reported_qty | remaining |
|------|--------------|-----------|
| 부분 일보 후 | 100 | 400 |
| 잔량 일보 후 | 500 | 0 |
| 잔량 일보 취소 후 | 0 | 500 |

### 3.4 E4 — 완성품 SALES

- 조립(최종 사내공정) 양품 500 누적 시 `inventory_balance` `abc` / `SALES` **+500** (4000→4500, 기존 테스트 잔량 4000 포함)

### 3.5 E5 — 취소 역전기

- `POST /work-reports/19/cancel` (`WR-20260706-4`)
- RAW 원복: abc-r 1000, abc-t 500
- 작업지시 잔량 500 복원

---

## 4. 발견 사항 (후속 조치)

| # | 내용 | 심각도 | Wave |
|---|------|--------|------|
| F1 | `inventory_balance`에 **RAW 이중 슬롯** (`output_process_id` NULL vs materialProcess) — 과거 materialProcess 버그 잔재, 빈 슬롯(0) 존재 | 낮음 | 정리 마이그레이션 또는 슬롯 병합 |
| F2 | `abc` SALES **4000** 등 기존 테스트 데이터 잔존 — E4 증분 검증 시 기준값 주의 | 정보 | 테스트 데이터 정리 |
| F3 | 현행: **일보 등록 = 투입 차감 통합** (PRD-W2 설계의 별도 `material_issue` TX 아님) | 정보 | 단계 2 PRD-W2 |

---

## 5. 다음 단계

- [x] **단계 1** 통합 E2E 검증
- [x] **단계 2** PRD-W2 — `material_issue` TX, 투입 충족 가드 ✅ ([e2e-step2-prd-w2-verification-results.md](./e2e-step2-prd-w2-verification-results.md))
- [x] **단계 3** 로드맵 — PRD-W1b, INF-1b, TX1-R(검사품) ✅ [e2e-step3](./e2e-step3-verification-results.md)

참고: [implementation-roadmap-and-exceptions.md](./implementation-roadmap-and-exceptions.md)

---

## 6. 재실행 방법

```powershell
# API 기동 후
& 'C:\Program Files\MariaDB 11.4\bin\mysql.exe' -uroot -p1111 smartmanager -e "SELECT id,report_num,status FROM work_report WHERE work_order_id=4"

# 활성 일보가 있으면 취소 후
powershell -File scripts/e2e-step1-verify.ps1
```

---

## 7. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-07-06 | 최초 E2E 검증 — 5항목 PASS |
