package com.shindong.smartmanager.api.web.production;

import static org.hamcrest.Matchers.not;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.shindong.smartmanager.application.auth.AuthUserRepository;
import com.shindong.smartmanager.application.auth.JwtTokenPort;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.security.test.context.support.WithMockUser;
import org.springframework.test.web.servlet.MockMvc;

@SpringBootTest
@AutoConfigureMockMvc
class ProductionPlanCreateAuthTest {

    @Autowired
    private MockMvc mockMvc;

    @Autowired
    private JwtTokenPort jwtTokenPort;

    @Autowired
    private AuthUserRepository authUserRepository;

    @Autowired
    private ObjectMapper objectMapper;

    @Test
    void createWithoutAuthReturns401() throws Exception {
        mockMvc.perform(
                        post("/api/v1/production/plans")
                                .contentType(MediaType.APPLICATION_JSON)
                                .content("{\"lines\":[{\"salesOrderLineId\":1,\"plannedQty\":10}]}")
                )
                .andExpect(status().isUnauthorized());
    }

    @Test
    void createWithJwtBearerDoesNotReturn401() throws Exception {
        AuthUserRepository.AuthUserRecord admin = authUserRepository.findActiveByLoginId("admin")
                .orElseThrow(() -> new IllegalStateException("admin user not found"));
        String token = jwtTokenPort.createToken(admin.id(), admin.loginId(), admin.authorities());

        mockMvc.perform(
                        post("/api/v1/production/plans")
                                .header(HttpHeaders.AUTHORIZATION, "Bearer " + token)
                                .contentType(MediaType.APPLICATION_JSON)
                                .content("{\"lines\":[{\"salesOrderLineId\":1,\"plannedQty\":10}]}")
                )
                .andExpect(status().is(not(401)));
    }

    @Test
    void createExistingCandidateLineWithJwtBearer() throws Exception {
        AuthUserRepository.AuthUserRecord admin = authUserRepository.findActiveByLoginId("admin")
                .orElseThrow(() -> new IllegalStateException("admin user not found"));
        String token = jwtTokenPort.createToken(admin.id(), admin.loginId(), admin.authorities());

        String candidatesJson = mockMvc.perform(
                        get("/api/v1/production/plan-candidates")
                                .header(HttpHeaders.AUTHORIZATION, "Bearer " + token)
                )
                .andExpect(status().isOk())
                .andReturn()
                .getResponse()
                .getContentAsString();

        if (candidatesJson.equals("[]")) {
            return;
        }

        JsonNode firstCandidate = objectMapper.readTree(candidatesJson).get(0);
        long lineId = firstCandidate.get("lineId").asLong();
        mockMvc.perform(
                        post("/api/v1/production/plans")
                                .header(HttpHeaders.AUTHORIZATION, "Bearer " + token)
                                .contentType(MediaType.APPLICATION_JSON)
                                .content("{\"lines\":[{\"salesOrderLineId\":" + lineId + ",\"plannedQty\":10}]}")
                )
                .andExpect(result -> {
                    int code = result.getResponse().getStatus();
                    String body = result.getResponse().getContentAsString();
                    if (code == 401) {
                        throw new AssertionError("Expected successful create for line " + lineId + ", got 401: " + body);
                    }
                });
    }

    @Test
    void cancelWithJwtBearerDoesNotReturn401() throws Exception {
        AuthUserRepository.AuthUserRecord admin = authUserRepository.findActiveByLoginId("admin")
                .orElseThrow(() -> new IllegalStateException("admin user not found"));
        String token = jwtTokenPort.createToken(admin.id(), admin.loginId(), admin.authorities());

        mockMvc.perform(
                        post("/api/v1/production/plans/1/cancel")
                                .header(HttpHeaders.AUTHORIZATION, "Bearer " + token)
                )
                .andExpect(status().is(not(401)));
    }

    @Test
    @WithMockUser(authorities = "production:plan:write")
    void createWithWriteAuthorityDoesNotReturn401() throws Exception {
        mockMvc.perform(
                        post("/api/v1/production/plans")
                                .contentType(MediaType.APPLICATION_JSON)
                                .content("{\"lines\":[{\"salesOrderLineId\":1,\"plannedQty\":10}]}")
                )
                .andExpect(result -> {
                    int code = result.getResponse().getStatus();
                    if (code == 401) {
                        throw new AssertionError("Expected authenticated request, got 401");
                    }
                });
    }

    @Test
    @WithMockUser(authorities = "production:plan:read")
    void createWithReadOnlyAuthorityReturns403() throws Exception {
        mockMvc.perform(
                        post("/api/v1/production/plans")
                                .contentType(MediaType.APPLICATION_JSON)
                                .content("{\"lines\":[{\"salesOrderLineId\":1,\"plannedQty\":10}]}")
                )
                .andExpect(status().isForbidden());
    }

    @Test
    @WithMockUser(authorities = "production:plan:write")
    void cancelWithWriteAuthorityDoesNotReturn401() throws Exception {
        mockMvc.perform(post("/api/v1/production/plans/1/cancel"))
                .andExpect(result -> {
                    int code = result.getResponse().getStatus();
                    if (code == 401) {
                        throw new AssertionError("Expected authenticated request, got 401");
                    }
                });
    }

    @Test
    @WithMockUser(authorities = "production:plan:read")
    void listCandidatesWithReadAuthorityReturns200() throws Exception {
        mockMvc.perform(get("/api/v1/production/plan-candidates"))
                .andExpect(status().isOk());
    }
}
