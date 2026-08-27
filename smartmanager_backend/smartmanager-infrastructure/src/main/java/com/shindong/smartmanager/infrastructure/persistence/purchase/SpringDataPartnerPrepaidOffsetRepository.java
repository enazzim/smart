package com.shindong.smartmanager.infrastructure.persistence.purchase;

import com.shindong.smartmanager.domain.purchase.PartnerPrepaidOffsetLedgerKind;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataPartnerPrepaidOffsetRepository extends JpaRepository<PartnerPrepaidOffsetJpaEntity, Long> {

    List<PartnerPrepaidOffsetJpaEntity> findByLedgerKindAndHistoryIdAndRecordingState(
            PartnerPrepaidOffsetLedgerKind ledgerKind,
            long historyId,
            int recordingState
    );

    @Query("""
            SELECT COALESCE(SUM(o.amount), 0)
            FROM PartnerPrepaidOffsetJpaEntity o
            WHERE o.ledgerKind = :ledgerKind
              AND o.historyId = :historyId
              AND o.recordingState = :active
            """)
    java.math.BigDecimal sumActiveOffsetByHistory(
            @Param("ledgerKind") PartnerPrepaidOffsetLedgerKind ledgerKind,
            @Param("historyId") long historyId,
            @Param("active") int active
    );
}
