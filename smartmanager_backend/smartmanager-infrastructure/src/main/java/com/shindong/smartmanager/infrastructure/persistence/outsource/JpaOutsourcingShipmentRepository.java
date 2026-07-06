package com.shindong.smartmanager.infrastructure.persistence.outsource;

import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderLineView;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderRepository;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderView;
import com.shindong.smartmanager.application.outsource.OutsourcingShipmentInputLineView;
import com.shindong.smartmanager.application.outsource.OutsourcingShipmentInputSaveCommand;
import com.shindong.smartmanager.application.outsource.OutsourcingShipmentLineSaveCommand;
import com.shindong.smartmanager.application.outsource.OutsourcingShipmentLineView;
import com.shindong.smartmanager.application.outsource.OutsourcingShipmentListCriteria;
import com.shindong.smartmanager.application.outsource.OutsourcingShipmentRepository;
import com.shindong.smartmanager.application.outsource.OutsourcingShipmentSaveCommand;
import com.shindong.smartmanager.application.outsource.OutsourcingShipmentView;
import com.shindong.smartmanager.domain.outsource.OutsourcingShipmentStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaOutsourcingShipmentRepository implements OutsourcingShipmentRepository {

    private static final int ACTIVE = 1;

    private final SpringDataOutsourcingShipmentRepository shipmentRepository;
    private final SpringDataOutsourcingShipmentLineRepository shipmentLineRepository;
    private final SpringDataOutsourcingShipmentInputLineRepository inputLineRepository;
    private final OutsourcingOrderRepository outsourcingOrderRepository;
    private final ItemRepository itemRepository;

    public JpaOutsourcingShipmentRepository(
            SpringDataOutsourcingShipmentRepository shipmentRepository,
            SpringDataOutsourcingShipmentLineRepository shipmentLineRepository,
            SpringDataOutsourcingShipmentInputLineRepository inputLineRepository,
            OutsourcingOrderRepository outsourcingOrderRepository,
            ItemRepository itemRepository
    ) {
        this.shipmentRepository = shipmentRepository;
        this.shipmentLineRepository = shipmentLineRepository;
        this.inputLineRepository = inputLineRepository;
        this.outsourcingOrderRepository = outsourcingOrderRepository;
        this.itemRepository = itemRepository;
    }

    @Override
    public long countByShipmentNoPrefix(String prefix) {
        return shipmentRepository.countByShipmentNoStartingWithAndRecordingState(prefix, ACTIVE);
    }

    @Override
    @Transactional
    public OutsourcingShipmentView save(OutsourcingShipmentSaveCommand command, String actorUserId) {
        Instant now = Instant.now();
        OutsourcingShipmentJpaEntity header = new OutsourcingShipmentJpaEntity();
        header.setShipmentNo(command.shipmentNo());
        header.setShipmentDate(command.shipmentDate());
        header.setStatus(OutsourcingShipmentStatus.ISSUED);
        header.setRecordingState(ACTIVE);
        header.setCreatedBy(actorUserId);
        header.setCreatedById(actorUserId);
        header.setCreatedAt(now);
        header.setUpdatedBy(actorUserId);
        header.setUpdatedById(actorUserId);
        header.setUpdatedAt(now);
        OutsourcingShipmentJpaEntity savedHeader = shipmentRepository.save(header);

        short lineNo = 1;
        for (OutsourcingShipmentLineSaveCommand lineCommand : command.lines()) {
            OutsourcingShipmentLineJpaEntity line = new OutsourcingShipmentLineJpaEntity();
            line.setOutsourcingShipmentId(savedHeader.getId());
            line.setLineNo(lineNo++);
            line.setOutsourcingOrderLineId(lineCommand.orderLineId());
            line.setShipmentQty(lineCommand.shipmentQty());
            line.setRecordingState(ACTIVE);
            line.setCreatedBy(actorUserId);
            line.setCreatedById(actorUserId);
            line.setCreatedAt(now);
            line.setUpdatedBy(actorUserId);
            line.setUpdatedById(actorUserId);
            line.setUpdatedAt(now);
            OutsourcingShipmentLineJpaEntity savedLine = shipmentLineRepository.save(line);

            short inputLineNo = 1;
            for (OutsourcingShipmentInputSaveCommand input : lineCommand.inputLines()) {
                OutsourcingShipmentInputLineJpaEntity inputLine = new OutsourcingShipmentInputLineJpaEntity();
                inputLine.setOutsourcingShipmentLineId(savedLine.getId());
                inputLine.setLineNo(inputLineNo++);
                inputLine.setItemId(input.itemId());
                inputLine.setItemCompositionId(input.itemCompositionId());
                inputLine.setIssueQty(input.issueQty());
                inputLine.setSourceLocationCode(input.sourceLocationCode());
                inputLine.setSourceProcessId(input.sourceProcessId());
                inputLine.setInputProcessId(input.inputProcessId());
                inputLine.setRecordingState(ACTIVE);
                inputLineRepository.save(inputLine);
            }
        }

        return findActiveIssuedById(savedHeader.getId())
                .orElseThrow(() -> new IllegalStateException("저장된 외주출고를 찾을 수 없습니다."));
    }

    @Override
    @Transactional
    public void cancelById(long id, String actorUserId) {
        OutsourcingShipmentJpaEntity entity = shipmentRepository
                .findByIdAndRecordingStateAndStatus(id, ACTIVE, OutsourcingShipmentStatus.ISSUED)
                .orElseThrow(() -> new IllegalArgumentException("외주출고를 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setStatus(OutsourcingShipmentStatus.CANCELLED);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        shipmentRepository.save(entity);
    }

    @Override
    @Transactional(readOnly = true)
    public List<OutsourcingShipmentView> findAllActive(OutsourcingShipmentListCriteria criteria) {
        List<OutsourcingShipmentJpaEntity> rows;
        if (criteria == null) {
            rows = shipmentRepository.findByRecordingStateOrderByShipmentDateDescIdDesc(ACTIVE);
        } else {
            rows = shipmentRepository.searchActive(
                    ACTIVE,
                    criteria.shipmentDateFrom(),
                    criteria.shipmentDateTo(),
                    normalize(criteria.shipmentNo()),
                    criteria.status(),
                    criteria.excludeCancelled(),
                    OutsourcingShipmentStatus.CANCELLED
            );
        }
        return rows.stream()
                .map(this::toView)
                .filter(view -> matchesPartnerName(view, criteria))
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<OutsourcingShipmentView> findActiveIssuedById(long id) {
        return shipmentRepository.findByIdAndRecordingStateAndStatus(id, ACTIVE, OutsourcingShipmentStatus.ISSUED)
                .map(this::toView);
    }

    private boolean matchesPartnerName(OutsourcingShipmentView view, OutsourcingShipmentListCriteria criteria) {
        if (criteria == null || criteria.partnerName() == null || criteria.partnerName().isBlank()) {
            return true;
        }
        String needle = criteria.partnerName().trim().toLowerCase();
        return view.lines().stream().anyMatch(line -> line.partnerName().toLowerCase().contains(needle));
    }

    private String normalize(String value) {
        if (value == null || value.isBlank()) {
            return null;
        }
        return value.trim();
    }

    private OutsourcingShipmentView toView(OutsourcingShipmentJpaEntity entity) {
        List<OutsourcingShipmentLineJpaEntity> lineEntities =
                shipmentLineRepository.findByOutsourcingShipmentIdAndRecordingStateOrderByLineNoAsc(
                        entity.getId(), ACTIVE);
        List<OutsourcingShipmentLineView> lines = new ArrayList<>();
        boolean cancelable = entity.getStatus() == OutsourcingShipmentStatus.ISSUED;
        for (OutsourcingShipmentLineJpaEntity lineEntity : lineEntities) {
            OutsourcingOrderLineView orderLine = outsourcingOrderRepository.findActiveLineById(
                    lineEntity.getOutsourcingOrderLineId()).orElse(null);
            OutsourcingOrderView order = outsourcingOrderRepository.findActiveByOrderLineId(
                    lineEntity.getOutsourcingOrderLineId()).orElse(null);
            if (orderLine != null && orderLine.receivedQty().compareTo(BigDecimal.ZERO) > 0) {
                cancelable = false;
            }
            List<OutsourcingShipmentInputLineJpaEntity> inputEntities =
                    inputLineRepository.findByOutsourcingShipmentLineIdAndRecordingStateOrderByLineNoAsc(
                            lineEntity.getId(), ACTIVE);
            List<OutsourcingShipmentInputLineView> inputLines = inputEntities.stream()
                    .map(input -> {
                        ItemView item = itemRepository.findActiveById(input.getItemId()).orElse(null);
                        return new OutsourcingShipmentInputLineView(
                                input.getItemId(),
                                item != null ? item.itemNo() : "",
                                item != null ? item.itemName() : "",
                                input.getItemCompositionId(),
                                input.getIssueQty(),
                                input.getSourceLocationCode(),
                                input.getSourceProcessId(),
                                input.getInputProcessId()
                        );
                    })
                    .toList();
            String processName = "";
            if (orderLine != null) {
                processName = orderLine.processName();
            }
            lines.add(new OutsourcingShipmentLineView(
                    lineEntity.getId(),
                    lineEntity.getLineNo(),
                    lineEntity.getOutsourcingOrderLineId(),
                    order != null ? order.orderNo() : "",
                    order != null ? order.partnerId() : 0L,
                    order != null ? order.partnerName() : "",
                    orderLine != null ? orderLine.itemNo() : "",
                    orderLine != null ? orderLine.itemName() : "",
                    processName,
                    lineEntity.getShipmentQty(),
                    inputLines
            ));
        }
        return new OutsourcingShipmentView(
                entity.getId(),
                entity.getShipmentNo(),
                entity.getShipmentDate(),
                entity.getStatus(),
                entity.getCreatedAt(),
                entity.getCreatedBy(),
                cancelable,
                lines
        );
    }
}
