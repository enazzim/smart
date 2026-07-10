# P0 E2E 검증 결과

> 검증일: 2026-07-10  
> 환경: localhost:8080 / admin  
> 실행: `run-fresh-p0-pipeline.bat` (DROP DB → API 재기동 → 시드 → E2E 1~3)

## P0-1 전체 TX 체인

- 결과: **PASS** (33/33)
- 제품: FG-01
- 원자재: RM-W01, RM-W02, RM-PA, RM-PB
- 외주: SF-01 seq10/20 + FG-01 seq10/20 (OO→OSh→OR 각 4건)
- 비고: Phase 4a 외주 TX 포함, Phase 6 취소 역전 포함

## P0-2 승인처리

- 결과: **PASS** (7/7)
- 원자재: RM-PA
- 전표: PR-20260710-002, historyId=5
- 비고: 미지급 0 → 500.00 → 0 원복

## P0-3 월 마감

- 결과: **PASS** (5/5)
- 마감 대상: 2026-06
- 비고: 마감 월 입고·승인 HTTP 500 차단 확인 후 cleanup reopen

## 시드 (SEED-OP)

- 결과: **PASS** (13/13)
- Companies 17 / Items 17 / BOM 26 / Prices 30 / WS 10 / Users 10

## Part06 외주 Projector

- 결과: **PASS** (8/8)
- 제품: FG-01 (시드 외주단가·공정 확인)
- 비고: P1/P2 negative, Case B/C, cleanup delete

## 종합

- **P0 E2E PASS 45 / FAIL 0** (P0-1: 33, P0-2: 7, P0-3: 5)
- **Part06 PASS 8 / FAIL 0**
- 최종 검증: 2026-07-10 `e2e-p0-verify.ps1` ALL PASS
