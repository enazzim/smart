package com.shindong.smartmanager.application.inventory;

import java.time.LocalDate;
import java.util.List;

public interface StocktakeBookQuery {

    List<StocktakeBookLine> listInhouseNonLot(LocalDate stocktakeDate, int fiscalYear);

    /** 원자재 마스터 중 사내 원자재창고 잔고가 없는 품목. 슬롯은 만들지 않는다. */
    List<StocktakeBookLine> listRawWithoutBalance(int fiscalYear);
}
