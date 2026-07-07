package com.shindong.smartmanager.infrastructure.board;

import com.shindong.smartmanager.application.board.BoardFileStorage;
import com.shindong.smartmanager.domain.board.BoardType;
import com.shindong.smartmanager.infrastructure.config.BoardProperties;
import java.io.IOException;
import java.io.InputStream;
import java.nio.file.Files;
import java.nio.file.Path;
import java.time.LocalDate;
import java.util.Locale;
import java.util.Optional;
import java.util.UUID;
import org.springframework.stereotype.Component;

@Component
public class FileSystemBoardFileStorage implements BoardFileStorage {

    private final Path rootDirectory;

    public FileSystemBoardFileStorage(BoardProperties boardProperties) {
        this.rootDirectory = resolveRootDirectory(boardProperties.filesDir());
        createRootDirectoryIfNeeded();
    }

    @Override
    public StoredFile store(
            BoardType boardType,
            String originalFileName,
            String contentType,
            long fileSize,
            InputStream inputStream
    ) {
        String extension = extractExtension(originalFileName);
        String storedFileName = UUID.randomUUID() + (extension.isEmpty() ? "" : "." + extension);
        LocalDate today = LocalDate.now();
        Path directory = rootDirectory
                .resolve(boardType.name().toLowerCase(Locale.ROOT))
                .resolve(String.valueOf(today.getYear()))
                .resolve(String.format("%02d", today.getMonthValue()));
        try {
            Files.createDirectories(directory);
            Path target = directory.resolve(storedFileName);
            Files.copy(inputStream, target);
            return new StoredFile(relativePath(boardType, today, storedFileName), contentType, fileSize);
        } catch (IOException ex) {
            throw new IllegalStateException("첨부파일 저장에 실패했습니다.", ex);
        }
    }

    @Override
    public Optional<InputStream> open(BoardType boardType, String storedFileName) {
        Path path = rootDirectory.resolve(storedFileName);
        if (!Files.exists(path)) {
            return Optional.empty();
        }
        try {
            return Optional.of(Files.newInputStream(path));
        } catch (IOException ex) {
            throw new IllegalStateException("첨부파일을 열 수 없습니다.", ex);
        }
    }

    @Override
    public void deletePhysical(BoardType boardType, String storedFileName) {
        Path path = rootDirectory.resolve(storedFileName);
        try {
            Files.deleteIfExists(path);
        } catch (IOException ex) {
            throw new IllegalStateException("첨부파일 삭제에 실패했습니다.", ex);
        }
    }

    private Path resolveRootDirectory(String configuredPath) {
        Path path = Path.of(configuredPath);
        if (!path.isAbsolute()) {
            path = Path.of(System.getProperty("user.dir")).resolve(path).normalize();
        }
        return path;
    }

    private void createRootDirectoryIfNeeded() {
        try {
            Files.createDirectories(rootDirectory);
        } catch (IOException ex) {
            throw new IllegalStateException("첨부파일 저장 경로를 생성할 수 없습니다: " + rootDirectory, ex);
        }
    }

    private String relativePath(BoardType boardType, LocalDate date, String storedFileName) {
        return boardType.name().toLowerCase(Locale.ROOT)
                + "/" + date.getYear()
                + "/" + String.format("%02d", date.getMonthValue())
                + "/" + storedFileName;
    }

    private String extractExtension(String fileName) {
        if (fileName == null) {
            return "";
        }
        int dot = fileName.lastIndexOf('.');
        if (dot < 0 || dot == fileName.length() - 1) {
            return "";
        }
        return fileName.substring(dot + 1).toLowerCase(Locale.ROOT);
    }
}
