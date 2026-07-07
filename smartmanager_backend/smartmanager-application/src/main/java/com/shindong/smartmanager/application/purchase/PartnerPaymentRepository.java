package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;
import java.util.List;
import java.util.Optional;

public interface PartnerPaymentRepository {

    long countByPaymentNoPrefix(String prefix);

    BigDecimal sumIssuedPayableAmountByPartnerId(long partnerId);

    BigDecimal sumIssuedPaymentAmountByPartnerId(long partnerId);

    List<PartnerPaymentCandidateView> findCandidates(PartnerPaymentCandidateCriteria criteria);

    PartnerPaymentView save(PartnerPaymentSaveCommand command, String actorUserId);

    List<PartnerPaymentView> findAllActive(PartnerPaymentListCriteria criteria);

    Optional<PartnerPaymentView> findActiveIssuedById(long id);

    void cancelById(long id, String actorUserId);
}
