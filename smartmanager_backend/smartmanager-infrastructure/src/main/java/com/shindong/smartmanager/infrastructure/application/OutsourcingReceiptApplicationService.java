package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.outsource.CreateOutsourcingReceiptCommand;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptCandidateCriteria;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptCandidateView;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptListCriteria;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptService;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class OutsourcingReceiptApplicationService {

    private final OutsourcingReceiptService outsourcingReceiptService;

    public OutsourcingReceiptApplicationService(OutsourcingReceiptService outsourcingReceiptService) {
        this.outsourcingReceiptService = outsourcingReceiptService;
    }

    @Transactional(readOnly = true)
    public List<OutsourcingReceiptCandidateView> listCandidates(OutsourcingReceiptCandidateCriteria criteria) {
        return outsourcingReceiptService.listCandidates(criteria);
    }

    @Transactional(readOnly = true)
    public List<OutsourcingReceiptView> list(OutsourcingReceiptListCriteria criteria) {
        return outsourcingReceiptService.list(criteria);
    }

    @Transactional(readOnly = true)
    public OutsourcingReceiptView get(long id) {
        return outsourcingReceiptService.get(id);
    }

    @Transactional
    public OutsourcingReceiptView register(CreateOutsourcingReceiptCommand command, String actorUserId) {
        return outsourcingReceiptService.register(command, actorUserId);
    }

    @Transactional
    public void cancel(long id, String actorUserId) {
        outsourcingReceiptService.cancel(id, actorUserId);
    }
}
