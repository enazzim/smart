package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.sales.CreateSalesCollectionCommand;
import com.shindong.smartmanager.application.sales.SalesCollectionCandidateCriteria;
import com.shindong.smartmanager.application.sales.SalesCollectionCandidateView;
import com.shindong.smartmanager.application.sales.SalesCollectionListCriteria;
import com.shindong.smartmanager.application.sales.SalesCollectionService;
import com.shindong.smartmanager.application.sales.SalesCollectionView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class SalesCollectionApplicationService {

    private final SalesCollectionService salesCollectionService;

    public SalesCollectionApplicationService(SalesCollectionService salesCollectionService) {
        this.salesCollectionService = salesCollectionService;
    }

    @Transactional(readOnly = true)
    public List<SalesCollectionCandidateView> listCandidates(SalesCollectionCandidateCriteria criteria) {
        return salesCollectionService.listCandidates(criteria);
    }

    @Transactional(readOnly = true)
    public List<SalesCollectionView> list(SalesCollectionListCriteria criteria) {
        return salesCollectionService.list(criteria);
    }

    @Transactional
    public SalesCollectionView register(CreateSalesCollectionCommand command, String actorUserId) {
        return salesCollectionService.register(command, actorUserId);
    }

    @Transactional
    public void cancel(long id, String actorUserId) {
        salesCollectionService.cancel(id, actorUserId);
    }
}
