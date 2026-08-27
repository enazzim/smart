package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;
import com.shindong.smartmanager.domain.purchase.PartnerPrepaidOffsetLedgerKind;
import java.math.BigDecimal;
import java.util.List;
import java.util.Optional;

public interface PartnerPaymentRepository {

    long countByPaymentNoPrefix(String prefix);

    BigDecimal sumIssuedPayableAmountByPartnerId(long partnerId);

    BigDecimal sumIssuedPaymentAmountByPartnerId(long partnerId);

    BigDecimal sumPrepaidRemainingByPartnerId(long partnerId);

    BigDecimal unpaidPartnerAmount(long partnerId);

    BigDecimal sumActiveOffsetByHistory(PartnerPrepaidOffsetLedgerKind ledgerKind, long historyId);

    boolean hasActiveOffsetByPaymentId(long paymentId);

    List<PartnerPaymentCandidateView> findCandidates(PartnerPaymentCandidateCriteria criteria);

    List<PrepaidBalanceView> findPrepaidBalances(Long partnerId, PartnerPaymentCostCategory costCategory);

    List<PrepaidOrderLineCandidateView> findOrderLineCandidates(PrepaidOrderLineCandidateCriteria criteria);

    BigDecimal remainingOrderLineAmount(PartnerPaymentCostCategory costCategory, long orderLineId);

    List<FifoPrepaidLineView> findFifoPrepaidLines(
            long partnerId,
            long itemId,
            PartnerPaymentCostCategory costCategory
    );

    void saveOffsets(List<PartnerPrepaidOffsetSaveCommand> offsets, String actorUserId);

    void deactivateOffsetsByHistory(PartnerPrepaidOffsetLedgerKind ledgerKind, long historyId, String actorUserId);

    PartnerPaymentView save(PartnerPaymentSaveCommand command, String actorUserId);

    List<PartnerPaymentView> findAllActive(PartnerPaymentListCriteria criteria);

    Optional<PartnerPaymentView> findActiveIssuedById(long id);

    void cancelById(long id, String actorUserId);
}
