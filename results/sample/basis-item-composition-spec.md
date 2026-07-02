# 품목구성(BOM) 기준정보 확정 스펙

> **문서 버전:** 1.1  
> **작성일:** 2026-06-22  
> **상태:** 확정안 (구현 대기)  
> **관련 Wave:** B2(현행) → **B2-R** (품목구성 UI·API 정비)  
> **관련 문서:**  
> - [거래처 기준정보 확정 스펙](./basis-company-spec.md)  
> - [품목 기준정보 확정 스펙](./basis-item-spec.md)  
> - [기준정보 API·필드 매핑](./basis-information-api-spec.md) §5.3  
> - [기준정보 구현 설계](./basis-information-implementation-spec.md)

---

## 1. 문서 목적

기준정보 정비 3단계로 **품목구성(`item_composition`, BOM)** 의 업무 정의, 데이터 모델, 화면·API, 적용기간 처리 방침, 후속 Wave를 정리한다.

**운영 SoT:** `plan` variant. `actual`은 대조·compare 전용(트랜잭션 미참조).

---

## 2. 기준정보 공통 UI 원칙

[거래처 스펙](./basis-company-spec.md) §2와 동일.

| 항목 | 확정 |
|------|------|
| 레거시 하단 부가 그리드 | **구현하지 않음** — 품목구성정보테이블, 자료실, 제목, 참고보기, 일괄수정 등 |
| 목록 화면 | 상단 검색 + 그리드 + 우측 상단 **등록(Modal)** + 행별 **수정·삭제** |
| 시스템 필드 | `id`, 등록/수정, `recoding_state` — 화면 입력 없음 |
| 삭제 | `recoding_state = 0` 소프트 삭제 |

---

## 3. 업무 정의

품목구성은 **모품목 1단위당 필요한 자품목과 수량 비율**을 정의한다.

| 원칙 | 내용 |
|------|------|
| 관계 UK | `(parent_item_id, child_item_id)` — 동일 모·자 **활성** 1건 |
| PK | `id` (BIGINT) |
| 실소요량 | `자품수량 ÷ 모품수량` (= `need_quantity_numerator / need_quantity_denominator`) |
| 자기참조 | 모품목 ≠ 자품목 |
| 품목 표시 | 모·자 품목번호·품목명 등은 `item` **조인 표시** (BOM 테이블 미저장) |

---

## 4. 데이터 모델

### 4.1 `item_composition` — 화면 입력 필드

| # | 화면 라벨 | API 필드 | DB 컬럼 | Modal |
|---|-----------|----------|---------|:-----:|
| 1 | 모품목 | `parentItemNum` | `parent_item_id` (FK) | 등록 |
| 2 | 자품목 | `childItemNum` | `child_item_id` (FK) | 등록 |
| 3 | 모품수량 | `parentQuantity` | `need_quantity_denominator` | 등록·수정 |
| 4 | 자품수량 | `childQuantity` | `need_quantity_numerator` | 등록·수정 |

- **등록 Modal:** 위 4필드 입력 (2줄 레이아웃 — 1행: 모·자품목, 2행: 모·자품수량).
- **수정 Modal:** 모품목·자품목 **읽기 전용**, **모품수량·자품수량만** 수정.

### 4.2 적용시작일·적용종료일 (1차 — 화면 비노출)

| 컬럼 | 1차 정책 |
|------|----------|
| `begin_date` | **화면 없음** — 등록 시 서버가 **당일(`LocalDate.now()`)** 자동 설정 |
| `end_date` | **화면 없음** — **`NULL`** (무기한 유효) |

- BOM 전개·MRP·소요자재 API는 추후 `begin_date` / `end_date` 필터 적용 (2차).
- 기간 입력 UI·기간 수정·버전별 다중 행은 **후속 Wave**에서 검토.

### 4.3 1차에서 사용하지 않는 컬럼 (DB 유지 또는 기본값)

레거시 화면 필드이나 1차 Modal에 **포함하지 않음**. 저장 시 서버 기본값.

| 컬럼 | 1차 기본값 |
|------|------------|
| `process_management` | `0` |
| `sub_division` | `NULL` |
| `supply_division` | `NULL` |
| `bom_unit` | 자품목 `unit` 과 동일하게 두거나 `NULL` |

### 4.4 시스템 필드 (화면 입력 없음)

| 컬럼 | 설명 |
|------|------|
| `id` | PK |
| `created_by` / `created_at` | 등록자·등록일 |
| `updated_by` / `updated_at` | 수정자·수정일 |
| `recoding_state` | `1`=유효, `0`=삭제 |

### 4.5 `item_composition_history` (후속)

변경사유·이력보기는 1차 **미구현**. 수량 수정 시 이력 INSERT는 2차에서 `item_composition_history` 연동.

---

## 5. 화면 (`/basis/item-composition/plan`)

### 5.1 목록

```
┌────────────────────────────────────────────────────────────────┐
│ 품목구성 (plan)  [정전개][역전개][BOM복사]              [등록] │
│ 모품목 [____]  자품목 [____]                                   │
├────────────────────────────────────────────────────────────────┤
│ 모품목 │ 자품목 │ 모품수량 │ 자품수량 │ [수정][삭제]             │
└────────────────────────────────────────────────────────────────┘
```

| 요소 | 동작 |
|------|------|
| 좌측 상단 검색 | **모품목**, **자품목** 각각 입력 (품목번호 또는 품목명으로 조회·필터) |
| 필터 비움 | **전체** 품목구성 레코드 (`recoding_state = 1` 만) |
| 둘 다 입력 | **AND** 조건 |
| 그리드 | 모품목, 자품목, 모품수량, 자품수량 (+ join: 품목명·자산분류는 선택 컬럼) |
| 우측 상단 | **등록** → Modal |
| 행 우측 | **수정** / **삭제** (소프트 삭제) |
| 하단 부가 영역 | **없음** |

### 5.2 등록 Modal

| 줄 | 필드 |
|----|------|
| 1 | 모품목 (SearchableSelect), 자품목 (SearchableSelect) |
| 2 | 모품수량, 자품수량 |

**검증**

- 모품목 ≠ 자품목
- 모품수량 > 0, 자품수량 > 0
- 동일 (모품목, 자품목) 활성 건 중복 불가

### 5.3 수정 Modal

- 모품목·자품목: **표시만** (변경 불가 — 관계 변경 시 삭제 후 재등록)
- 모품수량·자품수량: **수정 가능**

### 5.4 보조 기능 (버튼) — BOM 정전개 · 역전개 · BOM 복사

툴바 배치: 검색 필드와 **등록** 버튼 사이.

```
[ 모품목 ___ ] [ 자품목 ___ ]    [정전개] [역전개] [BOM복사]    [등록]
```

**공통**

| 항목 | 내용 |
|------|------|
| variant | **`plan`만** (운영 SoT). BOM 복사는 plan 전용 |
| 활성 BOM | `recoding_state = 1` + 서버 `begin_date` / `end_date` 유효(1차: 등록일·NULL) |
| 기존 API | 백엔드 `ItemCompositionService` 구현됨 — 품목구성 화면에서 Modal로 연동 |

---

#### 5.4.1 BOM 정전개 (Forward / Explosion)

| 항목 | 내용 |
|------|------|
| **정의** | 선택한 **모품목을 루트**로 BOM을 **모든 하위 단계까지 재귀 전개**한 트리 |
| **누적 수량** | 루트 **1단위** 기준 소요량. 비율 = `자품수량 ÷ 모품수량`, 하위 = 상위 수량 × 비율 |
| **API** | `GET /api/v1/basis/item-composition/plan/{itemNum}/explosion` |
| **응답** | `BomTreeNode` — `itemNum`, `itemName`, `level`, `quantity`, `children[]` |
| **기준 품목** | 상단 **모품목** 필터 값 **필수**. 없으면 안내 메시지 |
| **UI** | Modal — 들여쓰기 트리 (레벨 L0,L1…, 품목번호·품목명, 누적수량) |
| **오류** | BOM **순환 참조** 감지 시 전개 중단 (`BusinessException`) |
| **용도** | BOM 구조 확인, MRP·소요자재 산출의 기반 (`MrpService` 동일 API 사용) |

**예시 (루트 1개 기준)**

```text
P-1000 (1)
  └─ A-200 (2)
       └─ R-300 (4)   → 루트 1개당 R-300 은 8개
```

---

#### 5.4.2 BOM 역전개 (Reverse)

| 항목 | 내용 |
|------|------|
| **정의** | 선택 품목이 **자품목으로 등록된 직계 모품목(1단계 상위)** 목록 |
| **범위** | **1레벨만** (다단계 “최종 제품까지” 역추적은 후속 Wave) |
| **API** | `GET /api/v1/basis/item-composition/plan/{itemNum}/reverse` |
| **응답** | `ItemComposition[]` — 모품목·자품목·모품수량·자품수량 등 BOM 라인 |
| **기준 품목** | **자품목** 필터 우선. 없으면 Modal에서 품목 선택 |
| **UI** | Modal — 플랫 그리드 (모품목, 자품목, 모품수량, 자품수량). 행 선택 시 해당 모품목으로 목록 필터 이동(선택) |
| **용도** | 원자재·공용부품 **사용처** 확인, 품목 변경·삭제 전 영향 범위 |

---

#### 5.4.3 BOM 복사 (Copy)

| 항목 | 내용 |
|------|------|
| **정의** | **원본 모품목의 직계 자품목 구성(1레벨)** 을 **대상 모품목**에 복제 |
| **범위** | **손자 이하 BOM은 복사하지 않음** — 원본의 직계 자식 행만 |
| **API** | `POST /api/v1/basis/item-composition/plan/copy` |
| **Body** | `{ "sourceItemNum": "...", "targetItemNum": "..." }` |
| **응답** | 복사된 **행 수** (int) |
| **규칙** | 원본 ≠ 대상. 대상에 **동일 자품목** 활성 행이 있으면 **스킵**(덮어쓰기 없음) |
| **UI** | Modal — 원본 모품목(기본: 현재 모품목 필터), 대상 모품목(SearchableSelect) → 확인 후 POST → “N건 복사” 토스트 |
| **기간** | B2-R에서 복사 행은 `begin_date`=오늘, `end_date`=null 로 맞춤 (현행은 원본 날짜 복사 — **보완**) |
| **용도** | 유사 제품 BOM 빠른 등록 |

---

#### 5.4.4 기타 (1차·후속)

| 기능 | API | 1차 |
|------|-----|:---:|
| 소요자재보기 | `GET .../plan/{itemNum}/materials` | 정전개 Modal로 대체 가능, 필요 시 4번째 버튼 |
| 이력보기 | `GET .../plan/{id}/history` | 후속 |
| 다단계 역전개 | — | 후속 |
| `/basis/bom/plan` (`BomPage`) | 별도 조회 화면 | 품목구성 화면 버튼과 **동일 API** 공유, 메뉴 통합 여부는 구현 시 결정 |

`actual` variant는 compare용만. **운영 입력·전개·복사는 plan만.**


---

## 6. API (TO-BE)

Base: `/api/v1/basis/item-composition/plan`

| Method | Endpoint | 설명 |
|--------|----------|------|
| GET | `/plan?parentItemNum=&childItemNum=` | 목록 (`recoding_state=1`, 필터 선택) |
| GET | `/plan/{parentItemNum}` | 특정 모품목 하위 BOM |
| POST | `/plan` | 등록 |
| PUT | `/plan/{id}` | 수량 수정 |
| DELETE | `/plan/{id}` | 소프트 삭제 |
| GET | `/plan/{itemNum}/explosion` | BOM 전개 |
| GET | `/plan/{itemNum}/reverse` | 역전개 |
| GET | `/plan/{itemNum}/materials` | 소요자재 (말단 품목 목록) |
| POST | `/plan/copy` | BOM 복사 (1레벨) |

**BOM 복사 body**

```json
{
  "sourceItemNum": "P-1000",
  "targetItemNum": "P-2000"
}
```

**POST body 예시**

```json
{
  "parentItemNum": "P-1000",
  "childItemNum": "R-2000",
  "parentQuantity": 1,
  "childQuantity": 4
}
```

서버 매핑: `parentQuantity` → `need_quantity_denominator`, `childQuantity` → `need_quantity_numerator`, `beginDate` = 오늘, `endDate` = null.

**PUT body 예시** (수량만)

```json
{
  "parentQuantity": 1,
  "childQuantity": 5
}
```

---

## 7. 현행 구현과의 차이 (갭)

| 항목 | 현행 | TO-BE |
|------|------|-------|
| 검색 | 모품목 Select 1개 | **모품목 + 자품목**, 빈 값이면 전체 |
| Modal 필드 | 분자·분모·공정관리·BOM단위·시작일 | **모·자품목 + 모·자품수량** 4필드 |
| 수정 | 전체 필드 | **수량만** |
| 적용시작·종료일 | UI 노출 | **비노출**, 서버 자동 |
| API 필드명 | `needQuantityNumerator` 등 | `parentQuantity` / `childQuantity` 별칭 또는 DTO 매핑 |
| 도면번호 | 없음 | 유지(제외) |

---

## 8. 구현·후속 Wave (할 일)

### 8.1 B2-R — 품목구성 UI·API

- [ ] `GET /plan` — `parentItemNum`, `childItemNum` 필터, 미입력 시 전체(활성만)
- [ ] POST/PUT DTO — `parentQuantity` / `childQuantity` 매핑
- [ ] 등록 시 `begin_date`=today, `end_date`=null 서버 설정
- [ ] 수정 API — 수량 필드만 허용
- [ ] 프론트 `ItemCompositionPage` — 검색 2칸, Modal 4필드, 수량만 수정
- [ ] 툴바 **정전개·역전개·BOM복사** Modal 연동 (§5.4)
- [ ] `copy` 시 `begin_date`=today, `end_date`=null 보완
- [ ] 통합테스트 갱신

### 8.2 후속 (기간·이력)

- [ ] 적용시작일·적용종료일 **화면 노출** 및 전개/MRP 기준일 필터
- [ ] `item_composition_history` + 이력보기 Modal
- [ ] 공정관리·하위구분·조달구분·BOM단위 필요 시 Modal 확장
- [ ] BOM **다단계** 역전개 API
- [ ] 본 문서를 [basis-information-api-spec.md](./basis-information-api-spec.md) §5.3과 동기화

---

## 9. 의사결정 요약

| 항목 | 확정 |
|------|------|
| 운영 variant | `plan` |
| 테이블 입력 | 모·자품목 FK + 모품수량·자품수량 |
| UI | 거래처·품목과 동일 패턴, 검색=모품목+자품목 |
| 필터 없음 | 전체 활성 BOM |
| 수정 | 수량만 |
| 삭제 | 소프트 삭제 |
| 적용시작·종료일 | **1차 화면 비노출**, 서버 자동(오늘 / NULL) |
| BOM 버튼 | 정전개(트리)·역전개(1레벨)·BOM복사(1레벨) — §5.4 |
| 하단 부가 영역 | 미구현 |

---

## 10. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-06-22 | 품목구성 확정안 — 4필드 Modal, 검색·수량수정, 기간 비노출 |
| 1.1 | 2026-06-22 | §5.4 BOM 정전개·역전개·복사 정의·UI·API 상세 |
