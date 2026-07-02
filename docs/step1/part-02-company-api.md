# Step 1 Part 02 — 거래처 API + PartnerLedgerProjector

> **완료일:** 2026-07-02  
> **범위:** 거래처 등록·조회 REST API · `PartnerLedgerProjector` · React 등록/목록 화면  
> **설계 SSOT:** [`docs/step0/d4-company.md`](../step0/d4-company.md) v0.3  
> **다음 파트:** Part 03 — 품목 CRUD

---

## 1. 요약

| 항목 | 결과 |
|------|------|
| API | `POST/GET /api/v1/basis/companies`, `GET /{id}` |
| Projector | `PartnerLedgerProjector.ensureAccounts` — 당해 연도 `partner_ledger_account` |
| 이벤트 | `CompanyRegistered` → `domain_event` |
| Gradle 빌드 | `BUILD SUCCESSFUL` (`-x test`) |
| 앱 기동 | `Started SmartManagerApplication` |
| 프론트 빌드 | `npm run build` 성공 |
| E2E 검증 | SALES+PURCHASE 등록 → 원장 2행 · COST 단독 → 원장 0행 |

---

## 2. API 명세

### 2.1 거래처 등록

```
POST /api/v1/basis/companies
Content-Type: application/json
X-Actor-User-Id: (선택, 기본 local-dev)
```

**요청 예시**

```json
{
  "companyName": "테스트거래처",
  "presidentName": "홍길동",
  "businessRegNo": "123-45-67890",
  "businessAddress": "서울시 강남구",
  "roles": ["SALES", "PURCHASE"]
}
```

**응답:** `201 Created` — `CompanyResponse` (id, roles, createdAt 등)

### 2.2 거래처 목록 / 단건

| 메서드 | 경로 | 설명 |
|--------|------|------|
| `GET` | `/api/v1/basis/companies` | 활성(`recording_state=1`) 거래처 목록 |
| `GET` | `/api/v1/basis/companies/{id}` | 단건 조회 |

### 2.3 역할 → 원장 매핑

| `company_role` | `partner_ledger_account.ledger_type` |
|----------------|--------------------------------------|
| `SALES` | `SALES` |
| `PURCHASE` | `PURCHASE` |
| `OUTSOURCE` | `PURCHASE` (기존 행 재사용) |
| `COST` (단독) | **원장 미생성** |

---

## 3. 등록 트랜잭션 흐름

```
CompanyController.create
  → CompanyService.register (단일 TX)
      1. company INSERT
      2. company_role REPLACE
      3. PartnerLedgerProjector.ensureAccounts (당해 fiscal_year)
      4. domain_event.append(CompanyRegistered)
```

---

## 4. 백엔드 파일

### domain

| 파일 | 설명 |
|------|------|
| `domain/company/CompanyRoleType.java` | SALES, OUTSOURCE, PURCHASE, COST |
| `domain/company/PartnerLedgerType.java` | SALES, PURCHASE |

### application

| 파일 | 설명 |
|------|------|
| `application/company/CompanyService.java` | 등록·조회 유스케이스 |
| `application/company/PartnerLedgerProjector.java` | 역할별 원장 account ensure |
| `application/company/CompanyRepository.java` | 저장소 포트 |
| `application/company/PartnerLedgerAccountRepository.java` | 원장 저장소 포트 |

### infrastructure

| 파일 | 설명 |
|------|------|
| `persistence/company/CompanyJpaEntity.java` | `company` 매핑 (TINYINT 컬럼 명시) |
| `persistence/company/CompanyRoleJpaEntity.java` | `company_role` |
| `persistence/company/PartnerLedgerAccountJpaEntity.java` | `partner_ledger_account` |
| `persistence/company/JpaCompanyRepository.java` | Repository 구현 |
| `application/CompanyApplicationService.java` | `@Transactional` 래퍼 |
| `config/CompanyApplicationConfig.java` | 빈 조립 |

### api

| 파일 | 설명 |
|------|------|
| `web/company/CompanyController.java` | REST 엔드포인트 |
| `web/company/CreateCompanyRequest.java` | 요청 DTO |
| `web/company/CompanyResponse.java` | 응답 DTO |
| `config/WebConfig.java` | CORS `http://localhost:5173` |
| `web/ApiExceptionHandler.java` | 400/500 JSON 응답 |

---

## 5. 프론트엔드

| 파일 | 설명 |
|------|------|
| `src/App.tsx` | 거래처 등록 폼 + 목록 테이블 |
| `src/api/company.ts` | `fetchCompanies`, `createCompany` |
| `vite.config.ts` | dev proxy `/api` → `localhost:8080` |

```powershell
cd smartmanager_frontend
npm run dev    # http://localhost:5173
```

---

## 6. 로컬 실행·검증

### 백엔드

```powershell
cd smartmanager_backend
.\gradlew.bat :smartmanager-api:bootRun
```

### API 스모크 테스트

```powershell
# 헬스
Invoke-RestMethod http://localhost:8080/api/health

# 등록
$body = '{"companyName":"테스트","presidentName":"홍길동","businessRegNo":"123-45-67890","businessAddress":"서울","roles":["SALES","PURCHASE"]}'
Invoke-RestMethod -Uri http://localhost:8080/api/v1/basis/companies -Method POST -Body $body -ContentType "application/json; charset=utf-8"
```

### DB 확인 (MariaDB)

```sql
SELECT company_id, fiscal_year, ledger_type FROM partner_ledger_account;
SELECT event_type, aggregate_id FROM domain_event WHERE event_type = 'CompanyRegistered';
```

**검증 결과 (2026-07-02)**

| 시나리오 | `partner_ledger_account` | `domain_event` |
|----------|--------------------------|----------------|
| SALES + PURCHASE | SALES, PURCHASE 각 1행 (fiscal_year=2026) | `CompanyRegistered` / aggregate_id=1 |
| COST 단독 | 0행 | `CompanyRegistered` / aggregate_id=2 |

---

## 7. 해결한 이슈

| 이슈 | 조치 |
|------|------|
| Hibernate `recording_state` TINYINT vs INTEGER | `CompanyJpaEntity`, `PartnerLedgerAccountJpaEntity`에 `columnDefinition = "TINYINT"` |
| api 모듈에서 application 타입 미해결 | `smartmanager-api`에 `application`, `domain` 의존성 추가 |

---

## 8. Part 02 범위 밖 (후속)

- `PUT` / `DELETE` 거래처 (수정·논리삭제)
- `partner_ledger_monthly` 12행 Lazy 생성
- 사업자번호 중복 UI 처리
- B1 현업 D4 리뷰 — [`b1-business-d4-review-checklist.md`](../step0/b1-business-d4-review-checklist.md)
