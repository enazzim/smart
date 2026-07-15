import { useMemo, useState } from 'react';
import CompanySearchField, {
  PURCHASE_OUTSOURCE_PARTNER_ROLES,
  type CompanySearchSelection,
} from '../components/CompanySearchField';
import GridExcelExportButton from '../components/GridExcelExportButton';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import type { PropertyClassification } from '../api/item';
import {
  createDefectClaim,
  deleteDefectClaim,
  fetchDefectClaims,
  updateDefectClaim,
  type DefectClaim,
  type DefectClaimListParams,
} from '../api/defectClaim';
import { formatAmount, formatQty } from '../utils/numberFormat';
import { useConfirm } from '../context/ConfirmContext';

const ALL_ITEM_CLASSES: PropertyClassification[] = ['원자재', '제품', '상품', '공정품', '부자재'];

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function addDaysIso(iso: string, days: number): string {
  const date = new Date(`${iso}T00:00:00`);
  date.setDate(date.getDate() + days);
  return date.toISOString().slice(0, 10);
}

function formatDateTime(value: string) {
  return new Date(value).toLocaleString('ko-KR');
}

function recognitionLabel(value: DefectClaim['recognition']) {
  return value === 'APPROVED' ? '승인' : '미승인';
}

function emptyForm() {
  return {
    receiptDate: todayIso(),
    reason: '',
    claimQty: '0',
    amount: '0',
  };
}

function defaultListFilters(): {
  receiptDateFrom: string;
  receiptDateTo: string;
} {
  const today = todayIso();
  return {
    receiptDateFrom: addDaysIso(today, -30),
    receiptDateTo: today,
  };
}

export default function DefectClaimPage() {
  const confirm = useConfirm();

  const [form, setForm] = useState(emptyForm);
  const [partner, setPartner] = useState<CompanySearchSelection | null>(null);
  const [item, setItem] = useState<ItemSearchSelection | null>(null);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [submitting, setSubmitting] = useState(false);
  const [formError, setFormError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const [listFilters, setListFilters] = useState(defaultListFilters);
  const [listPartner, setListPartner] = useState<CompanySearchSelection | null>(null);
  const [listItem, setListItem] = useState<ItemSearchSelection | null>(null);
  const [listClearToken, setListClearToken] = useState(0);
  const [rows, setRows] = useState<DefectClaim[]>([]);
  const [searched, setSearched] = useState(false);
  const [loading, setLoading] = useState(false);
  const [listError, setListError] = useState<string | null>(null);

  const isEditing = editingId != null;

  const exportRows = useMemo(
    () =>
      rows.map((row) => ({
        품목번호: row.itemNo,
        도면번호: row.drawingNo ?? '',
        품목명: row.itemName,
        거래처명: row.partnerName,
        변상등록일: row.receiptDate,
        클레임수량: Number(row.claimQty),
        금액: Number(row.amount),
        불량변상이유: row.reason,
        승인상태: recognitionLabel(row.recognition),
        등록자: row.createdBy ?? '',
        등록일시: formatDateTime(row.createdAt),
      })),
    [rows],
  );

  const resetForm = () => {
    setForm(emptyForm());
    setPartner(null);
    setItem(null);
    setEditingId(null);
    setFormError(null);
  };

  const startEdit = (row: DefectClaim) => {
    if (!row.editable) {
      setFormError('승인된 불량변상은 수정할 수 없습니다.');
      return;
    }
    setEditingId(row.id);
    setPartner({
      id: row.partnerId,
      companyName: row.partnerName,
      businessRegNo: row.partnerBusinessRegNo,
    });
    setItem({
      id: row.itemId,
      itemNo: row.itemNo,
      itemName: row.itemName,
    });
    setForm({
      receiptDate: row.receiptDate,
      reason: row.reason,
      claimQty: String(row.claimQty),
      amount: String(row.amount),
    });
    setFormError(null);
    setMessage(null);
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!partner) {
      setFormError('거래처를 선택해 주세요.');
      return;
    }
    if (!item) {
      setFormError('품목을 선택해 주세요.');
      return;
    }
    const claimQty = Number(form.claimQty);
    const amount = Number(form.amount);
    if (!form.reason.trim()) {
      setFormError('불량변상 이유를 입력해 주세요.');
      return;
    }
    if (!Number.isFinite(claimQty) || claimQty <= 0) {
      setFormError('수량은 0보다 커야 합니다.');
      return;
    }
    if (!Number.isFinite(amount) || amount <= 0) {
      setFormError('금액은 0보다 커야 합니다.');
      return;
    }

    setSubmitting(true);
    setFormError(null);
    setMessage(null);
    try {
      const payload = {
        partnerId: partner.id,
        itemId: item.id,
        receiptDate: form.receiptDate,
        claimQty,
        amount,
        reason: form.reason.trim(),
      };
      if (isEditing && editingId != null) {
        await updateDefectClaim(editingId, payload);
        setMessage('불량변상을 수정했습니다.');
      } else {
        await createDefectClaim(payload);
        setMessage('불량변상을 등록했습니다.');
      }
      resetForm();
      if (searched) {
        await runSearch();
      }
    } catch (err) {
      setFormError(err instanceof Error ? err.message : '저장에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async (row: DefectClaim) => {
    if (!row.editable) {
      setListError('승인된 불량변상은 삭제할 수 없습니다.');
      return;
    }
    if (
      !(await confirm(`${row.itemNo} / ${row.partnerName} 불량변상을 삭제하시겠습니까?`, {
        title: '삭제 확인',
        confirmLabel: '예, 삭제',
        cancelLabel: '닫기',
        danger: true,
      }))
    ) {
      return;
    }
    setListError(null);
    setMessage(null);
    try {
      await deleteDefectClaim(row.id);
      setMessage('불량변상을 삭제했습니다.');
      if (editingId === row.id) {
        resetForm();
      }
      if (searched) {
        await runSearch();
      }
    } catch (err) {
      setListError(err instanceof Error ? err.message : '삭제에 실패했습니다.');
    }
  };

  const runSearch = async () => {
    setLoading(true);
    setListError(null);
    try {
      const params: DefectClaimListParams = {
        receiptDateFrom: listFilters.receiptDateFrom || undefined,
        receiptDateTo: listFilters.receiptDateTo || undefined,
        partnerId: listPartner?.id,
        partnerName: listPartner?.companyName,
        itemId: listItem?.id,
        itemNo: listItem?.itemNo,
        itemName: listItem?.itemName,
      };
      setRows(await fetchDefectClaims(params));
      setSearched(true);
    } catch (err) {
      setListError(err instanceof Error ? err.message : '변상 목록 조회 실패');
      setRows([]);
      setSearched(true);
    } finally {
      setLoading(false);
    }
  };

  const resetListFilters = () => {
    setListFilters(defaultListFilters());
    setListPartner(null);
    setListItem(null);
    setListClearToken((n) => n + 1);
    setListError(null);
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>불량변상</h1>
        <p>구매·외주 거래처 대상 불량변상을 등록하고 목록에서 조회·수정·삭제합니다.</p>
      </header>

      {message && <p className="success-banner">{message}</p>}

      <section className="panel">
        <h2>{isEditing ? '불량변상 수정' : '불량변상 등록'}</h2>
        {formError && <div className="error">{formError}</div>}
        <form onSubmit={(e) => void onSubmit(e)}>
          <div className="form-grid-wide">
            <CompanySearchField
              label="거래처"
              partnerTypes={PURCHASE_OUTSOURCE_PARTNER_ROLES}
              selectedCompany={partner}
              onSelect={setPartner}
            />
            <ItemSearchField
              label="품목"
              selectedItem={item}
              onSelect={setItem}
              allowedClassifications={ALL_ITEM_CLASSES}
            />
            <label>
              변상등록일
              <input
                type="date"
                value={form.receiptDate}
                onChange={(e) => setForm((f) => ({ ...f, receiptDate: e.target.value }))}
                required
              />
            </label>
            <label>
              수량
              <input
                type="number"
                min={0}
                step="any"
                value={form.claimQty}
                onChange={(e) => setForm((f) => ({ ...f, claimQty: e.target.value }))}
              />
            </label>
            <label>
              금액
              <input
                type="number"
                min={0}
                step="any"
                value={form.amount}
                onChange={(e) => setForm((f) => ({ ...f, amount: e.target.value }))}
              />
            </label>
            <label className="span-2">
              불량변상 이유
              <input
                value={form.reason}
                onChange={(e) => setForm((f) => ({ ...f, reason: e.target.value }))}
                placeholder="불량변상 이유 입력"
                maxLength={500}
              />
            </label>
          </div>
          <div className="form-actions">
            <button type="button" className="secondary" onClick={resetForm} disabled={submitting}>
              초기화
            </button>
            <button type="submit" disabled={submitting}>
              {submitting ? '처리 중…' : isEditing ? '수정' : '등록'}
            </button>
          </div>
        </form>
      </section>

      <section className="panel">
        <div className="panel-header-row">
          <h2>변상 목록</h2>
          <GridExcelExportButton fileBaseName="불량변상목록" disabled={!searched || loading} rows={exportRows} />
        </div>
        {listError && <div className="error">{listError}</div>}
        <div className="form-grid-wide">
          <CompanySearchField
            label="거래처"
            partnerTypes={PURCHASE_OUTSOURCE_PARTNER_ROLES}
            selectedCompany={listPartner}
            onSelect={setListPartner}
            clearToken={listClearToken}
          />
          <ItemSearchField
            label="품목"
            selectedItem={listItem}
            onSelect={setListItem}
            allowedClassifications={ALL_ITEM_CLASSES}
            clearToken={listClearToken}
          />
          <label>
            변상등록일(부터)
            <input
              type="date"
              value={listFilters.receiptDateFrom}
              onChange={(e) => setListFilters((f) => ({ ...f, receiptDateFrom: e.target.value }))}
            />
          </label>
          <label>
            변상등록일(까지)
            <input
              type="date"
              value={listFilters.receiptDateTo}
              onChange={(e) => setListFilters((f) => ({ ...f, receiptDateTo: e.target.value }))}
            />
          </label>
        </div>
        <div className="form-actions">
          <button type="button" onClick={() => void runSearch()} disabled={loading}>
            {loading ? '조회 중…' : '검색'}
          </button>
          <button type="button" className="secondary" onClick={resetListFilters} disabled={loading}>
            초기화
          </button>
        </div>

        {!searched ? (
          <p className="hint-text">검색 조건을 입력한 뒤 검색 버튼을 눌러 주세요.</p>
        ) : loading ? (
          <p>불러오는 중…</p>
        ) : rows.length === 0 ? (
          <p>조건에 맞는 불량변상이 없습니다.</p>
        ) : (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>품목번호</th>
                  <th>도면번호</th>
                  <th>품목명</th>
                  <th>거래처명</th>
                  <th>변상등록일</th>
                  <th className="num">수량</th>
                  <th className="num">금액</th>
                  <th>불량변상 이유</th>
                  <th>승인</th>
                  <th>관리</th>
                </tr>
              </thead>
              <tbody>
                {rows.map((row) => (
                  <tr key={row.id}>
                    <td>{row.itemNo}</td>
                    <td>{row.drawingNo || '—'}</td>
                    <td>{row.itemName}</td>
                    <td>{row.partnerName}</td>
                    <td>{row.receiptDate}</td>
                    <td className="num">{formatQty(row.claimQty)}</td>
                    <td className="num">{formatAmount(row.amount)}</td>
                    <td>{row.reason}</td>
                    <td>{recognitionLabel(row.recognition)}</td>
                    <td className="actions">
                      <button
                        type="button"
                        className="btn-action"
                        disabled={!row.editable}
                        onClick={() => startEdit(row)}
                      >
                        수정
                      </button>
                      <button
                        type="button"
                        className="btn-action danger"
                        disabled={!row.editable}
                        onClick={() => void onDelete(row)}
                      >
                        삭제
                      </button>
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
