package com.shindong.smartmanager.infrastructure.persistence.menu;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.IdClass;
import jakarta.persistence.Table;
import java.io.Serializable;
import java.util.Objects;

@Entity
@Table(name = "work_diary_group_menu")
@IdClass(WorkDiaryGroupMenuJpaEntity.Key.class)
public class WorkDiaryGroupMenuJpaEntity {

    @Id
    @Column(name = "work_diary_group_id", nullable = false)
    private long workDiaryGroupId;

    @Id
    @Column(name = "menu_id", nullable = false, length = 64)
    private String menuId;

    protected WorkDiaryGroupMenuJpaEntity() {
    }

    public WorkDiaryGroupMenuJpaEntity(long workDiaryGroupId, String menuId) {
        this.workDiaryGroupId = workDiaryGroupId;
        this.menuId = menuId;
    }

    public String getMenuId() {
        return menuId;
    }

    public static class Key implements Serializable {
        private long workDiaryGroupId;
        private String menuId;

        public Key() {
        }

        @Override
        public boolean equals(Object other) {
            if (this == other) {
                return true;
            }
            if (!(other instanceof Key key)) {
                return false;
            }
            return workDiaryGroupId == key.workDiaryGroupId && Objects.equals(menuId, key.menuId);
        }

        @Override
        public int hashCode() {
            return Objects.hash(workDiaryGroupId, menuId);
        }
    }
}
