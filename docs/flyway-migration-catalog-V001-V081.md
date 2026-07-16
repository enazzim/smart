# Flyway 마이그레이션 카탈로그 (V001–V081)

- **경로**: `smartmanager_backend/smartmanager-infrastructure/src/main/resources/db/migration/`
- **대상 DB**: MariaDB `smartmanager`
- **작성 기준**: 각 SQL 파일 상단 주석·파일명·주요 DDL/DML을 정리
- **버전 범위**: V001 ~ V081 (연속 81개, 번호 공백 없음)

| 버전 | 파일명 | 하는 일 |
|------|--------|---------|
| V001 | `V001__smartmanager_core.sql` | SmartManager 코어 스키마 생성. `domain_event`, `code_group`, `public_code`, `company`, `company_role`, 거래처 원장(`partner_ledger_*`) 등 Step1 기본 테이블 |
| V002 | `V002__seed_code_group_public_code.sql` | 공용코드 대분류·대표 소코드 시드. `code_group` / `public_code` 초기 데이터 적재 |
| V003 | `V003__domain_event_event_id_varchar.sql` | `domain_event.event_id`를 JPA 매핑에 맞게 `CHAR(36)` → `VARCHAR(36)`으로 변경 |
| V004 | `V004__item.sql` | 품목 마스터 `item` 테이블 생성 |
| V005 | `V005__process_inventory.sql` | 공정·재고 기반 스키마. `inventory_location`, `work_center`, `process_sequence`, `inventory_balance` 생성·초기화 |
| V006 | `V006__unit_price.sql` | 단가 마스터·변경이력. `unit_price`, `unit_price_change_log` 생성 |
| V007 | `V007__item_composition.sql` | BOM(구성). `item_composition`, `bom_change_log` 생성 |
| V008 | `V008__work_standard.sql` | 작업표준 `work_standard` 테이블 생성 |
| V009 | `V009__equipment.sql` | 설비 마스터 `equipment` 생성 및 관련 권한/연계 정리 |
| V010 | `V010__user.sql` | 사용자·RBAC 기반 스키마. `user`, `role`, `permission`, `user_role`, `role_permission` 생성 |
| V011 | `V011__production_calendar.sql` | 생산·작업장 캘린더. `production_calendar`, `work_center_calendar` 생성 |
| V012 | `V012__auth_permissions_admin.sql` | S0 RBAC 권한 코드·역할 매핑 시드 (SYSTEM_ADMIN 등) |
| V013 | `V013__public_holiday.sql` | 공휴일 테이블 `public_holiday` 생성 및 2026년 대한민국 공휴일 시드 |
| V014 | `V014__month_closing.sql` | 회계월 마감 `month_closing` 테이블·권한 구성 (INF-2) |
| V015 | `V015__sales_order.sql` | 수주 TX. `sales_order`, `sales_order_line` 생성·권한 (TX-S1) |
| V016 | `V016__sales_order_line_status.sql` | 수주 라인 이행·납품 상태 컬럼/정책 추가 |
| V017 | `V017__production_plan.sql` | 생산계획 `production_plan` 생성·권한 (PRD-W0) |
| V018 | `V018__sync_system_admin_permissions.sql` | SYSTEM_ADMIN에 V012 이후 추가된 권한(생산계획 등) 재동기화 |
| V019 | `V019__production_plan_mrp_work_plan_status.sql` | 생산계획에 자재소요·작업계획 상태 컬럼 추가 |
| V020 | `V020__production_plan_delete_cancelled.sql` | 취소 상태 생산계획 레코드 정리 (취소=삭제 정책 전환) |
| V021 | `V021__mrp_material_requirement.sql` | MRP 산출 결과. `mrp_run`, `material_requirement_line` 생성·권한 |
| V022 | `V022__sync_mrp_admin_permissions.sql` | SYSTEM_ADMIN에 MRP 권한 재동기화 |
| V023 | `V023__purchase_order_tx1_skeleton.sql` | 구매발주 골격. `purchase_order`, `purchase_order_line` (MRP 연동) |
| V024 | `V024__system_settings.sql` | 시스템 기능 설정 `system_settings` 테이블·초기 시드 (S0) |
| V025 | `V025__purchase_order_permissions.sql` | 구매발주 권한 코드·역할 매핑 시드 (TX1) |
| V026 | `V026__sync_purchase_order_admin_permissions.sql` | SYSTEM_ADMIN에 `purchase:order` 권한 재동기화 |
| V027 | `V027__purchase_order_line_delivery_date.sql` | 구매발주 라인 납기요구일 컬럼 추가 (리드타임 기반) |
| V028 | `V028__inventory_movement_monthly.sql` | 재고 이동·월별 잔고. `stock_movement`, `inventory_balance_monthly` 및 현재고 수량 보강 (INF-1b) |
| V029 | `V029__purchase_receipt.sql` | 구매입고. `purchase_receipt`, `purchase_receipt_line` 생성 및 발주 라인 연계 (TX1-R1) |
| V030 | `V030__purchase_history.sql` | 매입 통계 이력 `purchase_history` 생성 |
| V031 | `V031__quality_inspection.sql` | 품질검사 원장 `quality_inspection` 생성 (TX1-R2) |
| V032 | `V032__purchase_receipt_permissions.sql` | 구매입고·품질검사 권한 시드 |
| V033 | `V033__purchase_order_receipt_status.sql` | 발주 헤더에 입고 진행·완료 상태 컬럼 추가 |
| V034 | `V034__work_plan.sql` | 작업계획 원장 `work_plan` 생성·권한 (PRD-W1a) |
| V035 | `V035__purchase_order_line_requirement_on_delete_set_null.sql` | MRP 소요 삭제 시 발주 라인 FK를 `ON DELETE SET NULL`로 변경·잔존 데이터 정리 |
| V036 | `V036__work_plan_cancel_recording_state.sql` | 작업계획 취소 건이 유니크키를 막아 재수립이 막히는 문제 정리 |
| V037 | `V037__scheduling_permissions.sql` | 작업장 부하·Capa 조회 권한 및 경고 임계값 시드 (PRD-W1b) |
| V038 | `V038__scheduling_read_from_work_plan.sql` | `work-plan:read` 역할에 `scheduling:read` 동기화 (기존 DB 호환) |
| V039 | `V039__work_order.sql` | 작업지시 `work_order` 생성·권한 (PRD-W1c) |
| V040 | `V040__work_report.sql` | 작업일보·실적 이력. `work_report`, `work_report_history` 생성·권한 |
| V041 | `V041__work_order_cancel_purge_inactive.sql` | 작업지시 소프트삭제 잔존 행 정리 (UK 충돌 방지) |
| V042 | `V042__work_report_consumption_line.sql` | 작업일보 투입(하위품목) 이력 `work_report_consumption_line` 생성 (취소 역전기용) |
| V043 | `V043__material_issue.sql` | 자재투입 TX. `material_issue`, `material_issue_line` 생성·권한 (PRD-W2) |
| V044 | `V044__inventory_ledger_permissions.sql` | 재고·원장 조회 권한 시드 (INF-1b) |
| V045 | `V045__production_material_issue_setting.sql` | 생산 자재투입 사용 여부 시스템 설정 시드 (기본: 작업일보 백플러시) |
| V046 | `V046__outsourcing_order.sql` | 외주발주. `outsourcing_order`, `outsourcing_order_line` 생성·권한 (INF-4a) |
| V047 | `V047__outsourcing_shipment.sql` | 외주출고. 헤더·라인·투입라인 테이블 생성·권한 (INF-4b) |
| V048 | `V048__inventory_allow_negative_stock_setting.sql` | 마이너스 재고 허용 여부 시스템 설정 시드 (기본 허용) |
| V049 | `V049__outsourcing_receipt.sql` | 외주입고·외주이력·품질검사 FK 확장. `outsourcing_receipt*`, `outsource_history` (INF-4c) |
| V050 | `V050__sales_shipment.sql` | 영업 출고·납품. `sales_shipment`, `sales_shipment_line` 및 수주 라인 연계 (SALES→DELIVERY) |
| V051 | `V051__sales_revenue.sql` | 매출등록. `sales_revenue`, `sales_revenue_line`, `sales_history` (DELIVERY 감소) |
| V052 | `V052__sales_collection.sql` | 수금. `sales_collection` 및 거래처 월원장 연계 (TX-S3) |
| V053 | `V053__inventory_location_wip_label.sql` | WIP 창고 표시명을 ‘생산창고’→‘공정창고’로 변경 |
| V054 | `V054__production_plan_manual.sql` | 수주 없는 수동 생산계획(MANUAL) 지원 컬럼/정책 추가 |
| V055 | `V055__outsourcing_shipment_advance.sql` | 외주 선출고(발주 비연동·별도 출고) 지원 스키마 보강 |
| V056 | `V056__community_board.sql` | 공통 게시판. `board_post`, `board_attachment` 생성·권한 (COMM-1) |
| V057 | `V057__work_diary.sql` | 업무일지. `work_diary_template`, `work_diary_entry` 생성·권한 (COMM-3) |
| V058 | `V058__work_diary_template_remove_writer_directive.sql` | 작성자 양식에서 지시사항(레거시 필드) 제거 — 결재자 `directive_note` 전용화 |
| V059 | `V059__basis_user_read_for_operators.sql` | 운영 역할에도 기준정보 사용자(본인 조회) 화면 읽기 권한 부여 |
| V060 | `V060__partner_payment.sql` | 지급 TX. `partner_payment` 및 월원장 연계 (구매·외주 미지급 정리, INF-5) |
| V061 | `V061__system_backup_permissions.sql` | 데이터 백업·복구 권한 시드 (S1) |
| V062 | `V062__item_model_type.sql` | 품목 마스터에 기종(`model_type`) 필드 추가 |
| V063 | `V063__etc_purchase.sql` | 기타구매 발주·입고 테이블 생성 및 `purchase_history` 연동 |
| V064 | `V064__dashboard_community_write_all.sql` | 대시보드 게시판: 로그인 사용자 전원 작성·수정·삭제 권한 |
| V065 | `V065__board_moderate_admin_only.sql` | 게시판 moderate를 SYSTEM_ADMIN만 유지 (타인 글 삭제·고정 제한) |
| V066 | `V066__work_diary_hard_delete.sql` | 업무일지 소프트삭제 잔여 정리 + 물리삭제 정책용 유니크/인덱스 변경 |
| V067 | `V067__payable_approval.sql` | 입고 지급승인 컬럼·상태 (구매·외주·기타구매, TX1-PA) |
| V068 | `V068__work_diary_checklist_schema.sql` | 업무일지 `field_schema` v2(체크리스트) 및 07·14·15 그룹 시드 |
| V069 | `V069__misc_stock_movement.sql` | 기타 입출고 TX `misc_stock_movement` 생성·권한 |
| V070 | `V070__sync_viewer_read_permissions.sql` | VIEWER 역할에 누락된 `:read` 권한 일괄 부여 |
| V071 | `V071__closing_fiscal_cutover_day_setting.sql` | 매입마감일(회계월 기준일) 시스템 설정 시드 |
| V072 | `V072__drawing.sql` | 도면 마스터·이력. `drawing_master`, `drawing_history` 생성 |
| V073 | `V073__drawing_permissions.sql` | 도면관리 권한 시드 |
| V074 | `V074__drawing_item_link.sql` | 도면 마스터 ↔ 품목 마스터 선택 연동 컬럼/제약 추가 |
| V075 | `V075__drawing_hard_delete_permission.sql` | 도면 영구삭제 권한 (SYSTEM_ADMIN 전용) |
| V076 | `V076__drawing_reference.sql` | 도면 history 간 참조(버전 pin) `drawing_reference` 생성 |
| V077 | `V077__item_lot_tracked_and_model_type.sql` | LOT-1: 품목 `lot_tracked` 추가, `model_type` 필수화, 도면 `model_group`→`model_type` 통일 |
| V078 | `V078__inventory_lot.sql` | LOT-1: Lot 마스터·슬롯 잔량·채번. `inventory_lot`, `inventory_lot_balance`, `lot_number_sequence` + 권한 |
| V079 | `V079__stock_movement_lot_and_genealogy.sql` | LOT-2: `stock_movement.lot_id` 및 계보 `lot_genealogy` 추가 |
| V080 | `V080__work_report_lot.sql` | LOT-3: 작업일보 투입·산출 Lot FK (`work_report_consumption_line.lot_id`, `work_report.output_lot_id`) |
| V081 | `V081__lot4_tx_lot_id.sql` | LOT-4: 영업출고·매출·외주출고 투입·외주입고·기타입출고 라인에 `lot_id` 추가 |
| V090 | `V090__order_vs_receipt_permission.sql` | 발주 vs 입고 권한 분리 |
| V091 | `V091__notice_board_post_read.sql` | 공지 게시판 읽음 확인 `board_post_read` |
| V092 | `V092__drawing_lifecycle.sql` | 도면 `lifecycle_stage`·`source_partner_id`·`item_linked_at` — [`drawing-lifecycle-design.md`](step0/drawing-lifecycle-design.md) |
| V093 | `V093__partner_prepaid.sql` | 품목 단위 선지급·승인 FIFO 상계 (`payment_kind`, `partner_payment_line`, `partner_prepaid_offset`) — [`partner-prepaid-design.md`](step0/partner-prepaid-design.md) |

## 참고

- Flyway는 이미 적용된 버전을 재실행하지 않습니다. 시드성 INSERT가 있어도 **최초 적용 시에만** 반영됩니다.
- 운영 기준정보(거래처·품목·BOM 등) 대량 시드는 본 마이그레이션과 별도로 `run-seed-operational-basis.bat`를 사용합니다.
- 로컬 접속 설정: `smartmanager-api/src/main/resources/application-local.yml`
