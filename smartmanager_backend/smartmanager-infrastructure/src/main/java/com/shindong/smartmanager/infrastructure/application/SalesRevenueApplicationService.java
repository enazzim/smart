package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.sales.CreateSalesRevenueCommand;
import com.shindong.smartmanager.application.sales.SalesRevenueCandidateCriteria;
import com.shindong.smartmanager.application.sales.SalesRevenueCandidateView;
import com.shindong.smartmanager.application.sales.SalesRevenueListCriteria;
import com.shindong.smartmanager.application.sales.SalesRevenueService;
import com.shindong.smartmanager.application.sales.SalesRevenueView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class SalesRevenueApplicationService {

    private final SalesRevenueService salesRevenueService;

    public SalesRevenueApplicationService(SalesRevenueService salesRevenueService) {
        this.salesRevenueService = salesRevenueService;
    }

    @Transactional(readOnly = true)
    public List<SalesRevenueCandidateView> listCandidates(SalesRevenueCandidateCriteria criteria) {
        return salesRevenueService.listCandidates(criteria);
    }

    @Transactional(readOnly = true)
    public List<SalesRevenueView> list(SalesRevenueListCriteria criteria) {
        return salesRevenueService.list(criteria);
    }

    @Transactional
    public SalesRevenueView register(CreateSalesRevenueCommand command, String actorUserId) {
        return salesRevenueService.register(command, actorUserId);
    }

    @Transactional
    public void cancel(long id, String actorUserId) {
        salesRevenueService.cancel(id, actorUserId);
    }
}
