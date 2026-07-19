package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.FiscalPeriod;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.domain.company.CompanyRoleType;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.List;

public class DefectClaimService {

    private final DefectClaimRepository defectClaimRepository;
    private final CompanyRepository companyRepository;
    private final ItemRepository itemRepository;
    private final MonthClosingService monthClosingService;
    private final FiscalCalendarService fiscalCalendarService;

    public DefectClaimService(
            DefectClaimRepository defectClaimRepository,
            CompanyRepository companyRepository,
            ItemRepository itemRepository,
            MonthClosingService monthClosingService,
            FiscalCalendarService fiscalCalendarService
    ) {
        this.defectClaimRepository = defectClaimRepository;
        this.companyRepository = companyRepository;
        this.itemRepository = itemRepository;
        this.monthClosingService = monthClosingService;
        this.fiscalCalendarService = fiscalCalendarService;
    }

    public List<DefectClaimView> list(DefectClaimListCriteria criteria) {
        return defectClaimRepository.findActive(criteria);
    }

    public DefectClaimView get(long id) {
        return defectClaimRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("불량변상을 찾을 수 없습니다: " + id));
    }

    public DefectClaimView register(DefectClaimCommand command, String actorUserId) {
        DefectClaimCommand normalized = validate(command, null);
        FiscalPeriod period = fiscalCalendarService.resolvePeriod(normalized.receiptDate());
        long id = defectClaimRepository.save(
                normalized, period.fiscalYear(), period.fiscalMonth(), actorUserId);
        return get(id);
    }

    public DefectClaimView update(long id, DefectClaimCommand command, String actorUserId) {
        DefectClaimView existing = get(id);
        if (!existing.editable()) {
            throw new IllegalArgumentException("승인된 불량변상은 수정할 수 없습니다.");
        }
        DefectClaimCommand normalized = validate(command, existing);
        FiscalPeriod period = fiscalCalendarService.resolvePeriod(normalized.receiptDate());
        defectClaimRepository.update(
                id, normalized, period.fiscalYear(), period.fiscalMonth(), actorUserId);
        return get(id);
    }

    public void delete(long id, String actorUserId) {
        DefectClaimView existing = get(id);
        if (!existing.editable()) {
            throw new IllegalArgumentException("승인된 불량변상은 삭제할 수 없습니다.");
        }
        monthClosingService.assertTransactionOpen(existing.receiptDate());
        defectClaimRepository.softDelete(id, actorUserId);
    }

    private DefectClaimCommand validate(DefectClaimCommand command, DefectClaimView existing) {
        if (command.partnerId() <= 0) {
            throw new IllegalArgumentException("거래처를 선택해 주세요.");
        }
        companyRepository.findActiveById(command.partnerId())
                .orElseThrow(() -> new IllegalArgumentException("거래처를 찾을 수 없습니다: " + command.partnerId()));

        List<CompanyRoleType> roles = companyRepository.findRoles(command.partnerId());
        boolean allowed = roles.contains(CompanyRoleType.PURCHASE) || roles.contains(CompanyRoleType.OUTSOURCE);
        if (!allowed) {
            throw new IllegalArgumentException("구매 또는 외주 거래처만 불량변상을 등록할 수 있습니다.");
        }

        if (command.itemId() <= 0) {
            throw new IllegalArgumentException("품목을 선택해 주세요.");
        }
        itemRepository.findActiveById(command.itemId())
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + command.itemId()));

        if (command.receiptDate() == null) {
            throw new IllegalArgumentException("변상등록일은 필수입니다.");
        }
        monthClosingService.assertTransactionOpen(command.receiptDate());
        if (existing != null && !existing.receiptDate().equals(command.receiptDate())) {
            monthClosingService.assertTransactionOpen(existing.receiptDate());
        }

        String reason = command.reason() == null ? "" : command.reason().trim();
        if (reason.isEmpty()) {
            throw new IllegalArgumentException("불량변상 이유를 입력해 주세요.");
        }
        if (reason.length() > 500) {
            throw new IllegalArgumentException("불량변상 이유는 500자 이하여야 합니다.");
        }

        if (command.claimQty() == null) {
            throw new IllegalArgumentException("수량은 필수입니다.");
        }
        BigDecimal claimQty = command.claimQty().setScale(4, RoundingMode.HALF_UP);
        if (claimQty.compareTo(BigDecimal.ZERO) <= 0) {
            throw new IllegalArgumentException("수량은 0보다 커야 합니다.");
        }

        if (command.amount() == null) {
            throw new IllegalArgumentException("금액은 필수입니다.");
        }
        BigDecimal amount = command.amount().setScale(2, RoundingMode.HALF_UP);
        if (amount.compareTo(BigDecimal.ZERO) <= 0) {
            throw new IllegalArgumentException("금액은 0보다 커야 합니다.");
        }

        return new DefectClaimCommand(
                command.partnerId(),
                command.itemId(),
                command.receiptDate(),
                claimQty,
                amount,
                reason
        );
    }
}
