package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.FiscalPeriod;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.domain.company.CompanyRoleType;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.List;

public class EtcClaimService {

    private final EtcClaimRepository etcClaimRepository;
    private final CompanyRepository companyRepository;
    private final MonthClosingService monthClosingService;
    private final FiscalCalendarService fiscalCalendarService;

    public EtcClaimService(
            EtcClaimRepository etcClaimRepository,
            CompanyRepository companyRepository,
            MonthClosingService monthClosingService,
            FiscalCalendarService fiscalCalendarService
    ) {
        this.etcClaimRepository = etcClaimRepository;
        this.companyRepository = companyRepository;
        this.monthClosingService = monthClosingService;
        this.fiscalCalendarService = fiscalCalendarService;
    }

    public List<EtcClaimView> list(EtcClaimListCriteria criteria) {
        return etcClaimRepository.findActive(criteria);
    }

    public EtcClaimView get(long id) {
        return etcClaimRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("기타공제를 찾을 수 없습니다: " + id));
    }

    public EtcClaimView register(EtcClaimCommand command, String actorUserId) {
        EtcClaimCommand normalized = validate(command, null);
        FiscalPeriod period = fiscalCalendarService.resolvePeriod(normalized.receiptDate());
        long id = etcClaimRepository.save(
                normalized, period.fiscalYear(), period.fiscalMonth(), actorUserId);
        return get(id);
    }

    public EtcClaimView update(long id, EtcClaimCommand command, String actorUserId) {
        EtcClaimView existing = get(id);
        if (!existing.editable()) {
            throw new IllegalArgumentException("승인된 기타공제는 수정할 수 없습니다.");
        }
        EtcClaimCommand normalized = validate(command, existing);
        FiscalPeriod period = fiscalCalendarService.resolvePeriod(normalized.receiptDate());
        etcClaimRepository.update(
                id, normalized, period.fiscalYear(), period.fiscalMonth(), actorUserId);
        return get(id);
    }

    public void delete(long id, String actorUserId) {
        EtcClaimView existing = get(id);
        if (!existing.editable()) {
            throw new IllegalArgumentException("승인된 기타공제는 삭제할 수 없습니다.");
        }
        monthClosingService.assertTransactionOpen(existing.receiptDate());
        etcClaimRepository.softDelete(id, actorUserId);
    }

    private EtcClaimCommand validate(EtcClaimCommand command, EtcClaimView existing) {
        if (command.partnerId() <= 0) {
            throw new IllegalArgumentException("거래처를 선택해 주세요.");
        }
        companyRepository.findActiveById(command.partnerId())
                .orElseThrow(() -> new IllegalArgumentException("거래처를 찾을 수 없습니다: " + command.partnerId()));

        List<CompanyRoleType> roles = companyRepository.findRoles(command.partnerId());
        boolean allowed = roles.contains(CompanyRoleType.PURCHASE) || roles.contains(CompanyRoleType.OUTSOURCE);
        if (!allowed) {
            throw new IllegalArgumentException("구매 또는 외주 거래처만 기타공제를 등록할 수 있습니다.");
        }

        if (command.receiptDate() == null) {
            throw new IllegalArgumentException("접수일은 필수입니다.");
        }
        monthClosingService.assertTransactionOpen(command.receiptDate());
        if (existing != null && !existing.receiptDate().equals(command.receiptDate())) {
            monthClosingService.assertTransactionOpen(existing.receiptDate());
        }

        String reason = command.reason() == null ? "" : command.reason().trim();
        if (reason.isEmpty()) {
            throw new IllegalArgumentException("공제사유를 입력해 주세요.");
        }
        if (reason.length() > 500) {
            throw new IllegalArgumentException("공제사유는 500자 이하여야 합니다.");
        }

        if (command.amount() == null) {
            throw new IllegalArgumentException("금액은 필수입니다.");
        }
        BigDecimal amount = command.amount().setScale(2, RoundingMode.HALF_UP);
        if (amount.compareTo(BigDecimal.ZERO) <= 0) {
            throw new IllegalArgumentException("금액은 0보다 커야 합니다.");
        }

        return new EtcClaimCommand(command.partnerId(), command.receiptDate(), reason, amount);
    }
}
