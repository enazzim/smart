-- 게시판 moderate: SYSTEM_ADMIN만 유지 (타인 글 삭제·고정 방지)
DELETE rp
FROM role_permission rp
JOIN role r ON r.id = rp.role_id
JOIN permission p ON p.id = rp.permission_id
WHERE p.permission_code = 'community:board:moderate'
  AND r.role_code <> 'SYSTEM_ADMIN'
  AND r.recording_state = 1;
