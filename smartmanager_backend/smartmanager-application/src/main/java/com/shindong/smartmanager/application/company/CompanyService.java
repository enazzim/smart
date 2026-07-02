package com.shindong.smartmanager.application.company;

import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.domain.company.CompanyRoleType;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import java.time.Year;
import java.util.List;

public class CompanyService {

    private final CompanyRepository companyRepository;
    private final PartnerLedgerProjector partnerLedgerProjector;
    private final DomainEventStore domainEventStore;

    public CompanyService(
            CompanyRepository companyRepository,
            PartnerLedgerProjector partnerLedgerProjector,
            DomainEventStore domainEventStore
    ) {
        this.companyRepository = companyRepository;
        this.partnerLedgerProjector = partnerLedgerProjector;
        this.domainEventStore = domainEventStore;
    }

    public CompanyView register(CompanyCommand command, String actorUserId) {
        validateRegister(command);
        if (companyRepository.existsActiveByBusinessRegNo(command.businessRegNo())) {
            throw new IllegalArgumentException("이미 등록된 사업자번호입니다: " + command.businessRegNo());
        }

        long companyId = companyRepository.save(command, actorUserId);
        companyRepository.replaceRoles(companyId, command.roles());

        int fiscalYear = Year.now().getValue();
        partnerLedgerProjector.ensureAccounts(companyId, command.roles(), fiscalYear, actorUserId);

        domainEventStore.append(DomainEvent.create(
                EventTypes.COMPANY_REGISTERED,
                1,
                AggregateTypes.COMPANY,
                String.valueOf(companyId),
                actorUserId,
                buildRegisteredPayload(command, companyId, fiscalYear)
        ));

        return companyRepository.findActiveById(companyId)
                .orElseThrow(() -> new IllegalStateException("등록 직후 거래처를 조회할 수 없습니다."));
    }

    public List<CompanyView> listActive() {
        return companyRepository.findAllActive();
    }

    public CompanyView getActive(long id) {
        return companyRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("거래처를 찾을 수 없습니다: " + id));
    }

    private void validateRegister(CompanyCommand command) {
        if (command.roles() == null || command.roles().isEmpty()) {
            throw new IllegalArgumentException("거래처 역할은 최소 1개 이상 선택해야 합니다.");
        }
        if (command.companyName() == null || command.companyName().isBlank()) {
            throw new IllegalArgumentException("상호는 필수입니다.");
        }
        if (command.presidentName() == null || command.presidentName().isBlank()) {
            throw new IllegalArgumentException("대표자는 필수입니다.");
        }
        if (command.businessRegNo() == null || command.businessRegNo().isBlank()) {
            throw new IllegalArgumentException("사업자번호는 필수입니다.");
        }
        if (command.businessAddress() == null || command.businessAddress().isBlank()) {
            throw new IllegalArgumentException("사업장주소는 필수입니다.");
        }
    }

    private String buildRegisteredPayload(CompanyCommand command, long companyId, int fiscalYear) {
        String roles = command.roles().stream()
                .map(CompanyRoleType::name)
                .reduce((a, b) -> a + "\",\"" + b)
                .orElse("");
        return """
                {"companyId":%d,"businessRegNo":"%s","companyName":"%s","roles":["%s"],"fiscalYear":%d}
                """.formatted(companyId, escape(command.businessRegNo()), escape(command.companyName()), roles, fiscalYear)
                .trim();
    }

    private String escape(String value) {
        return value.replace("\\", "\\\\").replace("\"", "\\\"");
    }
}
