import { ZoomIn, ZoomOut, Maximize, Download } from 'lucide-react';
import { useEffect, useMemo, useRef, useState } from 'react';
import { Document, Page, pdfjs } from 'react-pdf';
import { fetchDrawingPdfBlob } from '../../api/drawing';
import { cacheDrawingPdf, getCachedDrawingPdf } from '../../services/drawingOfflineCacheService';
import 'react-pdf/dist/Page/AnnotationLayer.css';
import 'react-pdf/dist/Page/TextLayer.css';

pdfjs.GlobalWorkerOptions.workerSrc = `//unpkg.com/pdfjs-dist@${pdfjs.version}/build/pdf.worker.min.mjs`;

const BASE_PAGE_WIDTH = 800;
const MIN_ZOOM = 50;
const MAX_ZOOM = 1000;
const ZOOM_STEP = 10;
const MAX_DEVICE_PIXEL_RATIO = 2;
const RENDER_ZOOM_DEBOUNCE_MS = 120;

function resolveDevicePixelRatio(): number {
  if (typeof window === 'undefined') {
    return 1;
  }
  return Math.min(window.devicePixelRatio || 1, MAX_DEVICE_PIXEL_RATIO);
}

function clampZoom(value: number): number {
  if (!Number.isFinite(value)) {
    return 100;
  }
  return Math.min(MAX_ZOOM, Math.max(MIN_ZOOM, Math.round(value)));
}

interface PdfViewerProps {
  pdfUrl: string;
  /** IndexedDB 캐시 키 · 다운로드 파일명 */
  partNo: string;
  /** 툴바 표시용 제목 (미지정 시 partNo) */
  title?: string;
  /** 성공 시 IndexedDB 오프라인 캐시를 이 품번으로 갱신 (최신본 열람 시) */
  updateOfflineCache?: boolean;
  toolbarActions?: React.ReactNode;
  onError?: (message: string) => void;
}

type LoadState = 'loading' | 'ready' | 'error';

export default function PdfViewer({
  pdfUrl,
  partNo,
  title,
  updateOfflineCache = false,
  toolbarActions,
  onError,
}: PdfViewerProps) {
  const [zoom, setZoom] = useState(100);
  const [zoomInput, setZoomInput] = useState('100');
  const [renderZoom, setRenderZoom] = useState(100);
  const [numPages, setNumPages] = useState<number>();
  const [loadState, setLoadState] = useState<LoadState>('loading');
  const [fileSource, setFileSource] = useState<string | null>(null);
  const [fromCache, setFromCache] = useState(false);
  const [resolvedBlob, setResolvedBlob] = useState<Blob | null>(null);
  const [devicePixelRatio, setDevicePixelRatio] = useState(resolveDevicePixelRatio);
  const viewerRef = useRef<HTMLDivElement>(null);
  const objectUrlRef = useRef<string | null>(null);
  const onErrorRef = useRef(onError);
  onErrorRef.current = onError;

  const applyZoom = (next: number) => {
    const clamped = clampZoom(next);
    setZoom(clamped);
    setZoomInput(String(clamped));
  };

  useEffect(() => {
    const timer = window.setTimeout(() => setRenderZoom(zoom), RENDER_ZOOM_DEBOUNCE_MS);
    return () => window.clearTimeout(timer);
  }, [zoom]);

  useEffect(() => {
    const syncDpr = () => setDevicePixelRatio(resolveDevicePixelRatio());
    syncDpr();
    window.addEventListener('resize', syncDpr);
    return () => window.removeEventListener('resize', syncDpr);
  }, []);

  useEffect(() => {
    let cancelled = false;

    const revokeObjectUrl = () => {
      if (objectUrlRef.current) {
        URL.revokeObjectURL(objectUrlRef.current);
        objectUrlRef.current = null;
      }
    };

    const applyBlob = (blob: Blob, cached: boolean) => {
      revokeObjectUrl();
      const url = URL.createObjectURL(blob);
      objectUrlRef.current = url;
      setFileSource(url);
      setResolvedBlob(blob);
      setFromCache(cached);
      setLoadState('ready');
      setNumPages(undefined);
      setZoom(100);
      setZoomInput('100');
      setRenderZoom(100);
    };

    const load = async () => {
      setLoadState('loading');
      setFileSource(null);
      setResolvedBlob(null);
      setFromCache(false);
      setNumPages(undefined);
      setZoom(100);
      setZoomInput('100');
      setRenderZoom(100);

      const tryCache = async (): Promise<boolean> => {
        const cached = await getCachedDrawingPdf(partNo);
        if (!cached || cancelled) {
          return false;
        }
        applyBlob(cached, true);
        return true;
      };

      if (!navigator.onLine) {
        if (!(await tryCache()) && !cancelled) {
          setLoadState('error');
          onErrorRef.current?.(
            '오프라인이며 캐시된 도면이 없습니다. 「오늘 작업 도면 동기화」 후 다시 시도해 주세요.',
          );
        }
        return;
      }

      try {
        const blob = await fetchDrawingPdfBlob(pdfUrl);
        if (cancelled) {
          return;
        }
        applyBlob(blob, false);
        if (updateOfflineCache) {
          void cacheDrawingPdf(partNo, blob).catch(() => undefined);
        }
      } catch {
        if (cancelled) {
          return;
        }
        if (await tryCache()) {
          onErrorRef.current?.(
            '서버에서 PDF를 받지 못해 오프라인 캐시(동기화된 최신본)를 표시합니다.',
          );
          return;
        }
        setLoadState('error');
        onErrorRef.current?.('PDF를 불러오지 못했습니다.');
      }
    };

    void load();

    return () => {
      cancelled = true;
      revokeObjectUrl();
    };
  }, [pdfUrl, partNo, updateOfflineCache]);

  const pageWidth = useMemo(
    () => BASE_PAGE_WIDTH * (renderZoom / 100),
    [renderZoom],
  );

  const commitZoomInput = () => {
    const parsed = Number(zoomInput.replace(/%/g, '').trim());
    if (!Number.isFinite(parsed)) {
      setZoomInput(String(zoom));
      return;
    }
    applyZoom(parsed);
  };

  const handleContextMenu = (e: React.MouseEvent) => {
    e.preventDefault();
  };

  const handleDownload = async () => {
    try {
      const blob = resolvedBlob ?? (await fetchDrawingPdfBlob(pdfUrl));
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      link.download = `${partNo}.pdf`;
      document.body.appendChild(link);
      link.click();
      window.URL.revokeObjectURL(url);
      document.body.removeChild(link);
    } catch {
      onErrorRef.current?.('다운로드 중 오류가 발생했습니다.');
    }
  };

  const handleFullscreen = () => {
    if (!document.fullscreenElement) {
      void viewerRef.current?.requestFullscreen();
    } else {
      void document.exitFullscreen();
    }
  };

  const displayTitle = title ?? partNo;

  return (
    <div ref={viewerRef} className="drawing-pdf-viewer" onContextMenu={handleContextMenu}>
      <div className="drawing-pdf-toolbar">
        <div className="drawing-pdf-toolbar__left">
          <span className="drawing-pdf-toolbar__title">{displayTitle} 도면 뷰어</span>
          {fromCache && <span className="drawing-version-badge">오프라인 캐시</span>}
          {toolbarActions}
        </div>
        <div className="drawing-pdf-toolbar__right">
          <button
            type="button"
            title="축소"
            onClick={() => applyZoom(zoom - ZOOM_STEP)}
          >
            <ZoomOut size={18} />
          </button>
          <div className="drawing-pdf-toolbar__zoom">
            <input
              type="number"
              min={MIN_ZOOM}
              max={MAX_ZOOM}
              step={ZOOM_STEP}
              className="drawing-pdf-toolbar__zoom-input"
              value={zoomInput}
              title={`${MIN_ZOOM}~${MAX_ZOOM}%`}
              aria-label={`확대 비율 (${MIN_ZOOM}~${MAX_ZOOM})`}
              style={{
                boxSizing: 'border-box',
                width: 44,
                height: 32,
                minHeight: 32,
                maxHeight: 32,
                margin: 0,
                padding: '0 4px',
                border: '1px solid rgba(255,255,255,0.35)',
                borderRadius: 6,
                background: 'rgba(0,0,0,0.45)',
                color: '#fff',
                fontSize: 12,
                lineHeight: '30px',
                textAlign: 'center',
                boxShadow: 'none',
              }}
              onChange={(e) => setZoomInput(e.target.value)}
              onBlur={commitZoomInput}
              onKeyDown={(e) => {
                if (e.key === 'Enter') {
                  e.preventDefault();
                  commitZoomInput();
                  (e.target as HTMLInputElement).blur();
                }
              }}
            />
            <span className="drawing-pdf-toolbar__zoom-suffix" aria-hidden="true">
              %
            </span>
          </div>
          <button
            type="button"
            title="확대"
            onClick={() => applyZoom(zoom + ZOOM_STEP)}
          >
            <ZoomIn size={18} />
          </button>
          <button type="button" title="전체 화면" onClick={handleFullscreen}>
            <Maximize size={18} />
          </button>
          <button type="button" title="PDF 다운로드" onClick={() => void handleDownload()}>
            <Download size={18} />
          </button>
        </div>
      </div>

      <div className="drawing-pdf-scroll">
        {loadState === 'loading' && <p className="drawing-pdf-loading">PDF 불러오는 중…</p>}
        {loadState === 'error' && <p className="drawing-pdf-loading">PDF를 불러오지 못했습니다.</p>}
        {loadState === 'ready' && fileSource && (
          <div
            className="drawing-pdf-scroll__inner"
            style={{ width: `${pageWidth}px`, minWidth: `${pageWidth}px` }}
          >
            <Document
              key={fileSource}
              file={fileSource}
              onLoadSuccess={({ numPages: pages }) => setNumPages(pages)}
              loading={<p className="drawing-pdf-loading">PDF 불러오는 중…</p>}
              error={<p className="drawing-pdf-loading">PDF를 불러오지 못했습니다.</p>}
              className="drawing-pdf-document"
            >
              {Array.from(new Array(numPages), (_, index) => (
                <Page
                  key={`page_${index + 1}`}
                  className="drawing-pdf-page"
                  pageNumber={index + 1}
                  width={pageWidth}
                  devicePixelRatio={devicePixelRatio}
                  renderTextLayer={false}
                  renderAnnotationLayer={false}
                />
              ))}
            </Document>
          </div>
        )}
      </div>
    </div>
  );
}
