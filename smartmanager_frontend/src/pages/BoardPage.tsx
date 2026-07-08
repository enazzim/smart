import { useCallback, useEffect, useRef, useState } from 'react';
import RichTextEditor from '../components/board/RichTextEditor';
import {
  BOARD_TYPE_LABELS,
  type BoardAttachment,
  type BoardPostDetail,
  type BoardPostSummary,
  type BoardType,
  addBoardAttachments,
  createBoardPost,
  createBoardReply,
  deleteBoardAttachment,
  deleteBoardPost,
  downloadBoardAttachment,
  fetchBoardPost,
  fetchBoardPosts,
  fetchEditorTemplate,
  formatBoardDate,
  formatFileSize,
  replaceBoardAttachment,
  updateBoardPost,
} from '../api/board';
import type { AuthenticatedUser } from '../api/auth';

export type BoardScreen =
  | { mode: 'list' }
  | { mode: 'detail'; postId: number }
  | { mode: 'compose'; composeMode: 'create' | 'edit' | 'reply'; postId?: number; parentPostId?: number };

interface BoardPageProps {
  boardType: BoardType;
  screen: BoardScreen;
  currentUser: AuthenticatedUser | null;
  onNavigateHome: () => void;
  onNavigateList: () => void;
  onNavigateDetail: (postId: number) => void;
  onNavigateCompose: (compose: {
    composeMode: 'create' | 'edit' | 'reply';
    postId?: number;
    parentPostId?: number;
  }) => void;
}

const PAGE_SIZE = 20;
const MAX_ATTACHMENT_BYTES = 100 * 1024 * 1024;

function UploadIcon() {
  return (
    <svg className="import-upload-icon" viewBox="0 0 24 24" aria-hidden="true">
      <path
        d="M12 16V4m0 0L7 9m5-5 5 5M4 20h16"
        fill="none"
        stroke="currentColor"
        strokeWidth="1.8"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  );
}

export default function BoardPage({
  boardType,
  screen,
  currentUser,
  onNavigateHome,
  onNavigateList,
  onNavigateDetail,
  onNavigateCompose,
}: BoardPageProps) {
  const boardTitle = BOARD_TYPE_LABELS[boardType];
  const canWrite = currentUser?.authorities.includes('community:board:write') ?? false;

  if (screen.mode === 'list') {
    return (
      <BoardListView
        boardType={boardType}
        boardTitle={boardTitle}
        canWrite={canWrite}
        onNavigateHome={onNavigateHome}
        onNavigateDetail={onNavigateDetail}
        onNavigateCompose={() => onNavigateCompose({ composeMode: 'create' })}
      />
    );
  }

  if (screen.mode === 'detail') {
    return (
      <BoardDetailView
        boardType={boardType}
        boardTitle={boardTitle}
        postId={screen.postId}
        currentUser={currentUser}
        canWrite={canWrite}
        onNavigateHome={onNavigateHome}
        onNavigateList={onNavigateList}
        onNavigateDetail={onNavigateDetail}
        onNavigateCompose={onNavigateCompose}
      />
    );
  }

  return (
    <BoardComposeView
      boardType={boardType}
      boardTitle={boardTitle}
      composeMode={screen.composeMode}
      postId={screen.postId}
      parentPostId={screen.parentPostId}
      onNavigateHome={onNavigateHome}
      onNavigateList={onNavigateList}
      onNavigateDetail={onNavigateDetail}
    />
  );
}

function BoardListView({
  boardType,
  boardTitle,
  canWrite,
  onNavigateHome,
  onNavigateDetail,
  onNavigateCompose,
}: {
  boardType: BoardType;
  boardTitle: string;
  canWrite: boolean;
  onNavigateHome: () => void;
  onNavigateDetail: (postId: number) => void;
  onNavigateCompose: () => void;
}) {
  const [items, setItems] = useState<BoardPostSummary[]>([]);
  const [total, setTotal] = useState(0);
  const [page, setPage] = useState(0);
  const [keyword, setKeyword] = useState('');
  const [searchInput, setSearchInput] = useState('');
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const load = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const result = await fetchBoardPosts(boardType, { keyword, page, size: PAGE_SIZE });
      setItems(result.items);
      setTotal(result.totalElements);
    } catch (e) {
      setError(e instanceof Error ? e.message : '목록 조회 실패');
    } finally {
      setLoading(false);
    }
  }, [boardType, keyword, page]);

  useEffect(() => {
    void load();
  }, [load]);

  const totalPages = Math.max(1, Math.ceil(total / PAGE_SIZE));

  return (
    <div className="page board-page">
      <BoardBreadcrumb boardTitle={boardTitle} onNavigateHome={onNavigateHome} />
      <header className="page-header">
        <div>
          <h1>{boardTitle}</h1>
          <p>총 {total}건</p>
        </div>
        {canWrite && (
          <button type="button" onClick={onNavigateCompose}>
            글쓰기
          </button>
        )}
      </header>

      <div className="board-toolbar">
        <input
          type="search"
          placeholder="제목 검색"
          value={searchInput}
          onChange={(e) => setSearchInput(e.target.value)}
          onKeyDown={(e) => {
            if (e.key === 'Enter') {
              setPage(0);
              setKeyword(searchInput.trim());
            }
          }}
        />
        <button
          type="button"
          className="secondary"
          onClick={() => {
            setPage(0);
            setKeyword(searchInput.trim());
          }}
        >
          검색
        </button>
      </div>

      {error && <p className="error-banner">{error}</p>}

      <div className="table-wrap">
        <table>
          <thead>
            <tr>
              <th>번호</th>
              <th>제목</th>
              <th>작성자</th>
              <th>등록일</th>
              <th>조회</th>
              <th>첨부</th>
            </tr>
          </thead>
          <tbody>
            {loading ? (
              <tr>
                <td colSpan={6}>불러오는 중…</td>
              </tr>
            ) : items.length === 0 ? (
              <tr>
                <td colSpan={6}>등록된 글이 없습니다.</td>
              </tr>
            ) : (
              items.map((item, index) => (
                <tr key={item.id}>
                  <td>{total - page * PAGE_SIZE - index}</td>
                  <td>
                    <button type="button" className="link-button" onClick={() => onNavigateDetail(item.id)}>
                      {item.pinned ? '[고정] ' : ''}
                      {item.title}
                    </button>
                  </td>
                  <td>{item.authorName}</td>
                  <td>{formatBoardDate(item.createdAt)}</td>
                  <td>{item.viewCount}</td>
                  <td>{item.hasAttachment ? 'Y' : ''}</td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      <div className="board-pagination">
        <button type="button" className="secondary" disabled={page <= 0} onClick={() => setPage((p) => p - 1)}>
          이전
        </button>
        <span>
          {page + 1} / {totalPages}
        </span>
        <button
          type="button"
          className="secondary"
          disabled={page + 1 >= totalPages}
          onClick={() => setPage((p) => p + 1)}
        >
          다음
        </button>
      </div>
    </div>
  );
}

function BoardDetailView({
  boardType,
  boardTitle,
  postId,
  currentUser,
  canWrite,
  onNavigateHome,
  onNavigateList,
  onNavigateDetail,
  onNavigateCompose,
}: {
  boardType: BoardType;
  boardTitle: string;
  postId: number;
  currentUser: AuthenticatedUser | null;
  canWrite: boolean;
  onNavigateHome: () => void;
  onNavigateList: () => void;
  onNavigateDetail: (postId: number) => void;
  onNavigateCompose: BoardPageProps['onNavigateCompose'];
}) {
  const [post, setPost] = useState<BoardPostDetail | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [submitting, setSubmitting] = useState(false);

  const load = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const detail = await fetchBoardPost(boardType, postId);
      setPost(detail);
    } catch (e) {
      setError(e instanceof Error ? e.message : '상세 조회 실패');
    } finally {
      setLoading(false);
    }
  }, [boardType, postId]);

  useEffect(() => {
    void load();
  }, [load]);

  const isOwner = post != null && currentUser != null && post.authorUserId === currentUser.id;
  const canModerate = currentUser?.authorities.includes('community:board:moderate') ?? false;
  const canModify = canWrite && (isOwner || canModerate);

  const onDelete = async () => {
    if (!window.confirm('게시글을 삭제하시겠습니까? 첨부파일도 함께 삭제됩니다.')) return;
    setSubmitting(true);
    setError(null);
    try {
      await deleteBoardPost(boardType, postId);
      onNavigateList();
    } catch (e) {
      setError(e instanceof Error ? e.message : '삭제 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDeleteAttachment = async (attachment: BoardAttachment) => {
    if (!window.confirm(`첨부파일 "${attachment.originalFileName}"을(를) 삭제하시겠습니까?`)) return;
    setSubmitting(true);
    setError(null);
    try {
      await deleteBoardAttachment(boardType, postId, attachment.id);
      await load();
    } catch (e) {
      setError(e instanceof Error ? e.message : '첨부 삭제 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onReplaceAttachment = async (attachment: BoardAttachment, file: File) => {
    setSubmitting(true);
    setError(null);
    try {
      await replaceBoardAttachment(boardType, postId, attachment.id, file);
      await load();
    } catch (e) {
      setError(e instanceof Error ? e.message : '첨부 교체 실패');
    } finally {
      setSubmitting(false);
    }
  };

  if (loading) {
    return (
      <div className="page board-page">
        <BoardBreadcrumb boardTitle={boardTitle} onNavigateHome={onNavigateHome} onNavigateList={onNavigateList} />
        <p>불러오는 중…</p>
      </div>
    );
  }

  if (!post) {
    return (
      <div className="page board-page">
        <BoardBreadcrumb boardTitle={boardTitle} onNavigateHome={onNavigateHome} onNavigateList={onNavigateList} />
        <p className="error-banner">{error ?? '게시글을 찾을 수 없습니다.'}</p>
      </div>
    );
  }

  return (
    <div className="page board-page">
      <BoardBreadcrumb boardTitle={boardTitle} onNavigateHome={onNavigateHome} onNavigateList={onNavigateList} />
      {error && <p className="error-banner">{error}</p>}

      <article className="board-article">
        <header className="board-article-header">
          <h1>{post.title || '(제목 없음)'}</h1>
          <div className="board-meta">
            <span>{post.authorName}</span>
            <span>{formatBoardDate(post.createdAt)}</span>
            <span>조회 {post.viewCount}</span>
          </div>
          <div className="board-actions">
            {canWrite && post.postKind === 'TOP' && (
              <button
                type="button"
                className="secondary"
                disabled={submitting}
                onClick={() => onNavigateCompose({ composeMode: 'reply', parentPostId: post.id })}
              >
                답변
              </button>
            )}
            {canModify && (
              <>
                <button
                  type="button"
                  className="secondary"
                  disabled={submitting}
                  onClick={() => onNavigateCompose({ composeMode: 'edit', postId: post.id })}
                >
                  수정
                </button>
                <button type="button" className="danger" disabled={submitting} onClick={() => void onDelete()}>
                  삭제
                </button>
              </>
            )}
          </div>
        </header>

        <div className="board-content" dangerouslySetInnerHTML={{ __html: post.content }} />

        {post.attachments.length > 0 && (
          <section className="board-attachments">
            <h3>첨부파일</h3>
            <ul>
              {post.attachments.map((attachment) => (
                <li key={attachment.id} className="board-attachment-row">
                  <button
                    type="button"
                    className="link-button"
                    onClick={() =>
                      void downloadBoardAttachment(
                        boardType,
                        postId,
                        attachment.id,
                        attachment.originalFileName,
                      )
                    }
                  >
                    {attachment.originalFileName}
                  </button>
                  <span>{formatFileSize(attachment.fileSize)}</span>
                  {canModify && (
                    <>
                      <label className="file-replace-label">
                        교체
                        <input
                          type="file"
                          hidden
                          disabled={submitting}
                          onChange={(e) => {
                            const file = e.target.files?.[0];
                            e.target.value = '';
                            if (file) void onReplaceAttachment(attachment, file);
                          }}
                        />
                      </label>
                      <button
                        type="button"
                        className="danger-link"
                        disabled={submitting}
                        onClick={() => void onDeleteAttachment(attachment)}
                      >
                        삭제
                      </button>
                    </>
                  )}
                </li>
              ))}
            </ul>
          </section>
        )}
      </article>

      {post.replies.length > 0 && (
        <section className="board-replies">
          <h2>답변 ({post.replies.length})</h2>
          {post.replies.map((reply) => (
            <article key={reply.id} className="board-reply">
              <div className="board-meta">
                <span>{reply.authorName}</span>
                <span>{formatBoardDate(reply.createdAt)}</span>
              </div>
              <div className="board-content" dangerouslySetInnerHTML={{ __html: reply.content }} />
              {reply.attachments.length > 0 && (
                <ul className="board-reply-attachments">
                  {reply.attachments.map((attachment) => (
                    <li key={attachment.id}>
                      <button
                        type="button"
                        className="link-button"
                        onClick={() =>
                          void downloadBoardAttachment(
                            boardType,
                            reply.id,
                            attachment.id,
                            attachment.originalFileName,
                          )
                        }
                      >
                        {attachment.originalFileName}
                      </button>
                    </li>
                  ))}
                </ul>
              )}
              <button type="button" className="link-button" onClick={() => onNavigateDetail(reply.id)}>
                이 답변 보기
              </button>
            </article>
          ))}
        </section>
      )}
    </div>
  );
}

function BoardComposeView({
  boardType,
  boardTitle,
  composeMode,
  postId,
  parentPostId,
  onNavigateHome,
  onNavigateList,
  onNavigateDetail,
}: {
  boardType: BoardType;
  boardTitle: string;
  composeMode: 'create' | 'edit' | 'reply';
  postId?: number;
  parentPostId?: number;
  onNavigateHome: () => void;
  onNavigateList: () => void;
  onNavigateDetail: (postId: number) => void;
}) {
  const [title, setTitle] = useState('');
  const [content, setContent] = useState('');
  const [files, setFiles] = useState<File[]>([]);
  const [loading, setLoading] = useState(composeMode !== 'create');
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [dragOver, setDragOver] = useState(false);
  const fileInputRef = useRef<HTMLInputElement>(null);

  const appendFiles = (incoming: FileList | null) => {
    if (!incoming || incoming.length === 0) {
      return;
    }
    const oversize: string[] = [];
    setFiles((prev) => {
      const next = [...prev];
      for (const file of Array.from(incoming)) {
        if (file.size > MAX_ATTACHMENT_BYTES) {
          oversize.push(file.name);
          continue;
        }
        const duplicate = next.some((f) => f.name === file.name && f.size === file.size);
        if (!duplicate) {
          next.push(file);
        }
      }
      return next;
    });
    if (oversize.length > 0) {
      setError(`${oversize.join(', ')} — 파일당 100MB 이하만 첨부할 수 있습니다.`);
    } else {
      setError(null);
    }
  };

  const removeFile = (index: number) => {
    setFiles((prev) => prev.filter((_, i) => i !== index));
  };

  const clearFiles = () => {
    setFiles([]);
    if (fileInputRef.current) {
      fileInputRef.current.value = '';
    }
  };

  useEffect(() => {
    const init = async () => {
      setLoading(true);
      setError(null);
      try {
        if (composeMode === 'create') {
          const template = await fetchEditorTemplate();
          setContent(template);
          setTitle('');
          setFiles([]);
        } else if (composeMode === 'edit' && postId != null) {
          const post = await fetchBoardPost(boardType, postId);
          setTitle(post.title ?? '');
          setContent(post.content);
          setFiles([]);
        } else if (composeMode === 'reply') {
          const template = await fetchEditorTemplate();
          setContent(template);
          setTitle('');
          setFiles([]);
        }
      } catch (e) {
        setError(e instanceof Error ? e.message : '초기화 실패');
      } finally {
        setLoading(false);
      }
    };
    void init();
  }, [boardType, composeMode, postId]);

  const heading =
    composeMode === 'create' ? '글쓰기' : composeMode === 'edit' ? '글 수정' : '답변 작성';

  const onSubmit = async () => {
    setSubmitting(true);
    setError(null);
    try {
      if (composeMode === 'create') {
        const created = await createBoardPost(boardType, { title, content, files });
        onNavigateDetail(created.id);
        return;
      }
      if (composeMode === 'edit' && postId != null) {
        const updated = await updateBoardPost(boardType, postId, { title, content });
        if (files.length > 0) {
          await addBoardAttachments(boardType, postId, files);
        }
        onNavigateDetail(updated.id);
        return;
      }
      if (composeMode === 'reply' && parentPostId != null) {
        const created = await createBoardReply(boardType, parentPostId, { content, files });
        onNavigateDetail(created.id);
      }
    } catch (e) {
      setError(e instanceof Error ? e.message : '저장 실패');
    } finally {
      setSubmitting(false);
    }
  };

  if (loading) {
    return (
      <div className="page board-page">
        <BoardBreadcrumb boardTitle={boardTitle} onNavigateHome={onNavigateHome} onNavigateList={onNavigateList} />
        <p>불러오는 중…</p>
      </div>
    );
  }

  return (
    <div className="page board-page">
      <BoardBreadcrumb boardTitle={boardTitle} onNavigateHome={onNavigateHome} onNavigateList={onNavigateList} />
      <header className="page-header">
        <h1>
          {boardTitle} — {heading}
        </h1>
      </header>

      {error && <p className="error-banner">{error}</p>}

      <div className="board-compose-form">
        {composeMode !== 'reply' && (
          <label>
            제목
            <input
              type="text"
              value={title}
              disabled={submitting}
              onChange={(e) => setTitle(e.target.value)}
            />
          </label>
        )}

        <label>
          본문
          <RichTextEditor value={content} onChange={setContent} disabled={submitting} />
        </label>

        <div className="board-compose-attachments">
          <span className="board-compose-label">첨부파일 (복수 선택 가능, 파일당 100MB 이하)</span>
          <div
            className={`import-dropzone board-attachment-dropzone${dragOver ? ' import-dropzone--active' : ''}${files.length > 0 ? ' import-dropzone--filled' : ''}`}
            role="button"
            tabIndex={0}
            onClick={() => !submitting && fileInputRef.current?.click()}
            onKeyDown={(e) => {
              if (!submitting && (e.key === 'Enter' || e.key === ' ')) {
                e.preventDefault();
                fileInputRef.current?.click();
              }
            }}
            onDragOver={(e) => {
              e.preventDefault();
              if (!submitting) {
                setDragOver(true);
              }
            }}
            onDragLeave={() => setDragOver(false)}
            onDrop={(e) => {
              e.preventDefault();
              setDragOver(false);
              if (!submitting) {
                appendFiles(e.dataTransfer.files);
              }
            }}
          >
            <input
              ref={fileInputRef}
              type="file"
              multiple
              hidden
              disabled={submitting}
              onChange={(e) => {
                appendFiles(e.target.files);
                e.target.value = '';
              }}
            />
            <UploadIcon />
            {files.length > 0 ? (
              <div className="import-dropzone__file">
                <strong>{files.length}개 파일 선택됨</strong>
                <span>클릭하거나 드래그하여 파일을 추가할 수 있습니다</span>
                {!submitting && (
                  <button
                    type="button"
                    className="import-clear-btn"
                    onClick={(e) => {
                      e.stopPropagation();
                      clearFiles();
                    }}
                  >
                    전체 제거
                  </button>
                )}
              </div>
            ) : (
              <>
                <p className="import-dropzone__title">첨부파일 업로드</p>
                <p className="import-dropzone__hint">클릭하거나 파일을 여기로 드래그하세요</p>
              </>
            )}
          </div>
          {files.length > 0 && (
            <ul className="board-selected-files">
              {files.map((file, index) => (
                <li key={`${file.name}-${file.size}-${index}`}>
                  <span>
                    {file.name} ({formatFileSize(file.size)})
                  </span>
                  {!submitting && (
                    <button
                      type="button"
                      className="import-clear-btn"
                      onClick={() => removeFile(index)}
                    >
                      제거
                    </button>
                  )}
                </li>
              ))}
            </ul>
          )}
        </div>

        <div className="form-actions">
          <button type="button" disabled={submitting} onClick={() => void onSubmit()}>
            저장
          </button>
          <button
            type="button"
            className="secondary"
            disabled={submitting}
            onClick={() => {
              if (composeMode === 'edit' && postId != null) {
                onNavigateDetail(postId);
              } else if (composeMode === 'reply' && parentPostId != null) {
                onNavigateDetail(parentPostId);
              } else {
                onNavigateList();
              }
            }}
          >
            취소
          </button>
        </div>
      </div>
    </div>
  );
}

function BoardBreadcrumb({
  boardTitle,
  onNavigateHome,
  onNavigateList,
}: {
  boardTitle: string;
  onNavigateHome: () => void;
  onNavigateList?: () => void;
}) {
  return (
    <nav className="board-breadcrumb" aria-label="게시판 경로">
      <button type="button" className="link-button" onClick={onNavigateHome}>
        대시보드
      </button>
      {onNavigateList && (
        <>
          <span>/</span>
          <button type="button" className="link-button" onClick={onNavigateList}>
            {boardTitle}
          </button>
        </>
      )}
    </nav>
  );
}
