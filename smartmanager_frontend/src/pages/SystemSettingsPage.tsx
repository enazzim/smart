import { useEffect, useState } from 'react';
import {
  createBackup,
  createFullBackup,
  deleteBackup,
  deleteFullBackup,
  downloadBackup,
  fetchBackupList,
  restoreBackup,
  restoreFullBackup,
  type BackupListItem,
} from '../api/systemBackup';
import {
  fetchSystemSettings,
  FISCAL_CUTOVER_LAST,
  MATERIAL_ISSUE_ENABLED_LABELS,
  MRP_GROUPING_MODE_LABELS,
  SETTING_KEY_MATERIAL_ISSUE_ENABLED,
  SETTING_KEY_CLOSING_FISCAL_CUTOVER_DAY,
  SETTING_KEY_INVENTORY_ALLOW_NEGATIVE_STOCK,
  YES_NO_LABELS,
  updateSystemSetting,
  type SystemSetting,
} from '../api/systemSettings';
import {
  DEFAULT_FISCAL_CUTOVER_SETTING,
  formatFiscalCutoverSettingLabel,
  normalizeFiscalCutoverSetting,
} from '../utils/fiscalCalendar';
import { useMaterialIssueSetting } from '../context/MaterialIssueSettingContext';

const CLOSING_SETTING_ALLOWED_DAYS = [
  FISCAL_CUTOVER_LAST,
  ...Array.from({ length: 31 }, (_, index) => String(index + 1)),
];

const FALLBACK_CLOSING_SETTING: SystemSetting = {
  settingKey: SETTING_KEY_CLOSING_FISCAL_CUTOVER_DAY,
  label: '매입마감일',
  description:
    '거래일 기준 회계월 판정일. N일 이하 거래는 해당 월, 초과 거래는 익월. 매월 말일은 28~31일을 달마다 자동 적용합니다.',
  value: DEFAULT_FISCAL_CUTOVER_SETTING,
  allowedValues: CLOSING_SETTING_ALLOWED_DAYS,
  updatedAt: null,
  updatedBy: null,
};

function ensureClosingSetting(rows: SystemSetting[]): SystemSetting[] {
  if (rows.some((row) => row.settingKey === SETTING_KEY_CLOSING_FISCAL_CUTOVER_DAY)) {
    return rows;
  }
  return [FALLBACK_CLOSING_SETTING, ...rows];
}

function formatDateTime(value?: string | null): string {
  if (!value) {
    return '—';
  }
  const date = new Date(value);
  if (Number.isNaN(date.getTime())) {
    return value;
  }
  return date.toLocaleString('ko-KR');
}

function formatAllowedValue(settingKey: string, value: string): string {
  if (settingKey === 'mrp.grouping_mode') {
    return MRP_GROUPING_MODE_LABELS[value] ?? value;
  }
  if (settingKey === 'production.material_issue.enabled') {
    return MATERIAL_ISSUE_ENABLED_LABELS[value] ?? value;
  }
  if (settingKey === SETTING_KEY_INVENTORY_ALLOW_NEGATIVE_STOCK) {
    return YES_NO_LABELS[value] ?? value;
  }
  if (settingKey === SETTING_KEY_CLOSING_FISCAL_CUTOVER_DAY) {
    return formatFiscalCutoverSettingLabel(value);
  }
  return value;
}

function isRadioSetting(settingKey: string): boolean {
  return (
    settingKey === 'mrp.grouping_mode' ||
    settingKey === 'production.material_issue.enabled' ||
    settingKey === SETTING_KEY_INVENTORY_ALLOW_NEGATIVE_STOCK
  );
}

function SettingsList({
  settings,
  draftValues,
  submittingKey,
  onDraftChange,
  onSave,
}: {
  settings: SystemSetting[];
  draftValues: Record<string, string>;
  submittingKey: string | null;
  onDraftChange: (key: string, value: string) => void;
  onSave: (setting: SystemSetting) => void;
}) {
  if (settings.length === 0) {
    return <p className="settings-empty">관리 가능한 설정이 없습니다.</p>;
  }

  return (
    <ul className="settings-list">
      {settings.map((setting) => {
        const draftValue = draftValues[setting.settingKey] ?? setting.value;
        const dirty = draftValue !== setting.value;
        const meta = [
          setting.updatedAt ? formatDateTime(setting.updatedAt) : null,
          setting.updatedBy || null,
        ]
          .filter(Boolean)
          .join(' · ');
        return (
          <li key={setting.settingKey} className={`settings-item${dirty ? ' is-dirty' : ''}`}>
            <div className="settings-item-main">
              <div className="settings-item-copy">
                <strong>{setting.label}</strong>
                <p>{setting.description}</p>
                {meta ? <span className="settings-item-meta">{meta}</span> : null}
              </div>
              <div className="settings-item-controls">
                {isRadioSetting(setting.settingKey) ? (
                  <div className="radio-group radio-group-inline">
                    {setting.allowedValues.map((value) => (
                      <label key={value} className="radio-inline">
                        <input
                          type="radio"
                          name={setting.settingKey}
                          value={value}
                          checked={draftValue === value}
                          onChange={() => onDraftChange(setting.settingKey, value)}
                        />
                        {formatAllowedValue(setting.settingKey, value)}
                      </label>
                    ))}
                  </div>
                ) : (
                  <select
                    value={draftValue}
                    onChange={(e) => onDraftChange(setting.settingKey, e.target.value)}
                  >
                    {setting.allowedValues.map((value) => (
                      <option key={value} value={value}>
                        {formatAllowedValue(setting.settingKey, value)}
                      </option>
                    ))}
                  </select>
                )}
                <button
                  type="button"
                  disabled={!dirty || submittingKey === setting.settingKey}
                  onClick={() => onSave(setting)}
                >
                  {submittingKey === setting.settingKey ? '저장 중…' : '저장'}
                </button>
              </div>
            </div>
          </li>
        );
      })}
    </ul>
  );
}

function formatFileSize(bytes: number): string {
  if (bytes < 1024) return `${bytes} B`;
  if (bytes < 1024 * 1024) return `${(bytes / 1024).toFixed(1)} KB`;
  return `${(bytes / (1024 * 1024)).toFixed(1)} MB`;
}

function BackupPanel() {
  const [backups, setBackups] = useState<BackupListItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);
  const [createModalOpen, setCreateModalOpen] = useState(false);
  const [backupReason, setBackupReason] = useState('');

  const load = async () => {
    setLoading(true);
    setError(null);
    try {
      setBackups(await fetchBackupList());
    } catch (e) {
      setError(e instanceof Error ? e.message : '백업 목록 조회 실패');
      setBackups([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    void load();
  }, []);

  const openCreateModal = () => {
    setBackupReason('');
    setError(null);
    setCreateModalOpen(true);
  };

  const closeCreateModal = () => {
    if (submitting) return;
    setCreateModalOpen(false);
    setBackupReason('');
    setError(null);
  };

  const onCreateDb = async () => {
    const reason = backupReason.trim();
    if (!reason) {
      setError('백업 사유를 입력해 주세요.');
      return;
    }
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      const created = await createBackup(reason);
      setMessage(`DB 백업을 저장했습니다: ${created.fileName}`);
      setCreateModalOpen(false);
      setBackupReason('');
      await load();
    } catch (e) {
      setError(e instanceof Error ? e.message : '백업 저장 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onCreateFull = async () => {
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      const created = await createFullBackup();
      setMessage(
        `전체 백업을 저장했습니다: ${created.setName} (도면 PDF ${created.drawingPdfFileCount}건)`,
      );
      await load();
    } catch (e) {
      setError(e instanceof Error ? e.message : '전체 백업 저장 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDownload = async (fileName: string) => {
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      await downloadBackup(fileName);
      setMessage(`PC에 저장했습니다: ${fileName}`);
    } catch (e) {
      setError(e instanceof Error ? e.message : '백업 파일 저장 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async (item: BackupListItem) => {
    const label = item.kind === 'db-only' ? item.fileName : item.setName;
    if (!window.confirm(`백업을 삭제하시겠습니까?\n${label}`)) return;
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      if (item.kind === 'db-only') {
        await deleteBackup(item.fileName);
      } else {
        await deleteFullBackup(item.setName);
      }
      setMessage(`삭제했습니다: ${label}`);
      await load();
    } catch (e) {
      setError(e instanceof Error ? e.message : '삭제 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onRestore = async (item: BackupListItem) => {
    const label = item.kind === 'db-only' ? item.fileName : item.setName;
    const confirmMessage =
      item.kind === 'full'
        ? `현재 데이터베이스와 도면 PDF가 백업 시점으로 덮어씌워집니다.\n복구 중 도면 업로드·삭제를 하지 마세요.\n복구 후 재로그인이 필요할 수 있습니다.\n\n적용하시겠습니까?\n${label}`
        : `현재 데이터베이스가 백업 시점으로 덮어씌워집니다.\n복구 후 재로그인이 필요할 수 있습니다.\n\n적용하시겠습니까?\n${label}`;
    if (!window.confirm(confirmMessage)) {
      return;
    }
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      if (item.kind === 'db-only') {
        await restoreBackup(item.fileName);
      } else {
        await restoreFullBackup(item.setName);
      }
      setMessage(`복구를 적용했습니다. 재로그인해 주세요. (${label})`);
    } catch (e) {
      setError(e instanceof Error ? e.message : '복구 실패');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <section className="panel">
      <div className="panel-header-row">
        <h2>데이터 백업 및 복구</h2>
        <div className="inline-actions">
          <button type="button" className="secondary" disabled={submitting} onClick={openCreateModal}>
            {submitting ? '처리 중…' : 'DB 백업'}
          </button>
          <button type="button" disabled={submitting} onClick={() => void onCreateFull()}>
            {submitting ? '처리 중…' : '전체 백업'}
          </button>
        </div>
      </div>
      <p className="hint-text">
        프로젝트 <code>backup</code> 폴더에 저장됩니다. 전체 백업은 DB+도면 PDF 세트이며, 복구는 현재 데이터를
        덮어씁니다.
      </p>
      {message && <p className="settings-toast">{message}</p>}
      {error && !createModalOpen && <div className="error">{error}</div>}
      {loading ? (
        <p>불러오는 중…</p>
      ) : backups.length === 0 ? (
        <p className="settings-empty">저장된 백업이 없습니다.</p>
      ) : (
        <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>구분</th>
                <th>이름</th>
                <th>백업 사유</th>
                <th>크기</th>
                <th>생성일시</th>
                <th>관리</th>
              </tr>
            </thead>
            <tbody>
              {backups.map((item) => {
                const key = item.kind === 'db-only' ? item.fileName : item.setName;
                const label = item.kind === 'db-only' ? item.fileName : item.setName;
                const size = item.kind === 'db-only' ? item.fileSizeBytes : item.totalSizeBytes;
                const typeLabel =
                  item.kind === 'db-only' ? 'DB만' : `전체 (PDF ${item.drawingPdfFileCount}건)`;
                const reason = item.kind === 'db-only' ? item.reason?.trim() || '—' : '—';
                return (
                  <tr key={key}>
                    <td>{typeLabel}</td>
                    <td>{label}</td>
                    <td>{reason}</td>
                    <td>{formatFileSize(size)}</td>
                    <td>{formatDateTime(item.createdAt)}</td>
                    <td className="actions">
                      {item.kind === 'db-only' && (
                        <button type="button" disabled={submitting} onClick={() => void onDownload(item.fileName)}>
                          저장
                        </button>
                      )}
                      <button type="button" disabled={submitting} onClick={() => void onRestore(item)}>
                        적용
                      </button>
                      <button
                        type="button"
                        className="danger"
                        disabled={submitting}
                        onClick={() => void onDelete(item)}
                      >
                        삭제
                      </button>
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        </div>
      )}
      {createModalOpen && (
        <div className="modal-backdrop" role="presentation" onClick={closeCreateModal}>
          <div className="modal" onClick={(e) => e.stopPropagation()}>
            <h2>DB 백업 저장</h2>
            <div className="form-grid">
              <label>
                백업 사유 *
                <input
                  value={backupReason}
                  maxLength={500}
                  placeholder="예: A/S 품목 납품가능, 마이그레이션 적용 전"
                  onChange={(e) => setBackupReason(e.target.value)}
                  autoFocus
                />
              </label>
            </div>
            {error && <div className="error">{error}</div>}
            <div className="form-actions">
              <button type="button" disabled={submitting} onClick={() => void onCreateDb()}>
                {submitting ? '저장 중…' : '저장'}
              </button>
              <button type="button" className="secondary" disabled={submitting} onClick={closeCreateModal}>
                취소
              </button>
            </div>
          </div>
        </div>
      )}
    </section>
  );
}

export default function SystemSettingsPage() {
  const { setMaterialIssueEnabled, setNegativeStockAllowed, setFiscalCutoverSetting } = useMaterialIssueSetting();
  const [settings, setSettings] = useState<SystemSetting[]>([]);
  const [draftValues, setDraftValues] = useState<Record<string, string>>({});
  const [loading, setLoading] = useState(true);
  const [submittingKey, setSubmittingKey] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const load = async () => {
    setLoading(true);
    setError(null);
    try {
      const rows = ensureClosingSetting(await fetchSystemSettings());
      setSettings(rows);
      setDraftValues(Object.fromEntries(rows.map((row) => [row.settingKey, row.value])));
      const closingRow = rows.find((row) => row.settingKey === SETTING_KEY_CLOSING_FISCAL_CUTOVER_DAY);
      if (closingRow) {
        setFiscalCutoverSetting(normalizeFiscalCutoverSetting(closingRow.value));
      }
    } catch (e) {
      setError(e instanceof Error ? e.message : '시스템 설정 조회 실패');
      setSettings([]);
      setDraftValues({});
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    void load();
  }, []);

  const onSave = async (setting: SystemSetting) => {
    const nextValue = draftValues[setting.settingKey];
    if (!nextValue || nextValue === setting.value) {
      return;
    }
    setSubmittingKey(setting.settingKey);
    setError(null);
    setMessage(null);
    try {
      const updated = await updateSystemSetting(setting.settingKey, nextValue);
      setSettings((prev) => prev.map((row) => (row.settingKey === updated.settingKey ? updated : row)));
      setDraftValues((prev) => ({ ...prev, [updated.settingKey]: updated.value }));
      if (updated.settingKey === SETTING_KEY_MATERIAL_ISSUE_ENABLED) {
        setMaterialIssueEnabled(updated.value === 'YES');
      }
      if (updated.settingKey === SETTING_KEY_INVENTORY_ALLOW_NEGATIVE_STOCK) {
        setNegativeStockAllowed(updated.value !== 'NO');
      }
      if (updated.settingKey === SETTING_KEY_CLOSING_FISCAL_CUTOVER_DAY) {
        setFiscalCutoverSetting(normalizeFiscalCutoverSetting(updated.value));
      }
      setMessage(`${updated.label} 설정을 저장했습니다.`);
    } catch (e) {
      setError(e instanceof Error ? e.message : '설정 저장 실패');
    } finally {
      setSubmittingKey(null);
    }
  };

  const closingSettings = settings.filter((row) => row.settingKey.startsWith('closing.'));
  const inventorySettings = settings.filter((row) => row.settingKey.startsWith('inventory.'));
  const mrpSettings = settings.filter((row) => row.settingKey.startsWith('mrp.'));
  const productionSettings = settings.filter((row) => row.settingKey.startsWith('production.'));

  const settingSections = [
    { key: 'closing', title: '회계마감', settings: closingSettings },
    { key: 'inventory', title: '재고', settings: inventorySettings },
    { key: 'production', title: '생산', settings: productionSettings },
    { key: 'mrp', title: 'MRP', settings: mrpSettings },
  ] as const;

  return (
    <div className="page system-settings-page">
      <header className="page-header">
        <div>
          <h1>시스템 설정</h1>
          <p>전역 비즈니스 정책(Feature Flags)을 관리합니다.</p>
        </div>
      </header>
      {message && <p className="settings-toast">{message}</p>}
      {error && <div className="error">{error}</div>}

      {loading ? (
        <section className="panel">
          <p>불러오는 중…</p>
        </section>
      ) : (
        settingSections
          .filter((section) => section.settings.length > 0)
          .map((section) => (
            <section key={section.key} className="panel">
              <h2>{section.title}</h2>
              <SettingsList
                settings={section.settings}
                draftValues={draftValues}
                submittingKey={submittingKey}
                onDraftChange={(key, value) => setDraftValues((prev) => ({ ...prev, [key]: value }))}
                onSave={(setting) => void onSave(setting)}
              />
            </section>
          ))
      )}

      <BackupPanel />
    </div>
  );
}
