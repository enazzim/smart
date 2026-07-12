import { drawingPdfUrl, fetchDrawingPdfBlob } from '../api/drawing';

const DB_NAME = 'smartmanager-drawing-offline';
const STORE_NAME = 'pdfs';

interface CachedDrawingRecord {
  partNo: string;
  blob: Blob;
  cachedAt: string;
}

function openDatabase(): Promise<IDBDatabase> {
  return new Promise((resolve, reject) => {
    const request = indexedDB.open(DB_NAME, 1);
    request.onupgradeneeded = () => {
      const db = request.result;
      if (!db.objectStoreNames.contains(STORE_NAME)) {
        db.createObjectStore(STORE_NAME, { keyPath: 'partNo' });
      }
    };
    request.onsuccess = () => resolve(request.result);
    request.onerror = () => reject(request.error ?? new Error('IndexedDB open failed'));
  });
}

function runTransaction<T>(
  mode: IDBTransactionMode,
  handler: (store: IDBObjectStore) => IDBRequest<T>,
): Promise<T> {
  return openDatabase().then(
    (db) =>
      new Promise<T>((resolve, reject) => {
        const transaction = db.transaction(STORE_NAME, mode);
        const store = transaction.objectStore(STORE_NAME);
        const request = handler(store);
        request.onsuccess = () => resolve(request.result);
        request.onerror = () => reject(request.error ?? new Error('IndexedDB request failed'));
        transaction.oncomplete = () => db.close();
        transaction.onerror = () => reject(transaction.error ?? new Error('IndexedDB transaction failed'));
      }),
  );
}

export async function cacheDrawingPdf(partNo: string, blob: Blob): Promise<void> {
  const record: CachedDrawingRecord = {
    partNo,
    blob,
    cachedAt: new Date().toISOString(),
  };
  await runTransaction('readwrite', (store) => store.put(record));
}

export async function getCachedDrawingPdf(partNo: string): Promise<Blob | null> {
  const record = await runTransaction<CachedDrawingRecord | undefined>('readonly', (store) => store.get(partNo));
  return record?.blob ?? null;
}

export async function listCachedDrawingPartNos(): Promise<string[]> {
  const records = await runTransaction<CachedDrawingRecord[]>('readonly', (store) => store.getAll());
  return records.map((record) => record.partNo);
}

export async function syncDailyDrawings(partNos: string[]): Promise<{ success: number; failed: number }> {
  const uniquePartNos = [...new Set(partNos.map((partNo) => partNo.trim()).filter(Boolean))];
  let success = 0;
  let failed = 0;

  for (const partNo of uniquePartNos) {
    try {
      const blob = await fetchDrawingPdfBlob(drawingPdfUrl(partNo));
      await cacheDrawingPdf(partNo, blob);
      success += 1;
    } catch {
      failed += 1;
    }
  }

  return { success, failed };
}
