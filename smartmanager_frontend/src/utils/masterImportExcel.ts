import {
  createWorkbookWithSheet,
  downloadExcelWorkbook,
  normalizeExcelDate,
  parseFirstSheetRows,
} from './excelHelpers';
import { canonicalizeBusinessRegNo } from './businessRegNo';

export type ImportDomain =
  | 'company'
  | 'item'
  | 'item-composition'
  | 'work-center'
  | 'process'
  | 'work-standard'
  | 'unit-price';

export interface ImportDomainConfig {
  id: ImportDomain;
  label: string;
  uploadTitle: string;
  order: number;
  headers: readonly string[];
  sampleRow: Record<string, string | number>;
  fileName: string;
  sheetName: string;
}

export const IMPORT_DOMAINS: ImportDomainConfig[] = [
  {
    id: 'company',
    label: '① 거래처',
    uploadTitle: '거래처 업로드',
    order: 1,
    headers: [
      '상호', '대표자', '사업자등록번호', '법인등록번호', '사업장주소', '홈페이지', '업태', '종목',
      '전화번호', '팩스', '매출기준일', '어음승인기준', '정기수금일1', '담당자', '이메일',
      '판매거래처', '구매거래처', '외주거래처', '비용거래처',
    ],
    sampleRow: {
      상호: '샘플거래처', 대표자: '홍길동', 사업자등록번호: '1234567890', 법인등록번호: '',
      사업장주소: '서울시', 홈페이지: '', 업태: '', 종목: '', 전화번호: '', 팩스: '',
      매출기준일: '', 어음승인기준: '', 정기수금일1: '', 담당자: '', 이메일: '',
      판매거래처: 'Y', 구매거래처: 'Y', 외주거래처: '', 비용거래처: '',
    },
    fileName: '거래처일괄등록양식.xlsx',
    sheetName: '거래처',
  },
  {
    id: 'item',
    label: '② 품목',
    uploadTitle: '품목 업로드',
    order: 2,
    headers: [
      '품목번호', '품목명', '자산분류', '기종', '단위', '규격', '표준원가', '검사구분',
      '리드타임', '안전재고', '발주간격', '최소발주량', 'Lot추적',
    ],
    sampleRow: {
      품목번호: 'ITEM-001', 품목명: '샘플품목', 자산분류: '제품', 기종: '로더', 단위: 'EA', 규격: '',
      표준원가: 1000, 검사구분: 'NONE', 리드타임: 0, 안전재고: '', 발주간격: '', 최소발주량: '',
      Lot추적: 'N',
    },
    fileName: '품목일괄등록양식.xlsx',
    sheetName: '품목',
  },
  {
    id: 'item-composition',
    label: '③ 품목구성',
    uploadTitle: '품목구성 업로드',
    order: 3,
    headers: ['모품목번호', '자품목번호', '모품수량', '자품수량'],
    sampleRow: { 모품목번호: 'ITEM-001', 자품목번호: 'RAW-001', 모품수량: 1, 자품수량: 2 },
    fileName: '품목구성일괄등록양식.xlsx',
    sheetName: '품목구성',
  },
  {
    id: 'work-center',
    label: '④ 작업장',
    uploadTitle: '작업장 업로드',
    order: 4,
    headers: ['작업장명', '대표공정코드', '가동시간(분)'],
    sampleRow: { 작업장명: '절단라인2', 대표공정코드: '14000010', '가동시간(분)': 480 },
    fileName: '작업장일괄등록양식.xlsx',
    sheetName: '작업장',
  },
  {
    id: 'process',
    label: '⑤ 공정',
    uploadTitle: '공정 업로드',
    order: 5,
    headers: ['품목번호', '순서번호', '공정코드', '작업구분', '작업장명', '발주비율', '진척비율'],
    sampleRow: {
      품목번호: 'ITEM-001', 순서번호: 10, 공정코드: '14000010', 작업구분: 'INHOUSE',
      작업장명: '절단라인', 발주비율: '', 진척비율: 100,
    },
    fileName: '공정일괄등록양식.xlsx',
    sheetName: '공정',
  },
  {
    id: 'work-standard',
    label: '⑥ 작업표준',
    uploadTitle: '작업표준 업로드',
    order: 6,
    headers: [
      '품목번호', '공정순서번호', '공정코드', '작업장명', '설비코드', '우선순위',
      '주작업자로그인ID', '공구명', '준비시간(분)', '표준시간(초)',
    ],
    sampleRow: {
      품목번호: 'ITEM-001', 공정순서번호: 10, 공정코드: '14000010', 작업장명: '절단라인',
      설비코드: '', 우선순위: 1, 주작업자로그인ID: '', 공구명: '', '준비시간(분)': 10, '표준시간(초)': 60,
    },
    fileName: '작업표준일괄등록양식.xlsx',
    sheetName: '작업표준',
  },
  {
    id: 'unit-price',
    label: '⑦ 단가',
    uploadTitle: '단가 업로드',
    order: 7,
    headers: [
      '단가구분', '품목번호', '사업자등록번호', '시작공정코드', '종료공정코드', '발주비율',
      '표준단가', '할인단가', '적용시작일', '적용종료일',
    ],
    sampleRow: {
      단가구분: '판매단가', 품목번호: 'ITEM-001', 사업자등록번호: '1234567890',
      시작공정코드: '', 종료공정코드: '', 발주비율: '', 표준단가: 1000, 할인단가: '',
      적용시작일: '2026-01-01', 적용종료일: '',
    },
    fileName: '단가일괄등록양식.xlsx',
    sheetName: '단가',
  },
];

/** Y / 예 / 1 / O / ○ 등 → true. 빈칸·N → false */
function parseYesFlag(value: unknown): boolean {
  const raw = cellString(value).trim().toLowerCase();
  if (!raw) {
    return false;
  }
  if (raw === 'n' || raw === 'no' || raw === 'false' || raw === '0' || raw === '아니오' || raw === 'x') {
    return false;
  }
  return (
    raw === 'y' ||
    raw === 'yes' ||
    raw === 'true' ||
    raw === '1' ||
    raw === '예' ||
    raw === 'o' ||
    raw === '○' ||
    raw === 'v' ||
    raw === '✓'
  );
}

function parseLotTrackedFlag(value: unknown): boolean {
  return parseYesFlag(value);
}

/**
 * 판매/구매/외주/비용 컬럼 → API용 roles 문자열 (SALES,PURCHASE,...)
 * 구 양식 `역할` 컬럼이 있으면 하위 호환으로 사용.
 */
function parseCompanyRoles(row: Record<string, unknown>): string {
  const roles: string[] = [];
  if (parseYesFlag(row['판매거래처'])) roles.push('SALES');
  if (parseYesFlag(row['구매거래처'])) roles.push('PURCHASE');
  if (parseYesFlag(row['외주거래처'])) roles.push('OUTSOURCE');
  if (parseYesFlag(row['비용거래처'])) roles.push('COST');

  if (roles.length > 0) {
    return roles.join(',');
  }

  const legacy = cellString(row['역할']);
  if (legacy) {
    return legacy
      .split(/[,|]/)
      .map((s) => s.trim().toUpperCase())
      .filter(Boolean)
      .join(',');
  }
  return '';
}

function cellString(value: unknown): string {
  if (value == null) return '';
  return String(value).trim();
}

/** 레거시 자산분류 → 현행 Enum (Cut-over). 소모품→부자재, 반제품→공정품 */
function mapPropertyClassification(raw: string): string {
  switch (raw) {
    case '소모품':
      return '부자재';
    case '반제품':
      return '공정품';
    default:
      return raw;
  }
}

/** Cut-over 엑셀: 판매단가/구매단가/외주단가 → SALE/PURCHASE/OUTSOURCE */
function mapCostType(raw: string): string {
  const text = raw.trim();
  const upper = text.toUpperCase();
  if (upper === 'SALE' || text === '판매' || text === '판매단가') return 'SALE';
  if (upper === 'PURCHASE' || text === '구매' || text === '구매단가') return 'PURCHASE';
  if (upper === 'OUTSOURCE' || text === '외주' || text === '외주단가') return 'OUTSOURCE';
  return upper;
}

function cellNumber(value: unknown): number | undefined {
  const text = cellString(value).replace(/,/g, '');
  if (!text) return undefined;
  const parsed = Number(text);
  return Number.isFinite(parsed) ? parsed : undefined;
}

/** 수량 0을 유효 값으로 유지 (빈칸만 undefined) */
function cellNumberAllowZero(value: unknown): number | undefined {
  if (value === 0 || value === '0') {
    return 0;
  }
  return cellNumber(value);
}

function normalizeDate(value: unknown): string {
  return normalizeExcelDate(value);
}

export async function downloadImportTemplate(domain: ImportDomain) {
  const config = IMPORT_DOMAINS.find((d) => d.id === domain);
  if (!config) throw new Error('알 수 없는 도메인입니다.');
  const workbook = createWorkbookWithSheet(config.sheetName, config.headers, [config.sampleRow]);
  await downloadExcelWorkbook(workbook, config.fileName);
}

export async function parseImportExcel(
  domain: ImportDomain,
  buffer: ArrayBuffer,
  fileName?: string,
): Promise<Record<string, unknown>[]> {
  const config = IMPORT_DOMAINS.find((d) => d.id === domain);
  if (!config) throw new Error('알 수 없는 도메인입니다.');
  const rawRows = await parseFirstSheetRows(buffer, { fileName });
  return rawRows
    .map((row) => mapRow(domain, row))
    .filter((row) => Object.values(row).some((v) => v !== '' && v != null));
}

function mapRow(domain: ImportDomain, row: Record<string, unknown>): Record<string, unknown> {
  switch (domain) {
    case 'company':
      return {
        companyName: cellString(row['상호']),
        presidentName: cellString(row['대표자']),
        businessRegNo: canonicalizeBusinessRegNo(cellString(row['사업자등록번호'])),
        corporationRegNo: cellString(row['법인등록번호']) || null,
        businessAddress: cellString(row['사업장주소']),
        homepageUrl: cellString(row['홈페이지']) || null,
        businessType: cellString(row['업태']) || null,
        businessItem: cellString(row['종목']) || null,
        telephone: cellString(row['전화번호']) || null,
        fax: cellString(row['팩스']) || null,
        saleStandardDay: cellNumber(row['매출기준일']) ?? null,
        billApprovalStandard: cellNumber(row['어음승인기준']) ?? null,
        fixCollectDay1: cellNumber(row['정기수금일1']) ?? null,
        contactName: cellString(row['담당자']) || null,
        contactEmail: cellString(row['이메일']) || null,
        roles: parseCompanyRoles(row),
      };
    case 'item':
      return {
        itemNo: cellString(row['품목번호']),
        itemName: cellString(row['품목명']),
        propertyClassification: mapPropertyClassification(cellString(row['자산분류'])),
        modelType: cellString(row['기종']),
        unit: cellString(row['단위']),
        standard: cellString(row['규격']) || null,
        standardUnitCost: cellNumber(row['표준원가']) ?? null,
        checkDistinction: cellString(row['검사구분']) || 'NONE',
        leadTime: cellNumber(row['리드타임']) ?? null,
        safetyStockQuantity: cellNumber(row['안전재고']) ?? null,
        orderIntervalQuantity: cellNumber(row['발주간격']) ?? null,
        minOrderQuantity: cellNumber(row['최소발주량']) ?? null,
        lotTracked: parseLotTrackedFlag(row['Lot추적']),
      };
    case 'item-composition':
      return {
        parentItemNum: cellString(row['모품목번호']),
        childItemNum: cellString(row['자품목번호']),
        parentQuantity: cellNumber(row['모품수량']),
        // 0 허용(하위 전개 제외용). undefined만 미입력으로 취급
        childQuantity: cellNumberAllowZero(row['자품수량']),
      };
    case 'work-center':
      return {
        wcName: cellString(row['작업장명']),
        mainProcessSmallCode: cellString(row['대표공정코드']),
        operationTime: cellNumber(row['가동시간(분)']),
      };
    case 'process': {
      const workDistinction = cellString(row['작업구분']).toUpperCase();
      const usesOutsideRate = workDistinction === 'SPLIT';
      return {
        itemNo: cellString(row['품목번호']),
        processSequenceNum: cellNumber(row['순서번호']),
        processSmallCode: cellString(row['공정코드']),
        workDistinction,
        workCenterName: cellString(row['작업장명']) || null,
        // OUTSOURCE·INHOUSE: 엑셀 발주비율 무시. SPLIT만 0~100 사용
        outsideOrderRate: usesOutsideRate ? cellNumberAllowZero(row['발주비율']) ?? 0 : 0,
        progressRate: cellNumber(row['진척비율']) ?? null,
      };
    }
    case 'work-standard':
      return {
        itemNum: cellString(row['품목번호']),
        processSequenceNum: cellNumber(row['공정순서번호']),
        processSmallCode: cellString(row['공정코드']),
        workCenterName: cellString(row['작업장명']),
        equipmentNum: cellString(row['설비코드']) || null,
        priorityOrder: cellNumber(row['우선순위']),
        mainWorkerLoginId: cellString(row['주작업자로그인ID']) || null,
        toolName: cellString(row['공구명']) || null,
        setupTime: cellNumber(row['준비시간(분)']),
        standardTime: cellNumber(row['표준시간(초)']),
      };
    case 'unit-price': {
      const costType = mapCostType(cellString(row['단가구분']));
      const usesProcess = costType === 'OUTSOURCE';
      return {
        costType,
        itemNum: cellString(row['품목번호']),
        businessRegNo: canonicalizeBusinessRegNo(cellString(row['사업자등록번호'])),
        // 판매·구매: 엑셀 공정값 무시 → null / 외주만 사용
        beginProcessSmallCode: usesProcess ? cellString(row['시작공정코드']) || null : null,
        endProcessSmallCode: usesProcess ? cellString(row['종료공정코드']) || null : null,
        orderRate: cellNumber(row['발주비율']) ?? null,
        standardUnitCost: cellNumber(row['표준단가']),
        discountUnitCost: (() => {
          const discount = cellNumber(row['할인단가']);
          return discount != null && discount > 0 ? discount : null;
        })(),
        beginDate: normalizeDate(row['적용시작일']),
        endDate: normalizeDate(row['적용종료일']) || null,
      };
    }
    default:
      return row;
  }
}

export function validateImportRows(
  domain: ImportDomain,
  rows: Record<string, unknown>[],
): { rowNumber: number; message: string }[] {
  const errors: { rowNumber: number; message: string }[] = [];
  rows.forEach((row, index) => {
    const rowNumber = index + 2;
    const required = getRequiredFields(domain);
    for (const field of required) {
      const value = row[field];
      // 숫자 0은 유효 입력으로 인정 (자품수량 0 등)
      if (value === undefined || value === null || value === '') {
        if (domain === 'company' && field === 'roles') {
          errors.push({
            rowNumber,
            message: '판매/구매/외주/비용거래처 중 최소 1개에 Y 입력',
          });
        } else {
          errors.push({ rowNumber, message: `${field} 필수` });
        }
      }
    }
    if (domain === 'item-composition') {
      const parentQty = row.parentQuantity;
      const childQty = row.childQuantity;
      if (typeof parentQty === 'number' && !(parentQty > 0)) {
        errors.push({ rowNumber, message: '모품수량은 0보다 커야 합니다.' });
      }
      if (typeof childQty === 'number' && childQty < 0) {
        errors.push({ rowNumber, message: '자품수량은 0 이상이어야 합니다.' });
      }
    }
    if (domain === 'process') {
      const distinction = String(row.workDistinction ?? '').toUpperCase();
      const rate = row.outsideOrderRate;
      if (distinction === 'SPLIT' && typeof rate === 'number' && (rate < 0 || rate > 100)) {
        errors.push({ rowNumber, message: '혼합(SPLIT) 발주비율은 0~100 사이여야 합니다.' });
      }
    }
    if (domain === 'unit-price') {
      const costType = String(row.costType ?? '');
      if (costType && !['SALE', 'PURCHASE', 'OUTSOURCE'].includes(costType)) {
        errors.push({
          rowNumber,
          message: '단가구분은 SALE/PURCHASE/OUTSOURCE(또는 판매단가/구매단가/외주단가)여야 합니다.',
        });
      }
      if (costType === 'OUTSOURCE') {
        if (!row.beginProcessSmallCode) {
          errors.push({ rowNumber, message: '외주단가는 시작공정코드 필수' });
        }
        if (!row.endProcessSmallCode) {
          errors.push({ rowNumber, message: '외주단가는 종료공정코드 필수' });
        }
        if (row.orderRate === undefined || row.orderRate === null || row.orderRate === '') {
          errors.push({ rowNumber, message: '외주단가는 발주비율 필수' });
        }
      }
      if (costType === 'PURCHASE') {
        if (row.orderRate === undefined || row.orderRate === null || row.orderRate === '') {
          errors.push({ rowNumber, message: '구매단가는 발주비율 필수' });
        }
      }
    }
  });
  return errors;
}

function getRequiredFields(domain: ImportDomain): string[] {
  switch (domain) {
    case 'company':
      return ['companyName', 'presidentName', 'businessRegNo', 'businessAddress', 'roles'];
    case 'item':
      return ['itemNo', 'itemName', 'propertyClassification', 'modelType', 'unit'];
    case 'item-composition':
      return ['parentItemNum', 'childItemNum', 'parentQuantity', 'childQuantity'];
    case 'work-center':
      return ['wcName', 'mainProcessSmallCode', 'operationTime'];
    case 'process':
      return ['itemNo', 'processSequenceNum', 'processSmallCode', 'workDistinction'];
    case 'work-standard':
      return ['itemNum', 'processSequenceNum', 'processSmallCode', 'workCenterName', 'priorityOrder', 'setupTime', 'standardTime'];
    case 'unit-price':
      return ['costType', 'itemNum', 'businessRegNo', 'standardUnitCost', 'beginDate'];
    default:
      return [];
  }
}

export async function uploadImportDomain(
  domain: ImportDomain,
  rows: Record<string, unknown>[],
): Promise<import('../api/masterImport').BulkImportResult> {
  const api = await import('../api/masterImport');
  switch (domain) {
    case 'company':
      return api.importCompaniesBulk(rows);
    case 'item':
      return api.importItemsBulk(rows);
    case 'item-composition':
      return api.importItemCompositionsBulk(rows);
    case 'work-center':
      return api.importWorkCentersBulk(rows);
    case 'process':
      return api.importProcessesBulk(rows);
    case 'work-standard':
      return api.importWorkStandardsBulk(rows);
    case 'unit-price':
      return api.importUnitPricesBulk(rows);
    default:
      throw new Error('알 수 없는 도메인입니다.');
  }
}
