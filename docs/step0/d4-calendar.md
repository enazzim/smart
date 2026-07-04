# D4 — 생산달력 (기본 + 작업장 Override)

> Step 0 산출물 · **확정 v0.1** (2메뉴 · 월간 캘린더 Modal · 상속 연산)  
> SSOT (레거시 감사): `MasterInfoRecordRUD.cs` `m_FieldName[12·13]` (각 8필드, 동일 구조)  
> 화면: `StandardProductionCalendarInfo.aspx` · `WCProductionCalendarInfo.aspx`  
> SmartManager: **`production_calendar`** (공장 기본) + **`work_center_calendar`** (작업장 sparse Override)  
> 레거시 테이블: **`PCI_MT`** 단일 (`WCName`으로 기준작업장·작업장 구분)

**관련:** [`step0-plan-D1-D4.md`](./step0-plan-D1-D4.md) · [`domain-event-projector-matrix.md`](./domain-event-projector-matrix.md) §2 · [`d4-work-center.md`](./d4-work-center.md) §2.4 · [시스템 컬럼 규칙](../../.cursor/rules/master-audit-fields.mdc) · TO-BE [`results/sample/basis-production-calendar-spec.md`](../../results/sample/basis-production-calendar-spec.md)

### v0.1 확정 요약

| 항목 | 레거시 (`PCI_MT`) | SmartManager v0.1 |
|------|-------------------|-------------------|
| 기본달력 | `WCName = '기준 작업장'` 행 | **`production_calendar`** (작업장 FK 없음) |
| 작업장달력 | 작업장별 **전일자 행** (복제·직접 저장) | **`work_center_calendar`** — **기본과 다른 날만** sparse |
| 기본 입력 시 전파 | 신규 등록 시 **전 작업장 INSERT 복제** | **복제 없음** — 조회 시 **상속 연산** |
| 기본 수정 시 | `기준 작업장` 행만 UPDATE (WC 행 미갱신) | `production_calendar` upsert → **Override 없는 전 WC effective 즉시 반영** |
| 작업장 등록 | `ProductionCalendarTable` — 기준작업장 **미래일 복사** | **부수효과 없음** ([`d4-work-center`](./d4-work-center.md)) |
| 날짜 키 | `WC_Year`·`WC_Month`·`WC_Day` | **`calendar_date` DATE** |
| 가동시간 | `WC_Time` DECIMAL (시간 혼용·8=8h) | **`work_time` INT 분**, 기본 **480** |
| UI | 월 캘린더 + 일자 입력 | **월간 캘린더 + Modal** (거래처형 그리드 **아님**) |
| Capa 연계 | 작업장·달력 조인 | **`EffectiveMinutes(W,D)`** §4 |

---

## 1. 분류 범례

| 분류 | 의미 |
|------|------|
| **Keep (UI)** | 일자 Modal — 가동분·비고 |
| **시스템(자동)** | PK·감사·소프트삭제·effective 연산 |
| **Drop** | `WCName` 문자열·년월일 분리 컬럼·PCI 100일 가로 스키마 |
| **Phase2** | 월 일괄 휴무·템플릿 API |

---

## 2. 레거시 → TO-BE 구조 대비 (핵심)

### 2.1 레거시 물리 복제

`StandardProductionCalendarInfo.aspx.cs` — 기준생산달력 **신규 등록** 시:

1. `기준 작업장` `PCI_MT` INSERT
2. 활성 `WCI_MT` **전 작업장**에 동일 년·월·일·시간·비고 **INSERT**

> **수정(Update)** 은 `기준 작업장` 행만 갱신 — 이미 복제된 작업장 행은 **자동 미갱신** (레거시 불일치).

작업장 등록 시 `ProductionCalendarTable()` — `기준 작업장` 미래 `PCI_MT`를 **신규 WC에 복사**.

### 2.2 SmartManager 상속 연산 (B5-R)

**DB에 작업장마다 행을 만들지 않음.** Capa·스케줄 조회 시:

```
EffectiveMinutes(작업장 W, 일자 D):

  1) work_center_calendar (W, D) Override 있음  →  Override.work_time
  2) 없음 → production_calendar (D)             →  기본 work_time
  3) 없음 → work_center.operation_time          →  마스터 폴백 (480분 등)
```

| 시나리오 | 레거시 | v0.1 |
|----------|--------|------|
| 공장 전체 휴무(0분) | 기준 등록 + WC fan-out INSERT | `production_calendar` 1건 — Override 없는 WC **전부 0** |
| 특정 WC 야근 720분 | 해당 WC `PCI_MT` 행 | `work_center_calendar` Override 1건 |
| Override 해제 | WC 행 삭제/수정 | DELETE override → **기본달력 상속** |

**업무 의도(기본 → 작업장 반영)는 유지**, 구현은 **복제 → 상속**으로 변경.

---

## 3. SmartManager — `production_calendar` (메뉴 9 · 기본생산달력)

### 3.1 스키마

| UI 라벨 | React field | DB 컬럼 | API | 필수 | 비고 |
|---------|-------------|---------|-----|------|------|
| 일자 | calendarDate | calendar_date | `calendarDate` | Y | UK(활성) |
| 가동시간(분) | workTime | work_time | `workTime` | Y | INT 0~1440, 기본 **480** |
| 비고 | content | content | `content` | N | 휴무 사유 등 |

| 시스템 | 컬럼 |
|--------|------|
| id, recording_state, created_*, updated_* | [공통 규칙](../../.cursor/rules/master-audit-fields.mdc) |

- `work_time = 0` → **휴무** (해당 일 Capa 0, Override 없을 때 전 작업장 effective 0)

### 3.2 API · UI

Base: `/api/v1/basis/production-calendars`

| Method | Endpoint | 설명 |
|--------|----------|------|
| GET | `/?year=&month=` | 월별 목록 |
| GET | `/{id}` | 단건 |
| PUT | `/by-date/{calendarDate}` | **upsert** by date |
| DELETE | `/by-date/{calendarDate}` | 소프트 삭제 또는 480 복원 정책 |

**UI:** `/basis/production-calendars` — **월간 캘린더** + 일자 클릭 Modal

**PUT 예시**

```json
{
  "workTime": 0,
  "content": "설비 정기점검 — 공장 휴무"
}
```

---

## 4. SmartManager — `work_center_calendar` (메뉴 10 · WC Override)

### 4.1 스키마 (sparse Override 전용)

| UI 라벨 | React field | DB 컬럼 | API | 필수 | 비고 |
|---------|-------------|---------|-----|------|------|
| 작업장 | workCenterId | work_center_id | `workCenterId` | Y | FK → `work_center.id` |
| 일자 | calendarDate | calendar_date | `calendarDate` | Y | UK 일부 |
| 가동시간(분) | workTime | work_time | `workTime` | Y | INT 0~1440 |
| 비고 | content | content | `content` | N | |

**UK:** `(work_center_id, calendar_date)` 활성 1건

- **기본달력과 동일한 값**이면 Override 행 **저장 안 함** (또는 자동 DELETE)
- Override DELETE → 해당 일 **기본달력 상속**

### 4.2 API · UI

Base: `/api/v1/basis/work-center-calendars`

| Method | Endpoint | 설명 |
|--------|----------|------|
| GET | `/overrides?workCenterId=&year=&month=` | Override 행만 |
| GET | `/effective?workCenterId=&year=&month=` | **연산 결과** (표시용) |
| PUT | `/overrides` | body upsert |
| DELETE | `/overrides?workCenterId=&calendarDate=` | 예외 해제 → 기본 상속 |

**UI:** `/basis/work-center-calendars` — **좌: 작업장 선택** + **우: 월간 캘린더** (effective 표시, Override 주황 배지)

**effective 응답 필드:** `effectiveWorkTime`, `isOverride`, `baseWorkTime`, `content`

**PUT 예시**

```json
{
  "workCenterId": 3,
  "calendarDate": "2026-06-25",
  "workTime": 720,
  "content": "납기 긴급 야간근무"
}
```

### 4.3 `EffectiveMinutes` (Capa 공통)

[`d4-work-center.md` §2.4](./d4-work-center.md) · [production-calendar-spec §3.3](../../results/sample/basis-production-calendar-spec.md)

`capacity_distinction = TIME` → 일 Capa = `EffectiveMinutes`  
`TIME_WORKERS` → `EffectiveMinutes × retention_staff` (Phase2)

---

## 5. 이벤트 · Projector

| 이벤트 | Projector | 비고 |
|--------|-----------|------|
| `StandardCalendarDayRegistered` / `Updated` / `Deleted` | — | `production_calendar` CRUD |
| `WorkCenterCalendarDayRegistered` / `Updated` / `Deleted` | — | Override CRUD |
| `WorkCenterRegistered` | **— (달력 복사 없음)** | 레거시 `ProductionCalendarTable` 폐지 |

> 상속 연산은 **조회·Capa 서비스**에서 수행 — 등록 이벤트로 WC 행을 fan-out 하지 않음.

---

## 6. 레거시 8필드 대조표 (메뉴 9·10 공통)

| # | 레거시 컬럼 | 기본달력 v0.1 | WC달력 v0.1 |
|---|------------|---------------|-------------|
| 1 | WCName | **Drop** — `production_calendar`에 없음 | → `work_center_id` FK |
| 2 | WC_Year | **Drop** | → `calendar_date` |
| 3 | WC_Month | **Drop** | 동일 |
| 4 | WC_Day | **Drop** | 동일 |
| 5 | WC_Time | **Keep** → `work_time` **분 INT** | Override `work_time` |
| 6 | Content | **Keep** | **Keep** |
| 7 | RecodingState | 시스템(자동) | 시스템(자동) |
| 8 | ProductionCalendarInfoIndex | `id` PK | `id` PK |

**레거시 UK:** `(WCName, WC_Year, WC_Month, WC_Day)`

---

## 7. Cut-over

| 항목 | 처리 |
|------|------|
| `기준 작업장` `PCI_MT` | → `production_calendar` (`calendar_date`, `work_time` = `ROUND(WC_Time×60)` 또는 레거시 규칙 실측) |
| 작업장별 `PCI_MT` | **기본과 다른 effective** 만 `work_center_calendar`로 이관 |
| 중복 fan-out 행 | 정리·dedup 후 Override만 잔존 |
| `기준 작업장` WC | Capa용 유지 여부 운영 정책 — 달력 템플릿 역할 **폐지** |

---

## 8. `results/` 문서와의 관계

| 문서 | 채택 |
|------|------|
| `sample/basis-production-calendar-spec.md` v1.0 | **SSOT** — 2계층·분·sparse·EffectiveMinutes·월 캘린더 UI |
| `sample/basis-work-center-spec.md` | 작업장 등록 시 달력 복사 폐지 |
| `sample/basis-information-api-spec.md` §5.4a·§5.4b | endpoint |

---

## 9. 체크리스트

- [x] v0.1 2계층·상속 연산·레거시 fan-out 대비 확정
- [x] `work_time` 분(INT)·`calendar_date` DATE
- [x] 작업장 등록 달력 복사 **폐지** 정합
- [x] Step 1 Flyway + production-calendars + work-center-calendars CRUD
- [ ] `EffectiveMinutes` Capa E2E (PRD-W3)
- [ ] `기준 작업장` PCI → `production_calendar` Cut-over 스크립트

---

*v0.1: 2026-07-02 · TO-BE: `results/sample/basis-production-calendar-spec` · 레거시: `PCI_MT`, `StandardProductionCalendarInfo.aspx.cs`*