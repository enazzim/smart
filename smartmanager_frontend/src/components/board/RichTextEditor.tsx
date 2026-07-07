import { useEffect, useRef } from 'react';

interface RichTextEditorProps {
  value: string;
  onChange: (html: string) => void;
  disabled?: boolean;
}

export default function RichTextEditor({ value, onChange, disabled = false }: RichTextEditorProps) {
  const editorRef = useRef<HTMLDivElement | null>(null);

  useEffect(() => {
    if (!editorRef.current) return;
    if (editorRef.current.innerHTML !== value) {
      editorRef.current.innerHTML = value;
    }
  }, [value]);

  const exec = (command: string, arg?: string) => {
    if (disabled) return;
    document.execCommand(command, false, arg);
    editorRef.current?.focus();
    onChange(editorRef.current?.innerHTML ?? '');
  };

  return (
    <div className="rich-text-editor">
      <div className="rich-text-toolbar">
        <button type="button" disabled={disabled} onMouseDown={(e) => e.preventDefault()} onClick={() => exec('bold')}>
          굵게
        </button>
        <button type="button" disabled={disabled} onMouseDown={(e) => e.preventDefault()} onClick={() => exec('italic')}>
          기울임
        </button>
        <button type="button" disabled={disabled} onMouseDown={(e) => e.preventDefault()} onClick={() => exec('underline')}>
          밑줄
        </button>
        <button
          type="button"
          disabled={disabled}
          onMouseDown={(e) => e.preventDefault()}
          onClick={() => exec('insertUnorderedList')}
        >
          목록
        </button>
        <button
          type="button"
          disabled={disabled}
          onMouseDown={(e) => e.preventDefault()}
          onClick={() => {
            const url = window.prompt('링크 URL');
            if (url) exec('createLink', url);
          }}
        >
          링크
        </button>
      </div>
      <div
        ref={editorRef}
        className="rich-text-body"
        contentEditable={!disabled}
        suppressContentEditableWarning
        onInput={() => onChange(editorRef.current?.innerHTML ?? '')}
      />
    </div>
  );
}
