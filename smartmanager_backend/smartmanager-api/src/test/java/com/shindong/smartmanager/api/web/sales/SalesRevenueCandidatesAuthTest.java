package com.shindong.smartmanager.api.web.sales;

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
import org.springframework.test.web.servlet.MockMvc;

@SpringBootTest
@AutoConfigureMockMvc
class SalesRevenueCandidatesAuthTest {

    @Autowired
    private MockMvc mockMvc;

    @Autowired
    private JwtTokenPort jwtTokenPort;

    @Autowired
    private AuthUserRepository authUserRepository;

    @Test
    void candidatesWithoutAuthReturns401() throws Exception {
        mockMvc.perform(get("/api/v1/sales/revenues/candidates"))
                .andExpect(status().isUnauthorized());
    }

    @Test
    void candidatesWithAdminJwtDoesNotReturn401() throws Exception {
        AuthUserRepository.AuthUserRecord admin = authUserRepository.findActiveByLoginId("admin")
                .orElseThrow(() -> new IllegalStateException("admin user not found"));
        String token = jwtTokenPort.createToken(admin.id(), admin.loginId(), admin.authorities());

        mockMvc.perform(
                        get("/api/v1/sales/revenues/candidates")
                                .header(HttpHeaders.AUTHORIZATION, "Bearer " + token)
                )
                .andExpect(status().is(not(401)));
    }
}
