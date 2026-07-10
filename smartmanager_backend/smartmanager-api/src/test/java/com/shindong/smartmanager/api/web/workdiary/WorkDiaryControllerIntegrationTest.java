package com.shindong.smartmanager.api.web.workdiary;

import static org.hamcrest.Matchers.hasSize;
import static org.hamcrest.Matchers.hasItems;
import static org.hamcrest.Matchers.not;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.delete;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.put;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.shindong.smartmanager.application.auth.AuthUserRepository;
import com.shindong.smartmanager.application.auth.JwtTokenPort;
import com.shindong.smartmanager.application.code.CodeGroupOptionsRepository;
import com.shindong.smartmanager.application.workdiary.WorkDiaryRepository;
import com.shindong.smartmanager.application.role.RoleRepository;
import com.shindong.smartmanager.application.user.UserCommand;
import com.shindong.smartmanager.application.user.UserView;
import com.shindong.smartmanager.infrastructure.application.UserApplicationService;
import java.nio.charset.StandardCharsets;
import java.time.LocalDate;
import java.util.List;
import java.util.Map;
import java.util.UUID;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.MvcResult;

@SpringBootTest
@AutoConfigureMockMvc
class WorkDiaryControllerIntegrationTest {

    private static final String BASE = "/api/v1/work-diaries";

    @Autowired
    private MockMvc mockMvc;

    @Autowired
    private JwtTokenPort jwtTokenPort;

    @Autowired
    private AuthUserRepository authUserRepository;

    @Autowired
    private UserApplicationService userApplicationService;

    @Autowired
    private RoleRepository roleRepository;

    @Autowired
    private CodeGroupOptionsRepository codeGroupOptionsRepository;

    @Autowired
    private WorkDiaryRepository workDiaryRepository;

    @Autowired
    private ObjectMapper objectMapper;

    @Test
    void writerSeesOnlyOwnDiaryAndAdminSeesAll() throws Exception {
        String writer1Login = "wd-writer1-" + UUID.randomUUID().toString().substring(0, 8);
        String writer2Login = "wd-writer2-" + UUID.randomUUID().toString().substring(0, 8);
        String writer1Name = "작성자A-" + UUID.randomUUID().toString().substring(0, 4);
        String writer2Name = "작성자B-" + UUID.randomUUID().toString().substring(0, 4);
        UserView writer1 = registerViewer(writer1Login, writer1Name);
        UserView writer2 = registerViewer(writer2Login, writer2Name);

        String writer1Token = tokenWithAuthorities(writer1.id(), writer1.loginId(), List.of(
                "community:workdiary:read", "community:workdiary:write"
        ));
        String writer2Token = tokenWithAuthorities(writer2.id(), writer2.loginId(), List.of(
                "community:workdiary:read", "community:workdiary:write"
        ));
        String adminToken = adminToken();
        LocalDate workDate = LocalDate.of(2099, 1, 1);

        createDiary(writer1Token, workDate, Map.of("01", "writer1"));
        createDiary(writer2Token, workDate, Map.of("01", "writer2"));

        mockMvc.perform(get(BASE)
                        .param("fromDate", workDate.toString())
                        .param("toDate", workDate.toString())
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + writer1Token))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.items", hasSize(1)))
                .andExpect(jsonPath("$.items[0].authorName").value(writer1Name));

        mockMvc.perform(get(BASE)
                        .param("fromDate", workDate.toString())
                        .param("toDate", workDate.toString())
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + adminToken))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.items[*].authorName", hasItems(writer1Name, writer2Name)));
    }

    @Test
    void writerCanUpdateDeleteBeforeApprovalButCannotAfterApproval() throws Exception {
        String writerLogin = "wd-lock-" + UUID.randomUUID().toString().substring(0, 8);
        String writerName = "작성잠금-" + UUID.randomUUID().toString().substring(0, 4);
        UserView writer = registerViewer(writerLogin, writerName);
        String writerToken = tokenWithAuthorities(writer.id(), writer.loginId(), List.of(
                "community:workdiary:read", "community:workdiary:write"
        ));
        String adminToken = adminToken();
        LocalDate workDate = LocalDate.of(2099, 1, 2);

        long id = createDiary(writerToken, workDate, Map.of("01", "초안"));

        mockMvc.perform(put(BASE + "/{id}", id)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + writerToken)
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("""
                                {
                                  "fieldValues": {"01": "수정-결재전"},
                                  "listed": true,
                                  "closingNote": "결재전 수정"
                                }
                                """))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.fieldValues.01").value("수정-결재전"));

        mockMvc.perform(post(BASE + "/{id}/submit", id)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + writerToken)
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("{}"))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.status").value("SUBMITTED"));

        mockMvc.perform(put(BASE + "/{id}", id)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + writerToken)
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("""
                                {
                                  "fieldValues": {"01": "제출후 수정"},
                                  "listed": true,
                                  "closingNote": "결재 전 수정"
                                }
                                """))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.fieldValues.01").value("제출후 수정"))
                .andExpect(jsonPath("$.canEdit").value(true))
                .andExpect(jsonPath("$.canDelete").value(true));

        mockMvc.perform(post(BASE + "/{id}/approve", id)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + adminToken)
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("""
                                {
                                  "directiveNote": "결재합니다."
                                }
                                """))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.status").value("APPROVED"));

        mockMvc.perform(put(BASE + "/{id}", id)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + writerToken)
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("""
                                {
                                  "fieldValues": {"01": "수정-결재후"},
                                  "listed": true,
                                  "closingNote": "결재후 수정시도"
                                }
                                """))
                .andExpect(status().isConflict())
                .andExpect(jsonPath("$.message").value("결재 완료된 업무일지는 수정/삭제할 수 없습니다."));

        mockMvc.perform(delete(BASE + "/{id}", id)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + writerToken))
                .andExpect(status().isConflict())
                .andExpect(jsonPath("$.message").value("결재 완료된 업무일지는 수정/삭제할 수 없습니다."));
    }

    @Test
    void writerCanEditAgainAfterAdminCancelsApproval() throws Exception {
        String writerLogin = "wd-cancel-" + UUID.randomUUID().toString().substring(0, 8);
        String writerName = "취소복구-" + UUID.randomUUID().toString().substring(0, 4);
        UserView writer = registerViewer(writerLogin, writerName);
        String writerToken = tokenWithAuthorities(writer.id(), writer.loginId(), List.of(
                "community:workdiary:read", "community:workdiary:write"
        ));
        String adminToken = adminToken();
        LocalDate workDate = LocalDate.of(2099, 1, 3);

        long id = createDiary(writerToken, workDate, Map.of("01", "초안"));

        mockMvc.perform(post(BASE + "/{id}/approve", id)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + adminToken)
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("""
                                {
                                  "directiveNote": "결재합니다."
                                }
                                """))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.status").value("APPROVED"));

        mockMvc.perform(post(BASE + "/{id}/cancel-approval", id)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + adminToken)
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("{}"))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.status").value("SUBMITTED"));

        mockMvc.perform(get(BASE + "/{id}", id)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + writerToken))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.canEdit").value(true))
                .andExpect(jsonPath("$.canDelete").value(true));

        mockMvc.perform(put(BASE + "/{id}", id)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + writerToken)
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("""
                                {
                                  "fieldValues": {"01": "결재취소후 수정"},
                                  "listed": true,
                                  "closingNote": "재수정"
                                }
                                """))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.fieldValues.01").value("결재취소후 수정"));

        mockMvc.perform(delete(BASE + "/{id}", id)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + writerToken))
                .andExpect(status().isNoContent());

        mockMvc.perform(get(BASE + "/{id}", id)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + writerToken))
                .andExpect(status().isBadRequest());
    }

    @Test
    void duplicateCreateOnSameDateReturns409() throws Exception {
        String writerLogin = "wd-dup-" + UUID.randomUUID().toString().substring(0, 8);
        String writerName = "중복작성-" + UUID.randomUUID().toString().substring(0, 4);
        UserView writer = registerViewer(writerLogin, writerName);
        String writerToken = tokenWithAuthorities(writer.id(), writer.loginId(), List.of(
                "community:workdiary:read", "community:workdiary:write"
        ));
        LocalDate workDate = LocalDate.of(2099, 1, 4);

        createDiary(writerToken, workDate, Map.of("01", "첫 작성"));

        mockMvc.perform(post(BASE)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + writerToken)
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("""
                                {
                                  "workDate": "2099-01-04",
                                  "fieldValues": {"01": "중복 작성 시도"},
                                  "listed": true,
                                  "closingNote": "",
                                  "status": "DRAFT"
                                }
                                """))
                .andExpect(status().isConflict())
                .andExpect(jsonPath("$.message").value("해당 날짜에 이미 작성된 업무일지가 있습니다."));
    }

    @Test
    void nonApproverCannotApproveDiary() throws Exception {
        String writerLogin = "wd-na-w-" + UUID.randomUUID().toString().substring(0, 8);
        String writerName = "작성자-" + UUID.randomUUID().toString().substring(0, 4);
        String outsiderLogin = "wd-na-o-" + UUID.randomUUID().toString().substring(0, 8);
        String outsiderName = "일반사용자-" + UUID.randomUUID().toString().substring(0, 4);
        UserView writer = registerViewer(writerLogin, writerName);
        UserView outsider = registerViewer(outsiderLogin, outsiderName);
        String writerToken = tokenWithAuthorities(writer.id(), writer.loginId(), List.of(
                "community:workdiary:read", "community:workdiary:write"
        ));
        String outsiderToken = tokenWithAuthorities(outsider.id(), outsider.loginId(), List.of(
                "community:workdiary:read", "community:workdiary:write"
        ));
        LocalDate workDate = LocalDate.of(2099, 1, 5);

        long id = createDiary(writerToken, workDate, Map.of("01", "결재대기"));

        mockMvc.perform(post(BASE + "/{id}/approve", id)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + outsiderToken)
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("""
                                {
                                  "directiveNote": "일반 사용자가 결재 시도"
                                }
                                """))
                .andExpect(status().isForbidden());
    }

    @Test
    void nonOwnerCannotAccessOthersDiaryDetail() throws Exception {
        String ownerLogin = "wd-owner-" + UUID.randomUUID().toString().substring(0, 8);
        String ownerName = "소유자-" + UUID.randomUUID().toString().substring(0, 4);
        String otherLogin = "wd-other-" + UUID.randomUUID().toString().substring(0, 8);
        String otherName = "타사용자-" + UUID.randomUUID().toString().substring(0, 4);
        UserView owner = registerViewer(ownerLogin, ownerName);
        UserView other = registerViewer(otherLogin, otherName);
        String ownerToken = tokenWithAuthorities(owner.id(), owner.loginId(), List.of(
                "community:workdiary:read", "community:workdiary:write"
        ));
        String otherToken = tokenWithAuthorities(other.id(), other.loginId(), List.of(
                "community:workdiary:read", "community:workdiary:write"
        ));
        LocalDate workDate = LocalDate.of(2099, 1, 6);

        long id = createDiary(ownerToken, workDate, Map.of("01", "소유자 일지"));

        mockMvc.perform(get(BASE + "/{id}", id)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + otherToken))
                .andExpect(status().isConflict())
                .andExpect(jsonPath("$.message").value("업무일지를 수정할 권한이 없습니다."));
    }

    @Test
    void nonOwnerCannotUpdateOthersDiary() throws Exception {
        String ownerLogin = "wd-up-owner-" + UUID.randomUUID().toString().substring(0, 8);
        String ownerName = "수정소유자-" + UUID.randomUUID().toString().substring(0, 4);
        String otherLogin = "wd-up-other-" + UUID.randomUUID().toString().substring(0, 8);
        String otherName = "수정타인-" + UUID.randomUUID().toString().substring(0, 4);
        UserView owner = registerViewer(ownerLogin, ownerName);
        UserView other = registerViewer(otherLogin, otherName);
        String ownerToken = tokenWithAuthorities(owner.id(), owner.loginId(), List.of(
                "community:workdiary:read", "community:workdiary:write"
        ));
        String otherToken = tokenWithAuthorities(other.id(), other.loginId(), List.of(
                "community:workdiary:read", "community:workdiary:write"
        ));
        LocalDate workDate = LocalDate.of(2099, 1, 7);

        long id = createDiary(ownerToken, workDate, Map.of("01", "원본 내용"));

        mockMvc.perform(put(BASE + "/{id}", id)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + otherToken)
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("""
                                {
                                  "fieldValues": {"01": "타인이 수정 시도"},
                                  "listed": true,
                                  "closingNote": "권한 없음"
                                }
                                """))
                .andExpect(status().isConflict())
                .andExpect(jsonPath("$.message").value("업무일지를 수정할 권한이 없습니다."));
    }

    @Test
    void nonOwnerCannotDeleteOthersDiary() throws Exception {
        String ownerLogin = "wd-del-owner-" + UUID.randomUUID().toString().substring(0, 8);
        String ownerName = "삭제소유자-" + UUID.randomUUID().toString().substring(0, 4);
        String otherLogin = "wd-del-other-" + UUID.randomUUID().toString().substring(0, 8);
        String otherName = "삭제타인-" + UUID.randomUUID().toString().substring(0, 4);
        UserView owner = registerViewer(ownerLogin, ownerName);
        UserView other = registerViewer(otherLogin, otherName);
        String ownerToken = tokenWithAuthorities(owner.id(), owner.loginId(), List.of(
                "community:workdiary:read", "community:workdiary:write"
        ));
        String otherToken = tokenWithAuthorities(other.id(), other.loginId(), List.of(
                "community:workdiary:read", "community:workdiary:write"
        ));
        LocalDate workDate = LocalDate.of(2099, 1, 8);

        long id = createDiary(ownerToken, workDate, Map.of("01", "삭제 원본"));

        mockMvc.perform(delete(BASE + "/{id}", id)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + otherToken))
                .andExpect(status().isConflict())
                .andExpect(jsonPath("$.message").value("업무일지를 수정할 권한이 없습니다."));

        mockMvc.perform(get(BASE + "/{id}", id)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + ownerToken))
                .andExpect(status().isOk());
    }

    @Test
    void systemAdminCanUpdateTemplateAndWriterUsesUpdatedFields() throws Exception {
        String writerLogin = "wd-tpl-" + UUID.randomUUID().toString().substring(0, 8);
        String writerName = "양식작성-" + UUID.randomUUID().toString().substring(0, 4);
        UserView writer = registerViewer(writerLogin, writerName);
        String writerToken = tokenWithAuthorities(writer.id(), writer.loginId(), List.of(
                "community:workdiary:read", "community:workdiary:write"
        ));
        String adminToken = adminToken();
        long groupId = writer.workDiaryGroupId();
        LocalDate workDate = LocalDate.of(2099, 2, 1);

        mockMvc.perform(put(BASE + "/templates/{groupId}", groupId)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + adminToken)
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("""
                                {
                                  "templateName": "커스텀 업무일지",
                                  "legacyFields": {
                                    "01": "1. 커스텀 항목 A",
                                    "02": "2. 커스텀 항목 B"
                                  }
                                }
                                """))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.templateName").value("커스텀 업무일지"))
                .andExpect(jsonPath("$.fieldSchema.legacyFields.01").value("1. 커스텀 항목 A"));

        mockMvc.perform(put(BASE + "/templates/{groupId}", groupId)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + writerToken)
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("""
                                {
                                  "templateName": "권한 없음",
                                  "legacyFields": {"01": "실패"}
                                }
                                """))
                .andExpect(status().isConflict())
                .andExpect(jsonPath("$.message").value("업무일지 양식은 시스템 관리자만 수정할 수 있습니다."));

        mockMvc.perform(get(BASE + "/my-template")
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + writerToken))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.templateName").value("커스텀 업무일지"))
                .andExpect(jsonPath("$.fieldSchema.legacyFields.01").value("1. 커스텀 항목 A"));

        long id = createDiary(writerToken, workDate, Map.of("01", "값A", "02", "값B"));

        mockMvc.perform(get(BASE + "/{id}", id)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + writerToken))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.fieldValues.01").value("값A"))
                .andExpect(jsonPath("$.fieldValues.02").value("값B"));
    }

    private UserView registerViewer(String loginId, String name) {
        Long viewerRoleId = roleRepository.findAllActive().stream()
                .filter(role -> "VIEWER".equals(role.roleCode()))
                .findFirst()
                .orElseThrow(() -> new IllegalStateException("VIEWER role not found"))
                .id();
        long workDiaryGroupId = codeGroupOptionsRepository.findActiveOptions("WORK_DIARY_GROUP").stream()
                .map(option -> option.id())
                .filter(groupId -> workDiaryRepository.findActiveTemplateByGroupId(groupId).isPresent())
                .findFirst()
                .orElseThrow(() -> new IllegalStateException("work diary template seed not found"));
        return userApplicationService.register(
                new UserCommand(
                        loginId,
                        "Password123!",
                        name,
                        null,
                        null,
                        List.of(viewerRoleId),
                        workDiaryGroupId
                ),
                "admin"
        );
    }

    private String adminToken() {
        AuthUserRepository.AuthUserRecord admin = authUserRepository.findActiveByLoginId("admin")
                .orElseThrow(() -> new IllegalStateException("admin user not found"));
        return tokenWithAuthorities(admin.id(), admin.loginId(), List.of(
                "community:workdiary:read",
                "community:workdiary:write",
                "community:workdiary:approve"
        ));
    }

    private String tokenWithAuthorities(long userId, String loginId, List<String> authorities) {
        return jwtTokenPort.createToken(userId, loginId, authorities);
    }

    private long createDiary(String token, LocalDate workDate, Map<String, String> fieldValues) throws Exception {
        String body = objectMapper.writeValueAsString(Map.of(
                "workDate", workDate.toString(),
                "fieldValues", fieldValues,
                "listed", true,
                "closingNote", "",
                "status", "DRAFT"
        ));
        MvcResult result = mockMvc.perform(post(BASE)
                        .header(HttpHeaders.AUTHORIZATION, "Bearer " + token)
                        .contentType(MediaType.APPLICATION_JSON)
                        .characterEncoding(StandardCharsets.UTF_8)
                        .content(body))
                .andExpect(status().isCreated())
                .andExpect(jsonPath("$.workDateTitle").value(not("")))
                .andReturn();
        JsonNode json = objectMapper.readTree(result.getResponse().getContentAsString());
        return json.get("id").asLong();
    }
}

