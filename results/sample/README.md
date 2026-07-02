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
| [business-workflow-revision.md](./business-workflow-revision.md) | **업무 흐름 TO-BE** — 의뢰 단계 제거, INF-2→TX1 순서 |
| [../backend/docs/IMPLEMENTATION_PROCEDURE.md](../backend/docs/IMPLEMENTATION_PROCEDURE.md) | **전체 구현 절차·Wave·승인 프로세스** |

## 구현 순서 (요약)

1. 문서 검토·승인  
2. **S0 + B0** ? 시스템정보 + 로그인·공용코드·사용자  
3. **B1~B2** ? 거래처·품목·작업장·plan BOM/공정/작업표준  
4. **INF-1** — 재고·원장 서비스 (완료)  
5. **INF-2** — 회계월·월마감 (**TX1 선행**)  
6. **TX1** — 구매발주·구매입고 E2E (TO-BE: 구매의뢰 없음)  
7. **B3~B4** — 단가·actual/compare (B3·B4 완료 시 생략)  
8. **Frontend** — 별도 승인 후 Vite 생성  

업무 흐름 TO-BE: [business-workflow-revision.md](./business-workflow-revision.md)

## 핵심 원칙 (한 줄)

> plan이 운영 SoT이고, 마스터 등록 시 창고·BSI를 만들지 않으며, PK는 `id`·FK는 `*_id`로 통일한다.
