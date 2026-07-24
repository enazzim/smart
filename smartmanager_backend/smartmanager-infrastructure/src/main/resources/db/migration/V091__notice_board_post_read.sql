-- 공지사항(NOTICE) 원글 열람자 추적

CREATE TABLE IF NOT EXISTS board_post_read (
  id              BIGINT      NOT NULL AUTO_INCREMENT PRIMARY KEY,
  post_id         BIGINT      NOT NULL,
  reader_user_id  BIGINT      NOT NULL,
  read_at         DATETIME(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_board_post_read_post_reader (post_id, reader_user_id),
  INDEX idx_board_post_read_post (post_id, read_at),
  CONSTRAINT fk_board_post_read_post FOREIGN KEY (post_id) REFERENCES board_post (id),
  CONSTRAINT fk_board_post_read_user FOREIGN KEY (reader_user_id) REFERENCES `user` (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
