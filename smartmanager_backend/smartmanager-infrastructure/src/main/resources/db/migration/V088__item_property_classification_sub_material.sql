-- 자산구분: 부자재 추가 (창고 재고 관리 대상 아님, 구매단가·발주·입고만 사용)
ALTER TABLE item
  MODIFY COLUMN property_classification ENUM('원자재','제품','상품','공정품','부자재') NOT NULL;
