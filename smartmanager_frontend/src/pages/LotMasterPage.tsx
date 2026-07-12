import { useCallback, useEffect, useMemo, useState, type FormEvent } from 'react';
import type { PropertyClassification } from '../api/item';
import {
  createLot,
  deleteLot,
  fetchLots,
  updateLot,
  type LotRow,
  type LotStatus,
} from '../api/lot';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import GridExcelExportButton from '../components/GridExcelExportButton';
import { useAuth } from '../context/AuthContext';
import {
  formatInventoryLocation,
  INVENTORY_LOCATION_FILTER_OPTIONS,
} from '../utils/inventoryLocation';
import { formatInteger, formatQty } from '../utils/numberFormat';

const ALL_ITEM_CLASSES: PropertyClassification[] = ['원자재', '제품', '상품', '공정품'];

type EditForm = {
  status: LotStatus;
  p1: string;
  p2: string;
  expiryDate: string;
  certificateRef: string;
  remark: string;
};

function emptyEditForm(): EditForm {
  return {
    status: 'ACTIVE',
    p1: '',
    p2: '',
    expiryDate: '',
    certificateRef: '',
    remark: '',
  };
}

function sumLotQty(lot: LotRow): number {
  return lot.balances.reduce((sum, row) => sum + Number(row.qtyOnHand ?? 0), 0);
}

function toEditForm(lot: LotRow): EditForm {
  return {
    status: lot.status,
    p1: lot.p1 ?? '',
    p2: lot.p2 ?? '',
    expiryDate: lot.expiryDate ?? '',
    certificateRef: lot.certificateRef ?? '',
    remark: lot.remark ?? '',
  };
}

export default function LotMasterPage() {
  const { canWrite } = useAuth();
  const canEdit = canWrite('inventory:lot:write');

  const [itemNo, setItemNo] = useState('');
  const [lotNoFilter, setLotNoFilter] = useState('');
  const [locationCode, setLocationCode] = useState('');
  const [statusFilter, setStatusFilter] = useState<LotStatus | ''>('');
  const [lots, setLots] = useState<LotRow[]>([]);
  const [selectedId, setSelectedId] = useState<number | null>(null);
  const [loading, setLoading] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const [selectedItem, setSelectedItem] = useState<ItemSearchSelection | null>(null);
  const [createLotNo, setCreateLotNo] = useState('');
  const [autoGenerate, setAutoGenerate] = useState(true);
  const [createForm, setCreateForm] = useState(() => emptyEditForm());
  const [editForm, setEditForm] = useState(() => emptyEditForm());

  const selectedLot = useMemo(
    () => lots.find((lot) => lot.id === selectedId) ?? null,
    [lots, selectedId],
  );

  const loadLots = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const rows = await fetchLots({
        itemNo: itemNo || undefined,
        lotNo: lotNoFilter || undefined,
        status: statusFilter || undefined,
        locationCode: locationCode || undefined,
      });
      setLots(rows);
      setSelectedId((prev) => (prev != null && rows.some((row) => row.id === prev) ? prev : null));
    } catch (e) {
      setError(e instanceof Error ? e.message : 'Lot 목록을 불러오지 못했습니다.');
      setLots([]);
      setSelectedId(null);
    } finally {
      setLoading(false);
    }
  }, [itemNo, lotNoFilter, statusFilter, locationCode]);

  useEffect(() => {
    void loadLots();
  }, [loadLots]);

  useEffect(() => {
    if (selectedLot) {
      setEditForm(toEditForm(selectedLot));
    } else {
      setEditForm(emptyEditForm());
    }
  }, [selectedLot]);

  const resetCreateForm = () => {
    setSelectedItem(null);
    setCreateLotNo('');
    setAutoGenerate(true);
    setCreateForm(emptyEditForm());
  };

  const onSelectRow = (lot: LotRow) => {
    setSelectedId(lot.id);
    setMessage(null);
    setError(null);
  };

  const onClearSelection = () => {
    setSelectedId(null);
    setMessage(null);
    setError(null);
  };

  const onCreate = async (event: FormEvent) => {
    event.preventDefault();
    if (!canEdit) return;
    if (!selectedItem) {
      setError('품목을 선택해 주세요.');
      return;
    }
    if (!selectedItem.lotTracked) {
      setError('Lot 추적 품목만 등록할 수 있습니다. 품목 마스터에서 Lot 추적을 켜 주세요.');
      return;
    }
    if (!autoGenerate && !createLotNo.trim()) {
      setError('Lot번호를 입력하거나 자동 채번을 선택해 주세요.');
      return;
    }
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      const created = await createLot({
        itemId: selectedItem.id,
        lotNo: autoGenerate ? undefined : createLotNo.trim(),
        autoGenerate,
        originType: 'MANUAL',
        p1: createForm.p1 || undefined,
        p2: createForm.p2 || undefined,
        expiryDate: createForm.expiryDate || undefined,
        certificateRef: createForm.certificateRef || undefined,
        remark: createForm.remark || undefined,
      });
      setMessage(`Lot ${created.lotNo} 를 등록했습니다.`);
      resetCreateForm();
      await loadLots();
      setSelectedId(created.id);
    } catch (e) {
      setError(e instanceof Error ? e.message : 'Lot 등록에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  const onUpdate = async (event: FormEvent) => {
    event.preventDefault();
    if (!canEdit || selectedLot == null) return;
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      const updated = await updateLot(selectedLot.id, {
        status: editForm.status,
        p1: editForm.p1 || undefined,
        p2: editForm.p2 || undefined,
        expiryDate: editForm.expiryDate || null,
        certificateRef: editForm.certificateRef || undefined,
        remark: editForm.remark || undefined,
      });
      setMessage(`Lot ${updated.lotNo} 를 수정했습니다.`);
      await loadLots();
      setSelectedId(updated.id);
    } catch (e) {
      setError(e instanceof Error ? e.message : 'Lot 수정에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async () => {
    if (!canEdit || selectedLot == null) return;
    if (sumLotQty(selectedLot) > 0) {
      setError('잔량이 있는 Lot는 삭제할 수 없습니다.');
      return;
    }
    if (!window.confirm(`Lot ${selectedLot.lotNo} 를 삭제하시겠습니까?`)) {
      return;
    }
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      await deleteLot(selectedLot.id);
      setMessage(`Lot ${selectedLot.lotNo} 를 삭제했습니다.`);
      setSelectedId(null);
      await loadLots();
    } catch (e) {
      setError(e instanceof Error ? e.message : 'Lot 삭제에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  const exportRows = useMemo(
    () =>
      lots.map((row) => ({
        Lot번호: row.lotNo,
        품번: row.itemNo,
        품명: row.itemName,
        상태: row.statusLabel,
        출처: row.originTypeLabel,
        총잔량: sumLotQty(row),
        P1: row.p1 ?? '',
        P2: row.p2 ?? '',
        유효기한: row.expiryDate ?? '',
        비고: row.remark ?? '',
      })),
    [lots],
  );

  return (
    <div className="page">
      <header className="page-header">
        <h1>Lot 마스터</h1>
        <p>Lot 목록 조회, 수동 등록, 상태·비고 변경을 수행합니다. 잔량 조정은 기타입출고·TX 경로를 사용합니다.</p>
      </header>

      <section className="filter-panel">
        <label>
          품목번호
          <input value={itemNo} onChange={(e) => setItemNo(e.target.value)} />
        </label>
        <label>
          Lot번호
          <input value={lotNoFilter} onChange={(e) => setLotNoFilter(e.target.value)} />
        </label>
        <label>
          창고
          <select value={locationCode} onChange={(e) => setLocationCode(e.target.value)}>
            {INVENTORY_LOCATION_FILTER_OPTIONS.map((option) => (
              <option key={option.value || 'all'} value={option.value}>
                {option.label}
              </option>
            ))}
          </select>
        </label>
        <label>
          상태
          <select value={statusFilter} onChange={(e) => setStatusFilter(e.target.value as LotStatus | '')}>
            <option value="">전체</option>
            <option value="ACTIVE">활성</option>
            <option value="BLOCKED">차단</option>
            <option value="DEPLETED">소진</option>
          </select>
        </label>
        <button type="button" onClick={() => void loadLots()}>
          검색
        </button>
      </section>

      {error && <p className="error-banner">{error}</p>}
      {message && <p className="success-banner">{message}</p>}

      {canEdit && (
        <section className="panel">
          <h2>Lot 등록</h2>
          <form onSubmit={(e) => void onCreate(e)} className="form-grid form-grid-wide">
            <ItemSearchField
              label="품목 *"
              selectedItem={selectedItem}
              onSelect={setSelectedItem}
              allowedClassifications={ALL_ITEM_CLASSES}
              placeholder="Lot 추적 품목 검색"
            />
            {selectedItem && !selectedItem.lotTracked && (
              <p className="error-banner">선택한 품목은 Lot 추적이 꺼져 있습니다.</p>
            )}
            <label className="checkbox-label">
              <input
                type="checkbox"
                checked={autoGenerate}
                onChange={(e) => setAutoGenerate(e.target.checked)}
              />
              Lot번호 자동 채번
            </label>
            <label>
              Lot번호 {autoGenerate ? '' : '*'}
              <input
                value={createLotNo}
                onChange={(e) => setCreateLotNo(e.target.value)}
                disabled={autoGenerate}
                required={!autoGenerate}
              />
            </label>
            <label>
              P1
              <input
                value={createForm.p1}
                onChange={(e) => setCreateForm({ ...createForm, p1: e.target.value })}
              />
            </label>
            <label>
              P2
              <input
                value={createForm.p2}
                onChange={(e) => setCreateForm({ ...createForm, p2: e.target.value })}
              />
            </label>
            <label>
              유효기한
              <input
                type="date"
                value={createForm.expiryDate}
                onChange={(e) => setCreateForm({ ...createForm, expiryDate: e.target.value })}
              />
            </label>
            <label>
              성적서 참조
              <input
                value={createForm.certificateRef}
                onChange={(e) => setCreateForm({ ...createForm, certificateRef: e.target.value })}
              />
            </label>
            <label>
              비고
              <input
                value={createForm.remark}
                onChange={(e) => setCreateForm({ ...createForm, remark: e.target.value })}
              />
            </label>
            <div className="form-actions">
              <button type="submit" disabled={submitting}>
                등록
              </button>
              <button type="button" className="secondary" onClick={resetCreateForm}>
                초기화
              </button>
            </div>
          </form>
        </section>
      )}

      {selectedLot && (
        <section className="panel detail-panel">
          <div className="panel-header-row">
            <h2>
              Lot 수정 — {selectedLot.lotNo} ({selectedLot.itemNo})
            </h2>
            <button type="button" className="secondary" onClick={onClearSelection}>
              선택 해제
            </button>
          </div>
          <form onSubmit={(e) => void onUpdate(e)} className="form-grid form-grid-wide">
            <label>
              품목
              <input
                readOnly
                className="readonly"
                value={`${selectedLot.itemNo} ${selectedLot.itemName}`}
              />
            </label>
            <label>
              Lot번호
              <input readOnly className="readonly" value={selectedLot.lotNo} />
            </label>
            <label>
              출처
              <input readOnly className="readonly" value={selectedLot.originTypeLabel} />
            </label>
            <label>
              총잔량
              <input readOnly className="readonly" value={formatQty(sumLotQty(selectedLot))} />
            </label>
            <label>
              상태 *
              <select
                required
                value={editForm.status}
                disabled={!canEdit}
                onChange={(e) => setEditForm({ ...editForm, status: e.target.value as LotStatus })}
              >
                <option value="ACTIVE">활성</option>
                <option value="BLOCKED">차단</option>
                <option value="DEPLETED">소진</option>
              </select>
            </label>
            <label>
              P1
              <input
                value={editForm.p1}
                disabled={!canEdit}
                onChange={(e) => setEditForm({ ...editForm, p1: e.target.value })}
              />
            </label>
            <label>
              P2
              <input
                value={editForm.p2}
                disabled={!canEdit}
                onChange={(e) => setEditForm({ ...editForm, p2: e.target.value })}
              />
            </label>
            <label>
              유효기한
              <input
                type="date"
                value={editForm.expiryDate}
                disabled={!canEdit}
                onChange={(e) => setEditForm({ ...editForm, expiryDate: e.target.value })}
              />
            </label>
            <label>
              성적서 참조
              <input
                value={editForm.certificateRef}
                disabled={!canEdit}
                onChange={(e) => setEditForm({ ...editForm, certificateRef: e.target.value })}
              />
            </label>
            <label>
              비고
              <input
                value={editForm.remark}
                disabled={!canEdit}
                onChange={(e) => setEditForm({ ...editForm, remark: e.target.value })}
              />
            </label>
            {canEdit && (
              <div className="form-actions">
                <button type="submit" disabled={submitting}>
                  저장
                </button>
                <button
                  type="button"
                  className="danger"
                  disabled={submitting || sumLotQty(selectedLot) > 0}
                  onClick={() => void onDelete()}
                  title={sumLotQty(selectedLot) > 0 ? '잔량이 있으면 삭제할 수 없습니다' : undefined}
                >
                  삭제
                </button>
              </div>
            )}
          </form>

          {selectedLot.balances.length > 0 && (
            <div className="table-wrap" style={{ marginTop: '1rem' }}>
              <h3>슬롯 잔량</h3>
              <table>
                <thead>
                  <tr>
                    <th>창고</th>
                    <th className="num">잔량</th>
                  </tr>
                </thead>
                <tbody>
                  {selectedLot.balances.map((bal) => (
                    <tr key={bal.id}>
                      <td>
                        {bal.locationLabel ||
                          formatInventoryLocation(bal.locationCode, {
                            outputProcessSequence: bal.outputProcessSequence,
                            outputProcessName: bal.outputProcessName,
                          })}
                      </td>
                      <td className="num">{formatQty(bal.qtyOnHand)}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </section>
      )}

      <div className="panel-header-row">
        <h2>Lot 목록</h2>
        <GridExcelExportButton fileBaseName="Lot마스터" disabled={loading} rows={exportRows} />
      </div>

      {loading ? (
        <p>불러오는 중…</p>
      ) : (
        <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>Lot번호</th>
                <th>품목</th>
                <th>상태</th>
                <th>출처</th>
                <th className="num">총잔량</th>
                <th className="num">슬롯</th>
                <th>유효기한</th>
                <th>비고</th>
              </tr>
            </thead>
            <tbody>
              {lots.length === 0 ? (
                <tr>
                  <td colSpan={8}>Lot가 없습니다.</td>
                </tr>
              ) : (
                lots.map((row) => (
                  <tr
                    key={row.id}
                    className={selectedId === row.id ? 'is-selected' : undefined}
                    style={{ cursor: 'pointer' }}
                    onClick={() => onSelectRow(row)}
                  >
                    <td>{row.lotNo}</td>
                    <td>
                      {row.itemNo} {row.itemName}
                    </td>
                    <td>{row.statusLabel}</td>
                    <td>{row.originTypeLabel}</td>
                    <td className="num">{formatQty(sumLotQty(row))}</td>
                    <td className="num">{formatInteger(row.balances.length)}</td>
                    <td>{row.expiryDate ?? '—'}</td>
                    <td>{row.remark ?? '—'}</td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}
