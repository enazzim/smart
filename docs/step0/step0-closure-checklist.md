# Step 0 마무리 체크리스트

> **목적:** D1~D4 설계 산출물 완료 여부·잔여 검증·Step 1 착수 게이트를 한곳에서 추적한다.  
> **기준일:** 2026-07-02  
> **상태:** **설계 초안 완료** — 현업·운영 DB 검증 후 Step 0 **종료 서명** 가능

**본문 계획서:** [`step0-plan-D1-D4.md`](./step0-plan-D1-D4.md)

---

## 0. 한눈에 보기

| 구분 | 완료 | 잔여 | 비고 |
|------|:----:|:----:|------|
| **D1** 범위선언서 | ✅ | — | [`step0-plan` §1](./step0-plan-D1-D4.md) |
| **D2** 화면↔레거시 | ✅ v1 | 3건 | CT_T·PDF 매핑은 Phase 1 전 |
| **D3** 제외·보류 | ✅ 초안 | 현업 리뷰 | §3 |
| **D4** 필드 매트릭스 | ✅ 12건 | 현업 리뷰 | 선행+메뉴 1~11 |
| **선행 설계** | ✅ 2건 | PUC export | domain-event · code_group |
| **Step 1 착수** | — | 게이트 §6 | P0 Projector 4종 |

```
Step 0 설계 문서 ████████████████████ 100% (초안)
운영 DB 실측     ██████████░░░░░░░░░░  50% (interim CSV · DB 교체 대기)
현업 설계 리뷰   ░░░░░░░░░░░░░░░░░░░░   0% (미착수)
```

> **2026-07-02:** [`data/puc-large-distinct.csv`](./data/puc-large-distinct.csv), [`data/ct-t-basis-menu.csv`](./data/ct-t-basis-menu.csv) interim 반영.  
> 거래처분류 `0110~0130`은 **가설** — `$env:SMARTMANAGER_ERP_DSN` + [`export-step0-data.ps1`](./data/export-step0-data.ps1)로 최종 교체.

---

## 1. 산출물 인벤토리 (완료)

### 1.1 D1~D3 · 공통 설계

| # | 문서 | 버전 | 완료 |
|---|------|------|:----:|
| 1 | [`step0-plan-D1-D4.md`](./step0-plan-D1-D4.md) | — | ✅ |
| 2 | [`domain-event-projector-matrix.md`](./domain-event-projector-matrix.md) | — | ✅ |
| 3 | [`공용코드-콤보박스-매핑-초안-code-group-field-binding.md`](./공용코드-콤보박스-매핑-초안-code-group-field-binding.md) | 초안 | ✅ |

### 1.2 D4 — 메뉴별 (12파일)

| 순서 | 메뉴 | 파일 | 버전 | 레거시 필드 → TO-BE |
|------|------|------|------|---------------------|
| 선행 | 공용코드 | [`d4-public-code.md`](./d4-public-code.md) | v0.1 | 12 → 헤더+소분류+`usage_type` |
| 1 | 거래처 | [`d4-company.md`](./d4-company.md) | **v0.3** | 36 → 슬림+role+`partner_ledger_*` |
| 2 | 품목 | [`d4-item.md`](./d4-item.md) | v0.2 | 59 → **11필드** |
| 3 | 품목구성 | [`d4-bom-line.md`](./d4-bom-line.md) | v0.1 | 19 → **4+서버기본** |
| 4 | 작업장 | [`d4-work-center.md`](./d4-work-center.md) | v0.2 | 17 → **3+서버기본** |
| 5 | 공정 | [`d4-process.md`](./d4-process.md) | v0.1 | 17 → **7필드 Modal** |
| 6 | 설비 | [`d4-equipment.md`](./d4-equipment.md) | v0.1 | 42 → **6+샷2** |
| 7 | 작업표준 | [`d4-work-standard.md`](./d4-work-standard.md) | v0.1 | 28 → **9+FK** |
| 8 | 단가 3종 | [`d4-unit-price.md`](./d4-unit-price.md) | v0.1 | 19×3 → **`unit_price` 통합** |
| 9~10 | 생산달력 | [`d4-calendar.md`](./d4-calendar.md) | v0.1 | 8 → **2계층 Override** |
| 11 | 사용자 | [`d4-user.md`](./d4-user.md) | v0.1 | 45 → **7+RBAC** |
| — | Real 3종 | `d4-real-*.md` | — | **Phase 1 (P3)** — 미작성 |

---

## 2. Tier 분류 — 무엇을 언제 닫을 것인가

### Tier A — Step 0 설계 완료 (✅ 닫힘)

코딩 착수 **전제 문서**는 모두 초안 이상 존재.

- [x] D1 In/Out Scope 문장
- [x] D2 기준정보 14+3 화면 1:1 (`m_FieldName` index 대응)
- [x] D3 제외·보류 초안
- [x] D4 선행+1~11 Keep/Drop/시스템 분류
- [x] Domain Event · Projector 매트릭스 (등록·수정·삭제·FK §10)
- [x] 공용코드 `code_group` · 필드 바인딩 초안
- [x] 핵심 TO-BE 결정 6종 (§4)

### Tier B — Step 0 **종료 서명** 전 권장 (현업·데이터)

| # | 항목 | 담당 | 산출 | 상태 |
|---|------|------|------|:----:|
| B1 | **현업 D4 리뷰** — Drop 필드·슬림화·역할 필수 등 | 기준정보 담당 | 리뷰 메모 또는 D4 v0.x bump | [ ] |
| B2 | **`PUC_MT` DISTINCT export** | DBA/개발 | `large_code`↔`large_name` CSV | [x] interim |
| B3 | **`CT_T` 기준정보 메뉴 export** | DBA/개발 | Num·Title·EngTitle | [x] draft |
| B4 | `code_group` §3 **`TBD` 확정** | 설계+현업 | 공용코드 문서 §3 v0.2 | [x] |
| B5 | **`WORK_DIARY_GROUP`** `large_code` 확정 | 설계 | **`1900`** + seed CSV | [x] |
| B6 | 거래처분류1~3 `large_code` 확정 | 설계+현업 | **`0110`/`0120`/`0130` 가설** | [~] DB 검증 대기 |

**검증 SQL (복사용):**

```sql
-- B2
SELECT DISTINCT LargeClassificationCode, LargeClassificationName
FROM PUC_MT WHERE RecodingState = 1
ORDER BY LargeClassificationCode;

-- B3
SELECT Num, Title, EngTitle, [Group]
FROM CT_T WHERE [Group] = N'기준정보'
ORDER BY Num;
```

### Tier C — Step 1과 병행 가능 (설계 보완)

| # | 항목 | 시점 |
|---|------|------|
| C1 | D2 PDF 91화면 ↔ `.aspx` 매핑표 | Phase 1 착수 전 |
| C2 | Real 3종 `d4-real-*.md` | Phase 1 P3 |
| C3 | `field_code_binding` DB 시드 | Step 1 공통 컴포넌트 시 |
| C4 | `results/sample/*-spec.md` ↔ Step 0 diff 정리 | Step 1 API 스펙 동기화 시 |

### Tier D — Step 0 범위 밖 (의도적 제외)

- Spring Boot / React **구현**
- Flyway 마이그레이션 **실행**
- 레거시 데이터 **Cut-over 본 실행**
- 영업·생산·구매 모듈 D2/D4

---

## 3. 메뉴별 D4 · Step 1 잔여 (참고)

각 D4 문서 하단 체크리스트 요약. **Step 0 범위는 설계 [x] 항목까지.**

| 메뉴 | Step 0 설계 | Step 1 구현 (D4 [ ] 항목) |
|------|:-----------:|---------------------------|
| 공용코드 | ✅ v0.1 | Flyway·`CodeSelect`·`ProcessReferenceChecker` |
| 거래처 | ✅ v0.3 | CRUD·`PartnerLedgerProjector` |
| 품목 | ✅ v0.2 | CRUD·6종→4종 이관 |
| 품목구성 | ✅ v0.1 | CRUD·explosion/copy·`BomHistoryProjector` |
| 작업장 | ✅ v0.2 | CRUD |
| 공정 | ✅ v0.1 | CRUD·copy·`WipBalanceProjector` E2E |
| 설비 | ✅ v0.1 | CRUD |
| 작업표준 | ✅ v0.1 | CRUD·copy |
| 단가 | ✅ v0.1 | CRUD·history·`OutsourceInputBalanceProjector` |
| 생산달력 | ✅ v0.1 | CRUD·`EffectiveMinutes` |
| 사용자 | ✅ v0.1 | CRUD·RBAC·JWT |

---

## 4. 확정 설계 원칙 (Step 0 동결)

Step 1에서 **재논의 없이** 따를 항목. 변경 시 D4·domain-event **버전 bump** 필수.

| # | 원칙 | 근거 |
|---|------|------|
| 1 | FK = **부모 PK만** (`*_id`). `small_code`·`process_code` 문자열 FK **금지** | domain-event §10 |
| 2 | PUC 콤보 → `code_group` + `CodeSelect valueField="id"` | 공용코드 §8.1 |
| 3 | 영업창고 **1개** (`SALES`) | domain-event §3.1 |
| 4 | 품목 등록 시 재고 **Lazy** (`inventory_balance` 미생성) | d4-item · §3 |
| 5 | 거래처 원장 **`partner_ledger_account` + monthly** (`BSI_MT` 폐기) | d4-company v0.3 |
| 6 | 생산달력 **2계층** + `EffectiveMinutes` (PCI fan-out 폐지) | d4-calendar |
| 7 | 사용자 **RBAC** (`user_role`), PUC 권한 폐기 | d4-user |
| 8 | 단가 3탭 → **`unit_price` 통합** | d4-unit-price |
| 9 | 시스템 컬럼 **Request 제외**·프로그램 자동 | master-audit-fields.mdc |
| 10 | 공용코드 **헤더+소분류** 행 · `usage_type` Tier | d4-public-code |

---

## 5. 현업 리뷰 포인트 (B1 체크리스트)

> **현업 배포용 상세본:** [`b1-business-d4-review-checklist.md`](./b1-business-d4-review-checklist.md) (우선순위·질문·서명란)

리뷰 시 메뉴별 **확인·서명**용. 이슈는 D4 해당 섹션에 메모 후 버전 올림.

| 메뉴 | 확인 질문 |
|------|-----------|
| 거래처 | Drop 9필드·분류1~3 PUC·역할 최소 1개·원장 3종(SALES/PURCHASE/OUTSOURCE→PURCHASE) |
| 품목 | 11필드·자산분류 4종·분류1~4 Drop·단위 자유입력 |
| 품목구성 | 4필드 Modal·조달구분 PUC 유지 |
| 작업장 | 3필드·대표공정 FK·달력 복사 폐지 |
| 공정 | 제품·공정품만·7필드·WIP Lazy |
| 설비 | 6필드·위치·단위 Drop |
| 작업표준 | 9필드·공정 FK 경유 |
| 단가 | 3탭 통합·외주만 공정 FK |
| 생산달력 | 기본+WC Override·분 단위 |
| 사용자 | 7필드·부서·주민번호 Drop·RBAC |
| 공용코드 | 소분류만 운영 등록·예약코드·PROCESS 삭제 검사 |

---

## 6. Step 0 종료 · Step 1 착수 게이트

### 6.1 Step 0 **종료** 조건 (권장)

아래 **모두** 충족 시 Step 0 공식 종료.

- [x] §1 산출물 15문서 존재 (Real 3종 제외)
- [ ] Tier **B1** 현업 D4 리뷰 완료 (이메일·회의록·체크 서명)
- [x] Tier **B2+B3** interim CSV [`data/`](./data/) 보관 — **DB export로 최종 교체 권장**
- [x] Tier **B4+B5** 확정 · **B6** 가설(`0110~0130`) — DB 검증 후 서명

> **실무 타협:** B2~B6 미완이어도 **거래처·품목·공용코드 PROCESS**만 시드 확정되면 Step 1 **제한 착수** 가능 (범위: P0 E2E). 종료 서명은 B 전체 후.

### 6.2 Step 1 **착수** 조건 (필수)

| # | 조건 |
|---|------|
| G1 | Tier A 전부 ✅ |
| G2 | [`domain-event` §8 P0](./domain-event-projector-matrix.md) 4 Projector 범위 합의 |
| G3 | 저장소·브랜치·Flyway 네이밍 규칙 합의 |
| G4 | `public_code` + `PROCESS_CODE` 시드 스펙 (더미 `14000000`/`14009999` 제외 규칙) |

### 6.3 Step 1 첫 스프린트 (권장 순서)

| 순서 | 작업 | 근거 |
|------|------|------|
| 1 | 멀티모듈 보일러플레이트 + `domain_event` 테이블 | domain-event §7 |
| 2 | `public_code` · `code_group` Flyway 시드 | d4-public-code |
| 3 | **거래처** CRUD E2E + `PartnerLedgerProjector` | domain-event §8 P0 |
| 4 | 품목 11필드 CRUD (부수효과 없음) | d4-item |
| 5 | 공정 plan + `WipBalanceProjector` | d4-process |
| 6 | 외주단가 + `OutsourceInputBalanceProjector` | d4-unit-price |

---

## 7. 서명 · 이력

| 역할 | 이름 | Step 0 설계 승인 | 일자 |
|------|------|:----------------:|------|
| 기준정보 현업 | | [ ] | |
| 개발 리드 | | [ ] | |
| PM / 설계 | | [ ] | |

| 일자 | 변경 |
|------|------|
| 2026-07-02 | Tier B2~B6 interim — CSV·code_group §3 v0.2·WORK_DIARY_GROUP 1900 |

---

*갱신 시 [`step0-plan-D1-D4.md`](./step0-plan-D1-D4.md) §0·§6 상태와 동기화한다.*