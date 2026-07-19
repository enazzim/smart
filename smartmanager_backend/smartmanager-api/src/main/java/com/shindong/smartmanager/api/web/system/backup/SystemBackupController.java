package com.shindong.smartmanager.api.web.system.backup;

import com.shindong.smartmanager.application.system.backup.BackupFileView;
import com.shindong.smartmanager.application.system.backup.FullBackupSetView;
import com.shindong.smartmanager.infrastructure.application.DatabaseBackupApplicationService;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;
import java.io.IOException;
import java.io.OutputStream;
import java.nio.file.FileVisitResult;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.SimpleFileVisitor;
import java.nio.file.attribute.BasicFileAttributes;
import java.time.Instant;
import java.util.List;
import java.util.zip.ZipEntry;
import java.util.zip.ZipOutputStream;
import org.springframework.core.io.FileSystemResource;
import org.springframework.core.io.Resource;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.servlet.mvc.method.annotation.StreamingResponseBody;

@RestController
@RequestMapping("/api/v1/system/backups")
public class SystemBackupController {

    private final DatabaseBackupApplicationService databaseBackupApplicationService;

    public SystemBackupController(DatabaseBackupApplicationService databaseBackupApplicationService) {
        this.databaseBackupApplicationService = databaseBackupApplicationService;
    }

    @GetMapping
    @PreAuthorize("hasAuthority('system:backup:read')")
    public List<BackupFileResponse> list() {
        return databaseBackupApplicationService.listBackups().stream()
                .map(BackupFileResponse::from)
                .toList();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('system:backup:execute')")
    public BackupFileResponse create(@Valid @RequestBody CreateBackupRequest request) {
        return BackupFileResponse.from(databaseBackupApplicationService.createBackup(request.reason()));
    }

    @DeleteMapping("/{fileName:.+}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('system:backup:execute')")
    public void delete(@PathVariable String fileName) {
        databaseBackupApplicationService.deleteBackup(fileName);
    }

    @PostMapping("/{fileName:.+}/restore")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('system:backup:execute')")
    public void restore(@PathVariable String fileName) {
        databaseBackupApplicationService.restoreBackup(fileName);
    }

    @GetMapping("/{fileName:.+}/download")
    @PreAuthorize("hasAuthority('system:backup:read')")
    public ResponseEntity<Resource> download(@PathVariable String fileName) {
        Path path = databaseBackupApplicationService.getBackupFilePath(fileName);
        Resource resource = new FileSystemResource(path);
        return ResponseEntity.ok()
                .header(HttpHeaders.CONTENT_DISPOSITION, "attachment; filename=\"" + fileName + "\"")
                .contentType(MediaType.APPLICATION_OCTET_STREAM)
                .body(resource);
    }

    @GetMapping("/full")
    @PreAuthorize("hasAuthority('system:backup:read')")
    public List<FullBackupSetResponse> listFull() {
        return databaseBackupApplicationService.listFullBackups().stream()
                .map(FullBackupSetResponse::from)
                .toList();
    }

    @PostMapping("/full")
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('system:backup:execute')")
    public FullBackupSetResponse createFull(@Valid @RequestBody CreateBackupRequest request) {
        return FullBackupSetResponse.from(databaseBackupApplicationService.createFullBackup(request.reason()));
    }

    @DeleteMapping("/full/{setName}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('system:backup:execute')")
    public void deleteFull(@PathVariable String setName) {
        databaseBackupApplicationService.deleteFullBackup(setName);
    }

    @PostMapping("/full/{setName}/restore")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('system:backup:execute')")
    public void restoreFull(@PathVariable String setName) {
        databaseBackupApplicationService.restoreFullBackup(setName);
    }

    @GetMapping("/full/{setName}/download")
    @PreAuthorize("hasAuthority('system:backup:read')")
    public ResponseEntity<StreamingResponseBody> downloadFull(@PathVariable String setName) {
        Path setDirectory = databaseBackupApplicationService.getFullBackupSetDirectory(setName);
        String zipFileName = setName + ".zip";
        StreamingResponseBody body = outputStream -> writeDirectoryAsZip(setDirectory, outputStream);
        return ResponseEntity.ok()
                .header(HttpHeaders.CONTENT_DISPOSITION, "attachment; filename=\"" + zipFileName + "\"")
                .contentType(MediaType.APPLICATION_OCTET_STREAM)
                .body(body);
    }

    private static void writeDirectoryAsZip(Path root, OutputStream outputStream) throws IOException {
        try (ZipOutputStream zip = new ZipOutputStream(outputStream)) {
            Files.walkFileTree(root, new SimpleFileVisitor<>() {
                @Override
                public FileVisitResult visitFile(Path file, BasicFileAttributes attrs) throws IOException {
                    String entryName = root.relativize(file).toString().replace('\\', '/');
                    zip.putNextEntry(new ZipEntry(entryName));
                    Files.copy(file, zip);
                    zip.closeEntry();
                    return FileVisitResult.CONTINUE;
                }
            });
        }
    }

    public record CreateBackupRequest(
            @NotBlank @Size(max = 500) String reason
    ) {
    }

    public record BackupFileResponse(String fileName, long fileSizeBytes, Instant createdAt, String reason) {
        static BackupFileResponse from(BackupFileView view) {
            return new BackupFileResponse(
                    view.fileName(),
                    view.fileSizeBytes(),
                    view.createdAt(),
                    view.reason()
            );
        }
    }

    public record FullBackupSetResponse(
            String setName,
            String sqlFileName,
            long totalSizeBytes,
            long drawingPdfFileCount,
            Instant createdAt,
            String reason
    ) {
        static FullBackupSetResponse from(FullBackupSetView view) {
            return new FullBackupSetResponse(
                    view.setName(),
                    view.sqlFileName(),
                    view.totalSizeBytes(),
                    view.drawingPdfFileCount(),
                    view.createdAt(),
                    view.reason()
            );
        }
    }
}
