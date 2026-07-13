import type { BoardType } from '../api/board';
import type {
  InventoryPageId,
  OutsourcePageId,
  ProductionPageId,
  PurchasePageId,
  QualityPageId,
  SalesPageId,
  SystemPage,
} from './menuConfig';
import type { BoardScreen } from '../pages/BoardPage';
import type { WorkDiaryScreen } from '../pages/WorkDiaryPage';

export type AppSelection =
  | { category: 'home' }
  | { category: 'board'; boardType: BoardType; screen: BoardScreen }
  | { category: 'workdiary'; screen: WorkDiaryScreen }
  | { category: 'basis' }
  | { category: 'system'; page: SystemPage }
  | { category: 'sales'; page: SalesPageId }
  | { category: 'production'; page: ProductionPageId }
  | { category: 'purchase'; page: PurchasePageId }
  | { category: 'inventory'; page: InventoryPageId }
  | { category: 'quality'; page: QualityPageId }
  | { category: 'outsource'; page: OutsourcePageId };
