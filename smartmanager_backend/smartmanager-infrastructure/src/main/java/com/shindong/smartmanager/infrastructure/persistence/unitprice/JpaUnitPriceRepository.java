package com.shindong.smartmanager.infrastructure.persistence.unitprice;

import com.shindong.smartmanager.application.unitprice.UnitPriceChangeLogView;
import com.shindong.smartmanager.application.unitprice.UnitPriceCommand;
import com.shindong.smartmanager.application.unitprice.UnitPriceHistorySearchQuery;
import com.shindong.smartmanager.application.unitprice.UnitPriceRepository;
import com.shindong.smartmanager.application.unitprice.UnitPriceUpdateCommand;
import com.shindong.smartmanager.application.unitprice.UnitPriceView;
import com.shindong.smartmanager.domain.pricing.CostType;
import com.shindong.smartmanager.infrastructure.persistence.code.PublicCodeJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.code.SpringDataPublicCodeRepository;
import com.shindong.smartmanager.infrastructure.persistence.company.CompanyJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.company.SpringDataCompanyRepository;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.item.SpringDataItemRepository;
import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;
import java.time.ZoneId;
import java.util.Collection;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaUnitPriceRepository implements UnitPriceRepository {

    private final SpringDataUnitPriceRepository unitPriceRepository;
    private final SpringDataUnitPriceChangeLogRepository changeLogRepository;
    private final SpringDataItemRepository itemRepository;
    private final SpringDataCompanyRepository companyRepository;
    private final SpringDataPublicCodeRepository publicCodeRepository;
    private final MasterAuditActorLookup masterAuditActorLookup;

    public JpaUnitPriceRepository(
            SpringDataUnitPriceRepository unitPriceRepository,
            SpringDataUnitPriceChangeLogRepository changeLogRepository,
            SpringDataItemRepository itemRepository,
            SpringDataCompanyRepository companyRepository,
            SpringDataPublicCodeRepository publicCodeRepository,
            MasterAuditActorLookup masterAuditActorLookup
    ) {
        this.unitPriceRepository = unitPriceRepository;
        this.changeLogRepository = changeLogRepository;
        this.itemRepository = itemRepository;
        this.companyRepository = companyRepository;
        this.publicCodeRepository = publicCodeRepository;
        this.masterAuditActorLookup = masterAuditActorLookup;
    }

    @Override
    @Transactional
    public long save(UnitPriceCommand command, String actorUserId) {
        Instant now = Instant.now();
        UnitPriceJpaEntity entity = new UnitPriceJpaEntity();
        entity.setCostType(command.costType());
        entity.setItemId(command.itemId());
        entity.setCompanyId(command.companyId());
        entity.setBeginProcessCodeId(command.beginProcessCodeId());
        entity.setEndProcessCodeId(command.endProcessCodeId());
        entity.setOrderRate(normalizeOrderRate(command.costType(), command.orderRate()));
        entity.setStandardUnitCost(command.standardUnitCost());
        entity.setDiscountUnitCost(command.discountUnitCost());
        entity.setBeginDate(command.beginDate());
        entity.setEndDate(command.endDate());
        entity.setRecordingState(1);
        entity.setCreatedBy(masterAuditActorLookup.nameOf(actorUserId));
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(masterAuditActorLookup.nameOf(actorUserId));
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        return unitPriceRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public void update(long id, UnitPriceUpdateCommand command, String actorUserId) {
        UnitPriceJpaEntity entity = unitPriceRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("단가를 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setOrderRate(normalizeOrderRate(entity.getCostType(), command.orderRate()));
        entity.setStandardUnitCost(command.standardUnitCost());
        entity.setDiscountUnitCost(command.discountUnitCost());
        entity.setBeginDate(command.beginDate());
        entity.setEndDate(command.endDate());
        entity.setUpdatedBy(masterAuditActorLookup.nameOf(actorUserId));
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        unitPriceRepository.save(entity);
    }

    @Override
    @Transactional
    public void softDelete(long id, String actorUserId) {
        UnitPriceJpaEntity entity = unitPriceRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("단가를 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setRecordingState(0);
        entity.setUpdatedBy(masterAuditActorLookup.nameOf(actorUserId));
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        unitPriceRepository.save(entity);
    }

    @Override
    public Optional<UnitPriceView> findActiveById(long id) {
        return unitPriceRepository.findByIdAndRecordingState(id, 1).map(this::toView);
    }

    @Override
    public List<UnitPriceView> findAllActiveByCostType(CostType costType, String query) {
        String normalizedQuery = query == null ? "" : query.trim();
        List<UnitPriceJpaEntity> entities = normalizedQuery.isEmpty()
                ? unitPriceRepository.findByCostTypeAndRecordingStateOrderByBeginDateDescIdDesc(costType, 1)
                : unitPriceRepository.searchActive(costType, normalizedQuery);
        return entities.stream().map(this::toView).toList();
    }

    @Override
    public List<UnitPriceView> findAllActiveByCostTypeAndItemIds(CostType costType, Collection<Long> itemIds) {
        if (itemIds == null || itemIds.isEmpty()) {
            return List.of();
        }
        return unitPriceRepository.findActiveByCostTypeAndItemIds(costType, itemIds).stream()
                .map(this::toView)
                .toList();
    }

    @Override
    public Optional<UnitPriceView> findActiveByItemNoAndCostType(String itemNo, CostType costType) {
        List<UnitPriceJpaEntity> rows = unitPriceRepository.findActiveByItemNoAndCostType(itemNo, costType);
        if (rows.isEmpty()) {
            return Optional.empty();
        }
        return Optional.of(toView(rows.get(0)));
    }

    @Override
    public boolean existsActiveUk(
            CostType costType,
            long itemId,
            long companyId,
            LocalDate beginDate,
            Long beginProcessCodeId,
            Long endProcessCodeId,
            Long excludeId
    ) {
        return unitPriceRepository.existsActiveUk(
                costType, itemId, companyId, beginDate, beginProcessCodeId, endProcessCodeId, excludeId);
    }

    @Override
    @Transactional
    public void lockActiveRowsForOrderRateValidation(CostType costType, long itemId) {
        unitPriceRepository.lockActiveByCostTypeAndItemId(costType, itemId);
    }

    @Override
    public BigDecimal sumActiveOrderRate(CostType costType, long itemId, Long excludeId) {
        return unitPriceRepository.sumActiveOrderRate(costType, itemId, excludeId);
    }

    @Override
    public BigDecimal sumActiveOrderRateForOutsourceSegment(
            long itemId,
            long beginProcessCodeId,
            long endProcessCodeId,
            Long excludeId
    ) {
        return unitPriceRepository.sumActiveOrderRateForOutsourceSegment(
                itemId, beginProcessCodeId, endProcessCodeId, excludeId);
    }

    @Override
    @Transactional
    public void appendChangeLog(long unitPriceId, String updateReason, String actorUserId) {
        UnitPriceJpaEntity entity = unitPriceRepository.findByIdAndRecordingState(unitPriceId, 1)
                .orElseThrow(() -> new IllegalArgumentException("단가를 찾을 수 없습니다: " + unitPriceId));

        Instant now = Instant.now();
        UnitPriceChangeLogJpaEntity log = new UnitPriceChangeLogJpaEntity();
        log.setUnitPriceId(entity.getId());
        log.setCostType(entity.getCostType());
        log.setItemId(entity.getItemId());
        log.setCompanyId(entity.getCompanyId());
        log.setBeginProcessCodeId(entity.getBeginProcessCodeId());
        log.setEndProcessCodeId(entity.getEndProcessCodeId());
        log.setOrderRate(entity.getOrderRate());
        log.setStandardUnitCost(entity.getStandardUnitCost());
        log.setDiscountUnitCost(entity.getDiscountUnitCost());
        log.setBeginDate(entity.getBeginDate());
        log.setEndDate(entity.getEndDate());
        log.setUpdateReason(updateReason);
        log.setChangedBy(masterAuditActorLookup.nameOf(actorUserId));
        log.setChangedById(actorUserId);
        log.setChangedAt(now);
        changeLogRepository.save(log);
    }

    @Override
    public List<UnitPriceChangeLogView> findChangeLogs(long unitPriceId) {
        return changeLogRepository.findByUnitPriceIdOrderByChangedAtDesc(unitPriceId).stream()
                .map(this::toChangeLogView)
                .toList();
    }

    @Override
    public List<UnitPriceChangeLogView> findChangeLogs(UnitPriceHistorySearchQuery query) {
        ZoneId zone = ZoneId.of("Asia/Seoul");
        Instant fromInstant = query.changedFrom() == null
                ? null
                : query.changedFrom().atStartOfDay(zone).toInstant();
        Instant toExclusiveInstant = query.changedTo() == null
                ? null
                : query.changedTo().plusDays(1).atStartOfDay(zone).toInstant();
        String changedBy = query.changedBy() == null ? "" : query.changedBy().trim();
        return changeLogRepository.search(
                        query.costType(),
                        query.companyId(),
                        query.itemId(),
                        fromInstant,
                        toExclusiveInstant,
                        changedBy
                ).stream()
                .map(this::toChangeLogView)
                .toList();
    }

    private UnitPriceChangeLogView toChangeLogView(UnitPriceChangeLogJpaEntity log) {
        ItemJpaEntity item = itemRepository.findById(log.getItemId()).orElse(null);
        CompanyJpaEntity company = companyRepository.findById(log.getCompanyId()).orElse(null);
        ProcessCodeSnapshot begin = resolveProcessCodeQuiet(log.getBeginProcessCodeId());
        ProcessCodeSnapshot end = resolveProcessCodeQuiet(log.getEndProcessCodeId());
        return new UnitPriceChangeLogView(
                log.getId(),
                log.getUnitPriceId(),
                log.getCostType(),
                log.getItemId(),
                item != null ? item.getItemNo() : "",
                item != null ? item.getItemName() : "",
                log.getCompanyId(),
                company != null ? company.getCompanyName() : "",
                log.getBeginProcessCodeId(),
                begin.code(),
                begin.name(),
                log.getEndProcessCodeId(),
                end.code(),
                end.name(),
                log.getOrderRate(),
                log.getStandardUnitCost(),
                log.getDiscountUnitCost(),
                log.getBeginDate(),
                log.getEndDate(),
                log.getUpdateReason(),
                log.getChangedBy(),
                log.getChangedById(),
                log.getChangedAt()
        );
    }

    private ProcessCodeSnapshot resolveProcessCodeQuiet(Long processCodeId) {
        if (processCodeId == null) {
            return new ProcessCodeSnapshot(null, null);
        }
        return publicCodeRepository.findById(processCodeId)
                .map(code -> new ProcessCodeSnapshot(code.getSmallCode(), code.getSmallName()))
                .orElseGet(() -> new ProcessCodeSnapshot(null, null));
    }

    private BigDecimal normalizeOrderRate(CostType costType, BigDecimal orderRate) {
        if (costType == CostType.SALE) {
            return BigDecimal.ZERO;
        }
        return orderRate != null ? orderRate : BigDecimal.ZERO;
    }

    private UnitPriceView toView(UnitPriceJpaEntity entity) {
        ItemJpaEntity item = itemRepository.findById(entity.getItemId())
                .orElseThrow(() -> new IllegalStateException("품목을 찾을 수 없습니다: " + entity.getItemId()));
        CompanyJpaEntity company = companyRepository.findById(entity.getCompanyId())
                .orElseThrow(() -> new IllegalStateException("거래처를 찾을 수 없습니다: " + entity.getCompanyId()));

        ProcessCodeSnapshot begin = resolveProcessCode(entity.getBeginProcessCodeId());
        ProcessCodeSnapshot end = resolveProcessCode(entity.getEndProcessCodeId());

        return new UnitPriceView(
                entity.getId(),
                entity.getCostType(),
                entity.getItemId(),
                item.getItemNo(),
                item.getItemName(),
                item.getPropertyClassification() != null ? item.getPropertyClassification().name() : "",
                entity.getCompanyId(),
                company.getCompanyName(),
                company.getBusinessRegNo(),
                entity.getBeginProcessCodeId(),
                begin.code(),
                begin.name(),
                entity.getEndProcessCodeId(),
                end.code(),
                end.name(),
                entity.getOrderRate(),
                entity.getStandardUnitCost(),
                entity.getDiscountUnitCost(),
                entity.getBeginDate(),
                entity.getEndDate(),
                entity.getCreatedAt()
        );
    }

    private ProcessCodeSnapshot resolveProcessCode(Long processCodeId) {
        if (processCodeId == null) {
            return new ProcessCodeSnapshot(null, null);
        }
        PublicCodeJpaEntity code = publicCodeRepository.findById(processCodeId)
                .orElseThrow(() -> new IllegalStateException("공정코드를 찾을 수 없습니다: " + processCodeId));
        return new ProcessCodeSnapshot(code.getSmallCode(), code.getSmallName());
    }

    private record ProcessCodeSnapshot(String code, String name) {
    }
}
