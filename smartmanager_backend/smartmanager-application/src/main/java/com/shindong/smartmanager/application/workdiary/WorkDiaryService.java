package com.shindong.smartmanager.application.workdiary;

import com.shindong.smartmanager.application.code.CodeGroupOptionsRepository;
import com.shindong.smartmanager.application.code.CodeOptionView;
import com.shindong.smartmanager.application.publiccode.CreateSmallCommand;
import com.shindong.smartmanager.application.publiccode.PublicCodeLargeView;
import com.shindong.smartmanager.application.publiccode.PublicCodeRepository;
import com.shindong.smartmanager.application.publiccode.PublicCodeSmallView;
import com.shindong.smartmanager.application.user.UserRepository;
import java.time.Instant;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.util.Comparator;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import java.util.stream.Collectors;

public class WorkDiaryService {

    private static final String WORK_DIARY_GROUP = "WORK_DIARY_GROUP";
    private static final String WORK_DIARY_GROUP_LARGE_CODE = "1900";
    private static final Pattern GROUP_NAME_PATTERN = Pattern.compile("^(\\d+)\\s*그룹$");

    private final WorkDiaryRepository workDiaryRepository;
    private final UserRepository userRepository;
    private final CodeGroupOptionsRepository codeGroupOptionsRepository;
    private final PublicCodeRepository publicCodeRepository;
    private final WorkDiaryApproverPolicy workDiaryApproverPolicy;

    public WorkDiaryService(
            WorkDiaryRepository workDiaryRepository,
            UserRepository userRepository,
            CodeGroupOptionsRepository codeGroupOptionsRepository,
            PublicCodeRepository publicCodeRepository,
            WorkDiaryApproverPolicy workDiaryApproverPolicy
    ) {
        this.workDiaryRepository = workDiaryRepository;
        this.userRepository = userRepository;
        this.codeGroupOptionsRepository = codeGroupOptionsRepository;
        this.publicCodeRepository = publicCodeRepository;
        this.workDiaryApproverPolicy = workDiaryApproverPolicy;
    }

    public WorkDiaryTemplateView getMyTemplate(long userId) {
        var user = userRepository.findActiveById(userId)
                .orElseThrow(() -> new IllegalArgumentException("사용자를 찾을 수 없습니다: " + userId));
        Long groupId = user.workDiaryGroupId();
        if (groupId == null) {
            throw new IllegalStateException("업무일지 그룹이 지정되지 않았습니다.");
        }
        var template = workDiaryRepository.findActiveTemplateByGroupId(groupId)
                .orElseThrow(() -> new IllegalArgumentException("업무일지 템플릿을 찾을 수 없습니다. groupId=" + groupId));
        return new WorkDiaryTemplateView(
                template.templateCode(),
                template.workDiaryGroupId(),
                user.workDiaryGroupName(),
                template.templateName(),
                WorkDiaryFieldSchemaSupport.forWriter(template.fieldSchema())
        );
    }

    public List<WorkDiaryTemplateView> listTemplates() {
        Map<Long, String> groupNames = codeGroupOptionsRepository.findActiveOptions(WORK_DIARY_GROUP).stream()
                .collect(Collectors.toMap(CodeOptionView::id, CodeOptionView::name, (a, b) -> a));
        return workDiaryRepository.findAllActiveTemplates().stream()
                .map(template -> new WorkDiaryTemplateView(
                        template.templateCode(),
                        template.workDiaryGroupId(),
                        groupNames.getOrDefault(template.workDiaryGroupId(), ""),
                        template.templateName(),
                        WorkDiaryFieldSchemaSupport.forWriter(template.fieldSchema())
                ))
                .sorted(Comparator.comparing(WorkDiaryTemplateView::templateCode))
                .toList();
    }

    public WorkDiaryTemplateView updateTemplate(UpdateWorkDiaryTemplateCommand command) {
        requireSystemAdmin(command.actorUserId());
        var template = workDiaryRepository.findActiveTemplateByGroupId(command.workDiaryGroupId())
                .orElseThrow(() -> new IllegalArgumentException(
                        "업무일지 템플릿을 찾을 수 없습니다. groupId=" + command.workDiaryGroupId()));
        Map<String, Object> fieldSchema = WorkDiaryFieldSchemaSupport.buildFieldSchema(command.fields());
        workDiaryRepository.updateTemplate(
                command.workDiaryGroupId(),
                command.templateName().trim(),
                fieldSchema,
                command.actorLoginId(),
                command.actorUserIdText()
        );
        Map<Long, String> groupNames = codeGroupOptionsRepository.findActiveOptions(WORK_DIARY_GROUP).stream()
                .collect(Collectors.toMap(CodeOptionView::id, CodeOptionView::name, (a, b) -> a));
        return new WorkDiaryTemplateView(
                template.templateCode(),
                template.workDiaryGroupId(),
                groupNames.getOrDefault(template.workDiaryGroupId(), ""),
                command.templateName().trim(),
                WorkDiaryFieldSchemaSupport.forWriter(fieldSchema)
        );
    }

    public WorkDiaryTemplateView createGroup(long actorUserId, String actorUserIdText) {
        requireSystemAdmin(actorUserId);
        List<CodeOptionView> groups = codeGroupOptionsRepository.findActiveOptions(WORK_DIARY_GROUP);
        int nextNumber = nextGroupNumber(groups);
        if (nextNumber > 99) {
            throw new IllegalStateException("그룹 번호는 99까지 생성할 수 있습니다.");
        }
        String groupName = String.format("%02d 그룹", nextNumber);
        String templateCode = String.format("%02d", nextNumber);
        String smallCode = nextSmallCode(groups);

        PublicCodeLargeView header = publicCodeRepository.findActiveLargeHeader(WORK_DIARY_GROUP_LARGE_CODE)
                .orElseThrow(() -> new IllegalStateException("업무일지그룹 대분류(1900)를 찾을 수 없습니다."));
        if (!WORK_DIARY_GROUP.equals(header.usageType())) {
            throw new IllegalStateException("업무일지그룹 대분류 usageType이 올바르지 않습니다.");
        }
        if (publicCodeRepository.existsActiveSmall(WORK_DIARY_GROUP_LARGE_CODE, smallCode)) {
            throw new IllegalStateException("이미 존재하는 그룹 코드입니다: " + smallCode);
        }
        if (publicCodeRepository.existsActiveSmallByName(WORK_DIARY_GROUP_LARGE_CODE, groupName, null)) {
            throw new IllegalStateException("이미 존재하는 그룹명입니다: " + groupName);
        }

        long groupId = publicCodeRepository.saveSmall(
                new CreateSmallCommand(WORK_DIARY_GROUP_LARGE_CODE, smallCode, groupName),
                header,
                actorUserIdText
        );

        Map<String, Object> emptySchema = new LinkedHashMap<>();
        emptySchema.put("version", WorkDiaryFieldSchemaSupport.SCHEMA_VERSION);
        emptySchema.put("fields", List.of());

        var created = workDiaryRepository.createTemplate(
                templateCode,
                groupId,
                "업무일지 " + groupName,
                emptySchema,
                actorUserIdText
        );
        return new WorkDiaryTemplateView(
                created.templateCode(),
                created.workDiaryGroupId(),
                groupName,
                created.templateName(),
                WorkDiaryFieldSchemaSupport.forWriter(created.fieldSchema())
        );
    }

    public void deleteGroup(long workDiaryGroupId, long actorUserId, String actorUserIdText) {
        requireSystemAdmin(actorUserId);
        PublicCodeSmallView group = publicCodeRepository.findActiveSmallById(workDiaryGroupId)
                .orElseThrow(() -> new IllegalArgumentException("업무일지 그룹을 찾을 수 없습니다: " + workDiaryGroupId));
        if (!WORK_DIARY_GROUP.equals(group.usageType())) {
            throw new IllegalArgumentException("업무일지 그룹이 아닙니다: " + workDiaryGroupId);
        }

        String label = "「" + group.smallName() + "」";
        if (userRepository.existsActiveByWorkDiaryGroupId(workDiaryGroupId)) {
            throw new IllegalStateException(label + "은(는) 사용자에서 사용 중이라 삭제할 수 없습니다.");
        }
        if (workDiaryRepository.existsActiveEntryByGroupId(workDiaryGroupId)) {
            throw new IllegalStateException(label + "은(는) 업무일지에서 사용 중이라 삭제할 수 없습니다.");
        }

        workDiaryRepository.softDeleteTemplateByGroupId(workDiaryGroupId, actorUserIdText);
        publicCodeRepository.softDeleteSmall(workDiaryGroupId, actorUserIdText);
    }

    private void requireSystemAdmin(long actorUserId) {
        var user = userRepository.findActiveById(actorUserId)
                .orElseThrow(() -> new IllegalArgumentException("사용자를 찾을 수 없습니다: " + actorUserId));
        if (!user.roleCodes().contains("SYSTEM_ADMIN")) {
            throw new IllegalStateException("업무일지 그룹/양식은 시스템 관리자만 변경할 수 있습니다.");
        }
    }

    private static int nextGroupNumber(List<CodeOptionView> groups) {
        int max = -1;
        for (CodeOptionView group : groups) {
            Matcher matcher = GROUP_NAME_PATTERN.matcher(group.name() == null ? "" : group.name().trim());
            if (matcher.matches()) {
                max = Math.max(max, Integer.parseInt(matcher.group(1)));
            }
        }
        return max + 1;
    }

    private static String nextSmallCode(List<CodeOptionView> groups) {
        long maxCode = 19000000L;
        for (CodeOptionView group : groups) {
            try {
                long code = Long.parseLong(group.code().trim());
                maxCode = Math.max(maxCode, code);
            } catch (Exception ignored) {
                // ignore non-numeric codes
            }
        }
        return String.valueOf(maxCode + 10L);
    }

    public boolean isApprover(long actorUserId) {
        return workDiaryApproverPolicy.isApprover(actorUserId);
    }

    public WorkDiaryPageView list(WorkDiaryListCriteria criteria) {
        var items = workDiaryRepository.findActiveEntries(criteria).stream()
                .map(this::toListItemView)
                .toList();
        long total = workDiaryRepository.countActiveEntries(criteria);
        return new WorkDiaryPageView(items, total, criteria.page(), criteria.size());
    }

    public WorkDiaryDetailView getDetail(long id, long actorUserId, boolean approver) {
        var entry = workDiaryRepository.findActiveEntryById(id)
                .orElseThrow(() -> new IllegalArgumentException("업무일지를 찾을 수 없습니다: " + id));
        assertCanRead(entry, actorUserId, approver);
        return toDetailView(entry, actorUserId, approver);
    }

    public WorkDiaryDetailView getByDate(LocalDate workDate, long actorUserId) {
        var entry = workDiaryRepository.findActiveEntryByAuthorAndDate(actorUserId, workDate)
                .orElseThrow(() -> new IllegalArgumentException("해당 일자의 업무일지가 없습니다: " + workDate));
        return toDetailView(entry, actorUserId, false);
    }

    public WorkDiaryDetailView create(CreateWorkDiaryCommand command) {
        var user = userRepository.findActiveById(command.actorUserId())
                .orElseThrow(() -> new IllegalArgumentException("사용자를 찾을 수 없습니다: " + command.actorUserId()));
        Long groupId = user.workDiaryGroupId();
        if (groupId == null) {
            throw new IllegalStateException("업무일지 그룹이 지정되지 않았습니다.");
        }
        var template = workDiaryRepository.findActiveTemplateByGroupId(groupId)
                .orElseThrow(() -> new IllegalArgumentException("업무일지 템플릿을 찾을 수 없습니다. groupId=" + groupId));
        workDiaryRepository.findActiveEntryByAuthorAndDate(command.actorUserId(), command.workDate()).ifPresent(it -> {
            throw new IllegalStateException("해당 날짜에 이미 작성된 업무일지가 있습니다.");
        });
        String fixedTitle = user.name() + " 업무일지";
        Map<String, Object> normalizedValues = WorkDiaryFieldSchemaSupport.normalizeFieldValues(
                command.fieldValues(),
                template.fieldSchema()
        );
        long id = workDiaryRepository.saveEntry(
                command.workDate(),
                command.actorUserId(),
                groupId,
                template.templateCode(),
                fixedTitle,
                normalizedValues,
                command.listed(),
                command.closingNote(),
                command.status(),
                command.actorLoginId(),
                command.actorUserIdText()
        );
        return getDetail(id, command.actorUserId(), false);
    }

    public WorkDiaryDetailView update(UpdateWorkDiaryCommand command) {
        var entry = workDiaryRepository.findActiveEntryById(command.id())
                .orElseThrow(() -> new IllegalArgumentException("업무일지를 찾을 수 없습니다: " + command.id()));
        assertCanEdit(entry, command.actorUserId());
        var template = workDiaryRepository.findActiveTemplateByGroupId(entry.workDiaryGroupId())
                .orElseThrow(() -> new IllegalArgumentException("업무일지 템플릿을 찾을 수 없습니다."));
        Map<String, Object> normalizedValues = WorkDiaryFieldSchemaSupport.normalizeFieldValues(
                command.fieldValues(),
                template.fieldSchema()
        );
        workDiaryRepository.updateEntry(
                command.id(),
                normalizedValues,
                command.listed(),
                command.closingNote(),
                command.actorLoginId(),
                command.actorUserIdText()
        );
        return getDetail(command.id(), command.actorUserId(), false);
    }

    public void delete(long id, long actorUserId, String actorLoginId, String actorUserIdText) {
        var entry = workDiaryRepository.findActiveEntryById(id)
                .orElseThrow(() -> new IllegalArgumentException("업무일지를 찾을 수 없습니다: " + id));
        assertCanEdit(entry, actorUserId);
        workDiaryRepository.deleteEntry(id);
    }

    public WorkDiarySubmitResult submit(SubmitWorkDiaryCommand command) {
        var entry = workDiaryRepository.findActiveEntryById(command.id())
                .orElseThrow(() -> new IllegalArgumentException("업무일지를 찾을 수 없습니다: " + command.id()));
        assertOwner(entry, command.actorUserId());
        if (entry.status() == WorkDiaryStatus.APPROVED) {
            throw new IllegalStateException("결재 완료된 업무일지는 제출할 수 없습니다.");
        }
        Instant now = Instant.now();
        workDiaryRepository.markSubmitted(
                command.id(),
                WorkDiaryStatus.SUBMITTED,
                now,
                command.actorLoginId(),
                command.actorUserIdText()
        );
        return new WorkDiarySubmitResult(command.id(), WorkDiaryStatus.SUBMITTED, toLocalDateTime(now));
    }

    public WorkDiaryApproveResult approve(ApproveWorkDiaryCommand command) {
        assertApprover(command.actorUserId());
        var entry = workDiaryRepository.findActiveEntryById(command.id())
                .orElseThrow(() -> new IllegalArgumentException("업무일지를 찾을 수 없습니다: " + command.id()));
        if (entry.status() == WorkDiaryStatus.APPROVED) {
            throw new IllegalStateException("이미 결재된 업무일지입니다.");
        }
        if (entry.status() != WorkDiaryStatus.SUBMITTED) {
            throw new IllegalStateException("제출된 업무일지만 결재할 수 있습니다.");
        }
        Instant now = Instant.now();
        String approverName = userRepository.findActiveById(command.actorUserId())
                .map(u -> u.name())
                .orElse("");
        workDiaryRepository.markApproved(
                command.id(),
                command.directiveNote(),
                WorkDiaryStatus.APPROVED,
                now,
                command.actorUserId(),
                command.actorLoginId(),
                command.actorUserIdText()
        );
        return new WorkDiaryApproveResult(
                command.id(),
                WorkDiaryStatus.APPROVED,
                command.directiveNote(),
                toLocalDateTime(now),
                command.actorUserId(),
                approverName
        );
    }

    public WorkDiaryCancelApprovalResult cancelApproval(CancelWorkDiaryApprovalCommand command) {
        assertApprover(command.actorUserId());
        var entry = workDiaryRepository.findActiveEntryById(command.id())
                .orElseThrow(() -> new IllegalArgumentException("업무일지를 찾을 수 없습니다: " + command.id()));
        if (entry.status() != WorkDiaryStatus.APPROVED) {
            throw new IllegalStateException("결재 상태가 아니어서 결재취소할 수 없습니다.");
        }
        Instant now = Instant.now();
        String approverName = userRepository.findActiveById(command.actorUserId())
                .map(u -> u.name())
                .orElse("");
        workDiaryRepository.cancelApproval(
                command.id(),
                WorkDiaryStatus.SUBMITTED,
                now,
                command.actorUserId(),
                command.actorLoginId(),
                command.actorUserIdText()
        );
        return new WorkDiaryCancelApprovalResult(
                command.id(),
                WorkDiaryStatus.SUBMITTED,
                toLocalDateTime(now),
                command.actorUserId(),
                approverName
        );
    }

    private void assertCanRead(WorkDiaryRepository.WorkDiaryEntryRecord entry, long actorUserId, boolean approver) {
        if (approver && entry.status() != WorkDiaryStatus.DRAFT) {
            return;
        }
        assertOwner(entry, actorUserId);
    }

    private void assertCanEdit(WorkDiaryRepository.WorkDiaryEntryRecord entry, long actorUserId) {
        assertOwner(entry, actorUserId);
        if (!canAuthorEdit(entry)) {
            throw new IllegalStateException("결재 완료된 업무일지는 수정/삭제할 수 없습니다.");
        }
    }

    private boolean canAuthorEdit(WorkDiaryRepository.WorkDiaryEntryRecord entry) {
        if (entry.status() == WorkDiaryStatus.APPROVED) {
            return false;
        }
        return entry.status() == WorkDiaryStatus.DRAFT
                || entry.status() == WorkDiaryStatus.SUBMITTED
                || entry.status() == WorkDiaryStatus.REJECTED;
    }

    private void assertApprover(long actorUserId) {
        if (!isApprover(actorUserId)) {
            throw new IllegalStateException("업무일지 결재 권한이 없습니다.");
        }
    }

    private void assertOwner(WorkDiaryRepository.WorkDiaryEntryRecord entry, long actorUserId) {
        if (entry.authorUserId() != actorUserId) {
            throw new IllegalStateException("업무일지를 수정할 권한이 없습니다.");
        }
    }

    private WorkDiaryListItemView toListItemView(WorkDiaryRepository.WorkDiaryEntryRecord record) {
        String authorName = userRepository.findActiveById(record.authorUserId())
                .map(u -> u.name())
                .orElse("");
        String groupName = userRepository.findActiveById(record.authorUserId())
                .map(u -> u.workDiaryGroupName())
                .orElse("");
        return new WorkDiaryListItemView(
                record.id(),
                record.workDate(),
                record.workDateTitle(),
                record.authorUserId(),
                authorName,
                record.workDiaryGroupId(),
                groupName,
                record.templateCode(),
                record.status(),
                record.listed(),
                toLocalDateTime(record.approvedAt()),
                trimPreview(record.directiveNote(), 100)
        );
    }

    private WorkDiaryDetailView toDetailView(WorkDiaryRepository.WorkDiaryEntryRecord record, long actorUserId, boolean approver) {
        var author = userRepository.findActiveById(record.authorUserId()).orElse(null);
        String authorName = author != null ? author.name() : "";
        String groupName = author != null ? author.workDiaryGroupName() : "";
        String templateName = workDiaryRepository.findActiveTemplateByGroupId(record.workDiaryGroupId())
                .map(WorkDiaryRepository.WorkDiaryTemplateRecord::templateName)
                .orElse("");
        Map<String, Object> fieldSchema = workDiaryRepository.findActiveTemplateByGroupId(record.workDiaryGroupId())
                .map(WorkDiaryRepository.WorkDiaryTemplateRecord::fieldSchema)
                .map(WorkDiaryFieldSchemaSupport::forWriter)
                .orElse(Map.of());
        String approvedByName = record.approvedByUserId() != null
                ? userRepository.findActiveById(record.approvedByUserId()).map(u -> u.name()).orElse("")
                : null;
        String canceledByName = record.approvalCanceledByUserId() != null
                ? userRepository.findActiveById(record.approvalCanceledByUserId()).map(u -> u.name()).orElse("")
                : null;
        boolean canEdit = record.authorUserId() == actorUserId && canAuthorEdit(record);
        boolean canDelete = canEdit;
        boolean canApprove = approver && record.status() == WorkDiaryStatus.SUBMITTED;
        boolean canCancelApproval = approver && record.status() == WorkDiaryStatus.APPROVED;
        return new WorkDiaryDetailView(
                record.id(),
                record.workDate(),
                record.workDateTitle(),
                record.authorUserId(),
                authorName,
                record.workDiaryGroupId(),
                groupName,
                record.templateCode(),
                templateName,
                fieldSchema,
                record.fieldValues(),
                record.directiveNote(),
                record.status(),
                record.listed(),
                record.closingNote(),
                toLocalDateTime(record.submittedAt()),
                toLocalDateTime(record.approvedAt()),
                record.approvedByUserId(),
                approvedByName,
                toLocalDateTime(record.approvalCanceledAt()),
                record.approvalCanceledByUserId(),
                canceledByName,
                canEdit,
                canDelete,
                canApprove,
                canCancelApproval,
                toLocalDateTime(record.createdAt()),
                toLocalDateTime(record.updatedAt())
        );
    }

    private String trimPreview(String source, int maxLength) {
        if (source == null) {
            return null;
        }
        if (source.length() <= maxLength) {
            return source;
        }
        return source.substring(0, maxLength);
    }

    private LocalDateTime toLocalDateTime(Instant value) {
        if (value == null) {
            return null;
        }
        return LocalDateTime.ofInstant(value, ZoneId.systemDefault());
    }
}

