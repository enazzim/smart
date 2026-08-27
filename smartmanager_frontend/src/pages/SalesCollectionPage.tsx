import { useCallback, useMemo, useState } from 'react';
import {
  cancelSalesCollection,
  createSalesCollection,
  fetchSalesCollectionCandidates,
  fetchSalesCollections,
  type SalesCollection,
  type SalesCollectionCandidate,
  type SalesCollectionCandidateParams,
  type SalesCollectionListParams,
} from '../api/salesCollection';
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

type CollectionListSearchForm = {
  collectionDateFrom: string;
  collectionDateTo: string;
  partnerName: string;
  collectionNo: string;
};

function emptyCollectionListSearch(): CollectionListSearchForm {
  return {
    collectionDateFrom: '',
    collectionDateTo: '',
    partnerName: '',
    collectionNo: '',
  };
}

function parseAmount(value: string): number | null {
  const parsed = Number(value);
  return Number.isFinite(parsed) && parsed >= 0 ? parsed : null;
}

const PAYMENT_METHODS = ['현금', '계좌이체', '어음', '카드', '기타'];

export default function SalesCollectionPage() {
  const confirm = useConfirm();
  const [candidates, setCandidates] = useState<SalesCollectionCandidate[]>([]);
  const [collections, setCollections] = useState<SalesCollection[]>([]);
  const [candidateSearchPartnerName, setCandidateSearchPartnerName] = useState('');
  const [hasSearchedCandidates, setHasSearchedCandidates] = useState(false);
  const [listSearch, setListSearch] = useState<CollectionListSearchForm>(() => emptyCollectionListSearch());
  const [hasSearchedList, setHasSearchedList] = useState(false);
  const [selectedPartnerId, setSelectedPartnerId] = useState<number | null>(null);
  const [collectionDate, setCollectionDate] = useState(todayIso());
  const [supplyAmount, setSupplyAmount] = useState('');
  const [vatAmount, setVatAmount] = useState('0');
  const [totalAmountInput, setTotalAmountInput] = useState('');
  const [paymentMethod, setPaymentMethod] = useState('계좌이체');
  const [remark, setRemark] = useState('');
  const [loadingCandidates, setLoadingCandidates] = useState(false);
  const [loadingCollections, setLoadingCollections] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [candidateError, setCandidateError] = useState<string | null>(null);
  const [collectionError, setCollectionError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const loadCandidates = useCallback(async (partnerName?: string) => {
    setLoadingCandidates(true);
    setCandidateError(null);
    setHasSearchedCandidates(true);
    try {
      const trimmed = partnerName?.trim();
      const params: SalesCollectionCandidateParams = trimmed ? { partnerName: trimmed } : {};
      setCandidates(await fetchSalesCollectionCandidates(params));
    } catch (e) {
      setCandidateError(e instanceof Error ? e.message : '미수 후보 조회 실패');
      setCandidates([]);
    } finally {
      setLoadingCandidates(false);
    }
  }, []);

  const buildListParams = useCallback(
    (): SalesCollectionListParams => ({
      collectionDateFrom: listSearch.collectionDateFrom || undefined,
      collectionDateTo: listSearch.collectionDateTo || undefined,
      partnerName: listSearch.partnerName || undefined,
      collectionNo: listSearch.collectionNo || undefined,
      excludeCancelled: true,
    }),
    [listSearch],
  );

  const loadCollections = useCallback(
    async (override?: SalesCollectionListParams) => {
      setLoadingCollections(true);
      setCollectionError(null);
      setHasSearchedList(true);
      try {
        setCollections(await fetchSalesCollections(override ?? buildListParams()));
      } catch (e) {
        setCollectionError(e instanceof Error ? e.message : '수금 목록 조회 실패');
        setCollections([]);
      } finally {
        setLoadingCollections(false);
      }
    },
    [buildListParams],
  );

  const handleSearchCandidates = () => {
    void loadCandidates(candidateSearchPartnerName);
  };

  const handleSearchList = () => {
    void loadCollections(buildListParams());
  };

  const handleResetListSearch = () => {
    setListSearch(emptyCollectionListSearch());
    setCollections([]);
    setCollectionError(null);
    setHasSearchedList(false);
  };

  const selectedCandidate = useMemo(
    () => candidates.find((row) => row.partnerId === selectedPartnerId) ?? null,
    [candidates, selectedPartnerId],
  );

  const totalAmount = useMemo(() => parseAmount(totalAmountInput) ?? 0, [totalAmountInput]);

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

  const onSelectPartner = (row: SalesCollectionCandidate) => {
    setSelectedPartnerId(row.partnerId);
    const next = breakdownFromTotal(row.uncollectedAmount);
    applyBreakdown(next.supply, next.vat, next.total);
    setCollectionError(null);
  };

  const onCreateCollection = async () => {
    if (selectedPartnerId == null) {
      setCollectionError('수금할 거래처를 선택하세요.');
      return;
    }
    const supply = parseAmount(supplyAmount);
    if (supply == null || supply <= 0) {
      setCollectionError('공급가를 입력하세요.');
      return;
    }
    const vat = parseAmount(vatAmount) ?? 0;
    if (selectedCandidate && totalAmount > selectedCandidate.uncollectedAmount) {
      setCollectionError(`수금 금액이 미수 잔액(${formatAmount(selectedCandidate.uncollectedAmount)})을 초과합니다.`);
      return;
    }

    setSubmitting(true);
    setCollectionError(null);
    setMessage(null);
    try {
      const created = await createSalesCollection({
        partnerId: selectedPartnerId,
        collectionDate,
        supplyAmount: supply,
        vatAmount: vat,
        paymentMethod: paymentMethod || undefined,
        remark: remark.trim() || undefined,
      });
      setMessage(`수금 ${created.collectionNo}을(를) 등록했습니다.`);
      setSelectedPartnerId(null);
      setSupplyAmount('');
      setVatAmount('0');
      setTotalAmountInput('');
      setRemark('');
      if (hasSearchedCandidates) {
        await loadCandidates(candidateSearchPartnerName);
      }
      if (hasSearchedList) {
        await loadCollections();
      }
    } catch (e) {
      setCollectionError(e instanceof Error ? e.message : '수금 등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancel = async (collection: SalesCollection) => {
    if (!(await confirm(`수금 ${collection.collectionNo}을(를) 취소하시겠습니까?`, { title: '취소 확인', confirmLabel: '예, 취소', cancelLabel: '닫기', danger: true }))) return;
    setSubmitting(true);
    setCollectionError(null);
    try {
      await cancelSalesCollection(collection.id);
      setMessage(`수금 ${collection.collectionNo}을(를) 취소했습니다.`);
      if (hasSearchedCandidates) {
        await loadCandidates(candidateSearchPartnerName);
      }
      if (hasSearchedList) {
        await loadCollections();
      }
    } catch (e) {
      setCollectionError(e instanceof Error ? e.message : '수금 취소 실패');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>수금</h1>
        <p>매출 잔액(미수)이 있는 수주거래처에 대해 수금을 등록합니다.</p>
      </header>

      {message && <p className="success-banner">{message}</p>}
      {collectionError && <p className="error-banner">{collectionError}</p>}

      <section className="panel">
        <h2>미수 거래처</h2>
        <div className="filter-row">
          <label>
            거래처
            <input
              type="text"
              value={candidateSearchPartnerName}
              onChange={(e) => setCandidateSearchPartnerName(e.target.value)}
            />
          </label>
          <button type="button" className="secondary" onClick={handleSearchCandidates} disabled={loadingCandidates}>
            조회
          </button>
        </div>
        {loadingCandidates ? (
          <p>불러오는 중…</p>
        ) : !hasSearchedCandidates ? (
          <p className="hint">거래처를 입력하거나 비운 뒤 조회를 눌러 주세요.</p>
        ) : candidateError ? (
          <p className="error-banner">{candidateError}</p>
        ) : candidates.length === 0 ? (
          <p className="hint">수금 가능한 미수 거래처가 없습니다.</p>
        ) : (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th />
                  <th>거래처</th>
                  <th>사업자번호</th>
                  <th className="num">매출합계</th>
                  <th className="num">수금합계</th>
                  <th className="num">미수잔액</th>
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
                        name="collection-partner"
                        aria-label={`${row.partnerName} 선택`}
                        checked={selectedPartnerId === row.partnerId}
                        disabled={!row.collectable || submitting}
                        onChange={() => onSelectPartner(row)}
                      />
                    </td>
                    <td>{row.partnerName}</td>
                    <td>{row.partnerBusinessRegNo}</td>
                    <td className="num">{formatAmount(row.revenueAmount)}</td>
                    <td className="num">{formatAmount(row.collectedAmount)}</td>
                    <td className="num">{formatAmount(row.uncollectedAmount)}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}

        <div className="action-bar">
          <label>
            수금일
            <input
              type="date"
              value={collectionDate}
              onChange={(e) => setCollectionDate(e.target.value)}
              disabled={submitting}
            />
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
            onClick={() => void onCreateCollection()}
          >
            {submitting ? '등록 중…' : '수금 등록'}
          </button>
        </div>
        {selectedCandidate && (
          <p className="hint">
            선택: {selectedCandidate.partnerName} — 미수 잔액 {formatAmount(selectedCandidate.uncollectedAmount)}
          </p>
        )}
      </section>

      <section className="panel">
        <h2>수금 목록</h2>
        <div className="filter-row">
          <label>
            수금일 From
            <input
              type="date"
              value={listSearch.collectionDateFrom}
              onChange={(e) => setListSearch((f) => ({ ...f, collectionDateFrom: e.target.value }))}
            />
          </label>
          <label>
            To
            <input
              type="date"
              value={listSearch.collectionDateTo}
              onChange={(e) => setListSearch((f) => ({ ...f, collectionDateTo: e.target.value }))}
            />
          </label>
          <label>
            거래처
            <input
              type="text"
              value={listSearch.partnerName}
              onChange={(e) => setListSearch((f) => ({ ...f, partnerName: e.target.value }))}
            />
          </label>
          <label>
            수금번호
            <input
              type="text"
              value={listSearch.collectionNo}
              onChange={(e) => setListSearch((f) => ({ ...f, collectionNo: e.target.value }))}
            />
          </label>
          <button type="button" className="secondary" onClick={handleSearchList} disabled={loadingCollections}>
            조회
          </button>
          <button type="button" className="secondary" onClick={handleResetListSearch} disabled={loadingCollections}>
            초기화
          </button>
        </div>
        {loadingCollections ? (
          <p>불러오는 중…</p>
        ) : !hasSearchedList ? (
          <p className="hint">검색 조건을 입력한 뒤 조회를 눌러 주세요.</p>
        ) : collections.length === 0 ? (
          <p className="hint">조회 결과가 없습니다.</p>
        ) : (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>수금번호</th>
                  <th>수금일</th>
                  <th>거래처</th>
                  <th className="num">공급가</th>
                  <th className="num">부가세</th>
                  <th className="num">총액</th>
                  <th>결제수단</th>
                  <th>상태</th>
                  <th />
                </tr>
              </thead>
              <tbody>
                {collections.map((collection) => (
                  <tr key={collection.id}>
                    <td>{collection.collectionNo}</td>
                    <td>{collection.collectionDate}</td>
                    <td>{collection.partnerName}</td>
                    <td className="num">{formatAmount(collection.supplyAmount)}</td>
                    <td className="num">{formatAmount(collection.vatAmount)}</td>
                    <td className="num">{formatAmount(collection.totalAmount)}</td>
                    <td>{collection.paymentMethod ?? '-'}</td>
                    <td>{collection.statusLabel}</td>
                    <td>
                      {collection.cancelable && (
                        <button
                          type="button"
                          className="secondary"
                          disabled={submitting}
                          onClick={() => void onCancel(collection)}
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
