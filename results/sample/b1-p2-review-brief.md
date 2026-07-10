# B1 P2 현업 리뷰 브리프 (품목구성·작업장·작업표준·사용자 등)

> **일자:** 2026-07-10  
> **선행:** P1 리뷰(품목·공정·단가) · P0+P1 E2E PASS  
> **체크리스트:** [`docs/step0/b1-business-d4-review-checklist.md`](../../docs/step0/b1-business-d4-review-checklist.md) §H  
> **회의 진행안:** [`b1-p2-review-meeting-runbook.md`](./b1-p2-review-meeting-runbook.md)

---

## 1. 리뷰 목적

P1 이후 **나머지 기준정보 메뉴** 설계 확인 (3차, 약 60분).

| 차수 | 범위 |
|------|------|
| 3차 P2 | 품목구성 · 작업장 · 설비 · 작업표준 · 생산달력 · 사용자 |

---

## 2. 시연 데이터 (SEED-OP)

| 메뉴 | 시드 내용 |
|------|-----------|
| 품목구성 | 26 lines — FG-01: SF-01 + RM-PA/PB, SF-01: RM-W01/W02 |
| 작업장 | WC-Cut-01/02, WC-Form-01/02, WC-Weld-01 (5) |
| 작업표준 | INHOUSE 공정 10건 — worker01 주작업자 |
| 사용자 | worker01~10 / `Oper1234!` — 역할·업무일지 그룹 |
| 설비 | (시드 미포함 — 화면 구조만 시연 또는 수동 1건) |
| 생산달력 | Flyway 시드 + `e2e-calendar-capa.ps1` 검증 가능 |

---

## 3. 메뉴별 확인 포인트 (§H)

### 품목구성 ([d4-bom-line.md](../../docs/step0/d4-bom-line.md))

- Modal **4필드** (모품목·자품목·모수량·자수량)
- FG-01 **정전개** → leaf 4 (SF-01 + 원자재 3종)
- P0-1 MRP·외주 투입이 BOM explosion 기반

### 작업장 ([d4-work-center.md](../../docs/step0/d4-work-center.md))

- **3필드** (명칭·주공정코드·가동시간)
- 작업장별 달력 **복사 폐지** — Override만

### 설비 ([d4-equipment.md](../../docs/step0/d4-equipment.md))

- **6필드** 슬림
- 위치·단위 Drop

### 작업표준 ([d4-work-standard.md](../../docs/step0/d4-work-standard.md))

- **9필드** — 품목·공정·작업장·주작업자·시간
- 작업일보 자동 대입은 **Phase2** (현재 E2E는 수동 workerName)

### 생산달력 ([d4-calendar.md](../../docs/step0/d4-calendar.md))

- 기본 달력 + 작업장 Override
- 분 단위 CAPA (`e2e-calendar-capa.ps1`)

### 사용자 ([d4-user.md](../../docs/step0/d4-user.md))

- **7필드** · RBAC 역할
- 단축메뉴 20개 → **Drop** (역할 권한으로 대체)

---

## 4. E2E 연결

| 검증 | P2 메뉴 연관 |
|------|-------------|
| P0-1 FG-01 TX | BOM → MRP / 외주 투입 / 작업표준·작업장 |
| Part06 | BOM walk · 외주단가 Projector |
| `e2e-calendar-capa.ps1` | 생산달력·CAPA |

---

## 5. 서명

체크리스트 §I **3차 (P2)** 서명.

| 역할 | 3차 (P2) | 일자 |
|------|:--------:|------|
| 기준정보 현업 | [ ] | |
| 개발 리드 | [ ] | |
| PM / 설계 | [ ] | |
