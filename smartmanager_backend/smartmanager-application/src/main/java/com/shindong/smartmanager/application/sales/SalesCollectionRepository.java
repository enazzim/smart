package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.domain.sales.SalesCollectionStatus;
import java.math.BigDecimal;
import java.util.List;
import java.util.Optional;

public interface SalesCollectionRepository {

    long countByCollectionNoPrefix(String prefix);

    BigDecimal sumIssuedRevenueAmountByPartnerId(long partnerId);

    BigDecimal sumIssuedCollectionAmountByPartnerId(long partnerId);

    List<SalesCollectionCandidateView> findCandidates(SalesCollectionCandidateCriteria criteria);

    SalesCollectionView save(SalesCollectionSaveCommand command, String actorUserId);

    List<SalesCollectionView> findAllActive(SalesCollectionListCriteria criteria);

    Optional<SalesCollectionView> findActiveIssuedById(long id);

    void cancelById(long id, String actorUserId);
}
