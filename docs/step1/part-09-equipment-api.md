# Step 1 Part 09 — 설비 6필드 CRUD

> **완료일:** 2026-07-04  
> **범위:** 설비 CRUD REST API · `EquipmentRegistered/Updated/Deleted` · React 설비 화면 · 작업표준 설비 FK 연동  
> **설계 SSOT:** [`docs/step0/d4-equipment.md`](../step0/d4-equipment.md) v0.1  
> **다음 파트:** 생산달력

---

## 1. 요약

| 항목 | 결과 |
|------|------|
| Flyway | `V009__equipment.sql` — `equipment` + 설비분류 시드 + `work_standard.equipment_id` FK |
| API | `POST/GET/PUT/DELETE /api/v1/basis/equipment` |
| UI 입력 | 6필드: 설비번호·명·분류·작업장(null)·설계샷·초기샷 |
| 표시 | `workShot`, `accumulatedShot`, `replacementDue` (읽기 전용) |
| 부수효과 | **없음** |
| 연동 | 작업표준 `equipmentId` 검증 · 작업장 삭제 시 설비 참조 거부 |
| Gradle·프론트 빌드 | ✅ `gradlew build -x test` · `npm run build` |

---

## 2. API 명세

Base: `/api/v1/basis/equipment`

### 2.1 등록

```json
{
  "equipmentNum": "EQ-001",
  "equipmentName": "1500톤 사출기",
  "equipmentCategoryId": 42,
  "workCenterId": 3,
  "designShot": 500000,
  "initialShot": 12000
}
```

### 2.2 목록·조회

| 메서드 | 경로 | 설명 |
|--------|------|------|
| `GET` | `/api/v1/basis/equipment?q=` | 설비번호·설비명 부분 검색 |
| `GET` | `/api/v1/basis/equipment/{id}` | ID 조회 |

### 2.3 수정·삭제

| 메서드 | 경로 | 규칙 |
|--------|------|------|
| `PUT` | `/api/v1/basis/equipment/{id}` | 설비번호 읽기 전용, `accumulated_shot` 재계산 |
| `DELETE` | `/api/v1/basis/equipment/{id}` | 작업표준 참조 시 거부 |

### 2.4 샷 연산

- 등록: `work_shot=0`, `accumulated_shot=initial_shot`
- 수정: `accumulated_shot = work_shot + initial_shot`
- `replacementDue`: `design_shot > 0` ∧ `accumulated_shot >= design_shot`

### 2.5 보조 API

| 메서드 | 경로 | 설명 |
|--------|------|------|
| `GET` | `/api/v1/basis/code-groups/EQUIPMENT_CLASS/options` | 설비분류 콤보 |

---

## 3. 이벤트

| 이벤트 | Projector |
|--------|-----------|
| `EquipmentRegistered/Updated/Deleted` | — (부수효과 없음) |

---

## 4. 프론트엔드

| 파일 | 역할 |
|------|------|
| `src/api/equipment.ts` | CRUD + 설비분류 콤보 |
| `src/pages/EquipmentPage.tsx` | 등록·수정·검색·교체배지 |
| `src/pages/WorkStandardPage.tsx` | 사용설비 콤보 연동 |
| `src/App.tsx` | **설비** 탭 |

---

## 5. 수동 검증 체크리스트

- [ ] 설비 등록 — 6필드, 초기샷 0
- [ ] UK — 설비번호 중복 거부
- [ ] 수정 — 초기샷 변경 → 누계샷 재계산
- [ ] 교체 필요 — `accumulated >= design`
- [ ] 작업표준 — 설비 콤보 선택·저장
- [ ] 삭제 — 작업표준 참조 거부

---

*Part 09 · SmartManager Step 1*
