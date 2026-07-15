import { useCallback, useEffect, useMemo, useState } from 'react';
import {
  cancelPartnerPayment,
  createPartnerPayment,
  fetchPartnerPaymentCandidates,
  fetchPartnerPayments,
  type PartnerPayment,
  type PartnerPaymentCandidate,
  type PartnerPaymentCandidateParams,
  type PartnerPaymentCostCategory,
  type PartnerPaymentListParams,
} from '../api/partnerPayment';
import { formatAmount } from '../utils/numberFormat';
import {
  breakdownFromSupply,
  breakdownFromTotal,
  formatMoneyInput,
} from '../utils/vatAmount';
import { useConfirm } from '../context/ConfirmContext';

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function addDaysIso(iso: string, days: number): string {
  const date = new Date(`${iso}T00:00:00`);
  date.setDate(date.getDate() + days);
  return date.toISOString().slice(0, 10);
}

function parseAmount(value: string): number | null {
  const parsed = Number(value);
  return Number.isFinite(parsed) && parsed >= 0 ? parsed : null;
}

const PAYMENT_METHODS = ['현금', '계좌이체', '어음', '카드', '기타'];

function defaultCostCategory(row: PartnerPaymentCandidate): PartnerPaymentCostCategory {
  if (row.purchasePayableAmount > 0 && row.outsourcePayableAmount <= 0) return 'PURCHASE';
  if (row.outsourcePayableAmount > 0 && row.purchasePayableAmount <= 0) return 'OUTSOURCE';
  return 'PURCHASE';
}

export default function PartnerPaymentPage() {
  const confirm = useConfirm();
  const [candidates, setCandidates] = useState<PartnerPaymentCandidate[]>([]);
  const [payments, setPayments] = useState<PartnerPayment[]>([]);
  const [filters, setFilters] = useState<PartnerPaymentCandidateParams>({});
  const [listFilters, setListFilters] = useState<PartnerPaymentListParams>(() => ({
    paymentDateFrom: addDaysIso(todayIso(), -30),
    paymentDateTo: todayIso(),
    excludeCancelled: true,
  }));
  const [selectedPartnerId, setSelectedPartnerId] = useState<number | null>(null);
  const [costCategory, setCostCategory] = useState<PartnerPaymentCostCategory>('PURCHASE');
  const [paymentDate, setPaymentDate] = useState(todayIso());
  const [supplyAmount, setSupplyAmount] = useState('');
  const [vatAmount, setVatAmount] = useState('0');
  const [totalAmountInput, setTotalAmountInput] = useState('');
  const [paymentMethod, setPaymentMethod] = useState('계좌이체');
  const [remark, setRemark] = useState('');
  const [loadingCandidates, setLoadingCandidates] = useState(true);
  const [loadingPayments, setLoadingPayments] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [candidateError, setCandidateError] = useState<string | null>(null);
  const [paymentError, setPaymentError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const loadCandidates = useCallback(async () => {
    setLoadingCandidates(true);
    setCandidateError(null);
    try {
      setCandidates(await fetchPartnerPaymentCandidates(filters));
    } catch (e) {
      setCandidateError(e instanceof Error ? e.message : '미지급 후보 조회 실패');
      setCandidates([]);
    } finally {
      setLoadingCandidates(false);
    }
  }, [filters]);

  const loadPayments = useCallback(async () => {
    setLoadingPayments(true);
    setPaymentError(null);
    try {
      setPayments(await fetchPartnerPayments(listFilters));
    } catch (e) {
      setPaymentError(e instanceof Error ? e.message : '지급 목록 조회 실패');
      setPayments([]);
    } finally {
      setLoadingPayments(false);
    }
  }, [listFilters]);

  useEffect(() => {
    void loadCandidates();
  }, [loadCandidates]);

  useEffect(() => {
    void loadPayments();
  }, [loadPayments]);

  const selectedCandidate = useMemo(
    () => candidates.find((row) => row.partnerId === selectedPartnerId) ?? null,
    [candidates, selectedPartnerId],
  );

  const applyBreakdown = (supply: number, vat: number, total: number) => {
    setSupplyAmount(formatMoneyInput(supply));
    setVatAmount(formatMoneyInput(vat));
    setTotalAmountInput(formatMoneyInput(total));
  };

  /** 공급가 입력 → 부가세·총액 계산 (입력 중인 공급가는 그대로 유지) */
  const onSupplyAmountChange = (raw: string) => {
    setSupplyAmount(raw);
    if (raw.trim() === '') {
      setVatAmount('0');
      setTotalAmountInput('');
      return;
    }
    const supply = parseAmount(raw);
    if (supply == null) {
      return;
    }
    const next = breakdownFromSupply(supply);
    setVatAmount(formatMoneyInput(next.vat));
    setTotalAmountInput(formatMoneyInput(next.total));
  };

  /** 공급가 포커스 아웃 시 소수점 절사 후 부가세·총액 확정 */
  const onSupplyAmountBlur = () => {
    if (supplyAmount.trim() === '') {
      return;
    }
    const supply = parseAmount(supplyAmount);
    if (supply == null) {
      return;
    }
    const next = breakdownFromSupply(supply);
    applyBreakdown(next.supply, next.vat, next.total);
  };

  /** 총액 입력 → 공급가·부가세 계산 (입력 중인 총액은 그대로 유지) */
  const onTotalAmountChange = (raw: string) => {
    setTotalAmountInput(raw);
    if (raw.trim() === '') {
      setSupplyAmount('');
      setVatAmount('0');
      return;
    }
    const total = parseAmount(raw);
    if (total == null) {
      return;
    }
    const next = breakdownFromTotal(total);
    setSupplyAmount(formatMoneyInput(next.supply));
    setVatAmount(formatMoneyInput(next.vat));
  };

  /** 총액 포커스 아웃 시 공급가·부가세 재계산 후, 총액은 공급가+부가세로 확정 */
  const onTotalAmountBlur = () => {
    if (totalAmountInput.trim() === '') {
      return;
    }
    const total = parseAmount(totalAmountInput);
    if (total == null) {
      return;
    }
    const next = breakdownFromTotal(total);
    applyBreakdown(next.supply, next.vat, next.total);
  };

  const onSelectPartner = (row: PartnerPaymentCandidate) => {
    setSelectedPartnerId(row.partnerId);
    setCostCategory(defaultCostCategory(row));
    // 미지급 잔액 = 공급가액
    const next = breakdownFromSupply(row.unpaidAmount);
    applyBreakdown(next.supply, next.vat, next.total);
    setPaymentError(null);
  };

  const onCreatePayment = async () => {
    if (selectedPartnerId == null) {
      setPaymentError('지급할 거래처를 선택하세요.');
      return;
    }
    const supply = parseAmount(supplyAmount);
    if (supply == null || supply <= 0) {
      setPaymentError('공급가를 입력하세요.');
      return;
    }
    const vat = parseAmount(vatAmount) ?? 0;
    if (selectedCandidate && supply > selectedCandidate.unpaidAmount) {
      setPaymentError(`공급가가 미지급 잔액(${formatAmount(selectedCandidate.unpaidAmount)})을 초과합니다.`);
      return;
    }

    setSubmitting(true);
    setPaymentError(null);
    setMessage(null);
    try {
      const created = await createPartnerPayment({
        partnerId: selectedPartnerId,
        paymentDate,
        costCategory,
        supplyAmount: supply,
        vatAmount: vat,
        paymentMethod: paymentMethod || undefined,
        remark: remark.trim() || undefined,
      });
      setMessage(`지급 ${created.paymentNo}을(를) 등록했습니다.`);
      setSelectedPartnerId(null);
      setSupplyAmount('');
      setVatAmount('0');
      setTotalAmountInput('');
      setRemark('');
      await loadCandidates();
      await loadPayments();
    } catch (e) {
      setPaymentError(e instanceof Error ? e.message : '지급 등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancel = async (payment: PartnerPayment) => {
    if (!(await confirm(`지급 ${payment.paymentNo}을(를) 취소하시겠습니까?`, { title: '취소 확인', confirmLabel: '예, 취소', cancelLabel: '닫기', danger: true }))) return;
    setSubmitting(true);
    setPaymentError(null);
    try {
      await cancelPartnerPayment(payment.id);
      setMessage(`지급 ${payment.paymentNo}을(를) 취소했습니다.`);
      await loadCandidates();
      await loadPayments();
    } catch (e) {
      setPaymentError(e instanceof Error ? e.message : '지급 취소 실패');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>지급</h1>
        <p>구매·외주 미지급 잔액이 있는 거래처에 대해 지급을 등록합니다.</p>
      </header>

      {message && <p className="success-banner">{message}</p>}
      {paymentError && <p className="error-banner">{paymentError}</p>}

      <section className="panel">
        <h2>미지급 거래처</h2>
        <div className="filter-row">
          <label>
            거래처
            <input
              type="text"
              value={filters.partnerName ?? ''}
              onChange={(e) => setFilters((f) => ({ ...f, partnerName: e.target.value }))}
            />
          </label>
          <button type="button" className="secondary" onClick={() => void loadCandidates()} disabled={loadingCandidates}>
            조회
          </button>
        </div>
        {loadingCandidates ? (
          <p>불러오는 중…</p>
        ) : candidateError ? (
          <p className="error-banner">{candidateError}</p>
        ) : candidates.length === 0 ? (
          <p className="hint">지급 가능한 미지급 거래처가 없습니다.</p>
        ) : (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th />
                  <th>거래처</th>
                  <th>사업자번호</th>
                  <th className="num">구매발생</th>
                  <th className="num">외주발생</th>
                  <th className="num">지급합계</th>
                  <th className="num">미지급잔액(공급가)</th>
                </tr>
              </thead>
              <tbody>
                {candidates.map((row) => (
                  <tr
                    key={row.partnerId}
                    className={selectedPartnerId === row.partnerId ? 'row-selected' : undefined}
                  >
                    <td>
                      <input
                        type="radio"
                        name="payment-partner"
                        aria-label={`${row.partnerName} 선택`}
                        checked={selectedPartnerId === row.partnerId}
                        disabled={!row.payable || submitting}
                        onChange={() => onSelectPartner(row)}
                      />
                    </td>
                    <td>{row.partnerName}</td>
                    <td>{row.partnerBusinessRegNo}</td>
                    <td className="num">{formatAmount(row.purchasePayableAmount)}</td>
                    <td className="num">{formatAmount(row.outsourcePayableAmount)}</td>
                    <td className="num">{formatAmount(row.paidAmount)}</td>
                    <td className="num">{formatAmount(row.unpaidAmount)}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}

        <div className="action-bar">
          <label>
            지급일
            <input
              type="date"
              value={paymentDate}
              onChange={(e) => setPaymentDate(e.target.value)}
              disabled={submitting}
            />
          </label>
          <label>
            비용구분
            <select
              value={costCategory}
              onChange={(e) => setCostCategory(e.target.value as PartnerPaymentCostCategory)}
              disabled={submitting || selectedPartnerId == null}
            >
              <option value="PURCHASE">구매</option>
              <option value="OUTSOURCE">외주</option>
            </select>
          </label>
          <label>
            공급가
            <input
              type="number"
              min={0}
              step="any"
              value={supplyAmount}
              onChange={(e) => onSupplyAmountChange(e.target.value)}
              onBlur={onSupplyAmountBlur}
              disabled={submitting || selectedPartnerId == null}
              title="공급가 입력 시 부가세·총액 자동계산"
            />
          </label>
          <label>
            부가세
            <input
              type="number"
              min={0}
              step="any"
              value={vatAmount}
              readOnly
              disabled={submitting || selectedPartnerId == null}
              title="공급가 × 10% (소수점 있으면 절상)"
            />
          </label>
          <label>
            총액
            <input
              type="number"
              min={0}
              step="any"
              value={totalAmountInput}
              onChange={(e) => onTotalAmountChange(e.target.value)}
              onBlur={onTotalAmountBlur}
              disabled={submitting || selectedPartnerId == null}
              title="총액 입력 시 공급가·부가세 자동계산"
            />
          </label>
          <label>
            결제수단
            <select
              value={paymentMethod}
              onChange={(e) => setPaymentMethod(e.target.value)}
              disabled={submitting || selectedPartnerId == null}
            >
              {PAYMENT_METHODS.map((method) => (
                <option key={method} value={method}>
                  {method}
                </option>
              ))}
            </select>
          </label>
          <label>
            비고
            <input
              type="text"
              value={remark}
              onChange={(e) => setRemark(e.target.value)}
              disabled={submitting || selectedPartnerId == null}
            />
          </label>
          <button
            type="button"
            disabled={submitting || selectedPartnerId == null}
            onClick={() => void onCreatePayment()}
          >
            {submitting ? '등록 중…' : '지급 등록'}
          </button>
        </div>
        {selectedCandidate && (
          <p className="hint">
            선택: {selectedCandidate.partnerName} — 미지급 잔액(공급가) {formatAmount(selectedCandidate.unpaidAmount)}
          </p>
        )}
      </section>

      <section className="panel">
        <h2>지급 목록</h2>
        <div className="filter-row">
          <label>
            지급일 From
            <input
              type="date"
              value={listFilters.paymentDateFrom ?? ''}
              onChange={(e) => setListFilters((f) => ({ ...f, paymentDateFrom: e.target.value }))}
            />
          </label>
          <label>
            To
            <input
              type="date"
              value={listFilters.paymentDateTo ?? ''}
              onChange={(e) => setListFilters((f) => ({ ...f, paymentDateTo: e.target.value }))}
            />
          </label>
          <label>
            거래처
            <input
              type="text"
              value={listFilters.partnerName ?? ''}
              onChange={(e) => setListFilters((f) => ({ ...f, partnerName: e.target.value }))}
            />
          </label>
          <label>
            지급번호
            <input
              type="text"
              value={listFilters.paymentNo ?? ''}
              onChange={(e) => setListFilters((f) => ({ ...f, paymentNo: e.target.value }))}
            />
          </label>
          <button type="button" className="secondary" onClick={() => void loadPayments()}>
            조회
          </button>
        </div>
        {loadingPayments ? (
          <p>불러오는 중…</p>
        ) : payments.length === 0 ? (
          <p className="hint">지급 내역이 없습니다.</p>
        ) : (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>지급번호</th>
                  <th>지급일</th>
                  <th>거래처</th>
                  <th>구분</th>
                  <th className="num">공급가</th>
                  <th className="num">부가세</th>
                  <th className="num">총액</th>
                  <th>결제수단</th>
                  <th>상태</th>
                  <th />
                </tr>
              </thead>
              <tbody>
                {payments.map((payment) => (
                  <tr key={payment.id}>
                    <td>{payment.paymentNo}</td>
                    <td>{payment.paymentDate}</td>
                    <td>{payment.partnerName}</td>
                    <td>{payment.costCategoryLabel}</td>
                    <td className="num">{formatAmount(payment.supplyAmount)}</td>
                    <td className="num">{formatAmount(payment.vatAmount)}</td>
                    <td className="num">{formatAmount(payment.totalAmount)}</td>
                    <td>{payment.paymentMethod ?? '-'}</td>
                    <td>{payment.statusLabel}</td>
                    <td>
                      {payment.cancelable && (
                        <button
                          type="button"
                          className="secondary"
                          disabled={submitting}
                          onClick={() => void onCancel(payment)}
                        >
                          취소
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
