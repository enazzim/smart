package com.shindong.smartmanager.application.workdiary;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import java.util.Set;

/** 작성자 양식 — 레거시 필드 06(지시사항)은 결재자 전용 directive_note 로 분리한다. */
public final class WorkDiaryFieldSchemaSupport {

    public static final String DIRECTIVE_LEGACY_FIELD_KEY = "06";
    public static final int SCHEMA_VERSION = 2;

    private WorkDiaryFieldSchemaSupport() {
    }

    public static Map<String, Object> forWriter(Map<String, Object> fieldSchema) {
        List<WorkDiaryFieldDefinition> fields = parseFields(fieldSchema).stream()
                .filter(field -> !isDirectiveWriterField(field.key(), field.label()))
                .toList();
        if (fields.isEmpty()) {
            return Map.of();
        }
        return Map.of(
                "version", SCHEMA_VERSION,
                "fields", fields.stream().map(WorkDiaryFieldDefinition::toMap).toList()
        );
    }

    @SuppressWarnings("unchecked")
    public static List<WorkDiaryFieldDefinition> parseFields(Map<String, Object> fieldSchema) {
        if (fieldSchema == null || fieldSchema.isEmpty()) {
            return List.of();
        }
        Object fieldsObj = fieldSchema.get("fields");
        if (fieldsObj instanceof List<?> fieldsList && !fieldsList.isEmpty()) {
            List<WorkDiaryFieldDefinition> parsed = new ArrayList<>();
            for (Object item : fieldsList) {
                if (item instanceof Map<?, ?> map) {
                    parsed.add(WorkDiaryFieldDefinition.fromMap((Map<String, Object>) map));
                }
            }
            return sortFields(parsed);
        }
        Object legacyFieldsObj = fieldSchema.get("legacyFields");
        if (!(legacyFieldsObj instanceof Map<?, ?> legacyFields)) {
            return List.of();
        }
        List<WorkDiaryFieldDefinition> parsed = new ArrayList<>();
        legacyFields.forEach((key, value) -> {
            if (key == null || value == null) {
                return;
            }
            String keyStr = String.valueOf(key).trim();
            String label = String.valueOf(value).trim();
            if (keyStr.isEmpty() || label.isEmpty()) {
                return;
            }
            parsed.add(new WorkDiaryFieldDefinition(
                    keyStr,
                    label,
                    WorkDiaryFieldType.TEXTAREA,
                    List.of(),
                    List.of()
            ));
        });
        return sortFields(parsed);
    }

    public static Map<String, Object> buildFieldSchema(List<WorkDiaryFieldDefinition> fields) {
        if (fields == null || fields.isEmpty()) {
            throw new IllegalArgumentException("입력 항목(fields)이 비어 있습니다.");
        }
        List<WorkDiaryFieldDefinition> cleaned = new ArrayList<>();
        Set<String> keys = new HashSet<>();
        Set<String> checklistItemIds = new HashSet<>();
        int index = 0;
        for (WorkDiaryFieldDefinition field : fields) {
            if (isDirectiveWriterField(field.key(), field.label())) {
                continue;
            }
            String key = field.key().trim();
            String label = field.label().trim();
            if (key.isEmpty() || label.isEmpty()) {
                throw new IllegalArgumentException("필드 키와 항목명은 필수입니다.");
            }
            if (!keys.add(key)) {
                throw new IllegalArgumentException("중복된 필드 키입니다: " + key);
            }
            WorkDiaryFieldType type = field.type() != null ? field.type() : WorkDiaryFieldType.TEXTAREA;
            List<String> options = normalizeOptions(field.options());
            List<WorkDiaryChecklistItemDefinition> items = new ArrayList<>();
            if (type == WorkDiaryFieldType.CHECKLIST) {
                int sortOrder = 1;
                for (WorkDiaryChecklistItemDefinition item : field.items()) {
                    String itemId = item.id().trim();
                    String text = item.text().trim();
                    if (itemId.isEmpty() || text.isEmpty()) {
                        continue;
                    }
                    if (!checklistItemIds.add(itemId)) {
                        throw new IllegalArgumentException("중복된 체크리스트 항목 ID입니다: " + itemId);
                    }
                    int resolvedSortOrder = item.sortOrder() > 0 ? item.sortOrder() : sortOrder;
                    items.add(new WorkDiaryChecklistItemDefinition(
                            itemId,
                            item.group(),
                            text,
                            resolvedSortOrder
                    ));
                    sortOrder++;
                }
            }
            cleaned.add(new WorkDiaryFieldDefinition(key, label, type, options, items));
            index++;
        }
        if (cleaned.isEmpty()) {
            throw new IllegalArgumentException("입력 항목(fields)이 비어 있습니다.");
        }
        return Map.of(
                "version", SCHEMA_VERSION,
                "fields", cleaned.stream().map(WorkDiaryFieldDefinition::toMap).toList()
        );
    }

    public static Map<String, Object> normalizeFieldValues(
            Map<String, Object> fieldValues,
            Map<String, Object> fieldSchema
    ) {
        if (fieldValues == null) {
            throw new IllegalArgumentException("입력값(fieldValues)은 필수입니다.");
        }
        List<WorkDiaryFieldDefinition> fields = parseFields(fieldSchema).stream()
                .filter(field -> !isDirectiveWriterField(field.key(), field.label()))
                .toList();
        Map<String, Object> normalized = new LinkedHashMap<>();
        for (WorkDiaryFieldDefinition field : fields) {
            Object rawValue = fieldValues.get(field.key());
            if (field.type() == WorkDiaryFieldType.CHECKLIST) {
                normalized.put(field.key(), normalizeChecklistValue(rawValue, field));
            } else {
                normalized.put(field.key(), rawValue != null ? String.valueOf(rawValue) : "");
            }
        }
        return normalized;
    }

    @SuppressWarnings("unchecked")
    private static Map<String, Object> normalizeChecklistValue(Object rawValue, WorkDiaryFieldDefinition field) {
        Map<String, Object> normalized = new LinkedHashMap<>();
        if (!(rawValue instanceof Map<?, ?> rawMap)) {
            for (WorkDiaryChecklistItemDefinition item : field.items()) {
                normalized.put(item.id(), defaultChecklistEntry(field));
            }
            return normalized;
        }
        Set<String> allowedOptions = new HashSet<>(normalizeOptions(field.options()));
        for (WorkDiaryChecklistItemDefinition item : field.items()) {
            Object entryObj = rawMap.get(item.id());
            Map<String, Object> entry = new LinkedHashMap<>();
            if (entryObj instanceof Map<?, ?> entryMap) {
                String status = entryMap.get("status") != null ? String.valueOf(entryMap.get("status")).trim() : "";
                if (status.isEmpty() || !allowedOptions.contains(status)) {
                    status = allowedOptions.iterator().next();
                }
                String note = entryMap.get("note") != null ? String.valueOf(entryMap.get("note")).trim() : "";
                entry.put("status", status);
                if (!note.isEmpty()) {
                    entry.put("note", note);
                }
            } else {
                entry.putAll(defaultChecklistEntry(field));
            }
            normalized.put(item.id(), entry);
        }
        rawMap.forEach((itemId, entryObj) -> {
            String id = String.valueOf(itemId);
            if (normalized.containsKey(id)) {
                return;
            }
            if (entryObj instanceof Map<?, ?> entryMap) {
                Map<String, Object> entry = new LinkedHashMap<>();
                String status = entryMap.get("status") != null ? String.valueOf(entryMap.get("status")).trim() : "";
                if (!status.isEmpty()) {
                    entry.put("status", status);
                }
                String note = entryMap.get("note") != null ? String.valueOf(entryMap.get("note")).trim() : "";
                if (!note.isEmpty()) {
                    entry.put("note", note);
                }
                if (!entry.isEmpty()) {
                    normalized.put(id, entry);
                }
            }
        });
        return normalized;
    }

    private static Map<String, Object> defaultChecklistEntry(WorkDiaryFieldDefinition field) {
        List<String> options = normalizeOptions(field.options());
        return Map.of("status", options.get(0));
    }

    private static List<String> normalizeOptions(List<String> options) {
        List<String> normalized = new ArrayList<>();
        if (options != null) {
            for (String option : options) {
                if (option == null) {
                    continue;
                }
                String trimmed = option.trim();
                if (!trimmed.isEmpty() && !normalized.contains(trimmed)) {
                    normalized.add(trimmed);
                }
            }
        }
        if (normalized.isEmpty()) {
            return new ArrayList<>(WorkDiaryFieldDefinition.DEFAULT_CHECKLIST_OPTIONS);
        }
        return normalized;
    }

    private static List<WorkDiaryFieldDefinition> sortFields(List<WorkDiaryFieldDefinition> fields) {
        return fields.stream()
                .sorted(Comparator.comparing(WorkDiaryFieldDefinition::key, Comparator.nullsLast(String::compareTo)))
                .toList();
    }

    public static boolean isDirectiveWriterField(String key, String label) {
        return DIRECTIVE_LEGACY_FIELD_KEY.equals(key) || (label != null && label.contains("지시사항"));
    }

    /** @deprecated 레거시 API 호환 — {@link #buildFieldSchema(List)} 사용 */
    @Deprecated
    public static Map<String, Object> buildFieldSchema(Map<String, String> legacyFields) {
        if (legacyFields == null || legacyFields.isEmpty()) {
            throw new IllegalArgumentException("입력 항목(legacyFields)이 비어 있습니다.");
        }
        List<WorkDiaryFieldDefinition> fields = legacyFields.entrySet().stream()
                .filter(entry -> entry.getKey() != null && entry.getValue() != null)
                .map(entry -> new WorkDiaryFieldDefinition(
                        entry.getKey().trim(),
                        entry.getValue().trim(),
                        WorkDiaryFieldType.TEXTAREA,
                        List.of(),
                        List.of()
                ))
                .filter(field -> !field.key().isEmpty() && !field.label().isEmpty())
                .filter(field -> !isDirectiveWriterField(field.key(), field.label()))
                .toList();
        return buildFieldSchema(fields);
    }
}
