# Step 1 구현 진행 문서

| 파트 | 문서 | 상태 |
|------|------|:----:|
| 01 | [part-01-boilerplate.md](./part-01-boilerplate.md) | ✅ Gradle 보일러플레이트 |
| 01b | [part-01b-folder-java.md](./part-01b-folder-java.md) | ✅ backend/frontend 분리 · Java 17+ |
| 02 | [part-02-company-api.md](./part-02-company-api.md) | ✅ 거래처 CRUD + PartnerLedgerProjector |
| 03 | [part-03-item-api.md](./part-03-item-api.md) | ✅ 품목 11필드 CRUD |
| 04 | [part-04-process-api.md](./part-04-process-api.md) | ✅ 공정 CRUD + WipBalanceProjector |
| 05 | [part-05-unit-price-api.md](./part-05-unit-price-api.md) | ✅ 외주·판매·구매 단가 CRUD + Projector 1차 |
| 06 | [part-06-projector-alignment.md](./part-06-projector-alignment.md) | ✅ BOM + OutsourceInputBalanceProjector 400·walk |
| 07 | [part-07-work-center-api.md](./part-07-work-center-api.md) | ✅ 작업장 3필드 CRUD |
| 08 | [part-08-work-standard-api.md](./part-08-work-standard-api.md) | ✅ 작업표준 CRUD + copy |

**실행:** `cd smartmanager_backend` → `.\gradlew.bat :smartmanager-api:bootRun`

| 디렉터리 | 역할 |
|----------|------|
| `smartmanager_backend/` | Spring Boot 3.5 · Gradle · Java 17+ |
| `smartmanager_frontend/` | React + Vite (Part 02+) |
