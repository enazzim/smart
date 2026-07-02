# D4 — 공용코드 (`PublicUseCode` → `PUC_MT`)

> Step 0 산출물 · **확정 v0.1** (마스터-디테일 · `code_group` · `usage_type` · FK 소비)  
> SSOT (레거시 감사): `KIT_ERP/BasisInformation/MasterInfoRecordRUD.cs` `m_FieldName[11]` (12필드)  
> 화면: `SystemInfoManagement/PublicUseCode.aspx` / `PublicUseCode.aspx.cs`  
> SmartManager: **`public_code`** (+ 메타 **`code_group`**)  
> 내용키(UK): 대분류 `(large_code)` · 소분류 `(large_code, small_code)` — 활성만  
> PK: `id` ← `PublicUseCodeIndex`

**관련:** [`step0-plan-D1-D4.md`](./step0-plan-D1-D4.md) · [`domain-event-projector-matrix.md`](./domain-event-projector-matrix.md) §0 · [`공용코드-콤보박스-매핑-초안-code-group-field-binding.md`](./공용코드-콤보박스-매핑-초안-code-group-field-binding.md) · [시스템 컬럼 규칙](../../.cursor/rules/master-audit-fields.mdc) · TO-BE [`results/sample/basis-public-code-spec.md`](../../results/sample/basis-public-code-spec.md)

> **메뉴 위치:** 시스템정보 `/system/public-codes` — 기준정보 11메뉴 **선행** 인프라.  
> 타 화면 콤보 소비 규칙은 [공용코드 문서 §4·§8](./공용코드-콤보박스-매핑-초안-code-group-field-binding.md).

### v0.1 확정 요약

| 항목 | 레거시 (12필드·평면 행) | SmartManager v0.1 |
|------|------------------------|-------------------|
| UI | 좌 대분류 선택 · 우 소분류 등록/수정 | **마스터-디테일** (동일 패턴 강화) |
| 대분류 | DB 시드 · UI **선택만** | 헤더 행 + `usage_type` · `editable_level`별 등록 |
| 소분류 | 명칭 입력 · 코드 **+10 자동** | **서버 채번** (+10) · 예약코드 건너뛰기 |
| 행 모델 | 모든 행 large+small 필수 | **헤더**(small NULL) + **소분류** 행 |
| UK | `(LargeCode, SmallCode)` | 동일 의미 · 활성만 |
| 소비 저장 | 8자리 `SmallClassificationCode` | **`public_code.id` FK** (`*_id`) |
| 콤보 조회 | aspx 하드코딩 `large_code`/`Name` | **`code_group_key`** → `/api/code-groups/{key}/options` |
| 예약코드 | aspx **수정 금지** 하드코딩 | `code_group.exclude_codes` + API 제외 |
| 삭제 참조 | 없음 | `usage_type` Tier2 — 1차 **`PROCESS`만** |
| 등록 부수효과 | 없음 | **없음** |

---

## 1. 분류 범례

| 분류 | 의미 |
|------|------|
| **Keep (UI)** | 관리 화면·API 사용자 입력 |
| **시스템(자동)** | PK·감사·소프트삭제·소분류 자동채번 |
| **SYSTEM** | 시드·예약 — 운영자 **수정·삭제 불가** |
| **Drop** | 레거시 하단 부가 그리드·일괄수정 |
| **Phase2** | Tier3 레지스트리 · 벌크 Import |

---

## 2. SmartManager 스키마

### 2.1 `public_code` (데이터 SoT)

| UI 라벨 | React field | DB 컬럼 | API | 필수 | 분류 | 비고 |
|---------|-------------|---------|-----|------|------|------|
| 대분류코드 | largeCode | large_code | `largeCode` | Y | Keep* | 등록 후 **수정 불가** |
| 대분류명 | largeName | large_name | `largeName` | Y | Keep | 수정 시 동일 large 하위 **일괄 갱신** |
| 소분류코드 | smallCode | small_code | `smallCode` | 소분류● | 시스템/Keep | **서버 +10 채번** 기본 |
| 소분류명 | smallName | small_name | `smallName` | 소분류● | Keep (UI) | |
| 용도 | usageType | usage_type | `usageType` | 헤더● | Keep (UI) | §2.3 — 콤보·삭제 검사 기준 |
| (PK) | id | id | `id` | — | 시스템 | |
| 레코드상태 | — | recording_state | — | — | 시스템 | |
| 감사 | — | created_* / updated_* | — | — | 시스템 | |

\* `editable_level = FULL` 대분류만 UI에서 신규 등록. `SMALL_ONLY`·`SYSTEM`은 시드.

**행 종류 (CHECK)**

```
(small_code IS NULL AND small_name IS NULL)           -- 대분류 헤더
OR (small_code IS NOT NULL AND small_name IS NOT NULL) -- 소분류
```

### 2.2 `code_group` (콤보 메타 — [공용코드 문서 §7](./공용코드-콤보박스-매핑-초안-code-group-field-binding.md))

| 컬럼 | 설명 |
|------|------|
| `code_group_key` | PK — e.g. `PROCESS_CODE`, `ITEM_CLASSIFICATION_1` |
| `large_code` | `public_code.large_code` 와 1:1 (활성 헤더) |
| `large_name` | 표시용 — 헤더 `large_name` 동기화 |
| `usage_type` | 헤더와 **동일** — 옵션 API·삭제 검사 라우팅 |
| `editable_level` | `SYSTEM` \| `SMALL_ONLY` \| `FULL` |
| `exclude_codes` | JSON — 예약 소분류 (`14000000` 등) |
| `description` | 관리자 메모 |

> `code_group`은 **저장 테이블이 아님**. 화면 필드 바인딩·옵션 조회 메타. 실데이터는 `public_code`.

### 2.3 `usage_type` (Tier)

| 값 | Tier | 삭제 참조 검사 | 대표 `code_group_key` |
|----|:----:|:--------------:|----------------------|
| `GENERIC` | 1 | 없음 | 분류·부적합 등 |
| `PROCESS` | 2 | **● 1차** | `PROCESS_CODE` (`1400`) |
| `UNIT` | 2 | 2차 | `UNIT_GENERAL` (`0400`) |
| `NC_REASON` / `NC_DETAIL` | 2 | 2차 | `QC_*` |
| `WORK_DIARY_GROUP` | 1 | `user.work_diary_group_id` | `WORK_DIARY_GROUP` |

**`PROCESS` 콤보·저장 검증:** 더미 `14000000`, `14009999` 제외 ([§5](./d4-public-code.md)).

### 2.4 UK (활성 `recording_state = 1` 만)

| # | 규칙 |
|---|------|
| ① | `large_code` 유일 — 헤더 행 (`small_code IS NULL`) |
| ② | `(large_code, small_code)` 유일 — 소분류 행 |

비활성 행은 UK 판정 제외 (동일 코드 재등록 가능).

---

## 3. 관리 UI (`/system/public-codes`)

레거시와 동일 **좌 대분류 · 우 소분류**. 하단 부가 그리드·일괄수정 **없음**.

```
┌──────────────────────┬─────────────────────────────────────────┐
│ 대분류          [등록]*│ {선택 대분류} — 소분류              [등록]│
├──────────────────────┼─────────────────────────────────────────┤
│ 1400 공정  PROCESS   │ 14000010  절단      [수정][삭제]          │
│ 0210 품목분류1       │ ...                                     │
└──────────────────────┴─────────────────────────────────────────┘
```

\* `[등록]` — `editable_level = FULL` 그룹만 (관리자).

### 3.1 대분류 Modal (`editable_level = FULL`)

| 필드 | 필수 | 비고 |
|------|------|------|
| largeCode | Y | 4자리 · UK ① |
| largeName | Y | |
| usageType | Y | ENUM §2.3 |

등록 시: 헤더 행 INSERT + `code_group` 행 INSERT (트랜잭션).

### 3.2 소분류 Modal (일반 운영 — 레거시 동일)

| 필드 | 필수 | 비고 |
|------|------|------|
| smallName | Y | **유일한 필수 입력** (레거시) |
| smallCode | — | **서버 자동** — `MAX(하위4자리)+10`, `exclude_codes`·예약 구간 skip |
| largeCode | — | 좌측 선택 컨텍스트 |

수정: **`small_name`만** (코드 disabled). 예약·SYSTEM 행은 수정 불가.

### 3.3 삭제

| 대상 | 규칙 |
|------|------|
| 소분류 | Tier2 → 참조 검사 후 soft delete |
| 대분류 | 활성 소분류 전부 참조 검사 → 헤더+하위 **일괄** soft delete |
| SYSTEM·예약 | **삭제 불가** |

---

## 4. 소비 API · React (타 화면)

기준정보·생산 화면은 **관리 API 직접 호출 금지**. [공용코드 문서 §8.1](./공용코드-콤보박스-매핑-초안-code-group-field-binding.md):

| API | 용도 |
|-----|------|
| `GET /api/code-groups/{codeGroupKey}/options` | `CodeSelect` 옵션 — `{ id, value, label }` |
| `GET /api/code-groups` | 관리자 — 그룹 목록 |

```tsx
<CodeSelect
  codeGroup="PROCESS_CODE"
  valueField="id"
  excludeReserved
  value={form.processCodeId}
/>
```

| 계층 | 규칙 |
|------|------|
| 옵션 `id` | `public_code.id` — **저장 FK** |
| 옵션 `value` | `small_code` (8자리) — 표시·레거시 호환 |
| DB 자식 | `*_id` BIGINT → `public_code.id` **만** |
| 금지 | 자식에 `small_code` 문자열 FK 대용 |

**필드 바인딩:** [공용코드 문서 §4](./공용코드-콤보박스-매핑-초안-code-group-field-binding.md) — 거래처·품목·공정·설비·단가·사용자 등.

---

## 5. 시스템 예약 소분류

레거시 `PublicUseCode.aspx.cs` 수정 금지 목록 → `code_group.exclude_codes`:

| small_code | large | 용도 | code_group |
|------------|-------|------|------------|
| `14000000` | `1400` | 소재공정 | `PROCESS_CODE` |
| `14009999` | `1400` | 최종공정 | `PROCESS_CODE` |
| `07200010` | `0720` | 공구 | `EQUIPMENT_CLASS` |
| `07200020` | `0720` | 치구 | `EQUIPMENT_CLASS` |
| `06300010` | `0630` | 양산품코드 | `ITEM_STATE` |
| `03200010`~`03300010` | `0310`~`0330` | 사업·실행계획 | (Phase2) |
| `16000000`~`16000010` | `1600` | 이력 | (Phase2) |

옵션 API: 기본 **제외**. 관리 화면: **readonly**.

---

## 6. 이벤트 · Projector

| 이벤트 | Projector | 비고 |
|--------|-----------|------|
| `PublicCodeCreated` | — | 부수효과 없음 |
| `PublicCodeUpdated` | — | `large_name` 일괄 시 이벤트 1건 또는 배치 |
| `PublicCodeDeleted` | — | Tier2 참조 검사는 **서비스 동기** |

**`PROCESS` 삭제 참조 (1차):** `work_center.main_process_code_id`, `process_sequence.public_code_id`, `unit_price.begin/end_process_code_id`, … ([sample §6.3](../../results/sample/basis-public-code-spec.md))

---

## 7. REST API (관리)

Base: `/api/v1/system/public-codes`  
권한: `system:public-code:read` / `write`

| Method | Endpoint | 설명 |
|--------|----------|------|
| GET | `/large` | 활성 대분류 헤더 |
| GET | `/small?largeCode=` | 활성 소분류 |
| GET | `/{id}` | 단건 |
| POST | `/large` | 대분류+`code_group` (FULL만) |
| POST | `/small` | 소분류 — `smallName` (+ 선택 `smallCode`) |
| PUT | `/large/{largeCode}` | `largeName`, `usageType` |
| PUT | `/small/{id}` | `smallName`만 |
| DELETE | `/large/{largeCode}` | 연쇄 soft |
| DELETE | `/small/{id}` | soft |

**POST `/small` (운영 일반)**

```json
{
  "largeCode": "1400",
  "smallName": "절단"
}
```

**Response (소분류)**

```json
{
  "id": 42,
  "largeCode": "1400",
  "largeName": "공정",
  "smallCode": "14000010",
  "smallName": "절단",
  "usageType": "PROCESS"
}
```

---

## 8. 레거시 12필드 대조표

| # | 레거시 컬럼 | v0.1 | 비고 |
|---|------------|------|------|
| 1 | LargeClassificationCode | **Keep** | → `large_code` UK |
| 2 | LargeClassificationName | **Keep** | → `large_name` |
| 3 | SmallClassificationCode | **시스템** | +10 채번 · 수정 불가 |
| 4 | SmallClassificationName | **Keep (UI)** | |
| 5 | RecodingState | 시스템 | `recording_state` |
| 6~10 | Registration* | 시스템 | 감사 |
| 11~12 | Updating* | 시스템 | 감사 |
| — | (신규) | **Keep (UI)** | `usage_type` — 헤더만 |
| — | PublicUseCodeIndex | `id` PK | |

**레거시 UI 입력:** 대분류 **선택** + 소분류명 1개 → v0.1 **동일 운영 UX**.

**레거시 미구현·TO-BE 추가:** 대분류 신규 등록(`FULL`), `usage_type`, 삭제 참조, 헤더 행(small NULL).

---

## 9. `code_group` ↔ `public_code` 동기화

| 이벤트 | 동작 |
|--------|------|
| 대분류 등록 | `public_code` 헤더 + `code_group` INSERT |
| `large_name` 수정 | 헤더 + 동일 `large_code` 소분류 + `code_group.large_name` 일괄 |
| 소분류 CRUD | `public_code`만 — `code_group` 불변 |
| Cut-over | `PUC_MT` export → 헤더 행 생성 + §3 `code_group` 시드 |

§3 대분류 일람 TBD → [공용코드 문서 §9](./공용코드-콤보박스-매핑-초안-code-group-field-binding.md) SQL export 후 확정.

---

## 10. `results/` 문서와의 관계

| 문서 | 채택 |
|------|------|
| `sample/basis-public-code-spec.md` v1.2 | 행 모델·UK·`usage_type`·Tier·삭제 검사 **SSOT** |
| Step 0 v0.1 | 소비 API — **`/api/code-groups/{key}/options`** (sample `?usageType=` 대신) |

| 항목 | sample | Step 0 v0.1 |
|------|--------|-------------|
| 소비 조회 | `GET /basis/public-codes?usageType=` | **`GET /code-groups/{key}/options`** |
| FK 저장 | `public_code.id` | **동일** |
| 관리 UI·CRUD | `/system/public-codes` | **동일** |
| 소분류 채번 | body에 `smallCode` | **서버 +10 기본** (body 선택 허용) |

---

## 11. 체크리스트

- [x] v0.1 12필드 대조 · 마스터-디테일 · 예약코드
- [x] `code_group` + `usage_type` + FK 소비 규칙
- [x] domain-event §0 부수효과 없음
- [x] `PUC_MT` DISTINCT interim → [`data/puc-large-distinct.csv`](./data/puc-large-distinct.csv) · §3 v0.2
- [ ] Flyway `public_code` 헤더/소분류 + `code_group` 시드
- [ ] `ProcessReferenceChecker` + `CodeSelect` 공통 컴포넌트
- [ ] `WORK_DIARY_GROUP` Flyway 시드 — [`data/work-diary-group-seed.csv`](./data/work-diary-group-seed.csv) (`1900`)

---

*v0.1: 2026-07-02 · TO-BE: `results/sample/basis-public-code-spec` · 레거시: `MasterInfoRecordRUD.cs` m_FieldName[11], `PublicUseCode.aspx.cs`*