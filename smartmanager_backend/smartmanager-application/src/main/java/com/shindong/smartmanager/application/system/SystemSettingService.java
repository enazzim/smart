package com.shindong.smartmanager.application.system;

import com.shindong.smartmanager.application.closing.FiscalCutoverPolicy;
import com.shindong.smartmanager.domain.production.MrpGroupingMode;
import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import java.util.stream.IntStream;

public class SystemSettingService {

    public static final String KEY_CLOSING_FISCAL_CUTOVER_DAY = "closing.fiscal_cutover_day";
    public static final String KEY_MRP_GROUPING_MODE = "mrp.grouping_mode";
    public static final String KEY_PRODUCTION_MATERIAL_ISSUE_ENABLED = "production.material_issue.enabled";
    public static final String KEY_INVENTORY_ALLOW_NEGATIVE_STOCK = "inventory.allow_negative_stock";

    private static final String VALUE_YES = "YES";
    private static final String VALUE_NO = "NO";

    private static final Map<String, SettingDefinition> DEFINITIONS;

    static {
        DEFINITIONS = new LinkedHashMap<>();
        DEFINITIONS.put(
                KEY_CLOSING_FISCAL_CUTOVER_DAY,
                new SettingDefinition(
                        "매입마감일",
                        "거래일 기준 회계월 판정일. N일 이하 거래는 해당 월, 초과 거래는 익월. 매월 말일은 28~31일을 달마다 자동 적용",
                        fiscalCutoverAllowedValues(),
                        FiscalCutoverPolicy.DEFAULT_SETTING_VALUE
                )
        );
        DEFINITIONS.put(
                KEY_MRP_GROUPING_MODE,
                new SettingDefinition(
                        "MRP 자재소요 집계 모드",
                        "자재소요 목록·구매발주 시 표시/집계 단위",
                        List.of("BY_PLAN", "BY_COMPONENT"),
                        "BY_PLAN"
                )
        );
        DEFINITIONS.put(
                KEY_PRODUCTION_MATERIAL_ISSUE_ENABLED,
                new SettingDefinition(
                        "자재투입 여부",
                        "예: 작업계획→작업지시→자재투입→작업일보. 아니오: 작업일보 등록 시 BOM 자재를 자동 차감(백플러시)",
                        List.of(VALUE_YES, VALUE_NO),
                        VALUE_NO
                )
        );
        DEFINITIONS.put(
                KEY_INVENTORY_ALLOW_NEGATIVE_STOCK,
                new SettingDefinition(
                        "마이너스 재고 허용",
                        "예: 출고·투입 등으로 창고 수량이 0 미만이 되어도 허용. 아니오: 재고 부족 시 처리를 거부",
                        List.of(VALUE_YES, VALUE_NO),
                        VALUE_YES
                )
        );
    }

    private final SystemSettingRepository systemSettingRepository;

    public SystemSettingService(SystemSettingRepository systemSettingRepository) {
        this.systemSettingRepository = systemSettingRepository;
    }

    public List<SystemSettingItemView> listManagedSettings() {
        Map<String, SystemSettingView> stored = new LinkedHashMap<>();
        for (SystemSettingView view : systemSettingRepository.findAllActive()) {
            stored.put(view.settingKey(), view);
        }

        return DEFINITIONS.entrySet().stream()
                .map(entry -> {
                    String key = entry.getKey();
                    SettingDefinition definition = entry.getValue();
                    SystemSettingView storedView = stored.get(key);
                    String value = storedView != null
                            ? parseJsonString(storedView.valueJson())
                            : definition.defaultValue();
                    return new SystemSettingItemView(
                            key,
                            definition.label(),
                            definition.description(),
                            value,
                            definition.allowedValues(),
                            storedView != null ? storedView.updatedAt() : null,
                            storedView != null ? storedView.updatedBy() : null
                    );
                })
                .toList();
    }

    public SystemSettingItemView updateSetting(String settingKey, String value, String actorUserId) {
        SettingDefinition definition = DEFINITIONS.get(settingKey);
        if (definition == null) {
            throw new IllegalArgumentException("수정할 수 없는 설정 키입니다: " + settingKey);
        }
        String normalized = normalizeValue(settingKey, value);
        if (!definition.allowedValues().contains(normalized)) {
            throw new IllegalArgumentException("허용되지 않는 설정 값입니다: " + value);
        }
        systemSettingRepository.upsert(settingKey, toJsonString(normalized), actorUserId);
        return listManagedSettings().stream()
                .filter(item -> item.settingKey().equals(settingKey))
                .findFirst()
                .orElseThrow();
    }

    public MrpGroupingMode resolveMrpGroupingMode() {
        return systemSettingRepository.findActiveByKey(KEY_MRP_GROUPING_MODE)
                .map(view -> MrpGroupingMode.fromValue(parseJsonString(view.valueJson())))
                .orElse(MrpGroupingMode.BY_PLAN);
    }

    public boolean isMaterialIssueEnabled() {
        return systemSettingRepository.findActiveByKey(KEY_PRODUCTION_MATERIAL_ISSUE_ENABLED)
                .map(view -> VALUE_YES.equalsIgnoreCase(parseJsonString(view.valueJson())))
                .orElse(false);
    }

    public boolean isNegativeStockAllowed() {
        return systemSettingRepository.findActiveByKey(KEY_INVENTORY_ALLOW_NEGATIVE_STOCK)
                .map(view -> VALUE_YES.equalsIgnoreCase(parseJsonString(view.valueJson())))
                .orElse(true);
    }

    public String resolveFiscalCutoverSettingValue() {
        return systemSettingRepository.findActiveByKey(KEY_CLOSING_FISCAL_CUTOVER_DAY)
                .map(view -> FiscalCutoverPolicy.normalizeSettingValue(parseJsonString(view.valueJson())))
                .orElse(FiscalCutoverPolicy.DEFAULT_SETTING_VALUE);
    }

    private String normalizeValue(String settingKey, String value) {
        if (KEY_MRP_GROUPING_MODE.equals(settingKey)) {
            return MrpGroupingMode.fromValue(value).name();
        }
        if (KEY_PRODUCTION_MATERIAL_ISSUE_ENABLED.equals(settingKey)) {
            return normalizeYesNo(value, "자재투입 여부");
        }
        if (KEY_INVENTORY_ALLOW_NEGATIVE_STOCK.equals(settingKey)) {
            return normalizeYesNo(value, "마이너스 재고 허용");
        }
        if (KEY_CLOSING_FISCAL_CUTOVER_DAY.equals(settingKey)) {
            return FiscalCutoverPolicy.normalizeSettingValue(value);
        }
        if (value == null || value.isBlank()) {
            throw new IllegalArgumentException("설정 값을 입력하세요.");
        }
        return value.trim();
    }

    static String parseJsonString(String json) {
        if (json == null || json.isBlank()) {
            return "";
        }
        String trimmed = json.trim();
        if (trimmed.startsWith("\"") && trimmed.endsWith("\"") && trimmed.length() >= 2) {
            return trimmed.substring(1, trimmed.length() - 1);
        }
        return trimmed;
    }

    static String toJsonString(String value) {
        return "\"" + value.replace("\\", "\\\\").replace("\"", "\\\"") + "\"";
    }

    private static List<String> fiscalCutoverAllowedValues() {
        List<String> values = new ArrayList<>();
        values.add(FiscalCutoverPolicy.VALUE_LAST);
        values.addAll(IntStream.rangeClosed(1, 31).mapToObj(String::valueOf).toList());
        return values;
    }

    private static String normalizeYesNo(String value, String fieldLabel) {
        String normalized = value != null ? value.trim().toUpperCase() : "";
        if (VALUE_YES.equals(normalized) || "Y".equals(normalized) || "예".equals(value)) {
            return VALUE_YES;
        }
        if (VALUE_NO.equals(normalized) || "N".equals(normalized) || "아니오".equals(value)) {
            return VALUE_NO;
        }
        throw new IllegalArgumentException(fieldLabel + "은(는) YES(예) 또는 NO(아니오)만 허용됩니다.");
    }

    private record SettingDefinition(
            String label,
            String description,
            List<String> allowedValues,
            String defaultValue
    ) {
    }
}
