package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.system.backup.BackupFileView;
import com.shindong.smartmanager.application.system.backup.FullBackupSetView;
import com.shindong.smartmanager.infrastructure.system.FullBackupService;
import com.shindong.smartmanager.infrastructure.system.MariaDbBackupService;
import java.util.List;
import org.springframework.stereotype.Service;

@Service
public class DatabaseBackupApplicationService {

    private final MariaDbBackupService mariaDbBackupService;
    private final FullBackupService fullBackupService;

    public DatabaseBackupApplicationService(
            MariaDbBackupService mariaDbBackupService,
            FullBackupService fullBackupService
    ) {
        this.mariaDbBackupService = mariaDbBackupService;
        this.fullBackupService = fullBackupService;
    }

    public List<BackupFileView> listBackups() {
        return mariaDbBackupService.listBackups();
    }

    public BackupFileView createBackup(String reason) {
        return mariaDbBackupService.createBackup(reason);
    }

    public void deleteBackup(String fileName) {
        mariaDbBackupService.deleteBackup(fileName);
    }

    public void restoreBackup(String fileName) {
        mariaDbBackupService.restoreBackup(fileName);
    }

    public java.nio.file.Path getBackupFilePath(String fileName) {
        return mariaDbBackupService.getBackupFilePath(fileName);
    }

    public List<FullBackupSetView> listFullBackups() {
        return fullBackupService.listFullBackups();
    }

    public FullBackupSetView createFullBackup() {
        return fullBackupService.createFullBackup();
    }

    public void restoreFullBackup(String setName) {
        fullBackupService.restoreFullBackup(setName);
    }

    public void deleteFullBackup(String setName) {
        fullBackupService.deleteFullBackup(setName);
    }
}
