-- LOT-1: Lot 마스터·슬롯 잔량·채번 + 권한
-- Ref: docs/step0/lot-integration-design.md v1.2 §9 V078

CREATE TABLE IF NOT EXISTS inventory_lot (
  id                  BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  item_id             BIGINT       NOT NULL,
  lot_no              VARCHAR(100) NOT NULL,
  status              ENUM('ACTIVE','BLOCKED','DEPLETED') NOT NULL DEFAULT 'ACTIVE',
  origin_type         ENUM('MANUAL','PURCHASE','PRODUCTION','SPLIT','MERGE','ADJUSTMENT')
                      NOT NULL DEFAULT 'MANUAL',
  origin_doc_type     VARCHAR(50)  NULL COMMENT 'purchase_receipt, work_report, ...',
  origin_doc_id       BIGINT       NULL,
  p1                  VARCHAR(100) NULL COMMENT '레거시 LotLedger.P1',
  p2                  VARCHAR(100) NULL COMMENT '레거시 LotLedger.P2',
  expiry_date         DATE         NULL,
  certificate_ref     VARCHAR(200) NULL,
  remark              VARCHAR(500) NULL,
  recording_state     TINYINT      NOT NULL DEFAULT 1,
  created_by          VARCHAR(100) NULL,
  created_by_id       VARCHAR(100) NULL,
  created_at          DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by          VARCHAR(100) NULL,
  updated_by_id       VARCHAR(100) NULL,
  updated_at          DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_inventory_lot_item_lot (item_id, lot_no, recording_state),
  KEY idx_inventory_lot_status (status, recording_state),
  KEY idx_inventory_lot_item (item_id, recording_state),
  CONSTRAINT fk_inventory_lot_item FOREIGN KEY (item_id) REFERENCES item (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS inventory_lot_balance (
  id                    BIGINT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
  lot_id                BIGINT         NOT NULL,
  inventory_balance_id  BIGINT         NOT NULL,
  qty_on_hand           DECIMAL(18,4)  NOT NULL DEFAULT 0,
  recording_state       TINYINT        NOT NULL DEFAULT 1,
  created_by            VARCHAR(100)   NULL,
  created_by_id         VARCHAR(100)   NULL,
  created_at            DATETIME(3)    NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by            VARCHAR(100)   NULL,
  updated_by_id         VARCHAR(100)   NULL,
  updated_at            DATETIME(3)    NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_lot_balance (lot_id, inventory_balance_id, recording_state),
  KEY idx_lot_balance_inventory (inventory_balance_id, recording_state),
  CONSTRAINT fk_lot_balance_lot FOREIGN KEY (lot_id) REFERENCES inventory_lot (id),
  CONSTRAINT fk_lot_balance_inventory FOREIGN KEY (inventory_balance_id) REFERENCES inventory_balance (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS lot_number_sequence (
  id                    BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  item_id               BIGINT       NOT NULL,
  sequence_date         DATE         NOT NULL,
  last_seq              INT          NOT NULL DEFAULT 0,
  UNIQUE KEY uk_lot_seq (item_id, sequence_date),
  CONSTRAINT fk_lot_number_sequence_item FOREIGN KEY (item_id) REFERENCES item (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO permission (permission_code, description) VALUES
('inventory:lot:read', 'Lot 조회'),
('inventory:lot:write', 'Lot 등록·수정·삭제')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1
  AND p.permission_code IN ('inventory:lot:read', 'inventory:lot:write')
WHERE r.role_code IN ('SYSTEM_ADMIN', 'PURCHASE_OPERATOR', 'PRODUCTION_OPERATOR') AND r.recording_state = 1;

INSERT IGNORE INTO role_permission (role_id, permission_id)
SELECT r.id, p.id
FROM role r
JOIN permission p ON p.recording_state = 1 AND p.permission_code = 'inventory:lot:read'
WHERE r.role_code = 'VIEWER' AND r.recording_state = 1;
