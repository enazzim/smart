# Step 1 Part 06 — Projector 정합 (외주단가 · BOM)

> **완료일:** 2026-07-03  
> **선행:** [Part 05 — 외주단가](./part-05-unit-price-api.md)  
> **Lot:** [`d5-lot-traceability.md`](../step0/d5-lot-traceability.md) v0.1 초안만 — **본 파트 범위 밖**

---

## 1. 요약

| 항목 | 결과 |
|------|------|
| Flyway | `V007__item_composition.sql` — `item_composition`, `bom_change_log` |
| BOM API | `POST/GET/PUT/DELETE /api/v1/basis/item-composition/plan` |
| `BomHistoryProjector` | 등록·수정·삭제 시 `bom_change_log` 스냅샷 |
| `OutsourceInputBalanceProjector` | 앞 공정 walk + BOM walk + **400 거부** |
| 소재공정 | 원자재 BOM 투입 시 `ProcessRepository.ensureMaterialProcess` (14000000) |
| Gradle 빌드 | 성공 |

---

## 2. `OutsourceInputBalanceProjector` (완료)

Ref: [`domain-event-projector-matrix.md`](../step0/domain-event-projector-matrix.md) §4.5

**Phase 1 — 앞 공정 역탐색 (Part 05)**

1. 외주단가 시작공정 순번 **이전** 공정만 대상
2. `WorkDistinction` ≠ `OUTSOURCE` → `(모품목 item_id, input_process_id)` 슬롯 ensure

**Phase 2 — BOM walk (Part 06)**

앞 공정 슬롯이 **0건**일 때 `item_composition` 직계 하위 탐색:

| 자품목 분류 | 동작 |
|-------------|------|
| 원자재 | `(child_item_id, 소재공정 process_id)` — `ensureMaterialProcess` |
| 공정품 | 자품목 plan 공정 역탐색 → 없으면 BOM 재귀 (1단) |

**Phase 3 — 400 거부**

Phase 1·2 후 슬롯 0건 → `IllegalArgumentException` → HTTP 400  
메시지: **「하위 출고 공정 또는 원자재를 찾을 수 없음」**

`rebuild`: 신규 슬롯 검증 **후** 기존 비활성 → 신규 ensure (검증 실패 시 기존 슬롯 유지)

---

## 3. BOM API

Base: `/api/v1/basis/item-composition/plan`

| Method | Endpoint | 설명 |
|--------|----------|------|
| `GET` | `/` | 목록 (`parentItemNum`, `childItemNum` 필터) |
| `GET` | `/by-parent/{parentItemNum}` | 모품목 하위 BOM |
| `GET` | `/id/{id}` | 단건 |
| `POST` | `/` | 4필드 등록 |
| `PUT` | `/{id}` | 수량만 수정 |
| `DELETE` | `/{id}` | 소프트 삭제 |
| `GET` | `/{itemNum}/explosion` | BOM 정전개 (트리) |
| `GET` | `/{itemNum}/reverse` | 역전개 (1레벨) |
| `POST` | `/copy` | BOM 복사 (직계 1레벨) |

---

## 4. 프론트엔드

- 네비 **품목구성** 탭 (`ItemCompositionPage.tsx`)
- 상단 **인라인 등록·수정** 폼 + 목록 검색 (다른 기준정보와 동일 패턴)
- 툴바: **정전개** · **역전개** · **BOM복사** (결과·복사만 Modal)

---

## 5. 주요 파일

| 영역 | 경로 |
|------|------|
| 마이그레이션 | `V007__item_composition.sql` |
| BOM | `application/bom/ItemCompositionService.java`, `BomHistoryProjector.java` |
| Projector | `OutsourceInputBalanceProjector.java` |
| 소재공정 | `ProcessRepository.ensureMaterialProcess` |
| API | `api/web/bom/ItemCompositionController.java` |
| 프론트 | `pages/ItemCompositionPage.tsx`, `api/itemComposition.ts` |

---

## 6. E2E 수동 검증 체크리스트

### 6.0 공통 전제 (외주단가 등록)

`UnitPriceService`가 Projector **이전**에 검증합니다. 아래를 먼저 만족해야 케이스 A~C를 재현할 수 있습니다.

| # | 전제 | 미충족 시 HTTP 400 메시지 |
|---|------|---------------------------|
| P1 | 거래처에 `OUTSOURCE` 역할 | 거래처 역할이 단가 구분과 일치하지 않습니다. |
| P2 | 품목 plan에 **INHOUSE(자가) 또는 혼합(자가/외주) 공정 1건 이상** | **외주단가는 자가·혼합 공정이 1건 이상 등록된 품목만 가능합니다.** |
| P3 | 외주단가의 시작·종료 공정이 해당 품목 plan에 존재 | 품목 공정 계획에 공정이 없습니다: … |

> **주의:** §6 케이스 B·C의 「앞 공정 없음」은 **P2와 별개**입니다.  
> P2 = 품목에 자가·혼합 공정이 **어디에든** 1건 이상.  
> 「앞 공정 없음」= 외주 **시작공정 순번보다 앞**에 INHOUSE가 없음 (뒤쪽 순번의 INHOUSE는 P2만 충족).

**용어 정리**

| 표현 | 의미 |
|------|------|
| 앞 공정 있음 (케이스 A) | 시작공정 순번 **미만**에 `WorkDistinction` ≠ `OUTSOURCE` 인 공정 존재 |
| 앞 공정 없음 (케이스 B) | 시작공정 순번 **미만**에 INHOUSE 없음 → Projector가 **BOM walk** |
| Projector 400 (케이스 C) | 앞 공정 walk + BOM walk 후에도 슬롯 0건 |

### 6.1 실행 순서

1. 백엔드 재기동 (`V007` 적용 확인)
2. **케이스 A** — 앞 자가 공정 있음 → `OUTSOURCE` INPUT 슬롯 (모품목·앞 공정)
3. **케이스 B** — 앞 공정 없음 + BOM 원자재 → `child_item_id` 슬롯
4. **케이스 C** — 앞 공정·BOM 모두 슬롯 없음 → Projector **400**
5. BOM 수정·삭제 → `bom_change_log` 행 추가
6. UI 정전개·역전개·BOM복사 동작

### 6.2 케이스별 시나리오 (검증용 데이터 예)

**케이스 A — 앞 자가 공정 있음**

| 항목 | 예시 (`P-PROC-01`) |
|------|---------------------|
| 공정 | seq **10** INHOUSE(절단) → seq **20** OUTSOURCE(성형) |
| 외주단가 | 시작·종료 = 성형(14000020) |
| 기대 | `inventory_balance` — `(item_id=모품목, partner_id=외주거래처, input_process_id=seq10 공정)` |

**케이스 B — 앞 공정 없음 + BOM 원자재**

P2 + Projector B를 동시에 만족하는 전형 배치:

| 항목 | 예시 |
|------|------|
| 공정 | seq **20** OUTSOURCE(시작공정) → seq **30** INHOUSE(절단) |
| BOM | 모품목 → `abc`(원자재) 1:1 |
| 외주단가 | 시작·종료 = seq20 OUTSOURCE 공정코드 |
| 기대 | Phase 1 슬롯 0건 → BOM walk → `(item_id=abc, input_process_id=소재공정)` |

```
seq 20 OUTSOURCE  ← 외주단가 시작공정 (앞에 INHOUSE 없음 → 케이스 B)
seq 30 INHOUSE    ← P2 충족용 (시작공정보다 뒤라 Phase 1에 미포함)
```

> OUTSOURCE만 등록하면 P2에서 먼저 400 — 케이스 B가 아님.

**케이스 C — Projector 400**

| 항목 | 예시 |
|------|------|
| 공정 | 케이스 B와 동일 (seq20 OUTSOURCE + seq30 INHOUSE) |
| BOM | **없음** |
| 외주단가 | 시작·종료 = seq20 OUTSOURCE |
| 기대 | HTTP **400** — **「하위 출고 공정 또는 원자재를 찾을 수 없음」** |

> OUTSOURCE만 있고 INHOUSE가 없으면 케이스 C가 아니라 **P2** 400이 먼저 발생합니다.

### 6.3 DB 확인 쿼리 (선택)

```sql
-- 케이스 A 투입 슬롯
SELECT id, item_id, partner_id, input_process_id, recording_state
  FROM inventory_balance
 WHERE partner_id = :외주거래처_id AND item_id = :모품목_id AND recording_state = 1;

-- 케이스 B 원자재 슬롯
SELECT id, item_id, partner_id, input_process_id, recording_state
  FROM inventory_balance
 WHERE partner_id = :외주거래처_id AND item_id = :원자재_item_id AND recording_state = 1;

-- BOM 이력
SELECT * FROM bom_change_log ORDER BY id DESC LIMIT 5;
```

### 6.4 검증 결과

| 일자 | 범위 | 결과 | 스크립트 |
|------|------|:----:|----------|
| 2026-07-03 | API 초기 검증 | 12/12 PASS | — |
| 2026-07-03 | **§6.0 → §6.2 재검증** | **17/17 PASS** | [`scripts/e2e-part06.ps1`](../../scripts/e2e-part06.ps1) |

재실행 (백엔드 `8080` 기동 후):

```powershell
cd smartmanager_backend
.\gradlew.bat :smartmanager-api:bootRun

# 다른 터미널
powershell -ExecutionPolicy Bypass -File .\scripts\e2e-part06.ps1
```

---

## 7. 잔여 (Step 1 백로그)

- BOM explosion/reverse/copy API — **완료**
- BOM UI — **완료**
- E2E §6 수동 검증 — **완료** (2026-07-03, §6.0→§6.2 스크립트 17/17 PASS)

---

*Part 06 완료 · E2E §6 검증 완료 (2026-07-03)*
