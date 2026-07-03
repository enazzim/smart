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
- 검색 + 그리드 + Modal CRUD
- 툴바: **정전개** · **역전개** · **BOM복사**

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

1. 백엔드 재기동 (`V007` 적용 확인)
2. **케이스 A** — 앞 자가 공정 있음: 외주단가 등록 → `OUTSOURCE` INPUT 슬롯 생성
3. **케이스 B** — 앞 공정 없음 + BOM 원자재: BOM 등록 후 외주단가 → `child_item_id` 슬롯 생성
4. **케이스 C** — 공정·BOM 없음: 외주단가 등록 → **400** + 메시지 확인
5. BOM 수정·삭제 → `bom_change_log` 행 추가
6. UI 정전개·역전개·BOM복사 Modal 동작

---

## 7. 잔여 (Step 1 백로그)

- BOM explosion/reverse/copy API — **완료**
- BOM UI — **완료**
- E2E §6 수동 검증

---

*Part 06 완료 · Git commit/push는 사용자 요청 시 진행*
