# Flyway 마이그레이션 카탈로그 (V001–V010)

- **경로**: `smartmanager_backend/smartmanager-infrastructure/src/main/resources/db/migration/`
- **대상 DB**: MariaDB `smartmanager`
- **작성 기준**: 2026-08-01 안 B 통폐합 (구 V001–V095를 **시간순 concat**으로 9개 파일에 묶음) + 이후 추가분
- **적용 전제**: 로컬·EC2 테스트 DB를 drop 후 재생성한 환경에서 V001–V009 적용. 이후는 V010+만 추가 적용.

| 버전 | 파일명 | 하는 일 | 구 버전 범위 |
|------|--------|---------|--------------|
| V001 | `V001__core.sql` | 코어 스키마·공용코드 시드·`domain_event.event_id` 조정 | V001–V003 |
| V002 | `V002__basis_auth_calendar.sql` | 기준정보 마스터·RBAC·생산달력·공휴일·월마감 | V004–V014 |
| V003 | `V003__sales_production_mrp.sql` | 수주·생산계획·MRP·ADMIN 권한 sync | V015–V022 |
| V004 | `V004__purchase_receipt_qi.sql` | 구매발주·시스템설정·입고·매입이력·품질검사 | V023–V033 |
| V005 | `V005__work_inventory.sql` | 작업계획·지시·일보·자재불출·재고/원장 권한 | V034–V045 |
| V006 | `V006__outsource_sales_fulfill.sql` | 외주 3종·선출고·출고·매출·수금·수동 생산계획 | V046–V055 |
| V007 | `V007__community_payment_system.sql` | 게시판·업무일지·지급·승인·기타입출·VIEWER sync 등 | V056–V071 |
| V008 | `V008__drawing_lot.sql` | 도면·Lot 마스터·계보·TX lot_id | V072–V081 |
| V009 | `V009__stats_claims_extensions.sql` | 통계 권한·클레임·선지급·도면 lifecycle·품목 속성 확장 | V082–V095 |
| V010 | `V010__align_operator_menu_permissions.sql` | 생산←품질검사, 구매←재고·원장 권한 부여 | — |

## 이후 추가분

통폐합 이후 DB 변경은 **V010, V011…** 새 파일만 추가한다. V001–V009는 수정하지 않는다.

## 참고

- Flyway는 이미 적용된 버전을 재실행하지 않습니다. 시드성 INSERT는 **최초 적용 시에만** 반영됩니다.
- 운영 기준정보 대량 시드는 `run-seed-operational-basis.bat`를 사용합니다.
- 로컬 접속 설정: `smartmanager-api/src/main/resources/application-local.yml`