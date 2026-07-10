# B1 P1 현업 리뷰 브리프 (품목·공정·단가)

> **일자:** 2026-07-10  
> **선행:** P0 E2E 통과 (`run-fresh-p0-pipeline.bat` ALL PASS)  
> **체크리스트:** [`docs/step0/b1-business-d4-review-checklist.md`](../../docs/step0/b1-business-d4-review-checklist.md) §E·F·G  
> **회의 진행안:** [`b1-p1-review-meeting-runbook.md`](./b1-p1-review-meeting-runbook.md)

---

## 1. 리뷰 목적

P0(거래처·공용코드) E2E 이후, **기준정보 핵심 3메뉴** 설계가 현업 업무와 맞는지 2차 확인합니다.

| 차수 | 범위 | 소요 |
|------|------|------|
| 2차 P1 | 품목 · 공정 · 단가 | 60~90분 |

---

## 2. 시연 데이터 (운영 시드)

`run-seed-operational-basis.bat` 실행 후 화면/API에서 확인할 수 있는 구성입니다.

| 구분 | 내용 |
|------|------|
| 제품 | FG-01 ~ FG-05 |
| 공정품 | SF-01 ~ SF-05 |
| 원자재 | RM-PA, RM-PB, RM-W01 ~ W05 |
| 판매 거래처 | Sales-01 ~ 05 (제품당 1개) |
| 구매 거래처 | Purchase-01 ~ 10 (원자재 7개에 1:1 매핑, 08~10은 거래처만) |
| 외주 거래처 | Outsource-01, Outsource-02 |
| 단가 | 판매 5 + 구매 7 + **외주 18** (OUTSOURCE 공정당 1거래처, 발주비율 100%) |
| BOM | FG-01: SF-01 + RM-PA/PB, SF-01: RM-W01/W02 (다단계) |

테스트 사용자: `worker01`~`10` / `Oper1234!`

---

## 3. 섹션별 확인 포인트

### E. 품목 ([d4-item.md](../../docs/step0/d4-item.md))

| # | 설계 | 시연 시 확인 |
|---|------|-------------|
| E1 | 11필드 슬림 | 품목 등록 화면 필드 수 |
| E2 | 자산분류 4종 | FG=제품, SF=공정품, RM=원자재 표기 |
| E3 | 품목 등록 시 재고 Lazy | FG-01 등록 직후 재고 0, 입고/실적 후에만 증가 (P0-1 E2E) |
| E4 | Drop 필드 없음 | 도면번호·재질 등 미노출 |

### F. 공정 ([d4-process.md](../../docs/step0/d4-process.md))

| # | 설계 | 시연 시 확인 |
|---|------|-------------|
| F1 | Modal 7필드 | 공정 등록 UI |
| F2 | 제품·공정품만 | 원자재 RM-PA에 공정 없음 |
| F3 | 완료 WIP Lazy | 공정 등록 후 WIP 슬롯 (Projector) |
| F4 | INHOUSE/OUTSOURCE | FG-01: 용접·성형 OUTSOURCE + 절단 INHOUSE (시드) |

### G. 단가 ([d4-unit-price.md](../../docs/step0/d4-unit-price.md))

| # | 설계 | 시연 시 확인 |
|---|------|-------------|
| G1 | unit_price 통합 3탭 | 판매/구매/외주 탭 |
| G2 | 외주만 시작·끝 공정 FK | FG-01 외주단가: 공정코드별 begin=end |
| G3 | 변경사유 + 이력 | 단가 수정 시 사유 입력 |
| G4 | 외주 투입 창고 자동 생성 | 외주단가 등록 시 OUTSOURCE 투입 잔고 (Part 06 E2E) |

---

## 4. P0 E2E와의 연결

| P0 E2E | P1 설계 검증 |
|--------|-------------|
| P0-1 FG-01 전체 TX | 품목·BOM·공정·구매단가로 MRP→입고→작업→출고 |
| P0-2 RM-PA 입고 승인 | 구매단가·거래처 1:1 |
| P0-3 월마감 | 단가/전표 일자와 무관, TX 차단 정책 |

---

## 5. 서명

체크리스트 [`b1-business-d4-review-checklist.md`](../../docs/step0/b1-business-d4-review-checklist.md) §I 서명란에 2차(P1) 확인 후 기입합니다.

| 역할 | 2차 (P1) | 일자 |
|------|:--------:|------|
| 기준정보 현업 | [ ] | |
| 개발 리드 | [ ] | |
| PM / 설계 | [ ] | |
