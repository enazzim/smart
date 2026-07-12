package com.shindong.smartmanager.infrastructure.drawing.pdf;

import com.shindong.smartmanager.infrastructure.config.DrawingProperties;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.UUID;
import org.apache.pdfbox.Loader;
import org.apache.pdfbox.pdmodel.PDDocument;
import org.springframework.stereotype.Service;

@Service
public class DrawingPdfStorageService {

    private final Path storageDir;

    public DrawingPdfStorageService(DrawingProperties drawingProperties) {
        this.storageDir = Path.of(drawingProperties.storageDir()).toAbsolutePath().normalize();
        try {
            Files.createDirectories(this.storageDir);
        } catch (IOException ex) {
            throw new IllegalStateException("도면 저장 디렉터리를 생성할 수 없습니다: " + this.storageDir, ex);
        }
    }

    public String savePdf(byte[] content) throws IOException {
        String fileName = UUID.randomUUID() + ".pdf";
        Path targetPath = storageDir.resolve(fileName);
        try (PDDocument document = Loader.loadPDF(content)) {
            document.save(targetPath.toFile());
        }
        return targetPath.toString();
    }
}
