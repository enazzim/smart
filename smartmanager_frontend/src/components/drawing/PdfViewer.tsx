import { ZoomIn, ZoomOut, Maximize, Download } from 'lucide-react';
import { useEffect, useRef, useState } from 'react';
import { Document, Page, pdfjs } from 'react-pdf';
import { fetchDrawingPdfBlob } from '../../api/drawing';
import { cacheDrawingPdf, getCachedDrawingPdf } from '../../services/drawingOfflineCacheService';
import 'react-pdf/dist/Page/AnnotationLayer.css';
import 'react-pdf/dist/Page/TextLayer.css';

pdfjs.GlobalWorkerOptions.workerSrc = `//unpkg.com/pdfjs-dist@${pdfjs.version}/build/pdf.worker.min.mjs`;

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
  const [numPages, setNumPages] = useState<number>();
  const [loadState, setLoadState] = useState<LoadState>('loading');
  const [fileSource, setFileSource] = useState<string | null>(null);
  const [fromCache, setFromCache] = useState(false);
  const [resolvedBlob, setResolvedBlob] = useState<Blob | null>(null);
  const viewerRef = useRef<HTMLDivElement>(null);
  const objectUrlRef = useRef<string | null>(null);
  const onErrorRef = useRef(onError);
  onErrorRef.current = onError;

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
    };

    const load = async () => {
      setLoadState('loading');
      setFileSource(null);
      setResolvedBlob(null);
      setFromCache(false);
      setNumPages(undefined);
      setZoom(100);

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
          <button type="button" title="축소" onClick={() => setZoom((z) => Math.max(50, z - 10))}>
            <ZoomOut size={18} />
          </button>
          <span className="drawing-pdf-toolbar__zoom">{zoom}%</span>
          <button type="button" title="확대" onClick={() => setZoom((z) => Math.min(200, z + 10))}>
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
          <Document
            key={fileSource}
            file={fileSource}
            onLoadSuccess={({ numPages: pages }) => setNumPages(pages)}
            loading={<p className="drawing-pdf-loading">PDF 불러오는 중…</p>}
            error={<p className="drawing-pdf-loading">PDF를 불러오지 못했습니다.</p>}
          >
            {Array.from(new Array(numPages), (_, index) => (
              <div
                key={`page_${index + 1}`}
                className="drawing-pdf-page"
                style={{ transform: `scale(${zoom / 100})` }}
              >
                <Page pageNumber={index + 1} renderTextLayer={false} renderAnnotationLayer={false} width={800} />
              </div>
            ))}
          </Document>
        )}
      </div>
    </div>
  );
}
