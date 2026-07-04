package com.shindong.smartmanager.infrastructure.persistence.calendar;

import com.shindong.smartmanager.application.calendar.PublicHolidayRepository;
import com.shindong.smartmanager.application.calendar.PublicHolidayView;
import java.time.LocalDate;
import java.util.List;
import java.util.Set;
import java.util.stream.Collectors;
import org.springframework.stereotype.Repository;

@Repository
public class JpaPublicHolidayRepository implements PublicHolidayRepository {

    private final SpringDataPublicHolidayRepository publicHolidayRepository;

    public JpaPublicHolidayRepository(SpringDataPublicHolidayRepository publicHolidayRepository) {
        this.publicHolidayRepository = publicHolidayRepository;
    }

    @Override
    public Set<LocalDate> findHolidayDatesByYear(int year) {
        return findByYear(year).stream()
                .map(PublicHolidayView::holidayDate)
                .collect(Collectors.toSet());
    }

    @Override
    public List<PublicHolidayView> findByYear(int year) {
        return publicHolidayRepository.findByYear(year).stream()
                .map(entity -> new PublicHolidayView(entity.getHolidayDate(), entity.getHolidayName()))
                .toList();
    }
}
