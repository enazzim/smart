# 단계 4 E2E 검증 결과 — 자재투입 여부 시스템 설정 (NO/YES)

> **검증일:** 2026-07-06  
> **대상:** `WO-20260706-2` (품목 `abc`, 최종 사내공정 `조립`, 잔량 500)  
> **스크립트:** [scripts/e2e-step4-verify.ps1](../../scripts/e2e-step4-verify.ps1)  
> **설정 키:** `production.material_issue.enabled` (기본 `NO`)

---

## 1. 시나리오

```text
[Phase A — NO / 백플러시]
  설정 NO → 자재투입 API 차단
  → issue-on-hand · consumption-status(materialIssueEnabled=false)
  → 작업일보 good=10 (BOM 자동 차감) → SALES +10
  → 일보 취소 → RAW·지시 원복 검증

[Phase B — YES / PRD-W2]
  설정 YES → 일보 선행 차단(V2 가드)
  → 자재투입 MI → consumption 충족
  → 작업일보 WR (투입 라인 없음) → step2 흐름
  → 정리: WR/MI 취소, 설정 NO 복원
```

---

## 2. 체크리스트

| ID | 항목 | 결과 | 근거 |
|----|------|------|------|
| **E1** | 설정 `NO` 저장·조회 | **PASS** | `GET /system/settings` → `NO` |
| **E2a** | NO 시 자재투입 등록 차단 | **PASS** | `POST /material-issues` → 400 |
| **E2b** | `issue-on-hand` API (2 BOM 라인) | **PASS** | abc-r·abc-t 가용재고 반환 |
| **E2b** | `consumption-status` `materialIssueEnabled=false` | **PASS** | |
| **E2c** | 백플러시 RAW 차감 | **PASS** | abc-r **−20**, abc-t **−10** (good=10, 단위소요 2·1) |
| **E3** | **최종 사내공정** 산출 → SALES IN | **PASS** | abc SALES **+10**, WIP Δ0 |
| **E4** | 취소 시 RAW·`reported_qty`·SALES 원복 | **PASS** | abc-r·abc-t RAW 복원, SALES 복원, reported 0 유지 |
| **E5a** | 설정 `YES` 저장·조회 | **PASS** | |
| **E5b** | `materialIssueEnabled=true` | **PASS** | |
| **E5c** | 투입 없이 일보 차단 (V2 가드) | **PASS** | |
| **E5d** | 자재투입 후 `allSatisfied=true` | **PASS** | |
| **E5e** | 자재투입 → 일보 (step2 흐름) | **PASS** | `MI-20260706-10` → `WR-20260706-18` |

**단계 4 종합: PASS** (16/16)

---

## 3. E3 검증 기준 (정정 반영)

작업일보는 **공정별** 실적이며, 산출 재고는 공정 위치에 따라 다릅니다.

| 공정 유형 | 산출 반영 | 본 검증 |
|-----------|-----------|---------|
| 중간 사내공정 | WIP[다음 공정] IN | `test` 품목 별도 시나리오 (WIP +양품, SALES 무변동) |
| **최종 사내공정** | **SALES IN** | **본 E2E** — `abc` good=10 → SALES +10 |

---

## 4. E4 SALES 취소 이슈 (해결)

- **증상:** 백플러시(NO) 일보 등록 후 취소 시 RAW·투입은 원복되나 **SALES OUT(역전기) 미반영**으로 +10 잔존.
- **원인:** `WorkReportInventoryService.apply()` 호출부에서 `reverse` 시 movement type을 미리 반전한 뒤, `record()` 내부 `flip()`으로 **이중 반전** → 취소 시에도 SALES `IN`이 기록됨.
- **수정:** 등록 시 movement type(`OUT`/`IN`)만 전달하고, 취소 반전은 `record()`의 `flip()`에 일원화.
- **재검증:** 2026-07-06 — E4 SALES 원복 **PASS** (16/16).

---

## 5. 재실행

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File scripts/e2e-step4-verify.ps1
```

**전제:** API `http://localhost:8080`, `WO-20260706-2` 잔량 ≥ 10, abc-r RAW ≥ 20, abc-t RAW ≥ 10.

---

## 6. 문서 이력

| 일자 | 내용 |
|------|------|
| 2026-07-06 | 단계 4 최초 검증 — 설정 NO/YES 분기, E4 SALES 취소 갭 기록 |
| 2026-07-06 | `WorkReportInventoryService` 이중 flip 수정 후 재검증 — **16/16 PASS** |
