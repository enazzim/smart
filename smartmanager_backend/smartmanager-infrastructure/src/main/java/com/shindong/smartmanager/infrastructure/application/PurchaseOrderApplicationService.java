package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.purchase.CreatePurchaseOrderFromMrpCommand;
import com.shindong.smartmanager.application.purchase.MrpPurchaseCandidateView;
import com.shindong.smartmanager.application.purchase.PurchaseOrderCommand;
import com.shindong.smartmanager.application.purchase.PurchaseOrderListCriteria;
import com.shindong.smartmanager.application.purchase.PurchaseOrderPrintView;
import com.shindong.smartmanager.application.purchase.PurchaseOrderService;
import com.shindong.smartmanager.application.purchase.PurchaseOrderView;
import java.time.LocalDate;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class PurchaseOrderApplicationService {

    private final PurchaseOrderService purchaseOrderService;

    public PurchaseOrderApplicationService(PurchaseOrderService purchaseOrderService) {
        this.purchaseOrderService = purchaseOrderService;
    }

    @Transactional(readOnly = true)
    public List<PurchaseOrderView> list() {
        return purchaseOrderService.list();
    }

    @Transactional(readOnly = true)
    public List<PurchaseOrderView> list(PurchaseOrderListCriteria criteria) {
        return purchaseOrderService.list(criteria);
    }

    @Transactional(readOnly = true)
    public PurchaseOrderPrintView getPrintView(long id) {
        return purchaseOrderService.getPrintView(id);
    }

    @Transactional(readOnly = true)
    public List<PurchaseOrderPrintView> getBatchPrintViews(List<Long> orderIds) {
        return purchaseOrderService.getBatchPrintViews(orderIds);
    }

    @Transactional(readOnly = true)
    public PurchaseOrderView get(long id) {
        return purchaseOrderService.get(id);
    }

    @Transactional(readOnly = true)
    public List<MrpPurchaseCandidateView> listMrpCandidates(LocalDate orderDate) {
        return purchaseOrderService.listMrpCandidates(orderDate);
    }

    @Transactional(readOnly = true)
    public String previewNextOrderNo(LocalDate orderDate) {
        return purchaseOrderService.previewNextOrderNo(orderDate);
    }

    @Transactional
    public PurchaseOrderView register(PurchaseOrderCommand command, String actorUserId) {
        return purchaseOrderService.register(command, actorUserId);
    }

    @Transactional
    public PurchaseOrderView registerFromMrp(CreatePurchaseOrderFromMrpCommand command, String actorUserId) {
        return purchaseOrderService.registerFromMrp(command, actorUserId);
    }

    @Transactional
    public PurchaseOrderView update(long id, PurchaseOrderCommand command, String actorUserId) {
        return purchaseOrderService.update(id, command, actorUserId);
    }

    @Transactional
    public PurchaseOrderView confirm(long id, String actorUserId) {
        return purchaseOrderService.confirm(id, actorUserId);
    }

    @Transactional
    public void cancel(long id, String actorUserId) {
        purchaseOrderService.cancel(id, actorUserId);
    }
}
