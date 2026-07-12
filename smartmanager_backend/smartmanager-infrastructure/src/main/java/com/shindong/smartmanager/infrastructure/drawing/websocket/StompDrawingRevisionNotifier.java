package com.shindong.smartmanager.infrastructure.drawing.websocket;

import com.shindong.smartmanager.application.drawing.DrawingRevisionNotifier;
import java.util.HashMap;
import java.util.Map;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.messaging.simp.SimpMessagingTemplate;
import org.springframework.stereotype.Service;

@Service
public class StompDrawingRevisionNotifier implements DrawingRevisionNotifier {

    private static final Logger log = LoggerFactory.getLogger(StompDrawingRevisionNotifier.class);

    private final SimpMessagingTemplate messagingTemplate;

    public StompDrawingRevisionNotifier(SimpMessagingTemplate messagingTemplate) {
        this.messagingTemplate = messagingTemplate;
    }

    @Override
    public void notifyMajorRevision(String partNo, int newMajorVersion) {
        log.info("도면 메이저 개정 알림 전송 - 품번: {}, 버전: V{}", partNo, newMajorVersion);

        Map<String, Object> payload = new HashMap<>();
        payload.put("type", "MAJOR_REVISION");
        payload.put("partNo", partNo);
        payload.put("newMajorVersion", newMajorVersion);
        payload.put("message", "도면이 개정되었습니다! 즉시 화면을 갱신합니다.");

        messagingTemplate.convertAndSend("/topic/drawings/" + partNo, payload);
    }
}
