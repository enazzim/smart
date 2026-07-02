# KIT_ERP 시스템정보관리 설계·구현서

> **문서 버전:** 1.0  
> **작성일:** 2026-06-22  
> **대상 스택:** Spring Boot 3.5 + MariaDB + React (Vite)  
> **레거시:** `SystemInfoManagement/`  
> **관련 문서:**  
> - [기준정보 구현 설계서](./basis-information-implementation-spec.md)  
> - [기준정보 API·필드 매핑](./basis-information-api-spec.md)  
> - [전체 구현 절차](../backend/docs/IMPLEMENTATION_PROCEDURE.md)

---

## 1. 문서 목적

시스템정보관리 8개 메뉴를 신규 ERP에 맞게 **역할 재정의**, **API**, **DB**, **구현 Wave**, **레거시 대비 변경점**을 정의한다.

**원칙:** 시스템정보는 전 모듈이 의존하는 **인프라 레이어**이며, Wave **S0**에서 기준정보(B0)와 함께 또는 선행 구현한다.

```
/api/v1/system/*   ← 시스템정보
/api/v1/basis/*    ← 기준정보
```

---

## 2. 메뉴·레거시·신규 매핑

| # | 메뉴 | 레거시 화면 | 레거시 테이블/처리 | 신규 API |
|---|------|-------------|-------------------|----------|
| 1 | 공용코드 정보 | `PublicUseCode.aspx` | `PUC_MT` | `/api/v1/system/public-codes` |
| 2 | 기업정보 입력 | `UserCompanyInput.aspx` | `UCI_T` | `/api/v1/system/tenant-profile` |
| 3 | 초기정보 일괄입력 | `StandardInforbundleRegistration.aspx` | `Upload.cs` | `/api/v1/system/imports/master-data` |
| 4 | 초기재고 일괄입력 | `InitialStockInput.aspx` | 창고별 Excel | `/api/v1/system/imports/opening-inventory` |
| 5 | 시스템 기능 설정 | `SystemAbilitySet.aspx` | `SCS_T` | `/api/v1/system/settings` |
| 6 | 사용권한 설정 | `UserAuthoritySet.aspx` | 페이지×등급(00~20) | `/api/v1/system/roles`, `/permissions` |
| 7 | 데이터 백업/복구 | `DataBackUpRestoration.aspx` | MSSQL BACKUP | `/api/v1/system/backups` |
| 8 | 원장 정리 | `HistoryAdjustment.aspx` | `Arrangement_MT` 삭제 | `/api/v1/system/month-closings`, `/archives` |

---

## 3. 메뉴별 상세 정의

### 3.1 공용코드 정보

**역할:** ERP 전역 코드 사전(SoT). 드롭다운·검증·리포트 라벨의 단일 출처.

| 항목 | 내용 |
|------|------|
| DB | `public_code` ? PK `id`, UK `small_code` |
| CRUD | 시스템정보 메뉴(관리자)에서만 등록·수정 |
| 조회 | `GET /api/v1/basis/public-codes` ? 타 모듈 **읽기 전용** |
| 삭제 | 참조 중 hard delete 금지, soft + 참조 검사 |
| Wave | **S0** |

### 3.2 기업정보 입력

**역할:** **자사(테넌트)** 프로필. 거래처(`CI_MT`)와 무관.

| 항목 | 내용 |
|------|------|
| DB | `tenant_profile` ← `UCI_T`, PK `id` |
| API | `GET/PUT /api/v1/system/tenant-profile` |
| 필드 | 회사명, 대표자, 사업자/법인번호, 업태·종목, 사업장·명세서·계산서 주소 |
| 용도 | 세금계산서, 거래명세서, PDF 헤더 |
| Wave | **S0** |

### 3.3 초기정보 일괄입력

**역할:** 마이그레이션·초기 구축용 벌크 Import. 일상 CRUD 대체 아님.

| 항목 | 내용 |
|------|------|
| API | `POST /system/imports/master-data/{domain}`, `GET /imports/jobs/{id}` |
| domain | company, item, item-composition(plan), work-center, process(plan), equipment, work-standard(plan), unit-cost, user |
| 처리 | 비동기 Job, 행별 검증, 실패 리포트 Excel |
| 순서 | UI 권장 순서 표시 + 서버 FK 검증 |
| 금지 | Import 시 창고·BSI·외주창고 자동 생성 |
| actual | 기본 제외 (별도 job 선택 시만) |
| 권한 | `system:import:execute` |
| Wave | **S1** (기준정보 B1~B2 후) |

**권장 Import 순서 (레거시와 동일, 서버가 검증)**

1. 거래처 → 2. 품목 → 3. 품목구성(plan) → 4. 작업장 → 5. 공정(plan) → 6. 설비 → 7. 작업표준(plan) → 8. 단가 → 9. 사용자

### 3.4 초기재고 일괄입력

**역할:** 기초 재고(Opening Balance) 설정.

| 항목 | 내용 |
|------|------|
| API | `POST /system/imports/opening-inventory` |
| 입력 | item_id(또는 itemNum→resolve), location, fiscal_year, process_sequence/code(선택), qty, amount |
| 처리 | `stock_movement`(OPENING) + `inventory_balance` + `inventory_balance_monthly` |
| 금지 | RMS/BS/DS/PS/OS 5테이블, 더미 공정코드, 연말 SP |
| 제한 | 미마감 연·월만 |
| Wave | **S2** (inventory 모듈 후) |

### 3.5 시스템 기능 설정

**역할:** 전역 비즈니스 정책(Feature Flags).

| 항목 | 내용 |
|------|------|
| DB | `system_settings` (key, value_json) ← `SCS_T` |
| API | `GET/PUT /api/v1/system/settings` |

**레거시 → 신규 정책 키 예시**

| 레거시 (`SCS_T`) | 신규 키 |
|------------------|---------|
| PresentRowMaterialUsed 등 | `mrp.include_on_hand`, `mrp.include_outsource`, `mrp.include_safety_stock` |
| ContinuityWorkProgress / DailyWorkProgress | `production.plan_strategy` |
| 구매발주 체크박스들 | `purchase.order_policy.*` |
| MinusRowMaterialPermissionUsed | `inventory.allow_negative` |
| 월마감 UI | **설정과 분리** → `POST /system/month-closings` |

| Wave | **S0** (조회), **S1** (마감 API) |

### 3.6 사용권한 설정

**역할:** RBAC. 레거시 **페이지×등급(00~20) 행렬 폐기**.

| 항목 | 내용 |
|------|------|
| DB | `role`, `permission`, `role_permission`, `user_role` |
| Permission | `{domain}:{resource}:{action}` 예: `basis:item:write` |
| Role | SYSTEM_ADMIN, BASIS_MANAGER, PURCHASE_OPERATOR, PRODUCTION_OPERATOR, SALES_OPERATOR, VIEWER |
| UI | 역할별 권한 트리 (21열 체크박스 ?) |
| 메뉴 | `menu:{id}` → `*:read` 매핑 (선택) |
| Spring | `@PreAuthorize("hasAuthority('basis:item:write')")` |
| 이관 | `UI_MT.UserRank` → role 1회 매핑 — [사용자 스펙](./basis-user-spec.md) §3.4 |

**S0 권한 코드 초안**

```text
system:public-code:read|write
system:tenant:read|write
system:settings:read|write
system:role:read|write
system:import:execute
system:month-closing:execute
basis:company:read|write
basis:item:read|write
basis:process:read|write
purchase:receipt:read|post
```

### 3.7 데이터 백업/복구

**역할:** 백업 실행은 **인프라**(cron, `mariadb-dump`). 앱은 이력·상태 조회.

| 항목 | 내용 |
|------|------|
| API | `GET /system/backups`, `POST /system/backups` (스냅샷 요청, 선택) |
| 금지 | UI에서 DB 원클릭 RESTORE |
| 문서 | `backend/docs/OPS_BACKUP.md` (RPO/RTO, Runbook) |
| Wave | **S3** |

### 3.8 원장 정리 → 마감·아카이브

**역할:** 파괴적 삭제 **폐지**.

| 대체 | 내용 |
|------|------|
| 월/연 마감 | `month_closing` ? 마감 후 해당 기간 트랜잭션 수정 차단 |
| 아카이브 | N년 이전 movement 등 파티션/별도 DB 이동 (**삭제 아님**) |
| API | `POST /system/month-closings`, `GET/POST /system/archive-jobs` |
| 금지 | `Arrangement_MT`식 기간 hard delete UI |
| Wave | **S1** (마감), **S3** (아카이브) |

---

## 4. Backend 패키지

```text
com.kit.erp.system
├── code/
├── tenant/
├── settings/
├── closing/
├── auth/
├── import/
├── backup/
└── archive/
```

---

## 5. MariaDB 테이블

| 테이블 | 용도 |
|--------|------|
| `public_code` | PUC_MT |
| `tenant_profile` | UCI_T |
| `system_settings` | SCS_T |
| `role`, `permission`, `role_permission`, `user_role` | RBAC |
| `month_closing` | 월마감 |
| `import_job`, `import_job_line` | 일괄입력 |
| `backup_log` | 백업 이력 (선택) |
| `archive_job` | 아카이브 (선택) |

---

## 6. Frontend 메뉴 (Vite ? 별도 승인 후)

```text
/system/public-codes
/system/tenant-profile
/system/imports/master-data
/system/imports/opening-inventory
/system/settings
/system/roles
/system/month-closings
/system/backups
```

---

## 7. 구현 Wave

| Wave | 내용 |
|------|------|
| **S0** | 공용코드, 기업정보, settings, RBAC, JWT |
| **S1** | 마스터 Import, 월마감 API |
| **S2** | 초기재고 Opening Import |
| **S3** | 백업 이력, 아카이브 |

---

## 8. 레거시 대비 금지 사항

1. 마스터 Import 시 `RelatedTableRegistration` 재현 (창고·BSI)
2. 페이지×21등급 행렬 UI 복원
3. 원장 정리 = hard delete
4. 앱에서 MSSQL식 DB RESTORE
5. 연말 SP 전량 INSERT + 작년 데이터 강제 DELETE

---

## 9. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-06-22 | 초안 ? 8메뉴 재정의, RBAC, 마감·아카이브 |
