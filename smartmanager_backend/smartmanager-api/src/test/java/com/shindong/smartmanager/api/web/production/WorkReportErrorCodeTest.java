package com.shindong.smartmanager.api.web.production;

import static org.mockito.ArgumentMatchers.any;
import static org.mockito.ArgumentMatchers.anyLong;
import static org.mockito.ArgumentMatchers.anyString;
import static org.mockito.Mockito.doThrow;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

import com.shindong.smartmanager.application.auth.AuthUserRepository;
import com.shindong.smartmanager.application.auth.JwtTokenPort;
import com.shindong.smartmanager.application.common.AppBusinessException;
import com.shindong.smartmanager.application.common.AppErrorCode;
import com.shindong.smartmanager.infrastructure.application.WorkReportApplicationService;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.test.web.servlet.MockMvc;

@SpringBootTest
@AutoConfigureMockMvc
class WorkReportErrorCodeTest {

    @Autowired
    private MockMvc mockMvc;

    @Autowired
    private JwtTokenPort jwtTokenPort;

    @Autowired
    private AuthUserRepository authUserRepository;

    @MockBean
    private WorkReportApplicationService workReportApplicationService;

    private String adminBearerToken() {
        AuthUserRepository.AuthUserRecord admin = authUserRepository.findActiveByLoginId("admin")
                .orElseThrow(() -> new IllegalStateException("admin user not found"));
        return "Bearer " + jwtTokenPort.createToken(admin.id(), admin.loginId(), admin.authorities());
    }

    @Test
    void cancelWhenReportMissingReturnsWorkReportNotFoundCode() throws Exception {
        doThrow(new AppBusinessException(AppErrorCode.WORK_REPORT_NOT_FOUND, "문구 변경 가능"))
                .when(workReportApplicationService)
                .cancel(anyLong(), anyString());

        mockMvc.perform(post("/api/v1/production/work-reports/999999/cancel")
                        .header(HttpHeaders.AUTHORIZATION, adminBearerToken()))
                .andExpect(status().isBadRequest())
                .andExpect(jsonPath("$.errorCode").value("WORK_REPORT_NOT_FOUND"));
    }

    @Test
    void createWhenMaterialIssueInsufficientReturnsErrorCode() throws Exception {
        doThrow(new AppBusinessException(AppErrorCode.MATERIAL_ISSUE_SHORTAGE, "언어별 문구는 바뀔 수 있음"))
                .when(workReportApplicationService)
                .register(any(), anyString());

        mockMvc.perform(post("/api/v1/production/work-reports")
                        .header(HttpHeaders.AUTHORIZATION, adminBearerToken())
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("""
                                {
                                  "workOrderId": 1,
                                  "reportDate": "2026-07-07",
                                  "goodQty": 10,
                                  "scrapQty": 0
                                }
                                """))
                .andExpect(status().isBadRequest())
                .andExpect(jsonPath("$.errorCode").value("MATERIAL_ISSUE_SHORTAGE"));
    }

    @Test
    void createValidationFailureReturnsValidationCode() throws Exception {
        mockMvc.perform(post("/api/v1/production/work-reports")
                        .header(HttpHeaders.AUTHORIZATION, adminBearerToken())
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("""
                                {
                                  "reportDate": "2026-07-07",
                                  "goodQty": 1
                                }
                                """))
                .andExpect(status().isBadRequest())
                .andExpect(jsonPath("$.errorCode").value("VALIDATION_FAILED"));
    }
}
