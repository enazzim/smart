# SmartManager Step 0 — D1~D4 계획서

> **목적:** 코딩 착수 전 범위·매핑·필드 분류를 확정한다.  
> **원칙:** 운영 메뉴·레거시 코드 > 인터페이스 구현서 PDF(화면명 참고, 필드 스펙 없음)  
> **작성일:** 2026-07-02 · **상태:** 설계 초안 완료 — [`step0-closure-checklist.md`](./step0-closure-checklist.md) Tier B 검증 대기

---

## 0. Step 0 한눈에 보기

| 산출물 | 명칭 | 완료 기준 | 상태 |
|--------|------|-----------|------|
| **D1** | 범위선언서 | In/Out Scope 문장 확정 | ✅ 본 문서 §1 |
| **D2** | 화면 ↔ 레거시 매핑표 | 기준정보 14+3 화면 1:1 연결 | ✅ 본 문서 §2 (v1) |
| **D3** | 제외·보류 목록 | 메뉴 없음·PDF 미포함 화면 결정 | ✅ 본 문서 §3 (초안) |
| **D4** | UI필드 ↔ DB 매트릭스 | 메뉴별 Keep/숨김/Drop/검토 분류 | ✅ **선행+1~11** v0.1~v0.3 (D4 전부 초안 완료) |

**선행 산출물 (D4 보조·설계)**

| 문서 | 내용 | 상태 |
|------|------|------|
| [`domain-event-projector-matrix.md`](./domain-event-projector-matrix.md) | Domain Event · Projector · FK · 영업창고 1개 | ✅ |
| [`공용코드-콤보박스-매핑-초안-code-group-field-binding.md`](./공용코드-콤보박스-매핑-초안-code-group-field-binding.md) | code_group · field_binding (D4 콤보 부분) | ✅ 초안 |
| [`step0-closure-checklist.md`](./step0-closure-checklist.md) | **마무리** — Tier·게이트·현업 리뷰·서명 | ✅ v1 |

---

## 1. D1 — 범위선언서

### 1.1 프로젝트 정의

**SmartManager**는 레거시 `KIT_ERP`(ASP.NET WebForms, SQL Server SP)를 **Spring Boot 3.5 + React(Vite/TS) + MariaDB**로 전환하는 신규 시스템이다.

### 1.2 기준 문서 우선순위

| 순위 | 기준 | 용도 |
|------|------|------|
| **1** | 운영 DB `CT_T` 메뉴 + `KIT_ERP` 실제 화면·코드 | **In Scope 확정** |
| **2** | `MasterInfoRecordRUD.cs` / `Register.cs` 등 핵심 로직 | 부수효과·도메인 규칙 |
| **3** | 인터페이스 구현서 PDF (91페이지, 91화면) | **참고** — 화면명·도메인 구분, 필드 스펙 없음 |

### 1.3 In Scope (확정)

#### Phase 0 — 기준정보 (현재 Step 0 집중)

| 구분 | 범위 |
|------|------|
| 선행 | **공용코드** (`PublicUseCode.aspx` → `PUC_MT`) |
| 운영 메뉴 1~11 | 거래처 → 품목 → 품목구성 → 작업장 → 공정 → 설비 → 작업표준 → 단가 3종 → 기본생산달력 → WC생산달력 → 사용자 |
| Real 3종 | 진품목구성 · 진공정 · 진작업표준 (설계 3종과 **동일 UI**, 테이블·데이터만 분리) |
| 공용 팝업 | 거래처·품목·BOM·공정 등 기준정보 팝업 (`BasisInformation/Popup/`) |
| 설계 방향 | Domain Event + Projector, FK=PK만, 영업창고 1개 |

#### Phase 1 이후 (Step 0 범위 밖, D2 참고용)

인터페이스 구현서 기준 **91화면 / 6도메인** — 영업·생산·구매외주·지표·커뮤니티·공용컨트롤. 상세는 §3.3.

### 1.4 Out of Scope (Step 0)

- 레거시 ASPX **300개+** 전체 일괄 전환
- PDF에 없고 운영 메뉴·업무 흐름에서도 쓰이지 않는 화면 (§4)
- Spring/React **코드 구현** (Step 1 이후)
- 운영 DB 마이그레이션 스크립트 본 실행 (설계·검증 SQL만 Step 0)

### 1.5 기술 스택 (확정)

| 층 | 스택 |
|----|------|
| Backend | Spring Framework 6, Spring Boot 3.5, Java 최신 LTS |
| Frontend | React 19+, Vite, TypeScript |
| DB | MariaDB (레거시 SQL Server SP는 Projector·뷰로 단계적 호환 검토) |

---

## 2. D2 — 화면 ↔ 레거시 매핑표

### 2.1 확신도 정의

| 기호 | 의미 |
|------|------|
| ◎ | 운영 메뉴 + 코드·테이블 일치 확인 |
| ○ | 레거시 파일 확인, 메뉴명만 CT_T 미검증 |
| △ | PDF·레거시 폴더 불일치, 현업 확인 필요 |
| — | Out of Scope |

### 2.2 Phase 0 — 기준정보 공식 메뉴 순서 (In Scope)

> 조사·마이그레이션·D4 작성 순서 = 아래 표 순서.

| 순서 | 운영 메뉴명 | Legacy Page | DB 테이블 | 필드 수 | `PageName` / `m_FieldName` | 확신도 |
|------|------------|-------------|-----------|---------|---------------------------|--------|
| 선행 | 공용코드 | `SystemInfoManagement/PublicUseCode.aspx` | `PUC_MT` | 12 | index 11 | ◎ |
| 1 | 거래처 | `BasisInformation/CompanyInfo.aspx` | `CI_MT` | 36 | 0 | ◎ |
| 2 | 품목 | `BasisInformation/ItemInfo.aspx` | `II_MT` | 59 | 1 | ◎ |
| 3 | 품목구성 | `BasisInformation/ItemOrganizationInfo.aspx` | `IOI_MT` | 19 | 2 | ◎ |
| 4 | 작업장 | `BasisInformation/WCInfo.aspx` | `WCI_MT` | 17 | 3 | ◎ |
| 5 | 공정 | `BasisInformation/ProcessSequenceInfo.aspx` | `PSI_MT` | 17 | 4 | ◎ |
| 6 | 설비 | `BasisInformation/EquipmentInfo.aspx` | `EI_MT` | 42 | 5 | ◎ |
| 7 | 작업표준 | `BasisInformation/WorkStandardInfo.aspx` | `WSI_MT` | 28 | 6 | ◎ |
| 8a | 판매단가 | `BasisInformation/SaleUnitCodeInfo.aspx` | `UCI_MT` | 19 | 7 | ◎ |
| 8b | 구매단가 | `BasisInformation/BuyingUnitCodeInfo.aspx` | `UCI_MT` | 19 | 8 | ◎ |
| 8c | 외주단가 | `BasisInformation/OutSideOrderUnitCodeInfo.aspx` | `UCI_MT` | 19 | 9 | ◎ |
| 9 | 기본생산달력 | `BasisInformation/StandardProductionCalendarInfo.aspx` | `PCI_MT` | 8 | 12 | ◎ |
| 10 | WC생산달력 | `BasisInformation/WCProductionCalendarInfo.aspx` | `PCI_MT` | 8 | 13 | ◎ |
| 11 | 사용자 | `BasisInformation/UserInfo.aspx` | `UI_MT` | 45 | 10 | ◎ |

**Real 3종** (설계 3종과 UI 동일, `recordType=REAL`)

| 운영 메뉴명 | Legacy Page | 테이블 | 필드 수 | index | 확신도 |
|------------|-------------|--------|---------|-------|--------|
| 진품목구성 | `RealItemOrganizationInfo.aspx` | `RIOI_MT` | 19 | 17 | ◎ |
| 진공정 | `RealProcessSequenceInfo.aspx` | `RPSI_MT` | 17 | 18 | ◎ |
| 진작업표준 | `RealWorkStandardInfo.aspx` | `RWSI_MT` | 28 | 19 | ◎ |

**필드 수 근거:** `KIT_ERP/BasisInformation/MasterInfoRecordRUD.cs` → `m_FieldName[n]` 배열 길이.

**부수효과·이벤트 매핑:** [`domain-event-projector-matrix.md`](./domain-event-projector-matrix.md)

### 2.3 설계 vs Real 대응

| 설계 | Real | 비고 |
|------|------|------|
| 품목구성 `IOI_MT` | 진품목구성 `RIOI_MT` | Real만 `UPRIOI_HT` 이력 |
| 공정 `PSI_MT` | 진공정 `RPSI_MT` | Real 등록 시 창고 부수효과 없음 |
| 작업표준 `WSI_MT` | 진작업표준 `RWSI_MT` | 동일 |

### 2.4 인터페이스 구현서 91화면 (Phase 1+ 참고)

PDF는 **화면 캡처 + 화면명** 중심. D2 v2(전체 91행)는 Phase 1 착수 전 작성.

| PDF 도메인 | 화면 수 | 레거시 폴더 (대략) |
|-----------|---------|-------------------|
| 영업관리 | 20 | `BusinessManagement`, `BuyingOutside` 일부, LOT |
| 공용컨트롤 | 2 | `BasisInformation/Popup`, `Common` |
| 기준정보 | 18 | `BasisInformation`, `SystemInfoManagement` 일부 |
| 생산관리 | 32 | `ProductionManagement` |
| 지표분석 | 8 | `QualityInspection` |
| 커뮤니티 | 11 | `Community` |

> PDF vs 레거시 `Menu.aspx.cs`: 구매·발주가 PDF에서는 영업관리에 포함, 시스템·권한은 기준정보에 포함 등 **메뉴 트리 불일치** 있음 → Phase 0은 **운영 CT_T** 우선.

### 2.5 D2 미완 · 현업 확인 TODO

- [x] 운영 DB `CT_T` export — **draft** [`data/ct-t-basis-menu.csv`](./data/ct-t-basis-menu.csv) (DB 최종 대기)
- [ ] Real 3종 메뉴명·순서 CT_T 대조
- [ ] PDF 91화면 ↔ `.aspx` 1:1 매핑표 (Excel/시트, 확신도 △ 항목)

---

## 3. D3 — 제외·보류 목록

### 3.1 결정 원칙

| 결정 | 조건 |
|------|------|
| **제외** | 운영 메뉴 없음 + 타 화면에서 미호출 + PDF 미포함 |
| **보류** | 코드는 있으나 사용 빈도·필요성 불명 |
| **팝업·유틸** | 메인 메뉴는 아니나 In Scope 화면에서 **필수 호출** → Phase 0 포함 |
| **Phase 2** | 경영계획·마감 등 타 모듈 기준정보 |

### 3.2 `BasisInformation/` — 메인 화면 제외·보류

| Legacy 파일 | 테이블 | 결정 | 사유 |
|-------------|--------|------|------|
| `BusinessPlanInfo.aspx` | `BPI_MT` | **Phase 2** | 경영정보(사업계획), 기준정보 메뉴 1~11 외 |
| `ExecutionPlanInfo.aspx` | `EPI_MT` | **Phase 2** | 경영정보(실행계획) |
| `CopyBOM.aspx` 등 Copy* | — | **유틸** | BOM/공정/작업표준 복사 — Phase 1에서 API로 대체 검토 |
| `UpdateItemOrganizationInfoHistory.aspx` | — | **이력 UI** | 마스터 화면 내 탭/팝업으로 흡수 |
| `UpdateRealItemOrganizationInfoHistory.aspx` | — | **이력 UI** | 동일 |
| `UpdateUnitCostHistory.aspx` | — | **이력 UI** | 단가 화면에서 처리 |
| `IDCheck.aspx` | — | **유틸** | 사용자 ID 중복 — API 검증으로 대체 |

### 3.3 레거시 모듈 — Step 0 제외 (Phase 1+)

| 모듈 | 폴더 | 화면 규모 | 결정 |
|------|------|----------|------|
| 영업관리 | `BusinessManagement/` | 다수 | Phase 1+ (PDF 영업 20) |
| 생산관리 | `ProductionManagement/` | 다수 | Phase 1+ |
| 구매·외주 | `BuyingOutside/` | 다수 | Phase 1+ |
| 경영정보 | `ManagementInfomation/` | 다수 | Phase 2 |
| 현황·통계 | `EtcPresentCondition/` | 다수 | Phase 2 (리포트) |
| 품질·지표 | `QualityInspection/` | 다수 | Phase 2 |
| 커뮤니티 | `Community/` | 11 | PDF 포함, Phase 2 |
| 시스템관리 | `SystemInfoManagement/` | 일부 | 백업·권한 등 — 메뉴별 판단 |

### 3.4 SmartManager에서 의도적으로 바꾸는 항목 (제외가 아님)

| 레거시 | SmartManager | 근거 문서 |
|--------|--------------|-----------|
| `BS_MT` 영업1·2·3 | `SALES` 1개 | domain-event §3.1 |
| 공정 등록 시 무조건 `PS_MT` | WorkDistinction별 완료 슬롯만 | domain-event §4 |
| 외주 `OS_MT` 생성 시점 | 외주단가 등록 (투입 슬롯) | domain-event §4.5 |
| 내용키 FK | PK FK만 | domain-event §10 |

---

## 4. D4 — UI필드 ↔ DB 매트릭스

### 4.1 산출물 정의

화면별로 아래 열을 갖는 표 (메뉴당 1파일 또는 통합 시트).

| 열 | 설명 |
|----|------|
| UI 라벨 | 화면 표시명 |
| React field | camelCase |
| DB 컬럼 | 레거시 `*_MT` 컬럼명 |
| 타입 | string / number / boolean / code / date |
| code_group_key | PUC 연동 시 ([공용코드 문서](./공용코드-콤보박스-매핑-초안-code-group-field-binding.md)) |
| 분류 | **Keep** / **UI숨김** / **Drop후보** / **검토중** |
| 비고 | 필수·읽기전용·이벤트 payload 등 |

**분류 기준**

| 분류 | 의미 |
|------|------|
| Keep | SmartManager UI·API·DB에 유지 |
| UI숨김 | DB·API는 유지, 화면에서 숨김 (등록자·일시 등) |
| Drop후보 | 미사용·중복·레거시 부채 — 현업 확인 후 삭제 |
| 검토중 | 용도 불명·규칙 확인 필요 |

### 4.2 메뉴별 D4 작성 순서·파일 (계획)

| 순서 | 메뉴 | 필드 수 | 산출 파일 (예정) | 상태 |
|------|------|---------|------------------|------|
| 선행 | 공용코드 | 12→**헤더+소분류** | [`d4-public-code.md`](./d4-public-code.md) | ✅ **v0.1** |
| 1 | 거래처 | 36→슬림+role+ledger | [`d4-company.md`](./d4-company.md) | ✅ **v0.3** |
| 2 | 품목 | 59→**11** | [`d4-item.md`](./d4-item.md) | ✅ **v0.2** |
| 3 | 품목구성 | 19→**4+서버기본** | [`d4-bom-line.md`](./d4-bom-line.md) | ✅ **v0.1** |
| 4 | 작업장 | 17→**3+서버기본** | [`d4-work-center.md`](./d4-work-center.md) | ✅ **v0.2** |
| 5 | 공정 | 17 | [`d4-process.md`](./d4-process.md) | ✅ **v0.1** |
| 6 | 설비 | 42→**6+샷2** | [`d4-equipment.md`](./d4-equipment.md) | ✅ **v0.1** |
| 7 | 작업표준 | 28→**9+FK** | [`d4-work-standard.md`](./d4-work-standard.md) | ✅ **v0.1** |
| 8a~c | 단가 3종 | 19→**통합** | [`d4-unit-price.md`](./d4-unit-price.md) | ✅ **v0.1** |
| 9~10 | 생산달력 | 8→**2계층** | [`d4-calendar.md`](./d4-calendar.md) | ✅ **v0.1** |
| 11 | 사용자 | 45→**7+RBAC** | [`d4-user.md`](./d4-user.md) | ✅ **v0.1** |
| R1~R3 | Real 3종 | 설계와 동일 | `d4-real-*.md` 또는 recordType 병기 | Phase 1 (P3) |

**필드 목록 SSOT:** `MasterInfoRecordRUD.cs` `m_FieldName[n]` + 각 `*.aspx` 실제 바인딩.

**이미 작성된 D4 부분**

- 콤보박스·코드 그룹만: [공용코드 문서 §4](./공용코드-콤보박스-매핑-초안-code-group-field-binding.md)
- API·FK 규칙: [domain-event §10.4](./domain-event-projector-matrix.md)

### 4.3 D4 공통 규칙 (확정)

- 자식 테이블 FK: `*_id` (부모 PK만)
- 내용키: `item_no`, `business_reg_no` 등 마스터 UNIQUE — **자식 FK 금지**
- API: 화면은 내용키 입력 → 서비스에서 PK 해석
- 소프트 삭제: `recording_state` / `deleted_at` — 기준정보 마스터·이력 FK 유지
- **시스템 컬럼 자동화**: PK·`recording_state`·등록일·등록자·수정일·수정자 — 프로그램만 설정, 사용자·Request DTO 미포함 ([`master-audit-fields.mdc`](../../.cursor/rules/master-audit-fields.mdc))

### 4.4 D4 완료 전 필수 데이터

```sql
-- PUC 대분류 실측 (공용코드 문서 §9)
SELECT DISTINCT LargeClassificationCode, LargeClassificationName
FROM PUC_MT WHERE RecodingState = 1 ORDER BY LargeClassificationCode;

-- 운영 메뉴 실측
SELECT Num, Title, EngTitle, [Group]
FROM CT_T WHERE [Group] = N'기준정보' ORDER BY Num;
```

---

## 5. 권장 일정

| 주차 | 작업 | 산출 |
|------|------|------|
| 1일차 | D1 확정 | 본 문서 §1 |
| 2~3일차 | D2 기준정보 + CT_T 검증 | 본 문서 §2, CT_T export |
| 4일차 | D3 현업 리뷰 | §3 갱신 |
| 5일차 | D4 거래처(36) + 품목(59) 1차 | `d4-company.md`, `d4-item.md` |
| 2주차 | D4 나머지 메뉴, △ 항목 확정 | D4 파일 세트 v1.0 |
| Step 0 종료 | 설계 리뷰 | **Step 1 착수 게이트** (§6) |

---

## 6. Step 0 완료 기준 · Step 1 전환 게이트

> **상세 체크리스트:** [`step0-closure-checklist.md`](./step0-closure-checklist.md) — Tier A(완료)·B(종료 서명)·C(병행)·D(제외)·현업 리뷰·서명란.

### 6.1 Step 0 Done 요약

| Tier | 내용 | 상태 |
|------|------|:----:|
| **A** | D1~D4 + domain-event + code_group 설계 초안 | ✅ |
| **B** | PUC/CT_T interim CSV · code_group §3 v0.2 · WORK_DIARY 1900 | [~] DB 최종 대기 |
| **C** | PDF 91매핑 · Real D4 · sample diff | Step 1 병행 |
| **D** | 코드 구현 · Cut-over 실행 | Step 0 밖 |

### 6.2 레거시 체크리스트 (Tier A 상세)

- [x] D1 범위선언서
- [x] D2 기준정보 14+3 화면 매핑 (v1)
- [x] D3 제외·보류 초안
- [x] Domain Event · Projector 매트릭스
- [x] 공용코드 code_group 초안
- [x] D4 메뉴 선행+1~11 필드 분류 초안
- [x] 자산분류 4종·재고 Lazy (domain-event §3, d4-item v0.2)
- [ ] Tier B — [`step0-closure-checklist.md` §2](./step0-closure-checklist.md) B1 현업 리뷰 · B2/B3 DB 최종 export

### 6.3 Step 1 (코딩) 착수 시 첫 작업

[`domain-event-projector-matrix.md` §8](./domain-event-projector-matrix.md) Phase 1:

| 우선순위 | 구현 |
|---------|------|
| P0 | `CompanyRegistered` → `PartnerLedgerProjector.ensureAccounts` |
| P0 | `ItemRegistered` → **부수효과 없음** (11필드 마스터) |
| P0 | `ProcessRegistered` → `WipBalanceProjector.ensure` |
| P0 | `OutsourceUnitPriceRegistered` → `OutsourceInputBalanceProjector` |

**Step 1 보일러플레이트:** Spring Boot 멀티모듈, React 앱, `domain_event` 테이블, 기준정보 #1 거래처 CRUD E2E.

---

## 7. 관련 문서·소스 인덱스

| 경로 | 용도 |
|------|------|
| `docs/step0/step0-closure-checklist.md` | Step 0 마무리 Tier·게이트·서명 |
| `docs/step0/data/` | Tier B2/B3 CSV · export 스크립트 |
| `docs/step0/step0-plan-D1-D4.md` | **본 문서** — D1~D4 계획·상태 |
| docs/step0/d4-public-code.md | D4 공용코드 v0.1 (code_group·usage_type) |
| docs/step0/d4-company.md | D4 거래처 v0.3 (partner_ledger_*) |
| docs/step0/d4-item.md | D4 품목 11필드 v0.2 |
| docs/step0/d4-bom-line.md | D4 품목구성 BOM v0.1 |
| docs/step0/d4-work-center.md | D4 작업장 v0.2 (`main_process_code_id` FK) |
| docs/step0/d4-process.md | D4 공정순서 v0.1 |
| docs/step0/d4-equipment.md | D4 설비 v0.1 |
| docs/step0/d4-work-standard.md | D4 작업표준 v0.1 |
| docs/step0/d4-unit-price.md | D4 단가 3종 v0.1 (unit_price) |
| docs/step0/d4-calendar.md | D4 생산달력 v0.1 (기본+Override) |
| docs/step0/d4-user.md | D4 사용자 v0.1 (RBAC·7필드) |
| docs/step0/domain-event-projector-matrix.md | 이벤트·프로젝터·FK·재고 |
| `docs/step0/공용코드-콤보박스-매핑-초안-code-group-field-binding.md` | D4 콤보 부분 |
| `KIT_ERP/BasisInformation/MasterInfoRecordRUD.cs` | 필드 배열·등록 부수효과 |
| `KIT_ERP/BasisInformation/*.aspx` | UI 필드 |
| `KIT_ERP/Base.cs` | `PageName` enum |
| `KIT_ERP/Menu.aspx.cs` | `CT_T` 메뉴 로드 |
| .cursor/rules/master-audit-fields.mdc | 기준정보 PK·소프트삭제·감사 자동화 |
| .cursor/rules/markdown-utf8.mdc | md UTF-8 저장 규칙 |

---

*갱신 시 본 문서 상단 상태 표와 §6 체크리스트를 함께 수정한다.*