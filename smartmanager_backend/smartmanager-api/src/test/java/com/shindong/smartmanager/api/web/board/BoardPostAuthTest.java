package com.shindong.smartmanager.api.web.board;

import static org.hamcrest.Matchers.not;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.multipart;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

import com.shindong.smartmanager.application.auth.AuthUserRepository;
import com.shindong.smartmanager.application.auth.JwtTokenPort;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.mock.web.MockMultipartFile;
import org.springframework.security.test.context.support.WithMockUser;
import org.springframework.test.web.servlet.MockMvc;

@SpringBootTest
@AutoConfigureMockMvc
class BoardPostAuthTest {

    @Autowired
    private MockMvc mockMvc;

    @Autowired
    private JwtTokenPort jwtTokenPort;

    @Autowired
    private AuthUserRepository authUserRepository;

    @Test
    void listWithoutAuthReturns401() throws Exception {
        mockMvc.perform(get("/api/v1/boards/NOTICE/posts"))
                .andExpect(status().isUnauthorized());
    }

    @Test
    void listWithJwtDoesNotReturn401() throws Exception {
        AuthUserRepository.AuthUserRecord admin = authUserRepository.findActiveByLoginId("admin")
                .orElseThrow(() -> new IllegalStateException("admin user not found"));
        String token = jwtTokenPort.createToken(admin.id(), admin.loginId(), admin.authorities());

        mockMvc.perform(
                        get("/api/v1/boards/NOTICE/posts")
                                .header(HttpHeaders.AUTHORIZATION, "Bearer " + token)
                )
                .andExpect(status().is(not(401)));
    }

    @Test
    @WithMockUser(authorities = "community:board:read")
    void listWithReadAuthorityReturns200() throws Exception {
        mockMvc.perform(get("/api/v1/boards/NOTICE/posts"))
                .andExpect(status().isOk());
    }

    @Test
    @WithMockUser(authorities = "community:board:read")
    void createWithReadOnlyAuthorityReturns403() throws Exception {
        MockMultipartFile title = new MockMultipartFile(
                "title",
                "title",
                MediaType.TEXT_PLAIN_VALUE,
                "공지 제목".getBytes()
        );
        MockMultipartFile content = new MockMultipartFile(
                "content",
                "content",
                MediaType.TEXT_PLAIN_VALUE,
                "<p>본문</p>".getBytes()
        );

        mockMvc.perform(
                        multipart("/api/v1/boards/NOTICE/posts")
                                .file(title)
                                .file(content)
                )
                .andExpect(status().isForbidden());
    }

    @Test
    @WithMockUser(authorities = {"community:board:read", "community:board:write"})
    void editorTemplateWithWriteAuthorityReturns200() throws Exception {
        mockMvc.perform(get("/api/v1/boards/editor-template"))
                .andExpect(status().isOk());
    }
}
