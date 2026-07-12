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
import org.springframework.stereotype.Service;

@Service
public class DrawingPdfWatermarkService {

    public byte[] addWatermark(String pdfFilePath, String watermarkText) throws IOException {
        Path path = Path.of(pdfFilePath);
        try (PDDocument document = Loader.loadPDF(Files.readAllBytes(path))) {
            BufferedImage image = createWatermarkImage(watermarkText);
            PDImageXObject pdImage = LosslessFactory.createFromImage(document, image);

            for (PDPage page : document.getPages()) {
                float pageWidth = page.getMediaBox().getWidth();
                float pageHeight = page.getMediaBox().getHeight();

                float imgWidth = pdImage.getWidth() * 0.5f;
                float imgHeight = pdImage.getHeight() * 0.5f;
                float x = (pageWidth - imgWidth) / 2;
                float y = (pageHeight - imgHeight) / 2;

                try (PDPageContentStream contentStream = new PDPageContentStream(
                        document, page, PDPageContentStream.AppendMode.APPEND, true, true)) {

                    contentStream.saveGraphicsState();

                    PDExtendedGraphicsState extGState = new PDExtendedGraphicsState();
                    extGState.setNonStrokingAlphaConstant(0.4f);
                    contentStream.setGraphicsStateParameters(extGState);

                    contentStream.transform(
                            org.apache.pdfbox.util.Matrix.getTranslateInstance(x + imgWidth / 2, y + imgHeight / 2));
                    contentStream.transform(
                            org.apache.pdfbox.util.Matrix.getRotateInstance(Math.toRadians(30), 0, 0));
                    contentStream.transform(
                            org.apache.pdfbox.util.Matrix.getTranslateInstance(-imgWidth / 2, -imgHeight / 2));

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
        int width = 1200;
        int height = 200;
        BufferedImage image = new BufferedImage(width, height, BufferedImage.TYPE_INT_ARGB);
        Graphics2D g2d = image.createGraphics();

        g2d.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
        g2d.setRenderingHint(RenderingHints.KEY_TEXT_ANTIALIASING, RenderingHints.VALUE_TEXT_ANTIALIAS_ON);
        g2d.setColor(new Color(239, 68, 68));
        g2d.setFont(new Font("맑은 고딕", Font.BOLD, 80));

        FontMetrics fm = g2d.getFontMetrics();
        int x = (width - fm.stringWidth(text)) / 2;
        int y = ((height - fm.getHeight()) / 2) + fm.getAscent();

        g2d.drawString(text, x, y);
        g2d.dispose();
        return image;
    }
}
