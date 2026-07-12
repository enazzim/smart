package com.shindong.smartmanager.infrastructure.system;

import com.shindong.smartmanager.application.system.backup.FullBackupSetView;
import com.shindong.smartmanager.infrastructure.config.DrawingProperties;
import java.io.IOException;
import java.nio.file.FileVisitResult;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.SimpleFileVisitor;
import java.nio.file.StandardCopyOption;
import java.nio.file.attribute.BasicFileAttributes;
import java.time.Instant;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;
import java.util.Locale;
import java.util.regex.Pattern;
import java.util.stream.Stream;
import org.springframework.stereotype.Component;

@Component
public class FullBackupService {

    public static final String SET_PREFIX = "full_";
    public static final String DRAWING_PDF_DIR_NAME = "drawing-pdf";

    private static final DateTimeFormatter STAMP_FORMAT = DateTimeFormatter.ofPattern("yyyyMMdd_HHmmss");
    private static final Pattern SET_NAME_PATTERN = Pattern.compile("^full_\\d{8}_\\d{6}$");

    private final MariaDbBackupService mariaDbBackupService;
    private final Path drawingStorageDir;

    public FullBackupService(MariaDbBackupService mariaDbBackupService, DrawingProperties drawingProperties) {
        this.mariaDbBackupService = mariaDbBackupService;
        this.drawingStorageDir = resolveDirectory(drawingProperties.storageDir());
    }

    public List<FullBackupSetView> listFullBackups() {
        Path backupDirectory = mariaDbBackupService.getBackupDirectory();
        try {
            if (!Files.exists(backupDirectory)) {
                return List.of();
            }
            List<FullBackupSetView> sets = new ArrayList<>();
            try (Stream<Path> stream = Files.list(backupDirectory)) {
                stream.filter(Files::isDirectory)
                        .map(path -> path.getFileName().toString())
                        .filter(name -> SET_NAME_PATTERN.matcher(name).matches())
                        .forEach(name -> sets.add(toView(backupDirectory.resolve(name), name)));
            }
            sets.sort(Comparator.comparing(FullBackupSetView::createdAt).reversed());
            return sets;
        } catch (IOException ex) {
            throw new IllegalStateException("전체 백업 목록 조회에 실패했습니다.", ex);
        }
    }

    public FullBackupSetView createFullBackup() {
        String stamp = LocalDateTime.now().format(STAMP_FORMAT);
        String setName = SET_PREFIX + stamp;
        Path setDirectory = mariaDbBackupService.getBackupDirectory().resolve(setName).normalize();
        Path sqlTarget = setDirectory.resolve("smartmanager_" + stamp + ".sql");
        try {
            Files.createDirectories(setDirectory);
            mariaDbBackupService.createBackupAt(sqlTarget);
            copyDrawingPdfsToBackup(setDirectory.resolve(DRAWING_PDF_DIR_NAME));
            return toView(setDirectory, setName);
        } catch (RuntimeException | IOException ex) {
            deleteDirectoryQuietly(setDirectory);
            if (ex instanceof RuntimeException runtimeException) {
                throw runtimeException;
            }
            throw new IllegalStateException("전체 백업 생성에 실패했습니다.", ex);
        }
    }

    public void restoreFullBackup(String setName) {
        Path setDirectory = resolveFullBackupSet(setName);
        Path sqlFile = findSqlFile(setDirectory)
                .orElseThrow(() -> new IllegalArgumentException("백업 세트에 SQL 파일이 없습니다: " + setName));
        mariaDbBackupService.restoreBackupFromPath(sqlFile);
        restoreDrawingPdfsFromBackup(setDirectory.resolve(DRAWING_PDF_DIR_NAME));
    }

    public void deleteFullBackup(String setName) {
        Path setDirectory = resolveFullBackupSet(setName);
        try {
            deleteDirectory(setDirectory);
        } catch (IOException ex) {
            throw new IllegalStateException("전체 백업 세트 삭제에 실패했습니다: " + setName, ex);
        }
    }

    private void copyDrawingPdfsToBackup(Path targetDir) throws IOException {
        Files.createDirectories(targetDir);
        if (!Files.exists(drawingStorageDir)) {
            return;
        }
        try (Stream<Path> stream = Files.list(drawingStorageDir)) {
            for (Path source : stream.filter(Files::isRegularFile).toList()) {
                Files.copy(source, targetDir.resolve(source.getFileName()), StandardCopyOption.REPLACE_EXISTING);
            }
        }
    }

    private void restoreDrawingPdfsFromBackup(Path sourceDir) {
        try {
            Files.createDirectories(drawingStorageDir);
            clearRegularFiles(drawingStorageDir);
            if (!Files.exists(sourceDir) || !Files.isDirectory(sourceDir)) {
                return;
            }
            try (Stream<Path> stream = Files.list(sourceDir)) {
                for (Path source : stream.filter(Files::isRegularFile).toList()) {
                    Files.copy(
                            source,
                            drawingStorageDir.resolve(source.getFileName()),
                            StandardCopyOption.REPLACE_EXISTING
                    );
                }
            }
        } catch (IOException ex) {
            throw new IllegalStateException("도면 PDF 복구에 실패했습니다.", ex);
        }
    }

    private static void clearRegularFiles(Path directory) throws IOException {
        try (Stream<Path> stream = Files.list(directory)) {
            for (Path path : stream.filter(Files::isRegularFile).toList()) {
                Files.deleteIfExists(path);
            }
        }
    }

    private Path resolveFullBackupSet(String setName) {
        validateSetName(setName);
        Path setDirectory = mariaDbBackupService.getBackupDirectory().resolve(setName).normalize();
        if (!setDirectory.startsWith(mariaDbBackupService.getBackupDirectory())) {
            throw new IllegalArgumentException("잘못된 백업 세트 이름입니다.");
        }
        if (!Files.exists(setDirectory) || !Files.isDirectory(setDirectory)) {
            throw new IllegalArgumentException("백업 세트를 찾을 수 없습니다: " + setName);
        }
        return setDirectory;
    }

    private static void validateSetName(String setName) {
        if (setName == null || setName.isBlank() || !SET_NAME_PATTERN.matcher(setName).matches()) {
            throw new IllegalArgumentException("잘못된 백업 세트 이름입니다.");
        }
    }

    private static java.util.Optional<Path> findSqlFile(Path setDirectory) throws IllegalStateException {
        try (Stream<Path> stream = Files.list(setDirectory)) {
            return stream
                    .filter(Files::isRegularFile)
                    .filter(path -> path.getFileName().toString().toLowerCase(Locale.ROOT).endsWith(".sql"))
                    .findFirst();
        } catch (IOException ex) {
            throw new IllegalStateException("백업 SQL 파일 조회에 실패했습니다.", ex);
        }
    }

    private FullBackupSetView toView(Path setDirectory, String setName) {
        try {
            Path sqlFile = findSqlFile(setDirectory)
                    .orElseThrow(() -> new IllegalStateException("백업 세트에 SQL 파일이 없습니다: " + setName));
            Path drawingDir = setDirectory.resolve(DRAWING_PDF_DIR_NAME);
            long pdfCount = 0;
            if (Files.exists(drawingDir) && Files.isDirectory(drawingDir)) {
                try (Stream<Path> stream = Files.list(drawingDir)) {
                    pdfCount = stream.filter(Files::isRegularFile).count();
                }
            }
            return new FullBackupSetView(
                    setName,
                    sqlFile.getFileName().toString(),
                    directorySize(setDirectory),
                    pdfCount,
                    Files.getLastModifiedTime(setDirectory).toInstant()
            );
        } catch (IOException ex) {
            throw new IllegalStateException("전체 백업 정보를 읽을 수 없습니다: " + setName, ex);
        }
    }

    private static long directorySize(Path root) throws IOException {
        final long[] total = {0L};
        Files.walkFileTree(root, new SimpleFileVisitor<>() {
            @Override
            public FileVisitResult visitFile(Path file, BasicFileAttributes attrs) {
                total[0] += attrs.size();
                return FileVisitResult.CONTINUE;
            }
        });
        return total[0];
    }

    private static void deleteDirectory(Path directory) throws IOException {
        if (!Files.exists(directory)) {
            return;
        }
        Files.walkFileTree(directory, new SimpleFileVisitor<>() {
            @Override
            public FileVisitResult visitFile(Path file, BasicFileAttributes attrs) throws IOException {
                Files.delete(file);
                return FileVisitResult.CONTINUE;
            }

            @Override
            public FileVisitResult postVisitDirectory(Path dir, IOException exc) throws IOException {
                Files.delete(dir);
                return FileVisitResult.CONTINUE;
            }
        });
    }

    private static void deleteDirectoryQuietly(Path directory) {
        try {
            deleteDirectory(directory);
        } catch (IOException ignored) {
            // ignore cleanup failure
        }
    }

    private static Path resolveDirectory(String configuredPath) {
        Path path = Path.of(configuredPath);
        if (!path.isAbsolute()) {
            path = Path.of(System.getProperty("user.dir")).resolve(path).normalize();
        }
        return path;
    }
}
