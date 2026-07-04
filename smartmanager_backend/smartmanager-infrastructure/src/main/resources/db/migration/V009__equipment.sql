-- Ref: docs/step0/d4-equipment.md v0.1

INSERT INTO public_code (large_code, large_name, small_code, small_name, usage_type, created_by) VALUES
('0720', '설비분류', '07200030', '사출기', 'GENERIC', 'seed'),
('0720', '설비분류', '07200040', '프레스', 'GENERIC', 'seed'),
('0720', '설비분류', '07200050', 'CNC', 'GENERIC', 'seed')
ON DUPLICATE KEY UPDATE small_name = VALUES(small_name);

CREATE TABLE IF NOT EXISTS equipment (
  id                      BIGINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
  equipment_num           VARCHAR(50)  NOT NULL,
  equipment_name          VARCHAR(200) NOT NULL,
  equipment_category_id   BIGINT       NOT NULL,
  work_center_id          BIGINT       NULL,
  design_shot             INT          NOT NULL DEFAULT 0,
  initial_shot            INT          NOT NULL DEFAULT 0,
  work_shot               INT          NOT NULL DEFAULT 0,
  accumulated_shot        INT          NOT NULL DEFAULT 0,
  recording_state         TINYINT      NOT NULL DEFAULT 1,
  created_by              VARCHAR(100) NULL,
  created_by_id           VARCHAR(100) NULL,
  created_at              DATETIME(3)  NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  updated_by              VARCHAR(100) NULL,
  updated_by_id             VARCHAR(100) NULL,
  updated_at              DATETIME(3)  NULL ON UPDATE CURRENT_TIMESTAMP(3),
  UNIQUE KEY uk_equipment_num (equipment_num, recording_state),
  CONSTRAINT fk_equipment_category FOREIGN KEY (equipment_category_id) REFERENCES public_code (id),
  CONSTRAINT fk_equipment_work_center FOREIGN KEY (work_center_id) REFERENCES work_center (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

ALTER TABLE work_standard
  ADD CONSTRAINT fk_work_standard_equipment FOREIGN KEY (equipment_id) REFERENCES equipment (id);
