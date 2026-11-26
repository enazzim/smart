package com.shindong.smartmanager.infrastructure.persistence.menu;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataWorkDiaryGroupMenuRepository
        extends JpaRepository<WorkDiaryGroupMenuJpaEntity, WorkDiaryGroupMenuJpaEntity.Key> {

    List<WorkDiaryGroupMenuJpaEntity> findByWorkDiaryGroupId(long workDiaryGroupId);

    void deleteByWorkDiaryGroupId(long workDiaryGroupId);
}
