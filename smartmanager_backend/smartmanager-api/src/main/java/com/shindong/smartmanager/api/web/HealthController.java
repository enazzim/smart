package com.shindong.smartmanager.api.web;

import java.util.Map;
import javax.sql.DataSource;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api")
public class HealthController {

    private final DataSource dataSource;

    public HealthController(DataSource dataSource) {
        this.dataSource = dataSource;
    }

    @GetMapping("/health")
    public Map<String, Object> health() {
        try (var connection = dataSource.getConnection()) {
            return Map.of(
                    "status", "UP",
                    "database", connection.getCatalog(),
                    "dbProduct", connection.getMetaData().getDatabaseProductName()
            );
        } catch (Exception ex) {
            return Map.of(
                    "status", "DOWN",
                    "error", ex.getMessage()
            );
        }
    }
}
