import { useCallback, useEffect, useMemo, useState } from 'react';
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

function formatAmount(value: number): string {
  return value.toLocaleString(undefined, { maximumFractionDigits: 2 });
}

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

  const isEditing = editingId !== null;

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
    if (!window.confirm('작성중 수주를 취소하시겠습니까?')) {
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
      const parsed = parseSalesOrderExcel(buffer);
      setExcelPreview(parsed);
    } catch (err) {
      setExcelErrors([{ rowNumber: 0, message: err instanceof Error ? err.message : '엑셀 파싱 실패' }]);
    }
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

  return (
    <>
      <header>
        <h1>수주</h1>
        <p>
          거래처 선택 시 해당 거래처의 판매단가 품목이 바인딩되며, 단가는 수정할 수 있습니다. 건별 등록과
          엑셀 일괄 등록을 지원합니다.
        </p>
      </header>

      <section className="panel">
        <div className="form-actions" style={{ marginBottom: '1rem' }}>
          <button
            type="button"
            className={registerMode === 'single' ? 'btn-action' : 'secondary'}
            onClick={() => setRegisterMode('single')}
          >
            건별 등록
          </button>
          <button
            type="button"
            className={registerMode === 'excel' ? 'btn-action' : 'secondary'}
            onClick={() => setRegisterMode('excel')}
          >
            엑셀 일괄 등록
          </button>
        </div>

        {registerMode === 'single' ? (
          <>
            <h2>{isEditing ? '수주 수정' : '수주 등록'}</h2>
            <form onSubmit={(e) => void onSubmit(e)}>
              <div className="form-grid-wide">
                <label>
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
                <CompanySearchField
                  label="수주거래처"
                  partnerType="SALES"
                  selectedCompany={partner}
                  onSelect={setPartner}
                />
                <label>
                  수주일
                  <input
                    type="date"
                    value={orderDate}
                    onChange={(e) => setOrderDate(e.target.value)}
                    required
                  />
                </label>
                <label>
                  납기요구일
                  <input
                    type="date"
                    value={requestedDeliveryDate}
                    onChange={(e) => setRequestedDeliveryDate(e.target.value)}
                    required
                  />
                </label>
                <label>
                  비고
                  <input type="text" value={remark} onChange={(e) => setRemark(e.target.value)} />
                </label>
              </div>

              <h3>수주 품목</h3>
              {!partner && <p>수주거래처를 먼저 선택하면 판매단가 품목이 표시됩니다.</p>}
              {lines.map((line, index) => (
                <div key={`${partner?.id ?? 'none'}-${index}`} className="form-grid-wide line-block">
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
                  <label>
                    단가
                    <input
                      type="number"
                      min={0}
                      step="any"
                      value={line.unitPrice}
                      onChange={(e) => updateLine(index, { unitPrice: Number(e.target.value) })}
                    />
                  </label>
                  <label>
                    금액
                    <input
                      type="text"
                      className="readonly"
                      readOnly
                      value={formatAmount(lineAmount(line.orderQty, line.unitPrice))}
                    />
                  </label>
                  {lines.length > 1 && (
                    <button
                      type="button"
                      className="btn-action danger"
                      onClick={() => setLines(lines.filter((_, i) => i !== index))}
                    >
                      라인 삭제
                    </button>
                  )}
                </div>
              ))}

              <p>
                합계:{' '}
                {formatAmount(
                  lines.reduce((sum, line) => sum + lineAmount(line.orderQty, line.unitPrice), 0),
                )}
              </p>

              <div className="form-actions">
                <button type="button" disabled={!partner} onClick={() => setLines([...lines, emptyLine()])}>
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
            </form>
          </>
        ) : (
          <>
            <h2>엑셀 일괄 등록</h2>
            <p>
              양식을 내려받아 작성 후 업로드하세요. 동일한 수주번호는 한 건의 수주로 묶이며, 수주번호가
              비어 있으면 행마다 별도 수주가 생성됩니다.
            </p>
            <div className="form-actions">
              <button type="button" onClick={downloadSalesOrderTemplate}>
                수주 양식 다운로드
              </button>
            </div>
            <div className="form-grid-wide">
              <label>
                엑셀 파일
                <input
                  type="file"
                  accept=".xlsx,.xls"
                  onChange={(e) => void onExcelFileChange(e.target.files?.[0] ?? null)}
                />
              </label>
            </div>
            {excelPreview.length > 0 && (
              <p>미리보기: {excelPreview.length}행 (헤더 제외)</p>
            )}
            <div className="form-actions">
              <button
                type="button"
                disabled={submitting || excelPreview.length === 0}
                onClick={() => void onBulkUpload()}
              >
                {submitting ? '등록 중…' : '엑셀 일괄 등록'}
              </button>
            </div>
            {bulkMessage && <p>{bulkMessage}</p>}
            {excelErrors.length > 0 && (
              <div className="error">
                <ul>
                  {excelErrors.map((entry, index) => (
                    <li key={`${entry.rowNumber}-${index}`}>
                      {entry.rowNumber > 0 ? `${entry.rowNumber}행: ` : ''}
                      {entry.message}
                    </li>
                  ))}
                </ul>
              </div>
            )}
          </>
        )}

        {message && <p>{message}</p>}
        {error && <div className="error">{error}</div>}
      </section>

      <section className="panel">
        <h2>수주 목록</h2>
        <div className="form-grid-wide">
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
        <div className="form-actions">
          <button type="button" onClick={handleSearch}>
            검색
          </button>
          <button type="button" className="secondary" onClick={handleResetSearch}>
            초기화
          </button>
        </div>

        {listError && <div className="error">{listError}</div>}

        {loading ? (
          <p>불러오는 중…</p>
        ) : lineRows.length === 0 ? (
          <p>등록된 수주가 없습니다.</p>
        ) : (
          <table>
            <thead>
              <tr>
                <th>수주번호</th>
                <th>거래처</th>
                <th>수주일</th>
                <th>납기요구일</th>
                <th>단가</th>
                <th>수량</th>
                <th>총금액</th>
                <th>상태(생산·구매)</th>
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
                  <td>{formatAmount(row.unitPrice)}</td>
                  <td>{row.orderQty}</td>
                  <td>{formatAmount(row.amount)}</td>
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
        )}
      </section>
    </>
  );
}
