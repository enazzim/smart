package com.shindong.smartmanager.infrastructure.flyway;

import org.flywaydb.core.Flyway;
import org.flywaydb.core.api.output.MigrateResult;

/**
 * Local dev: apply pending Flyway migrations.
 * Usage: gradlew :smartmanager-infrastructure:flywayMigrateLocal
 */
public final class FlywayMigrateLocal {

    private static final String JDBC_URL =
            "jdbc:mariadb://localhost:3306/smartmanager?characterEncoding=utf8mb4&serverTimezone=Asia/Seoul";
    private static final String USER = "root";
    private static final String PASSWORD = "1111";

    private FlywayMigrateLocal() {
    }

    public static void main(String[] args) {
        Flyway flyway = Flyway.configure()
                .dataSource(JDBC_URL, USER, PASSWORD)
                .locations("classpath:db/migration")
                // 브랜치 전환으로 중간 버전 이력이 비는 경우(로컬) 허용
                .outOfOrder(true)
                .load();
        MigrateResult result = flyway.migrate();
        System.out.println("Flyway migrate completed.");
        System.out.println("Current version: " + result.targetSchemaVersion);
        if (result.migrationsExecuted == 0) {
            System.out.println("No pending migrations.");
        } else {
            result.migrations.forEach(m ->
                    System.out.println("Applied: " + m.version + " — " + m.description)
            );
        }
    }
}
