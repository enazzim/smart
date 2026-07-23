-- 자산분류: 팬텀 추가 (BOM 구성용, 일괄등록 전용 — 화면 등록 UI에서는 비노출)
ALTER TABLE item
  MODIFY COLUMN property_classification ENUM('원자재','제품','상품','공정품','부자재','팬텀') NOT NULL;
