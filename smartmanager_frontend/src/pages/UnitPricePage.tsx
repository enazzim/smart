import { useCallback, useEffect, useState } from 'react';
import type { CompanyRoleType } from '../api/company';
import type { PropertyClassification } from '../api/item';
import type { CodeOption } from '../api/process';
import { fetchProcessCodeOptions } from '../api/process';
import type { CostType, CreateUnitPriceRequest, UnitPrice, UpdateUnitPriceRequest } from '../api/unitPrice';
import {
  createUnitPrice,
  deleteUnitPrice,
  fetchUnitPrices,
  updateUnitPrice,
} from '../api/unitPrice';
import CompanySearchField, { type CompanySearchSelection } from '../components/CompanySearchField';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';

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
    itemClasses: ['제품', '상품'],
    showProcess: false,
    orderRateDisabled: true,
  },
  {
    type: 'PURCHASE',
    label: '구매단가',
    partnerType: 'PURCHASE',
    itemClasses: ['원자재', '상품'],
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
  const [activeTab, setActiveTab] = useState<CostType>('SALE');
  const tabConfig = TAB_CONFIG.find((tab) => tab.type === activeTab)!;

  const [processCodes, setProcessCodes] = useState<CodeOption[]>([]);
  const [prices, setPrices] = useState<UnitPrice[]>([]);
  const [filterItem, setFilterItem] = useState<ItemSearchSelection | null>(null);
  const [filterCompany, setFilterCompany] = useState<CompanySearchSelection | null>(null);
  const [formItem, setFormItem] = useState<ItemSearchSelection | null>(null);
  const [formCompany, setFormCompany] = useState<CompanySearchSelection | null>(null);
  const [form, setForm] = useState<CreateUnitPriceRequest & { updateReason?: string }>(emptyForm('SALE'));
  const [editingId, setEditingId] = useState<number | null>(null);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const isEditing = editingId !== null;

  const listQuery = filterItem?.itemNo ?? filterCompany?.companyName ?? '';

  const refreshList = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      setPrices(await fetchUnitPrices(activeTab, listQuery));
    } catch (e) {
      setError(e instanceof Error ? e.message : '목록 조회 실패');
    } finally {
      setLoading(false);
    }
  }, [activeTab, listQuery]);

  useEffect(() => {
    void fetchProcessCodeOptions().then(setProcessCodes).catch(() => setProcessCodes([]));
  }, []);

  useEffect(() => {
    void refreshList();
  }, [refreshList]);

  const resetForm = () => {
    setForm(emptyForm(activeTab));
    setFormItem(null);
    setFormCompany(null);
    setEditingId(null);
  };

  const switchTab = (type: CostType) => {
    setActiveTab(type);
    resetForm();
    setFilterItem(null);
    setFilterCompany(null);
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
      await refreshList();
    } catch (err) {
      setError(err instanceof Error ? err.message : '저장 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async (id: number) => {
    if (!window.confirm('이 단가를 삭제하시겠습니까?')) return;
    setError(null);
    try {
      await deleteUnitPrice(id);
      if (editingId === id) resetForm();
      await refreshList();
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
            <label className="full-width">
              변경 사유
              <input
                type="text"
                required
                value={form.updateReason ?? ''}
                onChange={(e) => setForm((prev) => ({ ...prev, updateReason: e.target.value }))}
                placeholder="수정 사유를 입력하세요"
              />
            </label>
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
        <h2>{tabConfig.label} 목록</h2>
        <div className="search-row">
          <ItemSearchField
            label="품목 필터 (선택)"
            selectedItem={filterItem}
            onSelect={setFilterItem}
            allowedClassifications={tabConfig.itemClasses}
            placeholder="전체 조회 — 품목번호 또는 품목명 입력"
          />
          <CompanySearchField
            label="거래처 필터 (선택)"
            partnerType={tabConfig.partnerType}
            selectedCompany={filterCompany}
            onSelect={setFilterCompany}
          />
          <button
            type="button"
            className="secondary"
            disabled={loading}
            onClick={() => {
              setFilterItem(null);
              setFilterCompany(null);
            }}
          >
            전체
          </button>
        </div>
        {loading ? (
          <p>불러오는 중…</p>
        ) : prices.length === 0 ? (
          <p>등록된 단가가 없습니다.</p>
        ) : (
          <table>
            <thead>
              <tr>
                <th>품목</th>
                <th>거래처</th>
                {tabConfig.showProcess && (
                  <>
                    <th>시작공정</th>
                    <th>종료공정</th>
                  </>
                )}
                {!tabConfig.orderRateDisabled && <th>발주비율</th>}
                <th>기준단가</th>
                <th>적용기간</th>
                <th>작업</th>
              </tr>
            </thead>
            <tbody>
              {prices.map((price) => (
                <tr key={price.id}>
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
                  {!tabConfig.orderRateDisabled && <td>{price.orderRate}%</td>}
                  <td>{price.standardUnitCost.toLocaleString()}</td>
                  <td>
                    {price.beginDate}
                    {price.endDate ? ` ~ ${price.endDate}` : ' ~'}
                  </td>
                  <td className="actions">
                    <button type="button" className="btn-action" onClick={() => startEdit(price)}>
                      수정
                    </button>
                    <button type="button" className="btn-action danger" onClick={() => void onDelete(price.id)}>
                      삭제
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </section>
    </div>
  );
}
