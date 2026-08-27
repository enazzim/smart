-- 공지사항(NOTICE) 필수 열람 대상

CREATE TABLE IF NOT EXISTS board_post_required_reader (
  id              BIGINT      NOT NULL AUTO_INCREMENT PRIMARY KEY,
  post_id         BIGINT      NOT NULL,
  user_id         BIGINT      NOT NULL,
  UNIQUE KEY uk_board_post_required_reader (post_id, user_id),
  INDEX idx_board_post_required_reader_user (user_id, post_id),
  CONSTRAINT fk_board_post_required_reader_post FOREIGN KEY (post_id) REFERENCES board_post (id),
  CONSTRAINT fk_board_post_required_reader_user FOREIGN KEY (user_id) REFERENCES `user` (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
