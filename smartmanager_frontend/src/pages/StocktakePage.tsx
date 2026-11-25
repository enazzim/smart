import { useRef, useState } from 'react';
import {
  commitStocktake,
  fetchStocktakeBook,
  previewStocktake,
  type StocktakeLinePayload,
  type StocktakePreviewRow,
} from '../api/stocktake';
import { downloadStocktakeBook, readStocktakeUpload } from '../utils/stocktakeExcel';
import { formatQty } from '../utils/numberFormat';

function todayIso(): string {
  const now = new Date();
  const month = String(now.getMonth() + 1).padStart(2, '0');
  const day = String(now.getDate()).padStart(2, '0');
  return `${now.getFullYear()}-${month}-${day}`;
}

function actionLabel(action: string): string {
  if (action === 'IN') return '입고';
  if (action === 'OUT') return '출고';
  if (action === 'NONE') return '차이 없음';
  if (action === 'SKIP') return '건너뜀';
  return '오류';
}

export default function StocktakePage() {
  const fileInputRef = useRef<HTMLInputElement>(null);
  const [stocktakeDate, setStocktakeDate] = useState(todayIso());
  const [lines, setLines] = useState<StocktakeLinePayload[]>([]);
  const [preview, setPreview] = useState<StocktakePreviewRow[]>([]);
  const [busy, setBusy] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const errorCount = preview.filter((row) => row.error).length;
  const postCount = preview.filter((row) => row.action === 'IN' || row.action === 'OUT').length;

  async function onDownloadBook() {
    setBusy(true);
    setError(null);
    setMessage(null);
    try {
      const book = await fetchStocktakeBook(stocktakeDate);
      await downloadStocktakeBook(book, stocktakeDate);
      setMessage(`장부 ${book.length}행을 내려받았습니다. 실사수량을 입력한 뒤 업로드하세요. 이력이 없는 품목은 행을 추가하면 됩니다.`);
    } catch (caught) {
      setError(caught instanceof Error ? caught.message : '장부를 내려받지 못했습니다.');
    } finally {
      setBusy(false);
    }
  }

  async function onUpload(file: File) {
    setBusy(true);
    setError(null);
    setMessage(null);
    setPreview([]);
    try {
      const uploaded = await readStocktakeUpload(await file.arrayBuffer());
      const result = await previewStocktake(stocktakeDate, uploaded);
      const withBook = uploaded.map((line) => {
        const evaluated = result.find((row) => row.rowNo === line.rowNo);
        return { ...line, expectedBookQty: evaluated?.bookQty ?? null };
      });
      setLines(withBook);
      setPreview(result);
      setMessage('미리보기를 확인한 뒤 반영하세요. 오류가 있으면 한 건도 반영되지 않습니다.');
    } catch (caught) {
      setLines([]);
      setError(caught instanceof Error ? caught.message : '업로드를 읽지 못했습니다.');
    } finally {
      setBusy(false);
    }
  }

  async function onCommit() {
    setBusy(true);
    setError(null);
    setMessage(null);
    try {
      const result = await commitStocktake(stocktakeDate, lines);
      setMessage(`실사 ${result.batchNo}를 반영했습니다. 잘못된 전표는 기타 입출고에서 삭제하면 됩니다.`);
      setPreview([]);
      setLines([]);
    } catch (caught) {
      setError(caught instanceof Error ? caught.message : '반영하지 못했습니다.');
    } finally {
      setBusy(false);
    }
  }

  return (
    <div className="page">
      <header className="page-header">
        <div>
          <h1>재고실사</h1>
          <p>
            장부에 없는 품목은 실사수량이 기초재고로 입고됩니다. 이력이 있는 품목은 차이만 실사조정됩니다.
            실사수량을 비우면 건너뜁니다. 외주 행의 거래처와 투입공정, Lot 추적 품목의 Lot번호는 장부에서 채웁니다.
          </p>
        </div>
      </header>

      <section className="filter-panel">
        <label>
          실사일
          <input type="date" value={stocktakeDate} onChange={(event) => setStocktakeDate(event.target.value)} />
        </label>
        <button type="button" onClick={() => void onDownloadBook()} disabled={busy}>
          장부 내려받기
        </button>
        <button
          type="button"
          className="secondary"
          disabled={busy}
          onClick={() => fileInputRef.current?.click()}
        >
          실사 업로드
        </button>
        <input
          ref={fileInputRef}
          type="file"
          accept=".xlsx"
          hidden
          onChange={(event) => {
            const file = event.target.files?.[0];
            event.target.value = '';
            if (file) void onUpload(file);
          }}
        />
        <button type="button" onClick={() => void onCommit()} disabled={busy || postCount === 0 || errorCount > 0}>
          반영
        </button>
      </section>

      {error && <p className="error-banner">{error}</p>}
      {message && <p className="success-banner">{message}</p>}

      <div className="panel-header-row">
        <h2>미리보기 {preview.length > 0 ? `(반영 ${postCount}건, 오류 ${errorCount}건)` : ''}</h2>
      </div>
      <div className="table-wrap">
        <table>
          <thead>
            <tr>
              <th className="num">행</th>
              <th>품목</th>
              <th>창고</th>
              <th>거래처</th>
              <th>공정</th>
              <th>Lot</th>
              <th className="num">장부</th>
              <th className="num">실사</th>
              <th>처리</th>
              <th className="num">수량</th>
              <th>사유</th>
              <th>오류</th>
            </tr>
          </thead>
          <tbody>
            {preview.length === 0 ? (
              <tr>
                <td colSpan={12}>장부를 내려받아 실사수량을 입력한 뒤 업로드하세요.</td>
              </tr>
            ) : (
              preview.map((row) => (
                <tr key={row.rowNo}>
                  <td className="num">{row.rowNo}</td>
                  <td>{row.itemNo} {row.itemName ?? ''}</td>
                  <td>{row.locationCode ?? ''}</td>
                  <td>{row.partnerNo ?? ''}</td>
                  <td>{row.processName ?? ''}</td>
                  <td>{row.lotNo ?? ''}</td>
                  <td className="num">{row.bookQty == null ? '' : formatQty(row.bookQty)}</td>
                  <td className="num">{row.countedQty == null ? '' : formatQty(row.countedQty)}</td>
                  <td>{actionLabel(row.action)}</td>
                  <td className="num">{row.movementQty == null ? '' : formatQty(row.movementQty)}</td>
                  <td>{row.reasonName ?? ''}</td>
                  <td>{row.error ?? ''}</td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}
