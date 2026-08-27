package com.shindong.smartmanager.infrastructure.drawing.pdf;

import java.awt.Color;
import java.awt.Font;
import java.awt.FontMetrics;
import java.awt.Graphics2D;
import java.awt.RenderingHints;
import java.awt.image.BufferedImage;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import org.apache.pdfbox.Loader;
import org.apache.pdfbox.pdmodel.PDDocument;
import org.apache.pdfbox.pdmodel.PDPage;
import org.apache.pdfbox.pdmodel.PDPageContentStream;
import org.apache.pdfbox.pdmodel.graphics.image.LosslessFactory;
import org.apache.pdfbox.pdmodel.graphics.image.PDImageXObject;
import org.apache.pdfbox.pdmodel.graphics.state.PDExtendedGraphicsState;
import org.apache.pdfbox.util.Matrix;
import org.springframework.stereotype.Service;

@Service
public class DrawingPdfWatermarkService {

    /** 페이지 긴 변 대비 워터마크 폭 비율 (1.0 = 페이지와 동일 폭) */
    private static final float WIDTH_RATIO = 1.15f;
    private static final float ALPHA = 0.5f;
    private static final double ROTATION_DEG = 32;

    public byte[] addWatermark(String pdfFilePath, String watermarkText) throws IOException {
        Path path = Path.of(pdfFilePath);
        try (PDDocument document = Loader.loadPDF(Files.readAllBytes(path))) {
            BufferedImage image = createWatermarkImage(watermarkText);
            PDImageXObject pdImage = LosslessFactory.createFromImage(document, image);

            for (PDPage page : document.getPages()) {
                float pageWidth = page.getMediaBox().getWidth();
                float pageHeight = page.getMediaBox().getHeight();

                // 긴 변 기준으로 페이지보다 크게 그려 대각선에서도 글자가 크게 보이게 한다.
                float targetWidth = Math.max(pageWidth, pageHeight) * WIDTH_RATIO;
                float scale = targetWidth / pdImage.getWidth();
                float imgWidth = pdImage.getWidth() * scale;
                float imgHeight = pdImage.getHeight() * scale;
                float centerX = pageWidth / 2f;
                float centerY = pageHeight / 2f;

                try (PDPageContentStream contentStream = new PDPageContentStream(
                        document, page, PDPageContentStream.AppendMode.APPEND, true, true)) {

                    contentStream.saveGraphicsState();

                    PDExtendedGraphicsState extGState = new PDExtendedGraphicsState();
                    extGState.setNonStrokingAlphaConstant(ALPHA);
                    contentStream.setGraphicsStateParameters(extGState);

                    contentStream.transform(Matrix.getTranslateInstance(centerX, centerY));
                    contentStream.transform(Matrix.getRotateInstance(Math.toRadians(ROTATION_DEG), 0, 0));
                    contentStream.transform(Matrix.getTranslateInstance(-imgWidth / 2f, -imgHeight / 2f));

                    contentStream.drawImage(pdImage, 0, 0, imgWidth, imgHeight);
                    contentStream.restoreGraphicsState();
                }
            }

            ByteArrayOutputStream baos = new ByteArrayOutputStream();
            document.save(baos);
            return baos.toByteArray();
        }
    }

    private BufferedImage createWatermarkImage(String text) {
        int width = 3600;
        int height = 900;
        BufferedImage image = new BufferedImage(width, height, BufferedImage.TYPE_INT_ARGB);
        Graphics2D g2d = image.createGraphics();

        g2d.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
        g2d.setRenderingHint(RenderingHints.KEY_TEXT_ANTIALIASING, RenderingHints.VALUE_TEXT_ANTIALIAS_ON);
        g2d.setRenderingHint(RenderingHints.KEY_RENDERING, RenderingHints.VALUE_RENDER_QUALITY);
        g2d.setColor(new Color(220, 38, 38));

        int fontSize = 320;
        Font font = new Font("맑은 고딕", Font.BOLD, fontSize);
        g2d.setFont(font);
        FontMetrics fm = g2d.getFontMetrics();
        while (fontSize > 120 && fm.stringWidth(text) > width - 120) {
            fontSize -= 12;
            font = new Font("맑은 고딕", Font.BOLD, fontSize);
            g2d.setFont(font);
            fm = g2d.getFontMetrics();
        }

        int x = (width - fm.stringWidth(text)) / 2;
        int y = ((height - fm.getHeight()) / 2) + fm.getAscent();
        g2d.drawString(text, x, y);
        g2d.dispose();
        return image;
    }
}
