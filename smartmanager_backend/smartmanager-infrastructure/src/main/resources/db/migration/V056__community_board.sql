-- COMM-1: 공통 게시판 엔진 (board_post, board_attachment)

CREATE TABLE IF NOT EXISTS board_post (
  id               BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  board_type       ENUM('NOTICE','PRESIDENT_NOTICE','PRODUCT','LASER','INSTITUTE','SALES_QC') NOT NULL,
  post_kind        ENUM('TOP','REPLY') NOT NULL,
  parent_post_id   BIGINT         NULL,
  thread_root_id   BIGINT         NULL,
  title            VARCHAR(500)   NULL,
  content          MEDIUMTEXT     NULL,
  author_user_id   BIGINT         NULL,
  view_count       INT            NOT NULL DEFAULT 0,
  pinned           TINYINT        NOT NULL DEFAULT 0,
  recording_state  TINYINT        NOT NULL DEFAULT 1,
  created_by       VARCHAR(100)   NULL,
  created_by_id    VARCHAR(100)   NULL,
  created_at       DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by       VARCHAR(100)   NULL,
  updated_by_id    VARCHAR(100)   NULL,
  updated_at       DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  INDEX idx_board_post_list (board_type, post_kind, recording_state, created_at),
  INDEX idx_board_post_thread (thread_root_id, created_at),
  INDEX idx_board_post_parent (parent_post_id),
  CONSTRAINT fk_board_post_author_user FOREIGN KEY (author_user_id) REFERENCES `user` (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 게시글 1건당 첨부파일 여러 개 (1:N). 파일 1개당 최대 100MB는 API·yml에서 검증.
CREATE TABLE IF NOT EXISTS board_attachment (
  id                 BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  post_id            BIGINT         NOT NULL,
  original_file_name VARCHAR(500)   NOT NULL,
  stored_file_name   VARCHAR(500)   NOT NULL,
  content_type       VARCHAR(100)   NULL,
  file_size          BIGINT         NOT NULL,
  recording_state    TINYINT        NOT NULL DEFAULT 1,
  created_at         DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  INDEX idx_board_attachment_post (post_id, recording_state),
  CONSTRAINT fk_board_attachment_post FOREIGN KEY (post_id) REFERENCES board_post (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('community:board:read', '게시판 조회'),
('community:board:write', '게시판 작성·수정'),
('community:board:moderate', '게시판 글 고정·삭제')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('community:board:read','community:board:write','community:board:moderate')
WHERE r.role_code = 'SYSTEM_ADMIN' AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'community:board:read'
WHERE r.role_code IN ('VIEWER','SALES_OPERATOR','PRODUCTION_OPERATOR','PURCHASE_OPERATOR','BASIS_MANAGER') 
  AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code = 'community:board:write'
WHERE r.role_code IN ('BASIS_MANAGER','SALES_OPERATOR','PRODUCTION_OPERATOR','PURCHASE_OPERATOR')
  AND r.recording_state = 1;

