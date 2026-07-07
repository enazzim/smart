plugins {
    id("io.spring.dependency-management")
}

dependencies {
    implementation(project(":smartmanager-application"))
    implementation(project(":smartmanager-domain"))
    implementation("org.springframework.boot:spring-boot-starter-data-jpa")
    implementation("org.springframework.boot:spring-boot-autoconfigure")
    implementation("org.springframework:spring-context")
    implementation("org.springframework:spring-tx")
    implementation("org.flywaydb:flyway-core")
    implementation("org.flywaydb:flyway-mysql")
    implementation("org.springframework.security:spring-security-crypto")
    implementation("io.jsonwebtoken:jjwt-api:0.12.6")
    runtimeOnly("io.jsonwebtoken:jjwt-impl:0.12.6")
    runtimeOnly("io.jsonwebtoken:jjwt-jackson:0.12.6")
    runtimeOnly("org.mariadb.jdbc:mariadb-java-client")
}

dependencyManagement {
    imports {
        mavenBom("org.springframework.boot:spring-boot-dependencies:3.5.0")
    }
}

tasks.register<JavaExec>("flywayRepairLocal") {
    group = "flyway"
    description = "Repair Flyway checksums for local smartmanager database"
    classpath = sourceSets.main.get().runtimeClasspath
    mainClass.set("com.shindong.smartmanager.infrastructure.flyway.FlywayRepairLocal")
}

tasks.register<JavaExec>("flywayMigrateLocal") {
    group = "flyway"
    description = "Apply pending Flyway migrations to local smartmanager database"
    classpath = sourceSets.main.get().runtimeClasspath
    mainClass.set("com.shindong.smartmanager.infrastructure.flyway.FlywayMigrateLocal")
}
