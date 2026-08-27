/** 필수 열람 미확인 뱃지 */
export default function RequiredUnreadBadge({ show }: { show?: boolean }) {
  if (!show) {
    return null;
  }
  return (
    <span className="board-required-unread-badge" title="필수 열람 미확인" aria-label="필수 열람 미확인">
      미확인
    </span>
  );
}
