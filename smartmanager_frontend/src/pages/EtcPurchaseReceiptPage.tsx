import { useCallback, useEffect, useMemo, useState } from 'react';
import FiscalPeriodDisplay from '../components/FiscalPeriodDisplay';
import { formatFiscalPeriodFromIso } from '../utils/fiscalCalendar';
import {
  cancelEtcPurchaseReceipt,
  createEtcPurchaseReceipts,
  fetchEtcPurchaseReceiptCandidates,
  fetchEtcPurchaseReceipts,
  updateEtcPurchaseReceipt,
  type EtcPurchaseReceipt,
  type EtcPurchaseReceiptCandidate,
  type EtcPurchaseReceiptCandidateParams,
  type EtcPurchaseReceiptListParams,
} from '../api/etcPurchase';

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function formatQty(value: number): string {
  return Number.isInteger(value) ? String(value) : value.toLocaleString(undefined, { maximumFractionDigits: 4 });
}

function formatAmount(value: number): string {
  return value.toLocaleString(undefined, { maximumFractionDigits: 2 });
}

export default function EtcPurchaseReceiptPage() {
  const [tab, setTab] = useState<'candidates' | 'history'>('candidates');
  const [candidates, setCandidates] = useState<EtcPurchaseReceiptCandidate[]>([]);
  const [receipts, setReceipts] = useState<EtcPurchaseReceipt[]>([]);
  const [selectedOrderId, setSelectedOrderId] = useState<number | null>(null);
  const [receiptQty, setReceiptQty] = useState('');
  const [receiptDate, setReceiptDate] = useState(todayIso());
  const [candidateFilters, setCandidateFilters] = useState<EtcPurchaseReceiptCandidateParams>({});
  const [historyFilters, setHistoryFilters] = useState<EtcPurchaseReceiptListParams>(() => ({
    receiptFrom: addDaysIso(todayIso(), -30),
    receiptTo: todayIso(),
  }));
  const [loadingCandidates, setLoadingCandidates] = useState(true);
  const [loadingHistory, setLoadingHistory] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [editingReceipt, setEditingReceipt] = useState<EtcPurchaseReceipt | null>(null);
  const [editReceiptDate, setEditReceiptDate] = useState(todayIso());
  const [editReceiptQty, setEditReceiptQty] = useState('');

  const selectedCandidate = useMemo(
    () => candidates.find((row) => row.etcPurchaseOrderId === selectedOrderId) ?? null,
    [candidates, selectedOrderId],
  );

  const loadCandidates = useCallback(async () => {
    setLoadingCandidates(true);
    setError(null);
    try {
      setCandidates(await fetchEtcPurchaseReceiptCandidates(candidateFilters));
    } catch (e) {
      setError(e instanceof Error ? e.message : '미완료 발주 조회 실패');
      setCandidates([]);
    } finally {
      setLoadingCandidates(false);
    }
  }, [candidateFilters]);

  const loadReceipts = useCallback(async () => {
    setLoadingHistory(true);
    try {
      setReceipts(await fetchEtcPurchaseReceipts(historyFilters));
    } catch (e) {
      setError(e instanceof Error ? e.message : '입고 내역 조회 실패');
      setReceipts([]);
    } finally {
      setLoadingHistory(false);
    }
  }, [historyFilters]);

  useEffect(() => {
    if (tab === 'candidates') {
      void loadCandidates();
    }
  }, [tab, loadCandidates]);

  useEffect(() => {
    if (tab === 'history') {
      void loadReceipts();
    }
  }, [tab, loadReceipts]);

  const onSelectCandidate = (row: EtcPurchaseReceiptCandidate) => {
    setSelectedOrderId(row.etcPurchaseOrderId);
    setReceiptQty(String(row.remainQty));
    setError(null);
    setSuccess(null);
  };

  const onSubmitReceipt = async () => {
    if (!selectedCandidate) {
      setError('입고할 발주를 선택해 주세요.');
      return;
    }
    const qty = Number(receiptQty);
    if (!Number.isFinite(qty) || qty <= 0) {
      setError('납품수량을 입력해 주세요.');
      return;
    }
    if (qty > selectedCandidate.remainQty) {
      setError(`납품수량이 잔량(${formatQty(selectedCandidate.remainQty)})을 초과합니다.`);
      return;
    }
    setSubmitting(true);
    setError(null);
    setSuccess(null);
    try {
      const created = await createEtcPurchaseReceipts({
        receiptDate,
        lines: [{ etcPurchaseOrderId: selectedCandidate.etcPurchaseOrderId, receiptQty: qty }],
      });
      setSuccess(`입고 등록 완료: ${created.map((r) => r.receiptNo).join(', ')}`);
      setSelectedOrderId(null);
      setReceiptQty('');
      await loadCandidates();
    } catch (e) {
      setError(e instanceof Error ? e.message : '입고 등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const openEditReceipt = (receipt: EtcPurchaseReceipt) => {
    setEditingReceipt(receipt);
    setEditReceiptDate(receipt.receiptDate);
    setEditReceiptQty(String(receipt.receiptQty));
  };

  const onSaveEditReceipt = async () => {
    if (!editingReceipt) {
      return;
    }
    const qty = Number(editReceiptQty);
    if (!Number.isFinite(qty) || qty <= 0) {
      setError('납품수량을 확인해 주세요.');
      return;
    }
    setSubmitting(true);
    setError(null);
    try {
      await updateEtcPurchaseReceipt(editingReceipt.id, {
        receiptDate: editReceiptDate,
        receiptQty: qty,
      });
      setSuccess('입고 내역을 수정했습니다.');
      setEditingReceipt(null);
      await loadReceipts();
      await loadCandidates();
    } catch (e) {
      setError(e instanceof Error ? e.message : '입고 수정 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancelReceipt = async (receipt: EtcPurchaseReceipt) => {
    if (!window.confirm(`${receipt.receiptNo} 입고를 취소하시겠습니까?`)) {
      return;
    }
    setError(null);
    try {
      await cancelEtcPurchaseReceipt(receipt.id);
      setSuccess(`입고 취소 완료: ${receipt.receiptNo}`);
      await loadReceipts();
      await loadCandidates();
    } catch (e) {
      setError(e instanceof Error ? e.message : '입고 취소 실패');
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>기타구매입고</h1>
        <p>미완료 기타구매발주에 대해 납품(입고)을 등록합니다. 재고 반영 없이 매입·미지급 원장에 반영됩니다.</p>
      </header>

      <div className="tab-row">
        <button type="button" className={tab === 'candidates' ? 'tab-active' : undefined} onClick={() => setTab('candidates')}>
          입고 처리
        </button>
        <button type="button" className={tab === 'history' ? 'tab-active' : undefined} onClick={() => setTab('history')}>
          입고 내역
        </button>
      </div>

      {error && <p className="error-banner">{error}</p>}
      {success && <p className="success-banner">{success}</p>}

      {tab === 'candidates' && (
        <>
          <section className="filter-panel">
            <label>
              품목명
              <input
                value={candidateFilters.itemName ?? ''}
                onChange={(e) => setCandidateFilters((f) => ({ ...f, itemName: e.target.value }))}
              />
            </label>
            <label>
              거래처
              <input
                value={candidateFilters.partnerName ?? ''}
                onChange={(e) => setCandidateFilters((f) => ({ ...f, partnerName: e.target.value }))}
              />
            </label>
            <label>
              납기요구일(부터)
              <input
                type="date"
                value={candidateFilters.deliveryFrom ?? ''}
                onChange={(e) => setCandidateFilters((f) => ({ ...f, deliveryFrom: e.target.value }))}
              />
            </label>
            <label>
              납기요구일(까지)
              <input
                type="date"
                value={candidateFilters.deliveryTo ?? ''}
                onChange={(e) => setCandidateFilters((f) => ({ ...f, deliveryTo: e.target.value }))}
              />
            </label>
            <button type="button" onClick={() => void loadCandidates()}>
              조회
            </button>
          </section>

          {loadingCandidates ? (
            <p>불러오는 중…</p>
          ) : (
            <div className="table-wrap">
              <table>
                <thead>
                  <tr>
                    <th />
                    <th>발주번호</th>
                    <th>품목명</th>
                    <th>거래처</th>
                    <th>발주수량</th>
                    <th>단가</th>
                    <th>잔량</th>
                    <th>납기요구일</th>
                    <th>상태</th>
                  </tr>
                </thead>
                <tbody>
                  {candidates.length === 0 ? (
                    <tr>
                      <td colSpan={9}>입고 가능한 발주가 없습니다.</td>
                    </tr>
                  ) : (
                    candidates.map((row) => (
                      <tr
                        key={row.etcPurchaseOrderId}
                        className={selectedOrderId === row.etcPurchaseOrderId ? 'row-selected' : undefined}
                        onClick={() => onSelectCandidate(row)}
                      >
                        <td>
                          <input
                            type="radio"
                            checked={selectedOrderId === row.etcPurchaseOrderId}
                            onChange={() => onSelectCandidate(row)}
                          />
                        </td>
                        <td>{row.orderNo}</td>
                        <td>{row.itemName}</td>
                        <td>{row.partnerName}</td>
                        <td>{formatQty(row.orderQty)}</td>
                        <td>{formatAmount(row.unitPrice)}</td>
                        <td>{formatQty(row.remainQty)}</td>
                        <td>{row.requestedDeliveryDate}</td>
                        <td>{row.statusLabel}</td>
                      </tr>
                    ))
                  )}
                </tbody>
              </table>
            </div>
          )}

          <section className="panel" style={{ marginTop: '1rem' }}>
            <h2>납품 입력</h2>
            {selectedCandidate ? (
              <p>
                선택: {selectedCandidate.itemName} / {selectedCandidate.partnerName} — 잔량{' '}
                {formatQty(selectedCandidate.remainQty)}
              </p>
            ) : (
              <p>위 목록에서 발주를 선택하세요.</p>
            )}
            <div className="action-bar">
              <label>
                납품일
                <input type="date" value={receiptDate} onChange={(e) => setReceiptDate(e.target.value)} />
              </label>
              <FiscalPeriodDisplay baseDate={receiptDate} />
              <label>
                납품수량
                <input
                  type="number"
                  min={0}
                  step="any"
                  value={receiptQty}
                  onChange={(e) => setReceiptQty(e.target.value)}
                  disabled={!selectedCandidate}
                />
              </label>
              <button type="button" disabled={submitting || !selectedCandidate} onClick={() => void onSubmitReceipt()}>
                {submitting ? '등록 중…' : '입고 등록'}
              </button>
            </div>
          </section>
        </>
      )}

      {tab === 'history' && (
        <>
          <section className="filter-panel">
            <label>
              품목명
              <input
                value={historyFilters.itemName ?? ''}
                onChange={(e) => setHistoryFilters((f) => ({ ...f, itemName: e.target.value }))}
              />
            </label>
            <label>
              거래처
              <input
                value={historyFilters.partnerName ?? ''}
                onChange={(e) => setHistoryFilters((f) => ({ ...f, partnerName: e.target.value }))}
              />
            </label>
            <label>
              납입일자(부터)
              <input
                type="date"
                value={historyFilters.receiptFrom ?? ''}
                onChange={(e) => setHistoryFilters((f) => ({ ...f, receiptFrom: e.target.value }))}
              />
            </label>
            <label>
              납입일자(까지)
              <input
                type="date"
                value={historyFilters.receiptTo ?? ''}
                onChange={(e) => setHistoryFilters((f) => ({ ...f, receiptTo: e.target.value }))}
              />
            </label>
            <button type="button" onClick={() => void loadReceipts()}>
              조회
            </button>
          </section>

          {loadingHistory ? (
            <p>불러오는 중…</p>
          ) : (
            <div className="table-wrap">
              <table>
                <thead>
                  <tr>
                    <th>입고번호</th>
                    <th>발주번호</th>
                    <th>품목명</th>
                    <th>거래처</th>
                    <th>납품수량</th>
                    <th>금액</th>
                    <th>납입일자</th>
                    <th>매입월</th>
                    <th />
                  </tr>
                </thead>
                <tbody>
                  {receipts.length === 0 ? (
                    <tr>
                      <td colSpan={9}>입고 내역이 없습니다.</td>
                    </tr>
                  ) : (
                    receipts.map((row) => (
                      <tr key={row.id}>
                        <td>{row.receiptNo}</td>
                        <td>{row.orderNo}</td>
                        <td>{row.itemName}</td>
                        <td>{row.partnerName}</td>
                        <td>{formatQty(row.receiptQty)}</td>
                        <td>{formatAmount(row.amount)}</td>
                        <td>{row.receiptDate}</td>
                        <td>{formatFiscalPeriodFromIso(row.receiptDate)}</td>
                        <td className="actions">
                          <button type="button" className="secondary" onClick={() => openEditReceipt(row)}>
                            수정
                          </button>
                          <button type="button" className="danger" onClick={() => void onCancelReceipt(row)}>
                            삭제
                          </button>
                        </td>
                      </tr>
                    ))
                  )}
                </tbody>
              </table>
            </div>
          )}
        </>
      )}

      {editingReceipt && (
        <div className="modal-backdrop" role="presentation" onClick={() => setEditingReceipt(null)}>
          <div className="modal" role="dialog" onClick={(e) => e.stopPropagation()}>
            <h2>입고 수정</h2>
            <p>
              {editingReceipt.receiptNo} — {editingReceipt.itemName}
            </p>
            <label>
              납입일자
              <input type="date" value={editReceiptDate} onChange={(e) => setEditReceiptDate(e.target.value)} />
            </label>
            <FiscalPeriodDisplay baseDate={editReceiptDate} />
            <label>
              납품수량
              <input
                type="number"
                min={0}
                step="any"
                value={editReceiptQty}
                onChange={(e) => setEditReceiptQty(e.target.value)}
              />
            </label>
            <div className="modal-actions">
              <button type="button" className="secondary" onClick={() => setEditingReceipt(null)} disabled={submitting}>
                닫기
              </button>
              <button type="button" disabled={submitting} onClick={() => void onSaveEditReceipt()}>
                {submitting ? '저장 중…' : '저장'}
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

function addDaysIso(iso: string, days: number): string {
  const date = new Date(`${iso}T00:00:00`);
  date.setDate(date.getDate() + days);
  return date.toISOString().slice(0, 10);
}
