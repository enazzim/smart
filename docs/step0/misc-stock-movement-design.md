# 기타 입출고 설계서 (TX-INV-MISC)

> **문서 버전:** 1.0  
> **작성일:** 2026-07-10  
> **상태:** 구현  
> **대상:** 수동 재고 입·출고 (레거시 기타입출고 대체)  
> **레거시 참고:** 경영정보관리 > 기타 입출고

---

## 1. 목적

레거시와 달리 **창고를 사용자가 선택하지 않고**, 품목 **재고분류**와 (필요 시) **공정**으로 입출고 창고를 자동 결정한다.

| 재고분류 | 공정 입력 | 창고 코드 | WIP 공정키 |
|---------|----------|-----------|------------|
| 원자재 | 불필요 | `RAW` | — |
| 상품 | 불필요 | `SALES` | — |
| 공정품 | **필수** | `WIP` | 선택 공정 |
| 제품 | **필수** | 마지막 공정 → `SALES` / 그 외 → `WIP` | 해당 공정 |
| 외주 | — | `OUTSOURCE` | **1차 유예** |

재고 반영은 **등록 즉시** (`InventoryBalanceService.recordMovement`). 삭제 시 역분개 INSERT 후 **업무 레코드는 물리 삭제** — 재고 원장(`stock_movement`)에 이력이 남습니다.

---

## 2. 레거시와의 차이

| 구분 | 레거시 | SmartManager |
|------|--------|--------------|
| 창고 선택 | 검색조건 창고 드롭다운 | **자동 결정** (읽기 전용 표시) |
| 상단 그리드 | 창고별 품목·재고 일람 | **없음** — 슬롯 Lazy 생성 구조상 창고 현황 브라우징 불가 |
| 선택 품목 현재고 | 그리드에서 확인 | 품목·공정 선택 후 **해당 슬롯 1건**만 표시 |
| 창고 전체 현황 | 기타입출고 화면 | **구매 > 재고·원장** 메뉴 |

---

## 3. 스키마 (V069)

### `misc_stock_movement`

| 컬럼 | 설명 |
|------|------|
| `movement_no` | MSM-YYYYMMDD-NNN |
| `movement_date` | 입출고 일자 |
| `movement_direction` | `IN` / `OUT` |
| `item_id` | 품목 |
| `location_code` | RAW / WIP / SALES (스냅샷) |
| `output_process_id` | WIP 슬롯 (nullable) |
| `qty` | 수량 |
| `reason_code_id` | public_code (1500 입출고사유) |
| `note` | 내용 |
| `status` | `REGISTERED` / `CANCELLED` |

### 공용코드

- `1500` 입출고사유 — 실사조정, 폐기, 샘플, 기타 등 시드

---

## 4. API

| Method | Path | 설명 |
|--------|------|------|
| GET | `/api/v1/inventory/misc-movements/preview` | 창고 자동결정 + 선택 슬롯 현재고 |
| GET | `/api/v1/inventory/misc-movements` | 기타 입출고 이력 |
| POST | `/api/v1/inventory/misc-movements` | 등록 |
| PUT | `/api/v1/inventory/misc-movements/{id}` | 수정 (기존 재고 역반영 후 재적용) |
| POST | `/api/v1/inventory/misc-movements/{id}/cancel` | 삭제 (재고 역반영 후 물리 삭제) |

권한: `inventory:misc-movement:read` / `inventory:misc-movement:write`

---

## 5. 메뉴

재고 > **기타 입출고** (`inventory-misc-movement`), **재고·원장** (`inventory-ledger`)

---

## 6. 업무 규칙

1. 등록·취소 모두 `movement_date` 기준 월마감 검사
2. 출고 시 해당 슬롯 재고 부족이면 거부 (마이너스 재고 설정 예외)
3. WIP 입출고 시 `WipBalanceProjector.ensure()` 선행
4. `stock_movement.reference_type` = `MISC_STOCK_MOVEMENT` / `MISC_STOCK_MOVEMENT_CANCEL`
5. 외주창고(`OUTSOURCE`)는 후속 Wave에서 확장

---

## 7. 화면

1. **입출고 등록** 패널: 1행(구분·품목·수량) / 2행(사유·사유(내용)·입출고일자·버튼), 공정·현재고는 품목 선택 시 표시
2. **입출고 내역** 그리드: 기간·품목 필터, 수정·삭제(재고 역반영)
