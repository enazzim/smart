package com.shindong.smartmanager.application.drawing;

import com.shindong.smartmanager.application.bom.BomTreeNode;
import com.shindong.smartmanager.application.bom.ItemCompositionService;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.domain.drawing.DrawingType;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.Objects;
import java.util.Set;

public class DrawingReferenceService {

    private static final Set<String> ALLOWED_ROLES = Set.of("COMPONENT", "RELATED", "SPEC");

    private final DrawingRepository drawingRepository;
    private final DrawingReferenceRepository drawingReferenceRepository;
    private final ItemCompositionService itemCompositionService;
    private final ItemRepository itemRepository;

    public DrawingReferenceService(
            DrawingRepository drawingRepository,
            DrawingReferenceRepository drawingReferenceRepository,
            ItemCompositionService itemCompositionService,
            ItemRepository itemRepository
    ) {
        this.drawingRepository = drawingRepository;
        this.drawingReferenceRepository = drawingReferenceRepository;
        this.itemCompositionService = itemCompositionService;
        this.itemRepository = itemRepository;
    }

    public List<DrawingReferenceView> listContains(String masterId, String historyId) {
        assertHistoryBelongsToMaster(masterId, historyId);
        return drawingReferenceRepository.findActiveByParentHistoryId(historyId);
    }

    public List<DrawingWhereUsedView> listWhereUsed(String historyId) {
        drawingRepository.findHistoryById(historyId)
                .orElseThrow(() -> new IllegalArgumentException("이력을 찾을 수 없습니다."));
        return drawingReferenceRepository.findActiveByChildHistoryId(historyId);
    }

    public void replaceReferences(
            String masterId,
            String historyId,
            List<DrawingReferenceChildCommand> children,
            String actorUserId
    ) {
        DrawingHistoryDetailView parent = assertHistoryBelongsToMaster(masterId, historyId);
        if (!"Y".equalsIgnoreCase(parent.isLatest())) {
            throw new IllegalArgumentException("최신 이력의 참조만 수정할 수 있습니다.");
        }

        List<DrawingReferenceChildCommand> commands = children == null ? List.of() : children;
        Set<String> seenChildren = new HashSet<>();
        for (DrawingReferenceChildCommand command : commands) {
            validateChildCommand(parent, command, seenChildren);
        }

        drawingReferenceRepository.deleteAllByParentHistoryId(historyId);
        int index = 0;
        for (DrawingReferenceChildCommand command : commands) {
            String role = normalizeRole(command.refRole());
            int sortOrder = command.sortOrder() > 0 ? command.sortOrder() : index + 1;
            drawingReferenceRepository.insertReference(
                    historyId,
                    command.childHistoryId().trim(),
                    role,
                    sortOrder,
                    blankToNull(command.remark()),
                    actorUserId
            );
            index++;
        }
    }

    public void copySnapshot(String fromParentHistoryId, String toParentHistoryId, String actorUserId) {
        drawingReferenceRepository.copyActiveReferences(fromParentHistoryId, toParentHistoryId, actorUserId);
    }

    public void assertProdChildrenForPromote(String parentHistoryId) {
        List<DrawingReferenceView> refs = drawingReferenceRepository.findActiveByParentHistoryId(parentHistoryId);
        for (DrawingReferenceView ref : refs) {
            if (ref.child().drawingType() != DrawingType.PROD) {
                throw new IllegalStateException(
                        "양산 이관 전 구성 참조의 자식 도면이 모두 PROD여야 합니다. 품번="
                                + ref.child().partNo()
                                + " (" + ref.child().drawingType() + ")");
            }
        }
    }

    public void deleteReferencesForHistories(java.util.Collection<String> historyIds) {
        drawingReferenceRepository.deleteByHistoryIds(historyIds);
    }

    /**
     * 활성 latest 도면들의 구성 참조 정합 스캔 (DR-4 리포트).
     */
    public List<DrawingReferenceIntegrityIssue> scanIntegrity() {
        List<DrawingReferenceIntegrityIssue> issues = new ArrayList<>();
        Map<String, DrawingHistoryDetailView> latestByHistoryId = new LinkedHashMap<>();

        for (DrawingListView row : drawingRepository.findLatestActiveDrawings()) {
            DrawingHistoryDetailView latest = drawingRepository.findLatestActiveHistoryByPartNo(row.partNo())
                    .orElse(null);
            if (latest == null) {
                continue;
            }
            latestByHistoryId.put(latest.id(), latest);

            for (DrawingReferenceView ref : drawingReferenceRepository.findActiveByParentHistoryId(latest.id())) {
                DrawingHistoryDetailView child = drawingRepository.findHistoryById(ref.childHistoryId()).orElse(null);
                if (child == null) {
                    issues.add(new DrawingReferenceIntegrityIssue(
                            "MISSING_CHILD",
                            latest.partNo(),
                            latest.id(),
                            ref.child().partNo(),
                            ref.childHistoryId(),
                            "참조된 자식 이력이 없습니다."
                    ));
                    continue;
                }
                if (child.masterRecordingState() != 1) {
                    issues.add(new DrawingReferenceIntegrityIssue(
                            "DELETED_CHILD",
                            latest.partNo(),
                            latest.id(),
                            child.partNo(),
                            child.id(),
                            "삭제된 도면을 참조합니다."
                    ));
                }
                if (latest.drawingType() == DrawingType.PROD && child.drawingType() != DrawingType.PROD) {
                    issues.add(new DrawingReferenceIntegrityIssue(
                            "PROD_CHILD_NOT_PROD",
                            latest.partNo(),
                            latest.id(),
                            child.partNo(),
                            child.id(),
                            "PROD 도면이 DEV(또는 비-PROD) 이력을 참조합니다."
                    ));
                }
            }
        }

        Set<String> visitedGlobal = new HashSet<>();
        for (String historyId : latestByHistoryId.keySet()) {
            if (!visitedGlobal.add(historyId)) {
                continue;
            }
            Set<String> stack = new HashSet<>();
            List<String> path = new ArrayList<>();
            detectCycle(historyId, stack, path, visitedGlobal, latestByHistoryId, issues);
        }
        return issues;
    }

    /**
     * 구성 추가 후보: 연결 품목 BOM 하위(루트 제외) 중 도면이 있는 것만.
     */
    public List<DrawingReferenceCandidateView> listBomCandidates(String masterId) {
        if (itemCompositionService == null || itemRepository == null) {
            throw new IllegalStateException("BOM 후보 조회가 구성되지 않았습니다.");
        }
        DrawingMasterView master = drawingRepository.findMasterById(masterId)
                .orElseThrow(() -> new IllegalArgumentException("도면을 찾을 수 없습니다."));
        if (master.itemId() == null) {
            return List.of();
        }
        ItemView rootItem = itemRepository.findActiveById(master.itemId())
                .orElseThrow(() -> new IllegalArgumentException("연결 품목을 찾을 수 없습니다."));

        BomTreeNode tree = itemCompositionService.explode(rootItem.itemNo());
        Map<String, Integer> itemNoToLevel = new LinkedHashMap<>();
        collectDescendants(tree, itemNoToLevel);

        if (itemNoToLevel.isEmpty()) {
            return List.of();
        }

        Map<Long, Integer> itemIdToLevel = new HashMap<>();
        for (Map.Entry<String, Integer> entry : itemNoToLevel.entrySet()) {
            itemRepository.findActiveByItemNo(entry.getKey()).ifPresent(item ->
                    itemIdToLevel.put(item.id(), entry.getValue()));
        }

        List<DrawingReferenceCandidateView> candidates = new ArrayList<>();
        for (DrawingListView drawing : drawingRepository.findLatestActiveDrawings()) {
            if (drawing.id().equals(masterId) || drawing.itemId() == null) {
                continue;
            }
            Integer level = itemIdToLevel.get(drawing.itemId());
            if (level == null) {
                continue;
            }
            DrawingHistoryDetailView latest = drawingRepository.findLatestActiveHistoryByPartNo(drawing.partNo())
                    .orElse(null);
            if (latest == null) {
                continue;
            }
            candidates.add(new DrawingReferenceCandidateView(
                    drawing.id(),
                    latest.id(),
                    drawing.partNo(),
                    drawing.partName(),
                    latest.drawingType(),
                    latest.majorVersion(),
                    latest.minorVersion(),
                    drawing.itemId(),
                    drawing.itemNo(),
                    level
            ));
        }
        candidates.sort((a, b) -> {
            int byLevel = Integer.compare(a.bomLevel(), b.bomLevel());
            if (byLevel != 0) {
                return byLevel;
            }
            return a.partNo().compareToIgnoreCase(b.partNo());
        });
        return candidates;
    }

    private void collectDescendants(BomTreeNode node, Map<String, Integer> itemNoToLevel) {
        for (BomTreeNode child : node.children()) {
            itemNoToLevel.putIfAbsent(child.itemNum(), child.level());
            collectDescendants(child, itemNoToLevel);
        }
    }

    private void detectCycle(
            String current,
            Set<String> stack,
            List<String> path,
            Set<String> visitedGlobal,
            Map<String, DrawingHistoryDetailView> latestByHistoryId,
            List<DrawingReferenceIntegrityIssue> issues
    ) {
        if (stack.contains(current)) {
            DrawingHistoryDetailView cur = latestByHistoryId.get(current);
            issues.add(new DrawingReferenceIntegrityIssue(
                    "CYCLE",
                    cur != null ? cur.partNo() : current,
                    current,
                    null,
                    null,
                    "구성 참조 순환이 감지되었습니다: " + String.join(" → ", path) + " → "
                            + (cur != null ? cur.partNo() : current)
            ));
            return;
        }
        stack.add(current);
        path.add(latestByHistoryId.containsKey(current) ? latestByHistoryId.get(current).partNo() : current);
        visitedGlobal.add(current);

        for (String childId : drawingReferenceRepository.findActiveChildHistoryIds(current)) {
            detectCycle(childId, stack, path, visitedGlobal, latestByHistoryId, issues);
        }

        stack.remove(current);
        path.remove(path.size() - 1);
    }

    private void validateChildCommand(
            DrawingHistoryDetailView parent,
            DrawingReferenceChildCommand command,
            Set<String> seenChildren
    ) {
        if (command == null || command.childHistoryId() == null || command.childHistoryId().isBlank()) {
            throw new IllegalArgumentException("자식 이력 ID는 필수입니다.");
        }
        String childHistoryId = command.childHistoryId().trim();
        if (!seenChildren.add(childHistoryId)) {
            throw new IllegalArgumentException("동일한 자식 이력이 중복되었습니다.");
        }

        DrawingHistoryDetailView child = drawingRepository.findHistoryById(childHistoryId)
                .orElseThrow(() -> new IllegalArgumentException("자식 도면 이력을 찾을 수 없습니다: " + childHistoryId));
        if (child.masterRecordingState() != 1) {
            throw new IllegalArgumentException("삭제된 도면은 참조할 수 없습니다: " + child.partNo());
        }
        if (Objects.equals(parent.masterId(), child.masterId())) {
            throw new IllegalArgumentException("동일 도면 마스터를 자기 참조할 수 없습니다.");
        }
        if (parent.drawingType() == DrawingType.PROD && child.drawingType() != DrawingType.PROD) {
            throw new IllegalArgumentException(
                    "PROD 도면의 구성 참조는 PROD 이력만 허용합니다. 품번=" + child.partNo());
        }
        normalizeRole(command.refRole());
        assertNoCycle(parent.id(), childHistoryId);
    }

    private void assertNoCycle(String parentHistoryId, String childHistoryId) {
        Set<String> visited = new HashSet<>();
        if (reaches(childHistoryId, parentHistoryId, visited)) {
            throw new IllegalArgumentException("순환 참조가 발생합니다.");
        }
    }

    private boolean reaches(String currentHistoryId, String targetHistoryId, Set<String> visited) {
        if (Objects.equals(currentHistoryId, targetHistoryId)) {
            return true;
        }
        if (!visited.add(currentHistoryId)) {
            return false;
        }
        for (String next : drawingReferenceRepository.findActiveChildHistoryIds(currentHistoryId)) {
            if (reaches(next, targetHistoryId, visited)) {
                return true;
            }
        }
        return false;
    }

    private DrawingHistoryDetailView assertHistoryBelongsToMaster(String masterId, String historyId) {
        drawingRepository.findMasterById(masterId)
                .orElseThrow(() -> new IllegalArgumentException("도면을 찾을 수 없습니다."));
        DrawingHistoryDetailView history = drawingRepository.findHistoryById(historyId)
                .orElseThrow(() -> new IllegalArgumentException("이력을 찾을 수 없습니다."));
        if (!Objects.equals(masterId, history.masterId())) {
            throw new IllegalArgumentException("이력이 해당 도면에 속하지 않습니다.");
        }
        return history;
    }

    private static String normalizeRole(String refRole) {
        String role = (refRole == null || refRole.isBlank()) ? "COMPONENT" : refRole.trim().toUpperCase(Locale.ROOT);
        if (!ALLOWED_ROLES.contains(role)) {
            throw new IllegalArgumentException("지원하지 않는 참조 역할입니다: " + refRole);
        }
        return role;
    }

    private static String blankToNull(String value) {
        if (value == null || value.isBlank()) {
            return null;
        }
        return value.trim();
    }
}
