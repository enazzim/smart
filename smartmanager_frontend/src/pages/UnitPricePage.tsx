import { useCallback, useEffect, useMemo, useState } from 'react';
import type { CompanyRoleType } from '../api/company';
import type { PropertyClassification } from '../api/item';
import type { CodeOption } from '../api/process';
import { fetchProcessCodeOptions } from '../api/process';
import { ALL_ITEM_CLASSES } from '../utils/itemClassFilters';
import type {
  CostType,
  CreateUnitPriceRequest,
  UnitPrice,
  UnitPriceHistory,
  UnitPriceHistorySearchParams,
  UpdateUnitPriceRequest,
} from '../api/unitPrice';
import {
  createUnitPrice,
  deleteUnitPrice,
  fetchAllUnitPriceHistory,
  fetchUnitPriceHistory,
  fetchUnitPrices,
  updateUnitPrice,
} from '../api/unitPrice';
import CompanySearchField, { type CompanySearchSelection } from '../components/CompanySearchField';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import GridExcelExportButton from '../components/GridExcelExportButton';
import VirtualMasterTable from '../components/VirtualMasterTable';
import { formatAmount, formatQty } from '../utils/numberFormat';
import { useConfirm } from '../context/ConfirmContext';

const ALL_HISTORY_PARTNER_ROLES: readonly CompanyRoleType[] = ['SALES', 'PURCHASE', 'OUTSOURCE'];
const ALL_HISTORY_ITEM_CLASSES: PropertyClassification[] = ['제품', '상품', '공정품', '원자재', '부자재'];

type HistoryCostTypeFilter = CostType | '';

type AllHistoryFilters = {
  costType: HistoryCostTypeFilter;
  company: CompanySearchSelection | null;
  item: ItemSearchSelection | null;
  changedFrom: string;
  changedTo: string;
  changedBy: string;
};

function emptyAllHistoryFilters(costType: HistoryCostTypeFilter = ''): AllHistoryFilters {
  return {
    costType,
    company: null,
    item: null,
    changedFrom: '',
    changedTo: '',
    changedBy: '',
  };
}

function toHistorySearchParams(filters: AllHistoryFilters): UnitPriceHistorySearchParams {
  return {
    type: filters.costType || undefined,
    companyId: filters.company?.id,
    itemId: filters.item?.id,
    changedFrom: filters.changedFrom || undefined,
    changedTo: filters.changedTo || undefined,
    changedBy: filters.changedBy.trim() || undefined,
  };
}

const TAB_CONFIG: {
  type: CostType;
  label: string;
  partnerType: CompanyRoleType;
  itemClasses: PropertyClassification[];
  showProcess: boolean;
  orderRateDisabled: boolean;
}[] = [
  {
    type: 'SALE',
    label: '판매단가',
    partnerType: 'SALES',
    itemClasses: ['제품', '상품', '공정품'],
    showProcess: false,
    orderRateDisabled: true,
  },
  {
    type: 'PURCHASE',
    label: '구매단가',
    partnerType: 'PURCHASE',
    itemClasses: ['원자재', '상품', '부자재'],
    showProcess: false,
    orderRateDisabled: false,
  },
  {
    type: 'OUTSOURCE',
    label: '외주단가',
    partnerType: 'OUTSOURCE',
    itemClasses: ['제품', '공정품'],
    showProcess: true,
    orderRateDisabled: false,
  },
];

const today = new Date().toISOString().slice(0, 10);

function costTypeLabel(type: CostType) {
  switch (type) {
    case 'SALE':
      return '판매단가';
    case 'PURCHASE':
      return '구매단가';
    case 'OUTSOURCE':
      return '외주단가';
    default:
      return type;
  }
}

function formatDateTime(value: string) {
  return new Date(value).toLocaleString('ko-KR');
}

function duplicateHistoryActorNames(rows: UnitPriceHistory[]): Set<string> {
  const idsByName = new Map<string, Set<string>>();
  for (const row of rows) {
    const name = row.changedBy?.trim();
    if (!name) {
      continue;
    }
    const ids = idsByName.get(name) ?? new Set<string>();
    ids.add(row.changedById?.trim() ?? '');
    idsByName.set(name, ids);
  }
  const duplicates = new Set<string>();
  for (const [name, ids] of idsByName) {
    if (ids.size > 1) {
      duplicates.add(name);
    }
  }
  return duplicates;
}

function formatHistoryActor(row: UnitPriceHistory, duplicateNames: Set<string>): string {
  const name = row.changedBy?.trim();
  if (!name) {
    return '—';
  }
  const loginId = row.changedById?.trim();
  if (loginId && duplicateNames.has(name)) {
    return `${name} (${loginId})`;
  }
  return name;
}

function emptyForm(tab: CostType): CreateUnitPriceRequest {
  return {
    type: tab,
    itemNum: '',
    companyId: 0,
    orderRate: tab === 'SALE' ? 0 : 100,
    standardUnitCost: 0,
    beginDate: today,
  };
}

function toForm(price: UnitPrice): CreateUnitPriceRequest & { updateReason?: string } {
  return {
    type: price.type,
    itemNum: price.itemNum,
    companyId: price.companyId,
    beginProcessCodeId: price.beginProcessCodeId ?? undefined,
    endProcessCodeId: price.endProcessCodeId ?? undefined,
    orderRate: price.orderRate,
    standardUnitCost: price.standardUnitCost,
    discountUnitCost: price.discountUnitCost ?? undefined,
    beginDate: price.beginDate,
    endDate: price.endDate ?? undefined,
    updateReason: '',
  };
}

export default function UnitPricePage() {
  const confirm = useConfirm();
  const [activeTab, setActiveTab] = useState<CostType>('SALE');
  const tabConfig = TAB_CONFIG.find((tab) => tab.type === activeTab)!;

  const [processCodes, setProcessCodes] = useState<CodeOption[]>([]);
  const [prices, setPrices] = useState<UnitPrice[]>([]);
  const [draftItem, setDraftItem] = useState<ItemSearchSelection | null>(null);
  const [draftCompany, setDraftCompany] = useState<CompanySearchSelection | null>(null);
  const [appliedItem, setAppliedItem] = useState<ItemSearchSelection | null>(null);
  const [appliedCompany, setAppliedCompany] = useState<CompanySearchSelection | null>(null);
  const [hasSearched, setHasSearched] = useState(false);
  const [clearToken, setClearToken] = useState(0);
  const [formItem, setFormItem] = useState<ItemSearchSelection | null>(null);
  const [formCompany, setFormCompany] = useState<CompanySearchSelection | null>(null);
  const [form, setForm] = useState<CreateUnitPriceRequest & { updateReason?: string }>(emptyForm('SALE'));
  const [editingId, setEditingId] = useState<number | null>(null);
  const [loading, setLoading] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [historyOpen, setHistoryOpen] = useState(false);
  const [historyMode, setHistoryMode] = useState<'single' | 'all'>('single');
  const [historyLoading, setHistoryLoading] = useState(false);
  const [historyError, setHistoryError] = useState<string | null>(null);
  const [historyRows, setHistoryRows] = useState<UnitPriceHistory[]>([]);
  const [historyTitle, setHistoryTitle] = useState('');
  const [allHistoryFilters, setAllHistoryFilters] = useState<AllHistoryFilters>(() => emptyAllHistoryFilters());
  const [historyFilterClearToken, setHistoryFilterClearToken] = useState(0);
  const [hasAllHistorySearched, setHasAllHistorySearched] = useState(false);

  const isEditing = editingId !== null;

  const historyPartnerRoles = useMemo((): readonly CompanyRoleType[] => {
    switch (allHistoryFilters.costType) {
      case 'SALE':
        return ['SALES'];
      case 'PURCHASE':
        return ['PURCHASE'];
      case 'OUTSOURCE':
        return ['OUTSOURCE'];
      default:
        return ALL_HISTORY_PARTNER_ROLES;
    }
  }, [allHistoryFilters.costType]);

  const historyItemClasses = useMemo((): PropertyClassification[] => {
    const matched = TAB_CONFIG.find((tab) => tab.type === allHistoryFilters.costType);
    return matched?.itemClasses ?? ALL_HISTORY_ITEM_CLASSES;
  }, [allHistoryFilters.costType]);

  const historyActorDuplicateNames = useMemo(
    () => duplicateHistoryActorNames(historyRows),
    [historyRows],
  );

  const historyExportRows = useMemo(
    () =>
      historyRows.map((row) => ({
        품목번호: row.itemNo,
        품목명: row.itemName,
        변경사유: row.updateReason,
        단가구분: costTypeLabel(row.type),
        거래처명: row.companyName,
        시작공정: row.beginProcessName ?? '',
        종료공정: row.endProcessName ?? '',
        기준단가: Number(row.standardUnitCost),
        적용시작일: row.beginDate,
        수정자: formatHistoryActor(row, historyActorDuplicateNames),
        수정일: formatDateTime(row.changedAt),
      })),
    [historyRows, historyActorDuplicateNames],
  );

  const displayedPrices = useMemo(() => {
    if (!hasSearched) {
      return [];
    }
    let rows = prices;
    if (appliedItem) {
      rows = rows.filter((price) => price.itemId === appliedItem.id);
    }
    if (appliedCompany) {
      rows = rows.filter((price) => price.companyId === appliedCompany.id);
    }
    return rows;
  }, [prices, appliedItem, appliedCompany, hasSearched]);

  const unitPriceExportRows = useMemo(
    () =>
      displayedPrices.map((price) => {
        const row: Record<string, string | number> = {
          품목번호: price.itemNum,
          품목명: price.itemName,
          거래처: price.companyName,
        };
        if (tabConfig.showProcess) {
          row['시작공정'] = price.processName ?? '';
          row['종료공정'] = price.endProcessName ?? '';
        }
        if (!tabConfig.orderRateDisabled) {
          row['발주비율'] = `${price.orderRate}%`;
        }
        row['기준단가'] = price.standardUnitCost;
        row['적용기간'] = price.endDate ? `${price.beginDate} ~ ${price.endDate}` : `${price.beginDate} ~`;
        return row;
      }),
    [displayedPrices, tabConfig],
  );

  useEffect(() => {
    void fetchProcessCodeOptions().then(setProcessCodes).catch(() => setProcessCodes([]));
  }, []);

  const onSearch = async () => {
    const listQuery = draftItem?.itemNo ?? draftCompany?.companyName ?? '';
    setLoading(true);
    setError(null);
    try {
      setPrices(await fetchUnitPrices(activeTab, listQuery));
      setAppliedItem(draftItem);
      setAppliedCompany(draftCompany);
      setHasSearched(true);
    } catch (e) {
      setError(e instanceof Error ? e.message : '목록 조회 실패');
      setPrices([]);
    } finally {
      setLoading(false);
    }
  };

  const onResetSearch = () => {
    setDraftItem(null);
    setDraftCompany(null);
    setAppliedItem(null);
    setAppliedCompany(null);
    setPrices([]);
    setHasSearched(false);
    setClearToken((token) => token + 1);
    setError(null);
  };

  const refreshListIfSearched = useCallback(async () => {
    if (!hasSearched) {
      return;
    }
    const listQuery = appliedItem?.itemNo ?? appliedCompany?.companyName ?? '';
    setLoading(true);
    setError(null);
    try {
      setPrices(await fetchUnitPrices(activeTab, listQuery));
    } catch (e) {
      setError(e instanceof Error ? e.message : '목록 조회 실패');
    } finally {
      setLoading(false);
    }
  }, [hasSearched, activeTab, appliedItem?.itemNo, appliedCompany?.companyName]);

  const resetForm = () => {
    setForm(emptyForm(activeTab));
    setFormItem(null);
    setFormCompany(null);
    setEditingId(null);
  };

  const switchTab = (type: CostType) => {
    setActiveTab(type);
    resetForm();
    setDraftItem(null);
    setDraftCompany(null);
    setAppliedItem(null);
    setAppliedCompany(null);
    setPrices([]);
    setHasSearched(false);
    setClearToken((token) => token + 1);
  };

  const startEdit = (price: UnitPrice) => {
    setEditingId(price.id);
    setForm(toForm(price));
    setFormItem({
      id: price.itemId,
      itemNo: price.itemNum,
      itemName: price.itemName,
    });
    setFormCompany({
      id: price.companyId,
      companyName: price.companyName,
      businessRegNo: price.businessRegistrationNum,
    });
    setError(null);
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  const openHistory = async (unitPriceId: number, label: string) => {
    setHistoryMode('single');
    setHistoryOpen(true);
    setHistoryLoading(true);
    setHistoryError(null);
    setHistoryRows([]);
    setHistoryTitle(label);
    try {
      setHistoryRows(await fetchUnitPriceHistory(unitPriceId));
    } catch (e) {
      setHistoryError(e instanceof Error ? e.message : '이력을 불러오지 못했습니다.');
    } finally {
      setHistoryLoading(false);
    }
  };

  const searchAllHistory = async (filters: AllHistoryFilters) => {
    setHasAllHistorySearched(true);
    setHistoryLoading(true);
    setHistoryError(null);
    try {
      setHistoryRows(await fetchAllUnitPriceHistory(toHistorySearchParams(filters)));
    } catch (e) {
      setHistoryError(e instanceof Error ? e.message : '이력을 불러오지 못했습니다.');
      setHistoryRows([]);
    } finally {
      setHistoryLoading(false);
    }
  };

  const openAllHistory = () => {
    const initial = emptyAllHistoryFilters(activeTab);
    setHistoryMode('all');
    setAllHistoryFilters(initial);
    setHistoryFilterClearToken((token) => token + 1);
    setHistoryOpen(true);
    setHistoryRows([]);
    setHistoryError(null);
    setHistoryLoading(false);
    setHasAllHistorySearched(false);
    setHistoryTitle('전체');
  };

  const onAllHistorySearch = (e: React.FormEvent) => {
    e.preventDefault();
    void searchAllHistory(allHistoryFilters);
  };

  const onAllHistoryReset = () => {
    const cleared = emptyAllHistoryFilters();
    setAllHistoryFilters(cleared);
    setHistoryFilterClearToken((token) => token + 1);
  };

  const closeHistory = () => {
    setHistoryOpen(false);
    setHistoryMode('single');
    setHistoryError(null);
    setHistoryRows([]);
    setHistoryTitle('');
    setHasAllHistorySearched(false);
    setAllHistoryFilters(emptyAllHistoryFilters());
  };

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!formItem) {
      setError('품목을 선택해 주세요.');
      return;
    }
    if (!isEditing && !formCompany) {
      setError('거래처를 선택해 주세요.');
      return;
    }
    if (tabConfig.showProcess && (!form.beginProcessCodeId || !form.endProcessCodeId)) {
      setError('시작·종료 공정을 선택해 주세요.');
      return;
    }

    setSubmitting(true);
    setError(null);
    try {
      if (isEditing && editingId !== null) {
        const payload: UpdateUnitPriceRequest = {
          orderRate: tabConfig.orderRateDisabled ? 0 : (form.orderRate ?? 0),
          standardUnitCost: form.standardUnitCost,
          discountUnitCost: form.discountUnitCost,
          beginDate: form.beginDate,
          endDate: form.endDate,
          updateReason: form.updateReason?.trim() ?? '',
        };
        await updateUnitPrice(editingId, payload);
      } else {
        await createUnitPrice({
          type: activeTab,
          itemNum: formItem.itemNo,
          companyId: formCompany!.id,
          beginProcessCodeId: form.beginProcessCodeId,
          endProcessCodeId: form.endProcessCodeId,
          orderRate: tabConfig.orderRateDisabled ? 0 : form.orderRate,
          standardUnitCost: form.standardUnitCost,
          discountUnitCost: form.discountUnitCost,
          beginDate: form.beginDate,
          endDate: form.endDate,
        });
      }
      resetForm();
      await refreshListIfSearched();
    } catch (err) {
      setError(err instanceof Error ? err.message : '저장 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async (id: number) => {
    if (!(await confirm('이 단가를 삭제하시겠습니까?', { title: '삭제 확인', confirmLabel: '삭제', cancelLabel: '닫기', danger: true }))) return;
    setError(null);
    try {
      await deleteUnitPrice(id);
      if (editingId === id) resetForm();
      await refreshListIfSearched();
    } catch (err) {
      setError(err instanceof Error ? err.message : '삭제 실패');
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>단가</h1>
        <div className="tab-row">
          {TAB_CONFIG.map((tab) => (
            <button
              key={tab.type}
              type="button"
              className={activeTab === tab.type ? 'tab-active' : undefined}
              onClick={() => switchTab(tab.type)}
            >
              {tab.label}
            </button>
          ))}
        </div>
      </header>

      {error && <div className="error">{error}</div>}

      <section className="panel">
        <h2>{isEditing ? '단가 수정' : '단가 등록'}</h2>
        <form className="form-grid form-grid-wide" onSubmit={onSubmit}>
          <ItemSearchField
            label="품목 *"
            selectedItem={formItem}
            onSelect={setFormItem}
            allowedClassifications={tabConfig.itemClasses}
            disabled={isEditing}
          />
          <CompanySearchField
            label="거래처"
            partnerType={tabConfig.partnerType}
            selectedCompany={formCompany}
            onSelect={setFormCompany}
          />
          {tabConfig.showProcess && (
            <>
              <label>
                시작공정
                <select
                  value={form.beginProcessCodeId ?? ''}
                  onChange={(e) =>
                    setForm((prev) => ({
                      ...prev,
                      beginProcessCodeId: e.target.value ? Number(e.target.value) : undefined,
                    }))
                  }
                >
                  <option value="">선택</option>
                  {processCodes.map((code) => (
                    <option key={code.id} value={code.id}>
                      {code.code} — {code.name}
                    </option>
                  ))}
                </select>
              </label>
              <label>
                종료공정
                <select
                  value={form.endProcessCodeId ?? ''}
                  onChange={(e) =>
                    setForm((prev) => ({
                      ...prev,
                      endProcessCodeId: e.target.value ? Number(e.target.value) : undefined,
                    }))
                  }
                >
                  <option value="">선택</option>
                  {processCodes.map((code) => (
                    <option key={code.id} value={code.id}>
                      {code.code} — {code.name}
                    </option>
                  ))}
                </select>
              </label>
            </>
          )}
          {!tabConfig.orderRateDisabled && (
            <label>
              발주비율 (%)
              <input
                type="number"
                min={0}
                max={100}
                step="0.01"
                value={form.orderRate ?? ''}
                onChange={(e) =>
                  setForm((prev) => ({ ...prev, orderRate: Number(e.target.value) }))
                }
              />
            </label>
          )}
          <label>
            기준단가
            <input
              type="number"
              min={0}
              step="0.0001"
              required
              value={form.standardUnitCost}
              onChange={(e) =>
                setForm((prev) => ({ ...prev, standardUnitCost: Number(e.target.value) }))
              }
            />
          </label>
          <label>
            할인단가
            <input
              type="number"
              min={0}
              step="0.0001"
              value={form.discountUnitCost ?? ''}
              onChange={(e) =>
                setForm((prev) => ({
                  ...prev,
                  discountUnitCost: e.target.value === '' ? undefined : Number(e.target.value),
                }))
              }
            />
          </label>
          <label>
            적용시작일
            <input
              type="date"
              required
              value={form.beginDate}
              onChange={(e) => setForm((prev) => ({ ...prev, beginDate: e.target.value }))}
            />
          </label>
          <label>
            적용종료일
            <input
              type="date"
              value={form.endDate ?? ''}
              onChange={(e) =>
                setForm((prev) => ({
                  ...prev,
                  endDate: e.target.value === '' ? undefined : e.target.value,
                }))
              }
            />
          </label>
          {isEditing && (
            <div className="unit-price-reason-row full-width">
              <label>
                변경 사유
                <input
                  type="text"
                  required
                  value={form.updateReason ?? ''}
                  onChange={(e) => setForm((prev) => ({ ...prev, updateReason: e.target.value }))}
                  placeholder="수정 사유를 입력하세요"
                />
              </label>
              <button
                type="button"
                className="secondary"
                disabled={editingId == null}
                onClick={() =>
                  void openHistory(
                    editingId!,
                    `${formItem?.itemNo ?? ''} ${formItem?.itemName ?? ''}`.trim() || tabConfig.label,
                  )
                }
              >
                이력보기
              </button>
            </div>
          )}
          <div className="form-actions full-width">
            <button type="submit" disabled={submitting}>
              {submitting ? '저장 중…' : isEditing ? '수정' : '등록'}
            </button>
            {isEditing && (
              <button type="button" className="secondary" onClick={resetForm}>
                취소
              </button>
            )}
          </div>
        </form>
        {isEditing && <p className="item-search-hint">품목·거래처·공정구간은 수정할 수 없습니다.</p>}
      </section>

      <section className="panel">
        <div className="panel-header-row">
          <h2>{tabConfig.label} 목록</h2>
          <div className="form-actions" style={{ margin: 0 }}>
            <button type="button" className="secondary" disabled={loading} onClick={openAllHistory}>
              전체 이력
            </button>
            <GridExcelExportButton
              fileBaseName={`${tabConfig.label}목록`}
              sheetName={tabConfig.label}
              disabled={loading}
              rows={unitPriceExportRows}
            />
          </div>
        </div>
        <div className="search-row">
          <CompanySearchField
            label="거래처 (선택)"
            selectedCompany={draftCompany}
            onSelect={setDraftCompany}
            clearToken={clearToken}
            placeholder="상호 또는 사업자번호 입력"
          />
          <ItemSearchField
            label="품목 (선택)"
            selectedItem={draftItem}
            onSelect={setDraftItem}
            allowedClassifications={ALL_ITEM_CLASSES}
            clearToken={clearToken}
            placeholder="품목번호 또는 품목명 입력"
          />
          <button type="button" disabled={loading} onClick={() => void onSearch()}>
            조회
          </button>
          <button type="button" className="secondary" disabled={loading} onClick={onResetSearch}>
            초기화
          </button>
        </div>
        {!hasSearched ? (
          <p className="hint-text">조회 버튼을 누르면 목록이 표시됩니다.</p>
        ) : loading ? (
          <p>불러오는 중…</p>
        ) : prices.length === 0 ? (
          <p>등록된 단가가 없습니다.</p>
        ) : displayedPrices.length === 0 ? (
          <p>검색 조건에 맞는 단가가 없습니다.</p>
        ) : (
          <VirtualMasterTable
            rows={displayedPrices}
            columnCount={
              5 + (tabConfig.showProcess ? 2 : 0) + (tabConfig.orderRateDisabled ? 0 : 1)
            }
            rowHeight={56}
            getRowKey={(price) => price.id}
            renderHeader={() => (
              <tr>
                <th>품목</th>
                <th>거래처</th>
                {tabConfig.showProcess && (
                  <>
                    <th>시작공정</th>
                    <th>종료공정</th>
                  </>
                )}
                {!tabConfig.orderRateDisabled && <th className="num">발주비율</th>}
                <th className="num">기준단가</th>
                <th>적용기간</th>
                <th>작업</th>
              </tr>
            )}
            renderRow={(price) => (
              <tr>
                <td>
                  {price.itemNum}
                  <br />
                  <small>{price.itemName}</small>
                </td>
                <td>{price.companyName}</td>
                {tabConfig.showProcess && (
                  <>
                    <td>{price.processName ?? '—'}</td>
                    <td>{price.endProcessName ?? '—'}</td>
                  </>
                )}
                {!tabConfig.orderRateDisabled && (
                  <td className="num">{formatQty(price.orderRate)}%</td>
                )}
                <td className="num">{formatAmount(price.standardUnitCost)}</td>
                <td>
                  {price.beginDate}
                  {price.endDate ? ` ~ ${price.endDate}` : ' ~'}
                </td>
                <td className="actions">
                  <button type="button" className="btn-action" onClick={() => startEdit(price)}>
                    수정
                  </button>
                  <button
                    type="button"
                    className="btn-action"
                    onClick={() =>
                      void openHistory(price.id, `${price.itemNum} ${price.itemName}`.trim())
                    }
                  >
                    이력
                  </button>
                  <button type="button" className="btn-action danger" onClick={() => void onDelete(price.id)}>
                    삭제
                  </button>
                </td>
              </tr>
            )}
          />
        )}
      </section>

      {historyOpen && (
        <div className="modal-backdrop" role="presentation" onClick={closeHistory}>
          <div
            className="modal modal-wide unit-price-history-modal"
            role="dialog"
            aria-labelledby="unit-price-history-title"
            onClick={(e) => e.stopPropagation()}
          >
            <h2 id="unit-price-history-title">단가 변경 이력{historyTitle ? ` — ${historyTitle}` : ''}</h2>
            {historyError && <p className="error-banner">{historyError}</p>}
            {historyMode === 'all' && (
              <form onSubmit={onAllHistorySearch} className="unit-price-history-filters">
                <CompanySearchField
                  label="거래처"
                  partnerTypes={historyPartnerRoles}
                  selectedCompany={allHistoryFilters.company}
                  onSelect={(company) => setAllHistoryFilters((prev) => ({ ...prev, company }))}
                  clearToken={historyFilterClearToken}
                  placeholder="거래처명 또는 사업자번호"
                />
                <ItemSearchField
                  label="품목"
                  selectedItem={allHistoryFilters.item}
                  onSelect={(item) => setAllHistoryFilters((prev) => ({ ...prev, item }))}
                  clearToken={historyFilterClearToken}
                  allowedClassifications={historyItemClasses}
                  placeholder="품목번호 또는 품목명"
                />
                <label>
                  구분
                  <select
                    value={allHistoryFilters.costType}
                    onChange={(e) => {
                      const costType = e.target.value as HistoryCostTypeFilter;
                      setAllHistoryFilters((prev) => ({
                        ...prev,
                        costType,
                        company: null,
                        item: null,
                      }));
                      setHistoryFilterClearToken((token) => token + 1);
                    }}
                  >
                    <option value="">전체</option>
                    <option value="SALE">판매</option>
                    <option value="PURCHASE">구매</option>
                    <option value="OUTSOURCE">외주</option>
                  </select>
                </label>
                <label>
                  수정일(시작)
                  <input
                    type="date"
                    value={allHistoryFilters.changedFrom}
                    onChange={(e) =>
                      setAllHistoryFilters((prev) => ({ ...prev, changedFrom: e.target.value }))
                    }
                    title="등록이면 등록일, 삭제이면 삭제일 기준으로 검색합니다."
                  />
                </label>
                <label>
                  수정일(종료)
                  <input
                    type="date"
                    value={allHistoryFilters.changedTo}
                    onChange={(e) =>
                      setAllHistoryFilters((prev) => ({ ...prev, changedTo: e.target.value }))
                    }
                    title="등록이면 등록일, 삭제이면 삭제일 기준으로 검색합니다."
                  />
                </label>
                <label>
                  수정자
                  <input
                    type="text"
                    value={allHistoryFilters.changedBy}
                    onChange={(e) =>
                      setAllHistoryFilters((prev) => ({ ...prev, changedBy: e.target.value }))
                    }
                    placeholder="등록자·수정자·삭제자"
                    title="등록이면 등록자, 삭제이면 삭제자로 검색합니다."
                  />
                </label>
                <div className="form-actions unit-price-history-filter-actions">
                  <button type="submit" disabled={historyLoading}>
                    검색
                  </button>
                  <button type="button" className="secondary" disabled={historyLoading} onClick={onAllHistoryReset}>
                    초기화
                  </button>
                </div>
              </form>
            )}
            <div className="panel-header-row">
              <p className="hint" style={{ margin: 0 }}>
                등록·수정·삭제 시점의 단가 스냅샷입니다. 최신순으로 표시합니다.
              </p>
              <GridExcelExportButton
                fileBaseName="단가변경이력"
                sheetName="이력"
                disabled={historyLoading || historyRows.length === 0}
                rows={historyExportRows}
              />
            </div>
            {historyLoading ? (
              <p>불러오는 중…</p>
            ) : historyMode === 'all' && !hasAllHistorySearched ? (
              <p className="hint">검색 버튼을 누르면 이력이 표시됩니다.</p>
            ) : historyRows.length === 0 && !historyError ? (
              <p className="hint">변경 이력이 없습니다.</p>
            ) : (
              <VirtualMasterTable
                rows={historyRows}
                columnCount={11}
                visibleRowCount={12}
                getRowKey={(row) => row.id}
                renderHeader={() => (
                  <tr>
                    <th>품목번호</th>
                    <th>품목명</th>
                    <th>변경사유</th>
                    <th>단가구분</th>
                    <th>거래처명</th>
                    <th>시작공정</th>
                    <th>종료공정</th>
                    <th className="num">기준단가</th>
                    <th>적용시작일</th>
                    <th>수정자</th>
                    <th>수정일</th>
                  </tr>
                )}
                renderRow={(row) => (
                  <tr>
                    <td>{row.itemNo}</td>
                    <td>{row.itemName}</td>
                    <td>{row.updateReason || '—'}</td>
                    <td>{costTypeLabel(row.type)}</td>
                    <td>{row.companyName}</td>
                    <td>{row.beginProcessName || '—'}</td>
                    <td>{row.endProcessName || '—'}</td>
                    <td className="num">{formatAmount(row.standardUnitCost)}</td>
                    <td>{row.beginDate}</td>
                    <td>{formatHistoryActor(row, historyActorDuplicateNames)}</td>
                    <td>{formatDateTime(row.changedAt)}</td>
                  </tr>
                )}
              />
            )}
            <div className="modal-actions">
              <button type="button" className="secondary" onClick={closeHistory}>
                닫기
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
