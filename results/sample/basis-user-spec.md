# 사용자 기준정보 확정 스펙

> **문서 버전:** 1.0  
> **작성일:** 2026-06-24  
> **상태:** 확정안 (구현 대기)  
> **관련 Wave:** B0(현행) → **B0-R** (사용자 UI·필드 슬림·검증 정비)  
> **관련 문서:**  
> - [기준정보 API·필드 매핑](./basis-information-api-spec.md) **§5.9**  
> - [시스템정보 설계서](./system-information-spec.md) §3.6 — RBAC·권한  
> - [공용코드 확정 스펙](./basis-public-code-spec.md) — 업무일지그룹 `usage_type`  
> - [작업표준 확정 스펙](./basis-work-standard-spec.md) — `mainWorkerId` FK  
> - [기준정보 구현 설계](./basis-information-implementation-spec.md)

**업무 원본:** `260624_기준정보_사용자.md` (PRODEV, 2026-06-24)

---

## 1. 문서 목적·적용 범위

ERP 로그인·업무 수행 **사원 마스터(`app_user`, 레거시 `UI_MT`)** 의 업무 정의, 7대 핵심 필드 슬림화, RBAC 연동, 화면·API, 현행 B0 구현과의 갭·후속 Wave를 정리한다.

| 구분 | 내용 |
|------|------|
| **슬림화** | 주민번호·주소·입사일·직책·부서·HP권한 등 레거시 10+ 필드 **제거** |
| **권한** | 레거시 `UserRank` / `AuthorityIndex`(공용코드) → **RBAC `role` + `user_role`** ([시스템정보 §3.6](./system-information-spec.md)) |
| **API·필드 SoT** | [basis-information-api-spec.md §5.9](./basis-information-api-spec.md) |
| **레거시 2분할 UI** | 좌 목록 / 우 상세 패널 — **미구현**. 공통 **목록 + Modal** |

---

## 2. 기준정보 공통 UI 원칙

[거래처 스펙](./basis-company-spec.md) §2와 동일.

| 항목 | 확정 |
|------|------|
| 레거시 하단 부가 그리드 | **구현하지 않음** |
| 목록 화면 | (선택) 검색 + 그리드 + **등록(Modal)** + 행별 **수정·삭제** |
| 2분할(좌/우) 상시 편집 | **사용하지 않음** — Modal CRUD |
| 시스템 필드 | `id`, 등록/수정, `recoding_state` — 화면 입력 없음 |
| 삭제 | `recoding_state = 0` 소프트 삭제 (`admin` 계정 **삭제 불가**) |

---

## 3. 업무 정의

### 3.1 식별자·내용키

| 구분 | 내용 |
|------|------|
| **PK** | `id` (BIGINT AUTO_INCREMENT) — PUT/DELETE **`/{id}`** |
| **내용키 (UK)** | **`login_id`** — 활성(`recoding_state = 1`) 구간 유일 |
| **비밀번호** | `password_hash` — **bcrypt** 저장. API 요청 `password`는 **평문**(HTTPS), DB **미저장** |

### 3.2 7대 핵심 필드 (1차 화면)

| # | 화면 라벨 | 필수 | DB | 비고 |
|---|-----------|:----:|-----|------|
| 1 | 로그인 아이디 | ● | `login_id` | 등록 후 **수정 불가** |
| 2 | 비밀번호 | ●* | `password_hash` | *등록 필수. 수정 시 **선택**(blank = 유지) |
| 3 | 사원명 | ● | `name` | |
| 4 | 연락처 | | `contact` | 휴대폰 등 — B0-R 컬럼 추가 |
| 5 | 사용권한 | ● | `user_role` | **RBAC `role_id` 다중** — Modal 체크박스 |
| 6 | 업무일지그룹 | | `work_diary_group_id` | FK → `public_code.id`, `usage_type = WORK_DIARY_GROUP` |
| 7 | 이메일 | | `email` | 형식 검증 |

> 레거시 `AuthorityIndex`(공용코드) → **`roleIds`** ([§3.4](#34-사용권한-rbac)).  
> 업무일지 모듈 **미구현** 시 그룹 FK **nullable**, 콤보만 선행 등록 가능.

### 3.3 로그인 ID 중복 검증

| 계층 | 규칙 |
|------|------|
| **POST** | 활성 `login_id` 중복 → **400** |
| **PUT** | `login_id` **변경 불가** |
| **실시간** | `GET /api/v1/basis/users/check-login-id?loginId=` → `{ available: true\|false }` (B0-R) |
| **UK** | DB `uk_app_user_login_id` + 서버 검증 이중 |

### 3.4 사용권한 (RBAC)

레거시 공용코드 `AuthorityIndex`·페이지×등급 행렬 **폐기**. [system-information-spec §3.6](./system-information-spec.md):

| 항목 | 내용 |
|------|------|
| **저장** | `user_role(user_id, role_id)` — **다중 역할** 허용 |
| **API** | 요청 `roleIds[]`, 응답 `roleIds`, `roleCodes` |
| **역할 마스터** | `GET /api/v1/system/roles` — 시스템정보에서 관리 |
| **메뉴·API 통제** | JWT **`authorities`** (`basis:item:write` 등) — `@PreAuthorize` |
| **프론트** | 역할별 메뉴 노출 — `roleCodes`·permission 매핑 (후속 강화) |

260624 §3.3.2 “JWT에 사용권한” → KIT_ERP는 **역할→permission 목록**을 토큰/세션에 실어 **세분화 RBAC** 적용.

### 3.5 업무일지그룹 (2차 모듈 연계)

| 항목 | 내용 |
|------|------|
| **용도** | 업무일지 작성 시 **보고서 템플릿** 자동 바인딩 (260624 §3.2) |
| **FK** | `work_diary_group_id` → `public_code.id` |
| **공용코드** | `usage_type = **WORK_DIARY_GROUP**` (Tier 1, [공용코드 §4.2](./basis-public-code-spec.md) 후속 추가) |
| **콤보** | `GET /api/v1/basis/public-codes?usageType=WORK_DIARY_GROUP` |
| **구현 Wave** | B0-R FK·콤보 선행 / **업무일지 TX** Wave에서 템플릿 바인딩 |

### 3.6 타 모듈 FK

| 참조 | 용도 |
|------|------|
| `work_standard.main_worker_user_id` | 주작업자 콤보 → `app_user.id` |
| `created_by` / `updated_by` | 감사 — `login_id` 문자열 |

---

## 4. API·필드

**엔드포인트·필드는 [basis-information-api-spec.md §5.9](./basis-information-api-spec.md) 를 따른다.**

### 4.1 REST API

| Method | Endpoint | 비고 |
|--------|----------|------|
| GET | `/api/v1/basis/users` | 목록 (선택 `?q=` 이름·아이디) |
| GET | `/api/v1/basis/users/{id}` | 단건 |
| GET | `/api/v1/basis/users/check-login-id?loginId=` | 중복 검사 (B0-R) |
| POST | `/api/v1/basis/users` | 등록 |
| PUT | `/api/v1/basis/users/{id}` | 수정 |
| DELETE | `/api/v1/basis/users/{id}` | 소프트 삭제 |

> **Deprecated:** `POST .../{id}/photo` — 1차 **미구현**. 필요 시 2차 Wave.

### 4.2 화면 입력 필드

| # | 화면 라벨 | 필수 | API (요청) | API (응답) | DB |
|---|-----------|:----:|------------|------------|-----|
| — | (시스템) | | — | `id` | `id` |
| 1 | 로그인 아이디 | ● | `loginId` | `loginId` | `login_id` |
| 2 | 비밀번호 | ●* | `password` | — | `password_hash` |
| 3 | 사원명 | ● | `name` | `name` | `name` |
| 4 | 연락처 | | `contact` | `contact` | `contact` |
| 5 | 사용권한 | ● | `roleIds` | `roleIds`, `roleCodes` | `user_role` |
| 6 | 업무일지그룹 | | `workDiaryGroupId` | `workDiaryGroupId`, `workDiaryGroupName` | `work_diary_group_id` (FK) |
| 7 | 이메일 | | `email` | `email` | `email` |

\* POST `password` 필수. PUT blank → 기존 hash 유지.

### 4.3 §5.9·현행 B0에서 1차 제거

| 제거 대상 (레거시 UI_MT) |
|--------------------------|
| `post_code`, `responsibility` (부서·직책) |
| `identification_number` (주민번호) |
| `telephone1` / `telephone2` → **`contact` 1개**로 통합 |
| `address`, `enter_date` |
| `homepage_competence`, `standard_info_competence` |
| `user_rank` (문자열) — **`user_role` RBAC** |
| `company_brn` (거래처 FK) |
| 사진 업로드 |

### 4.4 시스템 필드

| 컬럼 | API | 설명 |
|------|-----|------|
| `id` | `id` | PK |
| `created_by` / `created_at` | — | 등록 |
| `updated_by` / `updated_at` | — | 수정 |
| `recoding_state` | — | `1`=유효, `0`=삭제 |

---

## 5. §5.9 외 확정 사항

### 5.1 유효성

| 필드 | 규칙 |
|------|------|
| `loginId` | NOT NULL, trim, 길이 ≤ 50, 등록 후 변경 불가 |
| `password` | 등록·변경 시 최소 길이 정책 (예: ≥ 8) — B0-R |
| `name` | NOT NULL |
| `email` | null 허용. 값 있으면 **이메일 형식** 검증 |
| `contact` | null 허용. 전화번호 형식 **선택** 검증 |
| `roleIds` | **1개 이상** 권장(운영 정책). 존재·활성 `role` FK |
| `workDiaryGroupId` | null 허용. 활성 `public_code`, `usage_type = WORK_DIARY_GROUP` |

### 5.2 삭제·보호

- `login_id = 'admin'` — **삭제 불가** (현행 유지)
- 작업표준 `main_worker_user_id` 등 **참조 중** — soft delete 허용, 콤보에서 비활성 제외

### 5.3 로그인·JWT

- 인증: `login_id` + `password` → JWT 발급 (별도 auth 패키지)
- 토큰 claims: `sub`(loginId), `authorities`(permission 목록) — 역할 코드만이 아닌 **permission 기반** API 통제

---

## 6. 화면 (`/basis/users`)

### 6.1 목록 + Modal

```
┌──────────────────────────────────────────────────────────────┐
│ 사용자                                              [등록]    │
│ 검색 [이름·아이디________________]  (선택) 역할 필터           │
├──────────────────────────────────────────────────────────────┤
│ 아이디 │ 이름 │ 연락처 │ 이메일 │ 역할(코드) │ … │ [수정][삭제] │
└──────────────────────────────────────────────────────────────┘
```

| 요소 | 동작 |
|------|------|
| 검색 | `?q=` — `login_id`, `name` 부분 일치 (B0-R) |
| 그리드 | §3.2 핵심 컬럼 + `roleCodes` join |
| 등록 Modal | §4.2. `loginId`·`password` 필수. **역할** 체크박스 (`/system/roles`) |
| 수정 Modal | `loginId` **읽기 전용**. `password` 선택. 역할·연락·이메일·업무일지그룹 |
| 업무일지그룹 | `PublicCodeSelect` — `usageType=WORK_DIARY_GROUP` (B0-R) |

---

## 7. API 예시

**POST** `/api/v1/basis/users`

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

**PUT** `/api/v1/basis/users/10`

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

**Response**

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

---

## 8. 현행 B0 vs TO-BE

| 항목 | B0 (현행) | TO-BE |
|------|-----------|-------|
| 핵심 필드 | loginId, name, email, roleIds | + **contact**, **workDiaryGroupId** |
| 부서·직책 | DB 컬럼 잔존, UI null | **DROP** |
| 권한 | RBAC `roleIds` | 유지 — 260624 AuthorityIndex **미채택** |
| check-login-id | 없음 | **추가** |
| UI | 목록 + Modal | 유지 (2분할 **없음**) |
| photo API | api-spec만 | **1차 제외** |
| 업무일지 | 없음 | FK + 공용코드 **선행** |

---

## 9. 구현·후속 Wave

### 9.1 B0-R — 사용자

- [ ] Flyway: `contact`, `work_diary_group_id` FK; `post_code`, `responsibility` **제거**
- [ ] `usage_type = WORK_DIARY_GROUP` 시드 + 공용코드 헤더
- [ ] `UserService` — email/contact 검증, `check-login-id`, `workDiaryGroupId` FK
- [ ] `UserCreateRequest` / `UserUpdateRequest` — 슬림 7필드
- [ ] `UserController` — `GET check-login-id`, `?q=` 목록 필터
- [ ] 프론트 — 연락처·업무일지그룹 콤보, 등록 시 ID 중복 검사
- [ ] [basis-information-api-spec.md](./basis-information-api-spec.md) §5.9 동기화
- [ ] [basis-public-code-spec.md](./basis-public-code-spec.md) §4.2 — `WORK_DIARY_GROUP` 추가

### 9.2 후속

- [ ] 업무일지 모듈 — `workDiaryGroupId` 템플릿 바인딩
- [ ] 프론트 메뉴 — permission 기반 동적 노출
- [ ] (선택) 프로필 사진

---

## 10. 의사결정 요약

| 항목 | 확정 |
|------|------|
| UK | `login_id` |
| 핵심 7필드 | id·pw·이름·연락·**역할(RBAC)**·업무일지그룹·email |
| 권한 | **`role` / `user_role`** — 공용코드 Authority **아님** |
| UI | **목록 + Modal** (2분할 없음) |
| 비밀번호 | bcrypt |
| admin | 삭제 불가 |
| API SoT | **basis-information-api-spec §5.9** |

---

## 11. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-06-24 | 초안 — `260624_기준정보_사용자.md` + RBAC·Modal·7필드 슬림 |
