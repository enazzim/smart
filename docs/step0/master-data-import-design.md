# 초기정보 일괄입력 설계서

> **문서 버전:** 1.0  
> **작성일:** 2026-07-07  
> **Wave:** S1  
> **관련:** `results/sample/system-information-spec.md` §3.3, `d4-company.md` ~ `d4-unit-price.md`

---

## 1. 목적

마이그레이션·초기 구축 시 기준정보 7종을 **엑셀 양식 → 일괄 등록**한다. 일상 CRUD 화면을 대체하지 않는다.

| 구분 | 내용 |
|------|------|
| 메뉴 | 시스템정보 → **초기정보 일괄입력** |
| 권한 | `system:import:execute` (SYSTEM_ADMIN) |
| API prefix | `/api/v1/system/imports/master-data` |
| 처리 | 동기 bulk, 행별 검증, **부분 성공** 허용 |

---

## 2. 입력 순서 (FK 의존)

```
① 거래처 → ② 품목 → ③ 품목구성 → ④ 작업장 → ⑤ 공정 → ⑥ 작업표준 → ⑦ 단가
```

UI에 순서 안내. 서버는 도메인별 독립 API이나 FK 미충족 시 해당 행 실패.

---

## 3. API

| 메서드 | 경로 | 설명 |
|--------|------|------|
| POST | `/company` | 거래처 bulk |
| POST | `/item` | 품목 bulk |
| POST | `/item-composition` | 품목구성 bulk |
| POST | `/work-center` | 작업장 bulk |
| POST | `/process` | 공정 bulk |
| POST | `/work-standard` | 작업표준 bulk |
| POST | `/unit-price` | 단가 bulk |

### 공통 응답

```json
{
  "successCount": 10,
  "failureCount": 2,
  "failures": [
    { "rowIndex": 3, "key": "123-45-67890", "message": "사업자번호 중복" }
  ]
}
```

- `rowIndex`: 0-based (엑셀 2행 = index 0)
- 실패 시에도 성공 건은 커밋 (수주 bulk와 동일)

---

## 4. 엑셀 양식 (한글 헤더 1행)

### 4.1 거래처

| 컬럼 | 필수 | 비고 |
|------|------|------|
| 상호, 대표자, 사업자등록번호, 사업장주소 | Y | |
| 판매거래처, 구매거래처, 외주거래처, 비용거래처 | Y(최소 1) | Y/예/1 등 → 각각 SALES/PURCHASE/OUTSOURCE/COST 매핑. 해당 없으면 빈칸 |
| 법인등록번호, 홈페이지, 업태, 종목, 전화, 팩스, 매출기준일, 어음승인기준, 정기수금일1, 담당자, 이메일 | N | |

### 4.2 품목

| 컬럼 | 필수 |
|------|------|
| 품목번호, 품목명, 자산분류, 단위 | Y |
| 규격, 표준원가, 검사구분, 리드타임, 안전재고, 발주간격, 최소발주량 | N |

자산분류: `원자재`/`제품`/`상품`/`공정품`  
검사구분: `NONE`/`INSPECTION`

### 4.3 품목구성

모품목번호, 자품목번호, 모품수량, 자품수량 (모두 필수, > 0)

### 4.4 작업장

작업장명, 대표공정코드(small_code 예: 14000010), 가동시간(분)

### 4.5 공정

품목번호, 순서번호, 공정코드, 작업구분(`INHOUSE`/`OUTSOURCE`/`SPLIT`), 작업장명(조건부), 발주비율(조건부), 진척비율(기본 100)

### 4.6 작업표준

품목번호, 공정순서번호, 공정코드, 작업장명, 우선순위, 준비시간(분), 표준시간(초) + 설비코드, 주작업자로그인ID, 공구명(선택)

### 4.7 단가

단가구분(`SALE`/`PURCHASE`/`OUTSOURCE`), 품목번호, 사업자등록번호, 표준단가, 적용시작일 + 시작/종료공정코드, 발주비율, 할인단가, 적용종료일(선택)

---

## 5. FK Resolve 규칙

| 입력 키 | Resolve |
|---------|---------|
| 사업자등록번호 | `company.id` |
| 품목번호 | `item.id` |
| 공정코드 (small_code) | `public_code.id` (usage PROCESS) |
| 작업장명 | `work_center.id` |
| 품목+순서+공정코드 | `process_sequence.id` |
| 설비코드 | `equipment.id` |
| 로그인ID | `user.id` |

---

## 6. 금지 사항

1. Import 시 창고·BSI·외주창고 자동 생성 없음
2. 시스템 컬럼(`id`, `recording_state`, `created_*`, `updated_*`) 요청 수신 금지
3. `actual` variant 공정·BOM Import 제외 (plan만)

---

## 7. 구현 패키지

```text
api/web/system/import/MasterDataImportController.java
api/web/system/import/*BulkRequest.java, BulkImportResponse.java
application/system/MasterDataImportService.java
infrastructure/application/MasterDataImportApplicationService.java
frontend: MasterImportPage.tsx, utils/masterImportExcel.ts, api/masterImport.ts
```

---

## 8. 문서 이력

| 버전 | 일자 | 내용 |
|------|------|------|
| 1.0 | 2026-07-07 | 초안 — 7도메인 bulk API·엑셀 양식 |
| 1.1 | 2026-07-20 | 거래처 역할: 역할 콤마 → 판매/구매/외주/비용 Y/N 컬럼 |
