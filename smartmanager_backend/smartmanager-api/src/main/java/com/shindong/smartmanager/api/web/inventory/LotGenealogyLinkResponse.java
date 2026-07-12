package com.shindong.smartmanager.api.web.inventory;

import com.shindong.smartmanager.application.inventory.LotGenealogyLinkView;
import com.shindong.smartmanager.domain.inventory.LotGenealogyLinkType;
import java.math.BigDecimal;
import java.time.Instant;

public record LotGenealogyLinkResponse(
        long id,
        long parentLotId,
        String parentLotNo,
        long parentItemId,
        String parentItemNo,
        String parentItemName,
        long childLotId,
        String childLotNo,
        long childItemId,
        String childItemNo,
        String childItemName,
        LotGenealogyLinkType linkType,
        String linkTypeLabel,
        BigDecimal qty,
        Long stockMovementId,
        String sourceDocType,
        Long sourceDocId,
        Instant createdAt
) {
    public static LotGenealogyLinkResponse from(LotGenealogyLinkView view) {
        return new LotGenealogyLinkResponse(
                view.id(),
                view.parentLotId(),
                view.parentLotNo(),
                view.parentItemId(),
                view.parentItemNo(),
                view.parentItemName(),
                view.childLotId(),
                view.childLotNo(),
                view.childItemId(),
                view.childItemNo(),
                view.childItemName(),
                view.linkType(),
                linkTypeLabel(view.linkType()),
                view.qty(),
                view.stockMovementId(),
                view.sourceDocType(),
                view.sourceDocId(),
                view.createdAt()
        );
    }

    private static String linkTypeLabel(LotGenealogyLinkType linkType) {
        if (linkType == null) {
            return "";
        }
        return switch (linkType) {
            case CONSUME -> "소비";
            case PRODUCE -> "산출";
            case SPLIT -> "분할";
            case MERGE -> "병합";
        };
    }
}
