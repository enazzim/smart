package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;
import java.util.Optional;

public interface OutsourcingShipmentRepository {

    long countByShipmentNoPrefix(String prefix);

    OutsourcingShipmentView save(OutsourcingShipmentSaveCommand command, String actorUserId);

    void cancelById(long id, String actorUserId);

    List<OutsourcingShipmentView> findAllActive(OutsourcingShipmentListCriteria criteria);

    Optional<OutsourcingShipmentView> findActiveIssuedById(long id);
}
