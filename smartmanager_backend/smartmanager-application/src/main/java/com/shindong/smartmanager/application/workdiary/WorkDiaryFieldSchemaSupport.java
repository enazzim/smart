package com.shindong.smartmanager.application.workdiary;

import java.util.LinkedHashMap;
import java.util.Map;

/** 작성자 양식 — 레거시 필드 06(지시사항)은 결재자 전용 directive_note 로 분리한다. */
public final class WorkDiaryFieldSchemaSupport {

    public static final String DIRECTIVE_LEGACY_FIELD_KEY = "06";

    private WorkDiaryFieldSchemaSupport() {
    }

    @SuppressWarnings("unchecked")
    public static Map<String, Object> forWriter(Map<String, Object> fieldSchema) {
        if (fieldSchema == null || fieldSchema.isEmpty()) {
            return Map.of();
        }
        Object legacyFieldsObj = fieldSchema.get("legacyFields");
        if (!(legacyFieldsObj instanceof Map<?, ?> legacyFields)) {
            return fieldSchema;
        }
        Map<String, String> filtered = new LinkedHashMap<>();
        legacyFields.forEach((key, value) -> {
            if (key == null || value == null) {
                return;
            }
            String keyStr = String.valueOf(key);
            String label = String.valueOf(value).trim();
            if (label.isEmpty()) {
                return;
            }
            if (isDirectiveWriterField(keyStr, label)) {
                return;
            }
            filtered.put(keyStr, label);
        });
        return Map.of("legacyFields", filtered);
    }

    public static boolean isDirectiveWriterField(String key, String label) {
        return DIRECTIVE_LEGACY_FIELD_KEY.equals(key) || label.contains("지시사항");
    }

    public static Map<String, Object> buildFieldSchema(Map<String, String> legacyFields) {
        Map<String, String> cleaned = new LinkedHashMap<>();
        if (legacyFields != null) {
            legacyFields.forEach((key, value) -> {
                if (key == null || value == null) {
                    return;
                }
                String keyStr = key.trim();
                String label = value.trim();
                if (keyStr.isEmpty() || label.isEmpty()) {
                    return;
                }
                if (isDirectiveWriterField(keyStr, label)) {
                    return;
                }
                cleaned.put(keyStr, label);
            });
        }
        if (cleaned.isEmpty()) {
            throw new IllegalArgumentException("입력 항목(legacyFields)이 비어 있습니다.");
        }
        return Map.of("legacyFields", cleaned);
    }
}
