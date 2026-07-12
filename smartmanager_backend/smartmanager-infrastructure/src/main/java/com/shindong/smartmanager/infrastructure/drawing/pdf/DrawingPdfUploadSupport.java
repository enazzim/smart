package com.shindong.smartmanager.infrastructure.drawing.pdf;

import com.shindong.smartmanager.infrastructure.config.DrawingProperties;
import org.springframework.stereotype.Component;
import org.springframework.web.multipart.MultipartFile;

@Component
public class DrawingPdfUploadSupport {

    private final long maxFileSizeBytes;

    public DrawingPdfUploadSupport(DrawingProperties drawingProperties) {
        this.maxFileSizeBytes = drawingProperties.maxFileSizeBytes();
    }

    public void validate(MultipartFile file) {
        if (file == null || file.isEmpty()) {
            throw new IllegalArgumentException("PDF 파일이 필요합니다.");
        }
        String originalName = file.getOriginalFilename();
        if (originalName == null || !originalName.toLowerCase().endsWith(".pdf")) {
            throw new IllegalArgumentException("PDF 파일만 업로드 가능합니다.");
        }
        if (file.getSize() > maxFileSizeBytes) {
            throw new IllegalArgumentException("도면 PDF는 100MB 이하여야 합니다.");
        }
    }
}
