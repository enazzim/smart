package com.shindong.smartmanager.infrastructure.persistence.company;

import com.shindong.smartmanager.application.company.CompanyCommand;
import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.company.CompanyUpdateCommand;
import com.shindong.smartmanager.application.company.CompanyView;
import com.shindong.smartmanager.domain.company.BusinessRegNos;
import com.shindong.smartmanager.domain.company.CompanyRoleType;
import java.time.Instant;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaCompanyRepository implements CompanyRepository {

    private final SpringDataCompanyRepository companyRepository;
    private final SpringDataCompanyRoleRepository companyRoleRepository;

    public JpaCompanyRepository(
            SpringDataCompanyRepository companyRepository,
            SpringDataCompanyRoleRepository companyRoleRepository
    ) {
        this.companyRepository = companyRepository;
        this.companyRoleRepository = companyRoleRepository;
    }

    @Override
    public boolean existsActiveByBusinessRegNo(String businessRegNo) {
        return findActiveEntityByBusinessRegNo(businessRegNo).isPresent();
    }

    @Override
    @Transactional
    public long save(CompanyCommand command, String actorUserId) {
        Instant now = Instant.now();
        CompanyJpaEntity entity = new CompanyJpaEntity();
        entity.setCompanyName(command.companyName());
        entity.setPresidentName(command.presidentName());
        entity.setBusinessRegNo(command.businessRegNo());
        entity.setCorporationRegNo(command.corporationRegNo());
        entity.setBusinessAddress(command.businessAddress());
        entity.setHomepageUrl(command.homepageUrl());
        entity.setBusinessType(command.businessType());
        entity.setBusinessItem(command.businessItem());
        entity.setTelephone(command.telephone());
        entity.setFax(command.fax());
        entity.setSaleStandardDay(command.saleStandardDay());
        entity.setBillApprovalStandard(command.billApprovalStandard());
        entity.setFixCollectDay1(command.fixCollectDay1());
        entity.setContactName(command.contactName());
        entity.setContactEmail(command.contactEmail());
        entity.setRecordingState(1);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        return companyRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public void replaceRoles(long companyId, List<CompanyRoleType> roles) {
        companyRoleRepository.deleteByCompanyId(companyId);
        companyRoleRepository.flush();
        for (CompanyRoleType role : roles) {
            companyRoleRepository.save(new CompanyRoleJpaEntity(companyId, role));
        }
    }

    @Override
    public List<CompanyRoleType> findRoles(long companyId) {
        return companyRoleRepository.findByCompanyId(companyId).stream()
                .map(CompanyRoleJpaEntity::getRoleType)
                .toList();
    }

    @Override
    public List<CompanyView> findAllActive() {
        return companyRepository.findByRecordingStateOrderByIdDesc(1).stream()
                .map(entity -> toView(entity, findRoles(entity.getId())))
                .toList();
    }

    @Override
    public Optional<CompanyView> findActiveById(long id) {
        return companyRepository.findByIdAndRecordingState(id, 1)
                .map(entity -> toView(entity, findRoles(entity.getId())));
    }

    @Override
    public Optional<CompanyView> findActiveByBusinessRegNo(String businessRegNo) {
        return findActiveEntityByBusinessRegNo(businessRegNo)
                .map(entity -> toView(entity, findRoles(entity.getId())));
    }

    /**
     * 하이픈 유무·과거 숫자만 저장분까지 동일 사업자로 본다.
     */
    private Optional<CompanyJpaEntity> findActiveEntityByBusinessRegNo(String businessRegNo) {
        String canonical = BusinessRegNos.canonicalize(businessRegNo);
        Optional<CompanyJpaEntity> byCanonical =
                companyRepository.findByBusinessRegNoAndRecordingState(canonical, 1);
        if (byCanonical.isPresent()) {
            return byCanonical;
        }
        String digits = BusinessRegNos.digitsOnly(businessRegNo);
        if (!digits.isEmpty() && !digits.equals(canonical)) {
            Optional<CompanyJpaEntity> byDigits =
                    companyRepository.findByBusinessRegNoAndRecordingState(digits, 1);
            if (byDigits.isPresent()) {
                return byDigits;
            }
        }
        if (!businessRegNo.equals(canonical) && !businessRegNo.equals(digits)) {
            return companyRepository.findByBusinessRegNoAndRecordingState(businessRegNo.trim(), 1);
        }
        return Optional.empty();
    }

    @Override
    @Transactional
    public void update(long id, CompanyUpdateCommand command, String actorUserId) {
        CompanyJpaEntity entity = companyRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("거래처를 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setCompanyName(command.companyName());
        entity.setPresidentName(command.presidentName());
        entity.setCorporationRegNo(command.corporationRegNo());
        entity.setBusinessAddress(command.businessAddress());
        entity.setHomepageUrl(command.homepageUrl());
        entity.setBusinessType(command.businessType());
        entity.setBusinessItem(command.businessItem());
        entity.setTelephone(command.telephone());
        entity.setFax(command.fax());
        entity.setSaleStandardDay(command.saleStandardDay());
        entity.setBillApprovalStandard(command.billApprovalStandard());
        entity.setFixCollectDay1(command.fixCollectDay1());
        entity.setContactName(command.contactName());
        entity.setContactEmail(command.contactEmail());
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        companyRepository.save(entity);
    }

    @Override
    @Transactional
    public void softDelete(long id, String actorUserId) {
        CompanyJpaEntity entity = companyRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("거래처를 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setRecordingState(0);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        companyRepository.save(entity);
    }

    private CompanyView toView(CompanyJpaEntity entity, List<CompanyRoleType> roles) {
        return new CompanyView(
                entity.getId(),
                entity.getCompanyName(),
                entity.getPresidentName(),
                entity.getBusinessRegNo(),
                entity.getCorporationRegNo(),
                entity.getBusinessAddress(),
                entity.getHomepageUrl(),
                entity.getBusinessType(),
                entity.getBusinessItem(),
                entity.getTelephone(),
                entity.getFax(),
                entity.getSaleStandardDay(),
                entity.getBillApprovalStandard(),
                entity.getFixCollectDay1(),
                entity.getContactName(),
                entity.getContactEmail(),
                roles,
                entity.getCreatedAt()
        );
    }
}
