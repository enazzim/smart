import { useEffect, useState } from 'react';
import {
  createBackup,
  deleteBackup,
  downloadBackup,
  fetchBackups,
  restoreBackup,
  type BackupFileInfo,
} from '../api/systemBackup';
import {
  fetchSystemSettings,
  MATERIAL_ISSUE_ENABLED_LABELS,
  MRP_GROUPING_MODE_LABELS,
  SETTING_KEY_MATERIAL_ISSUE_ENABLED,
  SETTING_KEY_INVENTORY_ALLOW_NEGATIVE_STOCK,
  YES_NO_LABELS,
  updateSystemSetting,
  type SystemSetting,
} from '../api/systemSettings';
import { useMaterialIssueSetting } from '../context/MaterialIssueSettingContext';

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
  return value;
}

function isRadioSetting(settingKey: string): boolean {
  return (
    settingKey === 'mrp.grouping_mode' ||
    settingKey === 'production.material_issue.enabled' ||
    settingKey === SETTING_KEY_INVENTORY_ALLOW_NEGATIVE_STOCK
  );
}

function SettingsTable({
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
    return <p>관리 가능한 설정이 없습니다.</p>;
  }

  return (
    <table>
      <thead>
        <tr>
          <th>설정</th>
          <th>설명</th>
          <th>값</th>
          <th>최종 수정</th>
          <th>관리</th>
        </tr>
      </thead>
      <tbody>
        {settings.map((setting) => {
          const draftValue = draftValues[setting.settingKey] ?? setting.value;
          const dirty = draftValue !== setting.value;
          return (
            <tr key={setting.settingKey}>
              <td>{setting.label}</td>
              <td>{setting.description}</td>
              <td>
                {isRadioSetting(setting.settingKey) ? (
                  <div className="radio-group">
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
              </td>
              <td>
                {formatDateTime(setting.updatedAt)}
                {setting.updatedBy ? ` / ${setting.updatedBy}` : ''}
              </td>
              <td className="actions">
                <button
                  type="button"
                  disabled={!dirty || submittingKey === setting.settingKey}
                  onClick={() => onSave(setting)}
                >
                  {submittingKey === setting.settingKey ? '저장 중…' : '저장'}
                </button>
              </td>
            </tr>
          );
        })}
      </tbody>
    </table>
  );
}

function formatFileSize(bytes: number): string {
  if (bytes < 1024) return `${bytes} B`;
  if (bytes < 1024 * 1024) return `${(bytes / 1024).toFixed(1)} KB`;
  return `${(bytes / (1024 * 1024)).toFixed(1)} MB`;
}

function BackupPanel() {
  const [backups, setBackups] = useState<BackupFileInfo[]>([]);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const load = async () => {
    setLoading(true);
    setError(null);
    try {
      setBackups(await fetchBackups());
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

  const onCreate = async () => {
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      const created = await createBackup();
      setMessage(`백업을 저장했습니다: ${created.fileName}`);
      await load();
    } catch (e) {
      setError(e instanceof Error ? e.message : '백업 저장 실패');
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

  const onDelete = async (fileName: string) => {
    if (!window.confirm(`백업 파일을 삭제하시겠습니까?\n${fileName}`)) return;
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      await deleteBackup(fileName);
      setMessage(`삭제했습니다: ${fileName}`);
      await load();
    } catch (e) {
      setError(e instanceof Error ? e.message : '삭제 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onRestore = async (fileName: string) => {
    if (
      !window.confirm(
        `현재 데이터베이스가 백업 시점으로 덮어씌워집니다.\n복구 후 재로그인이 필요할 수 있습니다.\n\n적용하시겠습니까?\n${fileName}`,
      )
    ) {
      return;
    }
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      await restoreBackup(fileName);
      setMessage(`복구를 적용했습니다. 재로그인해 주세요. (${fileName})`);
    } catch (e) {
      setError(e instanceof Error ? e.message : '복구 실패');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <section className="panel">
      <h2>데이터 백업 및 복구</h2>
      <p className="hint-text">
        백업 파일은 프로젝트 <code>backup</code> 폴더에 저장됩니다. 복구(적용)는 현재 DB 전체를 덮어쓰므로
        로컬 개발 환경에서만 사용하세요.
      </p>
      <div className="form-actions">
        <button type="button" disabled={submitting} onClick={() => void onCreate()}>
          {submitting ? '처리 중…' : '백업 저장'}
        </button>
      </div>
      {message && <p>{message}</p>}
      {error && <div className="error">{error}</div>}
      {loading ? (
        <p>불러오는 중…</p>
      ) : backups.length === 0 ? (
        <p>저장된 백업이 없습니다.</p>
      ) : (
        <table>
          <thead>
            <tr>
              <th>파일명</th>
              <th>크기</th>
              <th>생성일시</th>
              <th>관리</th>
            </tr>
          </thead>
          <tbody>
            {backups.map((file) => (
              <tr key={file.fileName}>
                <td>{file.fileName}</td>
                <td>{formatFileSize(file.fileSizeBytes)}</td>
                <td>{formatDateTime(file.createdAt)}</td>
                <td className="actions">
                  <button type="button" disabled={submitting} onClick={() => void onDownload(file.fileName)}>
                    저장
                  </button>
                  <button type="button" disabled={submitting} onClick={() => void onRestore(file.fileName)}>
                    적용
                  </button>
                  <button type="button" disabled={submitting} onClick={() => void onDelete(file.fileName)}>
                    삭제
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </section>
  );
}

export default function SystemSettingsPage() {
  const { setMaterialIssueEnabled, setNegativeStockAllowed } = useMaterialIssueSetting();
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
      const rows = await fetchSystemSettings();
      setSettings(rows);
      setDraftValues(Object.fromEntries(rows.map((row) => [row.settingKey, row.value])));
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
      setMessage(`${updated.label} 설정을 저장했습니다.`);
    } catch (e) {
      setError(e instanceof Error ? e.message : '설정 저장 실패');
    } finally {
      setSubmittingKey(null);
    }
  };

  const inventorySettings = settings.filter((row) => row.settingKey.startsWith('inventory.'));
  const mrpSettings = settings.filter((row) => row.settingKey.startsWith('mrp.'));
  const productionSettings = settings.filter((row) => row.settingKey.startsWith('production.'));

  return (
    <div className="page">
      <header className="page-header">
        <div>
          <h1>시스템 설정</h1>
          <p>
            전역 비즈니스 정책(Feature Flags)을 관리합니다. 자재투입 여부는 생산 워크플로에, 마이너스 재고 허용은
            모든 창고 입·출고 처리에 반영됩니다.
          </p>
        </div>
      </header>
      {message && <p>{message}</p>}
      {error && <div className="error">{error}</div>}

      <section className="panel">
        <h2>재고</h2>
        {loading ? (
          <p>불러오는 중…</p>
        ) : (
          <SettingsTable
            settings={inventorySettings}
            draftValues={draftValues}
            submittingKey={submittingKey}
            onDraftChange={(key, value) => setDraftValues((prev) => ({ ...prev, [key]: value }))}
            onSave={(setting) => void onSave(setting)}
          />
        )}
      </section>

      <section className="panel">
        <h2>생산</h2>
        {loading ? (
          <p>불러오는 중…</p>
        ) : (
          <SettingsTable
            settings={productionSettings}
            draftValues={draftValues}
            submittingKey={submittingKey}
            onDraftChange={(key, value) => setDraftValues((prev) => ({ ...prev, [key]: value }))}
            onSave={(setting) => void onSave(setting)}
          />
        )}
      </section>

      <section className="panel">
        <h2>MRP</h2>
        {loading ? (
          <p>불러오는 중…</p>
        ) : (
          <SettingsTable
            settings={mrpSettings}
            draftValues={draftValues}
            submittingKey={submittingKey}
            onDraftChange={(key, value) => setDraftValues((prev) => ({ ...prev, [key]: value }))}
            onSave={(setting) => void onSave(setting)}
          />
        )}
      </section>

      <BackupPanel />
    </div>
  );
}
