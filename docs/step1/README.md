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
| 09 | [part-09-equipment-api.md](./part-09-equipment-api.md) | ✅ 설비 6필드 CRUD |
| 10 | [part-10-user-api.md](./part-10-user-api.md) | ✅ 사용자 7필드 CRUD |
| 11 | [part-11-production-calendar-api.md](./part-11-production-calendar-api.md) | ✅ 생산달력 2계층 |

---

## Step 1 완료 (2026-07-04)

**범위:** D4 기준정보 메뉴 1~11 — Flyway V001~V011 · REST API · 기능 검증용 React UI  
**마지막 커밋:** Part 09~11 (설비 · 사용자 · 생산달력)

### Flyway

| 버전 | 내용 |
|------|------|
| V001~V008 | Core · 품목~작업표준 (Part 01~08) |
| V009 | `equipment` + 작업표준 `equipment_id` FK |
| V010 | `user` · `role` · `user_role` |
| V011 | `production_calendar` · `work_center_calendar` |

### Step 1 백로그 (Step S0 / Part 12+)

- 공용코드 **관리** CRUD (`/system/public-codes`)
- JWT · `@PreAuthorize`
- 생산달력 주말·공휴일 자동 휴무
- UI 공통화 (Modal·레이아웃)
- Capa · `EffectiveMinutes` E2E (PRD-W3)

---

**실행:** `cd smartmanager_backend` → `.\gradlew.bat :smartmanager-api:bootRun`

| 디렉터리 | 역할 |
|----------|------|
| `smartmanager_backend/` | Spring Boot 3.5 · Gradle · Java 17+ |
| `smartmanager_frontend/` | React + Vite (Part 02+) |
