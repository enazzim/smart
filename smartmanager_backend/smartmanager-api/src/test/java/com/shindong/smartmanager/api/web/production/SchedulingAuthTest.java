package com.shindong.smartmanager.api.web.production;

import static org.hamcrest.Matchers.not;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

import com.shindong.smartmanager.application.auth.AuthUserRepository;
import com.shindong.smartmanager.application.auth.JwtTokenPort;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.http.HttpHeaders;
import org.springframework.security.test.context.support.WithMockUser;
import org.springframework.test.web.servlet.MockMvc;

@SpringBootTest
@AutoConfigureMockMvc
class SchedulingAuthTest {

    @Autowired
    private MockMvc mockMvc;

    @Autowired
    private JwtTokenPort jwtTokenPort;

    @Autowired
    private AuthUserRepository authUserRepository;

    @Test
    void loadWithoutAuthReturns401() throws Exception {
        mockMvc.perform(
                        get("/api/v1/production/scheduling/work-center-load")
                                .param("from", "2025-06-01")
                                .param("to", "2025-06-30")
                                .param("workCenterId", "1")
                )
                .andExpect(status().isUnauthorized());
    }

    @Test
    void loadWithAdminJwtDoesNotReturn401() throws Exception {
        AuthUserRepository.AuthUserRecord admin = authUserRepository.findActiveByLoginId("admin")
                .orElseThrow(() -> new IllegalStateException("admin user not found"));
        String token = jwtTokenPort.createToken(admin.id(), admin.loginId(), admin.authorities());

        mockMvc.perform(
                        get("/api/v1/production/scheduling/work-center-load")
                                .header(HttpHeaders.AUTHORIZATION, "Bearer " + token)
                                .param("from", "2025-06-01")
                                .param("to", "2025-06-30")
                                .param("workCenterId", "1")
                )
                .andExpect(status().is(not(401)));
    }

    @Test
    @WithMockUser(authorities = "production:work-plan:read")
    void loadWithWorkPlanReadReturns200() throws Exception {
        mockMvc.perform(
                        get("/api/v1/production/scheduling/work-center-load")
                                .param("from", "2025-06-01")
                                .param("to", "2025-06-30")
                                .param("workCenterId", "1")
                )
                .andExpect(status().isOk());
    }

    @Test
    @WithMockUser(authorities = "production:scheduling:read")
    void loadWithSchedulingReadReturns200() throws Exception {
        mockMvc.perform(
                        get("/api/v1/production/scheduling/work-center-load")
                                .param("from", "2025-06-01")
                                .param("to", "2025-06-30")
                                .param("workCenterId", "1")
                )
                .andExpect(status().isOk());
    }
}
