-- 기타공제 승인·매입년월 연동

ALTER TABLE etc_claim
  ADD COLUMN fiscal_year SMALLINT NULL AFTER amount,
  ADD COLUMN fiscal_month TINYINT NULL AFTER fiscal_year,
  ADD COLUMN approved_at DATETIME(3) NULL AFTER recognition,
  ADD COLUMN approved_by_user_id BIGINT NULL AFTER approved_at,
  ADD COLUMN approval_cancelled_at DATETIME(3) NULL AFTER approved_by_user_id,
  ADD COLUMN approval_cancelled_by_user_id BIGINT NULL AFTER approval_cancelled_at;

UPDATE etc_claim
SET fiscal_year = YEAR(receipt_date),
    fiscal_month = MONTH(receipt_date);

ALTER TABLE etc_claim
  MODIFY COLUMN fiscal_year SMALLINT NOT NULL,
  MODIFY COLUMN fiscal_month TINYINT NOT NULL;

CREATE INDEX idx_etc_claim_recognition_fiscal
  ON etc_claim (recognition, fiscal_year, fiscal_month, recording_state);
