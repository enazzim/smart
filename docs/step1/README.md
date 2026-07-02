# Step 1 구현 진행 문서

| 파트 | 문서 | 상태 |
|------|------|:----:|
| 01 | [part-01-boilerplate.md](./part-01-boilerplate.md) | ✅ Gradle 보일러플레이트 |
| 01b | [part-01b-folder-java.md](./part-01b-folder-java.md) | ✅ backend/frontend 분리 · Java 17+ |
| 02 | [part-02-company-api.md](./part-02-company-api.md) | ✅ 거래처 CRUD + PartnerLedgerProjector |
| 03 | (예정) 품목 11필드 CRUD | |
| 04 | (예정) 공정 + WipBalanceProjector | |

**실행:** `cd smartmanager_backend` → `.\gradlew.bat :smartmanager-api:bootRun`

| 디렉터리 | 역할 |
|----------|------|
| `smartmanager_backend/` | Spring Boot 3.5 · Gradle · Java 17+ |
| `smartmanager_frontend/` | React + Vite (Part 02+) |
