-- *_by 이름 컬럼 제거, *_by_id 를 user.id (BIGINT FK) 로 정규화.
-- 매칭 실패(local-dev, seed 등)는 NULL. domain_event.actor_user_id 는 스냅샷이라 제외.

-- user
ALTER TABLE `user`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `user` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `user`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `user`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- board_post
ALTER TABLE `board_post`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `board_post` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `board_post`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `board_post`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- bom_change_log
ALTER TABLE `bom_change_log`
  ADD COLUMN `changed_by_id_n` BIGINT NULL;
UPDATE `bom_change_log` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`changed_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`changed_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`changed_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`changed_by` AND u0b.recording_state = 1
SET
  t.`changed_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id);
ALTER TABLE `bom_change_log`
  DROP COLUMN `changed_by`,
  DROP COLUMN `changed_by_id`;
ALTER TABLE `bom_change_log`
  CHANGE COLUMN `changed_by_id_n` `changed_by_id` BIGINT NULL;

-- company
ALTER TABLE `company`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `company` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `company`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `company`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- defect_claim
ALTER TABLE `defect_claim`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `defect_claim` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `defect_claim`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `defect_claim`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- drawing_master
ALTER TABLE `drawing_master`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `drawing_master` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `drawing_master`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `drawing_master`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- drawing_reference
ALTER TABLE `drawing_reference`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `drawing_reference` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `drawing_reference`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `drawing_reference`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- equipment
ALTER TABLE `equipment`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `equipment` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `equipment`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `equipment`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- etc_claim
ALTER TABLE `etc_claim`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `etc_claim` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `etc_claim`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `etc_claim`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- etc_purchase_order
ALTER TABLE `etc_purchase_order`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `etc_purchase_order` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `etc_purchase_order`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `etc_purchase_order`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- etc_purchase_receipt
ALTER TABLE `etc_purchase_receipt`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `etc_purchase_receipt` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `etc_purchase_receipt`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `etc_purchase_receipt`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- inventory_balance
ALTER TABLE `inventory_balance`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `inventory_balance` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `inventory_balance`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `inventory_balance`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- inventory_lot
ALTER TABLE `inventory_lot`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `inventory_lot` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `inventory_lot`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `inventory_lot`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- inventory_lot_balance
ALTER TABLE `inventory_lot_balance`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `inventory_lot_balance` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `inventory_lot_balance`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `inventory_lot_balance`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- item
ALTER TABLE `item`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `item` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `item`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `item`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- item_composition
ALTER TABLE `item_composition`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `item_composition` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `item_composition`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `item_composition`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- lot_genealogy
ALTER TABLE `lot_genealogy`
  ADD COLUMN `created_by_id_n` BIGINT NULL;
UPDATE `lot_genealogy` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id);
ALTER TABLE `lot_genealogy`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`;
ALTER TABLE `lot_genealogy`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL;

-- material_issue
ALTER TABLE `material_issue`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `material_issue` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `material_issue`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `material_issue`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- material_requirement_line
ALTER TABLE `material_requirement_line`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `material_requirement_line` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `material_requirement_line`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `material_requirement_line`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- misc_stock_movement
ALTER TABLE `misc_stock_movement`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `misc_stock_movement` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `misc_stock_movement`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `misc_stock_movement`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- month_closing
ALTER TABLE `month_closing`
  ADD COLUMN `closed_by_id_n` BIGINT NULL;
UPDATE `month_closing` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`closed_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`closed_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`closed_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`closed_by` AND u0b.recording_state = 1
SET
  t.`closed_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id);
ALTER TABLE `month_closing`
  DROP COLUMN `closed_by`,
  DROP COLUMN `closed_by_id`;
ALTER TABLE `month_closing`
  CHANGE COLUMN `closed_by_id_n` `closed_by_id` BIGINT NULL;

-- mrp_run
ALTER TABLE `mrp_run`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `mrp_run` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `mrp_run`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `mrp_run`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- outsource_history
ALTER TABLE `outsource_history`
  ADD COLUMN `created_by_id_n` BIGINT NULL;
UPDATE `outsource_history` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id);
ALTER TABLE `outsource_history`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`;
ALTER TABLE `outsource_history`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL;

-- outsourcing_order
ALTER TABLE `outsourcing_order`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `outsourcing_order` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `outsourcing_order`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `outsourcing_order`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- outsourcing_order_line
ALTER TABLE `outsourcing_order_line`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `outsourcing_order_line` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `outsourcing_order_line`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `outsourcing_order_line`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- outsourcing_receipt
ALTER TABLE `outsourcing_receipt`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `outsourcing_receipt` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `outsourcing_receipt`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `outsourcing_receipt`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- outsourcing_receipt_line
ALTER TABLE `outsourcing_receipt_line`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `outsourcing_receipt_line` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `outsourcing_receipt_line`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `outsourcing_receipt_line`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- outsourcing_shipment
ALTER TABLE `outsourcing_shipment`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `outsourcing_shipment` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `outsourcing_shipment`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `outsourcing_shipment`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- outsourcing_shipment_line
ALTER TABLE `outsourcing_shipment_line`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `outsourcing_shipment_line` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `outsourcing_shipment_line`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `outsourcing_shipment_line`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- partner_ledger_account
ALTER TABLE `partner_ledger_account`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `partner_ledger_account` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `partner_ledger_account`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `partner_ledger_account`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- partner_payment
ALTER TABLE `partner_payment`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `partner_payment` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `partner_payment`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `partner_payment`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- partner_payment_line
ALTER TABLE `partner_payment_line`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `partner_payment_line` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `partner_payment_line`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `partner_payment_line`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- partner_prepaid_offset
ALTER TABLE `partner_prepaid_offset`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `partner_prepaid_offset` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `partner_prepaid_offset`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `partner_prepaid_offset`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- process_sequence
ALTER TABLE `process_sequence`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `process_sequence` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `process_sequence`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `process_sequence`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- production_calendar
ALTER TABLE `production_calendar`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `production_calendar` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `production_calendar`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `production_calendar`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- production_plan
ALTER TABLE `production_plan`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `production_plan` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `production_plan`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `production_plan`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- public_code
ALTER TABLE `public_code`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `public_code` t
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0b.id),
  t.`updated_by_id_n` = COALESCE(u1b.id);
ALTER TABLE `public_code`
  DROP COLUMN `created_by`,
  DROP COLUMN `updated_by`;
ALTER TABLE `public_code`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- purchase_history
ALTER TABLE `purchase_history`
  ADD COLUMN `created_by_id_n` BIGINT NULL;
UPDATE `purchase_history` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id);
ALTER TABLE `purchase_history`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`;
ALTER TABLE `purchase_history`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL;

-- purchase_order
ALTER TABLE `purchase_order`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `purchase_order` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `purchase_order`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `purchase_order`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- purchase_order_line
ALTER TABLE `purchase_order_line`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `purchase_order_line` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `purchase_order_line`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `purchase_order_line`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- purchase_receipt
ALTER TABLE `purchase_receipt`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `purchase_receipt` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `purchase_receipt`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `purchase_receipt`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- purchase_receipt_line
ALTER TABLE `purchase_receipt_line`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `purchase_receipt_line` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `purchase_receipt_line`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `purchase_receipt_line`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- quality_inspection
ALTER TABLE `quality_inspection`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `quality_inspection` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `quality_inspection`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `quality_inspection`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- sales_collection
ALTER TABLE `sales_collection`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `sales_collection` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `sales_collection`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `sales_collection`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- sales_history
ALTER TABLE `sales_history`
  ADD COLUMN `created_by_id_n` BIGINT NULL;
UPDATE `sales_history` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id);
ALTER TABLE `sales_history`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`;
ALTER TABLE `sales_history`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL;

-- sales_order
ALTER TABLE `sales_order`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL,
  ADD COLUMN `confirmed_by_id_n` BIGINT NULL;
UPDATE `sales_order` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
  LEFT JOIN `user` u2a ON u2a.login_id = t.`confirmed_by_id` AND u2a.recording_state = 1
  LEFT JOIN `user` u2c ON t.`confirmed_by_id` REGEXP '^[0-9]+$' AND u2c.id = CAST(t.`confirmed_by_id` AS UNSIGNED) AND u2c.recording_state = 1
  LEFT JOIN `user` u2b ON u2b.login_id = t.`confirmed_by` AND u2b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id),
  t.`confirmed_by_id_n` = COALESCE(u2a.id, u2c.id, u2b.id);
ALTER TABLE `sales_order`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`,
  DROP COLUMN `confirmed_by`,
  DROP COLUMN `confirmed_by_id`;
ALTER TABLE `sales_order`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL,
  CHANGE COLUMN `confirmed_by_id_n` `confirmed_by_id` BIGINT NULL;

-- sales_revenue
ALTER TABLE `sales_revenue`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `sales_revenue` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `sales_revenue`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `sales_revenue`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- sales_revenue_line
ALTER TABLE `sales_revenue_line`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `sales_revenue_line` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `sales_revenue_line`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `sales_revenue_line`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- sales_shipment
ALTER TABLE `sales_shipment`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `sales_shipment` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `sales_shipment`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `sales_shipment`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- sales_shipment_line
ALTER TABLE `sales_shipment_line`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `sales_shipment_line` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `sales_shipment_line`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `sales_shipment_line`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- stock_movement
ALTER TABLE `stock_movement`
  ADD COLUMN `created_by_id_n` BIGINT NULL;
UPDATE `stock_movement` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id);
ALTER TABLE `stock_movement`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`;
ALTER TABLE `stock_movement`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL;

-- system_settings
ALTER TABLE `system_settings`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `system_settings` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `system_settings`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `system_settings`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- unit_price
ALTER TABLE `unit_price`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `unit_price` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `unit_price`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `unit_price`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- unit_price_change_log
ALTER TABLE `unit_price_change_log`
  ADD COLUMN `changed_by_id_n` BIGINT NULL;
UPDATE `unit_price_change_log` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`changed_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`changed_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`changed_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`changed_by` AND u0b.recording_state = 1
SET
  t.`changed_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id);
ALTER TABLE `unit_price_change_log`
  DROP COLUMN `changed_by`,
  DROP COLUMN `changed_by_id`;
ALTER TABLE `unit_price_change_log`
  CHANGE COLUMN `changed_by_id_n` `changed_by_id` BIGINT NULL;

-- work_center
ALTER TABLE `work_center`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `work_center` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `work_center`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `work_center`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- work_center_calendar
ALTER TABLE `work_center_calendar`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `work_center_calendar` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `work_center_calendar`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `work_center_calendar`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- work_diary_entry
ALTER TABLE `work_diary_entry`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `work_diary_entry` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `work_diary_entry`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `work_diary_entry`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- work_diary_template
ALTER TABLE `work_diary_template`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `work_diary_template` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `work_diary_template`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `work_diary_template`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- work_order
ALTER TABLE `work_order`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `work_order` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `work_order`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `work_order`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- work_plan
ALTER TABLE `work_plan`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `work_plan` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `work_plan`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `work_plan`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- work_report
ALTER TABLE `work_report`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `work_report` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `work_report`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `work_report`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- work_report_consumption_line
ALTER TABLE `work_report_consumption_line`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `work_report_consumption_line` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `work_report_consumption_line`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `work_report_consumption_line`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- work_report_history
ALTER TABLE `work_report_history`
  ADD COLUMN `created_by_id_n` BIGINT NULL;
UPDATE `work_report_history` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id);
ALTER TABLE `work_report_history`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`;
ALTER TABLE `work_report_history`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL;

-- work_standard
ALTER TABLE `work_standard`
  ADD COLUMN `created_by_id_n` BIGINT NULL,
  ADD COLUMN `updated_by_id_n` BIGINT NULL;
UPDATE `work_standard` t
  LEFT JOIN `user` u0a ON u0a.login_id = t.`created_by_id` AND u0a.recording_state = 1
  LEFT JOIN `user` u0c ON t.`created_by_id` REGEXP '^[0-9]+$' AND u0c.id = CAST(t.`created_by_id` AS UNSIGNED) AND u0c.recording_state = 1
  LEFT JOIN `user` u0b ON u0b.login_id = t.`created_by` AND u0b.recording_state = 1
  LEFT JOIN `user` u1a ON u1a.login_id = t.`updated_by_id` AND u1a.recording_state = 1
  LEFT JOIN `user` u1c ON t.`updated_by_id` REGEXP '^[0-9]+$' AND u1c.id = CAST(t.`updated_by_id` AS UNSIGNED) AND u1c.recording_state = 1
  LEFT JOIN `user` u1b ON u1b.login_id = t.`updated_by` AND u1b.recording_state = 1
SET
  t.`created_by_id_n` = COALESCE(u0a.id, u0c.id, u0b.id),
  t.`updated_by_id_n` = COALESCE(u1a.id, u1c.id, u1b.id);
ALTER TABLE `work_standard`
  DROP COLUMN `created_by`,
  DROP COLUMN `created_by_id`,
  DROP COLUMN `updated_by`,
  DROP COLUMN `updated_by_id`;
ALTER TABLE `work_standard`
  CHANGE COLUMN `created_by_id_n` `created_by_id` BIGINT NULL,
  CHANGE COLUMN `updated_by_id_n` `updated_by_id` BIGINT NULL;

-- FK
ALTER TABLE `user`
  ADD CONSTRAINT `fk_user_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `user`
  ADD CONSTRAINT `fk_user_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `board_post`
  ADD CONSTRAINT `fk_board_post_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `board_post`
  ADD CONSTRAINT `fk_board_post_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `bom_change_log`
  ADD CONSTRAINT `fk_bom_change_log_changed_by_id` FOREIGN KEY (`changed_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `company`
  ADD CONSTRAINT `fk_company_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `company`
  ADD CONSTRAINT `fk_company_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `defect_claim`
  ADD CONSTRAINT `fk_defect_claim_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `defect_claim`
  ADD CONSTRAINT `fk_defect_claim_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `drawing_master`
  ADD CONSTRAINT `fk_drawing_master_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `drawing_master`
  ADD CONSTRAINT `fk_drawing_master_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `drawing_reference`
  ADD CONSTRAINT `fk_drawing_reference_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `drawing_reference`
  ADD CONSTRAINT `fk_drawing_reference_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `equipment`
  ADD CONSTRAINT `fk_equipment_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `equipment`
  ADD CONSTRAINT `fk_equipment_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `etc_claim`
  ADD CONSTRAINT `fk_etc_claim_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `etc_claim`
  ADD CONSTRAINT `fk_etc_claim_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `etc_purchase_order`
  ADD CONSTRAINT `fk_etc_purchase_order_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `etc_purchase_order`
  ADD CONSTRAINT `fk_etc_purchase_order_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `etc_purchase_receipt`
  ADD CONSTRAINT `fk_etc_purchase_receipt_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `etc_purchase_receipt`
  ADD CONSTRAINT `fk_etc_purchase_receipt_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `inventory_balance`
  ADD CONSTRAINT `fk_inventory_balance_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `inventory_balance`
  ADD CONSTRAINT `fk_inventory_balance_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `inventory_lot`
  ADD CONSTRAINT `fk_inventory_lot_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `inventory_lot`
  ADD CONSTRAINT `fk_inventory_lot_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `inventory_lot_balance`
  ADD CONSTRAINT `fk_inventory_lot_balance_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `inventory_lot_balance`
  ADD CONSTRAINT `fk_inventory_lot_balance_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `item`
  ADD CONSTRAINT `fk_item_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `item`
  ADD CONSTRAINT `fk_item_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `item_composition`
  ADD CONSTRAINT `fk_item_composition_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `item_composition`
  ADD CONSTRAINT `fk_item_composition_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `lot_genealogy`
  ADD CONSTRAINT `fk_lot_genealogy_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `material_issue`
  ADD CONSTRAINT `fk_material_issue_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `material_issue`
  ADD CONSTRAINT `fk_material_issue_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `material_requirement_line`
  ADD CONSTRAINT `fk_material_requirement_line_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `material_requirement_line`
  ADD CONSTRAINT `fk_material_requirement_line_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `misc_stock_movement`
  ADD CONSTRAINT `fk_misc_stock_movement_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `misc_stock_movement`
  ADD CONSTRAINT `fk_misc_stock_movement_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `month_closing`
  ADD CONSTRAINT `fk_month_closing_closed_by_id` FOREIGN KEY (`closed_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `mrp_run`
  ADD CONSTRAINT `fk_mrp_run_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `mrp_run`
  ADD CONSTRAINT `fk_mrp_run_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `outsource_history`
  ADD CONSTRAINT `fk_outsource_history_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `outsourcing_order`
  ADD CONSTRAINT `fk_outsourcing_order_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `outsourcing_order`
  ADD CONSTRAINT `fk_outsourcing_order_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `outsourcing_order_line`
  ADD CONSTRAINT `fk_outsourcing_order_line_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `outsourcing_order_line`
  ADD CONSTRAINT `fk_outsourcing_order_line_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `outsourcing_receipt`
  ADD CONSTRAINT `fk_outsourcing_receipt_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `outsourcing_receipt`
  ADD CONSTRAINT `fk_outsourcing_receipt_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `outsourcing_receipt_line`
  ADD CONSTRAINT `fk_outsourcing_receipt_line_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `outsourcing_receipt_line`
  ADD CONSTRAINT `fk_outsourcing_receipt_line_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `outsourcing_shipment`
  ADD CONSTRAINT `fk_outsourcing_shipment_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `outsourcing_shipment`
  ADD CONSTRAINT `fk_outsourcing_shipment_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `outsourcing_shipment_line`
  ADD CONSTRAINT `fk_outsourcing_shipment_line_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `outsourcing_shipment_line`
  ADD CONSTRAINT `fk_outsourcing_shipment_line_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `partner_ledger_account`
  ADD CONSTRAINT `fk_partner_ledger_account_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `partner_ledger_account`
  ADD CONSTRAINT `fk_partner_ledger_account_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `partner_payment`
  ADD CONSTRAINT `fk_partner_payment_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `partner_payment`
  ADD CONSTRAINT `fk_partner_payment_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `partner_payment_line`
  ADD CONSTRAINT `fk_partner_payment_line_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `partner_payment_line`
  ADD CONSTRAINT `fk_partner_payment_line_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `partner_prepaid_offset`
  ADD CONSTRAINT `fk_partner_prepaid_offset_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `partner_prepaid_offset`
  ADD CONSTRAINT `fk_partner_prepaid_offset_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `process_sequence`
  ADD CONSTRAINT `fk_process_sequence_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `process_sequence`
  ADD CONSTRAINT `fk_process_sequence_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `production_calendar`
  ADD CONSTRAINT `fk_production_calendar_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `production_calendar`
  ADD CONSTRAINT `fk_production_calendar_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `production_plan`
  ADD CONSTRAINT `fk_production_plan_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `production_plan`
  ADD CONSTRAINT `fk_production_plan_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `public_code`
  ADD CONSTRAINT `fk_public_code_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `public_code`
  ADD CONSTRAINT `fk_public_code_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `purchase_history`
  ADD CONSTRAINT `fk_purchase_history_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `purchase_order`
  ADD CONSTRAINT `fk_purchase_order_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `purchase_order`
  ADD CONSTRAINT `fk_purchase_order_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `purchase_order_line`
  ADD CONSTRAINT `fk_purchase_order_line_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `purchase_order_line`
  ADD CONSTRAINT `fk_purchase_order_line_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `purchase_receipt`
  ADD CONSTRAINT `fk_purchase_receipt_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `purchase_receipt`
  ADD CONSTRAINT `fk_purchase_receipt_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `purchase_receipt_line`
  ADD CONSTRAINT `fk_purchase_receipt_line_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `purchase_receipt_line`
  ADD CONSTRAINT `fk_purchase_receipt_line_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `quality_inspection`
  ADD CONSTRAINT `fk_quality_inspection_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `quality_inspection`
  ADD CONSTRAINT `fk_quality_inspection_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `sales_collection`
  ADD CONSTRAINT `fk_sales_collection_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `sales_collection`
  ADD CONSTRAINT `fk_sales_collection_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `sales_history`
  ADD CONSTRAINT `fk_sales_history_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `sales_order`
  ADD CONSTRAINT `fk_sales_order_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `sales_order`
  ADD CONSTRAINT `fk_sales_order_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `sales_order`
  ADD CONSTRAINT `fk_sales_order_confirmed_by_id` FOREIGN KEY (`confirmed_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `sales_revenue`
  ADD CONSTRAINT `fk_sales_revenue_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `sales_revenue`
  ADD CONSTRAINT `fk_sales_revenue_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `sales_revenue_line`
  ADD CONSTRAINT `fk_sales_revenue_line_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `sales_revenue_line`
  ADD CONSTRAINT `fk_sales_revenue_line_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `sales_shipment`
  ADD CONSTRAINT `fk_sales_shipment_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `sales_shipment`
  ADD CONSTRAINT `fk_sales_shipment_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `sales_shipment_line`
  ADD CONSTRAINT `fk_sales_shipment_line_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `sales_shipment_line`
  ADD CONSTRAINT `fk_sales_shipment_line_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `stock_movement`
  ADD CONSTRAINT `fk_stock_movement_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `system_settings`
  ADD CONSTRAINT `fk_system_settings_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `system_settings`
  ADD CONSTRAINT `fk_system_settings_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `unit_price`
  ADD CONSTRAINT `fk_unit_price_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `unit_price`
  ADD CONSTRAINT `fk_unit_price_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `unit_price_change_log`
  ADD CONSTRAINT `fk_unit_price_change_log_changed_by_id` FOREIGN KEY (`changed_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `work_center`
  ADD CONSTRAINT `fk_work_center_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `work_center`
  ADD CONSTRAINT `fk_work_center_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `work_center_calendar`
  ADD CONSTRAINT `fk_work_center_calendar_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `work_center_calendar`
  ADD CONSTRAINT `fk_work_center_calendar_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `work_diary_entry`
  ADD CONSTRAINT `fk_work_diary_entry_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `work_diary_entry`
  ADD CONSTRAINT `fk_work_diary_entry_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `work_diary_template`
  ADD CONSTRAINT `fk_work_diary_template_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `work_diary_template`
  ADD CONSTRAINT `fk_work_diary_template_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `work_order`
  ADD CONSTRAINT `fk_work_order_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `work_order`
  ADD CONSTRAINT `fk_work_order_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `work_plan`
  ADD CONSTRAINT `fk_work_plan_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `work_plan`
  ADD CONSTRAINT `fk_work_plan_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `work_report`
  ADD CONSTRAINT `fk_work_report_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `work_report`
  ADD CONSTRAINT `fk_work_report_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `work_report_consumption_line`
  ADD CONSTRAINT `fk_work_report_consumption_line_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `work_report_consumption_line`
  ADD CONSTRAINT `fk_work_report_consumption_line_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `work_report_history`
  ADD CONSTRAINT `fk_work_report_history_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `work_standard`
  ADD CONSTRAINT `fk_work_standard_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);
ALTER TABLE `work_standard`
  ADD CONSTRAINT `fk_work_standard_updated_by_id` FOREIGN KEY (`updated_by_id`) REFERENCES `user` (`id`);
