# 게시판·대시보드 Wave 설계 (초안)

> **문서 버전:** 0.1 (초안)  
> **작성일:** 2026-07-07  
> **상태:** 검토용 — Wave 1 구현 전 팀 합의 필요  
> **관련:** [basis-information-api-spec](../../results/sample/basis-information-api-spec.md) §7.1 · [master-audit-fields](../../.cursor/rules/master-audit-fields.mdc) · 레거시 `KIT_ERP/Contents.aspx` · `KIT_ERP/Community/`

---

## 1. 문서 목적

레거시 KIT_ERP **Community** 7종 게시판을 SMARTMANAGER TO-BE에 이식하기 위한 **Wave 분할·스키마·API·UI** 설계안을 정의한다.

구현 순서는 아래 3 Wave로 고정한다.

| Wave | 범위 | 산출물 |
|------|------|--------|
| **W1** | 공통 게시판 엔진 | DB · 파일저장 · 답변형 CRUD API · RBAC |
| **W2** | 대시보드 + 답변형 6종 UI | 로그인 후 홈 · 위젯 · 게시판 화면 |
| **W3** | 업무일지 | 그룹·템플릿 · 일지 작성/목록 · 대시보드 연동 |

---

## 2. 범위·용어

### 2.1 이번 범위에 포함

- 대시보드 2열 위젯 (레거시 `Contents.aspx` 레이아웃)
- 게시판 7종: 공지사항, 대표공지, **업무일지**, 생산자재공지, 레이저 공지, 연구소 공지, 영업 QC공지
- 답변형 6종: 원글 + 답글 + 첨부파일
- 업무일지: 사용자 `work_diary_group_id` 기반 템플릿 분기 (레거시 `WorkDiaryWrite01~16`, `WorkPageMove.aspx`)

### 2.2 이번 범위에서 제외

| 항목 | 사유 |
|------|------|
| 파이프 생산관리 공지 (`PipeList`) | 사용자 7종 목록에 없음 — 의도적 제외 |
| 레거시 데이터 일괄 이관 | Wave 1~3 완료 후 별도 마이그레이션 Wave |
| 업무일지 결재(사장 승인) 전자결재 | W3 최소 범위는 작성·조회·목록; 결재는 W3-R(후속) |
| 생산 **작업일보** (`work_report`, 메뉴 `prod-work-diary`) | TX 도메인 — Community 업무일지와 **별개** |

### 2.3 용어 정리

| 용어 | 의미 |
|------|------|
| **답변형 게시판** | `parent_post_id`로 스레드 구성. 목록은 **원글만**, 상세에서 답글 트리 표시 |
| **업무일지** | 일자·작성자·그룹별 양식 필드. 답글·첨부 **없음** (레거시 `CWD_T` 계열) |
| **board_type** | 게시판 종류 식별자 (ENUM) |
| **thread_root_id** | 스레드 루트 원글 ID (답글·원글 정렬·조회용) |

---

## 3. 레거시 매핑

### 3.1 대시보드 위젯 ↔ 게시판

레거시 `Contents.aspx` 2열 그리드 순서를 TO-BE 기본 배치로 따른다.

| 순서 | TO-BE 게시판 | `board_type` | 레거시 List | 레거시 테이블(참고) |
|------|-------------|--------------|-------------|---------------------|
| 1 | 공지사항 | `NOTICE` | `NoticeList` | `T_Notice` |
| 2 | 대표공지 | `PRESIDENT_NOTICE` | `PresidentNoticeList` | `T_PresidentNotice` |
| 3 | 업무일지 | *(W3 별도)* | `WorkReportList` | `CWD_T` |
| 4 | 생산자재공지 | `PRODUCT` | `ProductList` | `T_Product` |
| 5 | 레이저 공지 | `LASER` | `DevelopmentList` | `T_Development` |
| 6 | 연구소 공지 | `INSTITUTE` | `InstituteList` | `T_Institute` |
| 7 | 영업 QC공지 | `SALES_QC` | `MaterialsList` | `T_Materials` |

> **미결정(기본안):** 「레이저 공지」= `DevelopmentList`. 레거시 `LoaderList`는 대시보드에서 주석 처리되어 있어 W2 기본값은 `LASER`→`DevelopmentList`로 둔다. 변경 시 `board_type` 코드만 교체하면 된다.

### 3.2 레거시 답변·첨부 패턴

- 레거시 `T_*` 테이블은 단일 행 구조이나, 화면은 `*Write` / `*Edit` / `*Content` / `Down.aspx`로 **본문+첨부**를 처리했다.
- TO-BE는 스키마 수준에서 **답글(`parent_post_id`)·첨부(`board_attachment`)** 를 명시적으로 모델링한다.
- 첨부 컬럼(`FileName`, `AppendFile` 등)은 `board_attachment` 행으로 분리한다.

### 3.3 업무일지 레거시

| 레거시 | 내용 |
|--------|------|
| `CWD_T` | 일지 본문: `MorningWork`, `AfternoonWork`, `NightWork`, `TomorrowWork`, `WorkDateTitle`, `Listing`, `Closing` |
| `WorkDiaryWrite01~16` | 그룹별 입력 양식 (17종) |
| `WorkPageMove.aspx` | 로그인 사용자 그룹 → 작성 화면 라우팅 |
| `UI_MT.WorkDiaryGroupIndex` | TO-BE: `user.work_diary_group_id` → `public_code` (`WORK_DIARY_GROUP`, `1900`) |

공용코드 시드는 이미 `V002`에 `00~16` 그룹이 있다.

---

## 4. 아키텍처 개요

```text
┌─────────────────────────────────────────────────────────────┐
│  DashboardPage (W2) — GET /api/v1/dashboard/widgets         │
│  ┌──────────────┐  ┌──────────────┐                         │
│  │ 위젯 NOTICE  │  │ 위젯 PRESIDENT│  ... 2열 그리드          │
│  └──────┬───────┘  └──────┬───────┘                         │
└─────────┼─────────────────┼─────────────────────────────────┘
          │ MORE              │
          ▼                   ▼
┌─────────────────────────────────────────────────────────────┐
│  BoardPage (W2) — board_type 파라미터 공통 UI               │
│  목록 / 상세 / 작성 / 답글 / 첨부                           │
└──────────────────────────┬──────────────────────────────────┘
                           │ W1 API
                           ▼
┌─────────────────────────────────────────────────────────────┐
│  BoardPostService · BoardAttachmentService · FileStorage    │
│  board_post · board_attachment                              │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│  WorkDiaryPage (W3) — 별도 API                              │
│  work_diary_entry · work_diary_template                     │
│  user.work_diary_group_id → template_code                   │
└─────────────────────────────────────────────────────────────┘
```

### 4.1 모듈 배치 (Spring)

```
smartmanager-domain/          BoardType, PostKind, WorkDiaryStatus
smartmanager-application/     board/, workdiary/, dashboard/
smartmanager-infrastructure/  persistence, FileSystemBoardFileStorage
smartmanager-api/             BoardController, DashboardController, WorkDiaryController
```

의존: **W2 → W1**, **W3 → 사용자(B0-R)·공용코드(B0)**. W3는 W1 `board_post`를 쓰지 않는다.

---

## 5. Wave 1 — 공통 게시판 엔진

### 5.1 목표

답변형 6종이 **동일 서비스·동일 테이블**을 공유하도록 한다. 업무일지는 W3에서 별도 구현한다.

### 5.2 DB 스키마 (Flyway `V056__community_board.sql` 예상)

#### `board_post`

| 컬럼 | 타입 | 설명 |
|------|------|------|
| `id` | BIGINT PK | |
| `board_type` | ENUM | `NOTICE`, `PRESIDENT_NOTICE`, `PRODUCT`, `LASER`, `INSTITUTE`, `SALES_QC` |
| `post_kind` | ENUM | `TOP`(원글), `REPLY`(답글) — 목록 필터용 |
| `parent_post_id` | BIGINT NULL | 답글 시 직계 부모 |
| `thread_root_id` | BIGINT NULL | 스레드 루트 (원글은 self) |
| `title` | VARCHAR(500) | 원글 필수, 답글은 NULL 허용 |
| `content` | MEDIUMTEXT | HTML 또는 plain text (W2에서 rich text 여부 결정) |
| `author_user_id` | BIGINT FK → `user` | |
| `view_count` | INT DEFAULT 0 | |
| `pinned` | TINYINT DEFAULT 0 | 상단 고정 (공지·대표공지 우선 사용) |
| `recording_state` | TINYINT DEFAULT 1 | 소프트 삭제 |
| `created_by`, `created_by_id`, `created_at` | audit | [master-audit-fields](../../.cursor/rules/master-audit-fields.mdc) |
| `updated_by`, `updated_by_id`, `updated_at` | audit | |

인덱스:

- `(board_type, post_kind, recording_state, created_at DESC)` — 목록
- `(thread_root_id, created_at)` — 스레드 상세
- `(parent_post_id)` — 답글 조회

#### `board_attachment`

게시글 1건당 **첨부파일 여러 개** 등록 가능 (`board_post` 1 : N `board_attachment`).

| 컬럼 | 타입 | 설명 |
|------|------|------|
| `id` | BIGINT PK | |
| `post_id` | BIGINT FK → `board_post` | |
| `original_file_name` | VARCHAR(500) | 다운로드 표시명 |
| `stored_file_name` | VARCHAR(500) | 디스크 저장명 (UUID) |
| `content_type` | VARCHAR(100) | |
| `file_size` | BIGINT | bytes (파일당 최대 100MB) |
| `recording_state` | TINYINT DEFAULT 1 | |
| `created_at` | DATETIME(3) | |

#### 파일 저장

- 1차: **로컬 디스크** `application-local.yml` — `smartmanager.board.files-dir`
- 저장 경로: `{files-dir}/{board_type}/{yyyy}/{mm}/{uuid}`
- 다운로드: `GET /api/v1/boards/{boardType}/posts/{postId}/attachments/{id}/download`
- 업로드: `multipart/form-data` — 게시글 저장 시 **복수 파일** 또는 별도 POST
- **파일 1개당 최대 100MB** (`smartmanager.board.max-file-size-bytes: 104857600`)
- Spring multipart: `max-file-size: 100MB`, `max-request-size: 1050MB` (100MB × 10개 동시 업로드 여유)
- 확장자 화이트리스트는 yml (W1 고정)

### 5.3 API (W1)

Base: `/api/v1/boards/{boardType}/posts`

| Method | Path | 설명 |
|--------|------|------|
| GET | `/` | 원글 목록 (페이징, `post_kind=TOP`) |
| GET | `/{id}` | 상세 + 답글 트리 + 첨부 메타 |
| POST | `/` | 원글 작성 (+ 첨부 **복수**) |
| POST | `/{id}/replies` | 답글 작성 (+ 첨부 **복수**) |
| PUT | `/{id}` | 수정 (작성자 또는 `community:board:moderate`) |
| DELETE | `/{id}` | `recording_state=0` |
| POST | `/{id}/attachments` | 첨부 추가 (복수 파일 1회 요청 가능) |
| DELETE | `/{id}/attachments/{attachmentId}` | 첨부 삭제 |
| GET | `/{id}/attachments/{attachmentId}/download` | 파일 스트리밍 |

Query 공통: `page`, `size`, `keyword`(제목 검색)

대시보드용 (W1에 포함, W2에서 소비):

| Method | Path | 설명 |
|--------|------|------|
| GET | `/api/v1/dashboard/widgets` | 보드별 최근 N건 요약 |

응답 예:

```json
{
  "widgets": [
    {
      "boardType": "NOTICE",
      "title": "공지사항",
      "items": [
        { "id": 1, "title": "...", "createdAt": "2026-07-07T09:00:00", "authorName": "홍길동" }
      ]
    }
  ]
}
```

업무일지 위젯 슬롯은 W3 전 `items: []` 또는 `boardType: "WORK_DIARY"` placeholder.

### 5.4 비즈니스 규칙

1. **목록**: `post_kind=TOP` AND `recording_state=1`만. `pinned DESC`, `created_at DESC`.
2. **답글**: `parent_post_id` 필수. `thread_root_id`는 부모가 원글이면 부모 ID, 아니면 부모의 `thread_root_id`.
3. **삭제**: 원글 삭제 시 답글·첨부도 `recording_state=0` (연쇄 소프트 삭제). 물리 DELETE 금지.
4. **조회수**: 상세 GET 시 `view_count+1` (본인 연속 조회 스로틀은 W2-R).
5. **첨부**: 게시글·답글당 **여러 파일** 등록. **파일 1개당 ≤ 100MB**. 초과 시 `400` 거부.
6. **권한**: 기본 `community:board:read` / `community:board:write`. 타입별 세분화는 후속.

### 5.5 RBAC (W1)

```sql
INSERT INTO permission (permission_code, description) VALUES
('community:board:read', '게시판 조회'),
('community:board:write', '게시판 작성·수정'),
('community:board:moderate', '타인 글 삭제·고정');
```

- `VIEWER` → read
- `SYSTEM_ADMIN` 및 운영 역할 → read + write
- moderate는 SYSTEM_ADMIN 기본 부여

### 5.6 W1 완료 기준 (체크리스트)

- [ ] Flyway V056 적용
- [ ] 원글/답글/첨부 CRUD API Postman 또는 통합 테스트
- [ ] 파일 업·다운로드 E2E
- [ ] `recording_state` 소프트 삭제
- [ ] `GET /api/v1/dashboard/widgets` — 6종 board 최근 5건

---

## 6. Wave 2 — 대시보드 + 답변형 6종 UI

### 6.1 목표

로그인 후 **첫 화면 = 대시보드**. 레거시 `Contents.aspx`와 유사한 2열 위젯 + MORE → 게시판 전체 목록.

### 6.2 프론트 구조

```
src/pages/
  DashboardPage.tsx           # 홈, 2열 위젯 그리드
  board/
    BoardListPage.tsx         # ?boardType=NOTICE
    BoardDetailPage.tsx       # 본문 + 답글 + 첨부
    BoardWritePage.tsx        # 원글/답글 작성
src/components/board/
  BoardWidget.tsx             # 제목 + MORE + 최근 4~5건
  BoardAttachmentList.tsx
  BoardReplyThread.tsx
src/api/
  board.ts
  dashboard.ts
```

### 6.3 라우팅·메뉴

| 항목 | 제안 |
|------|------|
| 대메뉴 | `home` (대시보드) — 로그인 후 기본 선택 |
| 게시판 상세 | 대시보드 MORE 또는 사이드 서브링크 |
| `App.tsx` | `DEFAULT_SELECTION` → `{ category: 'home' }` |

`menuConfig.tsx`에 `MenuCategory 'home'` 추가. 기존 7개 업무 대메뉴는 유지.

### 6.4 UI 상세 (레거시 대응)

**대시보드 위젯 (`BoardWidget`)**

- 헤더: 게시판명 + `MORE` 링크
- 본문: 최근 4~5건 — 제목(ellipsis) + 등록일 `MM/dd`
- 신규 글 강조는 W2-R (아이콘)

**게시판 목록 (`BoardListPage`)**

- 컬럼: 번호, 제목, 작성자, 등록일, 조회수, 첨부 아이콘
- 제목 클릭 → 상세
- `글쓰기` 버튼 — `community:board:write` 있을 때만

**게시판 상세 (`BoardDetailPage`)**

- 원글 본문 + 첨부 목록(다운로드)
- 답글 목록 (들여쓰기 1단 — 레거시 수준; 다단은 W2-R)
- `답변` 버튼 → `BoardWritePage?parentId=`
- 수정/삭제 — 작성자 본인

**첨부**

- 작성 시 **multi-file input** (여러 개 선택·추가)
- 파일 1개당 **100MB 이하**
- 허용: pdf, doc, docx, xls, xlsx, png, jpg, zip (W1 yml과 동기)

### 6.5 board_type ↔ 화면 타이틀

| boardType | 화면 제목 |
|-----------|-----------|
| `NOTICE` | 공지사항 |
| `PRESIDENT_NOTICE` | 대표공지 |
| `PRODUCT` | 생산자재공지 |
| `LASER` | 레이저 공지 |
| `INSTITUTE` | 연구소 공지 |
| `SALES_QC` | 영업 QC공지 |

### 6.6 W2 완료 기준

- [ ] 로그인 → 대시보드 7슬롯 (업무일지 슬롯은 "준비 중" 또는 W3 연동)
- [ ] 6종 MORE → 목록 → 상세 → 답글 → 첨부 다운로드
- [ ] 모바일 최소 대응 (2열 → 1열 stack) — 선택
- [ ] `npm run build` 통과

---

## 7. Wave 3 — 업무일지

### 7.1 목표

레거시 `WorkReportList` + `WorkDiaryWrite**` + `WorkPageMove` 흐름을 TO-BE로 이식한다. **답변·첨부 없음.**

### 7.2 DB (`V057__work_diary.sql` 예상)

#### `work_diary_template`

| 컬럼 | 설명 |
|------|------|
| `id` | PK |
| `template_code` | `00`~`16` (레거시 Write 페이지 번호) |
| `work_diary_group_id` | FK → `public_code.id` (`WORK_DIARY_GROUP`) |
| `template_name` | 표시명 |
| `field_schema` | JSON — 필드 정의 (W3 최소: 공통 4필드 고정도 가능) |
| `recording_state` | |

**W3 최소안 (권장):** 17그룹 모두 동일 필드 스키마로 시작한다.

```json
{
  "fields": [
    { "key": "morningWork", "label": "오전 업무", "type": "textarea" },
    { "key": "afternoonWork", "label": "오후 업무", "type": "textarea" },
    { "key": "nightWork", "label": "야간 업무", "type": "textarea" },
    { "key": "tomorrowWork", "label": "익일 계획", "type": "textarea" }
  ]
}
```

레거시 `CWD_T` 컬럼과 1:1 대응. 그룹별 상이 양식은 `field_schema` 확장으로 후속.

#### `work_diary_entry`

| 컬럼 | 설명 |
|------|------|
| `id` | PK |
| `work_date` | DATE — 일지 일자 |
| `author_user_id` | FK → `user` |
| `work_diary_group_id` | 작성 시점 스냅샷 (감사) |
| `template_code` | |
| `work_date_title` | VARCHAR — 레거시 `WorkDateTitle` |
| `field_values` | JSON — 실제 입력값 |
| `listed` | TINYINT — 레거시 `Listing` (목록 노출 여부) |
| `closing_note` | VARCHAR — 레거시 `Closing` |
| `status` | ENUM `DRAFT`, `SUBMITTED` (결재는 W3-R) |
| audit + `recording_state` | |

UK 제안: `(author_user_id, work_date, recording_state)` — 1인 1일 1건.

### 7.3 API (W3)

Base: `/api/v1/work-diaries`

#### 7.3.1 권한/조회 정책

- 작성자: **본인 일지만** 조회/수정/삭제 가능
- 사장(결재권자): **전체 일지** 조회 가능
- 결재(`APPROVED`) 이후 작성자는 수정/삭제 불가
- 결재취소 시(`APPROVED -> SUBMITTED`) 작성자 수정/삭제 다시 허용
- `work_date_title`은 사용자 입력을 받지 않고 서버에서 **`{작성자명} 업무일지`** 로 고정 생성

권한 코드 제안:

- `community:workdiary:read` (기본 조회)
- `community:workdiary:write` (작성/수정/삭제)
- `community:workdiary:approve` (전체조회/지시사항/결재/결재취소)

#### 7.3.2 상태 전이

`DRAFT -> SUBMITTED -> APPROVED`

- `POST /{id}/submit`: `DRAFT|REJECTED -> SUBMITTED`
- `POST /{id}/approve`: `SUBMITTED -> APPROVED`
- `POST /{id}/cancel-approval`: `APPROVED -> SUBMITTED`

#### 7.3.3 Endpoint 목록

| Method | Path | 설명 |
|--------|------|------|
| GET | `/my-template` | 로그인 사용자 `work_diary_group_id` -> template |
| GET | `/` | 목록 (작성자/기간/상태, 서버에서 조회범위 강제) |
| GET | `/{id}` | 상세 (본인 또는 결재권자) |
| GET | `/by-date/{workDate}` | 내 일지 조회 (없으면 404) |
| POST | `/` | 작성 (그룹·템플릿 서버 강제) |
| PUT | `/{id}` | 수정 (작성자 + 결재전만) |
| DELETE | `/{id}` | 소프트 삭제 (작성자 + 결재전만) |
| POST | `/{id}/submit` | 제출 |
| POST | `/{id}/approve` | 지시사항 입력 + 결재 |
| POST | `/{id}/cancel-approval` | 결재취소 |

#### 7.3.4 DTO 설계

**A) `GET /my-template`**

Response:

```json
{
  "templateCode": "14",
  "workDiaryGroupId": 123,
  "workDiaryGroupName": "14 그룹",
  "templateName": "업무일지 14 그룹",
  "fieldSchema": {
    "legacyFields": {
      "01": "1. 레이져 작업 내역",
      "02": "2. 레이저불량내역",
      "03": "3. 레이져 절단기 체크LIST",
      "06": "5. 지시사항",
      "07": "4. 외근사항",
      "10": "* 로더 공정별 확인"
    }
  }
}
```

**B) `GET /` 목록**

Query:

- `fromDate` (YYYY-MM-DD)
- `toDate` (YYYY-MM-DD)
- `status` (`DRAFT|SUBMITTED|APPROVED|REJECTED`)
- `authorUserId` (결재권자만 허용)
- `page`, `size`

Response:

```json
{
  "items": [
    {
      "id": 101,
      "workDate": "2026-07-07",
      "workDateTitle": "2026-07-07 업무일지",
      "authorUserId": 15,
      "authorName": "홍길동",
      "workDiaryGroupId": 123,
      "workDiaryGroupName": "14 그룹",
      "templateCode": "14",
      "status": "SUBMITTED",
      "listed": true,
      "approvedAt": null,
      "directiveNotePreview": "레이저 컷팅 품질 기준 재점검..."
    }
  ],
  "totalElements": 1,
  "page": 0,
  "size": 20
}
```

**C) `GET /{id}` 상세**

Response:

```json
{
  "id": 101,
  "workDate": "2026-07-07",
  "workDateTitle": "2026-07-07 업무일지",
  "authorUserId": 15,
  "authorName": "홍길동",
  "workDiaryGroupId": 123,
  "workDiaryGroupName": "14 그룹",
  "templateCode": "14",
  "templateName": "업무일지 14 그룹",
  "fieldSchema": {
    "legacyFields": {
      "01": "1. 레이져 작업 내역",
      "02": "2. 레이저불량내역"
    }
  },
  "fieldValues": {
    "01": "주간작업 ...",
    "02": "불량 2건 ..."
  },
  "directiveNote": "내일 오전 재검증 결과 공유",
  "status": "SUBMITTED",
  "listed": true,
  "closingNote": null,
  "submittedAt": "2026-07-07T09:10:11",
  "approvedAt": null,
  "approvedByUserId": null,
  "approvedByName": null,
  "approvalCanceledAt": null,
  "approvalCanceledByUserId": null,
  "canEdit": true,
  "canDelete": true,
  "canApprove": true,
  "canCancelApproval": false,
  "createdAt": "2026-07-07T09:00:00",
  "updatedAt": "2026-07-07T09:10:11"
}
```

**D) `POST /` 작성**

Request:

```json
{
  "workDate": "2026-07-07",
  "fieldValues": {
    "01": "주간작업 ...",
    "02": "불량내역 ..."
  },
  "listed": true,
  "closingNote": null,
  "status": "DRAFT"
}
```

Response: `GET /{id}`와 동일 구조

**E) `PUT /{id}` 수정**

Request:

```json
{
  "fieldValues": {
    "01": "주간작업(수정) ...",
    "02": "불량내역(수정) ..."
  },
  "listed": true,
  "closingNote": "수정 메모"
}
```

Response: `GET /{id}`와 동일 구조

**F) `POST /{id}/submit` 제출**

Request:

```json
{
  "comment": "작성 완료, 결재 요청드립니다."
}
```

Response:

```json
{
  "id": 101,
  "status": "SUBMITTED",
  "submittedAt": "2026-07-07T09:15:00"
}
```

**G) `POST /{id}/approve` 결재**

Request:

```json
{
  "directiveNote": "내일 오전 9시 개선안 공유 바랍니다."
}
```

Response:

```json
{
  "id": 101,
  "status": "APPROVED",
  "directiveNote": "내일 오전 9시 개선안 공유 바랍니다.",
  "approvedAt": "2026-07-07T10:00:00",
  "approvedByUserId": 1,
  "approvedByName": "사장님"
}
```

**H) `POST /{id}/cancel-approval` 결재취소**

Request:

```json
{
  "reason": "추가 보완 후 재결재 필요"
}
```

Response:

```json
{
  "id": 101,
  "status": "SUBMITTED",
  "approvalCanceledAt": "2026-07-07T10:30:00",
  "approvalCanceledByUserId": 1,
  "approvalCanceledByName": "사장님"
}
```

대시보드 연동: `GET /api/v1/dashboard/widgets`에 `WORK_DIARY` 슬롯 추가 — 최근 `work_diary_entry` 5건 (`work_date_title` 또는 요약).

### 7.4 화면 (`WorkDiaryPage.tsx`)

1. **진입** — 메뉴 `home` 하위 또는 `community` → 업무일지
2. **작성** — `GET /my-template`으로 필드 렌더 (동적 form 또는 4 textarea 고정)
3. **목록** — `WorkReportList` 스타일: 일자, 제목, 작성자, 그룹
4. **그룹 불일치** — `work_diary_group_id` NULL이면 작성 불가 + 안내

`WorkPageMove` 대응: 프론트가 `/my-template`만 호출하면 서버가 그룹→템플릿 라우팅을 담당한다.

### 7.5 W3-R (후속, 문서만 기록)

- 결재 상태 (`approval`, `QC` — 레거시 `WorkReport` 컬럼)
- 그룹별 상이 필드 (`WorkDiaryWrite01` vs `16` diff 분석 후 `field_schema` 시드)
- 레거시 `CWD_T` / `WorkReport` 데이터 이관 스크립트

### 7.6 W3 완료 기준

- [ ] 사용자 그룹별 작성 화면 분기
- [ ] 일자별 1건 작성·수정·목록
- [ ] 대시보드 업무일지 위젯 LIVE
- [ ] `work_diary_group_id` 없는 사용자 가드

---

## 8. Wave 간 의존·일정

```text
[B0 사용자·WORK_DIARY_GROUP 시드] ──already done──┐
                                                   │
W1 공통 엔진 (2~3일) ──► W2 대시보드+UI (2~3일)     │
                              │                    │
                              └──────► W3 업무일지 (2~4일)
```

- W2는 W1 API **필수**
- W3는 W1과 **병렬 가능**하나, 대시보드 업무일지 위젯은 W3 API 후 연결
- INF-5 지급 등 TX Wave와 **독립** — 병행 가능

---

## 9. 레거시 데이터 이관 (후속 Wave 4)

| 레거시 | TO-BE | 비고 |
|--------|-------|------|
| `T_Notice` 등 6테이블 | `board_post` | 모두 `post_kind=TOP`, 답글 없으면 단일 행 |
| 첨부 파일 | `board_attachment` + 디스크 | `AppendFile` 경로 매핑 필요 |
| `CWD_T` | `work_diary_entry` | `field_values` JSON 변환 |
| EUC-KR → utf8mb4 | 이관 스크립트 | [basis-information-api-spec](../../results/sample/basis-information-api-spec.md) §8 |

---

## 10. 리스크·결정 사항

| # | 항목 | 기본안 | 결정 필요 |
|---|------|--------|-----------|
| 1 | 레이저 공지 레거시 | `DevelopmentList` / `LASER` | `LoaderList` 사용 여부 |
| 2 | 본문 에디터 | W2: plain `textarea` | rich text(HTML) 여부 |
| 3 | 파일 저장소 | 로컬 디스크 | 운영 NAS/S3 전환 시점 |
| 4 | 업무일지 템플릿 | W3: 17그룹 동일 4필드 | 그룹별 상이 양식 시점 |
| 5 | 게시판 메뉴 위치 | 대시보드 MORE 중심 | 사이드 전용 `게시판` 메뉴 추가 여부 |
| 6 | `prod-work-diary` 명칭 | 생산 작업일보 유지 | Community 업무일지는 `work-diary` id 사용 |

---

## 11. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 0.1 | 2026-07-07 | Wave 1~3 초안 — 공통 엔진·대시보드·업무일지 |
