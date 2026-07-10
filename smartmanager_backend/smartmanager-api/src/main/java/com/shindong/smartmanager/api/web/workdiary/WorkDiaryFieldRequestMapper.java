package com.shindong.smartmanager.api.web.workdiary;

import com.shindong.smartmanager.application.workdiary.WorkDiaryChecklistItemDefinition;
import com.shindong.smartmanager.application.workdiary.WorkDiaryFieldDefinition;
import com.shindong.smartmanager.application.workdiary.WorkDiaryFieldType;
import java.util.ArrayList;
import java.util.List;

final class WorkDiaryFieldRequestMapper {

    private WorkDiaryFieldRequestMapper() {
    }

    static List<WorkDiaryFieldDefinition> toDefinitions(List<WorkDiaryFieldRequest> fields) {
        if (fields == null || fields.isEmpty()) {
            return List.of();
        }
        List<WorkDiaryFieldDefinition> definitions = new ArrayList<>();
        for (WorkDiaryFieldRequest field : fields) {
            WorkDiaryFieldType type = WorkDiaryFieldType.from(field.type());
            List<WorkDiaryChecklistItemDefinition> items = new ArrayList<>();
            if (field.items() != null) {
                for (WorkDiaryChecklistItemRequest item : field.items()) {
                    items.add(new WorkDiaryChecklistItemDefinition(
                            item.id(),
                            item.group(),
                            item.text(),
                            item.sortOrder() != null ? item.sortOrder() : 0
                    ));
                }
            }
            definitions.add(new WorkDiaryFieldDefinition(
                    field.key(),
                    field.label(),
                    type,
                    field.options() != null ? field.options() : List.of(),
                    items
            ));
        }
        return definitions;
    }
}
