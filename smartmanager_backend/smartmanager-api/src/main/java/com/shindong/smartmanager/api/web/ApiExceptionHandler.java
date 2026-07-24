package com.shindong.smartmanager.api.web;

import com.fasterxml.jackson.databind.exc.InvalidFormatException;
import com.shindong.smartmanager.application.common.AppBusinessException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.dao.DataIntegrityViolationException;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.http.converter.HttpMessageNotReadableException;
import org.springframework.security.access.AccessDeniedException;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RestControllerAdvice;

@RestControllerAdvice
public class ApiExceptionHandler {

    private static final Logger log = LoggerFactory.getLogger(ApiExceptionHandler.class);
    @ExceptionHandler(AccessDeniedException.class)
    public ResponseEntity<ApiErrorResponse> handleAccessDenied(AccessDeniedException ex) {
        String detail = ex.getMessage();
        String message = (detail != null && !detail.isBlank() && !detail.equalsIgnoreCase("Access Denied"))
                ? detail
                : "접근 권한이 없습니다.";
        return ResponseEntity.status(HttpStatus.FORBIDDEN)
                .body(new ApiErrorResponse("ACCESS_DENIED", message));
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
        } else if (detail != null && (detail.contains("fk_work_report_order") || detail.contains("fk_material_issue_wo"))) {
            message = "작업일보 또는 자재투입 이력이 있어 작업지시를 삭제할 수 없습니다. 하위 전표를 먼저 취소해 주세요.";
        } else if (detail != null && detail.contains("uk_month_closing_period")) {
            message = "동일 회계월 마감 데이터가 중복되어 처리할 수 없습니다. 마감해제 후 다시 시도해 주세요.";
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

    @ExceptionHandler(HttpMessageNotReadableException.class)
    public ResponseEntity<ApiErrorResponse> handleNotReadable(HttpMessageNotReadableException ex) {
        Throwable root = ex.getMostSpecificCause() != null ? ex.getMostSpecificCause() : ex;
        String message = "요청 본문을 읽을 수 없습니다.";
        if (root instanceof InvalidFormatException invalidFormat) {
            String field = invalidFormat.getPath().isEmpty()
                    ? ""
                    : invalidFormat.getPath().get(invalidFormat.getPath().size() - 1).getFieldName();
            String value = String.valueOf(invalidFormat.getValue());
            if (invalidFormat.getTargetType() != null
                    && invalidFormat.getTargetType().isEnum()
                    && "costType".equals(field)) {
                message = "단가구분은 SALE/PURCHASE/OUTSOURCE(또는 판매단가/구매단가/외주단가)여야 합니다: " + value;
            } else if (field != null && !field.isBlank()) {
                message = field + " 값이 올바르지 않습니다: " + value;
            } else if (root.getMessage() != null && !root.getMessage().isBlank()) {
                message = root.getMessage();
            }
        } else if (root.getMessage() != null && !root.getMessage().isBlank()) {
            message = root.getMessage();
        }
        return ResponseEntity.badRequest()
                .body(new ApiErrorResponse("INVALID_REQUEST", message));
    }

    @ExceptionHandler(Exception.class)
    public ResponseEntity<ApiErrorResponse> handleUnexpected(Exception ex) {
        log.error("Unhandled API exception", ex);
        Throwable root = ex;
        while (root.getCause() != null && root.getCause() != root) {
            root = root.getCause();
        }
        String detail = root.getClass().getSimpleName();
        if (root.getMessage() != null && !root.getMessage().isBlank()) {
            detail = detail + ": " + root.getMessage();
        }
        return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                .body(new ApiErrorResponse(
                        "INTERNAL_ERROR",
                        "요청 처리 중 오류가 발생했습니다: " + detail
                ));
    }
}
