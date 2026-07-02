# Step 1 Part 01 — Spring Boot Gradle 보일러플레이트

> **완료일:** 2026-07-02  
> **범위:** 멀티모듈 골격 · Flyway · `domain_event` JPA · 로컬 DB 연결 · 헬스 API  
> **다음 파트:** [part-02-company-api.md](./part-02-company-api.md) (거래처 CRUD + `PartnerLedgerProjector`)

---

## 1. 요약

| 항목 | 결과 |
|------|------|
| 빌드 도구 | **Gradle 8.12.1** (Maven → Gradle 전환 완료) |
| Spring Boot | **3.5.0** |
| Java | **17+** (source/target 17, JDK 17 이상) |
| DB | `kit_erp` @ `localhost:3306` (MariaDB) |
| Gradle 빌드 | `BUILD SUCCESSFUL` |
| 앱 기동 | `Started SmartManagerApplication` |
| 헬스체크 | `GET /api/health` → `status: UP`, `database: kit_erp` |

---

## 2. 모듈 구조

```
SmartManager/
├── smartmanager_backend/         # Spring Boot · Gradle
│   ├── build.gradle.kts
│   ├── settings.gradle.kts
│   ├── gradlew.bat
│   ├── smartmanager-domain/      # 도메인 이벤트 · 상수 (Spring 비의존)
│   ├── smartmanager-application/   # 포트 (DomainEventStore)
│   ├── smartmanager-infrastructure/  # JPA · Flyway · 이벤트 저장소 구현
│   └── smartmanager-api/         # Spring Boot 진입점 · REST
└── smartmanager_frontend/        # React + Vite (Part 02+)
```

### 의존 방향

```
api → infrastructure → application → domain
```

---

## 3. 생성·변경 파일

### Gradle

| 파일 | 설명 |
|------|------|
| `smartmanager_backend/build.gradle.kts` | 루트 — Java 17 source/target, Spring Boot 3.5 BOM |
| `smartmanager_backend/settings.gradle.kts` | 4모듈 include |
| `smartmanager_backend/gradle/wrapper/*` | Gradle Wrapper 8.12.1 |
| `smartmanager_backend/gradlew.bat` | Windows 실행 스크립트 |
| `smartmanager_backend/*/build.gradle.kts` | 모듈별 의존성 |

### 제거 (Maven)

- `pom.xml` (루트·모듈 4개)
- `mvnw.cmd`, `.mvn/wrapper/*`

### 애플리케이션

| 파일 | 설명 |
|------|------|
| `smartmanager-domain/.../DomainEvent.java` | 이벤트 envelope |
| `smartmanager-application/.../DomainEventStore.java` | 저장 포트 |
| `smartmanager-infrastructure/.../JpaDomainEventStore.java` | JPA 구현 |
| `smartmanager-infrastructure/.../DomainEventJpaEntity.java` | `domain_event` 매핑 |
| `smartmanager-infrastructure/.../db/migration/V001,V002` | Flyway (sql/step1 동기) |
| `smartmanager-api/.../SmartManagerApplication.java` | `@SpringBootApplication` |
| `smartmanager-api/.../HealthController.java` | `GET /api/health` |
| `smartmanager-api/.../application-local.yml` | MariaDB `kit_erp` 로컬 설정 |

---

## 4. 로컬 실행

```powershell
cd smartmanager_backend

# 빌드
.\gradlew.bat build -x test

# API 기동 (profile local 기본)
.\gradlew.bat :smartmanager-api:bootRun
# 또는 저장소 루트에서: sql\local\run-api.ps1

# 헬스 확인
Invoke-RestMethod http://localhost:8080/api/health
```

**DB 연결:** `config/local-db.env` — `root` / `1111`

### Flyway (기존 DB에 수동 적용한 경우)

`application-local.yml`:

```yaml
spring.flyway.baseline-on-migrate: true
spring.flyway.baseline-version: 2
```

이미 `sql/local/apply-step1.ps1`로 V001·V002를 적용한 DB는 baseline 2로 스킵하고, 신규 DB는 Flyway가 마이그레이션을 실행합니다.

---

## 5. 빌드·기동 중 해결한 이슈

| 이슈 | 조치 |
|------|------|
| infrastructure에서 `DomainEvent` 미해결 | `smartmanager-infrastructure`에 `:smartmanager-domain` 의존 추가 |
| Hibernate `event_id` CHAR vs VARCHAR | `@Column(columnDefinition = "char(36)")` |
| 포트 8080 점유 | 기존 프로세스 종료 후 재기동 |

---

## 6. 검증 결과

### Gradle build

```
BUILD SUCCESSFUL in 1m 3s
```

### Health API (2026-07-02)

```json
{
  "status": "UP",
  "database": "kit_erp",
  "dbProduct": "MariaDB"
}
```

### DB 시드 (이전 파트에서 적용)

| 테이블 | 건수 |
|--------|------|
| `code_group` | 27 |
| `public_code` | 34 |
| `company` | 0 (Part 02에서 CRUD) |

---

## 7. Part 02 예정 작업

1. `company` / `company_role` JPA 엔티티
2. `RegisterCompany` 유스케이스 + `CompanyRegistered` 이벤트
3. **`PartnerLedgerProjector.ensureAccounts`** (P0)
4. `POST/GET /api/companies` REST
5. React 거래처 등록 화면 (E2E)

---

## 8. 참조

- [`docs/step0/step0-closure-checklist.md`](../step0/step0-closure-checklist.md) §6.3
- [`docs/step0/domain-event-projector-matrix.md`](../step0/domain-event-projector-matrix.md) §7·§8
- [`docs/step0/d4-company.md`](../step0/d4-company.md) v0.3
