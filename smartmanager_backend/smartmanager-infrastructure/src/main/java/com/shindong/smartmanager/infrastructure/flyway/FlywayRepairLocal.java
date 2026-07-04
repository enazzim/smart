package com.shindong.smartmanager.infrastructure.flyway;

import org.flywaydb.core.Flyway;

/**
 * Local dev: repair Flyway checksums when migration files changed after apply (e.g. line endings).
 * Usage: gradlew :smartmanager-infrastructure:flywayRepairLocal
 */
public final class FlywayRepairLocal {

    private static final String JDBC_URL =
            "jdbc:mariadb://localhost:3306/kit_erp?characterEncoding=utf8mb4&serverTimezone=Asia/Seoul";
    private static final String USER = "root";
    private static final String PASSWORD = "1111";

    private FlywayRepairLocal() {
    }

    public static void main(String[] args) {
        Flyway flyway = Flyway.configure()
                .dataSource(JDBC_URL, USER, PASSWORD)
                .locations("classpath:db/migration")
                .load();
        flyway.repair();
        System.out.println("Flyway repair completed.");
    }
}
