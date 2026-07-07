# TX 취소·삭제 정책 (초안)

> **문서 버전:** 0.1 (초안)  
> **작성일:** 2026-07-07  
> **상태:** 검토용 — 구현 강제 전 팀 합의 필요  
> **관련:** [master-audit-fields](../../.cursor/rules/master-audit-fields.mdc) · [domain-event-projector-matrix](./domain-event-projector-matrix.md) · [d5-lot-traceability](./d5-lot-traceability.md)

---

## 1. 문서 목적

SmartManager TX(수주·생산·구매·외주·영업·재고)에서 **취소·삭제** 시 `status`, `recording_state`, **물리 DELETE** 중 무엇을 표준으로 둘지 정의한다.

UK `(자연키, recording_state)` 충돌, 재고 이력(`stock_movement`) 불일치, purge 마이그레이션 남용을 줄이기 위한 **단일 기준**을 제공한다.

---

## 2. 용어

| 용어 | 의미 |
|------|------|
| `status` | 업무 상태 (`DRAFT`, `ISSUED`, `CANCELLED`, `REGISTERED` …) |
| `recording_state` (`rs`) | 활성 행 여부. `1`=조회·UK·참조 대상, `0`=비활성(소프트 삭제) |
| **물리 DELETE** | DB 행 삭제 (`repository.delete`) |
| **역분개** | 취소 시 `stock_movement`에 `*_CANCEL` 타입으로 **새 행 INSERT** |
| **부가 이력** | `sales_history`, `purchase_history`, `work_report_history`, `work_report_consumption_line` 등 |

---

## 3. 표준 패턴 3종

| 패턴 | 헤더 처리 | `recording_state` | 재고 (`stock_movement`) | 번호 재사용 | 용도 |
|------|-----------|-------------------|-------------------------|-------------|------|
| **A. 발행 전표 취소** | `status = CANCELLED` | **1 유지** | 역분개 행 **추가** (`*_CANCEL`) | **불가** | 재고·원장·감사가 있는 확정 TX |
| **B. 계획 슬롯 회수** | `status = CANCELLED` | **0 변경** | 해당 없음 또는 별도 규칙 | **가능** | 재수립·재발급이 필요한 계획 |
| **C. 임시·Ephemeral 제거** | — | — | 해당 없음 | **가능** | 하위 TX·재고 없는 초안/중간 산출물 |

### 3.1 패턴 A — 공통 부가 규칙

1. **부가 이력**: 헤더는 A, **자식은 `recording_state = 0`**
2. **`stock_movement`**: **삭제·`recording_state = 0` 금지** — 등록 + 취소 역분개만 (append-only, [d5-lot-traceability](./d5-lot-traceability.md) §2)
3. **`domain_event`**: 취소 이벤트 발행
4. **마감**: `monthClosingService.assertTransactionOpen` 선행

### 3.2 기준정보 (마스터)

기준정보 12종 및 RBAC 등 **마스터 테이블**은 [master-audit-fields](../../.cursor/rules/master-audit-fields.mdc) 를 따른다.

- HTTP DELETE → **`recording_state = 0`만** (물리 DELETE 금지)
- UK: `(자연키, recording_state)` — 활성(`1`) 구간 유일

---

## 4. 취소 처리 흐름 (의사결정)

```text
[취소 버튼 클릭]
        │
        ├─ 재고/원장 반영 TX? ──Yes──► 패턴 A
        │                              ├ status = CANCELLED
        │                              ├ recording_state = 1 (유지)
        │                              ├ stock_movement 역분개 INSERT
        │                              └ 부가 history/consumption → rs = 0
        │
        ├─ 계획·재발급 필요? ──Yes──► 패턴 B
        │                              ├ status = CANCELLED
        │                              └ recording_state = 0
        │
        └─ 하위 TX·재고 없음? ──Yes──► 패턴 C
                                       └ DELETE (또는 rs = 0 + purge)
```

---

## 5. UK·번호 정책 (전역)

| 문서 종류 | UK 예 | 취소 후 같은 번호 |
|-----------|-------|-------------------|
| **발행 전표** (출고, 작업일보, 매출, 입고 …) | `(번호, recording_state)` | **재사용 안 함** — 취소 건도 `rs = 1` 보관 |
| **계획** (작업계획) | `(plan 키, recording_state)` | **재사용 가능** — 취소 시 `rs = 0` |
| **Ephemeral** (작업지시, MRP, 조건부 생산계획) | 동일 | **재사용 가능** — DELETE 또는 `rs = 0` |

---

## 6. 업무별 정책표

### 6.1 영업

| 업무 | 테이블 | 현재 구현 | 권장 표준 | 번호 재사용 | 비고 |
|------|--------|-----------|-----------|-------------|------|
| 수주 | `sales_order` | A (`DRAFT`만, `rs=1`) | **A** | 불가 | `CONFIRMED` 후 취소 API 없음 — 유지 |
| 수주 (작성중) | ↑ | A | **A 또는 B** *(팀 결정)* | B 선택 시 가능 | 하위(생산계획) 없을 때만 B 검토 |
| 출고·납품 | `sales_shipment` | A + 재고 역분개 | **A** | 불가 | 물리 DELETE 비권장 |
| 매출 | `sales_revenue` | A + 재고·원장 역반영 | **A** | 불가 | `sales_history` → `rs=0` |
| 수금 | `sales_collection` | A + 원장 역반영 | **A** | 불가 | 재고 없음, 감사용 행 유지 |
| 매출 이력 | `sales_history` | 취소 시 `rs=0` | **자식: `rs=0`** | — | 헤더 A와 짝 |

### 6.2 생산

| 업무 | 테이블 | 현재 구현 | 권장 표준 | 번호 재사용 | 비고 |
|------|--------|-----------|-----------|-------------|------|
| 생산계획 | `production_plan` | **C** (물리 DELETE) | **C** *(조건부)* | 가능 | 실적·MRP·작업계획 없을 때만 DELETE |
| 작업계획 | `work_plan` | **B** (`CANCELLED` + `rs=0`) | **B** | 가능 | V036과 동일 — UK 해제 |
| 작업지시 | `work_order` | **C** (물리 DELETE) | **C** | 가능 | 실적·자재투입 없을 때만 |
| 작업일보 | `work_report` | A + 재고·이력 역분개 | **A** | 불가 | history/consumption → `rs=0` |
| 자재투입 | `material_issue` | A (헤더) + 라인 `rs=0` | **A** | 불가 | 작업지시 FK — 지시 DELETE 전 투입 취소 필수 |
| 작업실적 이력 | `work_report_history` | 취소 시 `rs=0` | **자식: `rs=0`** | — | |
| MRP 실행 | `mrp_run` / `material_requirement_line` | **C** (재계산 시 DELETE) | **C** | 가능 | 산출물 — 전표 아님 |

### 6.3 구매·품질

| 업무 | 테이블 | 현재 구현 | 권장 표준 | 번호 재사용 | 비고 |
|------|--------|-----------|-----------|-------------|------|
| 구매발주 | `purchase_order` | A | **A** | 불가 | 입고·검사 잔량 있으면 취소 불가 |
| 구매입고 | `purchase_receipt` | A + 재고·원장 역분개 | **A** | 불가 | |
| 매입 이력 | `purchase_history` | 취소 시 `rs=0` | **자식: `rs=0`** | — | |
| 품질검사 | `quality_inspection` | A | **A** | 불가 | 입고 연계 |

### 6.4 외주

| 업무 | 테이블 | 현재 구현 | 권장 표준 | 번호 재사용 | 비고 |
|------|--------|-----------|-----------|-------------|------|
| 외주발주 | `outsourcing_order` | A | **A** | 불가 | 출고·입고 실적 있으면 취소 불가 |
| 외주출고 | `outsourcing_shipment` | A + 재고 역분개 | **A** | 불가 | |
| 외주입고 | `outsourcing_receipt` | A + 재고·원장 | **A** | 불가 | |
| 외주 이력 | `outsource_history` | 취소 시 `rs=0` | **자식: `rs=0`** | — | |

### 6.5 재고·원장 (기준정보 아님)

| 업무 | 테이블 | 현재 구현 | 권장 표준 | 비고 |
|------|--------|-----------|-----------|------|
| 재고 이동 | `stock_movement` | append-only (`rs=1` INSERT만) | **변경 없음 (append-only)** | 취소 TX는 `*_CANCEL` 타입으로 추가 |
| 재고 슬롯 | `inventory_balance` | Projector 해제 시 `rs=0` | **`rs=0` (비활성)** | 단가·투입잔고 — TX 취소와 별개 |
| 월별 집계 | `inventory_balance_monthly` | `rs=1` | **`rs=1` 유지** | 슬롯과 함께 관리 |

---

## 7. 패턴 A — 취소 API 표준 템플릿

재고·원장이 있는 TX 취소 시 아래 순서를 **고정**한다.

1. 마감 검증 (`monthClosingService.assertTransactionOpen`)
2. 취소 가능 조건 (`cancelable()`)
3. **재고 역분개** (`*InventoryService.applyCancellation` → `stock_movement` INSERT)
4. **연관 수량·원장** 역반영 (수주 출고량, 매출·수금 원장, 생산계획 실적 등)
5. **부가 이력** `recording_state = 0`
6. 헤더 **`status = CANCELLED`** (`recording_state = 1` 유지)
7. **`domain_event`** 발행

### 7.1 하지 않을 것 (패턴 A)

- 헤더 **물리 DELETE**
- `stock_movement` **DELETE** 또는 **`recording_state = 0`**
- 패턴 A TX 헤더의 **`recording_state = 0`** (UK·감사 목록에서 제외됨)

---

## 8. 현재 ↔ 권장 차이 (정리 필요)

| 항목 | 현재 | 권장 | 조치 |
|------|------|------|------|
| 출고·작업일보·매출 취소 | A (일관) | A | **유지** — 물리 DELETE로 통일하지 않음 |
| 작업계획 취소 | B | B | **유지** |
| 작업지시 취소 | C | C | **유지** — 자재투입 FK 선행 취소 검증 강화 |
| 수주(DRAFT) 취소 | A (`rs=1`) | A 또는 B | **팀 결정** (§9) |
| `stock_movement` | append-only | append-only | 코드·리뷰 체크리스트에 명시 |
| purge (V041 등) | B/C 잔존 행 정리 | B/C만 | **패턴 A TX purge 금지** |

---

## 9. 미결정 사항 (팀 합의)

| # | 질문 | 선택지 |
|---|------|--------|
| 1 | **수주 `DRAFT` 취소** | **A**: 취소 건 목록에 `CANCELLED`로 남김, 번호 재사용 불가 |
|   | | **B**: `status=CANCELLED` + `rs=0`, 동일 `order_no` 재등록 가능 |
| 2 | 취소 전표 **목록 필터** | 기본: `status != CANCELLED` vs 취소 포함 토글 |
| 3 | `reference_id` 고아 | `stock_movement`는 원장 ID만 보관 — 출처 화면 링크는 `reference_type` + 번호(이벤트)로 보완할지 |

---

## 10. 구현·리뷰 체크리스트

신규 TX 또는 취소 API 수정 시:

- [ ] 패턴 A / B / C 중 하나를 §6 표에 태깅했는가?
- [ ] 재고 TX면 `applyCancellation` + `*_CANCEL` reference_type을 쓰는가?
- [ ] `stock_movement` DELETE/`rs=0` 코드가 없는가?
- [ ] UK `(번호, recording_state)`와 번호 재사용 정책(§5)이 일치하는가?
- [ ] 부가 이력은 `rs=0`, 헤더는 패턴에 맞는가?
- [ ] FK 하위(자재투입·작업일보 등) 선행 검증이 있는가?

---

## 11. 요약

| 질문 | 답 |
|------|-----|
| 대부분 TX는? | **패턴 A** (`status` 취소 + `rs=1` + 재고 역분개) |
| UK 때문에 `rs=0`? | **패턴 B** (작업계획 등 **재수립** 대상만) |
| 물리 DELETE? | **패턴 C** (작업지시, MRP, 조건부 생산계획) |
| 재고 이력? | **`stock_movement` append-only** — 취소도 새 행 |
| 기준정보? | **`recording_state=0` 소프트 DELETE** |

---

*다음 단계: §9 합의 후 v0.2 확정 · `.cursor/rules/` 또는 PR 리뷰 템플릿에 §10 체크리스트 연동*
