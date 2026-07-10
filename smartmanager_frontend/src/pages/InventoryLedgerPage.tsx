import { useCallback, useEffect, useMemo, useState } from 'react';
import {
  fetchInventoryBalances,
  fetchStockMovements,
  type InventoryBalance,
  type StockMovement,
} from '../api/inventoryLedger';
import GridExcelExportButton from '../components/GridExcelExportButton';
import {
  formatInventoryLocation,
  INVENTORY_LOCATION_FILTER_OPTIONS,
} from '../utils/inventoryLocation';

function formatQty(value: number): string {
  return Number.isInteger(value) ? String(value) : value.toLocaleString(undefined, { maximumFractionDigits: 4 });
}

export default function InventoryLedgerPage() {
  const [tab, setTab] = useState<'movements' | 'balances'>('movements');
  const [itemNo, setItemNo] = useState('');
  const [locationCode, setLocationCode] = useState('');
  const [fiscalYear, setFiscalYear] = useState(String(new Date().getFullYear()));
  const [movements, setMovements] = useState<StockMovement[]>([]);
  const [balances, setBalances] = useState<InventoryBalance[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

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

  useEffect(() => {
    if (tab === 'movements') {
      void loadMovements();
    } else {
      void loadBalances();
    }
  }, [tab, loadMovements, loadBalances]);

  const movementTypeLabel = (type: string) => {
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

  return (
    <div className="page">
      <header className="page-header">
        <h1>재고·원장</h1>
        <p>입출고 이력(`stock_movement`)과 월별 재고 잔고(`inventory_balance_monthly`)를 조회합니다.</p>
      </header>

      <div className="tab-row">
        <button type="button" className={tab === 'movements' ? 'tab-active' : undefined} onClick={() => setTab('movements')}>
          입출고 이력
        </button>
        <button type="button" className={tab === 'balances' ? 'tab-active' : undefined} onClick={() => setTab('balances')}>
          재고 잔고
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
        <button type="button" onClick={() => void (tab === 'movements' ? loadMovements() : loadBalances())}>
          검색
        </button>
      </section>

      {error && <p className="error-banner">{error}</p>}

      <div className="panel-header-row">
        <h2>{tab === 'movements' ? '입출고 이력' : '재고 잔고'}</h2>
        <GridExcelExportButton
          fileBaseName={tab === 'movements' ? '입출고이력' : '재고잔고'}
          disabled={loading}
          rows={tab === 'movements' ? movementExportRows : balanceExportRows}
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
                <th>수량</th>
                <th>원장유형</th>
                <th>원장ID</th>
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
                    <td>{formatQty(row.qty)}</td>
                    <td>{row.referenceType}</td>
                    <td>{row.referenceId}</td>
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
                <th>품목</th>
                <th>창고</th>
                <th>연도</th>
                <th>현재고</th>
                <th>월별 입고</th>
                <th>월별 출고</th>
                <th>월말 재고</th>
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
                      <td>{row.fiscalYear}</td>
                      <td>{formatQty(row.stockQty)}</td>
                      <td>
                        {monthSummary.map((m) => (
                          <div key={`in-${m.monthNum}`}>
                            {m.monthNum}월: {formatQty(m.inQty)}
                          </div>
                        ))}
                      </td>
                      <td>
                        {monthSummary.map((m) => (
                          <div key={`out-${m.monthNum}`}>
                            {m.monthNum}월: {formatQty(m.outQty)}
                          </div>
                        ))}
                      </td>
                      <td>
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
      )}
    </div>
  );
}
