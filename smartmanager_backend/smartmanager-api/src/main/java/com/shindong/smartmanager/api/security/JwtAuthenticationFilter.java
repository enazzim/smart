package com.shindong.smartmanager.api.security;

import com.shindong.smartmanager.application.auth.AuthUserRepository;
import com.shindong.smartmanager.application.auth.JwtTokenPort;
import jakarta.servlet.FilterChain;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.List;
import org.springframework.http.HttpHeaders;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Component;
import org.springframework.web.filter.OncePerRequestFilter;

@Component
public class JwtAuthenticationFilter extends OncePerRequestFilter {

    private final JwtTokenPort jwtTokenPort;
    private final AuthUserRepository authUserRepository;

    public JwtAuthenticationFilter(JwtTokenPort jwtTokenPort, AuthUserRepository authUserRepository) {
        this.jwtTokenPort = jwtTokenPort;
        this.authUserRepository = authUserRepository;
    }

    @Override
    protected void doFilterInternal(
            HttpServletRequest request,
            HttpServletResponse response,
            FilterChain filterChain
    ) throws ServletException, IOException {
        String authorization = request.getHeader(HttpHeaders.AUTHORIZATION);
        if (authorization != null && authorization.startsWith("Bearer ")) {
            String token = authorization.substring(7).trim();
            if (!token.isBlank()) {
                try {
                    JwtTokenPort.JwtClaims claims = jwtTokenPort.parseToken(token);
                    AuthUserRepository.AuthUserRecord user = authUserRepository.findActiveById(claims.userId())
                            .orElse(null);
                    if (user == null) {
                        SecurityContextHolder.clearContext();
                    } else {
                        JwtUserPrincipal principal = new JwtUserPrincipal(
                                user.id(),
                                user.loginId(),
                                user.authorities()
                        );
                        List<SimpleGrantedAuthority> authorities = user.authorities().stream()
                                .map(SimpleGrantedAuthority::new)
                                .toList();
                        UsernamePasswordAuthenticationToken authentication =
                                new UsernamePasswordAuthenticationToken(principal, token, authorities);
                        SecurityContextHolder.getContext().setAuthentication(authentication);
                    }
                } catch (RuntimeException ignored) {
                    SecurityContextHolder.clearContext();
                }
            }
        }

        filterChain.doFilter(request, response);
    }
}
