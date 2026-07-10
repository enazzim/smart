# 입고 지급 승인처리 설계서 (TX1-PA)

> **문서 버전:** 1.0  
> **작성일:** 2026-07-10  
> **상태:** 구현  
> **대상:** 구매입고 · 외주입고 · 기타구매입고  
> **레거시 참고:** 승인처리 화면 (현황관리)

---

## 1. 목적

입고·검사완료 시 **재고는 즉시 반영**하되, **거래처 미지급(지급 확정)은 승인 후** 반영한다.

| 구분 | 재고 | 지급 확정 |
|------|------|-----------|
| 구매입고 (무검사) | 입고 등록 즉시 | 승인 후 |
| 구매입고 (검사품) | 검사완료 시 | 승인 후 |
| 외주입고 | 동일 | 승인 후 |
| 기타구매입고 | 없음 | 승인 후 (입고 수량=지급 수량) |

**승인취소는 지급 확정만 취소**하며 재고는 변동하지 않는다.

---

## 2. 승인 단위

`purchase_history` / `outsource_history` **1행** = 승인 1건

- 창고 반영(또는 기타입고 등록) 시 `approval_status = PENDING` 으로 이력 생성
- 승인 시 `APPROVED` + `partner_ledger_monthly.purchase_amount` 가산
- 승인취소 시 `PENDING` 복귀 + 원장 차감 (재고 무관)

---

## 3. 스키마 (V067)

`purchase_history`, `outsource_history` 공통:

| 컬럼 | 설명 |
|------|------|
| `approval_status` | `PENDING` / `APPROVED` |
| `approved_at` | 승인 일시 |
| `approved_by_user_id` | 승인자 |
| `approval_cancelled_at` | 승인취소 일시 |
| `approval_cancelled_by_user_id` | 승인취소자 |

기존 데이터: `approval_status = APPROVED` (이미 원장 반영됨)

---

## 4. API

| Method | Path | 설명 |
|--------|------|------|
| GET | `/api/v1/purchase/payable-approvals/pending` | 미승인 목록 |
| GET | `/api/v1/purchase/payable-approvals/approved` | 승인 이력 |
| POST | `/api/v1/purchase/payable-approvals/approve` | 일괄 승인 |
| POST | `/api/v1/purchase/payable-approvals/cancel-approval` | 일괄 승인취소 |

권한: `purchase:payable-approval:read` / `purchase:payable-approval:write`

---

## 5. 메뉴

구매 > **승인처리** (`purchase-payable-approval`) — 지급 메뉴 상단

---

## 6. 업무 규칙

1. 입고 취소 시: 이력 `PENDING`이면 원장 영향 없이 비활성화; `APPROVED`이면 원장 차감 후 비활성화
2. 승인취소 시: 거래처 미지급 잔액 ≥ 취소 금액 (이미 지급된 금액 초과 취소 방지)
3. 마감: 승인·승인취소 모두 `history_date` 기준 `MonthClosingService` 검사
4. 지급 화면 미지급 집계: `approval_status = APPROVED` 건만 합산
