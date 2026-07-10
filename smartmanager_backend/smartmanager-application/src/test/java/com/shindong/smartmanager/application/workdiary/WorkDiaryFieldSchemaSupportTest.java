package com.shindong.smartmanager.application.workdiary;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.junit.jupiter.api.Assertions.assertTrue;

import java.util.List;
import java.util.Map;
import org.junit.jupiter.api.Test;

class WorkDiaryFieldSchemaSupportTest {

    @Test
    void buildFieldSchema_createsChecklistField() {
        Map<String, Object> schema = WorkDiaryFieldSchemaSupport.buildFieldSchema(List.of(
                new WorkDiaryFieldDefinition(
                        "04",
                        "4. 일일 불량 Check List",
                        WorkDiaryFieldType.CHECKLIST,
                        List.of("이상무", "이상있음"),
                        List.of(new WorkDiaryChecklistItemDefinition("chk-1", "전기", "분전반 점검", 1))
                )
        ));

        List<WorkDiaryFieldDefinition> fields = WorkDiaryFieldSchemaSupport.parseFields(schema);
        assertEquals(1, fields.size());
        assertEquals(WorkDiaryFieldType.CHECKLIST, fields.get(0).type());
        assertEquals(1, fields.get(0).items().size());
    }

    @Test
    void parseFields_convertsLegacyFields() {
        Map<String, Object> legacy = Map.of(
                "legacyFields", Map.of(
                        "01", "1. 업무현황",
                        "06", "4. 지시사항"
                )
        );

        List<WorkDiaryFieldDefinition> fields = WorkDiaryFieldSchemaSupport.parseFields(legacy);
        assertEquals(2, fields.size());
        assertEquals(WorkDiaryFieldType.TEXTAREA, fields.get(0).type());
    }

    @Test
    void forWriter_excludesDirectiveField() {
        Map<String, Object> legacy = Map.of(
                "legacyFields", Map.of(
                        "01", "1. 업무",
                        "06", "4. 지시사항"
                )
        );

        List<WorkDiaryFieldDefinition> writerFields = WorkDiaryFieldSchemaSupport.parseFields(
                WorkDiaryFieldSchemaSupport.forWriter(legacy)
        );
        assertEquals(1, writerFields.size());
        assertEquals("01", writerFields.get(0).key());
    }

    @Test
    void normalizeFieldValues_normalizesChecklistEntries() {
        Map<String, Object> schema = WorkDiaryFieldSchemaSupport.buildFieldSchema(List.of(
                new WorkDiaryFieldDefinition(
                        "02",
                        "2. 일일 체크LIST",
                        WorkDiaryFieldType.CHECKLIST,
                        List.of("이상무", "이상있음"),
                        List.of(
                                new WorkDiaryChecklistItemDefinition("chk-1", "전기", "분전반", 1),
                                new WorkDiaryChecklistItemDefinition("chk-2", "콤프레셔", "소음", 2)
                        )
                )
        ));

        Map<String, Object> normalized = WorkDiaryFieldSchemaSupport.normalizeFieldValues(
                Map.of("02", Map.of("chk-1", Map.of("status", "이상있음", "note", "트립"))),
                schema
        );

        @SuppressWarnings("unchecked")
        Map<String, Object> checklist = (Map<String, Object>) normalized.get("02");
        assertTrue(checklist.containsKey("chk-1"));
        assertTrue(checklist.containsKey("chk-2"));
        @SuppressWarnings("unchecked")
        Map<String, Object> first = (Map<String, Object>) checklist.get("chk-1");
        assertEquals("이상있음", first.get("status"));
        assertEquals("트립", first.get("note"));
    }

    @Test
    void buildFieldSchema_rejectsDuplicateChecklistItemId() {
        IllegalArgumentException ex = assertThrows(IllegalArgumentException.class, () ->
                WorkDiaryFieldSchemaSupport.buildFieldSchema(List.of(
                        new WorkDiaryFieldDefinition(
                                "02",
                                "체크",
                                WorkDiaryFieldType.CHECKLIST,
                                List.of(),
                                List.of(
                                        new WorkDiaryChecklistItemDefinition("dup", "", "A", 1),
                                        new WorkDiaryChecklistItemDefinition("dup", "", "B", 2)
                                )
                        )
                )));
        assertTrue(ex.getMessage().contains("중복된 체크리스트 항목 ID"));
    }
}
