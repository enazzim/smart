import { useCallback, useEffect, useMemo, useState } from 'react';
import {
  fetchInventoryBalances,
  fetchStockMovements,
  type InventoryBalance,
  type StockMovement,
} from '../api/inventoryLedger';
import {
  fetchLot,
  fetchLotGenealogy,
  fetchLotMovements,
  fetchLots,
  type LotGenealogyDirection,
  type LotGenealogyLink,
  type LotRow,
  type LotStatus,
} from '../api/lot';
import GridExcelExportButton from '../components/GridExcelExportButton';
import {
  formatInventoryLocation,
  INVENTORY_LOCATION_FILTER_OPTIONS,
} from '../utils/inventoryLocation';
import { formatInteger, formatQty } from '../utils/numberFormat';

type LedgerTab = 'movements' | 'balances' | 'lots';
type LotDetailPane = 'balances' | 'movements' | 'genealogy';

function movementTypeLabel(type: string) {
  switch (type) {
    case 'IN':
      return '입고';
    case 'OUT':
      return '출고';
    case 'ADJUST':
      return '조정';
    default:
      return type;
  }
}

function sumLotQty(lot: LotRow): number {
  return lot.balances.reduce((sum, row) => sum + Number(row.qtyOnHand ?? 0), 0);
}

export default function InventoryLedgerPage() {
  const [tab, setTab] = useState<LedgerTab>('movements');
  const [itemNo, setItemNo] = useState('');
  const [locationCode, setLocationCode] = useState('');
  const [fiscalYear, setFiscalYear] = useState(String(new Date().getFullYear()));
  const [lotNo, setLotNo] = useState('');
  const [lotStatus, setLotStatus] = useState<LotStatus | ''>('');
  const [movements, setMovements] = useState<StockMovement[]>([]);
  const [balances, setBalances] = useState<InventoryBalance[]>([]);
  const [lots, setLots] = useState<LotRow[]>([]);
  const [selectedLotId, setSelectedLotId] = useState<number | null>(null);
  const [lotDetailPane, setLotDetailPane] = useState<LotDetailPane>('balances');
  const [genealogyDirection, setGenealogyDirection] = useState<LotGenealogyDirection>('UP');
  const [lotMovements, setLotMovements] = useState<StockMovement[]>([]);
  const [genealogyLinks, setGenealogyLinks] = useState<LotGenealogyLink[]>([]);
  const [detailLoading, setDetailLoading] = useState(false);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const selectedLot = useMemo(
    () => lots.find((lot) => lot.id === selectedLotId) ?? null,
    [lots, selectedLotId],
  );

  const loadMovements = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      setMovements(
        await fetchStockMovements({
          itemNo: itemNo || undefined,
          locationCode: locationCode || undefined,
        }),
      );
    } catch (e) {
      setError(e instanceof Error ? e.message : '입출고 이력을 불러오지 못했습니다.');
      setMovements([]);
    } finally {
      setLoading(false);
    }
  }, [itemNo, locationCode]);

  const loadBalances = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const year = Number(fiscalYear);
      setBalances(
        await fetchInventoryBalances({
          itemNo: itemNo || undefined,
          locationCode: locationCode || undefined,
          fiscalYear: Number.isFinite(year) ? year : undefined,
        }),
      );
    } catch (e) {
      setError(e instanceof Error ? e.message : '재고 잔고를 불러오지 못했습니다.');
      setBalances([]);
    } finally {
      setLoading(false);
    }
  }, [itemNo, locationCode, fiscalYear]);

  const loadLots = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const rows = await fetchLots({
        itemNo: itemNo || undefined,
        lotNo: lotNo || undefined,
        status: lotStatus || undefined,
        locationCode: locationCode || undefined,
      });
      setLots(rows);
      setSelectedLotId((prev) => (prev != null && rows.some((row) => row.id === prev) ? prev : null));
    } catch (e) {
      setError(e instanceof Error ? e.message : 'Lot 목록을 불러오지 못했습니다.');
      setLots([]);
      setSelectedLotId(null);
    } finally {
      setLoading(false);
    }
  }, [itemNo, lotNo, lotStatus, locationCode]);

  const loadLotDetail = useCallback(async (lotId: number, pane: LotDetailPane, direction: LotGenealogyDirection) => {
    setDetailLoading(true);
    setError(null);
    try {
      if (pane === 'movements') {
        setLotMovements(await fetchLotMovements(lotId));
      } else if (pane === 'genealogy') {
        setGenealogyLinks(await fetchLotGenealogy(lotId, direction));
      }
    } catch (e) {
      setError(e instanceof Error ? e.message : 'Lot 상세를 불러오지 못했습니다.');
      if (pane === 'movements') setLotMovements([]);
      if (pane === 'genealogy') setGenealogyLinks([]);
    } finally {
      setDetailLoading(false);
    }
  }, []);

  useEffect(() => {
    if (tab === 'movements') {
      void loadMovements();
    } else if (tab === 'balances') {
      void loadBalances();
    } else {
      void loadLots();
    }
  }, [tab, loadMovements, loadBalances, loadLots]);

  useEffect(() => {
    if (tab !== 'lots' || selectedLotId == null || lotDetailPane === 'balances') {
      return;
    }
    void loadLotDetail(selectedLotId, lotDetailPane, genealogyDirection);
  }, [tab, selectedLotId, lotDetailPane, genealogyDirection, loadLotDetail]);

  const handleSearch = () => {
    if (tab === 'movements') void loadMovements();
    else if (tab === 'balances') void loadBalances();
    else void loadLots();
  };

  const selectLot = (lotId: number) => {
    setSelectedLotId(lotId);
  };

  const drillToLot = async (lotId: number) => {
    if (lots.some((lot) => lot.id === lotId)) {
      setSelectedLotId(lotId);
      return;
    }
    setDetailLoading(true);
    setError(null);
    try {
      const row = await fetchLot(lotId);
      setLots((prev) => (prev.some((lot) => lot.id === row.id) ? prev : [row, ...prev]));
      setSelectedLotId(row.id);
    } catch (e) {
      setError(e instanceof Error ? e.message : 'Lot를 불러오지 못했습니다.');
    } finally {
      setDetailLoading(false);
    }
  };

  const movementExportRows = useMemo(
    () =>
      movements.map((row) => ({
        일자: row.movementDate,
        품목: `${row.itemNo} ${row.itemName}`,
        창고: formatInventoryLocation(row.locationCode, {
          outputProcessSequence: row.outputProcessSequence,
          outputProcessName: row.outputProcessName,
        }),
        구분: movementTypeLabel(row.movementType),
        수량: row.qty,
        원장유형: row.referenceType,
        원장ID: row.referenceId,
      })),
    [movements],
  );

  const balanceExportRows = useMemo(
    () =>
      balances.map((row) => {
        const activeMonths = row.months.filter((m) => m.inQty > 0 || m.outQty > 0 || m.stockQty > 0);
        const monthSummary = activeMonths.length > 0 ? activeMonths : row.months;
        return {
          품목: `${row.itemNo} ${row.itemName}`,
          창고: formatInventoryLocation(row.locationCode, {
            outputProcessSequence: row.outputProcessSequence,
            outputProcessName: row.outputProcessName,
          }),
          연도: row.fiscalYear,
          현재고: row.stockQty,
          '월별 입고': monthSummary.map((m) => `${m.monthNum}월:${m.inQty}`).join(' '),
          '월별 출고': monthSummary.map((m) => `${m.monthNum}월:${m.outQty}`).join(' '),
          '월말 재고': monthSummary.map((m) => `${m.monthNum}월:${m.stockQty}`).join(' '),
        };
      }),
    [balances],
  );

  const lotExportRows = useMemo(
    () =>
      lots.map((row) => ({
        Lot번호: row.lotNo,
        품번: row.itemNo,
        품명: row.itemName,
        상태: row.statusLabel,
        출처: row.originTypeLabel,
        총잔량: sumLotQty(row),
        슬롯수: row.balances.length,
        유효기한: row.expiryDate ?? '',
      })),
    [lots],
  );

  return (
    <div className="page">
      <header className="page-header">
        <h1>재고·원장</h1>
        <p>
          입출고 이력(`stock_movement`), 월별 재고 잔고(`inventory_balance_monthly`), Lot 잔량·이력·계보를
          조회합니다.
        </p>
      </header>

      <div className="tab-row">
        <button type="button" className={tab === 'movements' ? 'tab-active' : undefined} onClick={() => setTab('movements')}>
          입출고 이력
        </button>
        <button type="button" className={tab === 'balances' ? 'tab-active' : undefined} onClick={() => setTab('balances')}>
          재고 잔고
        </button>
        <button type="button" className={tab === 'lots' ? 'tab-active' : undefined} onClick={() => setTab('lots')}>
          Lot
        </button>
      </div>

      <section className="filter-panel">
        <label>
          품목번호
          <input value={itemNo} onChange={(e) => setItemNo(e.target.value)} />
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
        {tab === 'balances' && (
          <label>
            회계연도
            <input type="number" value={fiscalYear} onChange={(e) => setFiscalYear(e.target.value)} />
          </label>
        )}
        {tab === 'lots' && (
          <>
            <label>
              Lot번호
              <input value={lotNo} onChange={(e) => setLotNo(e.target.value)} />
            </label>
            <label>
              상태
              <select value={lotStatus} onChange={(e) => setLotStatus(e.target.value as LotStatus | '')}>
                <option value="">전체</option>
                <option value="ACTIVE">활성</option>
                <option value="BLOCKED">차단</option>
                <option value="DEPLETED">소진</option>
              </select>
            </label>
          </>
        )}
        <button type="button" onClick={handleSearch}>
          검색
        </button>
      </section>

      {error && <p className="error-banner">{error}</p>}

      <div className="panel-header-row">
        <h2>{tab === 'movements' ? '입출고 이력' : tab === 'balances' ? '재고 잔고' : 'Lot 잔량'}</h2>
        <GridExcelExportButton
          fileBaseName={tab === 'movements' ? '입출고이력' : tab === 'balances' ? '재고잔고' : 'Lot잔량'}
          disabled={loading}
          rows={tab === 'movements' ? movementExportRows : tab === 'balances' ? balanceExportRows : lotExportRows}
        />
      </div>

      {loading ? (
        <p>불러오는 중…</p>
      ) : tab === 'movements' ? (
        <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>일자</th>
                <th>품목</th>
                <th>창고</th>
                <th>구분</th>
                <th className="num">수량</th>
                <th>원장유형</th>
                <th className="num">원장ID</th>
              </tr>
            </thead>
            <tbody>
              {movements.length === 0 ? (
                <tr>
                  <td colSpan={7}>입출고 이력이 없습니다.</td>
                </tr>
              ) : (
                movements.map((row) => (
                  <tr key={row.id}>
                    <td>{row.movementDate}</td>
                    <td>
                      {row.itemNo} {row.itemName}
                    </td>
                    <td>
                      {formatInventoryLocation(row.locationCode, {
                        outputProcessSequence: row.outputProcessSequence,
                        outputProcessName: row.outputProcessName,
                      })}
                    </td>
                    <td>{movementTypeLabel(row.movementType)}</td>
                    <td className="num">{formatQty(row.qty)}</td>
                    <td>{row.referenceType}</td>
                    <td className="num">{formatInteger(row.referenceId)}</td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      ) : tab === 'balances' ? (
        <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>품목</th>
                <th>창고</th>
                <th className="num">연도</th>
                <th className="num">현재고</th>
                <th className="num">월별 입고</th>
                <th className="num">월별 출고</th>
                <th className="num">월말 재고</th>
              </tr>
            </thead>
            <tbody>
              {balances.length === 0 ? (
                <tr>
                  <td colSpan={7}>재고 잔고가 없습니다.</td>
                </tr>
              ) : (
                balances.map((row) => {
                  const activeMonths = row.months.filter((m) => m.inQty > 0 || m.outQty > 0 || m.stockQty > 0);
                  const monthSummary = activeMonths.length > 0 ? activeMonths : row.months;
                  return (
                    <tr key={row.balanceId}>
                      <td>
                        {row.itemNo} {row.itemName}
                      </td>
                      <td>
                        {formatInventoryLocation(row.locationCode, {
                          outputProcessSequence: row.outputProcessSequence,
                          outputProcessName: row.outputProcessName,
                        })}
                      </td>
                      <td className="num">{formatInteger(row.fiscalYear)}</td>
                      <td className="num">{formatQty(row.stockQty)}</td>
                      <td className="num">
                        {monthSummary.map((m) => (
                          <div key={`in-${m.monthNum}`}>
                            {m.monthNum}월: {formatQty(m.inQty)}
                          </div>
                        ))}
                      </td>
                      <td className="num">
                        {monthSummary.map((m) => (
                          <div key={`out-${m.monthNum}`}>
                            {m.monthNum}월: {formatQty(m.outQty)}
                          </div>
                        ))}
                      </td>
                      <td className="num">
                        {monthSummary.map((m) => (
                          <div key={`stk-${m.monthNum}`}>
                            {m.monthNum}월: {formatQty(m.stockQty)}
                          </div>
                        ))}
                      </td>
                    </tr>
                  );
                })
              )}
            </tbody>
          </table>
        </div>
      ) : (
        <>
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
                </tr>
              </thead>
              <tbody>
                {lots.length === 0 ? (
                  <tr>
                    <td colSpan={7}>Lot가 없습니다.</td>
                  </tr>
                ) : (
                  lots.map((row) => (
                    <tr
                      key={row.id}
                      className={selectedLotId === row.id ? 'is-selected' : undefined}
                      style={{ cursor: 'pointer' }}
                      onClick={() => selectLot(row.id)}
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
                    </tr>
                  ))
                )}
              </tbody>
            </table>
          </div>

          {selectedLot && (
            <section className="detail-panel" style={{ marginTop: '1rem' }}>
              <div className="panel-header-row">
                <h3>
                  Lot 상세 — {selectedLot.lotNo} ({selectedLot.itemNo})
                </h3>
              </div>
              <div className="tab-row">
                <button
                  type="button"
                  className={lotDetailPane === 'balances' ? 'tab-active' : undefined}
                  onClick={() => setLotDetailPane('balances')}
                >
                  슬롯 잔량
                </button>
                <button
                  type="button"
                  className={lotDetailPane === 'movements' ? 'tab-active' : undefined}
                  onClick={() => setLotDetailPane('movements')}
                >
                  입출고 이력
                </button>
                <button
                  type="button"
                  className={lotDetailPane === 'genealogy' ? 'tab-active' : undefined}
                  onClick={() => setLotDetailPane('genealogy')}
                >
                  계보
                </button>
              </div>

              {lotDetailPane === 'genealogy' && (
                <div className="filter-panel" style={{ marginTop: '0.5rem' }}>
                  <label>
                    방향
                    <select
                      value={genealogyDirection}
                      onChange={(e) => setGenealogyDirection(e.target.value as LotGenealogyDirection)}
                    >
                      <option value="UP">상위 (투입·부모)</option>
                      <option value="DOWN">하위 (산출·자식)</option>
                    </select>
                  </label>
                </div>
              )}

              {detailLoading && lotDetailPane !== 'balances' ? (
                <p>상세 불러오는 중…</p>
              ) : lotDetailPane === 'balances' ? (
                <div className="table-wrap">
                  <table>
                    <thead>
                      <tr>
                        <th>창고</th>
                        <th className="num">잔량</th>
                      </tr>
                    </thead>
                    <tbody>
                      {selectedLot.balances.length === 0 ? (
                        <tr>
                          <td colSpan={2}>슬롯 잔량이 없습니다.</td>
                        </tr>
                      ) : (
                        selectedLot.balances.map((bal) => (
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
                        ))
                      )}
                    </tbody>
                  </table>
                </div>
              ) : lotDetailPane === 'movements' ? (
                <div className="table-wrap">
                  <table>
                    <thead>
                      <tr>
                        <th>일자</th>
                        <th>창고</th>
                        <th>구분</th>
                        <th className="num">수량</th>
                        <th>원장유형</th>
                        <th className="num">원장ID</th>
                      </tr>
                    </thead>
                    <tbody>
                      {lotMovements.length === 0 ? (
                        <tr>
                          <td colSpan={6}>이 Lot의 입출고 이력이 없습니다.</td>
                        </tr>
                      ) : (
                        lotMovements.map((row) => (
                          <tr key={row.id}>
                            <td>{row.movementDate}</td>
                            <td>
                              {formatInventoryLocation(row.locationCode, {
                                outputProcessSequence: row.outputProcessSequence,
                                outputProcessName: row.outputProcessName,
                              })}
                            </td>
                            <td>{movementTypeLabel(row.movementType)}</td>
                            <td className="num">{formatQty(row.qty)}</td>
                            <td>{row.referenceType}</td>
                            <td className="num">{formatInteger(row.referenceId)}</td>
                          </tr>
                        ))
                      )}
                    </tbody>
                  </table>
                </div>
              ) : (
                <div className="table-wrap">
                  <table>
                    <thead>
                      <tr>
                        <th>유형</th>
                        <th>부모 Lot</th>
                        <th>자식 Lot</th>
                        <th className="num">수량</th>
                        <th>원장</th>
                      </tr>
                    </thead>
                    <tbody>
                      {genealogyLinks.length === 0 ? (
                        <tr>
                          <td colSpan={5}>계보 링크가 없습니다.</td>
                        </tr>
                      ) : (
                        genealogyLinks.map((link) => (
                            <tr key={link.id}>
                              <td>{link.linkTypeLabel}</td>
                              <td>
                                <button
                                  type="button"
                                  className="link-button"
                                  onClick={() => void drillToLot(link.parentLotId)}
                                  disabled={link.parentLotId === selectedLotId}
                                >
                                  {link.parentLotNo} ({link.parentItemNo})
                                </button>
                              </td>
                              <td>
                                <button
                                  type="button"
                                  className="link-button"
                                  onClick={() => void drillToLot(link.childLotId)}
                                  disabled={link.childLotId === selectedLotId}
                                >
                                  {link.childLotNo} ({link.childItemNo})
                                </button>
                              </td>
                              <td className="num">{formatQty(link.qty)}</td>
                              <td>
                                {link.sourceDocType ?? '—'}
                                {link.sourceDocId != null ? ` #${link.sourceDocId}` : ''}
                              </td>
                            </tr>
                          ))
                      )}
                    </tbody>
                  </table>
                </div>
              )}
            </section>
          )}
        </>
      )}
    </div>
  );
}
