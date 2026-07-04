package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.sales.SalesOrderBulkResult;
import com.shindong.smartmanager.application.sales.SalesOrderCommand;
import com.shindong.smartmanager.application.sales.SalesOrderLineListCriteria;
import com.shindong.smartmanager.application.sales.SalesOrderLineListView;
import com.shindong.smartmanager.application.sales.SalesOrderService;
import com.shindong.smartmanager.application.sales.SalesOrderView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class SalesOrderApplicationService {

    private final SalesOrderService salesOrderService;

    public SalesOrderApplicationService(SalesOrderService salesOrderService) {
        this.salesOrderService = salesOrderService;
    }

    @Transactional(readOnly = true)
    public List<SalesOrderView> list() {
        return salesOrderService.list();
    }

    @Transactional(readOnly = true)
    public SalesOrderView get(long id) {
        return salesOrderService.get(id);
    }

    @Transactional
    public SalesOrderView register(SalesOrderCommand command, String actorUserId) {
        return salesOrderService.register(command, actorUserId);
    }

    @Transactional
    public SalesOrderView update(long id, SalesOrderCommand command, String actorUserId) {
        return salesOrderService.update(id, command, actorUserId);
    }

    @Transactional
    public SalesOrderView confirm(long id, String actorUserId) {
        return salesOrderService.confirm(id, actorUserId);
    }

    @Transactional
    public void cancel(long id, String actorUserId) {
        salesOrderService.cancel(id, actorUserId);
    }

    public String previewNextOrderNo(java.time.LocalDate orderDate) {
        return salesOrderService.previewNextOrderNo(orderDate);
    }

    public SalesOrderBulkResult registerBulk(
            java.util.List<SalesOrderCommand> commands,
            String actorUserId
    ) {
        return salesOrderService.registerBulk(commands, actorUserId);
    }

    @Transactional(readOnly = true)
    public java.util.List<SalesOrderLineListView> listLines(SalesOrderLineListCriteria criteria) {
        return salesOrderService.listLines(criteria);
    }

    @Transactional
    public SalesOrderLineListView startLineProgress(long lineId, String actorUserId) {
        return salesOrderService.startLineProgress(lineId, actorUserId);
    }

    @Transactional
    public SalesOrderLineListView completeLine(long lineId, String actorUserId) {
        return salesOrderService.completeLine(lineId, actorUserId);
    }

    @Transactional
    public SalesOrderLineListView forceCompleteLine(long lineId, String actorUserId) {
        return salesOrderService.forceCompleteLine(lineId, actorUserId);
    }
}
