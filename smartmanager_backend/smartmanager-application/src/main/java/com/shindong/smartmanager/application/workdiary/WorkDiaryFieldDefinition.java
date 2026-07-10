package com.shindong.smartmanager.application.workdiary;

import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;

public record WorkDiaryFieldDefinition(
        String key,
        String label,
        WorkDiaryFieldType type,
        List<String> options,
        List<WorkDiaryChecklistItemDefinition> items
) {

    public static final List<String> DEFAULT_CHECKLIST_OPTIONS = List.of("이상무", "이상있음");

    public WorkDiaryFieldDefinition {
        key = key != null ? key.trim() : "";
        label = label != null ? label.trim() : "";
        type = type != null ? type : WorkDiaryFieldType.TEXTAREA;
        options = options != null ? List.copyOf(options) : List.of();
        items = items != null ? List.copyOf(items) : List.of();
    }

    public Map<String, Object> toMap() {
        Map<String, Object> map = new LinkedHashMap<>();
        map.put("key", key);
        map.put("label", label);
        map.put("type", type.wireValue());
        if (type == WorkDiaryFieldType.CHECKLIST) {
            map.put("options", options.isEmpty() ? DEFAULT_CHECKLIST_OPTIONS : options);
            map.put("items", items.stream().map(WorkDiaryChecklistItemDefinition::toMap).toList());
        }
        return map;
    }

    @SuppressWarnings("unchecked")
    public static WorkDiaryFieldDefinition fromMap(Map<String, Object> map) {
        if (map == null) {
            throw new IllegalArgumentException("필드 정의가 비어 있습니다.");
        }
        String key = map.get("key") != null ? String.valueOf(map.get("key")) : "";
        String label = map.get("label") != null ? String.valueOf(map.get("label")) : "";
        WorkDiaryFieldType type = WorkDiaryFieldType.from(map.get("type") != null ? String.valueOf(map.get("type")) : null);
        List<String> options = new ArrayList<>();
        Object optionsObj = map.get("options");
        if (optionsObj instanceof List<?> list) {
            for (Object item : list) {
                if (item != null) {
                    String option = String.valueOf(item).trim();
                    if (!option.isEmpty()) {
                        options.add(option);
                    }
                }
            }
        }
        List<WorkDiaryChecklistItemDefinition> items = new ArrayList<>();
        Object itemsObj = map.get("items");
        if (itemsObj instanceof List<?> list) {
            for (Object item : list) {
                if (item instanceof Map<?, ?> itemMap) {
                    items.add(WorkDiaryChecklistItemDefinition.fromMap((Map<String, Object>) itemMap));
                }
            }
        }
        return new WorkDiaryFieldDefinition(key, label, type, options, items);
    }
}
