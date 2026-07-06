package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.purchase.CreatePurchaseReceiptCommand;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptCandidateCriteria;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptListCriteria;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptService;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class PurchaseReceiptApplicationService {

    private final PurchaseReceiptService purchaseReceiptService;

    public PurchaseReceiptApplicationService(PurchaseReceiptService purchaseReceiptService) {
        this.purchaseReceiptService = purchaseReceiptService;
    }

    @Transactional(readOnly = true)
    public List<com.shindong.smartmanager.application.purchase.PurchaseReceiptCandidateView> listCandidates(
            PurchaseReceiptCandidateCriteria criteria
    ) {
        return purchaseReceiptService.listCandidates(criteria);
    }

    @Transactional(readOnly = true)
    public List<PurchaseReceiptView> list(PurchaseReceiptListCriteria criteria) {
        return purchaseReceiptService.list(criteria);
    }

    @Transactional(readOnly = true)
    public PurchaseReceiptView get(long id) {
        return purchaseReceiptService.get(id);
    }

    @Transactional
    public PurchaseReceiptView register(CreatePurchaseReceiptCommand command, String actorUserId) {
        return purchaseReceiptService.register(command, actorUserId);
    }

    @Transactional
    public void cancel(long id, String actorUserId) {
        purchaseReceiptService.cancel(id, actorUserId);
    }
}
