/**
 * 목록 검색 버튼용: draft 필터를 applied로 확정한다.
 * 얕은 복사로, 이후 draft 입력이 이미 적용된 조회 조건을 바꾸지 않게 한다.
 */
export function commitListSearchFilters<T extends object>(draft: T): T {
  return { ...draft };
}