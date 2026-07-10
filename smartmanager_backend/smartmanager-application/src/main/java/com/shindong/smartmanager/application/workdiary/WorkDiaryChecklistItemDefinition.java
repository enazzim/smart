package com.shindong.smartmanager.application.workdiary;

import java.util.LinkedHashMap;
import java.util.Map;

public record WorkDiaryChecklistItemDefinition(
        String id,
        String group,
        String text,
        int sortOrder
) {

    public WorkDiaryChecklistItemDefinition {
        id = id != null ? id.trim() : "";
        group = group != null ? group.trim() : "";
        text = text != null ? text.trim() : "";
    }

    public Map<String, Object> toMap() {
        Map<String, Object> map = new LinkedHashMap<>();
        map.put("id", id);
        if (!group.isEmpty()) {
            map.put("group", group);
        }
        map.put("text", text);
        map.put("sortOrder", sortOrder);
        return map;
    }

    @SuppressWarnings("unchecked")
    public static WorkDiaryChecklistItemDefinition fromMap(Map<String, Object> map) {
        if (map == null) {
            throw new IllegalArgumentException("체크리스트 항목이 비어 있습니다.");
        }
        String id = map.get("id") != null ? String.valueOf(map.get("id")) : "";
        String group = map.get("group") != null ? String.valueOf(map.get("group")) : "";
        String text = map.get("text") != null ? String.valueOf(map.get("text")) : "";
        int sortOrder = 0;
        Object sortOrderObj = map.get("sortOrder");
        if (sortOrderObj instanceof Number number) {
            sortOrder = number.intValue();
        } else if (sortOrderObj != null) {
            try {
                sortOrder = Integer.parseInt(String.valueOf(sortOrderObj));
            } catch (NumberFormatException ignored) {
                sortOrder = 0;
            }
        }
        return new WorkDiaryChecklistItemDefinition(id, group, text, sortOrder);
    }
}
