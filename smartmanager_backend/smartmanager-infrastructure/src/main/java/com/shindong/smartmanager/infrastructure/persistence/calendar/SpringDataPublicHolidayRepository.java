package com.shindong.smartmanager.infrastructure.persistence.calendar;

import java.time.LocalDate;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataPublicHolidayRepository extends JpaRepository<PublicHolidayJpaEntity, LocalDate> {

    @Query("""
            select h from PublicHolidayJpaEntity h
            where year(h.holidayDate) = :year
            order by h.holidayDate
            """)
    List<PublicHolidayJpaEntity> findByYear(@Param("year") int year);
}
