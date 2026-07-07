package com.shindong.smartmanager.infrastructure.persistence.workdiary;

import com.shindong.smartmanager.application.workdiary.WorkDiaryStatus;
import java.time.LocalDate;
import java.util.List;
import java.util.Optional;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataWorkDiaryEntryRepository extends JpaRepository<WorkDiaryEntryJpaEntity, Long> {

    @Query("""
            SELECT e FROM WorkDiaryEntryJpaEntity e
            WHERE e.id = :id
              AND e.recordingState = 1
            """)
    Optional<WorkDiaryEntryJpaEntity> findActiveById(@Param("id") long id);

    @Query("""
            SELECT e FROM WorkDiaryEntryJpaEntity e
            WHERE e.authorUserId = :authorUserId
              AND e.workDate = :workDate
              AND e.recordingState = 1
            """)
    Optional<WorkDiaryEntryJpaEntity> findActiveByAuthorAndDate(
            @Param("authorUserId") long authorUserId,
            @Param("workDate") LocalDate workDate
    );

    @Query("""
            SELECT e FROM WorkDiaryEntryJpaEntity e
            WHERE e.recordingState = 1
              AND (:authorUserId IS NULL OR e.authorUserId = :authorUserId)
              AND (:fromDate IS NULL OR e.workDate >= :fromDate)
              AND (:toDate IS NULL OR e.workDate <= :toDate)
              AND (:status IS NULL OR e.status = :status)
            ORDER BY e.workDate DESC, e.id DESC
            """)
    List<WorkDiaryEntryJpaEntity> findActiveEntries(
            @Param("authorUserId") Long authorUserId,
            @Param("fromDate") LocalDate fromDate,
            @Param("toDate") LocalDate toDate,
            @Param("status") WorkDiaryStatus status,
            Pageable pageable
    );

    @Query("""
            SELECT COUNT(e) FROM WorkDiaryEntryJpaEntity e
            WHERE e.recordingState = 1
              AND (:authorUserId IS NULL OR e.authorUserId = :authorUserId)
              AND (:fromDate IS NULL OR e.workDate >= :fromDate)
              AND (:toDate IS NULL OR e.workDate <= :toDate)
              AND (:status IS NULL OR e.status = :status)
            """)
    long countActiveEntries(
            @Param("authorUserId") Long authorUserId,
            @Param("fromDate") LocalDate fromDate,
            @Param("toDate") LocalDate toDate,
            @Param("status") WorkDiaryStatus status
    );
}

