package com.shindong.smartmanager.application.auth;

public record ChangePasswordCommand(String currentPassword, String newPassword) {
}
