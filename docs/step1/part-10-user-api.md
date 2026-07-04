# Step 1 Part 10 — 사용자 7필드 CRUD

> **완료일:** 2026-07-04  
> **범위:** 사용자 CRUD REST API · RBAC `user_role` · React 사용자 화면 · 작업표준 주작업자 FK 연동  
> **설계 SSOT:** [`docs/step0/d4-user.md`](../step0/d4-user.md) v0.1  
> **다음:** Step 1 마무리 · UI 공통화

---

## 1. 요약

| 항목 | 결과 |
|------|------|
| Flyway | `V010__user.sql` — `user` · `role` · `user_role` · `work_standard` FK |
| API | `POST/GET/PUT/DELETE /api/v1/basis/users` + `check-login-id` |
| UI 입력 | 7필드: loginId·password·name·contact·roleIds·workDiaryGroupId·email |
| 비밀번호 | bcrypt (`spring-security-crypto`) · PUT blank = 유지 |
| 부수효과 | **없음** |
| 연동 | 작업표준 `mainWorkerId` 검증 · `GET /api/v1/system/roles` |
| Gradle·프론트 빌드 | ✅ `gradlew build -x test` · `npm run build` |

---

## 2. API 명세

Base: `/api/v1/basis/users`

### 2.1 등록

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

### 2.2 목록·조회

| 메서드 | 경로 | 설명 |
|--------|------|------|
| `GET` | `/api/v1/basis/users?q=` | loginId·name 검색 |
| `GET` | `/api/v1/basis/users/search?q=` | UserSelect용 (동일) |
| `GET` | `/api/v1/basis/users/{id}` | ID 조회 |
| `GET` | `/api/v1/basis/users/check-login-id?loginId=` | `{ "available": true }` |

### 2.3 수정·삭제

| 메서드 | 경로 | 규칙 |
|--------|------|------|
| `PUT` | `/api/v1/basis/users/{id}` | `loginId` 읽기 전용 · password null/blank = 유지 |
| `DELETE` | `/api/v1/basis/users/{id}` | `login_id = admin` 삭제 불가 |

### 2.4 보조 API

| 메서드 | 경로 | 설명 |
|--------|------|------|
| `GET` | `/api/v1/system/roles` | RBAC 역할 목록 |
| `GET` | `/api/v1/basis/code-groups/WORK_DIARY_GROUP/options` | 업무일지그룹 콤보 |

---

## 3. 이벤트

| 이벤트 | Projector |
|--------|-----------|
| `UserRegistered/Updated/Deleted` | — (부수효과 없음) |

---

## 4. 프론트엔드

| 파일 | 역할 |
|------|------|
| `src/api/user.ts` | CRUD + roles + check-login-id |
| `src/pages/UserPage.tsx` | 7필드·역할 체크박스·ID 중복 검사 |
| `src/pages/WorkStandardPage.tsx` | 주작업자 콤보 연동 |
| `src/App.tsx` | **사용자** 탭 |

---

## 5. 수동 검증 체크리스트

- [ ] 사용자 등록 — 7필드·역할 1개 이상
- [ ] loginId UK — 중복 거부 · check-login-id
- [ ] 수정 — loginId 변경 불가 · 비밀번호 blank 유지
- [ ] admin 삭제 거부
- [ ] 작업표준 — 주작업자 콤보 선택·저장
- [ ] 역할 목록 — `/api/v1/system/roles` 6종

---

*Part 10 · SmartManager Step 1*
