package com.shindong.smartmanager.api.web.system.imports;

import com.shindong.smartmanager.domain.pricing.CostType;
import tools.jackson.core.JsonParser;
import tools.jackson.databind.DeserializationContext;
import tools.jackson.databind.ValueDeserializer;

/** 단가 일괄등록: 영문 enum + 한글 라벨(판매단가/구매단가/외주단가) 수용. */
public class CostTypeImportDeserializer extends ValueDeserializer<CostType> {

    @Override
    public CostType deserialize(JsonParser parser, DeserializationContext context) {
        String raw = parser.getValueAsString();
        try {
            return CostType.fromImportValue(raw);
        } catch (IllegalArgumentException ex) {
            throw context.weirdStringException(raw, CostType.class, ex.getMessage());
        }
    }
}
