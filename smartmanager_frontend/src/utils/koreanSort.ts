/**
 * 검색조건 콤보·셀렉트 바인딩용 한글 정렬.
 * localeCompare('ko') — 숫자 포함 명칭도 자연 정렬.
 */
export function compareKorean(a: string | null | undefined, b: string | null | undefined): number {
  return String(a ?? '').localeCompare(String(b ?? ''), 'ko', {
    sensitivity: 'base',
    numeric: true,
  });
}

export function sortByKoreanField<T>(items: readonly T[], getLabel: (item: T) => string | null | undefined): T[] {
  return [...items].sort((left, right) => compareKorean(getLabel(left), getLabel(right)));
}
