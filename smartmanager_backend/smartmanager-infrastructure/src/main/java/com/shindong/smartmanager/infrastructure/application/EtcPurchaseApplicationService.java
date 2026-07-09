package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.purchase.CreateEtcPurchaseOrderCommand;
import com.shindong.smartmanager.application.purchase.CreateEtcPurchaseReceiptCommand;
import com.shindong.smartmanager.application.purchase.EtcPurchaseOrderListCriteria;
import com.shindong.smartmanager.application.purchase.EtcPurchaseOrderService;
import com.shindong.smartmanager.application.purchase.EtcPurchaseOrderView;
import com.shindong.smartmanager.application.purchase.EtcPurchaseReceiptCandidateCriteria;
import com.shindong.smartmanager.application.purchase.EtcPurchaseReceiptCandidateView;
import com.shindong.smartmanager.application.purchase.EtcPurchaseReceiptListCriteria;
import com.shindong.smartmanager.application.purchase.EtcPurchaseReceiptService;
import com.shindong.smartmanager.application.purchase.EtcPurchaseReceiptView;
import com.shindong.smartmanager.application.purchase.UpdateEtcPurchaseOrderCommand;
import com.shindong.smartmanager.application.purchase.UpdateEtcPurchaseReceiptCommand;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class EtcPurchaseApplicationService {

    private final EtcPurchaseOrderService orderService;
    private final EtcPurchaseReceiptService receiptService;

    public EtcPurchaseApplicationService(
            EtcPurchaseOrderService orderService,
            EtcPurchaseReceiptService receiptService
    ) {
        this.orderService = orderService;
        this.receiptService = receiptService;
    }

    @Transactional(readOnly = true)
    public List<EtcPurchaseOrderView> listOrders(EtcPurchaseOrderListCriteria criteria) {
        return orderService.list(criteria);
    }

    @Transactional(readOnly = true)
    public EtcPurchaseOrderView getOrder(long id) {
        return orderService.get(id);
    }

    @Transactional
    public EtcPurchaseOrderView createOrder(CreateEtcPurchaseOrderCommand command, String actorUserId) {
        return orderService.create(command, actorUserId);
    }

    @Transactional
    public EtcPurchaseOrderView updateOrder(long id, UpdateEtcPurchaseOrderCommand command, String actorUserId) {
        return orderService.update(id, command, actorUserId);
    }

    @Transactional
    public void deleteOrder(long id, String actorUserId) {
        orderService.delete(id, actorUserId);
    }

    @Transactional(readOnly = true)
    public List<EtcPurchaseReceiptCandidateView> listReceiptCandidates(EtcPurchaseReceiptCandidateCriteria criteria) {
        return receiptService.listCandidates(criteria);
    }

    @Transactional(readOnly = true)
    public List<EtcPurchaseReceiptView> listReceipts(EtcPurchaseReceiptListCriteria criteria) {
        return receiptService.list(criteria);
    }

    @Transactional(readOnly = true)
    public EtcPurchaseReceiptView getReceipt(long id) {
        return receiptService.get(id);
    }

    @Transactional
    public List<EtcPurchaseReceiptView> registerReceipts(CreateEtcPurchaseReceiptCommand command, String actorUserId) {
        return receiptService.register(command, actorUserId);
    }

    @Transactional
    public EtcPurchaseReceiptView updateReceipt(long id, UpdateEtcPurchaseReceiptCommand command, String actorUserId) {
        return receiptService.update(id, command, actorUserId);
    }

    @Transactional
    public void cancelReceipt(long id, String actorUserId) {
        receiptService.cancel(id, actorUserId);
    }
}
