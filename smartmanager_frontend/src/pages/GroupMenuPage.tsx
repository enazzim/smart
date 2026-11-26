import { useEffect, useMemo, useState } from 'react';
import {
  fetchGroupMenuSettings,
  saveGroupMenus,
  type GroupMenuAssignment,
  type GroupMenuItem,
} from '../api/groupMenu';

export default function GroupMenuPage() {
  const [groups, setGroups] = useState<GroupMenuAssignment[]>([]);
  const [menus, setMenus] = useState<GroupMenuItem[]>([]);
  const [groupId, setGroupId] = useState<number | null>(null);
  const [checked, setChecked] = useState<Set<string>>(new Set());
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  useEffect(() => {
    void fetchGroupMenuSettings()
      .then((settings) => {
        setGroups(settings.groups);
        setMenus(settings.menus);
        const first = settings.groups[0];
        if (first) {
          setGroupId(first.id);
          setChecked(new Set(first.menuIds));
        }
      })
      .catch((e: unknown) => {
        setError(e instanceof Error ? e.message : '그룹별 화면 조회 실패');
      })
      .finally(() => setLoading(false));
  }, []);

  const categories = useMemo(() => {
    const order: string[] = [];
    const byCategory = new Map<string, GroupMenuItem[]>();
    for (const menu of menus) {
      const rows = byCategory.get(menu.categoryLabel);
      if (rows) {
        rows.push(menu);
      } else {
        order.push(menu.categoryLabel);
        byCategory.set(menu.categoryLabel, [menu]);
      }
    }
    return order.map((label) => ({ label, items: byCategory.get(label) ?? [] }));
  }, [menus]);

  const selectGroup = (id: number) => {
    setGroupId(id);
    setMessage(null);
    const group = groups.find((row) => row.id === id);
    setChecked(new Set(group?.menuIds ?? []));
  };

  const toggle = (menuId: string, on: boolean) => {
    setChecked((prev) => {
      const next = new Set(prev);
      if (on) {
        next.add(menuId);
      } else {
        next.delete(menuId);
      }
      return next;
    });
  };

  const onSave = async () => {
    if (groupId == null) {
      return;
    }
    setSaving(true);
    setError(null);
    setMessage(null);
    const menuIds = menus.filter((menu) => checked.has(menu.menuId)).map((menu) => menu.menuId);
    try {
      await saveGroupMenus(groupId, menuIds);
      setGroups((prev) => prev.map((row) => (row.id === groupId ? { ...row, menuIds } : row)));
      setMessage('저장했습니다. 해당 그룹 사용자는 새로고침하면 반영됩니다.');
    } catch (e) {
      setError(e instanceof Error ? e.message : '저장 실패');
    } finally {
      setSaving(false);
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>그룹별 화면</h1>
        <p>업무일지 그룹에 보여줄 하위 메뉴를 선택합니다. 시스템 관리자는 이 설정과 관계없이 전체 메뉴를 봅니다.</p>
      </header>
      {error && <p className="error-banner">{error}</p>}
      {message && <p className="success-banner">{message}</p>}
      {loading ? (
        <p>불러오는 중…</p>
      ) : groups.length === 0 ? (
        <p>등록된 업무일지 그룹이 없습니다.</p>
      ) : (
        <section className="panel">
          <div className="group-menu-toolbar">
            <label className="group-menu-select">
              업무일지 그룹
              <select
                value={groupId ?? ''}
                onChange={(e) => selectGroup(Number(e.target.value))}
                disabled={saving}
              >
                {groups.map((group) => (
                  <option key={group.id} value={group.id}>
                    {group.name}
                  </option>
                ))}
              </select>
            </label>
            <div className="form-actions">
              <button type="button" disabled={saving || groupId == null} onClick={() => void onSave()}>
                {saving ? '저장 중…' : '저장'}
              </button>
            </div>
          </div>
          <div className="group-menu-groups">
            {categories.map((category) => (
              <fieldset key={category.label}>
                <legend>{category.label}</legend>
                <div className="group-menu-checks">
                  {category.items.map((menu) => (
                    <label key={menu.menuId} className="checkbox">
                      <input
                        type="checkbox"
                        checked={checked.has(menu.menuId)}
                        disabled={saving}
                        onChange={(e) => toggle(menu.menuId, e.target.checked)}
                      />
                      {menu.menuLabel}
                    </label>
                  ))}
                </div>
              </fieldset>
            ))}
          </div>
        </section>
      )}
    </div>
  );
}
