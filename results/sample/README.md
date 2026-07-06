# KIT_ERP 신규 이관 설계 문서

> Spring Boot 3.5 + MariaDB + React (Vite) 이관용 설계서 모음

## 문서 목록

| 문서 | 설명 |
|------|------|
| [basis-company-spec.md](./basis-company-spec.md) | **거래처 기준정보 확정 스펙** (B1-R, INF-5 후속 포함) |
| [basis-item-spec.md](./basis-item-spec.md) | **품목 기준정보 확정 스펙** (11필드, 영업창고 1개, B1-R) |
| [basis-item-composition-spec.md](./basis-item-composition-spec.md) | **품목구성(BOM) 확정 스펙** (4필드 Modal, B2-R) |
| [basis-process-sequence-spec.md](./basis-process-sequence-spec.md) | **제품·공정품 공정순서 확정 스펙** (§5.5 SoT, B2-R) |
| [basis-work-standard-spec.md](./basis-work-standard-spec.md) | **작업표준 확정 스펙** (§5.7, UK process_sequence_id, B2-R) |
| [basis-equipment-spec.md](./basis-equipment-spec.md) | **설비 기준정보 확정 스펙** (8필드·샷, §5.6, B5-R) |
| [basis-work-center-spec.md](./basis-work-center-spec.md) | **작업장 기준정보 확정 스펙** (3필드 Modal, 분·480, B1-R) |
| [basis-production-calendar-spec.md](./basis-production-calendar-spec.md) | **생산달력 확정 스펙** (Override·분, §5.4a·§5.4b, B5-R) |
| [basis-unit-cost-spec.md](./basis-unit-cost-spec.md) | **단가 기준정보 확정 스펙** (3탭·발주비율, §5.8, B3-R) |
| [basis-user-spec.md](./basis-user-spec.md) | **사용자 기준정보 확정 스펙** (7필드·RBAC, §5.9, B0-R) |
| [basis-public-code-spec.md](./basis-public-code-spec.md) | **공용코드 확정 스펙** (PUC_MT 계층, usage_type, S0-R) |
| [basis-information-api-spec.md](./basis-information-api-spec.md) | 기준정보 14도메인 REST API·필드 매핑 (v1.13) |
| [basis-information-implementation-spec.md](./basis-information-implementation-spec.md) | 기준정보 구현 Wave, PK/FK, plan/actual, 금지사항 |
| [system-information-spec.md](./system-information-spec.md) | 시스템정보 8메뉴, RBAC, 마감·아카이브 |
| [inventory-ledger-spec.md](./inventory-ledger-spec.md) | 통합재고, 거래처 원장, 구매입고, 연도운영 |
| [purchase-receipt-quality-spec.md](./purchase-receipt-quality-spec.md) | **구매입고·품질검사** (TX1-R, 1액션, QI 검사품만) |
| [business-workflow-revision.md](./business-workflow-revision.md) | **업무 흐름 TO-BE** — 1액션, 확정/전기 미사용, SALES/DELIVERY |
| [mrp-work-plan-implementation-plan.md](./mrp-work-plan-implementation-plan.md) | **MRP·작업계획·Capa** — Wave 순서·체크리스트 |
| [production-work-report-mapping.md](./production-work-report-mapping.md) | 작업일보 — **등록=재고**, 최종→SALES (PRD-W1) |
| [work-report-material-issue-spec.md](./work-report-material-issue-spec.md) | **자재투입·작업일보 BOM** — PRD-W2 (투입 TX 분리) |
| [outsource-order-spec.md](./outsource-order-spec.md) | **INF-4 외주발주·출고·입고** — 4a 발주 구현 |
| [implementation-roadmap-and-exceptions.md](./implementation-roadmap-and-exceptions.md) | **구현 우선순위·예외 업무 정책** (E2E → W2 → 로드맵) |
| [e2e-step3-verification-results.md](./e2e-step3-verification-results.md) | **단계 3 E2E** (W1b·INF-1b·TX1-R, 2026-07-06 PASS) |
| [../backend/docs/IMPLEMENTATION_PROCEDURE.md](../backend/docs/IMPLEMENTATION_PROCEDURE.md) | **전체 구현 절차·Wave·승인 프로세스** |

## 구현 순서 (요약)

> **상세·예외 정책:** [implementation-roadmap-and-exceptions.md](./implementation-roadmap-and-exceptions.md)

1. 문서 검토·승인  
2. **S0 + B0** — 시스템정보 + 로그인·공용코드·사용자  
3. **B1~B2** — 거래처·품목·작업장·plan BOM/공정/작업표준  
4. **INF-1** — 재고·원장 서비스 (완료)  
5. **INF-2** — 회계월·월마감 (**TX1 선행**)  
6. **TX1** — 구매발주 ✅ → **TX1-R** 구매입고 ([purchase-receipt-quality-spec.md](./purchase-receipt-quality-spec.md))  
7. **PRD-W1** — 작업일보 **등록=재고**, 최종→SALES ✅ (진행 중)  
8. **【완료】단계 1** — **통합 E2E 검증** ✅ ([e2e-step1-verification-results.md](./e2e-step1-verification-results.md))  
9. **【다음】단계 2** — **PRD-W2** 자재투입 TX + 일보 가드 ([work-report-material-issue-spec.md](./work-report-material-issue-spec.md))  
10. **【완료】단계 3** — PRD-W1b · INF-1b · TX1-R ✅ ([e2e-step3-verification-results.md](./e2e-step3-verification-results.md))  

**예외 업무(필수 허용):** 수주 없는 생산·발주, 생산계획 없는 작업계획, 선입고, 외주 선출고, 수주 전 선납품 — [implementation-roadmap-and-exceptions.md](./implementation-roadmap-and-exceptions.md) §3

업무 흐름 TO-BE: [business-workflow-revision.md](./business-workflow-revision.md)

## 핵심 원칙 (한 줄)

> plan이 운영 SoT이고, **등록 1번 = 재고·원장 반영**(검사품 입고만 2단계), PK는 `id`·FK는 `*_id`로 통일한다.
