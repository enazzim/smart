import { ZoomIn, ZoomOut, Maximize, Download } from 'lucide-react';
import { useMemo, useRef, useState } from 'react';
import { Document, Page, pdfjs } from 'react-pdf';
import { drawingPdfAuthHeaders } from '../../api/drawing';
import 'react-pdf/dist/Page/AnnotationLayer.css';
import 'react-pdf/dist/Page/TextLayer.css';

pdfjs.GlobalWorkerOptions.workerSrc = `//unpkg.com/pdfjs-dist@${pdfjs.version}/build/pdf.worker.min.mjs`;

interface PdfViewerProps {
  pdfUrl: string;
  partNo: string;
  toolbarActions?: React.ReactNode;
  onError?: (message: string) => void;
}

export default function PdfViewer({ pdfUrl, partNo, toolbarActions, onError }: PdfViewerProps) {
  const [zoom, setZoom] = useState(100);
  const [numPages, setNumPages] = useState<number>();
  const viewerRef = useRef<HTMLDivElement>(null);

  const fileSource = useMemo(
    () => ({
      url: pdfUrl,
      httpHeaders: drawingPdfAuthHeaders(),
    }),
    [pdfUrl],
  );

  const handleContextMenu = (e: React.MouseEvent) => {
    e.preventDefault();
  };

  const handleDownload = async () => {
    try {
      const headers = drawingPdfAuthHeaders();
      const response = await fetch(pdfUrl, { headers });
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      const blob = await response.blob();
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      link.download = `${partNo}.pdf`;
      document.body.appendChild(link);
      link.click();
      window.URL.revokeObjectURL(url);
      document.body.removeChild(link);
    } catch {
      onError?.('다운로드 중 오류가 발생했습니다.');
    }
  };

  const handleFullscreen = () => {
    if (!document.fullscreenElement) {
      void viewerRef.current?.requestFullscreen();
    } else {
      void document.exitFullscreen();
    }
  };

  return (
    <div ref={viewerRef} className="drawing-pdf-viewer" onContextMenu={handleContextMenu}>
      <div className="drawing-pdf-toolbar">
        <div className="drawing-pdf-toolbar__left">
          <span className="drawing-pdf-toolbar__title">{partNo} 도면 뷰어</span>
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
        <Document
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
      </div>
    </div>
  );
}
