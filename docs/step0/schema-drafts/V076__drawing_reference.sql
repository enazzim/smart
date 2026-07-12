-- =============================================================================
-- DRAFT — NOT APPLIED BY FLYWAY
-- Ref: docs/step0/drawing-reference-design.md v1.0
-- Apply as next free version (e.g. V076+) after V072–V075 drawing migrations.
-- Do NOT reuse V070–V075 (viewer perms / fiscal / drawing already used).
-- =============================================================================

CREATE TABLE IF NOT EXISTS drawing_reference (
  id                      VARCHAR(36)  NOT NULL PRIMARY KEY,
  parent_history_id       VARCHAR(36)  NOT NULL COMMENT '조립(상위) 도면 history',
  child_history_id        VARCHAR(36)  NOT NULL COMMENT '부품(하위) 도면 history — Rev pin',
  ref_role                VARCHAR(30)  NOT NULL DEFAULT 'COMPONENT'
                          COMMENT 'COMPONENT | RELATED | SPEC',
  sort_order              INT          NOT NULL DEFAULT 0,
  remark                  VARCHAR(500) NULL,
  recording_state         TINYINT      NOT NULL DEFAULT 1,
  created_by              VARCHAR(100) NULL,
  created_by_id           VARCHAR(100) NULL,
  created_at              DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by              VARCHAR(100) NULL,
  updated_by_id           VARCHAR(100) NULL,
  updated_at              DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_drawing_ref_parent_child
    (parent_history_id, child_history_id, recording_state),
  KEY idx_drawing_ref_child (child_history_id, recording_state),
  CONSTRAINT fk_drawing_ref_parent
    FOREIGN KEY (parent_history_id) REFERENCES drawing_history (id),
  CONSTRAINT fk_drawing_ref_child
    FOREIGN KEY (child_history_id) REFERENCES drawing_history (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
