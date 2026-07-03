# Flyway 스키마 초안 (미적용)

이 디렉터리의 SQL은 **Flyway가 자동 실행하지 않습니다.**

| 파일 | 목적 | 적용 시점 (합의) |
|------|------|------------------|
| `V007__lot_traceability.sql` | Lot 추적 + `stock_movement` 스케치 | Part 06(외주 Projector) · INF `stock_movement` 정합 **이후** |

적용 절차:

1. `docs/step0/d5-lot-traceability.md` v0.1 검토·확정
2. 필요 시 `V007`을 `V007`/`V008`으로 분리
3. `smartmanager-infrastructure/src/main/resources/db/migration/` 로 복사
4. 로컬 Flyway 적용 후 API·TX Wave 착수

설계 SSOT: [`../d5-lot-traceability.md`](../d5-lot-traceability.md)
