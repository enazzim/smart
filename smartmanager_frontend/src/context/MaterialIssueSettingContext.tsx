import { createContext, useContext, type ReactNode } from 'react';
import { DEFAULT_FISCAL_CUTOVER_SETTING } from '../utils/fiscalCalendar';

interface MaterialIssueSettingContextValue {
  materialIssueEnabled: boolean;
  setMaterialIssueEnabled: (enabled: boolean) => void;
  negativeStockAllowed: boolean;
  setNegativeStockAllowed: (allowed: boolean) => void;
  fiscalCutoverSetting: string;
  setFiscalCutoverSetting: (setting: string) => void;
}

const MaterialIssueSettingContext = createContext<MaterialIssueSettingContextValue | null>(null);

export function MaterialIssueSettingProvider({
  materialIssueEnabled,
  setMaterialIssueEnabled,
  negativeStockAllowed,
  setNegativeStockAllowed,
  fiscalCutoverSetting,
  setFiscalCutoverSetting,
  children,
}: MaterialIssueSettingContextValue & { children: ReactNode }) {
  return (
    <MaterialIssueSettingContext.Provider
      value={{
        materialIssueEnabled,
        setMaterialIssueEnabled,
        negativeStockAllowed,
        setNegativeStockAllowed,
        fiscalCutoverSetting,
        setFiscalCutoverSetting,
      }}
    >
      {children}
    </MaterialIssueSettingContext.Provider>
  );
}

export function useMaterialIssueSetting(): MaterialIssueSettingContextValue {
  const ctx = useContext(MaterialIssueSettingContext);
  if (!ctx) {
    return {
      materialIssueEnabled: false,
      setMaterialIssueEnabled: () => {},
      negativeStockAllowed: true,
      setNegativeStockAllowed: () => {},
      fiscalCutoverSetting: DEFAULT_FISCAL_CUTOVER_SETTING,
      setFiscalCutoverSetting: () => {},
    };
  }
  return ctx;
}
