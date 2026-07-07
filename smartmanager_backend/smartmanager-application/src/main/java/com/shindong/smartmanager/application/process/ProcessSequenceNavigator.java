package com.shindong.smartmanager.application.process;

import com.shindong.smartmanager.domain.process.ProcessVariant;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import java.util.Comparator;
import java.util.List;
import java.util.Optional;

public final class ProcessSequenceNavigator {

    private ProcessSequenceNavigator() {
    }

    public static List<ProcessView> sortedPlanProcesses(ProcessRepository processRepository, long itemId) {
        return processRepository.findAllActiveByItemId(itemId, ProcessVariant.plan).stream()
                .sorted(Comparator.comparingInt(ProcessView::processSequenceNum))
                .toList();
    }

    public static Optional<ProcessView> findNextProcess(
            ProcessRepository processRepository,
            long itemId,
            short currentSequenceNum
    ) {
        return sortedPlanProcesses(processRepository, itemId).stream()
                .filter(process -> process.processSequenceNum() > currentSequenceNum)
                .findFirst();
    }

    public static boolean isFinalProcess(
            ProcessRepository processRepository,
            long itemId,
            short currentSequenceNum
    ) {
        return findNextProcess(processRepository, itemId, currentSequenceNum).isEmpty();
    }

    public static boolean isFirstProcess(
            ProcessRepository processRepository,
            long itemId,
            short sequenceNum
    ) {
        return sortedPlanProcesses(processRepository, itemId).stream()
                .map(ProcessView::processSequenceNum)
                .min(Short::compare)
                .map(min -> min.equals(sequenceNum))
                .orElse(true);
    }

    public static boolean isFirstInhouseProcess(
            ProcessRepository processRepository,
            long itemId,
            short sequenceNum
    ) {
        return sortedPlanProcesses(processRepository, itemId).stream()
                .filter(process -> process.workDistinction() == WorkDistinction.INHOUSE
                        || process.workDistinction() == WorkDistinction.SPLIT)
                .map(ProcessView::processSequenceNum)
                .min(Short::compare)
                .map(min -> min.equals(sequenceNum))
                .orElse(true);
    }

    public static Optional<ProcessView> findImmediatePriorProcess(
            ProcessRepository processRepository,
            long itemId,
            short sequenceNum
    ) {
        return sortedPlanProcesses(processRepository, itemId).stream()
                .filter(process -> process.processSequenceNum() < sequenceNum)
                .max(Comparator.comparingInt(ProcessView::processSequenceNum));
    }

    public static Optional<ProcessView> findImmediatePriorInhouseProcess(
            ProcessRepository processRepository,
            long itemId,
            short sequenceNum
    ) {
        return sortedPlanProcesses(processRepository, itemId).stream()
                .filter(process -> process.processSequenceNum() < sequenceNum)
                .filter(process -> process.workDistinction() == WorkDistinction.INHOUSE
                        || process.workDistinction() == WorkDistinction.SPLIT)
                .max(Comparator.comparingInt(ProcessView::processSequenceNum));
    }

    public static Optional<ProcessView> findProcessBySequenceId(
            ProcessRepository processRepository,
            long itemId,
            long processSequenceId
    ) {
        return sortedPlanProcesses(processRepository, itemId).stream()
                .filter(process -> process.id() == processSequenceId)
                .findFirst();
    }

    public static boolean isFinalProcessBySequenceId(
            ProcessRepository processRepository,
            long itemId,
            long processSequenceId
    ) {
        return findProcessBySequenceId(processRepository, itemId, processSequenceId)
                .map(process -> isFinalProcess(processRepository, itemId, process.processSequenceNum()))
                .orElse(false);
    }
}
