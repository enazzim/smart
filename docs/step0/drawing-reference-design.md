# SmartManager — 도면 참조(drawing_reference) 설계서

> **문서 버전:** 1.3  
> **작성일:** 2026-07-12  
> **상태:** TO-BE — **DR-1~4 + lifecycle Phase 1~3 반영** · DR-5·후속 §10 확장  
> **접근:** **(A) PDF 기준 경량 참조** — Native CAD Vault(B)는 범위 외  
> **관련:**  
> - Flyway 적용분: `V072__drawing.sql` · `V074__drawing_item_link.sql` · `V092__drawing_lifecycle.sql`  
> - 업무 절차·lifecycle: [`drawing-lifecycle-design.md`](./drawing-lifecycle-design.md)  
> - 품목구성 BOM: [`d4-bom-line.md`](./d4-bom-line.md) · `ItemCompositionService.explode`  
> - 감사 컬럼: [master-audit-fields](../../.cursor/rules/master-audit-fields.mdc)  
> - 스키마 초안: [`schema-drafts/V076__drawing_reference.sql`](./schema-drafts/V076__drawing_reference.sql)

---

## 1. 문서 목적

본 문서는 도면관리에 **도면(문서) 이력 간 참조 관계**를 두어,  
조립 성격의 상위 도면이 하위 부품 도면의 **특정 리비전**을 가리키는 구성을  
개정·조회·감사가 가능하도록 정의한다.

| 구분 | 내용 |
|------|------|
| **대상** | 백엔드(Spring Boot 3.5), DB(Flyway), 프론트엔드(React) |
| **범위** | `drawing_reference` 스키마, 개정 시 스냅샷 복사, Contains/Where-used API·UI, DEV/PROD pin 정책 |
| **범위 외** | Native CAD 파일 Vault, CAD 애드인 자동 링크 추출, Copy Tree, Floating(항상 최신) 참조 |

### 1.1 왜 (A)인가

일반 EDMS는 파일·경로만 관리해 Assembly↔Part 링크가 깨지기 쉽다.  
정식 PDM(B)은 CAD 네이티브와 체크인이 필요하다.

SmartManager는 현재 **PDF + `drawing_history` 버전**만 있으므로,  
먼저 **PDF 세계 안의 버전 pin 그래프**를 도입한다.  
이후 Native Vault를 붙일 때 본 테이블을 승인·열람 레이어로 재사용할 수 있다.

---

## 2. AS-IS / Gap

### 2.1 구현된 것

| 항목 | 내용 |
|------|------|
| `drawing_master` / `drawing_history` | part_no UK, DEV/PROD, major/minor, PDF 경로 |
| `drawing_master.item_id` | 품목 1건 선택 연동 (V074, nullable) — **link-item API 전용** (V092 가드) |
| `drawing_master.lifecycle_stage` | 업무 단계 (V092) — [`drawing-lifecycle-design.md`](./drawing-lifecycle-design.md) |
| 개정·승격·휴지통·WebSocket | `DrawingService` |
| 전체 백업 | DB + 도면 PDF |

### 2.2 없는 것

| 항목 | 상태 |
|------|------|
| 도면 history 간 참조 테이블 | ❌ |
| Contains / Where-used | ❌ |
| 개정 시 구성 스냅샷 복사 | ❌ |
| BOM 정전개 ↔ 하위 도면 일괄 조회 | ❌ (별도 후속, §10) |

---

## 3. 핵심 원칙

1. **참조의 양끝은 `drawing_history.id`** — 마스터만 가리키면 “어느 Rev인지”가 사라져 EDMS와 동일 문제가 난다.  
2. **부모 개정 시** 직전 latest(동일 `drawing_type`)의 참조를 **새 history로 복사**한 뒤 수정한다. 과거 history 구성은 **불변**.  
3. **v1은 pin만** — Floating(“항상 자식 최신”) 미지원. “최신으로 맞추기”는 **부모 개정 + 명시적 pin 갱신**만 허용.  
4. **PROD 부모 → 자식도 PROD history** (강제). DEV 부모는 DEV/PROD 자식 허용(경고 가능).  
5. **품목 BOM과 강제 동기화하지 않음** — 문서 구성(SSOT for 출도) ≠ 생산 BOM.  
6. 시스템 감사 컬럼은 [master-audit-fields](../../.cursor/rules/master-audit-fields.mdc) 준수. 삭제 = `recording_state=0`.

```text
drawing_history (부모 Asm PDF · Rev 2.0)
    │ drawing_reference (pin)
    ├─► drawing_history (Part-A · Rev 1.1)
    └─► drawing_history (Part-B · Rev 3.0)
```

---

## 4. 데이터 모델

### 4.1 `drawing_reference`

| 컬럼 | 설명 |
|------|------|
| `id` | UUID PK |
| `parent_history_id` | 상위(조립) 도면의 **특정** history |
| `child_history_id` | 하위(부품) 도면의 **특정** history |
| `ref_role` | `COMPONENT` (기본) \| `RELATED` \| `SPEC` |
| `sort_order` | 표시 순서 |
| `remark` | 선택 |
| `recording_state` | 1 활성 / 0 소프트 삭제 |
| 감사 컬럼 | created_* / updated_* |

**UK:** `(parent_history_id, child_history_id, recording_state)`  
**인덱스:** `child_history_id` (Where-used)

FK → `drawing_history(id)`.

### 4.2 애플리케이션 정합

```text
∀ active reference R:
  R.parent / R.child history 유효 (마스터 recording_state=1, 이력 존재)
  parent.drawing_master_id ≠ child.drawing_master_id   -- 동일 마스터 자기참조 금지
  순환 경로 없음 (DFS)
  parent.drawing_type = PROD ⇒ child.drawing_type = PROD
```

SQL 초안: [`schema-drafts/V076__drawing_reference.sql`](./schema-drafts/V076__drawing_reference.sql)  
**실제 Flyway 번호:** 적용 시점의 다음 빈 번호(현재 저장소 기준 **V076+**. V070–V075는 권한·마감·도면에 사용됨).

---

## 5. 개정·승격 동작

### 5.1 참조 등록·수정

- 대상: **해당 drawing_type의 latest history만** 수정 가능.  
- API: 참조 목록 **전체 교체(PUT)**.  
- 자식은 **이미 존재하는 history**만 선택.  
- **구현 메모:** latest 구성 교체 시 해당 `parent_history_id`의 기존 참조 행은 UK(`recording_state`) 충돌을 피하기 위해 **물리 삭제 후 재삽입**한다.  
  과거 history의 참조 스냅샷은 손대지 않는다.

### 5.2 부모 `revise`

1. 기존과 같이 새 `drawing_history` 생성.  
2. 직전 latest(동일 `drawing_type`)의 active `drawing_reference`를  
   `parent_history_id = 새 history` 로 **행 복사**.  
3. 요청에 새 구성이 있으면 복사본을 그 구성으로 교체(또는 revise 직후 PUT).  
4. 구 history의 참조 행은 유지(스냅샷).

### 5.3 자식만 개정

- Where-used로 영향 부모(latest) 목록 제시.  
- v1: **부모 자동 개정 없음**.  
- UI: “부모 개정 시 이 자식 Rev로 pin 갱신” 안내만.

### 5.4 양산 승격(promote)

부모를 PROD로 올릴 때: 새(또는 대상) PROD history의 모든 자식 pin이 **PROD**인지 검증.  
실패 시 승격 거부. 성공 시 `lifecycle_stage` → `MASS_PROD_READY` ([`drawing-lifecycle-design.md`](./drawing-lifecycle-design.md)).

### 5.5 개발 재개(reopen-dev)

PROD latest에서 양산 변경이 필요할 때 `revise` 대신 **`reopen-dev`** 사용.

1. PROD latest history를 `is_latest=N` 처리  
2. DEV history 신규 생성 (major = max(PROD major)+1, minor=0)  
3. 직전 PROD의 `drawing_reference`를 새 DEV history로 **스냅샷 복사**  
4. `lifecycle_stage` → `SAMPLE`

이후 DEV 개정·재승격 시 PROD major가 증가한다 (예: PROD 1.0 → reopen → DEV 2.0 → promote → PROD 2.0).

---

## 6. API

Base: `/api/v1/basis/drawings`

| Method | Endpoint | 권한 | 설명 |
|--------|----------|------|------|
| `GET` | `/{masterId}/history/{historyId}/references` | DrawingRead | Contains |
| `GET` | `/history/{historyId}/where-used` | DrawingRead | Where-used |
| `PUT` | `/{masterId}/history/{historyId}/references` | DrawingWrite | latest만 전체 교체 |
| (기존) | `POST .../revise` | DrawingWrite | 내부에서 참조 복사 |

### 6.1 PUT body

```json
{
  "children": [
    { "childHistoryId": "…", "refRole": "COMPONENT", "sortOrder": 1, "remark": null },
    { "childHistoryId": "…", "refRole": "COMPONENT", "sortOrder": 2 }
  ]
}
```

과거 history에 PUT → `400`.  
순환·PROD 규칙 위반 → `400` + 메시지.

### 6.2 Contains 응답 (요약)

```typescript
type DrawingReferenceView = {
  id: string;
  parentHistoryId: string;
  childHistoryId: string;
  refRole: string;
  sortOrder: number;
  remark?: string | null;
  child: {
    masterId: string;
    partNo: string;
    partName: string;
    drawingType: 'DEV' | 'PROD';
    majorVersion: number;
    minorVersion: number;
    itemId?: number | null;
    itemNo?: string | null;
  };
};
```

---

## 7. UI

| 위치 | 내용 |
|------|------|
| 도면 상세 / 뷰어 | 탭 **「구성(참조)」** — 현재 history 기준 자식 목록, PDF 열람 |
| 동일 | 탭 **「역참조」** — 이 history를 부모로 쓰는 목록 |
| 개정 흐름 | 이전 구성 미리보기 → pin 변경 → 개정 사유와 저장 |
| 목록(선택) | `참조 N건` 배지 |

권한: 기존 도면 Read/Write. 입력 컴포넌트에 감사 컬럼 노출 금지.

---

## 8. 애플리케이션 컴포넌트 (예상)

| 컴포넌트 | 역할 |
|----------|------|
| `DrawingReferenceService` | 검증·PUT·복사·순환 검사 |
| `DrawingRepository` 확장 | reference CRUD, by parent/child history |
| `DrawingService.revise` | 이력 생성 후 reference snapshot copy 호출 |
| `DrawingController` | §6 엔드포인트 |

기존 PDF 저장·워터마크·WebSocket은 변경 없음.

---

## 9. 구현 Wave

| Wave | 내용 | 완료 기준 |
|------|------|-----------|
| **DR-1** | Flyway + Contains/Where-used API + 테스트 | 부모·자식 2건 pin 조회 |
| **DR-2** | revise 시 참조 복사 + PUT(latest만) | 개정 후 구 Rev 구성 불변 |
| **DR-3** | UI 구성·역참조 탭 | 화면에서 PDF 열람 |
| **DR-4** | PROD pin 강제, 순환 검사, 리포트 | PROD+DEV 자식 승격 거부 |
| **DR-5** (후속) | BOM 폭발 도면 조회 + (선택) reference diff | §10 — F-3 |

---

## 10. 후속 — BOM 정전개 도면 조회 (DR-5 / F-3)

> **상태:** 미구현 · **별도 Wave 착수용**  
> **연계:** [`drawing-lifecycle-design.md` §10.3](./drawing-lifecycle-design.md)  
> **선행:** 품목 `item_id`↔도면 `link-item` (V092), `ItemCompositionService.explode` (기존)

문서 참조(A)·`drawing_reference`와 **별개**로, 생산 BOM 기준 **현장 열람** UX.

```text
모품목 item_no
  → ItemCompositionService.explode(itemId, asOfDate?)
  → 각 구성 item_id
  → drawing_master (item_id, recording_state=1) + latest drawing_history
  → PDF URL / MISSING 표시
```

### 10.1 `drawing_reference` vs BOM → 도면

| | `drawing_reference` (본문 A) | BOM → 도면 (DR-5) |
|--|------------------------------|-------------------|
| 기준 | **도면 history pin** | **품목 BOM** |
| 버전 | 출도 스냅샷 (Rev 고정) | 보통 **latest** (또는 `MISSING`) |
| 용도 | 출도·감사·구성 재현 | 현장 “하위 PDF 모아보기” |
| 스키마 | `drawing_reference` | **신규 테이블 불필요** (집계 API) |
| SSOT | 출도 정합 | 보조 — 불일치 시 reference 우선 |

### 10.2 API (제안)

Base: `/api/v1/basis/drawings`

| Method | Endpoint | 권한 | 설명 |
|--------|----------|------|------|
| `GET` | `/bom-explosion/{itemNo}` | DrawingRead | BOM 레벨별 하위 품목 + 연결 도면(latest) |
| `GET` | `/bom-explosion/{itemNo}/compare` | DrawingRead | (선택 F-3-b) BOM item vs `drawing_reference` pin diff |

**쿼리**

| 파라미터 | 설명 |
|----------|------|
| `drawingType` | `DEV` \| `PROD` (기본 `PROD`) |
| `asOfDate` | BOM 유효일 (기존 explode 정책 따름) |
| `maxLevel` | 전개 깊이 상한 (기본 무제한 또는 10) |

**응답 (요약)**

```typescript
type BomDrawingExplosionNode = {
  bomLevel: number;
  itemId: number;
  itemNo: string;
  itemName: string;
  quantity: number;
  drawing: {
    masterId?: string | null;
    partNo?: string | null;
    historyId?: string | null;
    drawingType?: 'DEV' | 'PROD';
    majorVersion?: number;
    minorVersion?: number;
    status: 'LINKED' | 'MISSING' | 'MULTIPLE'; // item당 도면 0/1/N
  };
  children?: BomDrawingExplosionNode[];
};
```

- `MULTIPLE`: 동일 `item_id`에 활성 도면 복수 — UI에서 목록 선택 (V074 UK 없음 정책).
- 기존 `GET /{masterId}/reference-candidates`는 **단일 모 도면** BOM 하위만; 본 API는 **품목 번호 진입**.

### 10.3 UI (제안)

| 위치 | 내용 |
|------|------|
| 기준정보 > **품목** 상세/목록 | 「BOM 하위 도면」버튼 → 트리 + PDF 열람 |
| 도면 뷰어 | (선택) 연결 품목 있을 때 동일 모달 링크 |
| diff (F-3-b) | reference 탭과 나란히 “BOM-only / Reference-only” 배지 |

### 10.4 구현 Wave · 완료 기준

| 단계 | 내용 | 완료 기준 |
|------|------|-----------|
| **DR-5a** | `bom-explosion` API + 품목 화면 모달 | 모품목 1건, 3레벨 이하 전개·PDF 1클릭 |
| **DR-5b** | compare API + UI diff | reference와 BOM 불일치 1건 시각화 |
| **DR-5c** | 오프라인 캐시 연동 (선택) | explode 결과 partNo 일괄 `syncDailyDrawings` |

**재사용 코드**  
`DrawingReferenceService.listBomCandidates` · `ItemCompositionService.explode` · `drawingPdfUrl`.

---

## 11. (B) Native CAD와의 경계

| (A) 본 설계 | (B) 향후 |
|-------------|----------|
| PDF history pin | CAD 파일 Vault + 체크인 추출 |
| 수동·UI로 구성 편집 | 애드인이 `cad_usage` 자동 생성 |
| 열람·승인 | 설계 툴에서 링크 재바인딩 |

(A)의 `drawing_reference`는 (B) 도입 시 **뷰어/승인용 투영** 또는 **수동 보정 레이어**로 유지 가능하다.

---

## 12. 체크리스트 (구현 시)

- [x] Flyway `drawing_reference` (V076)
- [x] Contains / Where-used / PUT
- [x] `revise` 스냅샷 복사
- [x] PROD→PROD pin, 순환·자기참조 검증 · promote 전 PROD 자식 검사
- [x] UI 구성·역참조 탭
- [x] lifecycle·reopen-dev·link-item ([`drawing-lifecycle-design.md`](./drawing-lifecycle-design.md))
- [ ] 통합 테스트: 개정 전후 구 history 참조 불변
- [ ] **DR-5** BOM 폭발 연동 — §10 (F-3, 별도 착수)
- [ ] **F-1** `drawing_work_log` — [`drawing-lifecycle-design.md` §10.1](./drawing-lifecycle-design.md)
- [ ] **F-2** 세분 권한 Flyway — [`drawing-lifecycle-design.md` §10.2](./drawing-lifecycle-design.md)

---

## 13. 변경 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-07-12 | (A) PDF 경량 참조 초안 확정 · BOM 조회는 §10 후속 |
| 1.1 | 2026-07-12 | DR-1~3 구현 착수: V076 Flyway, API, revise/promote 스냅샷, 뷰어 구성·역참조 UI |
| 1.2 | 2026-07-16 | V092 lifecycle, reopen-dev·link-item, promote stage 연계 §5.4~5.5 |
| 1.3 | 2026-07-16 | §10 DR-5/F-3 착수용 상세 — API·응답·UI·Wave, 후속 체크리스트 |

---

*구현 착수 시 본 문서를 SSOT로 한다. 구현 반영 후 상태는 DR Wave 진행에 따라 갱신.*
