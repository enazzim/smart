package com.shindong.smartmanager.api.web.system.backup;

import com.shindong.smartmanager.application.system.backup.BackupFileView;
import com.shindong.smartmanager.infrastructure.application.DatabaseBackupApplicationService;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;
import java.nio.file.Path;
import java.time.Instant;
import java.util.List;
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
}
