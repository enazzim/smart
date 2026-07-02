-- SmartManager Step 1 core (TO-BE) — coexists with legacy tables in kit_erp
-- Ref: docs/step0/d4-public-code.md, d4-company.md, domain-event-projector-matrix.md

USE kit_erp;

CREATE TABLE IF NOT EXISTS domain_event (
  event_id            VARCHAR(36)  NOT NULL PRIMARY KEY,
  event_type          VARCHAR(64)  NOT NULL,
  schema_version      INT          NOT NULL DEFAULT 1,
  aggregate_type      VARCHAR(64)  NOT NULL,
  aggregate_id        VARCHAR(64)  NOT NULL,
  occurred_at         DATETIME(3)  NOT NULL,
  actor_user_id       VARCHAR(64)  NULL,
  payload_json        JSON         NOT NULL,
  INDEX idx_domain_event_type_time (event_type, occurred_at),
  INDEX idx_domain_event_aggregate (aggregate_type, aggregate_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS code_group (
  code_group_key   VARCHAR(64)  NOT NULL PRIMARY KEY,
  large_code       CHAR(4)      NOT NULL,
  large_name       VARCHAR(100) NOT NULL,
  usage_type       VARCHAR(20)  NOT NULL,
  editable_level   ENUM('SYSTEM','SMALL_ONLY','FULL') NOT NULL DEFAULT 'SMALL_ONLY',
  exclude_codes    JSON         NULL,
  description      VARCHAR(255) NULL,
  UNIQUE KEY uk_code_group_large_code (large_code)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS public_code (
  id               BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  large_code       VARCHAR(20)  NOT NULL,
  large_name       VARCHAR(100) NOT NULL,
  small_code       VARCHAR(20)  NULL,
  small_name       VARCHAR(100) NULL,
  usage_type       VARCHAR(20)  NOT NULL,
  recording_state  TINYINT      NOT NULL DEFAULT 1,
  created_by       VARCHAR(100) NULL,
  created_at       DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by       VARCHAR(100) NULL,
  updated_at       DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  INDEX idx_public_code_large (large_code, recording_state),
  INDEX idx_public_code_usage (usage_type, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS company (
  id                    BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  company_name          VARCHAR(200) NOT NULL,
  president_name        VARCHAR(100) NOT NULL,
  business_reg_no       VARCHAR(20)  NOT NULL,
  corporation_reg_no    VARCHAR(20)  NULL,
  business_address      VARCHAR(500) NOT NULL,
  homepage_url          VARCHAR(500) NULL,
  business_type         VARCHAR(100) NULL,
  business_item         VARCHAR(100) NULL,
  telephone             VARCHAR(50)  NULL,
  fax                   VARCHAR(50)  NULL,
  sale_standard_day     TINYINT      NULL,
  bill_approval_standard TINYINT     NULL,
  fix_collect_day_1     TINYINT      NULL,
  contact_name          VARCHAR(100) NULL,
  contact_email         VARCHAR(200) NULL,
  recording_state       TINYINT      NOT NULL DEFAULT 1,
  created_by            VARCHAR(100) NULL,
  created_by_id         VARCHAR(100) NULL,
  created_at            DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by            VARCHAR(100) NULL,
  updated_by_id         VARCHAR(100) NULL,
  updated_at            DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_company_business_reg_no (business_reg_no, recording_state)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS company_role (
  id          BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  company_id  BIGINT NOT NULL,
  role_type   ENUM('SALES','OUTSOURCE','PURCHASE','COST') NOT NULL,
  UNIQUE KEY uk_company_role (company_id, role_type),
  CONSTRAINT fk_company_role_company FOREIGN KEY (company_id) REFERENCES company(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS partner_ledger_account (
  id                    BIGINT        NOT NULL AUTO_INCREMENT PRIMARY KEY,
  company_id            BIGINT        NOT NULL,
  fiscal_year           SMALLINT      NOT NULL,
  ledger_type           ENUM('SALES','PURCHASE') NOT NULL,
  prior_sale_carryover  DECIMAL(18,2) NOT NULL DEFAULT 0,
  prior_buy_carryover   DECIMAL(18,2) NOT NULL DEFAULT 0,
  recording_state       TINYINT       NOT NULL DEFAULT 1,
  created_by            VARCHAR(100)  NULL,
  created_by_id         VARCHAR(100)  NULL,
  created_at            DATETIME(3)   NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by            VARCHAR(100)  NULL,
  updated_by_id         VARCHAR(100)  NULL,
  updated_at            DATETIME(3)   NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_partner_ledger (company_id, fiscal_year, ledger_type),
  CONSTRAINT fk_partner_ledger_company FOREIGN KEY (company_id) REFERENCES company(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS partner_ledger_monthly (
  id                  BIGINT        NOT NULL AUTO_INCREMENT PRIMARY KEY,
  ledger_account_id   BIGINT        NOT NULL,
  month_num           TINYINT       NOT NULL,
  sale_amount         DECIMAL(18,2) NOT NULL DEFAULT 0,
  purchase_amount     DECIMAL(18,2) NOT NULL DEFAULT 0,
  recording_state     TINYINT       NOT NULL DEFAULT 1,
  created_at          DATETIME(3)   NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_at          DATETIME(3)   NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_partner_ledger_monthly (ledger_account_id, month_num),
  CONSTRAINT fk_partner_ledger_monthly_account FOREIGN KEY (ledger_account_id) REFERENCES partner_ledger_account(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
