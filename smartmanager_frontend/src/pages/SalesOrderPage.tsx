import { useCallback, useEffect, useMemo, useRef, useState } from 'react';
import type { SalesOrderLineListRow, SalesOrderLineSearchParams, SalesOrderRequest } from '../api/salesOrder';
import type { SalesLineDeliveryStatus, SalesLineFulfillmentStatus } from '../api/salesOrder';
import {
  cancelSalesOrder,
  completeSalesOrderLine,
  createSalesOrder,
  createSalesOrdersBulk,
  fetchNextOrderNo,
  fetchSalesOrder,
  fetchSalesOrderLines,
  forceCompleteSalesOrderLine,
  updateSalesOrder,
} from '../api/salesOrder';
import CompanySearchField, { type CompanySearchSelection } from '../components/CompanySearchField';
import GridExcelExportButton from '../components/GridExcelExportButton';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import type { PropertyClassification } from '../api/item';
import type { PartnerPriceItem } from '../utils/unitPriceHelpers';
import { isUnitPriceEffective, toPartnerPriceItems } from '../utils/unitPriceHelpers';
import {
  buildBulkOrdersFromRows,
  buildUnitPriceLookup,
  downloadSalesOrderTemplate,
  indexCompaniesByRegNo,
  indexItemsByNo,
  parseSalesOrderExcel,
  type ParsedSalesOrderRow,
} from '../utils/salesOrderExcel';
import { fetchCompanies } from '../api/company';
import { fetchItems } from '../api/item';
import { fetchUnitPrices } from '../api/unitPrice';
import { formatAmount, formatQty } from '../utils/numberFormat';
import { useConfirm } from '../context/ConfirmContext';

const SALES_ITEM_CLASSES: PropertyClassification[] = ['상품', '제품', '공정품'];

const FULFILLMENT_STATUS_OPTIONS: { value: SalesLineFulfillmentStatus | ''; label: string }[] = [
  { value: '', label: '전체' },
  { value: 'WAITING', label: '대기' },
  { value: 'IN_PROGRESS', label: '진행' },
  { value: 'COMPLETED', label: '완료' },
  { value: 'FORCE_COMPLETED', label: '강제완료' },
];

const DELIVERY_STATUS_OPTIONS: { value: SalesLineDeliveryStatus | ''; label: string }[] = [
  { value: '', label: '전체' },
  { value: 'NOT_STARTED', label: '미납' },
  { value: 'IN_PROGRESS', label: '진행' },
  { value: 'COMPLETED', label: '완납' },
];

function lineAmount(qty: number, unitPrice: number): number {
  return Math.round(qty * unitPrice * 100) / 100;
}

type RegisterMode = 'single' | 'excel';

type LineForm = {
  itemId: number;
  orderQty: number;
  unitPrice: number;
  item?: PartnerPriceItem | null;
};

const emptyLine = (): LineForm => ({
  itemId: 0,
  orderQty: 1,
  unitPrice: 0,
  item: null,
});

function toItemSearchSelection(item: PartnerPriceItem): ItemSearchSelection {
  return {
    id: item.itemId,
    itemNo: item.itemNo,
    itemName: item.itemName,
  };
}

function lineSelectedItem(line: LineForm): ItemSearchSelection | null {
  return line.item ? toItemSearchSelection(line.item) : null;
}

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function UploadIcon() {
  return (
    <svg className="import-upload-icon" viewBox="0 0 24 24" aria-hidden="true">
      <path
        d="M12 16V4m0 0L7 9m5-5 5 5M4 20h16"
        fill="none"
        stroke="currentColor"
        strokeWidth="1.8"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  );
}

function toRequest(
  orderNo: string,
  partner: CompanySearchSelection,
  orderDate: string,
  requestedDeliveryDate: string,
  remark: string,
  lines: LineForm[],
): SalesOrderRequest {
  return {
    orderNo: orderNo.trim() || undefined,
    partnerId: partner.id,
    orderDate,
    requestedDeliveryDate,
    remark: remark || undefined,
    lines: lines.map((line) => ({
      itemId: line.itemId,
      orderQty: line.orderQty,
      unitPrice: line.unitPrice,
      deliveryDate: requestedDeliveryDate,
    })),
  };
}

export default function SalesOrderPage() {
  const confirm = useConfirm();
  const [registerMode, setRegisterMode] = useState<RegisterMode>('single');
  const [lineRows, setLineRows] = useState<SalesOrderLineListRow[]>([]);
  const [searchPartner, setSearchPartner] = useState<CompanySearchSelection | null>(null);
  const [searchItem, setSearchItem] = useState<ItemSearchSelection | null>(null);
  const [searchDeliveryFrom, setSearchDeliveryFrom] = useState('');
  const [searchDeliveryTo, setSearchDeliveryTo] = useState('');
  const [searchFulfillmentStatus, setSearchFulfillmentStatus] = useState<SalesLineFulfillmentStatus | ''>('');
  const [searchDeliveryStatus, setSearchDeliveryStatus] = useState<SalesLineDeliveryStatus | ''>('');
  const [partner, setPartner] = useState<CompanySearchSelection | null>(null);
  const [partnerPriceItems, setPartnerPriceItems] = useState<PartnerPriceItem[]>([]);
  const partnerItemOptions = useMemo(
    () => partnerPriceItems.map(toItemSearchSelection),
    [partnerPriceItems],
  );
  const [orderNo, setOrderNo] = useState('');
  const [orderNoManual, setOrderNoManual] = useState(false);
  const [orderDate, setOrderDate] = useState(todayIso());
  const [requestedDeliveryDate, setRequestedDeliveryDate] = useState('');
  const [remark, setRemark] = useState('');
  const [lines, setLines] = useState<LineForm[]>([emptyLine()]);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [listError, setListError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const [excelFile, setExcelFile] = useState<File | null>(null);
  const [excelPreview, setExcelPreview] = useState<ParsedSalesOrderRow[]>([]);
  const [excelErrors, setExcelErrors] = useState<{ rowNumber: number; message: string }[]>([]);
  const [bulkMessage, setBulkMessage] = useState<string | null>(null);
  const [excelDragOver, setExcelDragOver] = useState(false);
  const excelInputRef = useRef<HTMLInputElement>(null);

  const isEditing = editingId !== null;

  const listExportRows = useMemo(
    () =>
      lineRows.map((row) => ({
        수주번호: row.orderNo,
        거래처: row.partnerName,
        수주일: row.orderDate,
        납기요구일: row.requestedDeliveryDate ?? '',
        단가: row.unitPrice,
        수량: row.orderQty,
        총금액: row.amount,
        '이행상태': row.executionStatusLabel,
        납품상태: row.deliveryStatusLabel,
      })),
    [lineRows],
  );

  const buildSearchParams = useCallback(
    (): SalesOrderLineSearchParams => ({
      partnerId: searchPartner?.id,
      itemId: searchItem?.id,
      requestedDeliveryDateFrom: searchDeliveryFrom || undefined,
      requestedDeliveryDateTo: searchDeliveryTo || undefined,
      fulfillmentStatus: searchFulfillmentStatus || undefined,
      deliveryStatus: searchDeliveryStatus || undefined,
    }),
    [
      searchPartner,
      searchItem,
      searchDeliveryFrom,
      searchDeliveryTo,
      searchFulfillmentStatus,
      searchDeliveryStatus,
    ],
  );

  const loadLineList = useCallback(
    async (override?: SalesOrderLineSearchParams) => {
      setLoading(true);
      setListError(null);
      try {
        const params = override ?? buildSearchParams();
        setLineRows(await fetchSalesOrderLines(params));
      } catch (e) {
        setListError(e instanceof Error ? e.message : '수주 목록 조회 실패');
      } finally {
        setLoading(false);
      }
    },
    [buildSearchParams],
  );

  const handleSearch = () => {
    void loadLineList(buildSearchParams());
  };

  const handleResetSearch = () => {
    setSearchPartner(null);
    setSearchItem(null);
    setSearchDeliveryFrom('');
    setSearchDeliveryTo('');
    setSearchFulfillmentStatus('');
    setSearchDeliveryStatus('');
    void loadLineList({});
  };

  const load = loadLineList;

  const loadPartnerPriceItems = useCallback(async (companyId: number, refDate: string): Promise<PartnerPriceItem[]> => {
    const allPrices = await fetchUnitPrices('SALE');
    const filtered = allPrices.filter(
      (unitPrice) => unitPrice.companyId === companyId && isUnitPriceEffective(unitPrice, refDate),
    );
    const items = toPartnerPriceItems(filtered);
    setPartnerPriceItems(items);
    return items;
  }, []);

  const refreshNextOrderNo = useCallback(async (date: string) => {
    try {
      const nextNo = await fetchNextOrderNo(date);
      setOrderNo(nextNo);
    } catch {
      setOrderNo('');
    }
  }, []);

  useEffect(() => {
    void (async () => {
      setLoading(true);
      setListError(null);
      try {
        setLineRows(await fetchSalesOrderLines({}));
      } catch (e) {
        setListError(e instanceof Error ? e.message : '수주 목록 조회 실패');
      } finally {
        setLoading(false);
      }
    })();
  }, []);

  useEffect(() => {
    if (isEditing || orderNoManual) {
      return;
    }
    void refreshNextOrderNo(orderDate);
  }, [orderDate, isEditing, orderNoManual, refreshNextOrderNo]);

  useEffect(() => {
    if (!partner) {
      setPartnerPriceItems([]);
      setLines([emptyLine()]);
      return;
    }
    void loadPartnerPriceItems(partner.id, orderDate);
    setLines([emptyLine()]);
  }, [partner, orderDate, loadPartnerPriceItems]);

  const resetForm = () => {
    setPartner(null);
    setPartnerPriceItems([]);
    setOrderNoManual(false);
    setOrderDate(todayIso());
    setRequestedDeliveryDate('');
    setRemark('');
    setLines([emptyLine()]);
    setEditingId(null);
    void refreshNextOrderNo(todayIso());
  };

  const startEdit = async (orderId: number) => {
    try {
      const order = await fetchSalesOrder(orderId);
      if (order.status !== 'DRAFT') {
        return;
      }
      if (order.lines.some((line) => line.fulfillmentStatus !== 'WAITING')) {
        setError('생산계획 수립 또는 이행이 시작된 수주는 수정할 수 없습니다.');
        return;
      }
      setRegisterMode('single');
      setEditingId(order.id);
      setOrderNo(order.orderNo);
      setOrderNoManual(true);
      setPartner({
        id: order.partnerId,
        companyName: order.partnerName,
        businessRegNo: order.partnerBusinessRegNo,
      });
      setOrderDate(order.orderDate);
      setRequestedDeliveryDate(order.requestedDeliveryDate ?? '');
      setRemark(order.remark ?? '');
      const items = await loadPartnerPriceItems(order.partnerId, order.orderDate);
      const merged = [...items];
      for (const line of order.lines) {
        if (!merged.some((item) => item.itemId === line.itemId)) {
          merged.push({
            itemId: line.itemId,
            itemNo: line.itemNo,
            itemName: line.itemName,
            unitPrice: line.unitPrice,
          });
        }
      }
      setPartnerPriceItems(merged);
      setLines(
        order.lines.map((line) => ({
          itemId: line.itemId,
          orderQty: line.orderQty,
          unitPrice: line.unitPrice,
          item: {
            itemId: line.itemId,
            itemNo: line.itemNo,
            itemName: line.itemName,
            unitPrice: line.unitPrice,
          },
        })),
      );
      window.scrollTo({ top: 0, behavior: 'smooth' });
    } catch (err) {
      setError(err instanceof Error ? err.message : '수주 조회 실패');
    }
  };

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!partner) {
      setError('수주거래처를 선택해 주세요.');
      return;
    }
    if (!requestedDeliveryDate) {
      setError('납기요구일을 입력해 주세요.');
      return;
    }
    const validLines = lines.filter((line) => line.itemId > 0);
    if (validLines.length === 0) {
      setError('품목을 1건 이상 선택해 주세요.');
      return;
    }

    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      const body = toRequest(orderNo, partner, orderDate, requestedDeliveryDate, remark, validLines);
      if (isEditing && editingId !== null) {
        await updateSalesOrder(editingId, body);
        setMessage('수주가 수정되었습니다.');
      } else {
        const created = await createSalesOrder(body);
        setMessage(`수주가 등록되었습니다. (수주번호: ${created.orderNo})`);
      }
      resetForm();
      await load();
    } catch (err) {
      setError(err instanceof Error ? err.message : '저장 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onLineAction = async (action: () => Promise<SalesOrderLineListRow>) => {
    setSubmitting(true);
    setError(null);
    try {
      await action();
      setMessage('상태가 변경되었습니다.');
      await loadLineList();
    } catch (err) {
      setError(err instanceof Error ? err.message : '상태 변경 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancel = async (id: number) => {
    if (!(await confirm('작성중 수주를 취소하시겠습니까?', { title: '취소 확인', confirmLabel: '예, 취소', cancelLabel: '닫기', danger: true }))) {
      return;
    }
    setSubmitting(true);
    setError(null);
    try {
      await cancelSalesOrder(id);
      setMessage('수주가 취소되었습니다.');
      await load();
    } catch (err) {
      setError(err instanceof Error ? err.message : '취소 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onExcelFileChange = async (file: File | null) => {
    setExcelFile(file);
    setExcelPreview([]);
    setExcelErrors([]);
    setBulkMessage(null);
    if (!file) {
      return;
    }
    try {
      const buffer = await file.arrayBuffer();
      const parsed = await parseSalesOrderExcel(buffer);
      setExcelPreview(parsed);
    } catch (err) {
      setExcelErrors([{ rowNumber: 0, message: err instanceof Error ? err.message : '엑셀 파싱 실패' }]);
    }
  };

  const clearExcelFile = () => {
    setExcelFile(null);
    setExcelPreview([]);
    setExcelErrors([]);
    setBulkMessage(null);
    if (excelInputRef.current) {
      excelInputRef.current.value = '';
    }
  };

  const handleExcelFiles = (files: FileList | null) => {
    const file = files?.[0] ?? null;
    void onExcelFileChange(file);
  };

  const onBulkUpload = async () => {
    if (!excelFile || excelPreview.length === 0) {
      setExcelErrors([{ rowNumber: 0, message: '업로드할 엑셀 파일을 선택해 주세요.' }]);
      return;
    }

    setSubmitting(true);
    setExcelErrors([]);
    setBulkMessage(null);
    try {
      const [companies, items, unitPrices] = await Promise.all([
        fetchCompanies(),
        fetchItems(),
        fetchUnitPrices('SALE'),
      ]);

      const buildResult = buildBulkOrdersFromRows(excelPreview, {
        companiesByRegNo: indexCompaniesByRegNo(companies),
        itemsByNo: indexItemsByNo(items),
        unitPriceByPartnerItem: buildUnitPriceLookup(unitPrices),
      });

      if (buildResult.errors.length > 0) {
        setExcelErrors(buildResult.errors);
      }
      if (buildResult.orders.length === 0) {
        setBulkMessage('등록 가능한 수주가 없습니다.');
        return;
      }

      const result = await createSalesOrdersBulk(buildResult.orders);
      const serverErrors = result.failures.map((failure) => ({
        rowNumber: failure.rowIndex + 1,
        message: failure.message,
      }));
      setExcelErrors([...buildResult.errors, ...serverErrors]);
      setBulkMessage(`${result.successCount}건 등록, ${result.failureCount}건 실패`);
      if (result.successCount > 0) {
        setExcelFile(null);
        setExcelPreview([]);
        await load();
      }
    } catch (err) {
      setExcelErrors([{ rowNumber: 0, message: err instanceof Error ? err.message : '일괄 등록 실패' }]);
    } finally {
      setSubmitting(false);
    }
  };

  const updateLine = (index: number, patch: Partial<LineForm>) => {
    setLines((prev) => {
      const next = [...prev];
      next[index] = { ...next[index], ...patch };
      return next;
    });
  };

  const orderTotal = lines.reduce((sum, line) => sum + lineAmount(line.orderQty, line.unitPrice), 0);

  return (
    <div className="page">
      <header className="page-header">
        <div>
          <h1>수주</h1>
          <p>
            거래처 선택 시 해당 거래처의 판매단가 품목이 바인딩되며, 단가는 수정할 수 있습니다. 건별 등록과
            엑셀 일괄 등록을 지원합니다.
          </p>
        </div>
      </header>

      <section className="panel">
        <div className="tab-row">
          <button
            type="button"
            className={registerMode === 'single' ? 'tab-active' : ''}
            onClick={() => setRegisterMode('single')}
          >
            건별 등록
          </button>
          <button
            type="button"
            className={registerMode === 'excel' ? 'tab-active' : ''}
            onClick={() => setRegisterMode('excel')}
          >
            엑셀 일괄 등록
          </button>
        </div>

        {registerMode === 'single' ? (
          <form className="sales-order-form" onSubmit={(e) => void onSubmit(e)}>
            <div className="ui-section">
              <h2>{isEditing ? '수주 수정' : '수주 등록'}</h2>
              <div className="sales-order-header-grid">
                <label className="sales-order-field-order-no">
                  수주번호
                  <input
                    type="text"
                    className={isEditing ? 'readonly' : undefined}
                    value={orderNo}
                    readOnly={isEditing}
                    onChange={(e) => {
                      setOrderNoManual(true);
                      setOrderNo(e.target.value);
                    }}
                    placeholder="비우면 자동 채번"
                  />
                </label>
                <div className="sales-order-field-partner">
                  <CompanySearchField
                    label="수주거래처"
                    partnerType="SALES"
                    selectedCompany={partner}
                    onSelect={setPartner}
                  />
                </div>
                <label className="sales-order-field-date">
                  수주일
                  <input
                    type="date"
                    value={orderDate}
                    onChange={(e) => setOrderDate(e.target.value)}
                    required
                  />
                </label>
                <label className="sales-order-field-date">
                  납기요구일
                  <input
                    type="date"
                    value={requestedDeliveryDate}
                    onChange={(e) => setRequestedDeliveryDate(e.target.value)}
                    required
                  />
                </label>
                <label className="sales-order-field-full">
                  비고
                  <input type="text" value={remark} onChange={(e) => setRemark(e.target.value)} />
                </label>
              </div>
            </div>

            <div className="ui-section">
              <h3>수주 품목</h3>
              {!partner && (
                <p className="hint-text sales-order-hint">
                  수주거래처를 먼저 선택하면 판매단가 품목이 표시됩니다.
                </p>
              )}
              {lines.map((line, index) => (
                <div key={`${partner?.id ?? 'none'}-${index}`} className="ui-line-card line-block">
                  <div className="sales-order-line-grid">
                    <ItemSearchField
                      label={`품목 ${index + 1}`}
                      items={partnerItemOptions}
                      selectedItem={lineSelectedItem(line)}
                      disabled={!partner}
                      onSelect={(item) => {
                        if (!item) {
                          updateLine(index, {
                            item: null,
                            itemId: 0,
                            unitPrice: 0,
                          });
                          return;
                        }
                        const priceItem = partnerPriceItems.find((p) => p.itemId === item.id);
                        updateLine(index, {
                          item: priceItem ?? {
                            itemId: item.id,
                            itemNo: item.itemNo,
                            itemName: item.itemName,
                            unitPrice: 0,
                          },
                          itemId: item.id,
                          unitPrice: priceItem?.unitPrice ?? 0,
                        });
                      }}
                    />
                    <label>
                      수주수량
                      <input
                        type="number"
                        min={0.0001}
                        step="any"
                        value={line.orderQty}
                        onChange={(e) => updateLine(index, { orderQty: Number(e.target.value) })}
                        required
                      />
                    </label>
                    <label className="sales-order-money-field">
                      단가
                      <input
                        type="number"
                        className="sales-order-money-input"
                        min={0}
                        step="any"
                        value={line.unitPrice}
                        onChange={(e) => updateLine(index, { unitPrice: Number(e.target.value) })}
                      />
                    </label>
                    <label className="sales-order-money-field">
                      금액
                      <input
                        type="text"
                        className="readonly sales-order-money-display"
                        readOnly
                        value={formatAmount(lineAmount(line.orderQty, line.unitPrice))}
                      />
                    </label>
                    {lines.length > 1 && (
                      <button
                        type="button"
                        className="btn-action danger sales-order-line-remove"
                        onClick={() => setLines(lines.filter((_, i) => i !== index))}
                      >
                        라인 삭제
                      </button>
                    )}
                  </div>
                </div>
              ))}

              <div className="sales-order-footer">
                <p className="sales-order-total">
                  합계 <span>{formatAmount(orderTotal)}</span>
                </p>
                <div className="form-actions sales-order-form-actions">
                  <button type="button" className="secondary" disabled={!partner} onClick={() => setLines([...lines, emptyLine()])}>
                    라인 추가
                  </button>
                  <button type="submit" disabled={submitting}>
                    {submitting ? '저장 중…' : isEditing ? '수정 저장' : '등록'}
                  </button>
                  {isEditing && (
                    <button type="button" className="secondary" onClick={resetForm}>
                      취소
                    </button>
                  )}
                </div>
              </div>
            </div>
          </form>
        ) : (
          <div className="ui-section sales-order-excel-section">
            <div className="import-card__header sales-order-excel-header">
              <h2 className="sales-order-excel-title">엑셀 일괄 등록</h2>
              <button
                type="button"
                className="import-template-btn"
                disabled={submitting}
                onClick={() => void downloadSalesOrderTemplate()}
              >
                ↓ 수주 양식 다운로드
              </button>
            </div>
            <p className="hint-text sales-order-excel-hint">
              양식을 내려받아 작성 후 업로드하세요. 동일한 수주번호는 한 건의 수주로 묶이며, 수주번호가
              비어 있으면 행마다 별도 수주가 생성됩니다.
            </p>

            <div
              className={`import-dropzone${excelDragOver ? ' import-dropzone--active' : ''}${excelFile ? ' import-dropzone--filled' : ''}`}
              role="button"
              tabIndex={0}
              onClick={() => !submitting && excelInputRef.current?.click()}
              onKeyDown={(e) => {
                if (!submitting && (e.key === 'Enter' || e.key === ' ')) {
                  e.preventDefault();
                  excelInputRef.current?.click();
                }
              }}
              onDragOver={(e) => {
                e.preventDefault();
                if (!submitting) {
                  setExcelDragOver(true);
                }
              }}
              onDragLeave={() => setExcelDragOver(false)}
              onDrop={(e) => {
                e.preventDefault();
                setExcelDragOver(false);
                if (!submitting) {
                  handleExcelFiles(e.dataTransfer.files);
                }
              }}
            >
              <input
                ref={excelInputRef}
                type="file"
                accept=".xlsx,.xls"
                hidden
                disabled={submitting}
                onChange={(e) => handleExcelFiles(e.target.files)}
              />
              <UploadIcon />
              {excelFile ? (
                <div className="import-dropzone__file">
                  <strong>{excelFile.name}</strong>
                  <span>{excelPreview.length}행</span>
                  {!submitting && (
                    <button
                      type="button"
                      className="import-clear-btn"
                      onClick={(e) => {
                        e.stopPropagation();
                        clearExcelFile();
                      }}
                    >
                      제거
                    </button>
                  )}
                </div>
              ) : (
                <>
                  <p className="import-dropzone__title">수주 엑셀 업로드</p>
                  <p className="import-dropzone__hint">클릭하거나 파일을 여기로 드래그하세요 (.xlsx, .xls)</p>
                </>
              )}
            </div>

            {excelPreview.length > 0 && (
              <p className="hint-text sales-order-excel-preview">미리보기: {excelPreview.length}행 (헤더 제외)</p>
            )}
            <div className="form-actions sales-order-form-actions">
              <button
                type="button"
                disabled={submitting || excelPreview.length === 0}
                onClick={() => void onBulkUpload()}
              >
                {submitting ? '등록 중…' : '엑셀 일괄 등록'}
              </button>
            </div>
            {bulkMessage && <p className="success-banner">{bulkMessage}</p>}
            {excelErrors.length > 0 && (
              <ul className="import-card__errors">
                {excelErrors.map((entry, index) => (
                  <li key={`${entry.rowNumber}-${index}`}>
                    {entry.rowNumber > 0 ? `${entry.rowNumber}행: ` : ''}
                    {entry.message}
                  </li>
                ))}
              </ul>
            )}
          </div>
        )}

        {message && <p className="success-banner sales-order-feedback">{message}</p>}
        {error && <div className="error sales-order-feedback">{error}</div>}
      </section>

      <section className="panel">
        <div className="panel-header-row">
          <h2>수주 목록</h2>
          <GridExcelExportButton fileBaseName="수주목록" disabled={loading} rows={listExportRows} />
        </div>
        <div className="ui-filter-panel">
          <div className="sales-order-search-grid">
            <CompanySearchField
              label="거래처"
              partnerType="SALES"
              selectedCompany={searchPartner}
              onSelect={setSearchPartner}
            />
            <ItemSearchField
              label="품목"
              allowedClassifications={SALES_ITEM_CLASSES}
              selectedItem={searchItem}
              onSelect={setSearchItem}
            />
            <label>
              납기요구일(부터)
              <input
                type="date"
                value={searchDeliveryFrom}
                onChange={(e) => setSearchDeliveryFrom(e.target.value)}
              />
            </label>
            <label>
              납기요구일(까지)
              <input
                type="date"
                value={searchDeliveryTo}
                onChange={(e) => setSearchDeliveryTo(e.target.value)}
              />
            </label>
            <label>
              이행상태
              <select
                value={searchFulfillmentStatus}
                onChange={(e) => setSearchFulfillmentStatus(e.target.value as SalesLineFulfillmentStatus | '')}
              >
                {FULFILLMENT_STATUS_OPTIONS.map((option) => (
                  <option key={option.value || 'all'} value={option.value}>
                    {option.label}
                  </option>
                ))}
              </select>
            </label>
            <label>
              납품상태
              <select
                value={searchDeliveryStatus}
                onChange={(e) => setSearchDeliveryStatus(e.target.value as SalesLineDeliveryStatus | '')}
              >
                {DELIVERY_STATUS_OPTIONS.map((option) => (
                  <option key={option.value || 'all'} value={option.value}>
                    {option.label}
                  </option>
                ))}
              </select>
            </label>
          </div>
          <div className="form-actions sales-order-search-actions">
            <button type="button" onClick={handleSearch}>
              검색
            </button>
            <button type="button" className="secondary" onClick={handleResetSearch}>
              초기화
            </button>
          </div>
        </div>

        {listError && <div className="error">{listError}</div>}

        {loading ? (
          <p className="hint-text">불러오는 중…</p>
        ) : lineRows.length === 0 ? (
          <p className="ui-empty">등록된 수주가 없습니다.</p>
        ) : (
          <div className="ui-table-wrap">
            <table>
              <thead>
                <tr>
                  <th>수주번호</th>
                  <th>거래처</th>
                  <th>수주일</th>
                  <th>납기요구일</th>
                  <th className="num">단가</th>
                  <th className="num">수량</th>
                  <th className="num">총금액</th>
                  <th>이행상태</th>
                  <th>납품상태</th>
                  <th>관리</th>
                </tr>
              </thead>
              <tbody>
                {lineRows.map((row) => (
                  <tr key={row.lineId}>
                    <td>{row.orderNo}</td>
                    <td>{row.partnerName}</td>
                    <td>{row.orderDate}</td>
                    <td>{row.requestedDeliveryDate ?? '-'}</td>
                    <td className="num">{formatAmount(row.unitPrice)}</td>
                    <td className="num">{formatQty(row.orderQty)}</td>
                    <td className="num sales-order-amount">{formatAmount(row.amount)}</td>
                    <td>{row.executionStatusLabel}</td>
                    <td>{row.deliveryStatusLabel}</td>
                    <td className="actions">
                      {row.orderStatus === 'DRAFT' && row.orderEditable && (
                        <>
                          <button type="button" className="btn-action" onClick={() => void startEdit(row.orderId)}>
                            수정
                          </button>
                          <button type="button" className="btn-action danger" onClick={() => void onCancel(row.orderId)}>
                            취소
                          </button>
                        </>
                      )}
                      {row.orderStatus === 'CONFIRMED' && row.fulfillmentStatus === 'IN_PROGRESS' && (
                        <button
                          type="button"
                          className="btn-action"
                          onClick={() => void onLineAction(() => completeSalesOrderLine(row.lineId))}
                        >
                          완료
                        </button>
                      )}
                      {row.deliveryStatus !== 'COMPLETED' && (
                        <button
                          type="button"
                          className="btn-action"
                          onClick={() => void onLineAction(() => forceCompleteSalesOrderLine(row.lineId))}
                        >
                          강제완료
                        </button>
                      )}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </section>
    </div>
  );
}
