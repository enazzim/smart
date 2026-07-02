# 공용코드 기준정보 확정 스펙

> **문서 버전:** 1.2  
> **작성일:** 2026-06-22  
> **상태:** 확정안 (구현 대기)  
> **관련 Wave:** S0(현행) → **S0-R** (공용코드 UI·API·스키마 정비)  
> **관련 문서:**  
> - [시스템정보 설계서](./system-information-spec.md) §3.1  
> - [작업장 기준정보 확정 스펙](./basis-work-center-spec.md)  
> - [기준정보 API·필드 매핑](./basis-information-api-spec.md)  
> - [기준정보 구현 설계](./basis-information-implementation-spec.md)

---

## 1. 문서 목적

시스템정보 **공용코드(`public_code`, 레거시 `PUC_MT`)** 의 업무 정의, 계층 데이터 모델, CRUD·유효성, API·화면, **용도(`usage_type`)** 기반 확장·참조 검사, 현행 구현과의 갭·후속 Wave를 정리한다.

공용코드는 ERP 전역 **코드 사전(SoT)** 이다. 단위·분류·공정·부적합사유 등 **대분류를 운영자가 추가**할 수 있으며, 타 모듈은 `small_code` 문자열로 참조한다.

---

## 2. 메뉴·역할

| 항목 | 내용 |
|------|------|
| 메뉴 위치 | **시스템정보** — `/system/public-codes` (기준정보 메뉴 아님) |
| 관리 | `system:public-code:read` / `write` — CRUD |
| 조회(타 모듈) | `GET /api/v1/basis/public-codes` — 인증 사용자 **읽기 전용** |
| Wave | **S0-R** (B1-R 선행·병행 가능) |

레거시 하단 부가 그리드(자료실·일괄수정 등)는 **구현하지 않음**.

---

## 3. 업무 정의

| 원칙 | 내용 |
|------|------|
| 구조 | **대분류(그룹) 1 : N 소분류(상세 코드)** |
| 행 모델 | 레거시 `PUC_MT` — **대분류 전용 행** + **소분류 행** (단일 테이블) |
| PK | `id` (BIGINT, 레거시 `Index` 대체) |
| 외부 참조 | 기본: **`small_code` 문자열** 저장. **예외:** [설비](./basis-equipment-spec.md) `equipment_category_id`, [공정순서](./basis-process-sequence-spec.md) `public_code_id` → `public_code.id` FK |
| 삭제 | 물리 삭제 금지, `recoding_state = 0` 소프트 삭제 |
| 용도 | 대분류 헤더에 **`usage_type`** — 콤보·참조 검사·검증 전략 결정 |

### 3.1 확장 3단계 (Tier)

| Tier | 명칭 | 동작 | 배포 없이 대분류 추가 |
|:----:|------|------|:---------------------:|
| **1** | `GENERIC` | 마스터 CRUD, 콤보, 저장 시 **존재 검증**만. 삭제 시 **타 테이블 참조 검사 없음** | ● |
| **2** | `REFERENCED` | Tier 1 + **삭제 시** `usage_type`별 **참조 검사기** 실행 | ● (코드값). 검사기는 모듈별 등록 |
| **3** | 레지스트리 | `large_code` ↔ `(table, column)` DB 매핑으로 삭제 검사 | 2차 검토, **1차 비권장** |

1차 구현: Tier 1 전체 + Tier 2 **`PROCESS`만**.

---

## 4. 데이터 모델

### 4.1 `public_code` — 컬럼

| 컬럼 | API 필드 | 타입 (TO-BE) | Null | 설명 |
|------|----------|--------------|:----:|------|
| `id` | `id` | BIGINT PK | N | |
| `large_code` | `largeCode` | VARCHAR(20) | N | 대분류 코드 |
| `large_name` | `largeName` | VARCHAR(100) | N | 대분류 명칭 |
| `small_code` | `smallCode` | VARCHAR(20) | **Y** | 소분류 코드. **대분류 전용 행은 NULL** |
| `small_name` | `smallName` | VARCHAR(100) | **Y** | 소분류 명칭. **대분류 전용 행은 NULL** |
| `usage_type` | `usageType` | VARCHAR(20) | N | **대분류 헤더 행만** 유효. 소분류 행은 대분류와 동일 값 복제 저장 |
| `recoding_state` | — | TINYINT | N | `1`=유효, `0`=삭제 |
| `created_by` / `created_at` | — | | | 시스템 |
| `updated_by` / `updated_at` | — | | | 시스템 |

**행 종류**

| 종류 | `small_code` / `small_name` | `usage_type` |
|------|-----------------------------|--------------|
| **대분류 행** (헤더) | 둘 다 **NULL** | **필수** (등록 시 선택) |
| **소분류 행** | 둘 다 **NOT NULL** | 헤더와 **동일** (서버 복제) |

**NULL 규칙 (CHECK 또는 앱 검증)**

```
(small_code IS NULL AND small_name IS NULL)   -- 대분류 행
OR (small_code IS NOT NULL AND small_name IS NOT NULL)   -- 소분류 행
```

### 4.2 `usage_type` (1차)

| 값 | Tier | 설명 | 시드 예 |
|----|:----:|------|---------|
| `GENERIC` | 1 | 일반 — 존재 검증·콤보만 | 분류, 부적합내용 등 |
| `PROCESS` | 2 | 공정 — 삭제 시 다수 마스터·트랜잭션 참조 검사 | `large_code = 1400` |
| `UNIT` | 2 | 단위 — 품목 `unit` 등 연동 시 (후속) | 예: `1500` |
| `NC_REASON` | 2 | 부적합 사유 (품질 모듈 후속) | — |
| `NC_DETAIL` | 2 | 부적합 내용 (품질 모듈 후속) | — |
| `WORK_DIARY_GROUP` | 1 | 업무일지그룹 — [사용자 스펙](./basis-user-spec.md) `work_diary_group_id` FK | — |

- 신규 `usage_type` 추가 = **검사기(Validator) 클래스 등록** (코드 배포). **코드값(소분류) 자체**는 운영자가 계속 추가 가능.
- `PROCESS` 대분류의 콤보 API: `usage_type = PROCESS` 필터. **`1400` 하드코드 금지** (시드 기본값일 뿐).

**공정 더미 코드 제외** (`usage_type = PROCESS` 조회 시)

- `14000000`, `14009999` — 선택 목록·저장 검증에서 제외

### 4.3 고유성 (UK) — 활성 `recoding_state = 1` 만

| # | 규칙 | 대상 |
|---|------|------|
| ① | `large_code` 유일 | 대분류 헤더 행 (`small_code IS NULL`) |
| ② | `(large_code, small_code)` 유일 | 소분류 행 (`small_code IS NOT NULL`) |

- **서로 다른 대분류에 동일 `small_code` 문자열 허용.**
- 비활성(`recoding_state = 0`) 행은 UK 판정 **제외** (동일 코드 재등록 가능).
- 현행 `uk_public_code_small` (전역 `small_code` UK) → **폐기**.

---

## 5. 등록·수정·삭제 프로세스

### 5.1 등록 (Create)

**5.1.1. 대분류 등록**

- 입력: `largeCode`, `largeName`, **`usageType`**
- `smallCode`, `smallName` — **NULL** 로 저장
- ① UK 검증

**5.1.2. 소분류 등록**

- UI: 좌측에서 **대분류 선택** 후 소분류 코드·명칭 입력
- 서버: 선택된 대분류의 `large_code`, `large_name`, `usage_type` 을 소분류 행에 **복제**
- ② UK 검증, 활성 대분류 헤더 존재 검증

### 5.2 수정 (Update)

| 필드 | 대분류 행 | 소분류 행 |
|------|:---------:|:---------:|
| `large_code` | **수정 불가** | **수정 불가** |
| `large_name` | 수정 가능 → **동일 `large_code` 활성 소분류 행 `large_name` 일괄 UPDATE** | 읽기 전용(표시) |
| `usage_type` | 수정 가능. `PROCESS` → `GENERIC` 는 **참조 없을 때만** | (헤더 따름) |
| `small_code` | — | **수정 불가** |
| `small_name` | — | 해당 행만 수정 |

### 5.3 삭제 (소프트 삭제)

**5.3.1. 소분류 삭제**

1. `usage_type` 이 Tier 2이면 **§6.3 참조 검사** — 참조 중이면 거부
2. 해당 행 `recoding_state = 0`

**5.3.2. 대분류 삭제 (연쇄)**

1. 해당 `large_code` 의 **활성 소분류** 각각에 §6.3 참조 검사
2. 하나라도 참조 중이면 **대분류 삭제 전체 거부**
3. 모두 통과 시: 대분류 헤더 행 + 하위 소분류 행 **`recoding_state = 0` 일괄**

---

## 6. 유효성·제약

### 6.1 공통

| 규칙 | 내용 |
|------|------|
| 활성 조회 | `recoding_state = 1` |
| 콤보·기준정보 API | **`small_code IS NOT NULL`** 만 반환 (대분류 헤더 제외) |
| 저장 검증 (공통) | `PublicCodeValidator.assertActive(largeCode, smallCode)` — 활성 소분류 존재 |

### 6.2 API 조회 범위

| API | 권한 | 반환 |
|-----|------|------|
| `GET /api/v1/system/public-codes` | `system:public-code:read` | 활성 전체 (헤더+소분류, 관리 UI) |
| `GET /api/v1/system/public-codes/large` | 동일 | 활성 **대분류 헤더만** |
| `GET /api/v1/system/public-codes/small?largeCode=` | 동일 | 활성 **소분류** (`largeCode` 필터) |
| `GET /api/v1/basis/public-codes?largeCode=` | 인증 | 활성 **소분류만** |
| `GET /api/v1/basis/public-codes?usageType=PROCESS` | 인증 | 활성 소분류, `usage_type` 일치, 더미 공정 제외 |

### 6.3 삭제 참조 검사 (`usage_type`별)

Tier 2 검사기는 **Spring Bean 등록** (`PublicCodeReferenceChecker` 인터페이스). 1차 **`PROCESS`만** 구현.

| `usage_type` | 검사 대상 (활성 레코드, 코드 일치) | 1차 |
|--------------|-----------------------------------|:---:|
| `PROCESS` | `work_center.main_process_code`, `process_sequence.public_code_id`, `work_standard.process_sequence_id`, `unit_cost.begin_process_code_id` / `end_process_code_id`, `inventory_balance.process_code`, … | ● |
| `UNIT` | `item.unit` (품목 스펙 연동 후) | 2차 |
| `NC_REASON` / `NC_DETAIL` | 품질·일보 모듈 컬럼 (정의 후) | 2차 |
| `GENERIC` | 없음 | — |
| `WORK_DIARY_GROUP` | `app_user.work_diary_group_id` | B0-R |

에러 예: `「{smallName}({smallCode})」은(는) 작업장·공정순서에서 사용 중이라 삭제할 수 없습니다.`

### 6.4 소비 모듈 연동 (저장 시)

| 모듈 | 검증 | 관련 스펙 |
|------|------|-----------|
| 작업장 `mainProcessCode` | `usage_type = PROCESS` 인 활성 소분류 + 더미 제외 | [작업장 스펙](./basis-work-center-spec.md) §5.2 |
| 공정순서 `processCode` | 동일 | [기준정보 API](./basis-information-api-spec.md) §5.5 |
| (후속) 품목 `unit` | `usage_type = UNIT` | [품목 스펙](./basis-item-spec.md) |
| (후속) 부적합 필드 | `NC_REASON` / `NC_DETAIL` | — |

---

## 7. 화면 (`/system/public-codes`)

### 7.1 마스터-디테일

```
┌──────────────────────┬─────────────────────────────────────────┐
│ 대분류          [등록]│ {선택 대분류명} — 소분류            [등록]│
├──────────────────────┼─────────────────────────────────────────┤
│ 1400 공정  PROCESS   │ 14000001  절단      [수정][삭제]          │
│ 1500 단위  UNIT      │ 14000002  성형      [수정][삭제]          │
│ 1600 부적합 GENERIC  │ ...                                     │
└──────────────────────┴─────────────────────────────────────────┘
```

| 요소 | 동작 |
|------|------|
| 좌측 | 활성 대분류 헤더 목록 (`large_code`, `large_name`, `usage_type`) |
| 대분류 등록 Modal | `largeCode`, `largeName`, **`usageType`** 선택 |
| 우측 | 선택된 `large_code` 의 활성 소분류 |
| 소분류 등록 Modal | `smallCode`, `smallName` (대분류는 선택 컨텍스트) |
| 수정 | §5.2 — 코드 필드 **disabled**, 명칭·(대분류만) `usageType` |
| 삭제 | §5.3 |

### 7.2 프론트 소비 컴포넌트

| 컴포넌트 | 1차 API |
|----------|---------|
| `ProcessCodeSelect` | `GET /basis/public-codes?usageType=PROCESS` |
| `PublicCodeSelect` | `?largeCode=` 또는 `?usageType=` |
| (후속) `UnitCodeSelect` | `?usageType=UNIT` |

`PUBLIC_CODE_LARGE_PROCESS = '1400'` 상수 → **제거**, `usageType` 기반으로 통일.

---

## 8. API

### 8.1 시스템 (관리)

**Base:** `/api/v1/system/public-codes`

| Method | Endpoint | 설명 |
|--------|----------|------|
| GET | `/large` | 대분류 헤더 목록 |
| GET | `/small?largeCode=` | 소분류 목록 |
| GET | `/{id}` | 단건 |
| POST | `/large` | 대분류 등록 |
| POST | `/small` | 소분류 등록 (`largeCode` + small 필드) |
| PUT | `/large/{largeCode}` | 대분류명·`usageType` (코드 불변) |
| PUT | `/small/{id}` | 소분류명만 |
| DELETE | `/large/{largeCode}` | 대분류 연쇄 소프트 삭제 |
| DELETE | `/small/{id}` | 소분류 소프트 삭제 |

**POST `/large` body**

```json
{
  "largeCode": "1600",
  "largeName": "부적합사유",
  "usageType": "GENERIC"
}
```

**POST `/small` body**

```json
{
  "largeCode": "1400",
  "smallCode": "14000001",
  "smallName": "절단"
}
```

### 8.2 기준정보 (읽기)

**Base:** `/api/v1/basis/public-codes`

| Query | 설명 |
|-------|------|
| `largeCode` | 해당 대분류 소분류만 |
| `usageType` | 용도별 소분류 (`PROCESS` 시 더미 제외) |
| (없음) | 활성 소분류 전체 |

**Response (소분류)**

```json
{
  "id": 10,
  "largeCode": "1400",
  "largeName": "공정",
  "smallCode": "14000001",
  "smallName": "절단",
  "usageType": "PROCESS"
}
```

---

## 9. 시드·마이그레이션

### 9.1 시드 (예시)

| large_code | large_name | usage_type | 비고 |
|------------|------------|------------|------|
| `1400` | 공정 | `PROCESS` | 헤더 행 + 소분류 다수 |
| `1500` | 단위 | `UNIT` | 헤더 + EA, KG … |

더미: `14000000`, `14009999` — `PROCESS` 콤보 제외.

### 9.2 현행 데이터 이관

| 단계 | 내용 |
|------|------|
| 1 | `small_code`/`small_name` NULL 허용, `usage_type` 컬럼 추가 |
| 2 | `DISTINCT large_code` 별 **헤더 행 INSERT** (`usage_type` 매핑 테이블 또는 수동) |
| 3 | 기존 평면 행 → 소분류 행 유지, `usage_type` 헤더와 동기화 |
| 4 | `uk_public_code_small` 삭제 → ①② UK (앱 검증 또는 부분 인덱스) |

---

## 10. 현행 구현과의 차이 (갭)

| 항목 | 현행 | TO-BE |
|------|------|-------|
| 행 모델 | 모든 행에 large+small **필수** | **대분류 헤더** (small NULL) |
| UK | 전역 `small_code` | **(large_code, small_code)** |
| UI | 4필드 **평면** 그리드 | **마스터-디테일** |
| `usage_type` | 없음 | 대분류 헤더에 **필수** |
| 공정 콤보 | `largeCode=1400` 하드코드 | **`usageType=PROCESS`** |
| 대분류명 연쇄 수정 | 없음 | **일괄 UPDATE** |
| 대분류 연쇄 삭제 | 없음 | **소분류 포함 일괄** |
| 삭제 참조 검사 | 없음 | **`usage_type`별 검사기** |
| 시드 | 없음 | **1400 공정 등** |
| 품목 `unit` | 자유 입력 | **2차** `UNIT` 연동 |

---

## 11. 구현·후속 Wave (할 일)

### 11.1 S0-R — 공용코드

- [ ] Flyway: `usage_type`, NULL 소분류, UK 변경
- [ ] `PublicCode` 엔티티·Repository — 헤더/소분류 쿼리 분리
- [ ] `PublicCodeService` — §5 CRUD, §5.2 일괄 `large_name`, §5.3 연쇄 삭제
- [ ] `PublicCodeValidator` — 활성 소분류 존재 검증
- [ ] `ProcessReferenceChecker` — `usage_type = PROCESS` 삭제 검사
- [ ] API 분리 — `/system/public-codes/large|small`, `/basis/public-codes?usageType=`
- [ ] 프론트 `PublicCodesPage` — 마스터-디테일
- [ ] `ProcessCodeSelect` — `usageType=PROCESS`, `1400` 상수 제거
- [ ] `WorkCenterService` 등 — `assertActiveProcess` / PROCESS 검증 연동 ([작업장 B1-R](./basis-work-center-spec.md) §9.1)
- [ ] 시드: `1400` 공정, 더미, (선택) `1500` 단위
- [ ] 통합테스트 — CRUD, UK, 연쇄 삭제, PROCESS 참조 거부

### 11.2 후속

- [ ] `UNIT` — [품목 스펙](./basis-item-spec.md) `unit` 콤보 연동 + `UnitReferenceChecker`
- [ ] `NC_REASON` / `NC_DETAIL` — 품질·작업일보 모듈 확정 후 검사기 등록
- [ ] Tier 3 레지스트리 — 필요 시만
- [ ] `POST /system/imports/master-data/public-code` — S1 벌크 Import
- [ ] [system-information-spec.md](./system-information-spec.md) §3.1, [basis-information-api-spec.md](./basis-information-api-spec.md) 동기화

---

## 12. 의사결정 요약

| 항목 | 확정 |
|------|------|
| 테이블 | 단일 `public_code`, 대분류 헤더(small NULL) + 소분류 행 |
| UK | 활성 기준 `(large_code)` 헤더, `(large_code, small_code)` 소분류 |
| 대분류코드·소분류코드 | 등록 후 **수정 불가** |
| 대분류명 | 수정 시 하위 소분류 **`large_name` 일괄 갱신** |
| `usage_type` | 대분류 등록 시 선택 — **콤보·삭제 검사의 기준** (`1400` 하드코드 없음) |
| Tier 1 | `GENERIC` — 마스터·콤보·존재 검증, 삭제 참조 검사 없음 |
| Tier 2 (1차) | **`PROCESS`만** 삭제 참조 검사 |
| 기준정보 API | **소분류만**, `recoding_state = 1` |
| 컬럼 길이 | code 20, name 100 |
| UI | 시스템정보 마스터-디테일 |

---

## 13. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-06-22 | 공용코드 확정안 — PUC_MT 계층, usage_type, Tier 1/2, UK·API·참조 검사 |
| 1.1 | 2026-06-22 | [작업장 스펙](./basis-work-center-spec.md) §6.4·§11.1 연동 링크 |
| 1.2 | 2026-06-22 | §3 외부 참조 — 설비 `equipment_category_id` FK 예외 명시 |
