import { useEffect, useState } from 'react';
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
      <h1>시스템 설정</h1>
      <p className="hint-text">
        전역 비즈니스 정책(Feature Flags)을 관리합니다. 자재투입 여부는 생산 워크플로에, 마이너스 재고 허용은
        모든 창고 입·출고 처리에 반영됩니다.
      </p>
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
    </div>
  );
}
