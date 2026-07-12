# Flyway 스키마 초안 (미적용)

이 디렉터리의 SQL은 **Flyway가 자동 실행하지 않습니다.**

| 파일 | 목적 | 적용 시점 (합의) |
|------|------|------------------|
| `V007__lot_traceability.sql` | Lot 추적 + `stock_movement` 스케치 | [`lot-integration-design.md`](../lot-integration-design.md) §9 — **실번호 V078~** (Lot 테이블) |
| `V076__drawing_reference.sql` | 도면 history 간 참조(pin) | [`drawing-reference-design.md`](../drawing-reference-design.md) · **적용됨** |
| `V077__item_lot_tracked_and_model_type.sql` | 품목 `lot_tracked` · `model_type` 필수 · 도면 `model_type` 통일 | **적용됨** (`db/migration/V077__…`) |
| `V078__inventory_lot.sql` | Lot 마스터·잔량·채번·권한 | **적용됨** (`db/migration/V078__…`) |
| `V079__stock_movement_lot_and_genealogy.sql` | `stock_movement.lot_id` · `lot_genealogy` | **적용됨** (`db/migration/V079__…`) |

### Lot 실적용 번호 (2026-07-12 기준)

| 초안 / 내용 | 실적용 |
|-------------|--------|
| 품목 lot_tracked · model_type 필수 · 도면 model_type | **V077** ✅ |
| `inventory_lot` 등 Lot 마스터·잔량·채번 (+ `inventory:lot:*` 권한) | **V078** ✅ |
| `stock_movement.lot_id` · `lot_genealogy` | **V079** ✅ |
| 추가 인덱스 (선택) | **V080** |
