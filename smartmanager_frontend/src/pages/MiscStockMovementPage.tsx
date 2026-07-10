import { useCallback, useEffect, useMemo, useState } from 'react';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import type { PropertyClassification } from '../api/item';
import {
  cancelMiscStockMovement,
  createMiscStockMovement,
  fetchMiscStockMovementPreview,
  fetchMiscStockMovements,
  updateMiscStockMovement,
  type MiscStockMovementDirection,
  type MiscStockMovementPreview,
  type MiscStockMovementRow,
} from '../api/miscStockMovement';
import { fetchSmallPublicCodes, type PublicCodeSmall } from '../api/publicCode';
import { fetchProcessPlans, type ProcessPlan } from '../api/process';

const ALL_ITEM_CLASSES: PropertyClassification[] = ['원자재', '제품', '상품', '공정품'];

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function addDaysIso(iso: string, days: number): string {
  const date = new Date(`${iso}T00:00:00`);
  date.setDate(date.getDate() + days);
  return date.toISOString().slice(0, 10);
}

function formatQty(value: number): string {
  return Number.isInteger(value) ? String(value) : value.toLocaleString(undefined, { maximumFractionDigits: 4 });
}

function formatProcessLabel(process: ProcessPlan): string {
  return `${process.processSequenceNum} · ${process.processCode} ${process.processName}`;
}

export default function MiscStockMovementPage() {
  const [reasonOptions, setReasonOptions] = useState<PublicCodeSmall[]>([]);
  const [processOptions, setProcessOptions] = useState<ProcessPlan[]>([]);
  const [preview, setPreview] = useState<MiscStockMovementPreview | null>(null);
  const [rows, setRows] = useState<MiscStockMovementRow[]>([]);

  const [editingId, setEditingId] = useState<number | null>(null);
  const [selectedItem, setSelectedItem] = useState<ItemSearchSelection | null>(null);
  const [processSequenceId, setProcessSequenceId] = useState<number | ''>('');
  const [movementDirection, setMovementDirection] = useState<MiscStockMovementDirection>('IN');
  const [qty, setQty] = useState('');
  const [reasonCodeId, setReasonCodeId] = useState<number | ''>('');
  const [note, setNote] = useState('');
  const [movementDate, setMovementDate] = useState(todayIso());

  const [filterItemNo, setFilterItemNo] = useState('');
  const [filterItemName, setFilterItemName] = useState('');
  const [filterDateFrom, setFilterDateFrom] = useState(addDaysIso(todayIso(), -30));
  const [filterDateTo, setFilterDateTo] = useState(todayIso());

  const [loadingHistory, setLoadingHistory] = useState(true);
  const [loadingPreview, setLoadingPreview] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const processRequired = useMemo(() => {
    const cls = selectedItem?.propertyClassification;
    return cls === '공정품' || cls === '제품';
  }, [selectedItem]);

  useEffect(() => {
    void fetchSmallPublicCodes('1500')
      .then(setReasonOptions)
      .catch(() => setReasonOptions([]));
  }, []);

  const loadProcessOptions = useCallback(async (item: ItemSearchSelection | null, preferredProcessId?: number | null) => {
    if (!item || (item.propertyClassification !== '공정품' && item.propertyClassification !== '제품')) {
      setProcessOptions([]);
      setProcessSequenceId('');
      return;
    }
    const processes = await fetchProcessPlans(item.id);
    setProcessOptions(processes);
    if (preferredProcessId != null && processes.some((process) => process.id === preferredProcessId)) {
      setProcessSequenceId(preferredProcessId);
    } else if (processes.length === 1) {
      setProcessSequenceId(processes[0].id);
    } else {
      setProcessSequenceId('');
    }
  }, []);

  const loadPreview = useCallback(async () => {
    if (!selectedItem) {
      setPreview(null);
      return;
    }
    if (processRequired && processSequenceId === '') {
      setPreview(null);
      return;
    }
    setLoadingPreview(true);
    try {
      setPreview(
        await fetchMiscStockMovementPreview(
          selectedItem.id,
          processSequenceId === '' ? null : processSequenceId,
          movementDate,
        ),
      );
    } catch (e) {
      setPreview(null);
      setError(e instanceof Error ? e.message : '현재고 조회 실패');
    } finally {
      setLoadingPreview(false);
    }
  }, [selectedItem, processRequired, processSequenceId, movementDate]);

  const loadHistory = useCallback(async () => {
    setLoadingHistory(true);
    setError(null);
    try {
      setRows(
        await fetchMiscStockMovements({
          itemNo: filterItemNo || undefined,
          itemName: filterItemName || undefined,
          movementDateFrom: filterDateFrom || undefined,
          movementDateTo: filterDateTo || undefined,
        }),
      );
    } catch (e) {
      setError(e instanceof Error ? e.message : '이력 조회 실패');
      setRows([]);
    } finally {
      setLoadingHistory(false);
    }
  }, [filterItemNo, filterItemName, filterDateFrom, filterDateTo]);

  useEffect(() => {
    void loadHistory();
  }, [loadHistory]);

  useEffect(() => {
    void loadPreview();
  }, [loadPreview]);

  const onItemSelect = (item: ItemSearchSelection | null) => {
    setSelectedItem(item);
    setProcessSequenceId('');
    setPreview(null);
    void loadProcessOptions(item);
  };

  const resetForm = () => {
    setEditingId(null);
    setSelectedItem(null);
    setProcessSequenceId('');
    setProcessOptions([]);
    setPreview(null);
    setMovementDirection('IN');
    setQty('');
    setReasonCodeId('');
    setNote('');
    setMovementDate(todayIso());
  };

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!selectedItem) {
      setError('품목을 선택해 주세요.');
      return;
    }
    if (processRequired && processSequenceId === '') {
      setError('공정을 선택해 주세요.');
      return;
    }
    const parsedQty = Number(qty);
    if (!Number.isFinite(parsedQty) || parsedQty <= 0) {
      setError('수량은 0보다 커야 합니다.');
      return;
    }

    const payload = {
      movementDate,
      movementDirection,
      itemId: selectedItem.id,
      processSequenceId: processSequenceId === '' ? null : processSequenceId,
      qty: parsedQty,
      reasonCodeId: reasonCodeId === '' ? null : reasonCodeId,
      note: note.trim() || undefined,
    };

    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      if (editingId != null) {
        await updateMiscStockMovement(editingId, payload);
        setMessage('입출고가 수정되었습니다.');
      } else {
        await createMiscStockMovement(payload);
        setMessage('입출고가 등록되었습니다.');
      }
      resetForm();
      await loadHistory();
    } catch (err) {
      setError(err instanceof Error ? err.message : editingId != null ? '입출고 수정 실패' : '입출고 등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onEditRow = (row: MiscStockMovementRow) => {
    if (row.status !== 'REGISTERED') {
      return;
    }
    setEditingId(row.id);
    setSelectedItem({
      id: row.itemId,
      itemNo: row.itemNo,
      itemName: row.itemName,
      propertyClassification: row.propertyClassification,
    });
    void loadProcessOptions(
      {
        id: row.itemId,
        itemNo: row.itemNo,
        itemName: row.itemName,
        propertyClassification: row.propertyClassification,
      },
      row.outputProcessId,
    );
    setMovementDirection(row.movementDirection);
    setQty(String(row.qty));
    setReasonCodeId(row.reasonCodeId ?? '');
    setNote(row.note ?? '');
    setMovementDate(row.movementDate);
    setError(null);
    setMessage(null);
  };

  const onDeleteRow = async (row: MiscStockMovementRow) => {
    if (row.status !== 'REGISTERED') {
      return;
    }
    if (!window.confirm(`${row.movementNo} 건을 삭제하시겠습니까?\n재고 반영이 취소됩니다.`)) {
      return;
    }
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      await cancelMiscStockMovement(row.id);
      if (editingId === row.id) {
        resetForm();
      }
      setMessage('입출고가 삭제되었습니다.');
      await loadHistory();
    } catch (err) {
      setError(err instanceof Error ? err.message : '삭제 실패');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>기타 입출고</h1>
        <p>
          품목 재고분류에 따라 창고가 자동 결정됩니다. 이력이 없는 품목은 해당 슬롯을 생성한 뒤 입출고를 반영합니다.
        </p>
      </header>

      {error && <div className="error">{error}</div>}
      {message && <div className="success">{message}</div>}

      <section className="panel">
        <h2>{editingId != null ? `입출고 수정 (${editingId})` : '입출고 등록'}</h2>
        <form onSubmit={(e) => void onSubmit(e)} className="misc-movement-form">
          <div className="misc-movement-row misc-movement-row--main">
            <label className="misc-field-direction">
              구분
              <select
                value={movementDirection}
                onChange={(e) => setMovementDirection(e.target.value as MiscStockMovementDirection)}
                required
              >
                <option value="IN">입고</option>
                <option value="OUT">출고</option>
              </select>
            </label>

            <div className="misc-field-item">
              <ItemSearchField
                label="품목"
                selectedItem={selectedItem}
                onSelect={onItemSelect}
                allowedClassifications={ALL_ITEM_CLASSES}
                placeholder="품목번호 또는 품목명 입력"
              />
            </div>

            <label className="misc-field-qty">
              수량
              <input
                type="number"
                min={0}
                step="any"
                value={qty}
                onChange={(e) => setQty(e.target.value)}
                required
              />
            </label>

            <label className="misc-field-date">
              입출고일자
              <input type="date" value={movementDate} onChange={(e) => setMovementDate(e.target.value)} required />
            </label>
          </div>

          {selectedItem && (
            <p className="hint misc-slot-hint">
              재고분류: {selectedItem.propertyClassification ?? '—'}
              {' · '}
              창고: {preview?.locationLabel ?? (loadingPreview ? '확인 중…' : '—')}
              {' · '}
              현재고: {preview ? formatQty(preview.onHandQty) : loadingPreview ? '확인 중…' : '—'}
            </p>
          )}

          {processRequired && (
            <div className="search-row misc-movement-row">
              <label className="misc-field-process">
                공정
                <select
                  value={processSequenceId}
                  onChange={(e) =>
                    setProcessSequenceId(e.target.value === '' ? '' : Number(e.target.value))
                  }
                  required
                >
                  <option value="">— 선택 —</option>
                  {processOptions.map((process) => (
                    <option key={process.id} value={process.id}>
                      {formatProcessLabel(process)}
                    </option>
                  ))}
                </select>
              </label>
            </div>
          )}

          <div className="search-row misc-movement-row misc-movement-row--actions">
            <label className="misc-field-reason">
              사유
              <select
                value={reasonCodeId}
                onChange={(e) => setReasonCodeId(e.target.value === '' ? '' : Number(e.target.value))}
              >
                <option value="">— 선택 —</option>
                {reasonOptions.map((reason) => (
                  <option key={reason.id} value={reason.id}>
                    {reason.smallName}
                  </option>
                ))}
              </select>
            </label>

            <label className="misc-field-note">
              사유(내용)
              <input value={note} onChange={(e) => setNote(e.target.value)} maxLength={500} />
            </label>

            <button type="submit" disabled={submitting}>
              {submitting ? '처리 중…' : editingId != null ? '수정' : '입출고'}
            </button>
            {(editingId != null || selectedItem) && (
              <button type="button" className="secondary" onClick={resetForm} disabled={submitting}>
                초기화
              </button>
            )}
          </div>
        </form>
      </section>

      <section className="panel">
        <h2>입출고 내역</h2>
        <div className="filter-panel">
          <label>
            품목번호
            <input value={filterItemNo} onChange={(e) => setFilterItemNo(e.target.value)} />
          </label>
          <label>
            품목명
            <input value={filterItemName} onChange={(e) => setFilterItemName(e.target.value)} />
          </label>
          <label>
            일자(부터)
            <input type="date" value={filterDateFrom} onChange={(e) => setFilterDateFrom(e.target.value)} />
          </label>
          <label>
            일자(까지)
            <input type="date" value={filterDateTo} onChange={(e) => setFilterDateTo(e.target.value)} />
          </label>
          <button type="button" onClick={() => void loadHistory()}>
            조회
          </button>
        </div>

        {loadingHistory ? (
          <p>불러오는 중…</p>
        ) : rows.length === 0 ? (
          <p className="hint">입출고 내역이 없습니다.</p>
        ) : (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>번호</th>
                  <th>일자</th>
                  <th>구분</th>
                  <th>품목</th>
                  <th>창고</th>
                  <th>수량</th>
                  <th>사유</th>
                  <th>사유(내용)</th>
                  <th>상태</th>
                  <th />
                </tr>
              </thead>
              <tbody>
                {rows.map((row) => (
                  <tr key={row.id} className={editingId === row.id ? 'row-editing' : undefined}>
                    <td>{row.movementNo}</td>
                    <td>{row.movementDate}</td>
                    <td>{row.movementDirectionLabel}</td>
                    <td>
                      {row.itemNo} {row.itemName}
                    </td>
                    <td>{row.locationLabel}</td>
                    <td>{formatQty(row.qty)}</td>
                    <td>{row.reasonLabel || '—'}</td>
                    <td>{row.note || '—'}</td>
                    <td>{row.statusLabel}</td>
                    <td className="table-actions">
                      {row.status === 'REGISTERED' && (
                        <>
                          <button
                            type="button"
                            className="secondary"
                            disabled={submitting}
                            onClick={() => onEditRow(row)}
                          >
                            수정
                          </button>
                          <button
                            type="button"
                            className="secondary"
                            disabled={submitting}
                            onClick={() => void onDeleteRow(row)}
                          >
                            삭제
                          </button>
                        </>
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
