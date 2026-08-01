import { useEffect, useState } from 'react';
import {
  approvePayableItems,
  previewPayableApproval,
  type ApproveOffsetResult,
  type PayableApprovalLedgerKind,
} from '../../api/payableApproval';
import { formatAmount } from '../../utils/numberFormat';

type ApprovalItem = { ledgerKind: PayableApprovalLedgerKind; historyId: number };

interface ApprovePreviewModalProps {
  open: boolean;
  items: ApprovalItem[];
  onClose: () => void;
  onApproved: (result: ApproveOffsetResult) => void;
}

export default function ApprovePreviewModal({
  open,
  items,
  onClose,
  onApproved,
}: ApprovePreviewModalProps) {
  const [preview, setPreview] = useState<ApproveOffsetResult | null>(null);
  const [loading, setLoading] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!open || items.length === 0) {
      setPreview(null);
      setError(null);
      return;
    }
    let cancelled = false;
    setLoading(true);
    setError(null);
    void previewPayableApproval(items)
      .then((result) => {
        if (!cancelled) setPreview(result);
      })
      .catch((e) => {
        if (!cancelled) setError(e instanceof Error ? e.message : '상계 미리보기 실패');
      })
      .finally(() => {
        if (!cancelled) setLoading(false);
      });
    return () => {
      cancelled = true;
    };
  }, [open, items]);

  if (!open) return null;

  const offsetRows = preview?.offsets.filter((row) => row.offsetApplicable) ?? [];
  const claimRows = preview?.offsets.filter((row) => !row.offsetApplicable) ?? [];

  const onExecute = async () => {
    setSubmitting(true);
    setError(null);
    try {
      const result = await approvePayableItems(items);
      onApproved(result);
    } catch (e) {
      setError(e instanceof Error ? e.message : '승인 처리 실패');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="modal-backdrop" role="presentation" onClick={onClose}>
      <div
        className="modal-panel"
        role="dialog"
        aria-modal="true"
        aria-labelledby="approve-preview-title"
        onClick={(e) => e.stopPropagation()}
        style={{ maxWidth: 960, width: '92vw' }}
      >
        <header className="modal-header">
          <h2 id="approve-preview-title">승인 · 선급 상계 미리보기</h2>
          <button type="button" className="secondary" onClick={onClose} disabled={submitting}>
            닫기
          </button>
        </header>

        {error && <p className="error-banner">{error}</p>}
        {loading ? (
          <p>미리보기 조회 중…</p>
        ) : preview ? (
          <>
            <p className="hint">
              승인 {preview.itemCount}건 · 상계합 {formatAmount(preview.totalOffsetAmount)} · 신규미지급합{' '}
              {formatAmount(preview.totalUnpaidIncrease)}
            </p>

            <h3>상계 대상 (구매/외주)</h3>
            {offsetRows.length === 0 ? (
              <p className="hint">선급 상계 대상이 없습니다.</p>
            ) : (
              <div className="table-wrap">
                <table>
                  <thead>
                    <tr>
                      <th>거래처</th>
                      <th>품목</th>
                      <th>구분</th>
                      <th className="num">승인액</th>
                      <th className="num">상계</th>
                      <th className="num">잔여선급</th>
                      <th className="num">신규미지급</th>
                    </tr>
                  </thead>
                  <tbody>
                    {offsetRows.map((row) => (
                      <tr key={`${row.ledgerKind}-${row.historyId}`}>
                        <td>{row.partnerName}</td>
                        <td>
                          {row.itemNo} {row.itemName}
                        </td>
                        <td>{row.costCategoryLabel ?? '-'}</td>
                        <td className="num">{formatAmount(row.approveAmount)}</td>
                        <td className="num">{formatAmount(row.offsetAmount)}</td>
                        <td className="num">{formatAmount(row.prepaidAfter)}</td>
                        <td className="num">{formatAmount(row.unpaidIncrease)}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}

            {claimRows.length > 0 && (
              <>
                <h3>공제 (상계 비대상)</h3>
                <div className="table-wrap">
                  <table>
                    <thead>
                      <tr>
                        <th>거래처</th>
                        <th>구분</th>
                        <th className="num">금액</th>
                      </tr>
                    </thead>
                    <tbody>
                      {claimRows.map((row) => (
                        <tr key={`${row.ledgerKind}-${row.historyId}`}>
                          <td>{row.partnerName}</td>
                          <td>{row.ledgerKind === 'ETC_CLAIM' ? '기타공제' : '불량공제'}</td>
                          <td className="num">{formatAmount(row.approveAmount)}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              </>
            )}
          </>
        ) : null}

        <div className="action-bar" style={{ justifyContent: 'flex-end', marginTop: 16 }}>
          <button type="button" className="secondary" onClick={onClose} disabled={submitting}>
            닫기
          </button>
          <button type="button" onClick={() => void onExecute()} disabled={submitting || loading || !preview}>
            {submitting ? '승인 중…' : '승인 실행'}
          </button>
        </div>
      </div>
    </div>
  );
}
