package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.application.outsource.OutsourceHistoryRecord;
import com.shindong.smartmanager.domain.purchase.PayableApprovalStatus;
import java.util.List;

public interface PayableApprovalRepository {

    List<PayableApprovalView> findPending(PayableApprovalCriteria criteria);

    List<PayableApprovalView> findApproved(PayableApprovalCriteria criteria);

    PurchaseHistoryRecord findActivePurchaseHistory(long id);

    OutsourceHistoryRecord findActiveOutsourceHistory(long id);

    void approvePurchaseHistory(long id, long userId);

    void cancelApprovalPurchaseHistory(long id, long userId);

    void approveOutsourceHistory(long id, long userId);

    void cancelApprovalOutsourceHistory(long id, long userId);

    void updatePurchaseHistoryFiscalPeriod(long id, int fiscalYear, int fiscalMonth);

    void updateOutsourceHistoryFiscalPeriod(long id, int fiscalYear, int fiscalMonth);
}
