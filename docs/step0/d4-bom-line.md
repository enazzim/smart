# D4 — 품목구성 / BOM (`ItemOrganizationInfo` → `IOI_MT`)

> Step 0 산출물 · **확정 v0.1** (4필드 Modal · plan SoT · sample 정합)  
> SSOT (레거시 감사): `KIT_ERP/BasisInformation/MasterInfoRecordRUD.cs` `m_FieldName[2]` (19필드)  
> 화면: `ItemOrganizationInfo.aspx` / `ItemOrganizationInfo.aspx.cs`  
> SmartManager: `item_composition` (plan) + `bom_change_log` (구 `UPIOI_HT`)  
> 내용키(UK): `(parent_item_id, child_item_id)` 활성 1건  
> PK: `id` ← `ItemOrganizationInfoIndex`

**관련:** [`step0-plan-D1-D4.md`](./step0-plan-D1-D4.md) · [`domain-event-projector-matrix.md`](./domain-event-projector-matrix.md) §2·§5 · [`d4-item.md`](./d4-item.md) · [시스템 컬럼 규칙](../../.cursor/rules/master-audit-fields.mdc) · TO-BE [`results/sample/basis-item-composition-spec.md`](../../results/sample/basis-item-composition-spec.md) · [`basis-information-implementation-spec.md`](../../results/sample/basis-information-implementation-spec.md)

### v0.1 확정 요약

| 항목 | 레거시 | SmartManager v0.1 |
|------|--------|-------------------|
| 운영 SoT | `IOI_MT` (plan) | **`item_composition`** variant=`plan` |
| UI 입력 | 10+ 필드·트리·하단 그리드 | **Modal 4필드** (모·자품목 + 모·자품수량) |
| 수정 | 전 필드 | **수량만** (모·자품목 읽기 전용) |
| 적용기간 | 화면 입력 | **1차 비노출** — `begin_date`=등록일, `end_date`=NULL |
| 공정관리·조달·BOM단위 | 화면 입력 | **서버 기본값** (§2.2) |
| 등록 부수효과 | `UPIOI_HT` 이력 | `BomHistoryProjector` → `bom_change_log` |
| 창고 | 없음 | **없음** (재고 Lazy 정책 유지) |
| 진품목구성 | `RIOI_MT` + `UPRIOI_HT` | `real_item_composition` — compare·actual 전용 (Step 1 이후) |

---

## 1. 분류 범례

| 분류 | 의미 |
|------|------|
| **Keep (UI)** | Modal·API 사용자 입력 |
| **시스템(자동)** | 서버만 설정 (기간·감사·PK) |
| **서버기본** | DB 컬럼 유지, 1차 UI 없음 — 저장 시 기본값 |
| **Drop (UI)** | 1차 화면·Request DTO 미포함 |
| **Phase2** | 이력 UI·기간 UI·다단계 역전개 |

---

## 2. SmartManager 스키마 — `item_composition` (plan)

### 2.1 UI 입력 4필드

| # | UI 라벨 | React field | DB 컬럼 | API (권장) | 필수 | 비고 |
|---|---------|-------------|---------|------------|------|------|
| 1 | 모품목 | parentItemId | parent_item_id | `parentItemNum` → resolve | Y | FK → `item.id` |
| 2 | 자품목 | childItemId | child_item_id | `childItemNum` → resolve | Y | FK → `item.id` |
| 3 | 모품수량 | parentQuantity | need_quantity_denominator | `parentQuantity` | Y | > 0 |
| 4 | 자품수량 | childQuantity | need_quantity_numerator | `childQuantity` | Y | > 0 |

**실소요량:** `childQuantity ÷ parentQuantity` (= 분자 ÷ 분모)

| 시스템 | 컬럼 | 1차 정책 |
|--------|------|----------|
| 적용시작 | begin_date | 등록 시 **당일** (`LocalDate.now()`) |
| 적용종료 | end_date | **NULL** (무기한) |
| PK·감사·소프트삭제 | id, created_*, updated_*, recording_state | [공통 규칙](../../.cursor/rules/master-audit-fields.mdc) |

### 2.2 서버 기본값 (1차 UI 없음)

| DB 컬럼 (레거시) | v0.1 기본값 | Phase2 |
|-----------------|-------------|--------|
| process_management | `0` | Modal 확장 검토 |
| sub_division | NULL | |
| supply_division | NULL | `BOM_SUPPLY_DIVISION` |
| bom_unit | 자품목 `unit` 복사 또는 NULL | `UNIT_BOM` |

### 2.3 자산분류 제약 (d4-item 4종 연계)

| 역할 | 허용 자산분류 | 비고 |
|------|--------------|------|
| **모품목** | `제품`, `상품`, `공정품` | 레거시 제품·반제품 → **공정품**에 매핑 |
| **자품목** | `원자재`, `공정품` (+ 모품목이 제품·상품일 때 동급 조립 허용 검토) | 1차: **원자재·공정품** 우선 |

**공통 검증 (레거시 `ItemOrganizationInfoValidateField` 동일 의도)**

- 모품목 ≠ 자품목
- 모·자품수량 NOT NULL, > 0
- 활성 UK `(parent_item_id, child_item_id)` 중복 불가
- **순환 참조** 금지 — 등록·수정 시 DFS/재귀 검증 (`BusinessException`)

---

## 3. 이벤트 · Projector

| 이벤트 | Projector | Read Model | 비고 |
|--------|-----------|------------|------|
| `BomLineRegistered` | `BomHistoryProjector` | `bom_change_log` | 레거시 등록 시 `UpdateHistory` → `UPIOI_HT` |
| `BomLineUpdated` | `BomHistoryProjector` | 동일 | 수량 변경 이력 |
| `BomLineDeleted` | `BomHistoryProjector` | 동일 | 소프트 삭제 이력 |

- **창고·재고 Projector 없음** ([domain-event §2](./domain-event-projector-matrix.md) 행 3)
- 1차: 변경사유 UI 없음 — payload에 `reason=INITIAL` / `QUANTITY_CHANGE` / `DELETE` 코드

### 3.1 Real variant (`RIOI_MT` / `UPRIOI_HT`)

| variant | 테이블 | 용도 |
|---------|--------|------|
| `plan` | `item_composition` | **운영 SoT** — MRP·생산·구매가 참조 |
| `actual` | `real_item_composition` | 현장 확인·**compare** 전용 |

| 이벤트 | Projector |
|--------|-----------|
| `RealBomLineRegistered` / `Updated` / `Deleted` | `RealBomHistoryProjector` → `real_bom_change_log` |

> actual은 트랜잭션 API에서 **거부** ([implementation-spec](../../results/sample/basis-information-implementation-spec.md)).

---

## 4. API · 화면 (v0.1)

Base: `/api/v1/basis/item-composition/plan`

| Method | Endpoint | 설명 |
|--------|----------|------|
| GET | `/plan?parentItemNum=&childItemNum=` | 목록 (빈 필터 = 전체 활성) |
| GET | `/plan/{parentItemNum}` | 모품목 하위 BOM |
| POST | `/plan` | 등록 (4필드) |
| PUT | `/plan/{id}` | **수량만** 수정 |
| DELETE | `/plan/{id}` | 소프트 삭제 |
| GET | `/plan/{itemNum}/explosion` | BOM 정전개 (트리) |
| GET | `/plan/{itemNum}/reverse` | 역전개 (**1레벨**) |
| POST | `/plan/copy` | BOM 복사 (직계 1레벨) |

**POST 예시**

```json
{
  "parentItemNum": "P-1000",
  "childItemNum": "R-2000",
  "parentQuantity": 1,
  "childQuantity": 4
}
```

**PUT 예시 (수량만)**

```json
{
  "parentQuantity": 1,
  "childQuantity": 5
}
```

### 4.1 UI (`/basis/item-composition/plan`)

- 패턴: 거래처·품목과 동일 — **검색 + 그리드 + Modal 등록/수정/삭제**
- 검색: **모품목**, **자품목** 각각 (품번·품명), AND 조건
- 툴바: **[정전개] [역전개] [BOM복사]** — [`basis-item-composition-spec` §5.4](../../results/sample/basis-item-composition-spec.md)
- **하단 부가 그리드 없음** (품목구성정보테이블·자료실·일괄수정 등 Drop)

---

## 5. 레거시 19필드 대조표

| # | 레거시 컬럼 | v0.1 | 비고 |
|---|------------|------|------|
| 1 | ParentItemNum | **Keep (UI)** | → `parent_item_id` |
| 2 | ChildItemNum | **Keep (UI)** | → `child_item_id` |
| 3 | NeedQuantityNumerator | **Keep (UI)** | 자품수량 |
| 4 | NeedQuantityDenominator | **Keep (UI)** | 모품수량 |
| 5 | ProcessManagement | 서버기본 | `0` |
| 6 | SubDivision | 서버기본 | NULL |
| 7 | SupplyDivision | 서버기본 | NULL |
| 8 | BOMUnit | 서버기본 | 자품 `unit` |
| 9 | BeginDate | 시스템(자동) | 등록일 |
| 10 | EndDate | 시스템(자동) | NULL |
| 11 | UpdateReason | Phase2 | 이력 Modal |
| 12~18 | RecodingState, 감사 | 시스템(자동) | |
| 19 | ItemOrganizationInfoIndex | `id` PK | |

---

## 6. `results/` 문서와의 관계

| 문서 | 채택 |
|------|------|
| `sample/basis-item-composition-spec.md` v1.1 | **SSOT** — 4필드·검색·정전개·역전개·복사 |
| `260624_기준정보_BOM.md` | 순환참조·분수 소요량·트리 UI 아이디어 — **UI는 Modal+그리드로 단순화** |
| `sample/basis-information-api-spec.md` §5.3 | variant·endpoint 명명 |
| `260624_기준정보_종합화면설계.md` §3.4 | 트리-그리드 → v0.1은 **플랫 그리드 + Modal** (sample 우선) |

---

## 7. Cut-over · Phase2

| 항목 | 처리 |
|------|------|
| `IOI_MT` | `item_composition` + FK `item_id` |
| `UPIOI_HT` | `bom_change_log` (이력 마이그레이션 선택) |
| `RIOI_MT` | `real_item_composition` — compare용 |
| 기간·이력 UI | Phase2 |
| `item_composition_history` 별도 테이블 | Phase2 (1차는 `bom_change_log`만) |

---

## 8. 체크리스트

- [x] v0.1 4필드 Modal·plan SoT 확정
- [x] domain-event `BomHistoryProjector` 정합
- [x] 재고 부수효과 없음
- [ ] Step 1 Flyway + plan CRUD + explosion/reverse/copy
- [ ] actual/compare (P2)
- [ ] 공용코드 문서 §4.3과 `BOM_SUPPLY_DIVISION` 연동 검토

---

*v0.1: 2026-07-02 · TO-BE: `results/sample/basis-item-composition-spec` · 레거시: `MasterInfoRecordRUD.cs` m_FieldName[2]*