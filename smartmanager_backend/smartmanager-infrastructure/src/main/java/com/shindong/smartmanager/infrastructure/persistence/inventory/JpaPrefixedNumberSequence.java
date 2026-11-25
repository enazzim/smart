package com.shindong.smartmanager.infrastructure.persistence.inventory;

import com.shindong.smartmanager.application.inventory.NumberSequenceKind;
import com.shindong.smartmanager.application.inventory.PrefixedNumberSequence;
import org.springframework.dao.DataIntegrityViolationException;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaPrefixedNumberSequence implements PrefixedNumberSequence {

    private final SpringDataMiscMovementNoSequenceRepository miscSequences;
    private final SpringDataStocktakeBatchNoSequenceRepository stocktakeSequences;

    public JpaPrefixedNumberSequence(
            SpringDataMiscMovementNoSequenceRepository miscSequences,
            SpringDataStocktakeBatchNoSequenceRepository stocktakeSequences
    ) {
        this.miscSequences = miscSequences;
        this.stocktakeSequences = stocktakeSequences;
    }

    @Override
    @Transactional
    public int next(NumberSequenceKind kind, String prefix, int existingMax) {
        if (kind == NumberSequenceKind.MISC_MOVEMENT) {
            return nextMisc(prefix, existingMax);
        }
        return nextStocktake(prefix, existingMax);
    }

    private int nextMisc(String prefix, int existingMax) {
        var locked = miscSequences.findForUpdate(prefix);
        if (locked.isPresent()) {
            int next = locked.get().getLastSeq() + 1;
            locked.get().setLastSeq(next);
            miscSequences.save(locked.get());
            return next;
        }
        int issued = existingMax + 1;
        try {
            miscSequences.saveAndFlush(new MiscMovementNoSequenceJpaEntity(prefix, issued));
            return issued;
        } catch (DataIntegrityViolationException ex) {
            var raced = miscSequences.findForUpdate(prefix).orElseThrow(
                    () -> new IllegalStateException("기타 입출고 번호를 발급할 수 없습니다.")
            );
            int next = raced.getLastSeq() + 1;
            raced.setLastSeq(next);
            miscSequences.save(raced);
            return next;
        }
    }

    private int nextStocktake(String prefix, int existingMax) {
        var locked = stocktakeSequences.findForUpdate(prefix);
        if (locked.isPresent()) {
            int next = locked.get().getLastSeq() + 1;
            locked.get().setLastSeq(next);
            stocktakeSequences.save(locked.get());
            return next;
        }
        int issued = existingMax + 1;
        try {
            stocktakeSequences.saveAndFlush(new StocktakeBatchNoSequenceJpaEntity(prefix, issued));
            return issued;
        } catch (DataIntegrityViolationException ex) {
            var raced = stocktakeSequences.findForUpdate(prefix).orElseThrow(
                    () -> new IllegalStateException("실사 배치 번호를 발급할 수 없습니다.")
            );
            int next = raced.getLastSeq() + 1;
            raced.setLastSeq(next);
            stocktakeSequences.save(raced);
            return next;
        }
    }
}
