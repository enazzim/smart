package com.shindong.smartmanager.api.web;

import com.shindong.smartmanager.application.common.AppBusinessException;
import org.springframework.dao.DataIntegrityViolationException;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.AccessDeniedException;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RestControllerAdvice;

@RestControllerAdvice
public class ApiExceptionHandler {

    @ExceptionHandler(AccessDeniedException.class)
    public ResponseEntity<ApiErrorResponse> handleAccessDenied(AccessDeniedException ex) {
        return ResponseEntity.status(HttpStatus.FORBIDDEN)
                .body(new ApiErrorResponse("ACCESS_DENIED", ex.getMessage()));
    }

    @ExceptionHandler(AppBusinessException.class)
    public ResponseEntity<ApiErrorResponse> handleApiBusiness(AppBusinessException ex) {
        return ResponseEntity.badRequest()
                .body(new ApiErrorResponse(ex.errorCode().name(), ex.getMessage()));
    }

    @ExceptionHandler(DataIntegrityViolationException.class)
    public ResponseEntity<ApiErrorResponse> handleDataIntegrity(DataIntegrityViolationException ex) {
        String detail = ex.getMostSpecificCause() != null ? ex.getMostSpecificCause().getMessage() : ex.getMessage();
        String message;
        if (detail != null && detail.contains("uk_purchase_receipt_no")) {
            message = "동일 입고번호의 취소 이력이 있어 입고를 취소할 수 없습니다. 관리자에게 문의해 주세요.";
        } else if (detail != null && detail.contains("uk_work_plan")) {
            message = "동일 생산계획·공정의 작업계획이 이미 존재합니다. 목록에서 취소 후 다시 시도해 주세요.";
        } else if (detail != null && detail.contains("uk_work_order")) {
            message = "동일 작업계획의 작업지시가 이미 존재합니다. 목록에서 취소 후 다시 시도해 주세요.";
        } else {
            message = "이미 등록된 데이터와 충돌하여 요청을 처리할 수 없습니다.";
        }
        return ResponseEntity.status(HttpStatus.CONFLICT)
                .body(new ApiErrorResponse("DATA_CONFLICT", message));
    }

    @ExceptionHandler(IllegalArgumentException.class)
    public ResponseEntity<ApiErrorResponse> handleIllegalArgument(IllegalArgumentException ex) {
        return ResponseEntity.badRequest()
                .body(new ApiErrorResponse("INVALID_REQUEST", ex.getMessage()));
    }

    @ExceptionHandler(IllegalStateException.class)
    public ResponseEntity<ApiErrorResponse> handleIllegalState(IllegalStateException ex) {
        return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                .body(new ApiErrorResponse("INVALID_STATE", ex.getMessage()));
    }

    @ExceptionHandler(MethodArgumentNotValidException.class)
    public ResponseEntity<ApiErrorResponse> handleValidation(MethodArgumentNotValidException ex) {
        String message = ex.getBindingResult().getFieldErrors().stream()
                .findFirst()
                .map(error -> error.getField() + ": " + error.getDefaultMessage())
                .orElse("요청 값이 올바르지 않습니다.");
        return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                .body(new ApiErrorResponse("VALIDATION_FAILED", message));
    }
}
