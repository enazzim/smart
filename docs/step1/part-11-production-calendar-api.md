# Step 1 Part 11 — 생산달력 2계층 CRUD

> **완료일:** 2026-07-04  
> **범위:** 기본생산달력 + 작업장 Override · EffectiveMinutes 연산 · React 월간 캘린더 UI  
> **설계 SSOT:** [`docs/step0/d4-calendar.md`](../step0/d4-calendar.md) v0.1  
> **다음:** Step 1 마무리 · UI 공통화 · Git 커밋

---

## 1. 요약

| 항목 | 결과 |
|------|------|
| Flyway | `V011__production_calendar.sql` — `production_calendar` + `work_center_calendar` |
| 기본 API | `GET/PUT/DELETE /api/v1/basis/production-calendars` |
| WC API | `GET/PUT/DELETE /api/v1/basis/work-center-calendars` (overrides + effective) |
| 연산 | Override → 기본달력 → `work_center.operation_time` (480분 폴백) |
| 부수효과 | **없음** (작업장 등록 시 달력 복사 없음) |
| Gradle·프론트 빌드 | ✅ `gradlew build -x test` · `npm run build` |

---

## 2. API 명세

### 2.1 기본생산달력

Base: `/api/v1/basis/production-calendars`

| 메서드 | 경로 | 설명 |
|--------|------|------|
| `GET` | `/?year=&month=` | 월별 등록 행 (sparse) |
| `GET` | `/{id}` | 단건 |
| `PUT` | `/by-date/{calendarDate}` | upsert |
| `DELETE` | `/by-date/{calendarDate}` | 소프트 삭제 |

**PUT 예시**

```json
{
  "workTime": 0,
  "content": "설비 정기점검 — 공장 휴무"
}
```

### 2.2 작업장 Override

Base: `/api/v1/basis/work-center-calendars`

| 메서드 | 경로 | 설명 |
|--------|------|------|
| `GET` | `/overrides?workCenterId=&year=&month=` | Override 행만 |
| `GET` | `/effective?workCenterId=&year=&month=` | 월 전체 effective |
| `PUT` | `/overrides` | upsert (기본과 동일+비고없음 → 자동 해제) |
| `DELETE` | `/overrides?workCenterId=&calendarDate=` | Override 해제 |

**effective 응답 필드:** `effectiveWorkTime`, `isOverride`, `baseWorkTime`, `content`

---

## 3. EffectiveMinutes

```
1) work_center_calendar Override
2) production_calendar 해당 일
3) work_center.operation_time (기본 480분)
```

---

## 4. 이벤트

| 이벤트 | Projector |
|--------|-----------|
| `StandardCalendarDayRegistered/Updated/Deleted` | — |
| `WorkCenterCalendarDayRegistered/Updated/Deleted` | — |

---

## 5. 프론트엔드

| 파일 | 역할 |
|------|------|
| `src/api/productionCalendar.ts` | 기본달력 API |
| `src/api/workCenterCalendar.ts` | WC effective·Override API |
| `src/components/MonthCalendarGrid.tsx` | 월간 그리드 공통 |
| `src/pages/ProductionCalendarPage.tsx` | **기본달력** 탭 |
| `src/pages/WorkCenterCalendarPage.tsx` | **WC달력** 탭 |

---

## 6. 수동 검증 체크리스트

- [ ] 기본달력 — 특정일 0분(휴무) 저장
- [ ] 기본달력 — 미등록 일 표시 480분(기본)
- [ ] WC달력 — Override 720분 · 주황 배지
- [ ] Override 해제 → 기본달력 상속
- [ ] Override = 기본 effective → 자동 삭제

---

*Part 11 · SmartManager Step 1*
