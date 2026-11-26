package com.shindong.smartmanager.infrastructure.persistence.menu;

import com.shindong.smartmanager.application.menu.WorkDiaryGroupMenuRepository;
import java.util.List;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaWorkDiaryGroupMenuRepository implements WorkDiaryGroupMenuRepository {

    private final SpringDataWorkDiaryGroupMenuRepository repository;

    public JpaWorkDiaryGroupMenuRepository(SpringDataWorkDiaryGroupMenuRepository repository) {
        this.repository = repository;
    }

    @Override
    @Transactional(readOnly = true)
    public List<String> findMenuIds(long workDiaryGroupId) {
        return repository.findByWorkDiaryGroupId(workDiaryGroupId).stream()
                .map(WorkDiaryGroupMenuJpaEntity::getMenuId)
                .toList();
    }

    @Override
    @Transactional
    public void replace(long workDiaryGroupId, List<String> menuIds) {
        repository.deleteByWorkDiaryGroupId(workDiaryGroupId);
        repository.flush();
        for (String menuId : menuIds) {
            repository.save(new WorkDiaryGroupMenuJpaEntity(workDiaryGroupId, menuId));
        }
    }

    @Override
    @Transactional
    public void deleteByGroupId(long workDiaryGroupId) {
        repository.deleteByWorkDiaryGroupId(workDiaryGroupId);
    }
}
