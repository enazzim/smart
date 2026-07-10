package com.shindong.smartmanager.application.workdiary;

public enum WorkDiaryFieldType {
    TEXTAREA,
    CHECKLIST;

    public static WorkDiaryFieldType from(String value) {
        if (value != null && "checklist".equalsIgnoreCase(value.trim())) {
            return CHECKLIST;
        }
        return TEXTAREA;
    }

    public String wireValue() {
        return this == CHECKLIST ? "checklist" : "textarea";
    }
}
