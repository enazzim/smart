# Step 1 Part 03 — 품목 11필드 CRUD

> **완료일:** 2026-07-02  
> **범위:** 품목 CRUD REST API · `ItemRegistered/Updated/Deleted` 이벤트 · React 품목 화면  
> **설계 SSOT:** [`docs/step0/d4-item.md`](../step0/d4-item.md) v0.2  
> **다음 파트:** Part 04 — 공정 + `WipBalanceProjector`

---

## 1. 요약

| 항목 | 결과 |
|------|------|
| Flyway | `V004__item.sql` — `item` 테이블 |
| API | `POST/GET/PUT/DELETE /api/v1/basis/items`, `GET /by-no/{itemNo}` |
| 부수효과 | **없음** — `inventory_balance` 미생성 (Lazy) |
| 이벤트 | `ItemRegistered` / `ItemUpdated` / `ItemDeleted` |
| Gradle·프론트 빌드 | 성공 |
| E2E | 등록·수정·품목번호 조회·삭제 확인 |

---

## 2. API 명세

### 2.1 품목 등록

```
POST /api/v1/basis/items
```

```json
{
  "itemNo": "P-1001",
  "itemName": "샘플 제품",
  "propertyClassification": "제품",
  "unit": "EA",
  "standard": "100x50",
  "standardUnitCost": 5000,
  "checkDistinction": "NONE",
  "leadTime": 7,
  "safetyStockQuantity": 10,
  "orderIntervalQuantity": 0,
  "minOrderQuantity": 1
}
```

### 2.2 목록·조회

| 메서드 | 경로 | 설명 |
|--------|------|------|
| `GET` | `/api/v1/basis/items?itemNo=&itemName=` | 활성 품목 목록 (부분 검색) |
| `GET` | `/api/v1/basis/items/{id}` | ID 조회 |
| `GET` | `/api/v1/basis/items/by-no/{itemNo}` | 품목번호 조회 |

### 2.3 수정·삭제

| 메서드 | 경로 | 규칙 |
|--------|------|------|
| `PUT` | `/api/v1/basis/items/{id}` | `item_no` 변경 불가 |
| `DELETE` | `/api/v1/basis/items/{id}` | `recording_state=0` 논리삭제 |

### 2.4 자산분류 (4종)

`원자재` · `제품` · `상품` · `공정품`

### 2.5 검사구분

`NONE` · `INSPECTION` (nullable)

---

## 3. 주요 파일

### domain

| 파일 | 설명 |
|------|------|
| `domain/item/PropertyClassification.java` | 자산분류 4종 enum |
| `domain/item/CheckDistinction.java` | 검사구분 |
| `domain/event/EventTypes.java` | `ITEM_*` 이벤트 상수 |

### application · infrastructure · api

| 레이어 | 패키지 |
|--------|--------|
| application | `application/item/ItemService`, `ItemCommand`, `ItemUpdateCommand` |
| infrastructure | `persistence/item/ItemJpaEntity`, `JpaItemRepository` |
| api | `web/item/ItemController`, DTO 3종 |

### 프론트

| 파일 | 설명 |
|------|------|
| `src/App.tsx` | 거래처 / 품목 탭 네비 |
| `src/pages/ItemPage.tsx` | 품목 11필드 CRUD |
| `src/api/item.ts` | API 클라이언트 |

---

## 4. 검증 (2026-07-02)

| 시나리오 | 결과 |
|----------|------|
| POST P-1001 | `item` 1행, `ItemRegistered` |
| PUT 자산분류 변경 | `ItemUpdated`, `item_no` 유지 |
| GET by-no/P-1001 | 조회 성공 |
| DELETE | 목록 0건, `ItemDeleted` |
| `inventory_balance` | 테이블 없음 (Lazy 정책) |

---

## 5. 실행

```powershell
cd smartmanager_backend
.\gradlew.bat :smartmanager-api:bootRun

cd smartmanager_frontend
npm run dev
```

브라우저 `http://localhost:5173` → **품목** 탭
