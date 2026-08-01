export type DrawingLifecycleStage =
  | 'RECEIVED'
  | 'SAMPLE'
  | 'PARTNER_REVIEW'
  | 'MASS_PROD_READY'
  | 'ITEM_LINKED'
  | 'ARCHIVED';

export const LIFECYCLE_STAGE_LABELS: Record<DrawingLifecycleStage, string> = {
  RECEIVED: '선수신',
  SAMPLE: '샘플',
  PARTNER_REVIEW: '거래처 검토',
  MASS_PROD_READY: '양산 준비',
  ITEM_LINKED: '품목 연결',
  ARCHIVED: '보관',
};

/**
 * DEV 활성 업무 단계(수동 드롭다운).
 * 보관(ARCHIVED)은 드롭다운에 넣지 않고 「개발 중단(보관)」 버튼으로만 전환한다.
 */
export const DEV_ACTIVE_LIFECYCLE_STAGES: DrawingLifecycleStage[] = [
  'RECEIVED',
  'SAMPLE',
  'PARTNER_REVIEW',
];

/** @deprecated DEV_ACTIVE_LIFECYCLE_STAGES 사용 */
export const DEV_MANUAL_LIFECYCLE_STAGES = DEV_ACTIVE_LIFECYCLE_STAGES;

export function lifecycleStageLabel(stage?: DrawingLifecycleStage | null): string {
  if (!stage) {
    return '—';
  }
  return LIFECYCLE_STAGE_LABELS[stage] ?? stage;
}

export function lifecycleBadgeClass(stage?: DrawingLifecycleStage | null): string {
  switch (stage) {
    case 'RECEIVED':
      return 'drawing-lifecycle-badge drawing-lifecycle-badge--received';
    case 'SAMPLE':
      return 'drawing-lifecycle-badge drawing-lifecycle-badge--sample';
    case 'PARTNER_REVIEW':
      return 'drawing-lifecycle-badge drawing-lifecycle-badge--review';
    case 'MASS_PROD_READY':
      return 'drawing-lifecycle-badge drawing-lifecycle-badge--prod-ready';
    case 'ITEM_LINKED':
      return 'drawing-lifecycle-badge drawing-lifecycle-badge--linked';
    case 'ARCHIVED':
      return 'drawing-lifecycle-badge drawing-lifecycle-badge--archived';
    default:
      return 'drawing-lifecycle-badge';
  }
}

export function isArchivedLifecycle(stage?: DrawingLifecycleStage | null): boolean {
  return stage === 'ARCHIVED';
}

export function canLinkItem(
  drawingType: 'DEV' | 'PROD',
  lifecycleStage?: DrawingLifecycleStage | null,
): boolean {
  if (isArchivedLifecycle(lifecycleStage)) {
    return false;
  }
  return (
    drawingType === 'PROD' &&
    (lifecycleStage === 'MASS_PROD_READY' || lifecycleStage === 'ITEM_LINKED')
  );
}

export function canReviseDrawing(
  drawingType: 'DEV' | 'PROD',
  lifecycleStage?: DrawingLifecycleStage | null,
): boolean {
  return drawingType === 'DEV' && !isArchivedLifecycle(lifecycleStage);
}

export function canPromoteDrawing(
  drawingType: 'DEV' | 'PROD',
  lifecycleStage?: DrawingLifecycleStage | null,
): boolean {
  return drawingType === 'DEV' && !isArchivedLifecycle(lifecycleStage);
}

export function canReopenDev(
  drawingType: 'DEV' | 'PROD',
  lifecycleStage?: DrawingLifecycleStage | null,
): boolean {
  return drawingType === 'PROD' && !isArchivedLifecycle(lifecycleStage);
}

/** 활성 도면을 보관(사용 종료)으로 전환. AS·기출고 열람용으로 이력·PDF는 유지 */
export function canArchiveDrawing(
  drawingType: 'DEV' | 'PROD',
  isLatest: boolean,
  isDeleted: boolean,
  readOnly: boolean,
  lifecycleStage?: DrawingLifecycleStage | null,
): boolean {
  return (
    !readOnly &&
    !isDeleted &&
    isLatest &&
    !isArchivedLifecycle(lifecycleStage) &&
    (drawingType === 'DEV' || drawingType === 'PROD')
  );
}

/** 보관 해제(롤백) — 보관 상태에서만 */
export function canUnarchiveDrawing(
  isLatest: boolean,
  isDeleted: boolean,
  readOnly: boolean,
  lifecycleStage?: DrawingLifecycleStage | null,
): boolean {
  return !readOnly && !isDeleted && isLatest && isArchivedLifecycle(lifecycleStage);
}

/**
 * 보관 해제 시 복귀 단계.
 * - PROD + 품목 연결됨 → 품목 연결
 * - PROD + 미연결 → 양산 준비
 * - DEV → 샘플
 */
export function restoreStageAfterUnarchive(
  drawingType: 'DEV' | 'PROD',
  itemId?: number | null,
): DrawingLifecycleStage {
  if (drawingType === 'PROD') {
    return itemId != null ? 'ITEM_LINKED' : 'MASS_PROD_READY';
  }
  return 'SAMPLE';
}

export function archiveActionLabel(drawingType: 'DEV' | 'PROD'): string {
  return drawingType === 'PROD' ? '보관' : '개발 중단(보관)';
}

export function canEditLifecycleManually(
  drawingType: 'DEV' | 'PROD',
  isLatest: boolean,
  isDeleted: boolean,
  readOnly: boolean,
  lifecycleStage?: DrawingLifecycleStage | null,
): boolean {
  return (
    !readOnly &&
    !isDeleted &&
    isLatest &&
    drawingType === 'DEV' &&
    !isArchivedLifecycle(lifecycleStage)
  );
}
