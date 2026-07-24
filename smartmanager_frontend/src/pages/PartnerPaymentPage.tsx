import { useCallback, useEffect, useMemo, useState } from 'react';
import {
  cancelPartnerPayment,
  createPartnerPayment,
  fetchPartnerPaymentCandidates,
  fetchPartnerPayments,
  fetchPrepaidBalances,
  fetchPrepaidOrderLineCandidates,
  type PartnerPayment,
  type PartnerPaymentCandidate,
  type PartnerPaymentCandidateParams,
  type PartnerPaymentCostCategory,
  type PartnerPaymentKind,
  type PartnerPaymentListParams,
  type PrepaidBalance,
  type PrepaidOrderLineCandidate,
} from '../api/partnerPayment';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import CompanySearchField, {
  PURCHASE_OUTSOURCE_PARTNER_ROLES,
  type CompanySearchSelection,
} from '../components/CompanySearchField';
import { fetchUnitPrices, type CostType, type UnitPrice } from '../api/unitPrice';
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

function isEffectiveUnitPrice(price: UnitPrice, refDate: string): boolean {
  if (price.beginDate > refDate) return false;
  if (price.endDate && price.endDate < refDate) return false;
  return true;
}

/** 거래처 단가에서 품목 후보를 중복 제거해 추출 */
function itemsFromPartnerUnitPrices(
  prices: UnitPrice[],
  partnerId: number,
  refDate: string,
): ItemSearchSelection[] {
  const byItemId = new Map<number, ItemSearchSelection>();
  for (const price of prices) {
    if (price.companyId !== partnerId || !isEffectiveUnitPrice(price, refDate)) {
      continue;
    }
    if (byItemId.has(price.itemId)) {
      continue;
    }
    byItemId.set(price.itemId, {
      id: price.itemId,
      itemNo: price.itemNum,
      itemName: price.itemName,
    });
  }
  return [...byItemId.values()].sort((a, b) => a.itemNo.localeCompare(b.itemNo, 'ko'));
}

const PAYMENT_METHODS = ['현금', '계좌이체', '어음', '카드', '기타'];

type PrepaidLineDraft = {
  key: string;
  item: ItemSearchSelection | null;
  supplyAmount: string;
  vatAmount: string;
  totalAmount: string;
  purchaseOrderLineId?: number | null;
  outsourcingOrderLineId?: number | null;
  orderNo?: string | null;
  remainingLimit?: number | null;
};

function defaultCostCategory(row: PartnerPaymentCandidate): PartnerPaymentCostCategory {
  if (row.purchasePayableAmount > 0 && row.outsourcePayableAmount <= 0) return 'PURCHASE';
  if (row.outsourcePayableAmount > 0 && row.purchasePayableAmount <= 0) return 'OUTSOURCE';
  return 'PURCHASE';
}

function newPrepaidLine(): PrepaidLineDraft {
  return {
    key: `${Date.now()}-${Math.random().toString(36).slice(2, 8)}`,
    item: null,
    supplyAmount: '',
    vatAmount: '0',
    totalAmount: '',
  };
}

export default function PartnerPaymentPage() {
  const confirm = useConfirm();
  const [paymentKind, setPaymentKind] = useState<PartnerPaymentKind>('NORMAL');
  const [candidates, setCandidates] = useState<PartnerPaymentCandidate[]>([]);
  const [payments, setPayments] = useState<PartnerPayment[]>([]);
  const [prepaidBalances, setPrepaidBalances] = useState<PrepaidBalance[]>([]);
  const [filters, setFilters] = useState<PartnerPaymentCandidateParams>({});
  const [listFilters, setListFilters] = useState<PartnerPaymentListParams>(() => ({
    paymentDateFrom: addDaysIso(todayIso(), -30),
    paymentDateTo: todayIso(),
    excludeCancelled: true,
  }));
  const [selectedPartnerId, setSelectedPartnerId] = useState<number | null>(null);
  const [prepaidPartner, setPrepaidPartner] = useState<CompanySearchSelection | null>(null);
  const [prepaidPartnerSummary, setPrepaidPartnerSummary] = useState<PartnerPaymentCandidate | null>(null);
  const [partnerClearToken, setPartnerClearToken] = useState(0);
  const [costCategory, setCostCategory] = useState<PartnerPaymentCostCategory>('PURCHASE');
  const [paymentDate, setPaymentDate] = useState(todayIso());
  const [supplyAmount, setSupplyAmount] = useState('');
  const [vatAmount, setVatAmount] = useState('0');
  const [totalAmountInput, setTotalAmountInput] = useState('');
  const [paymentMethod, setPaymentMethod] = useState('계좌이체');
  const [remark, setRemark] = useState('');
  const [prepaidLines, setPrepaidLines] = useState<PrepaidLineDraft[]>([newPrepaidLine()]);
  const [partnerPriceItems, setPartnerPriceItems] = useState<ItemSearchSelection[]>([]);
  const [loadingPartnerItems, setLoadingPartnerItems] = useState(false);
  const [itemClearToken, setItemClearToken] = useState(0);
  const [orderLinkLineKey, setOrderLinkLineKey] = useState<string | null>(null);
  const [orderCandidates, setOrderCandidates] = useState<PrepaidOrderLineCandidate[]>([]);
  const [orderSearch, setOrderSearch] = useState({ orderNo: '', itemNo: '' });
  const [loadingCandidates, setLoadingCandidates] = useState(true);
  const [loadingPayments, setLoadingPayments] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [candidateError, setCandidateError] = useState<string | null>(null);
  const [paymentError, setPaymentError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);
  const [balancesOpen, setBalancesOpen] = useState(true);

  const loadCandidates = useCallback(async () => {
    if (paymentKind === 'PREPAID') {
      // 선지급은 CompanySearchField로 거래처를 고르므로 미지급 후보 테이블을 쓰지 않는다.
      setCandidates([]);
      setLoadingCandidates(false);
      setCandidateError(null);
      return;
    }
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
  }, [filters, paymentKind]);

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

  const loadPrepaidBalances = useCallback(async (partnerId: number | null) => {
    if (partnerId == null) {
      setPrepaidBalances([]);
      return;
    }
    try {
      setPrepaidBalances(await fetchPrepaidBalances({ partnerId }));
    } catch {
      setPrepaidBalances([]);
    }
  }, []);

  useEffect(() => {
    void loadCandidates();
  }, [loadCandidates]);

  useEffect(() => {
    void loadPayments();
  }, [loadPayments]);

  useEffect(() => {
    void loadPrepaidBalances(selectedPartnerId);
  }, [selectedPartnerId, loadPrepaidBalances]);

  useEffect(() => {
    if (paymentKind !== 'PREPAID' || selectedPartnerId == null) {
      setPartnerPriceItems([]);
      setLoadingPartnerItems(false);
      return;
    }
    let cancelled = false;
    const costType: CostType = costCategory === 'OUTSOURCE' ? 'OUTSOURCE' : 'PURCHASE';
    setLoadingPartnerItems(true);
    void fetchUnitPrices(costType)
      .then((prices) => {
        if (cancelled) return;
        setPartnerPriceItems(itemsFromPartnerUnitPrices(prices, selectedPartnerId, paymentDate));
      })
      .catch(() => {
        if (!cancelled) setPartnerPriceItems([]);
      })
      .finally(() => {
        if (!cancelled) setLoadingPartnerItems(false);
      });
    return () => {
      cancelled = true;
    };
  }, [paymentKind, selectedPartnerId, costCategory, paymentDate]);

  useEffect(() => {
    if (paymentKind !== 'PREPAID') return;
    const allowedIds = new Set(partnerPriceItems.map((item) => item.id));
    setPrepaidLines((rows) => {
      let changed = false;
      const next = rows.map((row) => {
        if (
          row.item == null
          || allowedIds.has(row.item.id)
          || row.purchaseOrderLineId
          || row.outsourcingOrderLineId
        ) {
          return row;
        }
        changed = true;
        return {
          ...row,
          item: null,
          purchaseOrderLineId: null,
          outsourcingOrderLineId: null,
          orderNo: null,
          remainingLimit: null,
        };
      });
      return changed ? next : rows;
    });
  }, [partnerPriceItems, paymentKind]);

  useEffect(() => {
    if (paymentKind !== 'PREPAID') return;
    setItemClearToken((token) => token + 1);
  }, [selectedPartnerId, costCategory, paymentKind]);

  const selectedCandidate = useMemo(
    () =>
      paymentKind === 'PREPAID'
        ? prepaidPartnerSummary
        : (candidates.find((row) => row.partnerId === selectedPartnerId) ?? null),
    [paymentKind, prepaidPartnerSummary, candidates, selectedPartnerId],
  );

  const applyBreakdown = (supply: number, vat: number, total: number) => {
    setSupplyAmount(formatMoneyInput(supply));
    setVatAmount(formatMoneyInput(vat));
    setTotalAmountInput(formatMoneyInput(total));
  };

  const onSupplyAmountChange = (raw: string) => {
    setSupplyAmount(raw);
    if (raw.trim() === '') {
      setVatAmount('0');
      setTotalAmountInput('');
      return;
    }
    const supply = parseAmount(raw);
    if (supply == null) return;
    const next = breakdownFromSupply(supply);
    setVatAmount(formatMoneyInput(next.vat));
    setTotalAmountInput(formatMoneyInput(next.total));
  };

  const onSupplyAmountBlur = () => {
    if (supplyAmount.trim() === '') return;
    const supply = parseAmount(supplyAmount);
    if (supply == null) return;
    const next = breakdownFromSupply(supply);
    applyBreakdown(next.supply, next.vat, next.total);
  };

  const onTotalAmountChange = (raw: string) => {
    setTotalAmountInput(raw);
    if (raw.trim() === '') {
      setSupplyAmount('');
      setVatAmount('0');
      return;
    }
    const total = parseAmount(raw);
    if (total == null) return;
    const next = breakdownFromTotal(total);
    setSupplyAmount(formatMoneyInput(next.supply));
    setVatAmount(formatMoneyInput(next.vat));
  };

  const onTotalAmountBlur = () => {
    if (totalAmountInput.trim() === '') return;
    const total = parseAmount(totalAmountInput);
    if (total == null) return;
    const next = breakdownFromTotal(total);
    applyBreakdown(next.supply, next.vat, next.total);
  };

  const updatePrepaidLine = (key: string, patch: Partial<PrepaidLineDraft>) => {
    setPrepaidLines((rows) => rows.map((row) => (row.key === key ? { ...row, ...patch } : row)));
  };

  const onPrepaidSupplyChange = (key: string, raw: string) => {
    const supply = parseAmount(raw);
    if (supply == null) {
      updatePrepaidLine(key, { supplyAmount: raw });
      return;
    }
    const next = breakdownFromSupply(supply);
    updatePrepaidLine(key, {
      supplyAmount: raw,
      vatAmount: formatMoneyInput(next.vat),
      totalAmount: formatMoneyInput(next.total),
    });
  };

  const onSelectPartner = (row: PartnerPaymentCandidate) => {
    setSelectedPartnerId(row.partnerId);
    setCostCategory(defaultCostCategory(row));
    if (paymentKind === 'NORMAL') {
      const next = breakdownFromSupply(row.unpaidAmount);
      applyBreakdown(next.supply, next.vat, next.total);
    }
    setPaymentError(null);
  };

  const onSelectPrepaidPartner = async (company: CompanySearchSelection | null) => {
    setPrepaidPartner(company);
    setPaymentError(null);
    if (company == null) {
      setSelectedPartnerId(null);
      setPrepaidPartnerSummary(null);
      return;
    }
    setSelectedPartnerId(company.id);
    // 선택 즉시 기본 정보 표시 후, 잔액은 API로 보강
    setPrepaidPartnerSummary({
      partnerId: company.id,
      partnerName: company.companyName,
      partnerBusinessRegNo: company.businessRegNo,
      purchasePayableAmount: 0,
      outsourcePayableAmount: 0,
      totalPayableAmount: 0,
      paidAmount: 0,
      unpaidAmount: 0,
      payable: false,
    });
    try {
      const rows = await fetchPartnerPaymentCandidates({
        includeZeroUnpaid: true,
      });
      const match = rows.find((row) => row.partnerId === company.id);
      if (match) {
        setPrepaidPartnerSummary(match);
        setCostCategory(defaultCostCategory(match));
      }
    } catch {
      // 기본 정보(상호·사업자번호)는 이미 표시됨
    }
  };

  const openOrderLink = async (lineKey: string) => {
    if (selectedPartnerId == null) {
      setPaymentError('발주라인 연결 전 거래처를 선택하세요.');
      return;
    }
    setOrderLinkLineKey(lineKey);
    setOrderSearch({ orderNo: '', itemNo: '' });
    try {
      setOrderCandidates(
        await fetchPrepaidOrderLineCandidates({
          partnerId: selectedPartnerId,
          costCategory,
        }),
      );
    } catch (e) {
      setPaymentError(e instanceof Error ? e.message : '발주라인 조회 실패');
      setOrderCandidates([]);
    }
  };

  const searchOrderLines = async () => {
    if (selectedPartnerId == null) return;
    try {
      setOrderCandidates(
        await fetchPrepaidOrderLineCandidates({
          partnerId: selectedPartnerId,
          costCategory,
          orderNo: orderSearch.orderNo || undefined,
          itemNo: orderSearch.itemNo || undefined,
        }),
      );
    } catch (e) {
      setPaymentError(e instanceof Error ? e.message : '발주라인 조회 실패');
    }
  };

  const applyOrderLine = (candidate: PrepaidOrderLineCandidate) => {
    if (!orderLinkLineKey) return;
    const next = breakdownFromSupply(candidate.remainingAmount);
    updatePrepaidLine(orderLinkLineKey, {
      item: {
        id: candidate.itemId,
        itemNo: candidate.itemNo,
        itemName: candidate.itemName,
      },
      purchaseOrderLineId: candidate.costCategory === 'PURCHASE' ? candidate.orderLineId : null,
      outsourcingOrderLineId: candidate.costCategory === 'OUTSOURCE' ? candidate.orderLineId : null,
      orderNo: candidate.orderNo,
      remainingLimit: candidate.remainingAmount,
      supplyAmount: formatMoneyInput(next.supply),
      vatAmount: formatMoneyInput(next.vat),
      totalAmount: formatMoneyInput(next.total),
    });
    setCostCategory(candidate.costCategory);
    setOrderLinkLineKey(null);
  };

  const onCreatePayment = async () => {
    if (selectedPartnerId == null) {
      setPaymentError('지급할 거래처를 선택하세요.');
      return;
    }

    setSubmitting(true);
    setPaymentError(null);
    setMessage(null);
    try {
      if (paymentKind === 'PREPAID') {
        const lines = prepaidLines
          .filter((line) => line.item != null)
          .map((line) => {
            const supply = parseAmount(line.supplyAmount);
            const vat = parseAmount(line.vatAmount) ?? 0;
            if (line.item == null || supply == null || supply <= 0) {
              throw new Error('선지급 라인의 품목과 공급가를 확인해 주세요.');
            }
            return {
              itemId: line.item.id,
              purchaseOrderLineId: line.purchaseOrderLineId ?? null,
              outsourcingOrderLineId: line.outsourcingOrderLineId ?? null,
              supplyAmount: supply,
              vatAmount: vat,
            };
          });
        if (lines.length === 0) {
          throw new Error('선지급 품목 라인을 1건 이상 입력하세요.');
        }
        const created = await createPartnerPayment({
          partnerId: selectedPartnerId,
          paymentDate,
          costCategory,
          paymentKind: 'PREPAID',
          paymentMethod: paymentMethod || undefined,
          remark: remark.trim() || undefined,
          lines,
        });
        const detail = (created.lines ?? [])
          .map((line) => `· ${line.itemNo} 선급 ${formatAmount(line.totalAmount)}`)
          .join('\n');
        setMessage(
          `선지급 ${created.paymentNo} 등록 — ${created.lines?.length ?? 0}품목 / 총 ${formatAmount(created.totalAmount)}\n${detail}`,
        );
        setPrepaidLines([newPrepaidLine()]);
      } else {
        const supply = parseAmount(supplyAmount);
        if (supply == null || supply <= 0) {
          throw new Error('공급가를 입력하세요.');
        }
        const vat = parseAmount(vatAmount) ?? 0;
        if (selectedCandidate && supply > selectedCandidate.unpaidAmount) {
          const diff = supply - selectedCandidate.unpaidAmount;
          const ok = await confirm(
            `지급해야할 금액보다 큽니다.(${formatAmount(diff)}원) 그래도 지급하시겠습니까?`,
            { title: '지급 금액 확인', confirmLabel: '지급', cancelLabel: '닫기' },
          );
          if (!ok) {
            setSubmitting(false);
            return;
          }
        }
        const created = await createPartnerPayment({
          partnerId: selectedPartnerId,
          paymentDate,
          costCategory,
          paymentKind: 'NORMAL',
          supplyAmount: supply,
          vatAmount: vat,
          paymentMethod: paymentMethod || undefined,
          remark: remark.trim() || undefined,
        });
        setMessage(`지급 ${created.paymentNo}을(를) 등록했습니다.`);
        setSupplyAmount('');
        setVatAmount('0');
        setTotalAmountInput('');
      }
      setSelectedPartnerId(null);
      setPrepaidPartner(null);
      setPrepaidPartnerSummary(null);
      setPartnerClearToken((token) => token + 1);
      setRemark('');
      await loadCandidates();
      await loadPayments();
      await loadPrepaidBalances(null);
    } catch (e) {
      setPaymentError(e instanceof Error ? e.message : '지급 등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancel = async (payment: PartnerPayment) => {
    if (
      !(await confirm(`지급 ${payment.paymentNo}을(를) 취소하시겠습니까?`, {
        title: '취소 확인',
        confirmLabel: '예, 취소',
        cancelLabel: '닫기',
        danger: true,
      }))
    ) {
      return;
    }
    setSubmitting(true);
    setPaymentError(null);
    try {
      await cancelPartnerPayment(payment.id);
      setMessage(`지급 ${payment.paymentNo}을(를) 취소했습니다.`);
      await loadCandidates();
      await loadPayments();
      if (selectedPartnerId != null) await loadPrepaidBalances(selectedPartnerId);
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
        <p>구매·외주 미지급 정리 및 품목 단위 선지급</p>
      </header>

      {message && <p className="success-banner" style={{ whiteSpace: 'pre-wrap' }}>{message}</p>}
      {paymentError && <p className="error-banner">{paymentError}</p>}

      <section className="panel">
        <h2>등록</h2>
        <div className="filter-row">
          <label>
            등록 모드
            <select
              value={paymentKind}
              onChange={(e) => {
                const next = e.target.value as PartnerPaymentKind;
                setPaymentKind(next);
                setSelectedPartnerId(null);
                setPrepaidPartner(null);
                setPrepaidPartnerSummary(null);
                setPartnerClearToken((token) => token + 1);
                setSupplyAmount('');
                setVatAmount('0');
                setTotalAmountInput('');
                setPrepaidLines([newPrepaidLine()]);
              }}
              disabled={submitting}
            >
              <option value="NORMAL">일반지급</option>
              <option value="PREPAID">선지급</option>
            </select>
          </label>
        </div>

        <h3>거래처·잔액</h3>
        {paymentKind === 'PREPAID' ? (
          <>
            <p className="hint">
              선지급은 미지급 잔액이 없어도 구매·외주 거래처를 선택해 등록할 수 있습니다.
            </p>
            <div className="filter-row">
              <CompanySearchField
                label="거래처"
                partnerTypes={PURCHASE_OUTSOURCE_PARTNER_ROLES}
                selectedCompany={prepaidPartner}
                onSelect={(company) => void onSelectPrepaidPartner(company)}
                clearToken={partnerClearToken}
                placeholder="상호 또는 사업자번호 입력 — 미지급 없어도 선택 가능"
              />
            </div>
            {prepaidPartner && prepaidPartnerSummary && (
              <div className="table-wrap" style={{ marginTop: 12 }}>
                <table>
                  <thead>
                    <tr>
                      <th>거래처</th>
                      <th>사업자번호</th>
                      <th className="num">구매발생</th>
                      <th className="num">외주발생</th>
                      <th className="num">지급합계(공급가)</th>
                      <th className="num">미지급잔액(보정)</th>
                      <th className="num">구매단가 품목</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr className="row-selected">
                      <td>{prepaidPartnerSummary.partnerName}</td>
                      <td>{prepaidPartnerSummary.partnerBusinessRegNo}</td>
                      <td className="num">{formatAmount(prepaidPartnerSummary.purchasePayableAmount)}</td>
                      <td className="num">{formatAmount(prepaidPartnerSummary.outsourcePayableAmount)}</td>
                      <td className="num">{formatAmount(prepaidPartnerSummary.paidAmount)}</td>
                      <td className="num">{formatAmount(prepaidPartnerSummary.unpaidAmount)}</td>
                      <td className="num">
                        {loadingPartnerItems ? '…' : partnerPriceItems.length}
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            )}
            {prepaidPartner && !prepaidPartnerSummary && (
              <p className="hint">거래처 잔액 정보를 불러오는 중…</p>
            )}
          </>
        ) : (
          <>
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
                      <th className="num">지급합계(공급가)</th>
                      <th className="num">미지급잔액(보정)</th>
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
                            disabled={submitting || !row.payable}
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
          </>
        )}

        {paymentKind === 'NORMAL' && selectedCandidate && (
          <p className="hint">
            선택: {selectedCandidate.partnerName} — 미지급(보정) {formatAmount(selectedCandidate.unpaidAmount)}
            {' · '}구매 {formatAmount(selectedCandidate.purchasePayableAmount)}
            {' · '}외주 {formatAmount(selectedCandidate.outsourcePayableAmount)}
          </p>
        )}

        {selectedPartnerId != null && paymentKind === 'PREPAID' && (
          <div style={{ marginTop: 12 }}>
            <button type="button" className="secondary" onClick={() => setBalancesOpen((v) => !v)}>
              품목별 선급 잔액 {balancesOpen ? '접기' : '펼치기'}
            </button>
            {balancesOpen && (
              prepaidBalances.length === 0 ? (
                <p className="hint">잔여 선급이 없습니다.</p>
              ) : (
                <div className="table-wrap">
                  <table>
                    <thead>
                      <tr>
                        <th>품목번호</th>
                        <th>품목명</th>
                        <th>구분</th>
                        <th className="num">선급잔액</th>
                      </tr>
                    </thead>
                    <tbody>
                      {prepaidBalances.map((row) => (
                        <tr key={`${row.itemId}-${row.costCategory}`}>
                          <td>{row.itemNo}</td>
                          <td>{row.itemName}</td>
                          <td>{row.costCategoryLabel}</td>
                          <td className="num">{formatAmount(row.prepaidRemaining)}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              )
            )}
          </div>
        )}

        <h3>지급 입력</h3>
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
        </div>

        {paymentKind === 'NORMAL' ? (
          <div className="action-bar">
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
              />
            </label>
            <label>
              부가세
              <input type="number" min={0} step="any" value={vatAmount} readOnly disabled />
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
        ) : (
          <>
            {selectedPartnerId != null && (
              <p className="hint" style={{ textAlign: 'left', marginBottom: 8 }}>
                {loadingPartnerItems
                  ? '거래처 단가 품목을 불러오는 중…'
                  : partnerPriceItems.length > 0
                    ? `선택 거래처 단가 품목 ${partnerPriceItems.length}건 — 품목란을 클릭하거나 글자를 입력해 선택하세요.`
                    : costCategory === 'OUTSOURCE'
                      ? '해당 거래처에 유효한 외주단가 품목이 없습니다.'
                      : '해당 거래처에 유효한 구매단가 품목이 없습니다.'}
              </p>
            )}
            <div className="prepaid-payment-lines">
              <table>
                <thead>
                  <tr>
                    <th>품목</th>
                    <th>발주연결</th>
                    <th className="num">공급가</th>
                    <th className="num">부가세</th>
                    <th className="num">총액</th>
                    <th />
                  </tr>
                </thead>
                <tbody>
                  {prepaidLines.map((line) => (
                    <tr key={line.key}>
                      <td className="prepaid-item-cell">
                        <ItemSearchField
                          label=""
                          selectedItem={line.item}
                          onSelect={(item) =>
                            updatePrepaidLine(line.key, {
                              item,
                              purchaseOrderLineId: null,
                              outsourcingOrderLineId: null,
                              orderNo: null,
                              remainingLimit: null,
                            })
                          }
                          items={partnerPriceItems}
                          clearToken={itemClearToken}
                          placeholder={
                            selectedPartnerId == null
                              ? '거래처 선택 후 품목 검색'
                              : loadingPartnerItems
                                ? '단가 품목 불러오는 중…'
                                : partnerPriceItems.length === 0
                                  ? '해당 거래처 단가 품목 없음'
                                  : '품목코드 또는 품목명 입력'
                          }
                          emptyMessage={
                            costCategory === 'OUTSOURCE'
                              ? '외주단가가 등록된 품목이 없습니다.'
                              : '구매단가가 등록된 품목이 없습니다.'
                          }
                          disabled={submitting || selectedPartnerId == null || loadingPartnerItems}
                        />
                      </td>
                      <td>
                        {line.orderNo ? (
                          <span>{line.orderNo}</span>
                        ) : (
                          <button
                            type="button"
                            className="secondary"
                            disabled={submitting || selectedPartnerId == null}
                            onClick={() => void openOrderLink(line.key)}
                          >
                            발주라인 연결
                          </button>
                        )}
                        {line.remainingLimit != null && (
                          <div className="hint">잔여 {formatAmount(line.remainingLimit)}</div>
                        )}
                      </td>
                      <td>
                        <input
                          type="number"
                          min={0}
                          step="any"
                          value={line.supplyAmount}
                          onChange={(e) => onPrepaidSupplyChange(line.key, e.target.value)}
                          disabled={submitting || selectedPartnerId == null}
                        />
                      </td>
                      <td className="num">{line.vatAmount || '0'}</td>
                      <td className="num">{line.totalAmount || '0'}</td>
                      <td>
                        <button
                          type="button"
                          className="secondary"
                          disabled={submitting || prepaidLines.length <= 1}
                          onClick={() => setPrepaidLines((rows) => rows.filter((r) => r.key !== line.key))}
                        >
                          삭제
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
            <div className="action-bar">
              <button
                type="button"
                className="secondary"
                disabled={submitting || selectedPartnerId == null}
                onClick={() => setPrepaidLines((rows) => [...rows, newPrepaidLine()])}
              >
                라인 추가
              </button>
              <button
                type="button"
                disabled={submitting || selectedPartnerId == null}
                onClick={() => void onCreatePayment()}
              >
                {submitting ? '등록 중…' : '선지급 등록'}
              </button>
            </div>
          </>
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
                  <th>종류</th>
                  <th>구분</th>
                  <th>품목/발주</th>
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
                    <td>{payment.paymentKindLabel ?? (payment.paymentKind === 'PREPAID' ? '선지급' : '일반')}</td>
                    <td>{payment.costCategoryLabel}</td>
                    <td>{payment.lineSummary ?? '-'}</td>
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

      {orderLinkLineKey && (
        <div className="modal-backdrop" role="presentation" onClick={() => setOrderLinkLineKey(null)}>
          <div
            className="modal-panel"
            role="dialog"
            aria-modal="true"
            onClick={(e) => e.stopPropagation()}
            style={{ maxWidth: 900, width: '92vw' }}
          >
            <header className="modal-header">
              <h2>발주라인 연결</h2>
              <button type="button" className="secondary" onClick={() => setOrderLinkLineKey(null)}>
                닫기
              </button>
            </header>
            <div className="filter-row">
              <label>
                발주번호
                <input
                  value={orderSearch.orderNo}
                  onChange={(e) => setOrderSearch((s) => ({ ...s, orderNo: e.target.value }))}
                />
              </label>
              <label>
                품목번호
                <input
                  value={orderSearch.itemNo}
                  onChange={(e) => setOrderSearch((s) => ({ ...s, itemNo: e.target.value }))}
                />
              </label>
              <button type="button" className="secondary" onClick={() => void searchOrderLines()}>
                조회
              </button>
            </div>
            <div className="table-wrap">
              <table>
                <thead>
                  <tr>
                    <th>발주번호</th>
                    <th>라인</th>
                    <th>품목</th>
                    <th>구분</th>
                    <th className="num">발주금액</th>
                    <th className="num">잔여</th>
                    <th />
                  </tr>
                </thead>
                <tbody>
                  {orderCandidates.map((row) => (
                    <tr key={`${row.costCategory}-${row.orderLineId}`}>
                      <td>{row.orderNo}</td>
                      <td>{row.lineNo}</td>
                      <td>
                        {row.itemNo} {row.itemName}
                      </td>
                      <td>{row.costCategoryLabel}</td>
                      <td className="num">{formatAmount(row.orderAmount)}</td>
                      <td className="num">{formatAmount(row.remainingAmount)}</td>
                      <td>
                        <button type="button" onClick={() => applyOrderLine(row)}>
                          선택
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
