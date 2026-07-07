package com.shindong.smartmanager.application.bom;

import java.util.List;
import java.util.Optional;

public interface ItemCompositionRepository {

    long save(ItemCompositionCommand command, String actorUserId);

    void updateQuantities(long id, ItemCompositionUpdateCommand command, String actorUserId);

    void softDelete(long id, String actorUserId);

    Optional<ItemCompositionView> findActiveById(long id);

    List<ItemCompositionView> findAllActive(
            Long parentItemId,
            Long childItemId,
            String parentItemNoQuery,
            String childItemNoQuery
    );

    List<ItemCompositionView> findActiveByParentItemId(long parentItemId);

    List<ItemCompositionView> findActiveByChildItemId(long childItemId);

    boolean existsActiveUk(long parentItemId, long childItemId, Long excludeId);

    void appendChangeLog(long itemCompositionId, String changeReason, String actorUserId);
}
