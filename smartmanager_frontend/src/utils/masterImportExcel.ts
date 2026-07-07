import * as XLSX from 'xlsx';

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
      '전화번호', '팩스', '매출기준일', '어음승인기준', '정기수금일1', '담당자', '이메일', '역할',
    ],
    sampleRow: {
      상호: '샘플거래처', 대표자: '홍길동', 사업자등록번호: '1234567890', 법인등록번호: '',
      사업장주소: '서울시', 홈페이지: '', 업태: '', 종목: '', 전화번호: '', 팩스: '',
      매출기준일: '', 어음승인기준: '', 정기수금일1: '', 담당자: '', 이메일: '', 역할: 'SALES,PURCHASE',
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
      '품목번호', '품목명', '자산분류', '단위', '규격', '표준원가', '검사구분',
      '리드타임', '안전재고', '발주간격', '최소발주량',
    ],
    sampleRow: {
      품목번호: 'ITEM-001', 품목명: '샘플품목', 자산분류: '제품', 단위: 'EA', 규격: '',
      표준원가: 1000, 검사구분: 'NONE', 리드타임: 0, 안전재고: '', 발주간격: '', 최소발주량: '',
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
      단가구분: 'SALE', 품목번호: 'ITEM-001', 사업자등록번호: '1234567890',
      시작공정코드: '', 종료공정코드: '', 발주비율: '', 표준단가: 1000, 할인단가: '',
      적용시작일: '2026-01-01', 적용종료일: '',
    },
    fileName: '단가일괄등록양식.xlsx',
    sheetName: '단가',
  },
];

function cellString(value: unknown): string {
  if (value == null) return '';
  return String(value).trim();
}

function cellNumber(value: unknown): number | undefined {
  const text = cellString(value).replace(/,/g, '');
  if (!text) return undefined;
  const parsed = Number(text);
  return Number.isFinite(parsed) ? parsed : undefined;
}

function normalizeDate(value: unknown): string {
  if (value == null || value === '') return '';
  if (value instanceof Date) return value.toISOString().slice(0, 10);
  if (typeof value === 'number') {
    const parsed = XLSX.SSF.parse_date_code(value);
    if (parsed) {
      return `${parsed.y}-${String(parsed.m).padStart(2, '0')}-${String(parsed.d).padStart(2, '0')}`;
    }
  }
  const text = cellString(value);
  if (/^\d{4}-\d{2}-\d{2}$/.test(text)) return text;
  if (/^\d{8}$/.test(text)) return `${text.slice(0, 4)}-${text.slice(4, 6)}-${text.slice(6, 8)}`;
  return text;
}

export function downloadImportTemplate(domain: ImportDomain) {
  const config = IMPORT_DOMAINS.find((d) => d.id === domain);
  if (!config) throw new Error('알 수 없는 도메인입니다.');
  const worksheet = XLSX.utils.json_to_sheet([config.sampleRow], { header: [...config.headers] });
  const workbook = XLSX.utils.book_new();
  XLSX.utils.book_append_sheet(workbook, worksheet, config.sheetName);
  XLSX.writeFile(workbook, config.fileName);
}

export function parseImportExcel(domain: ImportDomain, buffer: ArrayBuffer): Record<string, unknown>[] {
  const config = IMPORT_DOMAINS.find((d) => d.id === domain);
  if (!config) throw new Error('알 수 없는 도메인입니다.');
  const workbook = XLSX.read(buffer, { type: 'array', cellDates: true });
  const sheetName = workbook.SheetNames[0];
  if (!sheetName) throw new Error('엑셀 시트를 찾을 수 없습니다.');
  const sheet = workbook.Sheets[sheetName];
  const rawRows = XLSX.utils.sheet_to_json<Record<string, unknown>>(sheet, { defval: '' });
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
        businessRegNo: cellString(row['사업자등록번호']),
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
        roles: cellString(row['역할']),
      };
    case 'item':
      return {
        itemNo: cellString(row['품목번호']),
        itemName: cellString(row['품목명']),
        propertyClassification: cellString(row['자산분류']),
        unit: cellString(row['단위']),
        standard: cellString(row['규격']) || null,
        standardUnitCost: cellNumber(row['표준원가']) ?? null,
        checkDistinction: cellString(row['검사구분']) || 'NONE',
        leadTime: cellNumber(row['리드타임']) ?? null,
        safetyStockQuantity: cellNumber(row['안전재고']) ?? null,
        orderIntervalQuantity: cellNumber(row['발주간격']) ?? null,
        minOrderQuantity: cellNumber(row['최소발주량']) ?? null,
      };
    case 'item-composition':
      return {
        parentItemNum: cellString(row['모품목번호']),
        childItemNum: cellString(row['자품목번호']),
        parentQuantity: cellNumber(row['모품수량']),
        childQuantity: cellNumber(row['자품수량']),
      };
    case 'work-center':
      return {
        wcName: cellString(row['작업장명']),
        mainProcessSmallCode: cellString(row['대표공정코드']),
        operationTime: cellNumber(row['가동시간(분)']),
      };
    case 'process':
      return {
        itemNo: cellString(row['품목번호']),
        processSequenceNum: cellNumber(row['순서번호']),
        processSmallCode: cellString(row['공정코드']),
        workDistinction: cellString(row['작업구분']),
        workCenterName: cellString(row['작업장명']) || null,
        outsideOrderRate: cellNumber(row['발주비율']) ?? null,
        progressRate: cellNumber(row['진척비율']) ?? null,
      };
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
    case 'unit-price':
      return {
        costType: cellString(row['단가구분']),
        itemNum: cellString(row['품목번호']),
        businessRegNo: cellString(row['사업자등록번호']),
        beginProcessSmallCode: cellString(row['시작공정코드']) || null,
        endProcessSmallCode: cellString(row['종료공정코드']) || null,
        orderRate: cellNumber(row['발주비율']) ?? null,
        standardUnitCost: cellNumber(row['표준단가']),
        discountUnitCost: cellNumber(row['할인단가']) ?? null,
        beginDate: normalizeDate(row['적용시작일']),
        endDate: normalizeDate(row['적용종료일']) || null,
      };
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
      if (value === undefined || value === null || value === '') {
        errors.push({ rowNumber, message: `${field} 필수` });
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
      return ['itemNo', 'itemName', 'propertyClassification', 'unit'];
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
