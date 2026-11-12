-- 로그인 아이디는 활성(recording_state=1) 행만 유니크.
-- 소프트 삭제(recording_state=0)는 같은 아이디를 여러 건 남길 수 있다.
-- MariaDB UNIQUE는 NULL을 서로 다른 값으로 보므로, 비활성 행은 NULL로 둔다.

ALTER TABLE `user`
  ADD COLUMN `login_id_active` VARCHAR(100)
    GENERATED ALWAYS AS (CASE WHEN `recording_state` = 1 THEN `login_id` ELSE NULL END) STORED,
  ADD UNIQUE KEY `uk_user_login_id_active` (`login_id_active`);

ALTER TABLE `user` DROP INDEX `uk_user_login_id`;
