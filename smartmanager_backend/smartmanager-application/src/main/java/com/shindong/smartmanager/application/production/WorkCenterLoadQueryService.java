package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.application.calendar.WorkCenterCapaService;
import com.shindong.smartmanager.application.system.SystemSettingRepository;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Objects;
import java.util.TreeMap;

public class WorkCenterLoadQueryService {

    public static final String KEY_WARN_LOAD_THRESHOLD = "production.schedule.warn_load_threshold";
    private static final double DEFAULT_WARN_LOAD_THRESHOLD = 0.8;

    private final WorkPlanRepository workPlanRepository;
    private final WorkCenterCapaService workCenterCapaService;
    private final SystemSettingRepository systemSettingRepository;

    public WorkCenterLoadQueryService(
            WorkPlanRepository workPlanRepository,
            WorkCenterCapaService workCenterCapaService,
            SystemSettingRepository systemSettingRepository
    ) {
        this.workPlanRepository = workPlanRepository;
        this.workCenterCapaService = workCenterCapaService;
        this.systemSettingRepository = systemSettingRepository;
    }

    public WorkCenterLoadResult query(WorkCenterLoadQuery query) {
        Objects.requireNonNull(query.from(), "from");
        Objects.requireNonNull(query.to(), "to");
        if (query.from().isAfter(query.to())) {
            throw new IllegalArgumentException("시작일은 종료일보다 늦을 수 없습니다.");
        }

        double warnThreshold = resolveWarnLoadThreshold();
        List<WorkPlanView> plans = workPlanRepository.findActivePlannedForLoad(
                query.workCenterId(),
                query.from(),
                query.to()
        );

        Map<LoadKey, List<WorkPlanView>> grouped = new HashMap<>();
        Map<Long, String> workCenterNames = new HashMap<>();
        for (WorkPlanView plan : plans) {
            if (plan.workCenterId() == null || plan.planStartDate() == null) {
                continue;
            }
            LoadKey key = new LoadKey(plan.workCenterId(), plan.planStartDate());
            grouped.computeIfAbsent(key, ignored -> new ArrayList<>()).add(plan);
            workCenterNames.putIfAbsent(plan.workCenterId(), plan.workCenterName());
        }

        List<WorkCenterLoadDayView> days = new ArrayList<>();
        if (query.workCenterId() != null) {
            String wcName = workCenterNames.getOrDefault(query.workCenterId(), "");
            for (LocalDate date = query.from(); !date.isAfter(query.to()); date = date.plusDays(1)) {
                days.add(buildDayView(
                        query.workCenterId(),
                        wcName,
                        date,
                        grouped.getOrDefault(new LoadKey(query.workCenterId(), date), List.of()),
                        warnThreshold
                ));
            }
        } else {
            TreeMap<LoadKey, List<WorkPlanView>> sorted = new TreeMap<>(
                    Comparator.comparing(LoadKey::workCenterId).thenComparing(LoadKey::date)
            );
            sorted.putAll(grouped);
            for (Map.Entry<LoadKey, List<WorkPlanView>> entry : sorted.entrySet()) {
                LoadKey key = entry.getKey();
                days.add(buildDayView(
                        key.workCenterId(),
                        workCenterNames.getOrDefault(key.workCenterId(), ""),
                        key.date(),
                        entry.getValue(),
                        warnThreshold
                ));
            }
        }

        return new WorkCenterLoadResult(warnThreshold, days);
    }

    private WorkCenterLoadDayView buildDayView(
            long workCenterId,
            String workCenterName,
            LocalDate date,
            List<WorkPlanView> plans,
            double warnThreshold
    ) {
        List<WorkCenterLoadDetailView> details = new ArrayList<>();
        long demandMinutes = 0;
        for (WorkPlanView plan : plans) {
            long lineDemand = WorkPlanDemandCalculator.demandMinutes(
                    plan.setupTime(),
                    plan.standardTime(),
                    plan.plannedQty()
            );
            demandMinutes += lineDemand;
            details.add(new WorkCenterLoadDetailView(
                    plan.id(),
                    plan.planNo(),
                    plan.itemNo(),
                    plan.itemName(),
                    plan.processName(),
                    plan.plannedQty(),
                    lineDemand,
                    plan.setupTime(),
                    plan.standardTime()
            ));
        }

        int capaMinutes = workCenterCapaService.resolveCapa(workCenterId, date).capaMinutes();
        Double loadRate = resolveLoadRate(demandMinutes, capaMinutes);
        boolean overThreshold = loadRate != null && loadRate >= warnThreshold;

        return new WorkCenterLoadDayView(
                workCenterId,
                workCenterName,
                date,
                demandMinutes,
                capaMinutes,
                loadRate,
                plans.size(),
                overThreshold,
                details
        );
    }

    private static Double resolveLoadRate(long demandMinutes, int capaMinutes) {
        if (capaMinutes <= 0) {
            return demandMinutes > 0 ? null : 0.0;
        }
        return (double) demandMinutes / capaMinutes;
    }

    private double resolveWarnLoadThreshold() {
        return systemSettingRepository.findActiveByKey(KEY_WARN_LOAD_THRESHOLD)
                .map(view -> parseThreshold(parseJsonString(view.valueJson())))
                .orElse(DEFAULT_WARN_LOAD_THRESHOLD);
    }

    private static String parseJsonString(String json) {
        if (json == null || json.isBlank()) {
            return "";
        }
        String trimmed = json.trim();
        if (trimmed.startsWith("\"") && trimmed.endsWith("\"") && trimmed.length() >= 2) {
            return trimmed.substring(1, trimmed.length() - 1);
        }
        return trimmed;
    }

    private static double parseThreshold(String raw) {
        if (raw == null || raw.isBlank()) {
            return DEFAULT_WARN_LOAD_THRESHOLD;
        }
        try {
            double value = Double.parseDouble(raw.trim());
            if (value <= 0 || value > 1) {
                return DEFAULT_WARN_LOAD_THRESHOLD;
            }
            return value;
        } catch (NumberFormatException ex) {
            return DEFAULT_WARN_LOAD_THRESHOLD;
        }
    }

    private record LoadKey(long workCenterId, LocalDate date) {
    }
}
