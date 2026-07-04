package com.shindong.smartmanager.infrastructure.persistence.calendar;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.time.Instant;
import java.time.LocalDate;

@Entity
@Table(name = "public_holiday")
public class PublicHolidayJpaEntity {

    @Id
    @Column(name = "holiday_date", nullable = false)
    private LocalDate holidayDate;

    @Column(name = "holiday_name", nullable = false, length = 100)
    private String holidayName;

    @Column(name = "created_at", nullable = false)
    private Instant createdAt;

    protected PublicHolidayJpaEntity() {
    }

    public LocalDate getHolidayDate() {
        return holidayDate;
    }

    public String getHolidayName() {
        return holidayName;
    }
}
