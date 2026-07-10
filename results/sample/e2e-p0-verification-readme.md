# P0 E2E verification scripts

> **Updated:** 2026-07-10  
> **Scope:** P0 TX chain, payable approval, month closing + optional Part06 outsource

---

## 1. Script list

| # | Script | Scope |
|---|--------|-------|
| **P0-1** | [scripts/e2e-full-tx-chain-verify.ps1](../../scripts/e2e-full-tx-chain-verify.ps1) | SO -> plan -> MRP -> PO -> receipt -> **OO/OSh/OR** -> WO -> WR -> shipment -> revenue (+ cancel) |
| **P0-2** | [scripts/e2e-payable-approval-verify.ps1](../../scripts/e2e-payable-approval-verify.ps1) | Receipt PENDING -> approve -> unpaid -> cancel |
| **P0-3** | [scripts/e2e-month-closing-verify.ps1](../../scripts/e2e-month-closing-verify.ps1) | Close month -> block TX -> reopen cleanup |
| **P1-4** | [scripts/e2e-part06-outsource-verify.ps1](../../scripts/e2e-part06-outsource-verify.ps1) | Outsource unit price projector (P1/P2 neg, Case B/C) |
| **Batch P0** | [scripts/e2e-p0-verify.ps1](../../scripts/e2e-p0-verify.ps1) | P0-1 ~ P0-3 |
| **Batch P0+P1** | [scripts/e2e-p1-verify.ps1](../../scripts/e2e-p1-verify.ps1) | P0-1 ~ P0-3 + Part06 |
| **Common** | [scripts/e2e-common.ps1](../../scripts/e2e-common.ps1) | Login, REST, PASS/FAIL tracker |
| **Seed** | [scripts/seed-operational-basis.ps1](../../scripts/seed-operational-basis.ps1) | Operational basis master data |

---

## 2. BAT shortcuts

| BAT | Action |
|-----|--------|
| `run-seed-operational-basis.bat` | Seed only |
| `run-e2e-p0-verify.bat` | E2E P0-1~3 only |
| `run-e2e-p1-verify.bat` | E2E P0-1~3 + Part06 |
| `run-p0-pipeline.bat` | Seed + P0 E2E |
| `run-clean-test-db.bat` | Truncate TX tables (basis kept) |
| `run-drop-test-db.bat` | DROP DATABASE |
| `run-fresh-p0-pipeline.bat` | Stop API -> DROP -> restart API -> wait health -> seed + P0 |
| `run-full-e2e-pipeline.bat` | `run-fresh-p0-pipeline.bat` + Part06 only |

> **Tip:** After `run-fresh-p0-pipeline.bat`, run `run-e2e-p1-verify.bat` only for Part06 add-on (avoids duplicate P0).

---

## 3. Prerequisites

1. API `http://localhost:8080` (`GET /api/health` 200)
2. Login `admin` / `Admin123!`
3. Seed via `run-seed-operational-basis.bat`
4. P0-1 uses `production.material_issue.enabled=NO` (backflush)

### Seed summary (SEED-OP)

| Item | Count | Rule |
|------|-------|------|
| Companies | 17 | Sales 5 + Purchase 10 + Outsource 2 |
| Items | 17 | FG 5 + SF 5 + Raw 7 |
| BOM | 26 lines | FG-01 multi-level (SF-01 + raw) |
| Unit prices | **30** | Sale 1/FG, Purchase 1/raw, **Outsource 1/OUTSOURCE process @ 100%** |
| Users | 10 | worker01~10 / `Oper1234!` |

Default E2E product: **FG-01**. Default raw (P0-2/3): **RM-PA**.

---

## 4. Run commands

```powershell
cd C:\Users\LG\Desktop\신동공업\SmartManager

# Cleanest full run (DROP + API + seed + P0)
.\run-fresh-p0-pipeline.bat

# Add Part06 after fresh P0
.\run-e2e-p1-verify.bat

# TX only cleanup then re-run
.\run-clean-test-db.bat
.\run-p0-pipeline.bat
```

### Environment variables

| Variable | Default | Description |
|----------|---------|-------------|
| `SM_E2E_BASE_URL` | `http://localhost:8080` | API base |
| `SM_E2E_LOGIN_ID` | `admin` | Login ID |
| `SM_E2E_PASSWORD` | `Admin123!` | Password |
| `SM_E2E_TX_DATE` | today | TX date (yyyy-MM-dd) |
| `SM_E2E_PRODUCT_ITEM` | `FG-01` | P0-1 / Part06 product |
| `SM_E2E_RAW_ITEM` | `RM-PA` | P0-2/3 raw item |
| `SM_E2E_ORDER_QTY` | `10` | P0-1 order qty |
| `SM_E2E_SKIP_CANCEL` | — | `1` skips P0-1 cancel phase |
| `SM_E2E_SKIP_OUTSOURCE` | — | `1` skips P0-1 Phase 4a outsource TX |
| `SM_E2E_CLOSE_YEAR` / `SM_E2E_CLOSE_MONTH` | prior month | P0-3 closing target |

---

## 5. Results

- Latest run: [e2e-p0-verification-results.md](./e2e-p0-verification-results.md)
- B1 P1 review brief: [b1-p1-review-brief.md](./b1-p1-review-brief.md)
- B1 P1 meeting runbook: [b1-p1-review-meeting-runbook.md](./b1-p1-review-meeting-runbook.md)
- B1 P2 review brief: [b1-p2-review-brief.md](./b1-p2-review-brief.md)
- B1 P2 meeting runbook: [b1-p2-review-meeting-runbook.md](./b1-p2-review-meeting-runbook.md)

---

## 6. Known limits

- Re-running E2E **accumulates** TX data. Use `run-clean-test-db.bat` or `run-fresh-p0-pipeline.bat` for a clean state.
- Seed is **idempotent** (`Ensure-*`): existing master rows are skipped, not updated.
- `run-full-e2e-pipeline.bat` = fresh P0 + Part06 (P0 중복 없음).
