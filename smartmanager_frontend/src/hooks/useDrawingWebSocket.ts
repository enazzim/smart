import { useEffect, useState } from 'react';
import { Client } from '@stomp/stompjs';

export interface DrawingRevisionPayload {
  type: string;
  partNo: string;
  newMajorVersion: number;
  message: string;
}

export function useDrawingWebSocket(partNo: string) {
  const [isRevisionAlertOpen, setIsRevisionAlertOpen] = useState(false);
  const [revisionData, setRevisionData] = useState<DrawingRevisionPayload | null>(null);

  useEffect(() => {
    if (!partNo) {
      return;
    }

    let client: Client | null = null;
    let disposed = false;

    void (async () => {
      const { default: SockJS } = await import('sockjs-client');
      if (disposed) {
        return;
      }

      client = new Client({
        webSocketFactory: () => new SockJS('/ws-drawing'),
        reconnectDelay: 5000,
        onConnect: () => {
          client?.subscribe(`/topic/drawings/${partNo}`, (message) => {
            if (!message.body) {
              return;
            }
            const payload = JSON.parse(message.body) as DrawingRevisionPayload;
            if (payload.type === 'MAJOR_REVISION') {
              setRevisionData(payload);
              setIsRevisionAlertOpen(true);
            }
          });
        },
        onStompError: (frame) => {
          console.error('STOMP Error:', frame.headers.message);
        },
      });

      client.activate();
    })();

    return () => {
      disposed = true;
      void client?.deactivate();
    };
  }, [partNo]);

  const dismissRevisionAlert = () => {
    setIsRevisionAlertOpen(false);
  };

  const forceReload = () => {
    setIsRevisionAlertOpen(false);
    window.location.reload();
  };

  return {
    isRevisionAlertOpen,
    revisionData,
    dismissRevisionAlert,
    forceReload,
  };
}
