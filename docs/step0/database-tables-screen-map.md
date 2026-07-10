# SmartManager 테이블·역할·화면 매핑

> **기준:** Flyway `V001` ~ `V069` (2026-07-10)  
> **총 테이블 수:** 69개

**표기**

- **직접** — 해당 화면에서 CRUD·조회
- **연동** — 다른 TX 처리 시 자동 기록·갱신
- **—** — 전용 화면 없음 (인프라·마스터·로그)

---

## 1. 공통·인프라·시스템

| 테이블 | 역할 | 활용 화면 |
|--------|------|-----------|
| `domain_event` | 도메인 이벤트 저장 (프로젝터·감사) | — |
| `code_group` | 공용코드 대분류 | 시스템정보 > **공용코드** |
| `public_code` | 공용코드 소분류 (전역 참조) | **공용코드** · 거래처·품목·입출고사유 등 전 화면 |
| `role` | 역할 마스터 | 시스템정보 > **권한** (플레이스홀더) · 로그인 |
| `permission` | 권한 코드 | **권한** · 로그인 |
| `role_permission` | 역할-권한 매핑 | **권한** · 로그인 |
| `user` | 사용자 | 기준정보 > **사용자** · 로그인 |
| `user_role` | 사용자-역할 매핑 | **사용자** |
| `system_settings` | 시스템 설정 (마이너스재고·자재투입 등) | 시스템정보 > **시스템 설정** |
| `month_closing` | 월마감 상태 | 시스템정보 > **월마감** |
| `public_holiday` | 공휴일 | 기준정보 > **기본달력** (연동) |

---

## 2. 기준정보

| 테이블 | 역할 | 활용 화면 |
|--------|------|-----------|
| `company` | 거래처 마스터 | 기준정보 > **거래처** |
| `company_role` | 거래처 역할 (구매·영업·외주 등) | **거래처** |
| `item` | 품목 마스터 | 기준정보 > **품목** · 전 업무 화면 검색 |
| `item_composition` | BOM(품목구성) | 기준정보 > **품목구성** |
| `bom_change_log` | BOM 변경 이력 | — (변경 시 자동 기록) |
| `process_sequence` | 품목별 공정 순서 | 기준정보 > **공정** · 생산·외주·재고 |
| `work_center` | 작업장 | 기준정보 > **작업장** |
| `work_standard` | 작업표준 | 기준정보 > **작업표준** |
| `equipment` | 설비 | 기준정보 > **설비** |
| `unit_price` | 단가 | 기준정보 > **단가** |
| `unit_price_change_log` | 단가 변경 이력 | — (변경 시 자동 기록) |
| `production_calendar` | 기본(회사) 달력 | 기준정보 > **기본달력** |
| `work_center_calendar` | 작업장별 달력 | 기준정보 > **WC달력** |
| `inventory_location` | 창고 코드 마스터 (RAW/WIP/SALES 등) | — (시드) · 재고 전반 연동 |

---

## 3. 재고·원장 (공통)

| 테이블 | 역할 | 활용 화면 |
|--------|------|-----------|
| `inventory_balance` | 품목·창고·연도·공정별 재고 슬롯(현재고) | 재고 > **재고·원장** · **기타 입출고**(현재고) |
| `inventory_balance_monthly` | 슬롯별 월별 입·출·잔량 집계 | **재고·원장** |
| `stock_movement` | 재고 수량 변경 이력(단일 원장) | **재고·원장** · 모든 입출고 TX **연동** |
| `misc_stock_movement` | 기타 입출고 업무 전표 | 재고 > **기타 입출고** |
| `partner_ledger_account` | 거래처 원장 계정 | **재고·원장** · 구매·영업 **연동** |
| `partner_ledger_monthly` | 거래처 월별 원장 집계 | **재고·원장** · 승인·매출·수금 **연동** |

---

## 4. 영업

| 테이블 | 역할 | 활용 화면 |
|--------|------|-----------|
| `sales_order` | 수주 헤더 | 영업 > **수주** |
| `sales_order_line` | 수주 품목 라인 | **수주** |
| `sales_shipment` | 출고·납품 헤더 | 영업 > **출고·납품** |
| `sales_shipment_line` | 출고 라인 | **출고·납품** |
| `sales_revenue` | 매출 헤더 | 영업 > **매출** |
| `sales_revenue_line` | 매출 라인 | **매출** |
| `sales_collection` | 수금 | 영업 > **수금** |
| `sales_history` | 영업(매출·수금) 원장 이력 | **매출** · **수금** · **재고·원장** 연동 |

---

## 5. 생산

| 테이블 | 역할 | 활용 화면 |
|--------|------|-----------|
| `production_plan` | 생산계획 | 생산 > **생산계획** |
| `mrp_run` | MRP 실행 이력 | 생산 > **자재소요** |
| `material_requirement_line` | 자재소요 산출 라인 | **자재소요** |
| `work_plan` | 작업계획(공정별) | 생산 > **작업계획** · **작업장 부하** |
| `work_order` | 작업지시 | 생산 > **작업지시** |
| `material_issue` | 자재투입 헤더 | 생산 > **자재투입** |
| `material_issue_line` | 자재투입 라인 | **자재투입** |
| `work_report` | 작업일보 헤더 | 생산 > **작업일보** |
| `work_report_history` | 작업일보 공정별 실적 | **작업일보** |
| `work_report_consumption_line` | 작업일보 소모 자재 라인 | **작업일보** |

> 자재투입·작업일보·구매입고 등은 `stock_movement`·`inventory_balance`에 **연동**됩니다.

---

## 6. 구매

| 테이블 | 역할 | 활용 화면 |
|--------|------|-----------|
| `purchase_order` | 구매발주 헤더 | 구매 > **구매발주** |
| `purchase_order_line` | 구매발주 라인 | **구매발주** |
| `purchase_receipt` | 구매입고 헤더 | 구매 > **구매입고** |
| `purchase_receipt_line` | 구매입고 라인 | **구매입고** |
| `purchase_history` | 구매·기타구매 입고 원장 이력·승인상태 | **구매입고** · **기타구매입고** · **승인처리** · **재고·원장** |
| `etc_purchase_order` | 기타구매발주 | 구매 > **기타구매발주** |
| `etc_purchase_receipt` | 기타구매입고 | 구매 > **기타구매입고** |
| `partner_payment` | 거래처 지급 | 구매 > **지급** |

---

## 7. 외주

| 테이블 | 역할 | 활용 화면 |
|--------|------|-----------|
| `outsourcing_order` | 외주발주 헤더 | 외주 > **외주발주** |
| `outsourcing_order_line` | 외주발주 라인 | **외주발주** |
| `outsourcing_shipment` | 외주 출고 헤더 | 외주 > **출고** |
| `outsourcing_shipment_line` | 외주 출고 라인 | **출고** |
| `outsourcing_shipment_input_line` | 외주 출고 투입 자재 | **출고** |
| `outsourcing_receipt` | 외주 입고 헤더 | 외주 > **입고** |
| `outsourcing_receipt_line` | 외주 입고 라인 | **입고** |
| `outsource_history` | 외주 입고 원장·승인상태 | **입고** · **승인처리** · **재고·원장** |

---

## 8. 품질·커뮤니티·업무일지

| 테이블 | 역할 | 활용 화면 |
|--------|------|-----------|
| `quality_inspection` | 품질검사 | 품질 > **품질검사** |
| `board_post` | 게시판 글 | 대시보드 > **게시판** |
| `board_attachment` | 게시판 첨부 | **게시판** |
| `work_diary_template` | 업무일지 그룹별 양식 | 대시보드 > **업무일지** (양식 관리) |
| `work_diary_entry` | 업무일지 작성·결재 | 대시보드 > **업무일지** |

---

## 9. 일괄입력·백업

| 대상 | 역할 | 활용 화면 |
|------|------|-----------|
| 거래처·품목·BOM·공정·단가·작업장 등 | Excel 일괄 등록 | 시스템정보 > **초기정보 일괄입력** |
| 전체 DB | 백업·복구 (파일) | 시스템정보 > **시스템 설정** (백업 탭) |

---

## 10. 화면보다 테이블이 많은 이유

| 유형 | 예시 | 설명 |
|------|------|------|
| **헤더·라인 분리** | `sales_order` / `sales_order_line` | 1건 TX = 헤더 + 상세 |
| **원장 이력** | `purchase_history`, `sales_history`, `stock_movement` | 승인·원장·재고 추적용 |
| **변경 로그** | `bom_change_log`, `unit_price_change_log` | 감사용, 전용 화면 없음 |
| **마스터·시드** | `inventory_location`, `code_group` | 코드·창고 정의 |
| **인프라** | `domain_event` | 이벤트 기반 연동 |

---

## 11. 주요 화면 → 테이블 요약 (역방향)

| 화면 (메뉴) | 주요 테이블 |
|-------------|-------------|
| **재고 > 기타 입출고** | `misc_stock_movement`, `stock_movement`, `inventory_balance` |
| **재고 > 재고·원장** | `inventory_balance`, `stock_movement`, `inventory_balance_monthly`, `partner_ledger_*` |
| **구매 > 승인처리** | `purchase_history`, `outsource_history` |
| **생산 > 작업일보** | `work_report`, `work_report_history`, `work_report_consumption_line` + 재고 연동 |
| **대시보드 > 업무일지** | `work_diary_entry`, `work_diary_template` |
| **기준정보** (전 탭) | `company`, `item`, `item_composition`, `process_sequence`, `work_center` 등 |

---

## 12. 마이그레이션 경로

```
smartmanager_backend/smartmanager-infrastructure/src/main/resources/db/migration/
  V001__smartmanager_core.sql
  ...
  V069__misc_stock_movement.sql
```
