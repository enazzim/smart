# Flyway 스키마 초안 (미적용)

이 디렉터리의 SQL은 **Flyway가 자동 실행하지 않습니다.**

| 파일 | 목적 | 적용 시점 (합의) |
|------|------|------------------|
| `V007__lot_traceability.sql` | Lot 추적 + `stock_movement` 스케치 | Lot 연동 설계 확정 후 · **실번호 V076+로 재부여** (V028 ALTER만) |
| `V076__drawing_reference.sql` | 도면 history 간 참조(pin) | [`drawing-reference-design.md`](../drawing-reference-design.md) DR-1 · 빈 번호 확인 후 migration 복사 |

적용 절차:

1. 해당 step0 설계서 검토·확정
2. 파일명의 `V00x`가 저장소 최신 Flyway와 충돌하지 않는지 확인 (필요 시 번호만 변경)
3. `smartmanager-infrastructure/src/main/resources/db/migration/` 로 복사
4. 로컬 Flyway 적용 후 API Wave 착수

설계 SSOT:
- Lot: [`../lot-integration-design.md`](../lot-integration-design.md) · [`../d5-lot-traceability.md`](../d5-lot-traceability.md)
- 도면 참조: [`../drawing-reference-design.md`](../drawing-reference-design.md)
