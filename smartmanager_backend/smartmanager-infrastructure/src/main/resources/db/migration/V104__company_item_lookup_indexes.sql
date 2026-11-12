-- 거래처·품목 검색(lookup)용 보조 인덱스
CREATE INDEX idx_company_name_state ON company (company_name, recording_state);
CREATE INDEX idx_company_role_type_company ON company_role (role_type, company_id);
CREATE INDEX idx_item_class_state ON item (property_classification, recording_state);
