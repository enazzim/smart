# D4 — 사용자 (`UserInfo` → `UI_MT`)

> Step 0 산출물 · **확정 v0.1** (7필드 Modal · RBAC · `public_code` FK)  
> SSOT (레거시 감사): `KIT_ERP/BasisInformation/MasterInfoRecordRUD.cs` `m_FieldName[10]` (45필드)  
> 화면: `UserInfo.aspx` / `UserInfo.aspx.cs`  
> SmartManager: **`user`** (+ `user_role` M:N)  
> 내용키: `login_id` ← `ID` (UNIQUE, 활성)  
> PK: `id` ← `UserInfoIndex`

**관련:** [`step0-plan-D1-D4.md`](./step0-plan-D1-D4.md) · [`domain-event-projector-matrix.md`](./domain-event-projector-matrix.md) §2 · [`d4-work-standard.md`](./d4-work-standard.md) · [`공용코드 문서`](./공용코드-콤보박스-매핑-초안-code-group-field-binding.md) §4.9 · [시스템 컬럼 규칙](../../.cursor/rules/master-audit-fields.mdc) · TO-BE [`results/sample/basis-user-spec.md`](../../results/sample/basis-user-spec.md) · [`system-information-spec.md`](../../results/sample/system-information-spec.md) §3.6

> sample 테이블명 `app_user` — Step 0는 domain-event 정합 **`user`** 사용.

### v0.1 확정 요약

| 항목 | 레거시 (45필드) | SmartManager v0.1 |
|------|----------------|-------------------|
| UI | 2분할·10+ 필드 | **Modal 7필드** |
| 로그인 | `ID` + `Password` | `login_id` UK + `password_hash` (bcrypt) |
| 권한 | `UserRank`·`AuthorityIndex`(PUC) | **`user_role`** — RBAC `role_id` **다중** |
| 부서·직책 | `PostCode`·`Responsibility` (PUC) | **Drop** |
| 연락처 | `Telephone1`·`Telephone2` | **`contact` 1개** |
| 업무일지 | `WorkDiary` (PUC) | **`work_diary_group_id`** → `public_code.id` FK |
| 거래처 | `BusinessRegistrationNum` | **Drop** |
| 단축메뉴 20개 | `ShotcutUserMenuPage1~20` | **Drop** — RBAC 메뉴 |
| 사진 | `Picture` | **Drop** (Phase2) |
| 등록 부수효과 | 없음 | **없음** |

---

## 1. 분류 범례

| 분류 | 의미 |
|------|------|
| **Keep (UI)** | Modal·API 사용자 입력 |
| **시스템(자동)** | PK·감사·소프트삭제·`password_hash` |
| **Drop** | SmartManager 미구현·미이전 |
| **Phase2** | 프로필 사진·업무일지 TX 연계 |

---

## 2. SmartManager 스키마 — `user`

### 2.1 Modal 입력 7필드

| # | UI 라벨 | React field | DB 컬럼 | API | 필수 | 비고 |
|---|---------|-------------|---------|-----|------|------|
| 1 | 로그인 아이디 | loginId | login_id | `loginId` | Y | UK · **등록 후 수정 불가** |
| 2 | 비밀번호 | password | password_hash | `password` | 등록● | bcrypt. PUT blank = 유지 |
| 3 | 사원명 | name | name | `name` | Y | |
| 4 | 연락처 | contact | contact | `contact` | N | 휴대폰 등 |
| 5 | 사용권한 | roleIds | (user_role) | `roleIds` | Y | RBAC — **1개 이상** 권장 |
| 6 | 업무일지그룹 | workDiaryGroupId | work_diary_group_id | `workDiaryGroupId` | N | FK → `public_code.id` |
| 7 | 이메일 | email | email | `email` | N | 형식 검증 |

### 2.2 사용권한 (RBAC — PUC 아님)

레거시 `AuthorityIndex`·공용코드 권한 행렬 **미채택**.

| 계층 | 규칙 |
|------|------|
| 저장 | `user_role(user_id, role_id)` M:N |
| API 요청 | `roleIds[]` |
| API 응답 | `roleIds`, `roleCodes` |
| 역할 마스터 | `GET /api/v1/system/roles` ([system-information-spec §3.6](../../results/sample/system-information-spec.md)) |
| API 통제 | JWT `authorities` (permission) — `@PreAuthorize` |

React: 역할 **체크박스** (`/system/roles` 목록)

### 2.3 업무일지그룹 (`code_group` + `public_code` PK)

- React: `<CodeSelect codeGroup="WORK_DIARY_GROUP" valueField="id" />` (Phase2 모듈 선행 FK)
- API: `GET /api/code-groups/WORK_DIARY_GROUP/options`
- 저장: `work_diary_group_id` ← option `id`
- `?usageType=WORK_DIARY_GROUP` **미채택**
- 업무일지 모듈 미구현 시 **nullable** — 콤보만 선행 등록 가능

> `code_group` §3에 `WORK_DIARY_GROUP` 행 추가 예정 (Cut-over 시 `PUC_MT` export).

### 2.4 시스템 필드

| 컬럼 | 분류 |
|------|------|
| id, recording_state, created_*, updated_* | 시스템(자동) — [공통 규칙](../../.cursor/rules/master-audit-fields.mdc) |

### 2.5 검증·보호

| 규칙 | 내용 |
|------|------|
| `login_id` UK | 활성 구간 유일 |
| 중복 검사 | `GET /api/v1/basis/users/check-login-id?loginId=` |
| `login_id = admin` | **삭제 불가** |
| `password` | 등록 필수 · 변경 시 최소 길이 정책 (B0-R) |
| `roleIds` | 활성 `role` FK 존재 검증 |
| 삭제 | 소프트 — `work_standard.main_worker_user_id` 등 참조 시 콤보 비활성 |

---

## 3. 이벤트 · Projector

| 이벤트 | v0.1 Projector | 비고 |
|--------|----------------|------|
| `UserRegistered` | **— (부수효과 없음)** | |
| `UserUpdated` | — | `user_role` 동기화 |
| `UserDeleted` | — | admin 보호 |

---

## 4. API · 화면 (v0.1)

Base: `/api/v1/basis/users`

| Method | Endpoint | 설명 |
|--------|----------|------|
| GET | `/?q=` | 목록 — `login_id`·`name` 검색 |
| GET | `/search?q=` | 타 화면 `UserSelect`용 |
| GET | `/{id}` | 상세 |
| GET | `/check-login-id?loginId=` | ID 중복 검사 |
| POST | `/` | 등록 (7필드) |
| PUT | `/{id}` | 수정 (`loginId` 읽기 전용) |
| DELETE | `/{id}` | 소프트 삭제 |

**POST 예시**

```json
{
  "loginId": "hong.gd",
  "password": "initialSecret1!",
  "name": "홍길동",
  "contact": "010-1234-5678",
  "email": "hong@example.com",
  "roleIds": [2, 5],
  "workDiaryGroupId": 120
}
```

**PUT 예시**

```json
{
  "name": "홍길동",
  "password": null,
  "contact": "010-9876-5432",
  "email": "hong@example.com",
  "roleIds": [2],
  "workDiaryGroupId": null
}
```

**Response (요약)**

```json
{
  "id": 10,
  "loginId": "hong.gd",
  "name": "홍길동",
  "contact": "010-1234-5678",
  "email": "hong@example.com",
  "roleIds": [2, 5],
  "roleCodes": ["BASIS_MANAGER", "PRODUCTION_OPERATOR"],
  "workDiaryGroupId": 120,
  "workDiaryGroupName": "생산팀 일지"
}
```

### 4.1 UI (`/basis/users`)

- 패턴: **검색 + 그리드 + Modal** (거래처형)
- 그리드: 아이디, 이름, 연락처, 이메일, 역할코드
- **2분할 우측 상시 편집 없음**
- 등록: `loginId`·`password` 필수 + 실시간 중복 검사
- **하단 부가 그리드 없음**

---

## 5. 레거시 45필드 대조표

| # | 레거시 컬럼 | v0.1 | 비고 |
|---|------------|------|------|
| 1 | ID | **Keep (UI)** | → `login_id` UK |
| 2 | Password | **Keep (UI)** | → `password_hash` |
| 3 | Name | **Keep (UI)** | |
| 4 | PostCode | **Drop** | 부서 PUC |
| 5 | Responsibility | **Drop** | 직책 |
| 6 | IdentificationNumber | **Drop** | 주민번호 |
| 7 | Telephone1 | **Keep (UI)** | → `contact` 통합 |
| 8 | Telephone2 | Drop | → `contact` |
| 9 | Address | **Drop** | |
| 10 | EnterDate | **Drop** | |
| 11 | HomepageCompetence | **Drop** | HP 권한 → RBAC |
| 12 | StandardiInfoCompetence | **Drop** | 기준정보 권한 → RBAC |
| 13 | UserRank | **Drop** | → `user_role` |
| 14 | Picture | **Drop** | Phase2 |
| 15 | EMail | **Keep (UI)** | `email` |
| 16 | BusinessRegistrationNum | **Drop** | 거래처 FK |
| 17 | WorkDiary | **Keep (UI)** | → `work_diary_group_id` FK |
| 18~37 | ShotcutUserMenuPage1~20 | **Drop** | |
| 38~44 | RecodingState, 감사 | 시스템(자동) | |
| 45 | UserInfoIndex | `id` PK | |

---

## 6. `results/` 문서와의 관계

| 문서 | 채택 |
|------|------|
| `sample/basis-user-spec.md` v1.0 | **SSOT** — 7필드·RBAC·check-login-id (**테이블명 `user`**) |
| `sample/system-information-spec.md` §3.6 | role·permission·JWT |
| `sample/basis-information-api-spec.md` §5.9 | endpoint |

**Step 0 vs sample 차이**

| 항목 | sample | Step 0 v0.1 |
|------|--------|-------------|
| 테이블명 | `app_user` | **`user`** |
| 업무일지 콤보 API | `?usageType=WORK_DIARY_GROUP` | **`/api/code-groups/WORK_DIARY_GROUP/options`** |
| FK·RBAC·7필드 | 동일 | **동일** |

---

## 7. Cut-over · Phase2

| 항목 | 처리 |
|------|------|
| `UI_MT` | `user` + `user_role` |
| `ID` | `login_id` |
| `WorkDiary` PUC | `work_diary_group_id` (`public_code` 조인) |
| `AuthorityIndex` / Rank | `user_role` + `role` 마스터 매핑 테이블 |
| Drop 컬럼 | 미이전 |
| Phase2 | 업무일지 템플릿 바인딩, 프로필 사진, permission 메뉴 |

---

## 8. 체크리스트

- [x] v0.1 7필드 Modal·RBAC·login_id UK 확정
- [x] `work_diary_group_id` FK · `WORK_DIARY_GROUP` code_group (usageType 미채택)
- [x] domain-event 부수효과 없음
- [x] `code_group` §3 `WORK_DIARY_GROUP` → **`1900`** ([`work-diary-group-seed.csv`](./data/work-diary-group-seed.csv))
- [x] Step 1 Flyway + users CRUD + check-login-id + RBAC
- [ ] Auth JWT·`@PreAuthorize` (system 모듈)

---

*v0.1: 2026-07-02 · TO-BE: `results/sample/basis-user-spec` · 레거시: `MasterInfoRecordRUD.cs` m_FieldName[10]*