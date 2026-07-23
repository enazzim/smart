package com.shindong.smartmanager.application.company;

import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.domain.company.BusinessRegNos;
import com.shindong.smartmanager.domain.company.CompanyRoleType;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import java.time.Year;
import java.util.List;

public class CompanyService {

    private final CompanyRepository companyRepository;
    private final PartnerLedgerProjector partnerLedgerProjector;
    private final PartnerLedgerAccountRepository partnerLedgerAccountRepository;
    private final DomainEventStore domainEventStore;

    public CompanyService(
            CompanyRepository companyRepository,
            PartnerLedgerProjector partnerLedgerProjector,
            PartnerLedgerAccountRepository partnerLedgerAccountRepository,
            DomainEventStore domainEventStore
    ) {
        this.companyRepository = companyRepository;
        this.partnerLedgerProjector = partnerLedgerProjector;
        this.partnerLedgerAccountRepository = partnerLedgerAccountRepository;
        this.domainEventStore = domainEventStore;
    }

    public CompanyView register(CompanyCommand command, String actorUserId) {
        CompanyCommand normalized = withCanonicalBusinessRegNo(command);
        validateRoles(normalized.roles());
        validateRequiredFields(
                normalized.companyName(),
                normalized.presidentName(),
                normalized.businessRegNo(),
                normalized.businessAddress());
        if (companyRepository.existsActiveByBusinessRegNo(normalized.businessRegNo())) {
            throw new IllegalArgumentException("이미 등록된 사업자번호입니다: " + normalized.businessRegNo());
        }

        long companyId = companyRepository.save(normalized, actorUserId);
        companyRepository.replaceRoles(companyId, normalized.roles());

        int fiscalYear = Year.now().getValue();
        partnerLedgerProjector.ensureAccounts(companyId, normalized.roles(), fiscalYear, actorUserId);

        domainEventStore.append(DomainEvent.create(
                EventTypes.COMPANY_REGISTERED,
                1,
                AggregateTypes.COMPANY,
                String.valueOf(companyId),
                actorUserId,
                buildRegisteredPayload(normalized, companyId, fiscalYear)
        ));

        return companyRepository.findActiveById(companyId)
                .orElseThrow(() -> new IllegalStateException("등록 직후 거래처를 조회할 수 없습니다."));
    }

    public CompanyView update(long id, CompanyUpdateCommand command, String actorUserId) {
        CompanyView existing = getActive(id);
        validateRoles(command.roles());
        validateRequiredFields(command.companyName(), command.presidentName(), existing.businessRegNo(), command.businessAddress());

        companyRepository.update(id, command, actorUserId);
        companyRepository.replaceRoles(id, command.roles());

        int fiscalYear = Year.now().getValue();
        partnerLedgerProjector.ensureAccounts(id, command.roles(), fiscalYear, actorUserId);

        domainEventStore.append(DomainEvent.create(
                EventTypes.COMPANY_UPDATED,
                1,
                AggregateTypes.COMPANY,
                String.valueOf(id),
                actorUserId,
                buildUpdatedPayload(existing.businessRegNo(), command, id, fiscalYear)
        ));

        return getActive(id);
    }

    public void delete(long id, String actorUserId) {
        CompanyView existing = getActive(id);

        companyRepository.softDelete(id, actorUserId);
        partnerLedgerAccountRepository.deactivateByCompanyId(id, actorUserId);

        domainEventStore.append(DomainEvent.create(
                EventTypes.COMPANY_DELETED,
                1,
                AggregateTypes.COMPANY,
                String.valueOf(id),
                actorUserId,
                """
                {"companyId":%d,"businessRegNo":"%s","companyName":"%s"}
                """.formatted(id, escape(existing.businessRegNo()), escape(existing.companyName())).trim()
        ));
    }

    public List<CompanyView> listActive() {
        return companyRepository.findAllActive();
    }

    public CompanyView getActive(long id) {
        return companyRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("거래처를 찾을 수 없습니다: " + id));
    }

    private CompanyCommand withCanonicalBusinessRegNo(CompanyCommand command) {
        String canonical = BusinessRegNos.canonicalize(command.businessRegNo());
        if (canonical.equals(command.businessRegNo())) {
            return command;
        }
        return new CompanyCommand(
                command.companyName(),
                command.presidentName(),
                canonical,
                command.corporationRegNo(),
                command.businessAddress(),
                command.homepageUrl(),
                command.businessType(),
                command.businessItem(),
                command.telephone(),
                command.fax(),
                command.saleStandardDay(),
                command.billApprovalStandard(),
                command.fixCollectDay1(),
                command.contactName(),
                command.contactEmail(),
                command.roles()
        );
    }

    private void validateRoles(List<CompanyRoleType> roles) {
        if (roles == null || roles.isEmpty()) {
            throw new IllegalArgumentException("거래처 역할은 최소 1개 이상 선택해야 합니다.");
        }
    }

    private void validateRequiredFields(
            String companyName,
            String presidentName,
            String businessRegNo,
            String businessAddress
    ) {
        if (companyName == null || companyName.isBlank()) {
            throw new IllegalArgumentException("상호는 필수입니다.");
        }
        if (presidentName == null || presidentName.isBlank()) {
            throw new IllegalArgumentException("대표자는 필수입니다.");
        }
        if (businessRegNo == null || businessRegNo.isBlank()) {
            throw new IllegalArgumentException("사업자번호는 필수입니다.");
        }
        if (businessAddress == null || businessAddress.isBlank()) {
            throw new IllegalArgumentException("사업장주소는 필수입니다.");
        }
    }

    private String buildRegisteredPayload(CompanyCommand command, long companyId, int fiscalYear) {
        return buildRolesPayload(command.roles(), companyId, command.businessRegNo(), command.companyName(), fiscalYear);
    }

    private String buildUpdatedPayload(
            String businessRegNo,
            CompanyUpdateCommand command,
            long companyId,
            int fiscalYear
    ) {
        return buildRolesPayload(command.roles(), companyId, businessRegNo, command.companyName(), fiscalYear);
    }

    private String buildRolesPayload(
            List<CompanyRoleType> roles,
            long companyId,
            String businessRegNo,
            String companyName,
            int fiscalYear
    ) {
        String roleList = roles.stream()
                .map(CompanyRoleType::name)
                .reduce((a, b) -> a + "\",\"" + b)
                .orElse("");
        return """
                {"companyId":%d,"businessRegNo":"%s","companyName":"%s","roles":["%s"],"fiscalYear":%d}
                """.formatted(companyId, escape(businessRegNo), escape(companyName), roleList, fiscalYear)
                .trim();
    }

    private String escape(String value) {
        return value.replace("\\", "\\\\").replace("\"", "\\\"");
    }
}
