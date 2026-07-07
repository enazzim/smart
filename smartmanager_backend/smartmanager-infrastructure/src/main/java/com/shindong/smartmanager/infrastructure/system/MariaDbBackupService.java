package com.shindong.smartmanager.infrastructure.system;

import com.shindong.smartmanager.application.system.backup.BackupFileView;
import com.shindong.smartmanager.infrastructure.config.BackupProperties;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;
import java.util.Locale;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

@Component
public class MariaDbBackupService {

    private static final Pattern JDBC_URL_PATTERN = Pattern.compile(
            "jdbc:mariadb://([^:/]+)(?::(\\d+))?/([^?;]+)"
    );
    private static final DateTimeFormatter FILE_NAME_FORMAT = DateTimeFormatter.ofPattern("yyyyMMdd_HHmmss");

    private final Path backupDirectory;
    private final String jdbcUrl;
    private final String username;
    private final String password;
    private final String mariadbBinDir;

    public MariaDbBackupService(
            BackupProperties backupProperties,
            @Value("${spring.datasource.url}") String jdbcUrl,
            @Value("${spring.datasource.username}") String username,
            @Value("${spring.datasource.password}") String password
    ) {
        this.backupDirectory = resolveDirectory(backupProperties.dir());
        this.jdbcUrl = jdbcUrl;
        this.username = username;
        this.password = password;
        this.mariadbBinDir = backupProperties.mariadbBinDir();
        createDirectoryIfNeeded();
    }

    public List<BackupFileView> listBackups() {
        try {
            if (!Files.exists(backupDirectory)) {
                return List.of();
            }
            List<BackupFileView> files = new ArrayList<>();
            try (var stream = Files.list(backupDirectory)) {
                stream.filter(path -> path.getFileName().toString().toLowerCase(Locale.ROOT).endsWith(".sql"))
                        .forEach(path -> files.add(toView(path)));
            }
            files.sort(Comparator.comparing(BackupFileView::createdAt).reversed());
            return files;
        } catch (IOException ex) {
            throw new IllegalStateException("백업 목록 조회에 실패했습니다.", ex);
        }
    }

    public BackupFileView createBackup() {
        String fileName = "smartmanager_" + LocalDateTime.now().format(FILE_NAME_FORMAT) + ".sql";
        Path target = backupDirectory.resolve(fileName);
        JdbcTarget targetDb = parseJdbcUrl(jdbcUrl);
        String dumpExecutable = resolveExecutable("mariadb-dump");
        if (!Files.isRegularFile(Path.of(dumpExecutable)) && !isOnPath(dumpExecutable)) {
            throw new IllegalStateException(
                    "mariadb-dump를 찾을 수 없습니다. MariaDB bin 경로를 smartmanager.backup.mariadb-bin-dir 에 설정해 주세요."
            );
        }
        List<String> command = new ArrayList<>();
        command.add(dumpExecutable);
        command.add("-h");
        command.add(targetDb.host());
        if (targetDb.port() != null) {
            command.add("-P");
            command.add(targetDb.port());
        }
        command.add("-u");
        command.add(username);
        command.add("--password=" + password);
        command.add("--single-transaction");
        command.add("--routines");
        command.add("--events");
        command.add(targetDb.database());
        try {
            runToFile(command, target);
            return toView(target);
        } catch (RuntimeException ex) {
            try {
                Files.deleteIfExists(target);
            } catch (IOException ignored) {
                // ignore cleanup failure
            }
            throw ex;
        }
    }

    public void deleteBackup(String fileName) {
        Path path = resolveBackupFile(fileName);
        try {
            Files.deleteIfExists(path);
        } catch (IOException ex) {
            throw new IllegalStateException("백업 파일 삭제에 실패했습니다: " + fileName, ex);
        }
    }

    public void restoreBackup(String fileName) {
        Path path = resolveBackupFile(fileName);
        JdbcTarget targetDb = parseJdbcUrl(jdbcUrl);
        List<String> command = new ArrayList<>();
        command.add(resolveExecutable("mariadb"));
        command.add("-h");
        command.add(targetDb.host());
        if (targetDb.port() != null) {
            command.add("-P");
            command.add(targetDb.port());
        }
        command.add("-u");
        command.add(username);
        command.add("--password=" + password);
        command.add(targetDb.database());
        runFromFile(command, path);
    }

    public Path getBackupFilePath(String fileName) {
        return resolveBackupFile(fileName);
    }

    private Path resolveBackupFile(String fileName) {
        validateFileName(fileName);
        Path path = backupDirectory.resolve(fileName).normalize();
        if (!path.startsWith(backupDirectory)) {
            throw new IllegalArgumentException("잘못된 백업 파일명입니다.");
        }
        if (!Files.exists(path)) {
            throw new IllegalArgumentException("백업 파일을 찾을 수 없습니다: " + fileName);
        }
        return path;
    }

    private static void validateFileName(String fileName) {
        if (fileName == null || fileName.isBlank()) {
            throw new IllegalArgumentException("백업 파일명이 필요합니다.");
        }
        if (fileName.contains("..") || fileName.contains("/") || fileName.contains("\\")) {
            throw new IllegalArgumentException("잘못된 백업 파일명입니다.");
        }
        if (!fileName.toLowerCase(Locale.ROOT).endsWith(".sql")) {
            throw new IllegalArgumentException("SQL 백업 파일만 처리할 수 있습니다.");
        }
    }

    private void runToFile(List<String> command, Path target) {
        ProcessBuilder builder = new ProcessBuilder(command);
        builder.redirectOutput(target.toFile());
        builder.redirectError(ProcessBuilder.Redirect.PIPE);
        runProcess(builder, "백업 생성");
    }

    private void runFromFile(List<String> command, Path source) {
        ProcessBuilder builder = new ProcessBuilder(command);
        builder.redirectInput(source.toFile());
        builder.redirectError(ProcessBuilder.Redirect.PIPE);
        runProcess(builder, "백업 복구");
    }

    private void runProcess(ProcessBuilder builder, String actionLabel) {
        try {
            Process process = builder.start();
            String errorOutput = new String(process.getErrorStream().readAllBytes());
            int exitCode = process.waitFor();
            if (exitCode != 0) {
                throw new IllegalStateException(
                        actionLabel + "에 실패했습니다. (exit=" + exitCode + ") " + errorOutput.trim()
                );
            }
        } catch (IOException ex) {
            throw new IllegalStateException(
                    actionLabel + " 명령을 실행할 수 없습니다. MariaDB 클라이언트(mariadb-dump/mariadb) 설치 및 PATH를 확인해 주세요.",
                    ex
            );
        } catch (InterruptedException ex) {
            Thread.currentThread().interrupt();
            throw new IllegalStateException(actionLabel + " 중 오류가 발생했습니다.", ex);
        }
    }

    private static boolean isOnPath(String executable) {
        return !executable.contains("/") && !executable.contains("\\");
    }

    private String resolveExecutable(String name) {
        String suffix = System.getProperty("os.name", "").toLowerCase(Locale.ROOT).contains("win") ? ".exe" : "";
        if (mariadbBinDir != null && !mariadbBinDir.isBlank()) {
            return Path.of(mariadbBinDir, name + suffix).toString();
        }
        Path fromPath = findOnWindows(name, suffix);
        if (fromPath != null) {
            return fromPath.toString();
        }
        return name;
    }

    private static Path findOnWindows(String name, String suffix) {
        if (!System.getProperty("os.name", "").toLowerCase(Locale.ROOT).contains("win")) {
            return null;
        }
        String fileName = name + suffix;
        List<Path> roots = List.of(
                Path.of("C:/Program Files/MariaDB 11.4/bin"),
                Path.of("C:/Program Files/MariaDB 11.3/bin"),
                Path.of("C:/Program Files/MariaDB 11.2/bin"),
                Path.of("C:/Program Files/MariaDB 10.11/bin"),
                Path.of("C:/Program Files (x86)/MariaDB 11.4/bin")
        );
        for (Path root : roots) {
            Path candidate = root.resolve(fileName);
            if (Files.isRegularFile(candidate)) {
                return candidate;
            }
        }
        try {
            Path programFiles = Path.of("C:/Program Files");
            if (Files.isDirectory(programFiles)) {
                try (var stream = Files.list(programFiles)) {
                    return stream
                            .filter(path -> path.getFileName().toString().toLowerCase(Locale.ROOT).startsWith("mariadb"))
                            .map(path -> path.resolve("bin").resolve(fileName))
                            .filter(Files::isRegularFile)
                            .findFirst()
                            .orElse(null);
                }
            }
        } catch (IOException ignored) {
            return null;
        }
        return null;
    }

    private BackupFileView toView(Path path) {
        try {
            return new BackupFileView(
                    path.getFileName().toString(),
                    Files.size(path),
                    Files.getLastModifiedTime(path).toInstant()
            );
        } catch (IOException ex) {
            throw new IllegalStateException("백업 파일 정보를 읽을 수 없습니다.", ex);
        }
    }

    private static JdbcTarget parseJdbcUrl(String url) {
        Matcher matcher = JDBC_URL_PATTERN.matcher(url);
        if (!matcher.find()) {
            throw new IllegalStateException("JDBC URL을 파싱할 수 없습니다: " + url);
        }
        return new JdbcTarget(matcher.group(1), matcher.group(2), matcher.group(3));
    }

    private static Path resolveDirectory(String configuredPath) {
        Path path = Path.of(configuredPath);
        if (!path.isAbsolute()) {
            path = Path.of(System.getProperty("user.dir")).resolve(path).normalize();
        }
        return path;
    }

    private void createDirectoryIfNeeded() {
        try {
            Files.createDirectories(backupDirectory);
        } catch (IOException ex) {
            throw new IllegalStateException("백업 디렉터리를 생성할 수 없습니다: " + backupDirectory, ex);
        }
    }

    private record JdbcTarget(String host, String port, String database) {
    }
}
